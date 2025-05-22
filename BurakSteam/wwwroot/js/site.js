// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let currentIndex = 0;

function moveSlide(direction) {
    const slides = document.querySelectorAll('.slider-item');
    const totalSlides = slides.length;

    // Update current index
    currentIndex += direction;

    // Loop to the beginning or end
    if (currentIndex < 0) {
        currentIndex = totalSlides - 1;
    } else if (currentIndex >= totalSlides) {
        currentIndex = 0;
    }

    // Move the slider
    const slider = document.querySelector('.slider');
    slider.style.transform = `translateX(-${currentIndex * 100}%)`;
}





// Bu kısmı istediğiniz gibi özelleştirebilirsiniz, video oynatma başlatılacak
// Video oynatma ve durdurma işlemleri
document.querySelectorAll('.game-card').forEach(card => {
    card.addEventListener('mouseenter', () => {
        const video = card.querySelector('.game-video');
        if (video) {
            video.play();  // Videoyu oynat
        }
    });

    card.addEventListener('mouseleave', () => {
        const video = card.querySelector('.game-video');
        if (video) {
            video.pause();  // Videoyu durdur
            video.currentTime = 0; // Videoyu sıfırdan başlat
        }
    });
});


// Slider'ın otomatik olarak geçmesini sağlamak için
var myCarousel = document.querySelector('#imageSlider');
var carousel = new bootstrap.Carousel(myCarousel, {
    interval: 3000, // Her 3 saniyede bir kaydırılır
    ride: 'carousel' // Otomatik kaydırma aktif olacak
});




//detaylar

// Video açma işlemi için script
// Video Modal'ı açma
document.getElementById('gameVideo').addEventListener('click', function () {
    var modal = new bootstrap.Modal(document.getElementById('videoModal'));
    modal.show();
});




    document.querySelector("form").addEventListener("submit", function(event) {
        var password = document.getElementById("Password").value;
    var confirmPassword = document.getElementById("ConfirmPassword").value;
    var email = document.getElementById("Email").value;

    // Şifre doğrulama
    if (password !== confirmPassword) {
        alert("Şifreler eşleşmiyor!");
    event.preventDefault();
    return;
        }

    // E-posta doğrulama
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2, 6}$/;
    if (!emailPattern.test(email)) {
        alert("Geçerli bir e-posta adresi giriniz!");
    event.preventDefault();
    return;
        }
    });


