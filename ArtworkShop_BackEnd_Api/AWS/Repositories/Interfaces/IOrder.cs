using AWS.DTO.Order;
using AWS.Models;
using AWS.Repositories.Services;

namespace AWS.Repositories.Interfaces
{
    public interface IOrder
    {
        Task<Ordertb> GetOrderById(string id);
        Task<Ordertb> GetOrderByStatusTrue(string id);
        Task<Ordertb> GetOrderByStatusFalse(string id);
        Task<List<Ordertb>> GetAll();
        Task<Ordertb> CreateNewOrder(CreateOrderDTO order);
        Task<Ordertb> UpdateOrder(string orderId);
        Task<Ordertb> DeleteOrder(string orderId);
    }
}
