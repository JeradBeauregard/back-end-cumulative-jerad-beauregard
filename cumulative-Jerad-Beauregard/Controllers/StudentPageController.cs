using cumulative_Jerad_Beauregard.Models;
using Microsoft.AspNetCore.Mvc;

namespace cumulative_Jerad_Beauregard.Controllers
{
	public class StudentPageController : Controller
	{
		//dependency injection for list api
		private readonly StudentAPIController _api;
		public StudentPageController(StudentAPIController api)
		{
			_api = api;
		}


		public IActionResult New()
		{
			return View();
		}

		public IActionResult Create(string firstname, string lastname, string studentnumber, string enroldate)
		{

			// Make new student object and populate it from our form data

			Student NewStudent = new Student();

			string FirstName = firstname;
			string LastName = lastname;
			string StudentNumber = studentnumber;
			DateOnly EnrolDate = DateOnly.FromDateTime(Convert.ToDateTime(enroldate));

			NewStudent.StudentFName = FirstName;
			NewStudent.StudentLName = LastName;
			NewStudent.StudentNumber = StudentNumber;
			NewStudent.EnrolDate = EnrolDate;

			//send to create student api

			int StudentId = _api.AddStudent(NewStudent);

			return RedirectToAction("Show", new { id = StudentId });


		
        }
		public IActionResult Lists()
		{

			// to get information from the database

			List<Student> Students = _api.ListStudents();


			return View(Students);
		}

		public IActionResult Show(int id)
		{
			Student SelectedStudent = _api.FindStudent(id);
			return View(SelectedStudent);
		}

		public ActionResult Delete(int id)
		{

			_api.DeleteStudent(id);
			return RedirectToAction("Lists");
		}


		public IActionResult DeleteConfirm(int id)
		{

			Student SelectedStudent = _api.FindStudent(id);


			return View(SelectedStudent);
		}

		public IActionResult Edit(int id)
		{
			Student SelectedStudent = _api.FindStudent(id);
			ViewData["selectedstudent"] = SelectedStudent;

			return View();

		}

		public IActionResult Update(int id, string firstname, string lastname, string studentnumber, string enroldate)
		{
			string fname = firstname;
			string lname = lastname;
			string snumber = studentnumber;
			DateOnly EnrolDate = DateOnly.FromDateTime(Convert.ToDateTime(enroldate));

			Student UpdatedStudent = new Student();

			UpdatedStudent.StudentFName = fname;
			UpdatedStudent.StudentLName = lname;
			UpdatedStudent.StudentNumber = snumber;
			UpdatedStudent.EnrolDate = EnrolDate;

			int StudentId = _api.UpdateStudent(id, UpdatedStudent);

			return RedirectToAction("Show", new { id = StudentId });
		}
	}
}
