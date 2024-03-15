using AWS.DTO;
using AWS.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReport report;

        public ReportController(IReport report)
        {
            this.report = report;
        }



        [HttpGet]
        [Route("get-all")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var a = await this.report.GetAll();
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

        [HttpGet]
        [Route("get-by-id")]

        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var a = await this.report.GetById(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetById method: {ex}");

                throw;
            }

        }

        [HttpGet]
        [Route("get-report-by-user-id")]

        public async Task<IActionResult> GetReportByUserId(string userId)
        {
            try
            {
                var a = await this.report.GetReportByUserId(userId);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetReportByUserId method: {ex}");

                throw;
            }

        }

        [HttpPost]
        [Route("create-new-report")]

        public async Task<IActionResult> CreateNewReport(string UserId, ReportDTO rp)
        {
            try
            {
                var a = await this.report.CreateNew(UserId,rp);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the CreateNewReport method: {ex}");

                throw;
            }

        }
    }
}
