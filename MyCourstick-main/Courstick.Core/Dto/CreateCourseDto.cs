using Courstick.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Courstick.Core.Dto
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int[] Image { get; set; }
        
        public double Price { get; set; }
        
        public string SmallDescription { get; set; }
        
        public int Type { get; set; }
        
        public List<Page> Pages { get; set; }
    }
}