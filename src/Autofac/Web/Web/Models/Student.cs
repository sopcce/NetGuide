using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sop.Web.Models
{
    [PetaPoco.TableName("Sop_UsersLogin")]
    public class Student : Sop.Framework.Repositories.BaseEntity
    {
        public string UserId { get; set; }
      
    }
}