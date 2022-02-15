# EdaCam_GraduationProject
- Site Fatura Ödeme Sisteminde bir sitede yer alan dairelerin aidat ve faturalarının yönetimi için bir sistem oluşturulmuştur. Bu sistemde 2 tip kullanıcı bulunmaktadır. Bunlar; yönetici ve kullanıcıdır.

<br> Yönetici rolüne sahip kullanıcı sistemin yönetilmesine dair işlemleri gerçekleştirme yeteneğine sahiptir.Bu işlemler ;

-	Daire bilgilerini girebilir. Daire/konut bilgilerini listeler, düzenler siler.
-	İkamet eden kullanıcı bilgilerini girer. Kişileri listeler, düzenler, siler.
-	Daire başına ödenmesi gereken aidat ve fatura bilgilerini girer(Aylık olarak). Toplu veya tek tek atama yapılabilir. Aylık olarak borç-alacak listesini görür.
-	Gelen ödeme bilgilerini görür.
-	Gelen mesajları görür.(Mesajların okunmuş/okunmamış/yeni mesaj olduğu anlaşılmalıdır.)

<br> Kullanıcı rolüne sahip kullanıcı sitede yaşayan kişilerin fatura ve aidat işlemlerini gerçekleştirme yeteneğine sahip olacaktır. Bu işlemler ;

-	Kendisine atanan fatura ve aidat bilgilerini görür.
- Yaptığı ödemelerini görür şeklindedir.
- Sadece kredi kartı ile ödeme yapabilir.
- Yöneticiye mesaj gönderebilir.(Mesajların okunmuş/okunmamış/yeni mesaj olduğu anlaşılmalı.)

- Ayrıca bu sistemde kullanıcılarının kredi kartları ile ödeme işlemlerini gerçekleştirebilmeleri için ayrı bir servis gerekmektedir. Bu servis ile kullanıcıların banka bilgileri doğrulanarak ödeme işlemini gerçekleştirmeleri sağlanmalıdır.

<br> Kullanılan Teknolojiler
1. Web projesi Backend için 
.Net Core
2. Web projesi Frontend için
React.js 
3. Sistemin yönetimi/database için
 MS SQL Server 

Veri Tabanı Mimarisi

![image](https://user-images.githubusercontent.com/54909611/153974237-ed268256-817c-47a1-b375-a44677387c8f.png)
 
- Her tabloda Primary key özelliğinde bir Id tanımlanmıştır. 
- Daire tablosunda dairede yaşayan kullanıcı bilgisinin tutulması için User tablosunun idsini referans alan FK_Apartment_User Foreign keyi tanımlanarak bu iki tablo arasındaki ilişki sağlanmıştır.
- Mesaj tablosunda mesajı gönderen kullanıcı bilgisinin tutulması için User tablosunun idsini referans alan FK_Message_User foreign keyi tanımlanarak bu iki tablo arasındaki ilişki sağlanmıştır.
- Borç tablosunda borç sahibinin bilgisinin tutulması için User tablosunun idsini referans alan FK_Debt_User foreign keyi tanımlanarak bu iki tablo arasındaki ilişki sağlanmıştır.
- Debt Tablosunda borç tipinin ve borç sahibinin tutulması için için User ve DebtType tablolarının idlerini referans alan FK_Debt_User ve FK_Debt_DebtType foreign keyleri tanımlanarak bu iki tablo ve Debt tablosu arasındaki ilişki sağlanmıştır.
- Payment tablosunda ödeme sahibinin ve borç bilgisinin tutulması için User ve Debt tablolarının idlerini referans alan FK_Payment_User ve FK_Debt_User foreign keyleri tanımlanarak bu iki tablo ve Payment tablosu arasındaki ilişki sağlanmıştır.
- User tablosundaki TCKN, E-Mail, Phone, Car Plate değerleri kullanıcıya özel değerler olduğu için bu değerler Unique key olarak tanımlanmıştır.
- Apartment tablosunda Block ve ApartmentNo aynı blokta yanlızca bir daire numarası bulunabileceğinden iki değer birlikte UK_Block_ApartmentNo şeklinde tanımlanmıştır.

---------------
## Kullanıcı Arayüzleri
### Giriş Ekranı
![giris](https://user-images.githubusercontent.com/54909611/153983741-e65a6bf9-f8be-4e8c-ae95-53ff01b55707.JPG)

### ADMIN- Kullanıcılar
![kullanıcılar](https://user-images.githubusercontent.com/54909611/153983817-6d161f9a-d5ee-43f9-a0df-0b1b197f9989.JPG)

### ADMIN - Kullanıcı Güncelleme
![kullanıcıgüncelle](https://user-images.githubusercontent.com/54909611/153983852-4a73365c-8576-474d-9b24-9a7efe9be92c.JPG)

### ADMIN - Daire Güncelleme
![dairegüncelle](https://user-images.githubusercontent.com/54909611/153983939-93486f43-e68d-43c6-b41d-69cc76ecfb30.JPG)

### ADMIN - Fatura İşlemleri
![faturaislem](https://user-images.githubusercontent.com/54909611/153984023-f09d39ca-caf1-4576-a6d7-34c9d6b13da1.JPG)

### ADMIN - Ödemeler
![Ödemeler](https://user-images.githubusercontent.com/54909611/153984056-17259418-370c-4220-8c4d-ffcdda9d59d7.JPG)

### USER - Kullanıcı Bilgileri
![UserKullanıcıBilgileri](https://user-images.githubusercontent.com/54909611/153984097-8b288100-a333-413d-9e92-c0a7065043e8.JPG)

### USER - Faturalarım
![KullanıcıFaturalarım](https://user-images.githubusercontent.com/54909611/153984148-3e5e864e-6c02-4fdb-be0b-32a5de4fb749.JPG)

### USER - Fatura Ödeme
![Ödeme](https://user-images.githubusercontent.com/54909611/153984253-8735a067-3c01-4fae-ae8c-235e9393787d.JPG)

### USER - Ödemelerim
![KullanıcıÖdemelerim](https://user-images.githubusercontent.com/54909611/153984212-81f7eb49-4e29-4285-b27b-ce353fdc39c0.JPG)

Admin Demo :https://watch.screencastify.com/v/3gxrUjLJcfjI8DY6A4Jl
User Demo : https://watch.screencastify.com/v/Do6AKBT3BRW516ei5cUK




