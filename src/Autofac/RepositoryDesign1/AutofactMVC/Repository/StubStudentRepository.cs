using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutofactMVC.Models;

namespace AutofactMVC.Repository
{
    public class StubStudentRepository: IStudentRepository
    {
        public IEnumerable<dynamic> GetIEnumerableStudents()
        {
            return new[]
                       {
                           new Student {Id = 1, Name = "Sam", Age = 14}
                       };
        }

        public IQueryable<dynamic> GetIQueryableStudents()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Student> All()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Student> Filter(Expression<Func<Student, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Student> Filter(Expression<Func<Student, bool>> filter, out int total, int index = 0, int size = 50)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Expression<Func<Student, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Student Find(params object[] keys)
        {
            throw new NotImplementedException();
        }

        public Student Find(Expression<Func<Student, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Student t)
        {
            throw new NotImplementedException();
        }

        public void Delete(Student t)
        {
            throw new NotImplementedException();
        }

        public int Delete(Expression<Func<Student, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(Student t)
        {
            throw new NotImplementedException();
        }

        public Student FirstOrDefault(Expression<Func<Student, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}