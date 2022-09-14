using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Courstick.Core.Models
{
    public class Role: IdentityRole<int>
    {
        public Role(string name ):base(name)
        {
            
        }
    }
}
