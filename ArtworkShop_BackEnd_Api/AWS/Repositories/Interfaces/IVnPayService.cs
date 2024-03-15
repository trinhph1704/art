using AWS.Models;
using CodeMegaVNPay.Models;

namespace AWS.Repositories;
public interface IVnPayService
{
    string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}