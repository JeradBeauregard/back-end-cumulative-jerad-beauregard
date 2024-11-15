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
	}
}
