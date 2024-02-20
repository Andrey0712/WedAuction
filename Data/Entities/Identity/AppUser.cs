using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Identity
{
    public class AppUser:IdentityUser<long>
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string? Avatar { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<CartEntity>? CartEntities { get; set; }
    }
}
