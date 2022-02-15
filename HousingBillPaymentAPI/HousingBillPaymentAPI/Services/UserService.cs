using DAL.Models;
using Entities;
using HousingBillPaymentAPI.Helper;

namespace HousingBillPaymentAPI.Services
{
    public class UserService
    {
        private HousingBillPaymentContext _context = new HousingBillPaymentContext();
        private AuthorityOperations authorityOperations = new AuthorityOperations();

        /// <summary>
        /// Veri tabanına yeni bir kullanıcı ekler.
        /// </summary>
        /// <param name="_user"></param>
        /// <returns>Kullanıcı ekleme işleminin cevabını döner</returns>
        public BaseResponse AddUser(User _user)
        {
            var response = new BaseResponse();
            if(CheckIfUserExist(_user))
            {
                response.Success = false;
                response.Message = "Girilen bilgilere sahip kullanıcı sistemde mevcut.";
                response.Data = GetUsers();
            }
            else
            {
                try
                {
                    string password = authorityOperations.CreatePassword();
                    _user.Password = authorityOperations.MD5Hashing(password);
                    _context.User.Add(_user);     //Content içerisine yeni bir kullanıcı eklenir.
                    _context.SaveChanges();      //Ekleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                    response.Success = true;
                    response.Message = "Kullanıcı kaydedilmiştir. Parolanız : "+ password;
                    response.Data = GetUsers();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    response.Success = false;
                    response.Message = "Kullanıcı kaydetme işlemi sırasında hata meydana geldi.";
                    response.Data = GetUsers();
                }
            }
 
            return response;
        }

        /// <summary>
        /// Parametre olarak verilen id değerine sahip veya  kullanıcıyı getirir.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns>Daire döner</returns>
        public User GetUser(int _id)
        {
               return _context.User.FirstOrDefault(u => u.Id == _id);
        }


        /// <summary>
        /// Veritabanındaki kullanıcı bilgilerini listeler
        /// </summary>
        /// <returns>Kullanıcı listesini döner</returns>
        public List<User> GetUsers()
        {
            return _context.User.OrderBy(u => u.Id).ToList(); //Id bilgisinie göre kullanıcıları sıralanır ve getirilir.
        }

        /// <summary>
        /// Parametre olarak verilen user bilgileri ile sitemde kullanıcı adı ve parola ile eşleşen kullanıcı olup olmadığı kontrol edilir.
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>Kullanıcı adı ve parola ile eşleşen kullanıcı bilgisini döner</returns>
        public BaseResponse Login(LoginDto loginUser,IConfiguration _configuration)
        {
            var response = new BaseResponse();
            if (!string.IsNullOrWhiteSpace(loginUser.UserMail) && !string.IsNullOrWhiteSpace(loginUser.Password))
            {
                try
                {
                    User user = _context.User.First(u => u.EMail.Equals(loginUser.UserMail) && u.Password.Equals(authorityOperations.MD5Hashing(loginUser.Password))); //Username ve Password bilgileri eşleşiyorsa kullanıcı giriş işlemi başarılı olur.
                    if (user!= null)
                    {
                        response.Data = new LoginResponse()
                        {
                            Username = user.Name,
                            isAdmin = user.IsAdmin,
                            Token = authorityOperations.CreateToken(user, _configuration),
                        };
                    }; 
                        response.Success = true;
                   
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Kullanıcı E-Posta veya parola yanlış";
                }

            }
            else
            {
                response.Success = false;
                response.Message = "Kullanıcı bulunamadı";
            }
            return response;

        }

        /// <summary>
        /// Veritabanında kayıtlı olan kullanıcının bilgilerini, parametre olarak verilen kullanıcı bilgileri ile günceller.
        /// <param name="_user"></param>
        /// </summary>
        /// <returns>Güncelleme işleminin cevabını döner/returns>
        public BaseResponse UpdateUser(User _user)
        {
            var response = new BaseResponse();
            User exist = _context.User.Find(_user.Id);//Kullanıcının veritabanında bulunma durumu kontrol edilir.
            if(exist != null)
            {
                if (CheckIfUpdatedUserExist(_user))
                {
                    response.Success = false;
                    response.Message = "Girilen bilgilere sahip kullanıcı sistemde mevcut.";
                }
                else
                {
                    try
                    {
                        _context.Entry(exist).CurrentValues.SetValues(_user); //Bulunan kullanıcı bilgileri yenileri ile güncellenir.
                        _context.SaveChanges();// Güncelleme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                        response.Success = true;
                        response.Message = "Kullanıcı güncellendi";
                        response.Data = GetUsers();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        response.Success = false;
                        response.Message = "Kullanıcı güncelleme işlemi sırasında hata meydana geldi";
                    }
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Kullanıcı bulunamadı";
            }
           
            return response;
        }

        /// <summary>
        /// Veri tabanından verilen id'ye sahip kullanıcıyı siler.
        /// <param name=_id"></param>
        /// </summary>
        /// <returns>Kullanıcı silme işleminin cevabını döner/returns>
        public BaseResponse DeleteUser(int _id)
        {
            var response = new BaseResponse();
            try
            {
                _context.User.Remove(GetUser(_id)); //GetUser metodu ile getirilen ilgili id'ye sahip kullanıcıyı siler.
                _context.SaveChanges();// Silme işlemi veri tabanında değişikliklere neden olduğu için değişiklikler kaydedilir.
                response.Success = true;
                response.Message = "Kullanıcı silindi";
                response.Data = GetUsers();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Success = false;
                response.Message = "Kullanıcı silme işlemi sırasında hata meydana geldi";
                response.Data = GetUsers();
            }
            return response;
        }


        /// <summary>
        /// Veri tabanındaki kullanıcı listesini sayfalanabilir yapıda döner.
        /// <param name=pagingParameters"></param>
        /// </summary>
        /// <returns>Kullanıcı listesini sayfalama işlemi cevabını döner./returns>
        public BaseResponse PaingUsers(PagingParameters pagingParameters)
        {
            var response = new BaseResponse();
            try
            {
                 List<User> pageableData = GetUsers() //Veri tabanındaki kullanıcıları listesi
                .OrderBy(u => u.Name) //Kullanıcıları adlarına göre sıralar
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize) //Yeni sayfadaki verilerin listedeki hangi indexdeki veriden itibaren devam edeceğini belirtir.
                .Take(pagingParameters.PageSize) //Sayfada listelenecek veri miktarını belirtir.
                .ToList();
                response.Success = true;
                response.Data = pageableData;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Kullanıcıları sayfalama işlemi sırasında hata meydana geldi";
            }
            return response;
        }

        public bool CheckIfUserExist(User _user)
        {
            return _context.User.Any(u=> u.TCKN == _user.TCKN || u.EMail == _user.EMail || u.Phone == _user.Phone || (u.CarPlate == _user.CarPlate && _user.CarPlate!=""));
        }
        public bool CheckIfUpdatedUserExist(User _user)
        {
            return _context.User.Any(u => u.Id != _user.Id && (u.TCKN == _user.TCKN || u.EMail == _user.EMail || u.Phone == _user.Phone || u.CarPlate == _user.CarPlate));
        }
    }
 
}
