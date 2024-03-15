using AWS.DTO;
using AWS.DTO.ArtworkDTO;
using AWS.Models;
using AWS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AWS.Repositories.Services
{
    public class SReport : IReport
    {
        private readonly ARTWORKPLATFORMContext _context;

        public SReport(ARTWORKPLATFORMContext context)
        {
            _context = context;
        }

        public async Task<Report> CreateNew(string UserId, ReportDTO rp)
        {
            try
            {
                var user = await _context.Usertbs.FindAsync(UserId);

                var report = new Report
                {
                    ReportId =  "R" + Guid.NewGuid().ToString().Substring(0, 5),
                    UserId = UserId,
                    ArtworkId = rp.ArtworkID,
                    ReportDate = DateTime.Now,  
                    Description = rp.Description,   
                   
                };

                //Add Genres to the artwork if provided
               

                _context.Reports.Add(report);
                await _context.SaveChangesAsync();

                return report;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<List<Report>> GetAll()
        {
            try
            {
                var x =await this._context.Reports.ToListAsync();
                return x;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task<Report> GetById(string id)
        {
            try
            {
                var a = await this._context.Reports.Where(x => x.ReportId.Equals(id)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Report> GetReportByUserId(string userID)
        {
            try
            {
                var a = await this._context.Reports.Where(x => x.UserId.Equals(userID)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
