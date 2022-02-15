using DAL.Models;
using HousingBillPaymentAPI.Helper;
using HousingBillPaymentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HousingBillPaymentAPI.Controllers
{
    public class PaymentController:BaseApiController
    {
        PaymentService paymentService = new PaymentService();

        /// <summary>
        /// Sistemdeki ödeme bilgilerini listeler
        /// </summary>
        /// <returns>Ödeme listesini döner</returns>
        [HttpGet]
        public List<Payment> GetPayments()
        {
            return paymentService.GetPayments();
        }

        /// <summary>
        /// Sistemdeki ödeme bilgilerini listeler
        /// </summary>
        /// <returns>Ödeme listesini döner</returns>
        [HttpGet("detail")]
        public List<PaymentDetail> GetPaymentDetails()
        {
            return paymentService.GetPaymentDetails();
        }

        /// <summary>
        /// Parametre olarak verilen id değerine sahip ödemeyi getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ödeme döner</returns>
        [HttpGet("{id}")]
        public Payment GetPayment(int id)
        {
            return paymentService.GetPayment(id);
        }
        /// <summary>
        /// Parametre olarak verilen kullanıcı id değeri ile kullanıcıya ait ödemeleri getirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Ödemeleri döner</returns>
        [HttpGet("users/{userId}")]
        public IActionResult GetPaymentWithUserId(int userId)
        {
            var response = paymentService.GetPaymentWithUserId(userId);

            if (response.Success)
            {
                return SuccessData(response);
            }
            else
            {
                return BadRequestMessage(response);
            }
        }
        /// <summary>
        /// Sisteme yeni ödeme ekler.
        /// </summary>
        /// /// <param name="payment"></param>
        /// <returns>IActionResult objesini döner/returns>
        /// 
        [HttpPost]
        public IActionResult SavePayment(Payment payment)
        {
            var response = paymentService.AddPayment(payment);

            if (response.Success)
            {
                return Success(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        /// <summary>
        /// Parametre olarak verilen id'ye sahip ödemeyi parametre olarak verilen ödeme bilgileri ile günceller.
        /// <param name="payment"></param>
        /// </summary>
        /// <returns>IActionResult objesini döner/returns>
        [HttpPut]
        public IActionResult UpdatePayment(Payment payment)
        {
            var response = paymentService.UpdatePayment(payment);

            if (response.Success)
            {
                return Success(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        /// <summary>
        /// Parametre olarak verilen id'ye sahip ödemeyi siler.
        /// <param name="id"></param>
        /// </summary>
        /// <returns>Result objesini döner/returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var response = paymentService.DeletePayment(id);

            if (response.Success)
            {
                return Success(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        /// <summary>
        /// Sistemdeki ödeme bilgilerini sayfalanmış şekilde listeler.
        /// </summary>
        /// <returns>Ödeme listesini sayfalanmış yapıda döner</returns>
        [HttpGet("paging")]
        public IActionResult GetPaymentPaging([FromQuery] PagingParameters pagingParameters)
        {
            var response = paymentService.PaingPayments(pagingParameters);

            if (response.Success)
            {
                return SuccessData(response);
            }
            else
            {
                return BadRequestMessage(response);
            }
        }
    }
}
