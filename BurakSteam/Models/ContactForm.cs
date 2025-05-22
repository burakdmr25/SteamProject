    using System.ComponentModel.DataAnnotations;

    namespace BurakSteam.Models
    {
        public class ContactForm
        {
            public int Id { get; set; } // Birincil anahtar

            [Required(ErrorMessage = "Adınız gerekli.")]
            public string? Name { get; set; }

            [Required(ErrorMessage = "E-posta adresiniz gerekli.")]
            [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "Mesajınız gerekli.")]
            [StringLength(500, ErrorMessage = "Mesajınız en fazla 500 karakter olabilir.")]
            public string? Message { get; set; }

            public DateTime SubmittedAt { get; set; } // Formun gönderilme tarihi
        }
    }

