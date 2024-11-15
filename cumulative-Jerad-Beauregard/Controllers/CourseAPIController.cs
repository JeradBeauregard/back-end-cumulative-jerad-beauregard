using cumulative_Jerad_Beauregard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace cumulative_Jerad_Beauregard.Controllers
{
	[Route("api/Course")]
	[ApiController]
	public class CourseAPIController : ControllerBase
	{

		private readonly SchoolDbContext _context;
		// dependency injection of database context - I will trust
		public CourseAPIController(SchoolDbContext context)
		{
			_context = context;
		}


		/// <summary>
		/// Returns a list of courses from the database
		/// </summary>
		/// <example>
		/// GET api/Course/ListCourses -> [{"courseId":1,"courseCode":"http5101","teacherId":1,"startDate":"2018-09-04","finishDate":"2018-12-14","courseName":"Web Application Development"},
		/// {"courseId":2,"courseCode":"http5102","teacherId":2,"startDate":"2018-09-04","finishDate":"2018-12-14","courseName":"Project Management"}...]
		/// </example>
		/// <returns>
		/// A list of Course Objects
		/// </returns>

		[HttpGet]
		[Route(template: "ListCourses")]
		public List<Course> ListCourses()
		{
			// creating and empty list for the Course names

			List<Course> Courses = new List<Course>();

			// connecton to database

			using (MySqlConnection Connection = _context.AccessDatabase())
			{
				// opening the connection
				Connection.Open();

				// Establishing command (the query) to the database
				MySqlCommand Command = Connection.CreateCommand();

				// query to get all teacher info
				Command.CommandText = "SELECT * FROM courses";

				// storing the results in a variable

				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{


					// access all column information then output it to new Student object

					while (ResultSet.Read())
					{
						int Id = Convert.ToInt32(ResultSet["courseid"]);
						string CourseCode = ResultSet["coursecode"].ToString();
						int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
						DateOnly StartDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["startdate"]));
						DateOnly FinishDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["finishdate"]));
						string CourseName = ResultSet["coursename"].ToString();
				

						Course CurrentCourse = new Course()
						{
							CourseId = Id,
							CourseCode = CourseCode,
							TeacherId = TeacherId,
							StartDate = StartDate,
							FinishDate = FinishDate,
							CourseName = CourseName

						};

						Courses.Add(CurrentCourse);
					}

				}
				// outputs our list of Students names to api controller
				return Courses;
			}

		}


		/// <summary>
		/// Returns a Course from an ID
		/// </summary>
		/// <example>
		/// GET api/Course/FindCourse/{1} -> {"courseId":1,"courseCode":"http5101","teacherId":1,"startDate":"2018-09-04","finishDate":"2018-12-14","courseName":"Web Application Development"}
		/// </example>
		/// <returns>
		/// A Course Object
		/// </returns>
		/// 
		[HttpGet]
		[Route(template: "FindCourse/{id}")]

		public Course FindCourse(int id)
		{

			// New empty Student object

			Course SelectedCourse = new Course();

			// using to close connection after code executes
			using (MySqlConnection Connection = _context.AccessDatabase())
			{
				Connection.Open();
				//new command query to our ddatabase
				MySqlCommand Command = Connection.CreateCommand();

				//Reference our id paramater of our function in our command
				Command.CommandText = "SELECT * FROM courses WHERE courseid=@id";

				// allows our id parameter to be injected where the @id placeholder is
				Command.Parameters.AddWithValue("@id", id);

				// gather our result set
				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{

					while (ResultSet.Read())
					{
						int Id = Convert.ToInt32(ResultSet["courseid"]);
						string CourseCode = ResultSet["coursecode"].ToString();
						int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
						DateOnly StartDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["startdate"]));
						DateOnly FinishDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["finishdate"]));
						string CourseName = ResultSet["coursename"].ToString();


						SelectedCourse.CourseId = Id;
						SelectedCourse.CourseCode = CourseCode;
						SelectedCourse.TeacherId = TeacherId;
						SelectedCourse.StartDate = StartDate;
						SelectedCourse.FinishDate = FinishDate;
						SelectedCourse.CourseName = CourseName;

						


					}

				}
				return SelectedCourse;

			}
		}
	}
}

