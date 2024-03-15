using AWS.Models;
using AWS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AWS.Repositories.Services
{
    public class SPayment : IPayment
    {
        private readonly ARTWORKPLATFORMContext context;

        public SPayment(ARTWORKPLATFORMContext context)
        {
            this.context = context;
        }

        public async Task<Payment> createPayment(string OrderId)
        {
            try
            { 
                var payment = new Payment();
                var order = await this.context.Ordertbs
                              .Where(x => x.OrderId.Equals(OrderId))
                              .FirstOrDefaultAsync();
                if (order != null)
                {
                    payment.PaymentId = "P" + Guid.NewGuid().ToString().Substring(0, 8);
                    payment.OrderId = order.OrderId;
                    payment.CreateDate = DateTime.Now;
                    payment.Amount = order.Total;
                    payment.Status = false;
                    payment.StatusCancle = true;
                    await this.context.Payments.AddAsync(payment);
                    await this.context.SaveChangesAsync();
                    return payment;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Payment> DeletePayment(string paymentID)
        {
            try
            {
                if (paymentID != null)
                {
                    var obj = await this.context.Payments.Where(x => x.PaymentId.Equals(paymentID)).FirstOrDefaultAsync();
                    obj.StatusCancle = false;   
                    this.context.Payments.Update(obj);
                    await this.context.SaveChangesAsync();
                    return obj;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Payment> GetPayment(string OrderId)
        {
            try
            {
                var payment = await this.context.Payments
                                .Where(x => x.OrderId.Equals(OrderId))
                                .FirstOrDefaultAsync();
                if (payment != null)
                {
                    return payment;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Payment> GetPaymentFail(string OrderId)
        {
            try
            {
                var payment = await this.context.Payments
                                .Where(x => x.OrderId.Equals(OrderId) && x.Status==false)
                                .FirstOrDefaultAsync();
                if (payment != null)
                {
                    return payment;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Payment>> GetPaymentList()
        {
            try
            {
                var list = await this.context.Payments.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Payment> GetPaymentSuccess(string OrderId)
        {
            try
            {
                var payment = await this.context.Payments
                                .Where(x => x.OrderId.Equals(OrderId) && x.Status == true)
                                .FirstOrDefaultAsync();
                if (payment != null)
                {
                    return payment;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Payment> UpdatePayment(string id)
        {

            try
            {
                // Retrieve the artwork from the database
                var payment = await context.Payments.FindAsync(id);

                if (payment == null)
                {
                    throw new Exception($"Artwork with ID {payment} not found.");
                }

                // Update the artwork properties
                payment.Status = true;


                // Update the artwork in the database
                context.Payments.Update(payment);
                await context.SaveChangesAsync();

                return payment;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while updating order.", e);
            }
        }
    }
}
