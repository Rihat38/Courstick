using Courstick.Core.Models;

namespace Courstick.Dto;

public class AuthorDto
{
    public string Name { get; set; }
    
    public string Surname { get; set; }

    public AuthorDto(User author)
    {
        Name = author.Name!;
        Surname = author.Surname!;
    }
}