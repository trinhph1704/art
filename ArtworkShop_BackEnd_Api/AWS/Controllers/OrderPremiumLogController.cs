using AWS.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPremiumLogController : ControllerBase
    {
        private readonly IOrderPremiumLog orderPremiumLog;

        public OrderPremiumLogController(IOrderPremiumLog orderPremiumLog)
        {
            this.orderPremiumLog = orderPremiumLog;
        }

        [HttpGet]
        [Route("get-all")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var a = await this.orderPremiumLog.GetPaymentList();
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetPaymentList method: {ex}");

                throw;
            }

        }

        [HttpDelete]
        [Route("Delete")]

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var a = await this.orderPremiumLog.DeleteOrPreLog(id);
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

        [HttpGet]
        [Route("get-OrderPre-By-LogId")]

        public async Task<IActionResult> GetOrderPreByLog(string id)
        {
            try
            {
                var a = await this.orderPremiumLog.GetPaymentLogByOrderPreId(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetOrderPreByLog method: {ex}");

                throw;
            }

        }

        [HttpPost]
        [Route("Update-Status")]

        public async Task<IActionResult> UpdateStatus(string id)
        {
            try
            {
                var a = await this.orderPremiumLog.UpdateStatusSuccess(id);
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
        [Route("create-new-Premium-log")]

        public async Task<IActionResult> CreateNew(string id)
        {
            try
            {
                var a = await this.orderPremiumLog.createPayment(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the CreateNew method: {ex}");

                throw;
            }

        }

    }
}
