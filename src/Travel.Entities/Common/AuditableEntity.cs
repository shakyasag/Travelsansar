using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Entities.Common
{
     public  abstract class AuditableEntity
    {

         public Guid? CreatedBy { get; set; }

        public DateTime CreatedTs { get; set; }

        public Guid? LastModifiedBy { get; set; }

        public DateTime? LastModifiedTs { get; set; }
    }
}
