using Sop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sop.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> Gets();
    }
}