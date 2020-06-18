using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        private readonly IOrderRepository _orderRepo;
        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, IPaymentService paymentService, IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress, bool paymentMethod, DateTimeOffset shippingDate)
        {
            // get basket
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // get items from meal repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                // Get meal - but just for Id since it won't grab photos
                var mealItem = await _unitOfWork.Repository<Meal>().GetByIdAsync(item.Id);
                // Create Spec cause we need to get Meals with Photos or Photo is null when viewed in Orders
                var mealspec = new MealsWithTypesAndMenusSpecification(mealItem.Id);
                // Get meal with Photos and other entitys
                var meal = await _unitOfWork.Repository<Meal>().GetEnitityWithSpec(mealspec);
                // Update Stock
                meal.Stock -= item.Quantity;
                _unitOfWork.Repository<Meal>().Update(meal);
                // Create Item Ordered
                var itemOrdered = new MealItemOrdered(meal.Id, meal.Name, meal.Photos.FirstOrDefault(x => x.IsMain)?.PictureUrl);
                // Add Ingrediants and Quantity
                var itemIngrediantsOrdered = new List<OrderItemIngrediant>();
                foreach (var ingrediant in item.Ingrediants)
                {
                    // If ingrediant Quantity is greater than default value then add to list
                    if (ingrediant.Quantity != meal.Ingrediants.FirstOrDefault(x => x.Id == ingrediant.Id).Quantity)
                    {
                        // Get ingrediant from repo
                        var repoIngrediant = meal.Ingrediants.FirstOrDefault(x => x.Id == ingrediant.Id);
                        // Get Quantity
                        var quantity = ingrediant.Quantity;
                        var ingrediantToAdd = new OrderItemIngrediant(quantity, repoIngrediant.Price, repoIngrediant.Name);
                        itemIngrediantsOrdered.Add(ingrediantToAdd);
                    }
                }
                // Create orderItem
                var orderItem = new OrderItem(itemOrdered, meal.Price, item.Quantity, itemIngrediantsOrdered);
                // Add extra ingrediant price to total order Price
                foreach (var ingrediant in orderItem.Ingrediants)
                {
                    if (ingrediant.Quantity > 1)
                    {
                        orderItem.Price += ingrediant.Price * (ingrediant.Quantity);
                    }
                    else
                        orderItem.Price -= ingrediant.Price;
                }
                // Add to list
                items.Add(orderItem);
            }

            // get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            // check to see if order exists for orders done by credit card
            if (paymentMethod)
            {
                var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
                var existingOrder = await _unitOfWork.Repository<Order>().GetEnitityWithSpec(spec);

                if (existingOrder != null)
                {
                    _unitOfWork.Repository<Order>().Delete(existingOrder);
                    await _paymentService.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
                }
            }


            // create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal, basket.PaymentIntentId, paymentMethod, shippingDate);

            // edit status for payroll
            if (!paymentMethod)
            {
                order.Status = OrderStatus.PayRollPending;
            }

            _unitOfWork.Repository<Order>().Add(order);

            // save to db
            var result = await _unitOfWork.Complete();

            if (result < 0) return null;

            // return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            return await _orderRepo.GetOrderAsync(id, buyerEmail);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            return await _orderRepo.GetOrdersAsync(buyerEmail);
        }
    }
}