using AWS.Models;

namespace AWS.Repositories.Interfaces
{
    public interface IOrderPremiumLog
    {
        Task<List<OrderPremiumLog>> GetPaymentList();
        Task<OrderPremiumLog> GetPaymentLogByOrderPreId(string OrPreId);
        Task<OrderPremiumLog> UpdateStatusSuccess(string LogId);
        Task<OrderPremiumLog> createPayment(string OrderPremiumId);
        Task<bool> DeleteOrPreLog(string OrderPreLogId);


    }
}
