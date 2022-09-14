using System.ComponentModel.DataAnnotations;

namespace Courstick.Core.Models
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }
        
        public int CourseId { get; set; }
        public int Type { get; set; }
        public string? Text { get; set; }
        public string? Movie { get; set; }
        public Comment? Comment { get; set; }
        public byte[]? Image { get; set; }
    }
}
