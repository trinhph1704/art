using AWS.DTO;
using AWS.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPremiumController : ControllerBase
    {
        private readonly IOrderPremium orderPremium;

        public OrderPremiumController(IOrderPremium orderPremium)
        {
            this.orderPremium = orderPremium;
        }

        [HttpGet]
        [Route("get-all")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var a = await this.orderPremium.GetAll();
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetAll method: {ex}");

                throw;
            }

        }

        [HttpDelete]
        [Route("Delete")]

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var a = await this.orderPremium.DeleteOrPre(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the Delete method: {ex}");

                throw;
            }

        }

        [HttpPost]
        [Route("update-status")]

        public async Task<IActionResult> UpdateStatus(string id)
        {
            try
            {
                var a = await this.orderPremium.UpdateStatus(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the UpdateStatus method: {ex}");

                throw;
            }

        }

        [HttpPost]
        [Route("get-orderid-update-premiumid-in-user")]

        public async Task<IActionResult> GetAndUpdatePremiumByOrderStatusTrue(string OrderPreId)
        {
            try
            {
                var a = await this.orderPremium.GetAndUpdatePremiumByOrderStatusTrue(OrderPreId);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetAndUpdatePremiumByOrderStatusTrue method: {ex}");

                throw;
            }

        }

        [HttpGet]
        [Route("get-order-premeium-by-id")]

        public async Task<IActionResult> GetByID(string id)
        {
            try
            {
                var a = await this.orderPremium.GetByID(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetByID method: {ex}");

                throw;
            }

        }

        [HttpPost]
        [Route("add-new-order-premeium")]

        public async Task<IActionResult> AddNewOrder(CreateOrderPremiumDTO create)
        {
            try
            {
                var a = await this.orderPremium.AddNewOrder(create);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the AddNewOrder method: {ex}");

                throw;
            }

        }
    }
}
