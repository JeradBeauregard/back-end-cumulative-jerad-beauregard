using Microsoft.AspNetCore.Mvc;
using cumulative_Jerad_Beauregard.Models;
using Org.BouncyCastle.Security;
using Mysqlx.Expr;

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

	

		public IActionResult Lists(string StartDate, string EndDate)
		{

			// to get information from the database

			List<Teacher> Teachers = teacher_api.ListTeachers();

			//convert start date string to datetime

			DateTime StartDateDate = Convert.ToDateTime(StartDate);
			DateTime EndDateDate = Convert.ToDateTime(EndDate);


			// loop through teachers list output to new list of teachers within hiring range

			List<Teacher> TeachersInRange = new List<Teacher>();

			for(int i = 0; i < Teachers.Count; i++)
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

		


	}
}

