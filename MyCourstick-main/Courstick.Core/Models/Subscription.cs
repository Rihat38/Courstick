using System.ComponentModel.DataAnnotations;

namespace Courstick.Core.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public SubType SubscriptionType { get; set; }
        public List<User> Users { get; set; }
    }
}
