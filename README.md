# BoatApp

BoatApp, çeşitli tekne verilerini yöneten ve sunan bir ASP.NET Core Web API projesidir. Bu proje, kullanıcılara tekne özelliklerini görüntüleme, düzenleme, ekleme ve silme gibi CRUD (Create, Read, Update, Delete) işlemlerini yapma imkânı sunar.

## Özellikler

- Tekne verilerini listeleme
- Yeni tekne ekleme
- Tekne bilgilerini güncelleme
- Tekne verilerini silme
- RESTful API yapısı

## Gereksinimler

- **.NET 8 SDK** veya üstü
- **SQL Server** (isteğe bağlı, uygulamada veritabanı kullanılıyorsa)
- **Visual Studio** veya **VS Code** (tercihen)

## Kurulum ve Çalıştırma

1. **Projeyi Klonlayın**:
   ```bash
   git clone https://github.com/zekeriyajpg/BoatApp.git
   cd BoatApp
2. Bağımlılıkları Yükleyin: Projenin bağımlılıklarını yüklemek için terminalde aşağıdaki komutu çalıştırın:

    ```bash 
    dotnet restore
3. Uygulamayı Çalıştırın: Projeyi çalıştırmak için aşağıdaki komutu kullanın:
    ```bash
    dotnet run --project BoatApp.WebApi
4. Uygulamayı Test Eddin

## Ek Bilgiler
- Hata Yönetimi: UseExceptionHandler middleware’i ile temel hata yönetimi sağlanmıştır.
- Güvenlik: Kimlik doğrulama ve yetkilendirme işlemleri için JWT (JSON Web Token) kullanılmıştır.
-  Swagger UI: API endpoint'lerini dokümante etmek için projenizde Swagger etkinleştirilmiştir. Çalıştırıldığında tarayıcıda 
    https://localhost:7012/swagger adresine giderek test edebilirsiniz.

## Katkıda Bulunma

Projeye katkıda bulunmak için aşağıdaki adımları izleyin:

1. Bu repoyu fork edin.
2. Kendi branch'inizde değişiklik yapın (`git checkout -b feature/ÖzellikAdı`).
3. Değişiklikleri commitleyin (`git commit -m 'Özellik ekle: ÖzellikAdı'`).
4. Branch'inizi pushlayın (`git push origin feature/ÖzellikAdı`).
5. Bir Pull Request açın.

## İletişim

Herhangi bir sorunuz veya öneriniz varsa [LinkedIn](https://www.linkedin.com/in/zekeriya-palabıyık-58a764213/) üzerinden benimle iletişime geçebilirsiniz.

