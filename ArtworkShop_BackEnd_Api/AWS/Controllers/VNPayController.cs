using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AWS.Models;
using AWS.Repositories.Services;
using AWS.Repositories;

namespace AWS.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        protected readonly ARTWORKPLATFORMContext context;
        //private readonly double _exchangeRate;
        private readonly IConfiguration _configuration;
        public string URL_VNPAY_REFUND;
        public string VNPAY_TMNCODE = "PYEUR6Q3";
        public string VNPAY_HASH_SECRECT = "MUBZXLIDNFAVPCJGNIBPMJVYJGNHESRG";
        public string VNPAY_VERSION = "2.1.0";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public VNPayController(IConfiguration configuration, ARTWORKPLATFORMContext context)
        {
            _configuration = configuration;
            this.context = context;
        }

        /// <summary>
        /// [Guest] Endpoint for company create url payment with condition
        /// </summary>
        /// <returns>List of user</returns>
        /// <response code="200">Returns the list of user</response>
        /// <response code="204">Returns if list of user is empty</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string PaymentID)
        {
            try
            {
                var check = await this.context.Payments.Where(x => x.PaymentId.Equals(PaymentID)).FirstOrDefaultAsync();
                if (check != null)
                {
                    string ip = "256.256.256.1";
                    string url = _configuration["VnPay:Url"];
                    string returnUrl = _configuration["VnPay:ReturnAdminPath"];
                    string tmnCode = _configuration["VnPay:TmnCode"];
                    string hashSecret = _configuration["VnPay:HashSecret"];
                    VnPayLibrary pay = new VnPayLibrary();

                    pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
                    pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
                    pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
                    pay.AddRequestData("vnp_Amount", ((int)check.Amount * 100).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
                    pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
                    pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
                    pay.AddRequestData("vnp_IpAddr", ip); //Địa chỉ IP của khách hàng thực hiện giao dịch
                    pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
                    //pay.AddRequestData("vnp_OrderInfo", "ĐASADASOOPAO23SDSD"); //Thông tin mô tả nội dung thanh toán
                    pay.AddRequestData("vnp_OrderInfo", "Thanh toán sản phẩm thông qua hệ thống BCS");
                    pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
                    pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
                    // tách hóa đơn ra để thêm vào db
                    string taxVNPay = DateTime.Now.Ticks.ToString();
                    pay.AddRequestData("vnp_TxnRef", taxVNPay); //mã hóa đơn
                    pay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddHours(1).ToString("yyyyMMddHHmmss")); //Thời gian kết thúc thanh toán
                    string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

                    // update db
                    check.TransactionCode = taxVNPay;
                    this.context.Payments.Update(check);
                    if (await this.context.SaveChangesAsync() > 0)
                    {
                        return Ok(paymentUrl);
                    }
                    else
                    {
                        throw new Exception("Lỗi trong quá trình lưu vào cơ sở dữ liệu");
                    }

                }
                else
                {
                    throw new Exception("không tồn tại donate id");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// [Guest] Endpoint for company confirm payment with condition
        /// </summary>
        /// <returns>List of user</returns>
        /// <response code="200">Returns the list of user</response>
        /// <response code="204">Returns if list of user is empty</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet("PaymentConfirm")]
        public async Task<IActionResult> Confirm()
        {
            string returnUrl = _configuration["VnPay:ReturnPath"];
            float amount = 0;
            string status = "failed";
            if (Request.Query.Count > 0)
            {
                string vnp_HashSecret = _configuration["VnPay:HashSecret"]; //Secret key
                var vnpayData = Request.Query;
                VnPayLibrary vnpay = new VnPayLibrary();
                foreach (string s in vnpayData.Keys)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //Lay danh sach tham so tra ve tu VNPAY
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                float vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                amount = vnp_Amount;
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.Query["vnp_SecureHash"];
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
                var vnp_TransDate = vnpay.GetResponseData("vnp_PayDate");
                //Guid companyId = Guid.Parse(vnp_OrderInfo);
                status = "success";

                string taxVNPay = orderId.ToString();
                var check = await this.context.Payments.Where(x => x.TransactionCode.Equals(taxVNPay)).FirstOrDefaultAsync();
                check.Status = true;
                check.CreateDate = DateTime.Now;
                //check.VnpTransDate = vnp_TransDate;

                var order = await this.context.Ordertbs.Where(x => x.OrderId.Equals(check.OrderId)).FirstOrDefaultAsync();
                if (order != null)
                {
                    order.Status = true;
                    this.context.Update(order);
                    await this.context.SaveChangesAsync();
                }
                //var orderDetail = await this.context.OrderDetail.Where(x => x.OrderId.Equals(check.OrderId)).ToListAsync();
                //if (orderDetail != null)
                //{
                //    foreach (var detail in orderDetail)
                //    {
                //        detail.Status = true;
                //        this.context.OrderDetail.Update(detail);
                //        await this.context.SaveChangesAsync();
                //    }
                //}

                this.context.Payments.Update(check);
                await this.context.SaveChangesAsync();
            }

            return Redirect(returnUrl + "?amount=" + amount + "&status=" + status);
        }



    }
}
