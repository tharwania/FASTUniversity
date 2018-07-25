using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FASTUniversity.DAL;
using FASTUniversity.Models;
using FASTUniversity.Domain.ViewModels;

namespace FASTUniversity.Controllers
{
    public class InstructorController : Controller
    {
        private UnitOfWork unitOfWork;
        public InstructorController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Instructors
        public ActionResult Index(int? id, int? courseID)
        {
            var viewModel = new InstructorIndexData();
            viewModel.Instructors = unitOfWork.InstructorRepository
                .Get(null, x => x.OrderBy(y => y.LastName),
                "OfficeAssignment,Courses,Courses.Department");
            


            if (id.HasValue)
            {

                ViewBag.InstructorID = id.Value;
                viewModel.Courses = viewModel.Instructors.Where(x => x.ID == id.Value).Single().Courses;
               
            }

            if (courseID.HasValue)
            {
                ViewBag.CourseID = courseID;
                viewModel.Enrollments = viewModel.Courses.Where(x => x.CourseID == courseID.Value).Single().Enrollements;
            }

            return View(viewModel);
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = unitOfWork.InstructorRepository.GetByID(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(unitOfWork.OfficeAssignmentRepository.Get(), "InstructorID", "Location");
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,HireDate")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.InstructorRepository.Insert(instructor);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(unitOfWork.OfficeAssignmentRepository.Get(), "InstructorID", "Location", instructor.ID);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = unitOfWork.InstructorRepository.GetByID(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(unitOfWork.InstructorRepository.Get(), "InstructorID", "Location", instructor.ID);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,HireDate")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.InstructorRepository.Update(instructor);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(unitOfWork.OfficeAssignmentRepository.Get(), "InstructorID", "Location", instructor.ID);
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = unitOfWork.InstructorRepository.GetByID(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = unitOfWork.InstructorRepository.GetByID(id);
            unitOfWork.InstructorRepository.Delete(instructor);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
