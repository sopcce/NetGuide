using System.Linq;
using System.Web.Mvc;
using AutofactMVC.Models;
using AutofactMVC.Repository;
using AutofactMVC.Repository.Core;

namespace AutofactMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWorkRepository;
        public HomeController(IUnitOfWork unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        public ActionResult Index(Student sessionStudent)
        {
            //Repository使用IEnumerable返回结果
            var studentRepository = _unitOfWorkRepository.GetRepository<IStudentRepository>();
            var students = studentRepository.GetIEnumerableStudents().Take(2).ToList();
            if (sessionStudent != null)
            {
                students.Add(sessionStudent);
            }
            //Repository使用IQueryable返回结果
            //var students = _studentRepository.GetIQueryableStudents().Take(2);
            return View(students);
        }

        public ActionResult SetSession()
        {
            var student = new Student
                              {
                                  Age = 18,
                                  Id= 13,
                                  Name = "Tester"
                              };
            Session["student"] = student;
            return new ContentResult {Content = "Add student in session"};
        }

        public ActionResult AddNewStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewStudent(Student student)
        {
            return null;
        }
    }
}
