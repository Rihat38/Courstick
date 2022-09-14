using System.ComponentModel.DataAnnotations;

namespace Courstick.Core.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
