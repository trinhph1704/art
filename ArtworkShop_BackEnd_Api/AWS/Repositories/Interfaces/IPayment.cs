using AWS.Models;

namespace AWS.Repositories.Interfaces
{
    public interface IPayment
    {
        Task<Payment> createPayment(string OrderId);
        Task<Payment> UpdatePayment(string OrderId);
        Task<Payment> GetPaymentFail(string OrderId);
        Task<Payment> GetPaymentSuccess(string OrderId);
        Task<Payment> GetPayment(string OrderId);
        Task<List<Payment>> GetPaymentList();
        Task<Payment> DeletePayment(string paymentID);

    }
}
