using AWS.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment payment;

        public PaymentController(IPayment payment)
        {
            this.payment = payment;
        }


        [HttpPost]
        [Route("create-new-payment")]

        public async Task<IActionResult> CreatePayment(string id)
        {
            try
            {
                var a = await this.payment.createPayment(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the CreatePayment method: {ex}");

                throw;
            }

        }


        [HttpPost]
        [Route("delete-payment")]

        public async Task<IActionResult> DeletePayment(string id)
        {
            try
            {
                var a = await this.payment.DeletePayment(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the DeletePayment method: {ex}");

                throw;
            }

        }

        [HttpPost]
        [Route("update-payment")]

        public async Task<IActionResult> Update(string id)
        {
            try
            {
                var a = await this.payment.UpdatePayment(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the CreatePayment method: {ex}");

                throw;
            }

        }

        [HttpGet]
        [Route("get-payment-by-order-id")]

        public async Task<IActionResult> GetPayement(string id)
        {
            try
            {
                var a = await this.payment.GetPayment(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetPayement method: {ex}");

                throw;
            }

        }

        [HttpGet]
        [Route("get-payment-by-order-id-fail")]

        public async Task<IActionResult> GetPaymentFail(string id)
        {
            try
            {
                var a = await this.payment.GetPaymentFail(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetPayement method: {ex}");

                throw;
            }

        }

        [HttpGet]
        [Route("get-payment-by-order-id-success")]

        public async Task<IActionResult> GetPaymentSuccess(string id)
        {
            try
            {
                var a = await this.payment.GetPaymentSuccess(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetPayement method: {ex}");

                throw;
            }

        }

        [HttpGet]
        [Route("get-payments")]

        public async Task<IActionResult> GetAllPayement()
        {
            try
            {
                var a = await this.payment.GetPaymentList();
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetAllPayement method: {ex}");

                throw;
            }

        }
    }
}
