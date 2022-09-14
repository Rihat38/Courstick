using Microsoft.AspNetCore.Identity;


namespace Courstick.Core.Models
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }
        
        public string? Surname { get; set; }
        
        public byte[]? Avatar { get; set; }
        
        public List<Course> AuthorOf { get; set; }
        
        public List<Course> Courses { get; set; }
        
        public List<Subscription> Subscriptions { get; set; }
        
        public double Balance { get; set; }
    }
}
