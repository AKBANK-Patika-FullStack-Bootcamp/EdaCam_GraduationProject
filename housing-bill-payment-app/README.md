## FrontEnd
- Projenin front-end kısmı authentication, components, pages, store ve layout kısımlarından oluşmaktadır.
#### Authentication : 
Kullanıcı mail ve parola bilgileri ile giriş yaptığında kullanıcının rolünü içeren bir token üretilmektedir. Üretilen token jwt-decode kütüphanesinden yararlanılarak çözümlenerek token içerisindeki bilgiler localStorage’a kaydedilmiştir. Bu bilgiler sadece kullanıcı rolü,idsi ve tokendan oluşmaktadır.
Kullanıcıyı giriş esnasında üretilen tokendan elde edilen bilgiler ışığında kullanıcının yetkisine bağlı olarak (User/Admin) farklı arayüzler karşılamaktadır. Bu arayüzler Layout kısmında ele alınmıştır.
#### Layout:
Layout ile ilgili kodlar hoc klasörü altında yer almaktadır. Layout tasarımının oluşturulmasında antd kütüphanesi kullanılmıştır. 
Kullanıcıyı sahip olduğu role bağlı olarak 2 farklı menü karşılayacaktır. 
Admin’e ait olan menüde Kullanıcı İşlemleri,Daire İşlemleri,  Fatura İşlemleri, Ödeme İşlemleri ve çıkış bulunur.
Kullanıcı’ya ait olan menüde Kullanıcı Bilgileri,Faturalarım, Ödemelerim ve çıkış bulunur.
#### Pages:
Layout’da yer alan siderdaki butonlara tıklandığında değişecek ve ekranda görüntülenecek olan içerikler pages klasörü altında ele alınmıştır. Önceki kısımda ele alınan menu işlemlerinin her biri için(çıkış hariç) kullanıcıya gösterilecek olan ekranlar bu kısımdadır. 
Bunlar ApartmentPage, DebtPage, PaymentPage, UserDebtPage, UserInfoPage, UserPage ve UserPaymentPage şeklindedir.
#### Store:
Store klasörü altında backend ile iletişimin sağlanması ele alınmıştır. Bu katmanda backend’e gönderilecek istekler için endpointler belirtilerek backend istekleri atan metotlar halinde export edilmiştir. 
Backend’e atılan istekler için promise tabanlı HTTP İstemcisi olan axios kütüphanesi kullanılmıştır. Tüm servisler için bir instance oluşturulmuş(api.js’de) ve bu instance ile tüm istekler için ortak özellikler tanımlanmıştır.


#### Components:
Component katmanında uygulamada ihtiyaç duyulan tekrar kullanılabilir parçalara ayırılmış sınıflar bulunmaktadır. Birbirine benzer olan ve birbiri içerisinde kullanılan komponentler bir klasör altında toplanmıştır. 
Örneğin Daire işlemleri için oluşturulan komponentler apartments klasörü altında yer almaktadır ve içerisinde Apartment, ApartmentForm, apartmentHelper ve ApartmentTable componentleri yer almaktadır.

<br> **Apartment componenti** ApartmentPage içerisinde çağırılmaktadır ve ApartmentTable componentini döner. İçerisindeki apartmentServices sınıfından aldığı axios metotlarını Apartment table’a props olarak gönderir.

<br> **ApartmentTable** daire verilerinin çekilmesi ve diğer crud operasyonlarının yönetilmesi ile ilgilenir. Tablo yapısının sağlanması için MaterialTable kütüphanesi kullanılmaktadır.

<br> **ApartmetnForm** componenti ise ekleme-güncelleme işlemleri için kullanılan form yapısını sağlar. Form işlemleri için antd ve formik kütüphaneleri kullanılmıştır.
apartmentHelper’da ise form içerisinde kullanılan default valuelar ve güncelleme işlemi sırasında formun ilgili satırdaki verilerle dolu gelmesini sağlayan getInitialValues metodu yer almaktadır.

<br> **Login** katmanında kullanıcıyı karşılayan giriş ekranı formu bulunmaktadır. Kullanıcı mail ve parola bilgileri burada girilir ve doğrulanarak token oluşturulur.

<br> **Helpers** katmanında ise componentlerde ortak olarak kullanılan yardımcı araçlar yer almaktadır. Bunlar formun bir pop-up olarak açılmasını sağlayan OpenDialog ve slime işlemi gibi işlemlerde kullanıcı onayı almak isteyen Evet/Hayır seçeneklerinden oluşan ConfirmModalButton’dur.




---------

# Getting Started with Create React App

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in your browser.

The page will reload when you make changes.\
You may also see any lint errors in the console.

### `npm test`

Launches the test runner in the interactive watch mode.\
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `npm run build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `npm run eject`

**Note: this is a one-way operation. Once you `eject`, you can't go back!**

If you aren't satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you're on your own.

You don't have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn't feel obligated to use this feature. However we understand that this tool wouldn't be useful if you couldn't customize it when you are ready for it.

## Learn More

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).

### Code Splitting

This section has moved here: [https://facebook.github.io/create-react-app/docs/code-splitting](https://facebook.github.io/create-react-app/docs/code-splitting)

### Analyzing the Bundle Size

This section has moved here: [https://facebook.github.io/create-react-app/docs/analyzing-the-bundle-size](https://facebook.github.io/create-react-app/docs/analyzing-the-bundle-size)

### Making a Progressive Web App

This section has moved here: [https://facebook.github.io/create-react-app/docs/making-a-progressive-web-app](https://facebook.github.io/create-react-app/docs/making-a-progressive-web-app)

### Advanced Configuration

This section has moved here: [https://facebook.github.io/create-react-app/docs/advanced-configuration](https://facebook.github.io/create-react-app/docs/advanced-configuration)

### Deployment

This section has moved here: [https://facebook.github.io/create-react-app/docs/deployment](https://facebook.github.io/create-react-app/docs/deployment)

### `npm run build` fails to minify

This section has moved here: [https://facebook.github.io/create-react-app/docs/troubleshooting#npm-run-build-fails-to-minify](https://facebook.github.io/create-react-app/docs/troubleshooting#npm-run-build-fails-to-minify)
