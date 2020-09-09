using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Travel.Entities.Entities;

namespace Travel.Entities
{
    public class User:IdentityUser<int>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName);}
        }
        public Gender Gender { get; set; }
        public string Country { get; set; }
        public DateTime LastActiveDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string CreateBy { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
