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
	}
}
