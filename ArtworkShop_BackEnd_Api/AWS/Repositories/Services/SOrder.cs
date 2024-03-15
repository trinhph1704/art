using AWS.DTO.ArtworkDTO;
using AWS.DTO.Order;
using AWS.Models;
using AWS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace AWS.Repositories.Services
{
    public class SOrder : IOrder
    {
        private readonly ARTWORKPLATFORMContext cxt;

        public SOrder(ARTWORKPLATFORMContext cxt)
        {
            this.cxt = cxt;
        }

        public async Task<Ordertb> CreateNewOrder(CreateOrderDTO order)
        {
            try
            {
                var add = new Ordertb();
                add.OrderId = "O" + Guid.NewGuid().ToString().Substring(0, 6);
                add.ArtworkId = order.ArtwokID;
                add.UserId = order.UserID;
                add.CreateDate = order.CreateDate;
                add.Status = false;
                add.StatusCancel = true;

                var artwork = await cxt.Artworks.FindAsync(order.ArtwokID);
                if (artwork != null)
                {
                    add.Total = artwork.Price; // Gán giá trị Price từ Artwork cho đơn hàng
                }

                await this.cxt.Ordertbs.AddAsync(add);
                await this.cxt.SaveChangesAsync();
                return add;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
     
        }

        public async Task<Ordertb> DeleteOrder(string orderId)
        {
            try
            {
                if (orderId != null)
                {
                    var obj = await this.cxt.Ordertbs.Where(x => x.OrderId.Equals(orderId)).FirstOrDefaultAsync();
                    obj.StatusCancel = false;
                    this.cxt.Ordertbs.Update(obj);
                    await this.cxt.SaveChangesAsync();
                    return obj;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<List<Ordertb>> GetAll()
        {
            try
            {
                var list = await this.cxt.Ordertbs.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Ordertb> GetOrderById(string id)
        {
            try
            {
                var a = await this.cxt.Ordertbs.Where(x => x.OrderId.Equals(id)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex) 
            {


                throw new Exception(ex.Message);
            }
          
        }

        public async Task<Ordertb> GetOrderByStatusFalse(string id)
        {
            try
            {
                var a = await this.cxt.Ordertbs.Where(x => x.Status==false && x.OrderId.Equals(id)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }
        }

        public async Task<Ordertb> GetOrderByStatusTrue(string id)
        {
             try
            {
                var a = await this.cxt.Ordertbs.Where(x => x.Status == true && x.OrderId.Equals(id)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }
        }

        public async Task<Ordertb> UpdateOrder(string orderId)
        {
            try
            {
                // Find the payment associated with the paymentId
                //var payment = await cxt.Payments.FirstOrDefaultAsync(p => p.PaymentId == paymentID);

                // Find the order associated with the payment
                var order = await cxt.Ordertbs.FirstOrDefaultAsync(o => o.OrderId == orderId);
           
                // Update order status based on payment status
               
                    order.Status = true; // Assuming true means paid
              
                

                cxt.Ordertbs.Update(order);
                await cxt.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
