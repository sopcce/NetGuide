using Sop.Framework.Repositories;
using Sop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sop.Services
{
    public class StudentService : IStudentService
    {
    

        private readonly IRepository<Student> _studentRepository;

        public StudentService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }



        public IEnumerable<Student> Gets()
        {
           return  _studentRepository.Table.ToList();
        }

    
    }
}