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
        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
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
                // Create Item Ordered
                var itemOrdered = new MealItemOrdered(meal.Id, meal.Name, meal.Photos.FirstOrDefault(x => x.IsMain)?.PictureUrl);
                // Create orderItem
                var orderItem = new OrderItem(itemOrdered, meal.Price, item.Quantity, item.Ingrediants);
                // Add extra ingrediant price to total order Price
                foreach (var ingrediant in orderItem.Ingrediants)
                {
                    orderItem.Price += ingrediant.Price * ingrediant.Quantity;
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
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);
            return await _unitOfWork.Repository<Order>().GetEnitityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}