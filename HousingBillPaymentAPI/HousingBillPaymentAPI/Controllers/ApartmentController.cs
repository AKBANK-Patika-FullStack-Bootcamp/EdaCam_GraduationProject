using DAL.Models;
using Entities;
using HousingBillPaymentAPI.Helper;
using HousingBillPaymentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HousingBillPaymentAPI.Controllers
{
    public class ApartmentController : BaseApiController
    {
        ApartmentService apartmentService = new ApartmentService();

        /// <summary>
        /// Sistemdeki daire bilgilerini listeler
        /// </summary>
        /// <returns>Daire listesini döner</returns>
        [HttpGet]
        public List<Apartment> GetApartment()
        {
            return apartmentService.GetApartments();
        }

        /// <summary>
        /// Sistemdeki daire detay bilgilerini listeler
        /// </summary>
        /// <returns>Daire detay listesini döner</returns>
        [HttpGet("detail")]
        public List<ApartmentDeatil> GetApartmentDetail()
        {
            return apartmentService.GetApartmentDetails();
        }

        /// <summary>
        /// Parametre olarak verilen id değerine sahip daireyi getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ürün döner</returns>
        [HttpGet("{id}")]
        public Apartment GetApartment(int id)
        {
            return apartmentService.GetApartment(id);
        }

        /// <summary>
        /// Sisteme yeni apartman ekler.
        /// </summary>
        /// /// <param name="_apartment"></param>
        /// <returns>IActionResult objesini döner/returns>
        [HttpPost]
        public IActionResult SaveApartment(Apartment apartment)
        {
            var response = apartmentService.AddApartment(apartment);

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
        /// Parametre olarak verilen id'ye sahip daireyi parametre olarak verilen daire bilgileri ile günceller.
        /// <param name="apartment"></param>
        /// </summary>
        /// <returns>IActionResult objesini döner/returns>
        [HttpPut]
        public IActionResult UpdateApartment(Apartment apartment)
        {
            var response = apartmentService.UpdateApartment(apartment);

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
        /// Parametre olarak verilen id'ye sahip daireyi siler.
        /// <param name="id"></param>
        /// </summary>
        /// <returns>Result objesini döner/returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteApartment(int id)
        {
            var response = apartmentService.DeleteApartment(id);

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
        /// Sistemdeki daire bilgilerini sayfalanmış şekilde listeler.
        /// </summary>
        /// <returns>Daire listesini sayfalanmış yapıda döner</returns>
        [HttpGet("paging")]
        public IActionResult GetApartmentPaging([FromQuery] PagingParameters pagingParameters)
        {
            var response = apartmentService.PaingApartments(pagingParameters);

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
