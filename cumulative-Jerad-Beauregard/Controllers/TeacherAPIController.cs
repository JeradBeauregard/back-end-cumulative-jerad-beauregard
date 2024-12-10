using cumulative_Jerad_Beauregard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Mozilla;
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
		/// update a teacher in the database
		/// </summary>
		/// /// <parameter>Teacher Object</parameter>
		/// <example>
		/// POST: api/Teacher/UpdateTeacher
		/// Header: Content-type : application/json 
		/// Request Body (From teacher object created through page controller):
		/// {		"TeacherId : "1",
		///			"TeacherFname":"Jerad",
		///			"TeacherLname":"Beauregard",
		///			"EmployeeNumber":"T1232",
		///			"HireDate":""2016-08-05 00:00:00",
		///			"Salary":"10.11",
		///			"PhoneNumber":"123456789"
		///			
		///	} -> 	"TeacherId : "1",
		///			"TeacherFname":"Jerad",
		///			"TeacherLname":"Beauregard",
		///			"EmployeeNumber":"T1232",
		///			"HireDate":""2016-08-05 00:00:00",
		///			"Salary":"10.11",
		///			"PhoneNumber":"123456789"
		/// </example>
		/// <returns>
		/// the updated teacher object
		/// </returns>
		/// 

		[HttpPut(template:"UpdateTeacher")]
		
		public int UpdateTeacher(Teacher UpdatedTeacher)
		{
			string query = "UPDATE teachers SET teacherfname = @fname, teacherlname = @lname, employeenumber = @employeenum, hiredate = @hired, salary = @salary, teacherworknumber = @phonenumber WHERE teacherid = @id";

			using (MySqlConnection Connection = _context.AccessDatabase())
			{

				Connection.Open();

				MySqlCommand Command = Connection.CreateCommand();
				Command.CommandText = query;
				Command.Parameters.AddWithValue("@fname", UpdatedTeacher.TeacherFName);
				Command.Parameters.AddWithValue("@lname", UpdatedTeacher.TeacherLName);
				Command.Parameters.AddWithValue("@employeenum", UpdatedTeacher.EmployeeNumber);
				Command.Parameters.AddWithValue("@hired", UpdatedTeacher.HireDate);
				Command.Parameters.AddWithValue("@salary", UpdatedTeacher.Salary);
				Command.Parameters.AddWithValue("@phonenumber", UpdatedTeacher.PhoneNumber);
				Command.Parameters.AddWithValue("@id", UpdatedTeacher.TeacherId);

				Command.ExecuteNonQuery();
				return UpdatedTeacher.TeacherId;
			}

		}
		/// <summary>
		/// Add a teacher to the database
		/// </summary>
		/// /// <parameter>Teacher Object</parameter>
		/// <example>
		/// POST: api/Teacher/AddTeacher
		/// Header: Content-type : application/json 
		/// Request Body (From teacher object created through page controller):
		/// {
		///			"TeacherFname":"Jerad",
		///			"TeacherLname":"Beauregard",
		///			"EmployeeNumber":"T1232",
		///			"HireDate":""2016-08-05 00:00:00",
		///			"Salary":"10.11",
		///			"PhoneNumber":"123456789"
		///	} -> id
		/// </example>
		/// <returns>
		/// the id of the teacher that is succesfully added
		/// </returns>
		/// 

		[HttpPost]
		[Route(template: "AddTeacher")]

		public int AddTeacher(Teacher NewTeacher)
		{
			// query for our database to insert new teacher information


			string query = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary, teacherworknumber) values (@fname, @lname, @employeenum, @hired, @salary, @phonenumber)";

			//open database connection

			using (MySqlConnection Connection = _context.AccessDatabase())
			{

				Connection.Open();

				MySqlCommand Command = Connection.CreateCommand();
				Command.CommandText = query;
				Command.Parameters.AddWithValue("@fname", NewTeacher.TeacherFName);
				Command.Parameters.AddWithValue("@lname", NewTeacher.TeacherLName);
				Command.Parameters.AddWithValue("@employeenum", NewTeacher.EmployeeNumber);
				Command.Parameters.AddWithValue("@hired", NewTeacher.HireDate);
				Command.Parameters.AddWithValue("@salary", NewTeacher.Salary);
				Command.Parameters.AddWithValue("@phonenumber", NewTeacher.PhoneNumber);

				Command.ExecuteNonQuery();
				return Convert.ToInt32(Command.LastInsertedId);
			}

			return 0;

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

		/// <summary>
		/// Deletes a Teacher from the database
		/// </summary>
		/// <parameter>Teacher Id of teacher object (primary key)</parameter>
		/// <example>
		/// DELETE: api/Teacher/DeleteTeacher ->1
		/// </example>
		/// <returns>
		/// Number of rows affected by delete operation.
		/// </returns>

		[HttpDelete(template:"DeleteTeacher/{TeacherId}")]
		public int DeleteTeacher(int TeacherId)
		{
			// using so the connection will close after the code executes

			string query = "DELETE FROM teachers WHERE teacherid=@id";

			using (MySqlConnection Connection = _context.AccessDatabase())
			{
				Connection.Open();

				// Establish a new command
				MySqlCommand Command = Connection.CreateCommand();

				Command.CommandText = query;
				Command.Parameters.AddWithValue("@id", TeacherId);
				return	Command.ExecuteNonQuery();
			}
			return 0;
		}
	}
}