using DAL.Models;
using Entities;
using HousingBillPaymentAPI.Helper;

namespace HousingBillPaymentAPI.Services
{
    public class MessageService
    {
        private HousingBillPaymentContext _context = new HousingBillPaymentContext();

        /// <summary>
        /// Veri tabanına yeni bir mesaj ekler.
        /// </summary>
        /// <param name="_message"></param>
        /// <returns>Mesaj ekleme işleminin cevabını döner</returns>
        public BaseResponse AddMessage(Message _message)
        {
            var response = new BaseResponse();
            try
            {
                _context.Message.Add(_message);    //Content içerisine yeni bir mesaj eklenir.
                _context.SaveChanges();           //Ekleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                response.Success = true;
                response.Message = "Mesaj kaydedilmiştir.";
                response.Data = GetMessages();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Mesaj kaydetme işlemi sırasında hata meydana geldi.";
            }
            return response;
        }

        /// <summary>
        /// Veritabanında bulunan mesajları getirir.
        /// </summary>
        /// <returns>Mesaj listesi döner</returns>
        public List<Message> GetMessages()
        {
            return _context.Message.OrderBy(m => m.Id).ToList(); //Id bilgisinie göre ödemeler sıralanır ve getirilir.
        }

        /// <summary>
        /// Parametre olarak verilen id değerine sahip mesajı getirir.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns>Mesaj döner</returns>
        public Message GetMessage(int _id)
        {
            return _context.Message.FirstOrDefault(d => d.Id == _id);
        }
        /// <summary>
        /// Kullanıcı id bilgisi parametresi ile kullanıcıya ait mesajları getirir.
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns>Mesaj listesi döner</returns>
        public BaseResponse GetMessagesWithUserId(int _userId)
        {
            var response = new BaseResponse();
            try
            {
                var messages = _context.Message.Where(m => m.UserId == _userId); //Kullanıcı idsine göre mesajlarını getirir. 
                response.Success = true;
                response.Data = messages;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Mesaj listeleme işlemi sırasında hata meydana geldi.";
            }
            return response;

        }

        /// <summary>
        /// Veritabanında kayıtlı olan mesaj bilgilerini, parametre olarak verilen mesaj bilgileri ile günceller.
        /// <param name="_message"></param>
        /// </summary>
        /// <returns>Güncelleme işleminin cevabını döner/returns>
        public BaseResponse UpdateMessage(Message _message)
        {
            var response = new BaseResponse();
            Message exist = _context.Message.Find(_message.Id);//Mesajın veritabanında bulunma durumu kontrol edilir.
            if (exist != null)
            {
                try
                {
                    _context.Entry(exist).CurrentValues.SetValues(_message); //Bulunan mesaj bilgileri yenileri ile güncellenir.
                    _context.SaveChanges();// Güncelleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                    response.Success = true;
                    response.Message = "Mesaj güncellendi";
                    response.Data = GetMessages();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    response.Success = false;
                    response.Message = "Mesaj güncelleme işlemi sırasında hata meydana geldi";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Mesaj bulunamadı";
            }

            return response;
        }

        /// <summary>
        /// Veri tabanından verilen id'ye sahip mesajı siler.
        /// <param name=_id"></param>
        /// </summary>
        /// <returns>Mesaj silme işleminin cevabını döner/returns>
        public BaseResponse DeleteMessage(int _id)
        {
            var response = new BaseResponse();
            try
            {
                _context.Message.Remove(GetMessage(_id)); //GetMessage metodu ile getirilen ilgili id'ye sahip mesajı siler.
                _context.SaveChanges();// Silme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                response.Success = true;
                response.Message = "Mesaj silindi";
                response.Data = GetMessages();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Mesaj silme işlemi sırasında hata meydana geldi";
            }
            return response;
        }

        /// <summary>
        /// Veri tabanındaki mesaj listesini sayfalanabilir yapıda döner.
        /// <param name=pagingParameters"></param>
        /// </summary>
        /// <returns>Mesaj listesinin sayfalama işlemi cevabını döner./returns>
        public BaseResponse PaingMessages(PagingParameters pagingParameters)
        {
            var response = new BaseResponse();
            try
            {
                List<Message> pageableData = GetMessages() //Veri tabanındaki mesajları listesi
               .OrderBy(m => m.Id) //Mesaj idsine göre sıralar
               .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize) //Yeni sayfadaki verilerin listedeki hangi indexdeki veriden itibaren devam edeceğini belirtir.
               .Take(pagingParameters.PageSize) //Sayfada listelenecek veri miktarını belirtir.
               .ToList();
                response.Success = true;
                response.Data = pageableData;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Mesaj sayfalama işlemi sırasında hata meydana geldi";
            }
            return response;
        }
    }
}
