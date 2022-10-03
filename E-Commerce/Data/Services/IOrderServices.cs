using E_Commerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce.Data.Services
{
    public interface IOrderServices
    {
        Task StoreOrderAsync(List<CartItem> items, string userId);
        Task<List<order>> GetOrderByUserIdAsync(string userId);
    }
}
