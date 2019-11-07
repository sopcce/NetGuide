using System.Collections.Generic;
using System.Linq;
using AutofactMVC.Models;
using AutofactMVC.Repository.Core;

namespace AutofactMVC.Repository
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<dynamic> GetIEnumerableStudents();
        IQueryable<dynamic> GetIQueryableStudents();
    }
}