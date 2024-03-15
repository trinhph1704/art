using AWS.DTO;
using AWS.Models;

namespace AWS.Repositories.Interfaces
{
    public interface IReport
    {
        Task<List<Report>> GetAll();
        Task<Report> GetById(string id);       
        Task<Report> GetReportByUserId(string userID);       
        Task<Report> CreateNew(string UserId, ReportDTO rp);       

    }
}
