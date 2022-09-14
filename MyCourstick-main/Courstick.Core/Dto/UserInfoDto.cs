using System.ComponentModel.DataAnnotations;
using Courstick.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Courstick.Core.Dto;

public class UserInfoDto
{
    public string Login { get; set; }
    public string Email { get; set; }
    
    public IFormFile Image { get; set; }
    
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public List<Course> Courses { get; set; }
}