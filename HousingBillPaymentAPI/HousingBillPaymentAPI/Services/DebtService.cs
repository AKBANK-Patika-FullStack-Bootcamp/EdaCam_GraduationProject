using DAL.Models;
using Entities;
using HousingBillPaymentAPI.Helper;

namespace HousingBillPaymentAPI.Services
{
    public class DebtService
    {
        private HousingBillPaymentContext _context = new HousingBillPaymentContext();


        /// <summary>
        /// Veri tabanına yeni bir borç ekler.
        /// </summary>
        /// <param name="_debt"></param>
        /// <returns>Borç ekleme işleminin cevabını döner</returns>
        public BaseResponse AddDebt(Debt _debt)
        {
            var response = new BaseResponse();
            try
            {
                _context.Debt.Add(_debt);     //Content içerisine yeni bir borç eklenir.
                _context.SaveChanges();      //Ekleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                response.Success = true;
                response.Message = "Borç kaydedilmiştir.";
                response.Data = GetDebts();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Borç kaydetme işlemi sırasında hata meydana geldi.";
            }
            return response;
        }
        /// <summary>
        /// Veri tabanına toplu borç ekler.
        /// </summary>
        /// <param name="_debt"></param>
        /// <returns>Borç ekleme işleminin cevabını döner</returns>
        public BaseResponse AddDebtToAll(Debt _debt)
        {
            var response = new BaseResponse();
            try
            {
                List<int> userIdList = _context.User.Select(u => u.Id).ToList();
                foreach (int id in userIdList)
                {
                    Debt debt=new Debt();
                    debt.UserId = id;
                    debt.Amount = _debt.Amount;
                    debt.DebtTypeId = _debt.DebtTypeId;
                    debt.Date = _debt.Date;
                    _context.Debt.Add(debt);     //Content içerisine yeni bir borç eklenir.
                    _context.SaveChanges();      //Ekleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                }
                response.Success = true;
                response.Message = "Borçlar kaydedilmiştir.";
                response.Data = GetDebts();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Toplu borç kaydetme işlemi sırasında hata meydana geldi.";
            }
            return response;
        }

        /// <summary>
        /// Veritabanında bulunan borçları getirir.
        /// </summary>
        /// <returns>Borç listesi döner</returns>
        public List<Debt> GetDebts()
        {
            return _context.Debt.OrderBy(u => u.Id).ToList(); //Id bilgisinie göre borçları sıralanır ve getirilir.
        }

        /// <summary>
        /// Veritabanındaki borç ve kullanıcı tablolarını Join ederek oluşan detay bilgilerini getirir.
        /// </summary>
        /// <returns>Borç detay listesini döner</returns>
        public List<DebtDetail> GetDebtDetails()
        {
     
                return (from a in _context.Debt
                        join u in _context.User on a.UserId equals u.Id into usertemp
                        from x in usertemp.DefaultIfEmpty()
                        join t in _context.DebtType on a.DebtTypeId equals t.Id into typetemp
                        from y in typetemp.DefaultIfEmpty()
                        select new DebtDetail
                        {
                            Id = a.Id,
                            User = x.Name +" "+ x.Surname,
                            DebtType = y.Type,
                            Date = a.Date,
                            Amount = a.Amount
                        }).ToList();

        }

        /// <summary>
        /// Kullanıcı id bilgisi ve ödenen borç id bilgilerine göre filtreleme yaparak kullanıcıya ait borçları getirir.
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns>Borç listesi döner</returns>
        public BaseResponse GetDebtsWithUserId(int _userId)
        {
            var response = new BaseResponse();
            try
            {
                var paidDebtIdList = _context.Payment.Select(x => x.DebtId);
                var debtList=(from a in _context.Debt.Where(d => d.UserId == _userId && !paidDebtIdList.Contains(d.Id))
                        join u in _context.User on a.UserId equals u.Id into usertemp
                        from x in usertemp.DefaultIfEmpty()
                        join t in _context.DebtType on a.DebtTypeId equals t.Id into typetemp
                        from y in typetemp.DefaultIfEmpty()
                        select new DebtDetail
                        {
                            Id = a.Id,
                            User = x.Name + " " + x.Surname,
                            DebtType = y.Type,
                            Date = a.Date,
                            Amount = a.Amount
                        }).ToList();
                response.Success = true;
                response.Data = debtList;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Borç listeleme işlemi sırasında hata meydana geldi.";
            }
            return response;

        }

        /// <summary>
        /// Parametre olarak verilen id değerine sahip borcu getirir.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns>Borç döner</returns>
        public Debt GetDebt(int _id)
        {
            return _context.Debt.FirstOrDefault(u => u.Id == _id);
        }

        /// <summary>
        /// Veritabanında kayıtlı olan borç bilgilerini, parametre olarak verilen borç bilgileri ile günceller.
        /// <param name="_debt"></param>
        /// </summary>
        /// <returns>Güncelleme işleminin cevabını döner/returns>
        public BaseResponse UpdateDebt(Debt _debt)
        {
            var response = new BaseResponse();
            Debt exist = _context.Debt.Find(_debt.Id);//Borcun veritabanında bulunma durumu kontrol edilir.
            if (exist != null)
            {
                try
                {
                    _context.Entry(exist).CurrentValues.SetValues(_debt); //Bulunan borç bilgileri yenileri ile güncellenir.
                    _context.SaveChanges();// Güncelleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                    response.Success = true;
                    response.Message = "Borç güncellendi";
                    response.Data = GetDebts();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    response.Success = false;
                    response.Message = "Borç güncelleme işlemi sırasında hata meydana geldi";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Borç bulunamadı";
            }

            return response;
        }

        /// <summary>
        /// Veritabanında kayıtlı olan borç bilgilerini, parametre olarak verilen borç bilgileri ile günceller.
        /// <param name="_debt"></param>
        /// </summary>
        /// <returns>Güncelleme işleminin cevabını döner/returns>
        public BaseResponse UpdateAllDebts(Debt _debt, DateTime _date, int _debtType)
        {
            var response = new BaseResponse();
            List<Debt> debts = GetDebtsByDateAndType(_date, _debtType);//Tarih ve borç tipine göre filtreleyerek güncellenecek borçları getirir.
            if (debts != null)
            {
                try
                {
                    foreach (Debt debt in debts)
                    {
                        _context.Entry(debt).CurrentValues.SetValues(_debt); //Bulunan borç bilgileri yenileri ile güncellenir.
                        _context.SaveChanges();// Güncelleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                    }

                    response.Success = true;
                    response.Message = "Borçlar güncellendi";
                    response.Data = GetDebts();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    response.Success = false;
                    response.Message = "Toplu Borç güncelleme işlemi sırasında hata meydana geldi";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Borç bulunamadı";
            }

            return response;
        }

        /// <summary>
        /// Veri tabanından verilen id'ye sahip borcu siler.
        /// <param name=_id"></param>
        /// </summary>
        /// <returns>Borç silme işleminin cevabını döner/returns>
        public BaseResponse DeleteDebt(int _id)
        {
            var response = new BaseResponse();
            try
            {
                _context.Debt.Remove(GetDebt(_id)); //GetDebt metodu ile getirilen ilgili id'ye sahip borcu siler.
                _context.SaveChanges();// Silme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                response.Success = true;
                response.Message = "Borç silindi";
                response.Data = GetDebts();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Borç silme işlemi sırasında hata meydana geldi";
            }
            return response;
        }

        /// <summary>
        /// Veri tabanından verilen tarih ve borç türüne sahip borçları siler.
        /// <param name=_id"></param>
        /// </summary>
        /// <returns>Borç silme işleminin cevabını döner/returns>
        public BaseResponse DeleteDebts(DateTime _date, int _debtType)
        {
            var response = new BaseResponse();
            List<Debt> debts = GetDebtsByDateAndType(_date, _debtType);
            if (debts != null)
            {
                try
                {
                    foreach (Debt debt in debts)
                    {
                        _context.Debt.Remove(GetDebt(debt.Id)); //GetDebt metodu ile getirilen ilgili id'ye sahip borcu siler.
                        _context.SaveChanges();// Silme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                    }

                    response.Success = true;
                    response.Message = "Borçlar silindi";
                    response.Data = GetDebts();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    response.Success = false;
                    response.Message = "Toplu Borç silme işlemi sırasında hata meydana geldi";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Borç bulunamadı";
            }

            return response;
        }


        /// <summary>
        /// Veri tabanındaki borç listesini sayfalanabilir yapıda döner.
        /// <param name=pagingParameters"></param>
        /// </summary>
        /// <returns>Borç listesinin sayfalama işlemi cevabını döner./returns>
        public BaseResponse PaingDebts(PagingParameters pagingParameters)
        {
            var response = new BaseResponse();
            try
            {
                List<Debt> pageableData = GetDebts() //Veri tabanındaki borçları listesi
               .OrderBy(u => u.Id) //Borç idlerine göre sıralar
               .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize) //Yeni sayfadaki verilerin listedeki hangi indexdeki veriden itibaren devam edeceğini belirtir.
               .Take(pagingParameters.PageSize) //Sayfada listelenecek veri miktarını belirtir.
               .ToList();
                response.Success = true;
                response.Data = pageableData;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Borçları sayfalama işlemi sırasında hata meydana geldi";
            }
            return response;
        }

        public List<Debt> GetDebtsByDateAndType(DateTime _date, int _debtType)
        {
            //Tarih ve borç tipine göre filtreleyerek güncellenecek borçları getirir.
            return _context.Debt.Where(d => d.Date == _date && d.DebtTypeId == _debtType).ToList();
        }
    }
}
