using DAL.Models;
using Entities;
using HousingBillPaymentAPI.Helper;

namespace HousingBillPaymentAPI.Services
{
    public class DebtTypeService
    {
        private HousingBillPaymentContext _context = new HousingBillPaymentContext();


        /// <summary>
        /// Veri tabanına yeni bir borç tipi ekler.
        /// </summary>
        /// <param name="_debtType"></param>
        /// <returns>Borç tipi ekleme işleminin cevabını döner</returns>
        public BaseResponse AddDebtType(DebtType _debtType)
        {
            var response = new BaseResponse();
            try
            {
                _context.DebtType.Add(_debtType);    //Content içerisine yeni bir borç eklenir.
                _context.SaveChanges();             //Ekleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                response.Success = true;
                response.Message = "Borç tipi kaydedilmiştir.";
                response.Data = GetDebtTypes();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Borç tipi kaydetme işlemi sırasında hata meydana geldi.";
            }
            return response;
        }
        
        /// <summary>
        /// Veritabanında bulunan borç tiplerini getirir.
        /// </summary>
        /// <returns>Borç tipi listesi döner</returns>
        public List<DebtType> GetDebtTypes()
        {
            return _context.DebtType.OrderBy(d => d.Id).ToList(); //Id bilgisinie göre borç tipleri sıralanır ve getirilir.
        }

        /// <summary>
        /// Parametre olarak verilen id değerine sahip borç tipini getirir.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns>Borç tipi döner</returns>
        public DebtType GetDebtType(int _id)
        {
            return _context.DebtType.FirstOrDefault(d => d.Id == _id);
        }

        /// <summary>
        /// Veritabanında kayıtlı olan borç tipi bilgilerini, parametre olarak verilen borç tipi bilgileri ile günceller.
        /// <param name="_debtType"></param>
        /// </summary>
        /// <returns>Güncelleme işleminin cevabını döner/returns>
        public BaseResponse UpdateDebtType(DebtType _debtType)
        {
            var response = new BaseResponse();
            DebtType exist = _context.DebtType.Find(_debtType.Id);//Borç tipinin veritabanında bulunma durumu kontrol edilir.
            if (exist != null)
            {
                try
                {
                    _context.Entry(exist).CurrentValues.SetValues(_debtType); //Bulunan borç tipi bilgileri yenileri ile güncellenir.
                    _context.SaveChanges();// Güncelleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                    response.Success = true;
                    response.Message = "Borç tipi güncellendi";
                    response.Data = GetDebtTypes();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    response.Success = false;
                    response.Message = "Borç tipi güncelleme işlemi sırasında hata meydana geldi";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Borç tipi bulunamadı";
            }

            return response;
        }

        /// <summary>
        /// Veri tabanından verilen id'ye sahip borç tipini siler.
        /// <param name=_id"></param>
        /// </summary>
        /// <returns>Borç tipi silme işleminin cevabını döner/returns>
        public BaseResponse DeleteDebtType(int _id)
        {
            var response = new BaseResponse();
            try
            {
                _context.DebtType.Remove(GetDebtType(_id)); //GetDebt metodu ile getirilen ilgili id'ye sahip borç tipini siler.
                _context.SaveChanges();// Silme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                response.Success = true;
                response.Message = "Borç tipi silindi";
                response.Data = GetDebtTypes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Borç tipi silme işlemi sırasında hata meydana geldi";
            }
            return response;
        }

        /// <summary>
        /// Veri tabanındaki borç tipi listesini sayfalanabilir yapıda döner.
        /// <param name=pagingParameters"></param>
        /// </summary>
        /// <returns>Borç tipi listesinin sayfalama işlemi cevabını döner./returns>
        public BaseResponse PaingDebtTypes(PagingParameters pagingParameters)
        {
            var response = new BaseResponse();
            try
            {
                List<DebtType> pageableData = GetDebtTypes() //Veri tabanındaki borç tiplerinin listesi
               .OrderBy(d => d.Type) //Borç tipi ismine göre sıralar
               .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize) //Yeni sayfadaki verilerin listedeki hangi indexdeki veriden itibaren devam edeceğini belirtir.
               .Take(pagingParameters.PageSize) //Sayfada listelenecek veri miktarını belirtir.
               .ToList();
                response.Success = true;
                response.Data = pageableData;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Borç tiplerini sayfalama işlemi sırasında hata meydana geldi";
            }
            return response;
        }
    }
}
