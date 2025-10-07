# 🎮 SteamProject

**SteamProject**, Steam oyun platformundan esinlenerek geliştirilen tam kapsamlı (full-stack) bir web uygulamasıdır.  
Kullanıcıların oyunları görüntüleyip, hesap oluşturup, oyun detaylarını inceleyebildiği bir oyun mağazası simülasyonu sunmayı amaçlar.

---

## 🚀 Özellikler

- 🔹 Kullanıcı kayıt ve giriş sistemi  
- 🔹 Oyun listesi ve detay sayfaları  
- 🔹 Modern ve responsive arayüz  
- 🔹 C# tabanlı veritabanı entegrasyonu  
- 🔹 Full-stack mimari (Frontend + Backend)  
- 🔹 Modüler ve genişletilebilir yapı

---

## 🏗️ Proje Yapısı

Proje, backend kısmında **ASP.NET (C#)**, frontend kısmında ise **HTML, CSS ve JavaScript** kullanılarak geliştirilmiştir.

```
SteamProject/
│
├── BurakSteam.sln              # Visual Studio çözüm dosyası
├── /wwwroot                    # Statik dosyalar (CSS, JS, görseller)
├── /Controllers                # C# Controller dosyaları
├── /Views                      # Razor veya HTML sayfaları
├── /Models                     # Veri modelleri
├── /bin, /obj                  # Derleme çıktıları (gitignore’a eklenmeli)
└── README.md                   # Proje açıklaması (bu dosya)
```

---

## 🧩 Kullanılan Teknolojiler

| Katman | Teknoloji |
|--------|------------|
| Backend | C#, ASP.NET Core |
| Frontend | HTML5, CSS3, JavaScript |
| Veritabanı |  MSSQL |
| IDE | Visual Studio |
| Versiyon Kontrol | Git & GitHub |

---

## ⚙️ Kurulum ve Çalıştırma

### 1️⃣ Depoyu Klonla
```bash
git clone https://github.com/burakdmr25/SteamProject.git
```

### 2️⃣ Projeyi Aç
- **Visual Studio** ile `BurakSteam.sln` dosyasını aç.  
- Gerekli NuGet paketleri otomatik olarak yüklenecektir.

### 3️⃣ Veritabanı Ayarları
- Eğer veritabanı kullanıyorsan, `appsettings.json` içindeki bağlantı bilgilerini düzenle.  
- Gerekirse migration işlemini çalıştır:
```bash
dotnet ef database update
```

### 4️⃣ Uygulamayı Başlat
- Visual Studio’da **F5** veya **Run** tuşuna bas.  
- Tarayıcıda şu adrese git:
```
https://localhost:5001
```

---

## 🧹 Önerilen .gitignore

Git’e eklenmemesi gereken dosyalar:
```
.vs/
bin/
obj/
*.user
*.bak
*.log
```

---

## 🛠️ Katkıda Bulunma

Projeye katkı sağlamak istiyorsan:

1. Bu repoyu **forkla**  
2. Yeni bir branch oluştur  
   ```bash
   git checkout -b feature/yeni-ozellik
   ```
3. Değişikliklerini commit et  
   ```bash
   git commit -m "Yeni özellik eklendi"
   ```
4. Branch’ini pushla ve **Pull Request** oluştur 🎯

---

## 📈 Yol Haritası

- 🔸 Admin paneli (oyun ekleme/silme)  
- 🔸 Kullanıcı yorum sistemi  
- 🔸 Satın alma / sepet modülü  
- 🔸 API endpoint’leri  
- 🔸 Responsive arayüz geliştirmeleri  

---

## 👤 Geliştirici

**Burak Demir**  
🌐 GitHub: [github.com/burakdmr25](https://github.com/burakdmr25)

---

## 📝 Lisans

Bu proje **eğitim ve kişisel gelişim** amaçlı olarak oluşturulmuştur.  
İstersen açık kaynak lisansı (MIT, GPL, Apache vb.) ekleyebilirsin.

---

> 💡 **Not:** Proje hâlâ geliştirme aşamasındadır.  
> `.vs`, `bin`, `obj` gibi bazı dosyalar geçici olup ilerleyen sürümlerde temizlenecektir.
