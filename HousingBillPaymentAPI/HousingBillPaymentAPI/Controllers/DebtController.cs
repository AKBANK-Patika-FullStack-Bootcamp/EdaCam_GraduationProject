using DAL.Models;
using HousingBillPaymentAPI.Helper;
using HousingBillPaymentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HousingBillPaymentAPI.Controllers
{
    public class DebtController : BaseApiController
    {
        DebtService debtService = new DebtService();

        /// <summary>
        /// Sistemdeki borç bilgilerini listeler
        /// </summary>
        /// <returns>Borç listesini döner</returns>
        [HttpGet]
        public List<Debt> GetDebts()
        {
            return debtService.GetDebts();
        }

        /// <summary>
        /// Sistemdeki borç detay bilgilerini listeler
        /// </summary>
        /// <returns>Borç detay listesini döner</returns>
        [HttpGet("detail")]
        public List<DebtDetail> GetDebtDetail()
        {
            return debtService.GetDebtDetails();
        }

        /// <summary>
        /// Parametre olarak verilen id değerine sahip borcu getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Borç döner</returns>
        [HttpGet("{id}")]
        public Debt GetDebt(int id)
        {
            return debtService.GetDebt(id);
        }
        /// <summary>
        /// Parametre olarak verilen kullanıcı id değeri ile kullanıcıya ait borçları getirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Borçları döner</returns>
        [HttpGet("users/{userId}")]
        public IActionResult GetDebtWithUserId(int userId)
        {
            var response = debtService.GetDebtsWithUserId(userId);

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
        /// Sisteme yeni borç ekler.
        /// </summary>
        /// /// <param name="debt"></param>
        /// <returns>IActionResult objesini döner/returns>
        [HttpPost]
        public IActionResult SaveDebt(Debt debt)
        {
            var response = debtService.AddDebt(debt);

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
        /// Sistemdeki tüm kullanıcılara yeni borç ekler.
        /// </summary>
        /// /// <param name="debt"></param>
        /// <returns>IActionResult objesini döner/returns>
        [HttpPost("bulk")]
        public IActionResult SaveDebtToAll(Debt debt)
        {
            var response = debtService.AddDebtToAll(debt);

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
        /// Parametre olarak verilen id'ye sahip borcu parametre olarak verilen borç bilgileri ile günceller.
        /// <param name="apartment"></param>
        /// </summary>
        /// <returns>IActionResult objesini döner/returns>
        [HttpPut]
        public IActionResult UpdateDebt(Debt debt)
        {
            var response = debtService.UpdateDebt(debt);

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
        /// Parametre olarak verilen tarih ve borç türüne sahip borcu parametre olarak verilen borç bilgileri ile günceller.
        /// <param name="debt"></param>
        /// <param name="date"></param>
        /// <param name="debtType"></param>
        /// </summary>
        /// <returns>IActionResult objesini döner/returns>
        [HttpPut("bulk")]
        public IActionResult UpdateAllDebts(Debt debt,DateTime date,int debtType)
        {
            var response = debtService.UpdateAllDebts(debt,date,debtType);

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
        /// Parametre olarak verilen id'ye sahip borcu siler.
        /// <param name="id"></param>
        /// </summary>
        /// <returns>Result objesini döner/returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteDebt(int id)
        {
            var response = debtService.DeleteDebt(id);

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
        /// Parametre olarak verilen id'ye sahip borcu siler.
        /// <param name="date"></param>
        /// /// <param name="debtType"></param>
        /// </summary>
        /// <returns>Result objesini döner/returns>
        [HttpDelete("{date}/{debtType}")]
        public IActionResult DeleteAllDebts(DateTime date, int debtType)
        {
            var response = debtService.DeleteDebts(date, debtType);

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
        /// Sistemdeki borç bilgilerini sayfalanmış şekilde listeler.
        /// </summary>
        /// <returns>Borç listesini sayfalanmış yapıda döner</returns>
        [HttpGet("paging")]
        public IActionResult GetDebtPaging([FromQuery] PagingParameters pagingParameters)
        {
            var response = debtService.PaingDebts(pagingParameters);

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
