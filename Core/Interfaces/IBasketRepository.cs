using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> UpdateBastketAsync(CustomerBasket basket);
        Task<bool> DeleteBastketAsync(string basketId);
    }
}