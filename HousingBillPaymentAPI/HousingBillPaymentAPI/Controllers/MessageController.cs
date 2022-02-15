using DAL.Models;
using HousingBillPaymentAPI.Helper;
using HousingBillPaymentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HousingBillPaymentAPI.Controllers
{
    public class MessageController:BaseApiController
    {
        MessageService messageService= new MessageService();

        /// <summary>
        /// Sistemdeki mesajları listeler
        /// </summary>
        /// <returns>Mesaj listesini döner</returns>
        [HttpGet]
        public List<Message> GetMessages()
        {
            return messageService.GetMessages();
        }

        /// <summary>
        /// Parametre olarak verilen id değerine sahip mesajı getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Mesaj döner</returns>
        [HttpGet("{id}")]
        public Message GetMessage(int id)
        {
            return messageService.GetMessage(id);
        }
        /// <summary>
        /// Parametre olarak verilen kullanıcı id değeri ile kullanıcıya ait mesajları getirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Mesajları döner</returns>
        [HttpGet("users/{userId}")]
        public IActionResult GetMessagesWithUserId(int userId)
        {
            var response = messageService.GetMessagesWithUserId(userId);

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
        /// Sisteme yeni mesaj ekler.
        /// </summary>
        /// /// <param name="message"></param>
        /// <returns>IActionResult objesini döner/returns>
        /// 
        [HttpPost]
        public IActionResult SaveMessage(Message message)
        {
            var response = messageService.AddMessage(message);

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
        /// Parametre olarak verilen id'ye sahip mesajı parametre olarak verilen mesaj bilgileri ile günceller.
        /// <param name="message"></param>
        /// </summary>
        /// <returns>IActionResult objesini döner/returns>
        [HttpPut]
        public IActionResult UpdateMessage(Message message)
        {
            var response = messageService.UpdateMessage(message);

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
        /// Parametre olarak verilen id'ye sahip mesajı siler.
        /// <param name="id"></param>
        /// </summary>
        /// <returns>Result objesini döner/returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var response = messageService.DeleteMessage(id);

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
        /// Sistemdeki mesaj bilgilerini sayfalanmış şekilde listeler.
        /// </summary>
        /// <returns>Mesaj listesini sayfalanmış yapıda döner</returns>
        [HttpGet("paging")]
        public IActionResult GetMessagePaging([FromQuery] PagingParameters pagingParameters)
        {
            var response = messageService.PaingMessages(pagingParameters);

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
