using AWS.DTO;
using AWS.Models;

namespace AWS.Repositories.Interfaces
{
    public interface IOrderPremium
    {
        Task<List<OrderPremium>> GetAll();
        Task<OrderPremium> GetByID(string id); 
        Task<OrderPremium> UpdateStatus(string OrderPreId); 
        Task<OrderPremium> GetAndUpdatePremiumByOrderStatusTrue(string OrderPreId); 
        Task<bool> DeleteOrPre(string OrderPreId); 
        Task<OrderPremium> AddNewOrder(CreateOrderPremiumDTO create); 

        
    }
}
