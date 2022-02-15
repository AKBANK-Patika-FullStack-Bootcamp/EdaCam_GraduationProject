using DAL.Models;
using HousingBillPaymentAPI.Helper;
using HousingBillPaymentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HousingBillPaymentAPI.Controllers
{
    public class DebtTypeController: BaseApiController
    {
            DebtTypeService debtTypeService = new DebtTypeService();

            /// <summary>
            /// Sistemdeki borç tipi bilgilerini listeler
            /// </summary>
            /// <returns>Borç tipi listesini döner</returns>
            [HttpGet]
            public List<DebtType> GetDebtTypes()
            {
                return debtTypeService.GetDebtTypes();
            }

            /// <summary>
            /// Parametre olarak verilen id değerine sahip borç tipini getirir.
            /// </summary>
            /// <param name="id"></param>
            /// <returns>Borç tipi döner</returns>
            [HttpGet("{id}")]
            public DebtType GetDebtType(int id)
            {
                return debtTypeService.GetDebtType(id);
            }
            /// <summary>
            /// Sisteme yeni borç tipi ekler.
            /// </summary>
            /// /// <param name="debtType"></param>
            /// <returns>IActionResult objesini döner/returns>
            [HttpPost]
            public IActionResult SaveDebtType(DebtType debtType)
            {
                var response = debtTypeService.AddDebtType(debtType);

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
        /// Parametre olarak verilen id'ye sahip borç tipini parametre olarak verilen borç tipi bilgileri ile günceller.
        /// <param name="debtType"></param>
        /// </summary>
        /// <returns>IActionResult objesini döner/returns>
        [HttpPut]
            public IActionResult UpdateDebtType(DebtType debtType)
            {
                var response = debtTypeService.UpdateDebtType(debtType);

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
            /// Parametre olarak verilen id'ye sahip borç tipini siler.
            /// <param name="id"></param>
            /// </summary>
            /// <returns>Result objesini döner/returns>
            [HttpDelete("{id}")]
            public IActionResult DeleteDebtType(int id)
            {
                var response = debtTypeService.DeleteDebtType(id);

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
            /// Sistemdeki borç tipi bilgilerini sayfalanmış şekilde listeler.
            /// </summary>
            /// <returns>Borç tipi listesini sayfalanmış yapıda döner</returns>
            [HttpGet("paging")]
            public IActionResult GetDebtTypePaging([FromQuery] PagingParameters pagingParameters)
            {
                var response = debtTypeService.PaingDebtTypes(pagingParameters);

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
