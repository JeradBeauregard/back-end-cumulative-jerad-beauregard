using cumulative_Jerad_Beauregard.Models;
using Microsoft.AspNetCore.Mvc;

namespace cumulative_Jerad_Beauregard.Controllers
{
	public class CoursePageController : Controller
	{
		//dependency injection for list api
		private readonly CourseAPIController _api;
		public CoursePageController(CourseAPIController api)
		{
			_api = api;
		}


		public IActionResult New()
		{
			return View();
		}


		public IActionResult Create(string coursecode, string teacherid, string startdate, string enddate, string coursename)
		{

			string CourseCode = coursecode;
			int TeacherId = Convert.ToInt32(teacherid);
            DateOnly StartDate = DateOnly.FromDateTime(Convert.ToDateTime(startdate));
            DateOnly EndDate = DateOnly.FromDateTime(Convert.ToDateTime(enddate));
			string CourseName = coursename;

			Course NewCourse = new Course();

			NewCourse.CourseCode = CourseCode;
			NewCourse.TeacherId = TeacherId;
			NewCourse.StartDate = StartDate;
			NewCourse.FinishDate = EndDate;
			NewCourse.CourseName = CourseName;

			int CourseId = _api.AddCourse(NewCourse);

            return RedirectToAction("Show", new { id= CourseId });

        }
		public IActionResult Lists()
		{

			// to get information from the database

			List<Course> Course = _api.ListCourses();


			return View(Course);
		}

		public IActionResult Show(int id)
		{
			Course SelectedCourse = _api.FindCourse(id);
			return View(SelectedCourse);
		}

		public ActionResult Delete(int id)
		{

			_api.DeleteCourse(id);
			return RedirectToAction("Lists");
		}


		public IActionResult DeleteConfirm(int id)
		{

			Course SelectedCourse = _api.FindCourse(id);


			return View(SelectedCourse);
		}
	}
}
