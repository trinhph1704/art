using AWS.DTO;
using AWS.DTO.Order;
using AWS.DTO.Premium;
using AWS.Models;
using AWS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AWS.Repositories.Services
{
    public class SPremium:IPremium
    {
        private readonly ARTWORKPLATFORMContext cxt;
        public SPremium(ARTWORKPLATFORMContext cxt)
        {
            this.cxt = cxt;
        }

        //public async Task<OrderPremium> CreateNewOrderPremium(string OrderPremiumId)
        //{
        //    try
        //    {
        //        var add = new OrderPremium();
        //        add.OrderPremiumId = "OP" + Guid.NewGuid().ToString().Substring(0, 6);
        //        add.PremiumId = OrderPremiumId;
        //        add.OrderDate = DateTime.Now;
        //        add.Status = false;

        //        var premium = await cxt.OrderPremia.FindAsync(OrderPremiumId);
        //        if (premium != null)
        //        {
        //            //add.Total = premium.Price; // Gán giá trị Price từ Artwork cho đơn hàng
        //        }

        //        await this.cxt.OrderPremia.AddAsync(add);
        //        await this.cxt.SaveChangesAsync();
        //        OrderPremium OrderPremiumID = null;
        //        return OrderPremiumID ;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<List<Premium>> GetAll()
        {
            try
            {
                var list = await this.cxt.Premia.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //public async Task<OrderPremium> GetOrderPremium(string OrderPremiumId)
        //{
        //    try
        //    {
        //        var a = await this.cxt.OrderPremia
        //    .Where(x => x.OrderPremiumId.Equals(OrderPremiumId))
        //    .FirstOrDefaultAsync();

        //        return a;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
