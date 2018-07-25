using FASTUniversity.DAL;
using FASTUniversity.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FASTUniversity.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork;

        public HomeController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var data = from student in this.unitOfWork.StudentRepository.Get()
                       group student by student.EnrolementDate into dataGroup
                       select new EnrollmentDataGroup()
                       {
                           EnrollmentData = dataGroup.Key,
                           StudentCount = dataGroup.Count()
                       };

            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}