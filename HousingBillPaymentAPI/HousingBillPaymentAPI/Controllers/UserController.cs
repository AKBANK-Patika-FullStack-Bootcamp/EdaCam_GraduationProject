using DAL.Models;
using Entities;
using HousingBillPaymentAPI.Helper;
using HousingBillPaymentAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HousingBillPaymentAPI.Controllers
{
    public class UserController : BaseApiController
    {
        UserService userService = new UserService();
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Sistemdeki kullanıcı bilgilerini listeler
        /// </summary>
        /// <returns>Kullanıcı listesini döner</returns>
        [HttpGet]
        public List<User> GetUsers()
        {
            return userService.GetUsers();
        }

        /// <summary>
        /// Parametre olarak verilen id değerine sahip kullanıcıyı getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ürün döner</returns>
        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            return userService.GetUser(id);
        }
        /// <summary>
        /// Header'dan parametre olarak verilen user bilgileri ile siteme giriş yapılır.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Giriş işleminin başarı durumunu döner</returns>
        [HttpPost("login")]
        public IActionResult Login([FromHeader] LoginDto request)
        {

            var response = userService.Login(request,_configuration);

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
        /// Sisteme yeni kullanıcı ekler.
        /// </summary>
        /// /// <param name="user"></param>
        /// <returns>IActionResult objesini döner/returns>
        [HttpPost]
        public IActionResult SaveUser(User user)
        {
            var response = userService.AddUser(user);

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
        /// Parametre olarak verilen id'ye sahip kullanıcıyı parametre olarak verilen kullanıcı bilgileri ile günceller.
        /// <param name="user"></param>
        /// </summary>
        /// <returns>IActionResult objesini döner/returns>
        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            var response = userService.UpdateUser(user);

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
        /// Parametre olarak verilen id'ye sahip kullanıcıyı siler.
        /// <param name="id"></param>
        /// </summary>
        /// <returns>Result objesini döner/returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var response = userService.DeleteUser(id);

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
        /// Sistemdeki kullanıcı bilgilerini sayfalanmış şekilde listeler.
        /// </summary>
        /// <returns>Kullanıcı listesini sayfalanmış yapıda döner</returns>
        [HttpGet("paging")]
        public IActionResult GetUserPaging([FromQuery] PagingParameters pagingParameters)
        {
            var response = userService.PaingUsers(pagingParameters);

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
