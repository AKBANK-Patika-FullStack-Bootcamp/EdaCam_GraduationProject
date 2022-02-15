using DAL.Models;
using Entities;
using HousingBillPaymentAPI.Helper;

namespace HousingBillPaymentAPI.Services
{
    public class ApartmentService
    {
        private HousingBillPaymentContext _context=new HousingBillPaymentContext();


        /// <summary>
        /// Veri tabanına yeni bir daire ekler.
        /// </summary>
        /// <param name="_apartment"></param>
        /// <returns>Daire ekleme işleminin başarı durumunu döner</returns>
        public BaseResponse AddApartment(Apartment _apartment)
        {
            var response = new BaseResponse();
            if (CheckIfApartmentExist(_apartment))
            {
                response.Success = false;
                response.Message = "Girilen bilgilere sahip daire sistemde mevcut.";
            }
            else
            {
                try
                {
                    _context.Apartment.Add(_apartment);  //Content içerisine yeni bir daire eklenir.
                    _context.SaveChanges();             //Ekleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                    response.Success = true;
                    response.Message = "Daire kaydedilmiştir.";
                    response.Data = GetApartments();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    response.Success = false;
                    response.Message = "Daire kaydetme işlemi sırasında hata meydana geldi.";
                }
            }
            return response;
        }

        /// <summary>
        /// Parametre olarak verilen id değerine sahip daireyi getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Daire döner</returns>
        public Apartment GetApartment(int Id)
        {
               return _context.Apartment.FirstOrDefault(a => a.Id == Id);
        }

        /// <summary>
        /// Veritabanındaki daire ve kullanıcı tablolarını Left Join ederek oluşan detay bilgilerini getirir.
        /// </summary>
        /// <returns> Daire detay listesini döner</returns>
        public List<ApartmentDeatil> GetApartmentDetails()
        {
            return (from a in _context.Apartment
             join u in _context.User on a.UserId equals u.Id into temp
             from t in temp.DefaultIfEmpty()
             select new ApartmentDeatil
             {
                Id= a.Id,
                User= t.Name+" " + t.Surname,
                Block= a.Block,
                IsEmpty= a.IsEmpty,
                Type= a.Type,
                ApartmentNo= a.ApartmentNo,
                Floor= a.Floor
             }).ToList();
            
        }

        /// <summary>
        /// Veritabanındaki daire bilgilerini listeler
        /// </summary>
        /// <returns>Daire listesini döner</returns>
        public List<Apartment> GetApartments()
        {
            return _context.Apartment.OrderBy(a => a.Id).ToList(); //Id bilgisinie göre daireler sıralanır ve getirilir.
        }

        /// <summary>
        /// Veritabanında kayıtlı olan dairenin bilgilerini, parametre olarak verilen daire bilgileri ile günceller.
        /// <param name="_apartment"></param>
        /// </summary>
        /// <returns>Güncelleme işleminin başarı durumunu döner/returns>
        public BaseResponse UpdateApartment(Apartment _apartment)
        {
            var response = new BaseResponse();
            Apartment exist = _context.Apartment.Find(_apartment.Id);//Dairenin veritabanında bulunma durumu kontrol edilir.
            if (exist != null)
            {
                if (CheckIfUpdatedApartmentExist(_apartment))
                {
                    response.Success = false;
                    response.Message = "Girilen bilgilere sahip daire sistemde mevcut.";
                }
                else
                {
                    try
                    {
                        _context.Entry(exist).CurrentValues.SetValues(_apartment); //Bulunan daire bilgileri yenileri ile güncellenir.
                        _context.SaveChanges();// Güncelleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                        response.Success = true;
                        response.Message = "Daire güncellendi";
                        response.Data = GetApartments();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        response.Success = false;
                        response.Message = "Daire güncelleme işlemi sırasında hata meydana geldi";
                    }
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Daire bulunamadı";
            }

            return response;
        }

        /// <summary>
        /// Veri tabanından verilen id'ye sahip daireyi siler.
        /// <param name="Id"></param>
        /// </summary>
        /// <returns>Daire silme işleminin başarı durumunu döner/returns>
        public BaseResponse DeleteApartment(int _id)
        {
            var response = new BaseResponse();
            try
            {
                _context.Apartment.Remove(GetApartment(_id)); //GetUser metodu ile getirilen ilgili id'ye sahip daireyi siler.
                _context.SaveChanges();// Silme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                response.Success = true;
                response.Message = "Daire silindi";
                response.Data = GetApartments();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Daire silme işlemi sırasında hata meydana geldi";
            }
            return response;
        }


        /// <summary>
        /// Veri tabanındaki daire listesini sayfalanabilir yapıda döner.
        /// <param name=pagingParameters"></param>
        /// </summary>
        /// <returns>Daire listesini sayfalama işlemi cevabını döner./returns>
        public BaseResponse PaingApartments(PagingParameters pagingParameters)
        {
            var response = new BaseResponse();
            try
            {
                List<Apartment> pageableData = GetApartments() //Veri tabanındaki kullanıcıları listesi
               .OrderBy(u => u.Block) //Kullanıcıları adlarına göre sıralar
               .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize) //Yeni sayfadaki verilerin listedeki hangi indexdeki veriden itibaren devam edeceğini belirtir.
               .Take(pagingParameters.PageSize) //Sayfada listelenecek veri miktarını belirtir.
               .ToList();
                response.Success = true;
                response.Data = pageableData;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Daireleri sayfalama işlemi sırasında hata meydana geldi";
            }
            return response;
        }

        public bool CheckIfApartmentExist(Apartment _apartment)
        {
            return _context.Apartment.Any(a => a.Block == _apartment.Block && a.ApartmentNo == _apartment.ApartmentNo);
        }
        public bool CheckIfUpdatedApartmentExist(Apartment _apartment)
        {
            return _context.Apartment.Any(a => a.Id != _apartment.Id && a.Block == _apartment.Block && a.ApartmentNo == _apartment.ApartmentNo);
        }

    }
}
