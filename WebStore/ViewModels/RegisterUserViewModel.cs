using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [MaxLength(256)]
        [DataType(DataType.Text)]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
    }
}
