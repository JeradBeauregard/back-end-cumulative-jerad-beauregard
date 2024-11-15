using cumulative_Jerad_Beauregard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;
using System;


namespace cumulative_Jerad_Beauregard.Controllers
{
	[Route("api/Teacher")]
	[ApiController]


	public class TeacherAPIController : ControllerBase
	{
		private readonly SchoolDbContext _context;
		// dependency injection of database context - I will trust
		public TeacherAPIController(SchoolDbContext context)
		{
			_context = context;
		}


		/// <summary>
		/// Returns a list of teachers from the database
		/// </summary>
		/// <example>
		/// GET api/Teacher/ListTeachers -> [{"teacherId":1,"teacherFName":"Alexander","teacherLName":"Bennett","employeeNumber":"T378","hireDate":"2016-08-05T00:00:00","salary":55.30},
		/// {"teacherId":2,"teacherFName":"Caitlin","teacherLName":"Cummings","employeeNumber":"T381","hireDate":"2014-06-10T00:00:00","salary":62.77} ...]
		/// </example>
		/// <returns>
		/// A list of Teacher Objects
		/// </returns>

		[HttpGet]
		[Route(template: "ListTeachers")]
		public List<Teacher> ListTeachers()
		{
			// creating and empty list for the teacher names

			List<Teacher> Teachers = new List<Teacher>();

			// connecton to database

			using (MySqlConnection Connection = _context.AccessDatabase())
			{
				// opening the connection
				Connection.Open();

				// Establishing command (the query) to the database
				MySqlCommand Command = Connection.CreateCommand();

				// query to get all teacher info
				Command.CommandText = "SELECT * FROM teachers";

				// storing the results in a variable

				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{


					// access all column information then output it to new teacher object

					while (ResultSet.Read())
					{

						int Id = Convert.ToInt32(ResultSet["teacherid"]);
						string FirstName = ResultSet["teacherfname"].ToString();
						string LastName = ResultSet["teacherlname"].ToString();
						string TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
						DateTime TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
						decimal TeacherSalary = Convert.ToDecimal(ResultSet["salary"]);

						Teacher CurrentTeacher = new Teacher()
						{
							TeacherId = Id,
							TeacherFName = FirstName,
							TeacherLName = LastName,
							EmployeeNumber = TeacherEmployeeNumber,
							HireDate = TeacherHireDate,
							Salary = TeacherSalary
						};

						Teachers.Add(CurrentTeacher);
					}

				}
				// outputs our list of teacher names to api controller
				return Teachers;
			}

		}

		/// <summary>
		/// Returns a teacher from an ID
		/// </summary>
		/// <example>
		/// GET api/Teacher/FindTeacher/{1} -> {"teacherId":1,"teacherFName":"Alexander","teacherLName":"Bennett","employeeNumber":"T378","hireDate":"2016-08-05T00:00:00","salary":55.30}
		/// </example>
		/// <returns>
		/// A Teacher Object
		/// </returns>

		[HttpGet]
		[Route(template: "FindTeacher/{id}")]

		public Teacher FindTeacher(int id)
		{

			// New empty teacher object

			Teacher SelectedTeacher = new Teacher();

			// using to close connection after code executes
			using (MySqlConnection Connection = _context.AccessDatabase()) 
			{
				Connection.Open();
				//new command query to our ddatabase
				MySqlCommand Command = Connection.CreateCommand();

				//Reference our id paramater of our function in our command
				Command.CommandText = "SELECT * FROM teachers WHERE teacherid=@id";

				// allows our id parameter to be injected where the @id placeholder is
				Command.Parameters.AddWithValue("@id", id);

				// gather our result set
				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{

					while (ResultSet.Read())
					{

						int Id = Convert.ToInt32(ResultSet["teacherid"]);
						string FirstName = ResultSet["teacherfname"].ToString();
						string LastName = ResultSet["teacherlname"].ToString();
						string TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
						DateTime TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
						decimal TeacherSalary = Convert.ToDecimal(ResultSet["salary"]);

						SelectedTeacher.TeacherId = Id;
						SelectedTeacher.TeacherFName = FirstName;
						SelectedTeacher.TeacherLName = LastName;
						SelectedTeacher.EmployeeNumber = TeacherEmployeeNumber;
						SelectedTeacher.HireDate = TeacherHireDate;
						SelectedTeacher.Salary = TeacherSalary;

					}

				}
				return SelectedTeacher;

			}
		}
	}
}