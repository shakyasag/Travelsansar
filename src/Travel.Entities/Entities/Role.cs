
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Entities.Entities
{
     public class Role:IdentityRole<int>
    {
        public virtual ICollection<UserRole> Userrole { get; set; }
    }
}
