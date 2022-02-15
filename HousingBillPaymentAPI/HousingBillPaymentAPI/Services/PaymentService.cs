using DAL.Models;
using Entities;
using HousingBillPaymentAPI.Helper;

namespace HousingBillPaymentAPI.Services
{
    public class PaymentService
    {
        private HousingBillPaymentContext _context = new HousingBillPaymentContext();

        /// <summary>
        /// Veri tabanına yeni bir ödeme ekler.
        /// </summary>
        /// <param name="_payment"></param>
        /// <returns>Ödeme ekleme işleminin cevabını döner</returns>
        public BaseResponse AddPayment(Payment _payment)
        {
            var response = new BaseResponse();
            try
            {
                _context.Payment.Add(_payment);    //Content içerisine yeni bir ödeme eklenir.
                _context.SaveChanges();           //Ekleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                response.Success = true;
                response.Message = "Ödeme kaydedilmiştir.";
                response.Data = GetPayments();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Ödeme kaydetme işlemi sırasında hata meydana geldi.";
            }
            return response;
        }

        /// <summary>
        /// Veritabanında bulunan ödemeleri getirir.
        /// </summary>
        /// <returns>Ödeme listesi döner</returns>
        public List<Payment> GetPayments()
        {
            return _context.Payment.OrderBy(p => p.Id).ToList(); //Id bilgisinie göre ödemeler sıralanır ve getirilir.
        }

        /// <summary>
        /// Veritabanındaki ödeme,borç ve kullanıcı tablolarını Join ederek oluşan detay bilgilerini getirir.
        /// </summary>
        /// <returns>Ödeme detay listesini döner</returns>
        public List<PaymentDetail> GetPaymentDetails()
        {

            return (from p in _context.Payment
                    join u in _context.User on p.UserId equals u.Id into usertemp
                    from x in usertemp.DefaultIfEmpty()
                    join d in _context.Debt on p.DebtId equals d.Id into debttemp
                    from y in debttemp.DefaultIfEmpty()
                    join a in _context.DebtType on y.DebtTypeId equals a.Id into debtTypetemp
                    from b in debtTypetemp.DefaultIfEmpty()
                    select new PaymentDetail
                    {
                        Id = p.Id,
                        PaymentDate = p.Date,
                        DebtDate = y.Date,
                        Amount=y.Amount,
                        User = x.Name + " " + x.Surname,
                        DebtType = b.Type,
                    }).ToList();
        }

        /// <summary>
        /// Parametre olarak verilen id değerine sahip ödemeyi getirir.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns>Ödeme döner</returns>
        public Payment GetPayment(int _id)
        {
            return _context.Payment.FirstOrDefault(p => p.Id == _id);
        }
        /// <summary>
        /// Kullanıcı id bilgisi parametresi ile kullanıcıya ait ödemeleri getirir.
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns>Ödeme listesi döner</returns>
        public BaseResponse GetPaymentWithUserId(int _userId)
        {
            var response = new BaseResponse();
            try
            {
                 var payments=(from p in _context.Payment.Where(p => p.UserId == _userId)
                         join u in _context.User on p.UserId equals u.Id into usertemp
                        from x in usertemp.DefaultIfEmpty()
                        join d in _context.Debt on p.DebtId equals d.Id into debttemp
                        from y in debttemp.DefaultIfEmpty()
                        join a in _context.DebtType on y.DebtTypeId equals a.Id into debtTypetemp
                        from b in debtTypetemp.DefaultIfEmpty()
                        select new PaymentDetail
                        {
                            Id = p.Id,
                            PaymentDate = p.Date,
                            DebtDate = y.Date,
                            Amount = y.Amount,
                            User = x.Name + " " + x.Surname,
                            DebtType = b.Type,
                        }).ToList();
              //  var payments = _context.Payment.Where(p => p.UserId == _userId); //Kullanıcı idsine göre ödemelerini getirir. 
                response.Success = true;
                response.Data = payments;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Ödeme listeleme işlemi sırasında hata meydana geldi.";
            }
            return response;

        }

        /// <summary>
        /// Veritabanında kayıtlı olan ödeme bilgilerini, parametre olarak verilen ödeme bilgileri ile günceller.
        /// <param name="_payment"></param>
        /// </summary>
        /// <returns>Güncelleme işleminin cevabını döner/returns>
        public BaseResponse UpdatePayment(Payment _payment)
        {
            var response = new BaseResponse();
            Payment exist = _context.Payment.Find(_payment.Id);//Ödemenin veritabanında bulunma durumu kontrol edilir.
            if (exist != null)
            {
                try
                {
                    _context.Entry(exist).CurrentValues.SetValues(_payment); //Bulunan ödeme bilgileri yenileri ile güncellenir.
                    _context.SaveChanges();// Güncelleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                    response.Success = true;
                    response.Message = "Ödeme güncellendi";
                    response.Data = GetPayments();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    response.Success = false;
                    response.Message = "Ödeme güncelleme işlemi sırasında hata meydana geldi";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Ödeme bulunamadı";
            }

            return response;
        }

        /// <summary>
        /// Veri tabanından verilen id'ye sahip ödeme siler.
        /// <param name=_id"></param>
        /// </summary>
        /// <returns>Ödeme silme işleminin cevabını döner/returns>
        public BaseResponse DeletePayment(int _id)
        {
            var response = new BaseResponse();
            try
            {
                _context.Payment.Remove(GetPayment(_id)); //GetPayment metodu ile getirilen ilgili id'ye sahip ödemeleri siler.
                _context.SaveChanges();// Silme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                response.Success = true;
                response.Message = "Ödeme silindi";
                response.Data = GetPayments();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Ödeme silme işlemi sırasında hata meydana geldi";
            }
            return response;
        }

        /// <summary>
        /// Veri tabanındaki ödeme listesini sayfalanabilir yapıda döner.
        /// <param name=pagingParameters"></param>
        /// </summary>
        /// <returns>Ödeme listesinin sayfalama işlemi cevabını döner./returns>
        public BaseResponse PaingPayments(PagingParameters pagingParameters)
        {
            var response = new BaseResponse();
            try
            {
                List<Payment> pageableData = GetPayments() //Veri tabanındaki ödemeleri listesi
               .OrderBy(p => p.Id) //Ödeme idsine göre sıralar
               .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize) //Yeni sayfadaki verilerin listedeki hangi indexdeki veriden itibaren devam edeceğini belirtir.
               .Take(pagingParameters.PageSize) //Sayfada listelenecek veri miktarını belirtir.
               .ToList();
                response.Success = true;
                response.Data = pageableData;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ödeme sayfalama işlemi sırasında hata meydana geldi";
            }
            return response;
        }
    }
}
