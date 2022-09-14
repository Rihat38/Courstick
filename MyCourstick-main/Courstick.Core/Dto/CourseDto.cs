using Courstick.Dto;

namespace Courstick.Core.Dto;

public class CourseDto
{
    public int CourseId { get; set; }
    
    public List<PageDto> Lessons { get; set; }
    
    public AuthorDto? Author { get; set; }
    
    public string Name { get; set; }
        
    public string Description { get; set; }
        
    public double Price { get; set; }
    public string SmallDescription { get; set; }
    
    public bool IsBought { get; set; }
    
    public byte[] Image { get; set; }
}