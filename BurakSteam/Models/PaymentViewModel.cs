using System.ComponentModel.DataAnnotations;

namespace BurakSteam.Models;
public class PaymentViewModel
{
    [Required]
    [StringLength(16, MinimumLength = 13, ErrorMessage = "Kart numarası geçerli değil.")]
    public string? CardNumber { get; set; }

    [Required]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Son kullanma tarihi geçerli değil.")]
    public string? ExpiryDate { get; set; }  // MM/YY formatında

    [Required]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "CVC kodu geçerli değil.")]
    public string? Cvc { get; set; }

    [Required]
    public int Amount { get; set; }  // Ödeme tutarı
}
