using System.ComponentModel.DataAnnotations;

namespace Entity.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email boş geçilemez.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifrenizi girmelisiniz.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public string returnUrl { get; set; }
    }
}
