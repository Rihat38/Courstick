using Courstick.Core.Models;

namespace Courstick.Dto
{
    public class EditCourseDto
    {
        public int Id { get; set; }
        
        public int[] Image { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string SmallDescription { get; set; }
        
        public double Price { get; set; }
        
        public List<PageDto>? Pages { get; set; }
    }
}