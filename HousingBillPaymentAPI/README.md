## Backend
Projenin backend tarafında sistemin gerçekleşmesi için gerekli tüm işlemler sağlanmıştır.
Proje yapısı DAL,Entities ve API olmak üzere 3 katmana ayrılarak yapı kurulmuştur. 
Bu katmanlarda yer alan class ve operasyonlar şöyledir:
### 1.DAL 
Data Access Layer (DAL) katmanında yer alan Model kalsöründe sistemde kullanılacak tüm model sınıfları bulunmaktadır. 
Bunlar Apartment, ApartmentDetail, BaseResponse, Debt, DebtDetail, DebtType, Login, Message, Payment, PaymentDetail ve User’dır.
Veritabanındaki her tabloya karşılık gelen bir Model classı oluşturulmuştur.
Bunun yanı sıra verileri manipüle ederek istediğimiz şekilde görüntülememizi sağlayan Join işlemleri sonucu elde edilen verileri saklayacağımız nesne tipi için Detail sınıfları oluşturulmuştur. 
BaseResponse classı serviste gerçekleştirilen işlemler sonucunda başarı durumunu, kullanıcıya gösterilecek mesajı ve veriyi içeren bir Response objesidir. Generic bir sınıf özelliği taşıması sayesinde tüm sınıflar için esnek bir şekilde kullanılabilmektedir.
Login classı ise kullanıcı giriş işlemleri için veritabanında kayıtlı olan kullanıcı mail ve parola bilgilerini içeren bir sınıftır. Sınıfın bu değerleri giriş işlemi sırasında header’dan okunarak mail ve parola değerleri ile eşleşen kullanıcı bulunması halinde doğrulama gerçekleştirip kullanıcının Token üretmesine olanak vermektedir.

### 2.Entities
Entities katmanında bulunan HousingBillPaymentContext sınıfı veritabanı bağlantısı gerçekleştiren sınıftır.
Bu sınıfta DbContextOptionsBuilder nesnesi kullanılarak ilgili Data Source’daki veri tabanına bağlanma işlemi gerçekleştirilmektedir ve veri tabanında yer alan tablolar DataSet’ler olarak oluşturulmaktadır.

### 3. HousingBillPaymentAPI
HousingBillPaymentAPI katmanının
* **Servis Katmanında:** (CRUD) işlemlerini gerçekleştiren sınıflar yer almaktadır, Bu katman tamamen iş katmanı operasyonlarını gerçekleştiren sınıfları içerir. Yani veritabanına verileri eklemeden önce yapılan tüm kontrol ve düzenlemeler bu sınıfta yapılarak veri tabanına eklenir veya veri tabanından çekilir.
#### Örneğin kullanıcı kayıt veya güncelleme işlemleri sırasında veri tabanında unique olarak tanımlanmış alanlara daha önce başka kullanıcılar tarafından kullanılan bir değer girilmeye çalışıldığında CheckIfUserExist ve CheckIfUserExist metotları ile kontrol edilerek  Girilen bilgilere sahip kullanıcı sistemde mevcut.şeklinde bir mesaj döndürülmesi sağlanmıştır.
#### Bunun yanı sıra ilşkisel tablolar kullanıldığı için verileri çektiğimizde örneğin Debt tablosu için DebtTypeId ve UserId alanları bulunmaktadır. Bu değerleri ön yüzde kullanıcı id’si veya borç tipi id’si olarak göstermek anlamsız olacaktır.Bu nedenle bu 3 tablonun Join işlemine tabi tutularak tablolardan elde edilmek istenen değerler bir Debt Detail nesnesine kaydedilmiştir.
#### Benzer bir durum dairelerin listelenmesinde dairede konaklamakta olan kullanıcı bilgisinin getirilmesinde de bulunmaktadır. Bu noktada daireler boş olabileceği için dairede konaklayan kullanıcı bulunmayabilir bu nedenle Join işleminin Left Join olması önemlidir çünkü aksi taktirde sadece user tablosundaki id değeri ile apartment tablosundaki userId verileri eşleşen değerler gelecektir, yani boş daire bilgileri gelmeyecektir.Tüm daireleri görüntüleyebilmek için left join yapılmalıdır.
Bu ve bunun gibi veri kontrolü / manüpilasyonu gerektiren işlemler ve bunların ardından veritabanına kayıt,güncelleme,slime vb işlemler servis katmanında gerçekleştirilmiştir.

* **Helper Katmanında:** Araç özelliğindeki bu katmanda Token oluşturma, Parola üretme, Parola Hashleme ve sayfalama işlemlerine yönelik sınıflar bulunmaktadır.
<br> **Token Oluşturma:** Login işlemi sırasında kullanıcının mail adresi ve parolası control edilip doğru eşleşme varsa üretilmektedir ve token’ın Claimsleri içeisine id ve role Claimleri eklenerek kullanıcı giriş işlemi sırasında sahip olduğu role göre client ekranında farklı arayüzlere yönlendirilmesi giriş yapan kullanıcının bilgisinin alınması sağlanmıştır. Token üretme ile ilgili konfigürasyonlar için Program.cs ve appsettings.json sınıflarına ilgili ayarlar eklenmiştir.
<br> **Parola Üretme &Hashleme:** Sisteme yeni bir kullanıcı kaydı gerçekleştiğinde kullanıcıya özel olarak random 8 haneli bir parola üretilmesi sağlanmıştır. Bu parola kullanıcı kayıt işlemi sonrasında kullanıcıya Response mesajı içerisinde Parolanız: xxxxxxxx şeklinde iletilmektedir. Kullanıcı parolası üretildikten sonra parola hashlenir ve kullanıcı parolası sisteme bu şekilde kaydedilir.

* **Controller Katmanında:** Bu katmanda tüm modellere ait HTTP requestlerini(GET, POST, PUT, DELETE) yerine getirecek metotlar bulunmaktadır. Bu katmandaki metotlar servis katmanlarındaki metotları çağırır yani iş katmanı operasyonları servis katmanında gerçekleştirilir. 
Burada yalnızca servis katmanındaki işlemin döndürdüğü BaseResponse cevabının Success değerine göre başarılı veya başarısız olduğu yönünde bilgilendirmeler ile bir IActionResult cevabı dönülür. Bunun için ise içerisinde başarılı ve başarısız durumlar için BaseResponse nesnesini ve durum kodlarını döndüren BaseApiController sınıfı miras alınır. 
BaseApiController sınıfında ayrıca tüm controllerlarda ortak olarak tanımlanan Controller base alma işlemi ve [Route("api/[controller]s")] işaretleyicisi ile endpointlerdeki ortak söz diziminin de miras alınması sağlanmıştır.

<br> Ayrıca backend’e atılan isteklerin client tarafından gönderiminin sağlanabilmesi için CORS ayarları etkinleştirilmelidir.Bunun için Program.cs’e Cors konfigürasyonu eklenmiştir.

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader()); 
     


