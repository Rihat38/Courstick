using System.ComponentModel.DataAnnotations;

namespace Courstick.Views.Auth
{
    public class AuthorizationModel
    {
            [Required(ErrorMessage = "Не указан Email")]
            public string Email { get; set; }
            public string Login { get; set; }

            [Required(ErrorMessage = "Не указан пароль")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            
            public bool IsRemember { get; set; }
    }
}
