using System.ComponentModel.DataAnnotations;

namespace Courstick.Core.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public string Description { get; set; }
        public List<User> Author { get; set; }
        public double? Rating { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
        public TimeOnly Passing_Time { get; set; }
        public List<Page>? Page { get; set; }
        public Status? Status { get; set; }
        public List<Tag>? Tag { get; set; }
        public List<User>? Users { get; set; }
    }
}
