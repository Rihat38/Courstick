using System.ComponentModel.DataAnnotations; 

namespace Courstick.Views.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Login { get; set; }
        

    }
}

