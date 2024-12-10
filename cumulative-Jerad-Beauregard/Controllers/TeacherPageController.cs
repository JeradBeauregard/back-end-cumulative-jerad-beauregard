using Microsoft.AspNetCore.Mvc;
using cumulative_Jerad_Beauregard.Models;
using Org.BouncyCastle.Security;
using Mysqlx.Expr;
using Mysqlx.Datatypes;
using System.Diagnostics;


namespace cumulative_Jerad_Beauregard.Controllers
{
	public class TeacherPageController : Controller
	{
		//dependency injection for list apis
		private readonly TeacherAPIController teacher_api;
		private readonly CourseAPIController course_api;
		public TeacherPageController(TeacherAPIController TeacherApi, CourseAPIController CourseApi)
		{
			teacher_api = TeacherApi;
			course_api = CourseApi;
		}


		public ActionResult Delete(int id)
		{

			teacher_api.DeleteTeacher(id);
			return RedirectToAction("Lists");
		}


		public IActionResult DeleteConfirm(int id)
		{

			Teacher SelectedTeacher = teacher_api.FindTeacher(id);


			return View(SelectedTeacher);
		}

		public IActionResult New()
		{



			return View();

		}

		public IActionResult Create(string firstname, string lastname, string employeenumber, string hiredate, string salary, string phonenumber)
		{


			// take in form data

			string fname = firstname;
			string lname = lastname;
			string enumber = employeenumber;
			DateTime hired = Convert.ToDateTime(hiredate);
			decimal Salary = Convert.ToDecimal(salary);
			string Phonenumber = phonenumber;


			// make new teacher object

			Teacher NewTeacher = new Teacher();

			// add form data to new teacher object 

			NewTeacher.TeacherFName = fname;
			NewTeacher.TeacherLName = lname;
			NewTeacher.EmployeeNumber = enumber;
			NewTeacher.HireDate = hired;
			NewTeacher.Salary = Salary;
			NewTeacher.PhoneNumber = Phonenumber;



			// send to new teacher api

			int TeacherId = teacher_api.AddTeacher(NewTeacher);

			return RedirectToAction("Show", new { id = TeacherId });

		}



		public IActionResult Lists(string StartDate, string EndDate)
		{

			// to get information from the database

			List<Teacher> Teachers = teacher_api.ListTeachers();

			//convert start date string to datetime

			DateTime StartDateDate = Convert.ToDateTime(StartDate);
			DateTime EndDateDate = Convert.ToDateTime(EndDate);


			// loop through teachers list output to new list of teachers within hiring range

			List<Teacher> TeachersInRange = new List<Teacher>();

			for (int i = 0; i < Teachers.Count; i++)
			{
				if (Teachers[i].HireDate >= StartDateDate && Teachers[i].HireDate <= EndDateDate)
				{
					TeachersInRange.Add(Teachers[i]);
				}
			}

			//output search result to view


			ViewData["teachersearch"] = TeachersInRange;
			ViewData["teachers"] = Teachers;


			return View();
		}

		public IActionResult Show(int id)
		{
			Teacher SelectedTeacher = teacher_api.FindTeacher(id);



			List<Course> Courses = course_api.ListCourses();
			List<Course> TeacherCourses = new List<Course>();

			//loop through listcourses
			for (int i = 0; i < Courses.Count; i++)
			{

				//if teacher if and course id match add course to teachercourselist
				if (SelectedTeacher.TeacherId == Courses[i].TeacherId)
				{
					TeacherCourses.Add(Courses[i]);
				}
			}



			//output teacher courselist to show view using view data

			ViewData["selectedteacher"] = SelectedTeacher;
			ViewData["courses"] = TeacherCourses;


			return View();


		}


		public IActionResult Edit(int id)
		{
			Teacher SelectedTeacher = teacher_api.FindTeacher(id);
			ViewData["selectedteacher"] = SelectedTeacher;

			return View();

		}
		/*
		public IActionResult Update(int id, string firstname, string lastname, string employeenumber, string hiredate, string salary, string phonenumber)
		{

			string fname = firstname;
			string lname = lastname;
			string enumber = employeenumber;
			DateTime hired = Convert.ToDateTime(hiredate);
			decimal Salary = Convert.ToDecimal(salary);
			string Phonenumber = phonenumber;


			// make new teacher object

			Teacher UpdatedTeacher = new Teacher();

			// add form data to new teacher object 


			UpdatedTeacher.TeacherFName = fname;
			UpdatedTeacher.TeacherLName = lname;
			UpdatedTeacher.EmployeeNumber = enumber;
			UpdatedTeacher.HireDate = hired;
			UpdatedTeacher.Salary = Salary;
			UpdatedTeacher.PhoneNumber = Phonenumber;


			int TeacherId = teacher_api.UpdateTeacher(id, UpdatedTeacher);
			

			return RedirectToAction("Show", new { id = TeacherId });

		}
		*/

		public IActionResult UpdateAjax([FromBody] TeacherData teacherData)
		{

		Teacher UpdatedTeacher = new Teacher();

			UpdatedTeacher.TeacherId = Convert.ToInt32(teacherData.TeacherId);
			UpdatedTeacher.TeacherFName = teacherData.TeacherFName;
			UpdatedTeacher.TeacherLName = teacherData.TeacherLName;
			UpdatedTeacher.HireDate = Convert.ToDateTime(teacherData.HireDate);
			UpdatedTeacher.Salary = Convert.ToDecimal(teacherData.Salary);
			UpdatedTeacher.PhoneNumber = teacherData.PhoneNumber;



			int TeacherId = teacher_api.UpdateTeacher(UpdatedTeacher);


			return Json(new { success = true, message = "Teacher updated successfully!" });
			

		}
	}
}

