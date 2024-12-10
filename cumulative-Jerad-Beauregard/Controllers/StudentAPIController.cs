using cumulative_Jerad_Beauregard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace cumulative_Jerad_Beauregard.Controllers
{
	[Route("api/Student")]
	[ApiController]
	public class StudentAPIController : ControllerBase
	{

		private readonly SchoolDbContext _context;
		// dependency injection of database context - I will trust
		public StudentAPIController(SchoolDbContext context)
		{
			_context = context;
		}


		/// <summary>
		/// Returns a list of students from the database
		/// </summary>
		/// <example>
		/// GET api/Student/ListStudents -> [{"studentId":1,"studentFName":"Sarah","studentLName":"Valdez","studentNumber":"N1678","enrolDate":"2018-06-18"},
		/// {"studentId":2,"studentFName":"Jennifer","studentLName":"Faulkner","studentNumber":"N1679","enrolDate":"2018-08-02"}...]
		/// </example>
		/// <returns>
		/// A list of Student Objects
		/// </returns>

		[HttpGet]
		[Route(template: "ListStudents")]
		public List<Student> ListStudents()
		{
			// creating and empty list for the Student names

			List<Student> Students = new List<Student>();

			// connecton to database

			using (MySqlConnection Connection = _context.AccessDatabase())
			{
				// opening the connection
				Connection.Open();

				// Establishing command (the query) to the database
				MySqlCommand Command = Connection.CreateCommand();

				// query to get all teacher info
				Command.CommandText = "SELECT * FROM students";

				// storing the results in a variable

				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{


					// access all column information then output it to new Student object

					while (ResultSet.Read())
					{

						int Id = Convert.ToInt32(ResultSet["studentid"]);
						string FirstName = ResultSet["studentfname"].ToString();
						string LastName = ResultSet["studentlname"].ToString();
						string StudentNumber = ResultSet["studentnumber"].ToString();
						DateOnly EnrolDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["enroldate"]));

						Student CurrentStudent = new Student()
						{
							StudentId = Id,
							StudentFName = FirstName,
							StudentLName = LastName,
							StudentNumber = StudentNumber,
							EnrolDate = EnrolDate

						};

						Students.Add(CurrentStudent);
					}

				}
				// outputs our list of Students names to api controller
				return Students;
			}

		}

		/// <summary>
		/// Returns a Student from an ID
		/// </summary>
		/// <example>
		/// GET api/Student/FindStudent/{1} -> {"studentId":1,"studentFName":"Sarah","studentLName":"Valdez","studentNumber":"N1678","enrolDate":"2018-06-18"}
		/// </example>
		/// <returns>
		/// A Student Object
		/// </returns>
		[HttpGet]
		[Route(template: "FindStudent/{id}")]

		public Student FindStudent(int id)
		{

			// New empty Student object

			Student SelectedStudent = new Student();

			// using to close connection after code executes
			using (MySqlConnection Connection = _context.AccessDatabase())
			{
				Connection.Open();
				//new command query to our ddatabase
				MySqlCommand Command = Connection.CreateCommand();

				//Reference our id paramater of our function in our command
				Command.CommandText = "SELECT * FROM students WHERE studentid=@id";

				// allows our id parameter to be injected where the @id placeholder is
				Command.Parameters.AddWithValue("@id", id);

				// gather our result set
				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{

					while (ResultSet.Read())
					{

						int Id = Convert.ToInt32(ResultSet["studentid"]);
						string FirstName = ResultSet["studentfname"].ToString();
						string LastName = ResultSet["studentlname"].ToString();
						string StudentNumber = ResultSet["studentnumber"].ToString();
						DateOnly EnrolDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["enroldate"]));

						SelectedStudent.StudentId = Id;
						SelectedStudent.StudentFName = FirstName;
						SelectedStudent.StudentLName = LastName;
						SelectedStudent.StudentNumber = StudentNumber;
						SelectedStudent.EnrolDate = EnrolDate;

					}

				}
				return SelectedStudent;

			}
		}

		/// <summary>
		/// Add a student to the database
		/// </summary>
		/// /// <parameter>Student Object</parameter>
		/// <example>
		/// POST: api/Student/AddStudent
		/// Header: Content-type : application/json 
		/// Request Body (From Student object created through page controller):
		/// {
		///			"StudentFName":"Jerad",
		///			"StudentLName":"Beauregard",
		///			"StudentNumber":"n3231",
		///			"EnrolDate":"2018-06-18"
		///	} -> status code 201 Created 
		/// </example>
		/// <returns>
		/// the id of the student that is succesfully added
		/// </returns>
		/// 

		[HttpPost]
		[Route(template: "AddStudent")]

		public int AddStudent(Student NewStudent)
		{

			string query = "INSERT INTO students (studentfname, studentlname, studentnumber,enroldate) VALUES (@fname, @lname, @studentnumber, @enroll)";

			using (MySqlConnection Connection = _context.AccessDatabase())
			{

				Connection.Open();

				MySqlCommand Command = Connection.CreateCommand();
				Command.CommandText = query;
				Command.Parameters.AddWithValue("@fname", NewStudent.StudentFName);
				Command.Parameters.AddWithValue("@lname", NewStudent.StudentLName);
				Command.Parameters.AddWithValue("@studentnumber", NewStudent.StudentNumber);
				Command.Parameters.AddWithValue("@enroll", NewStudent.EnrolDate);

				Command.ExecuteNonQuery();
				return Convert.ToInt32(Command.LastInsertedId);
			}


		}


		/// <summary>
		/// Deletes a student from the database
		/// </summary>
		/// <parameter>Student Id of student object (primary key)</parameter>
		/// <example>
		/// DELETE: api/Student/DeleteStudent ->1
		/// </example>
		/// <returns>
		/// Number of rows affected by delete operation.
		/// </returns>

		[HttpDelete(template: "DeleteStudent/{StudentId}")]

		public int DeleteStudent(int StudentId)
		{

			string query = "DELETE FROM students WHERE studentid=@id";

			using (MySqlConnection Connection = _context.AccessDatabase())
			{

				Connection.Open();
				MySqlCommand Command = Connection.CreateCommand();

				Command.CommandText = query;
				Command.Parameters.AddWithValue("@id", StudentId);
				return Command.ExecuteNonQuery();
			}
			return 0;
		}


		/// <summary>
		/// Updates a student in the database
		/// </summary>
		/// /// <parameter>Student Object</parameter>
		/// <example>
		/// POST: api/Student/AddStudent/{id}
		/// Header: Content-type : application/json 
		/// Request Body (From Student object created through page controller):
		/// {
		///			"StudentFName":"Jerad",
		///			"StudentLName":"Beauregard",
		///			"StudentNumber":"n3231",
		///			"EnrolDate":"2018-06-18"
		///	} -> 
		///			"StudentFName":"Jerad",
		///			"StudentLName":"Beauregard",
		///			"StudentNumber":"n3231",
		///			"EnrolDate":"2018-06-18"
		/// </example>
		/// <returns>
		/// the updated student object
		/// </returns>
		/// 

		[HttpPut(template: "UpdateStudent/{id}")]

		public int UpdateStudent(int id, Student UpdatedStudent)
		{
			string query = "UPDATE students SET studentfname = @fname, studentlname = @lname, studentnumber = @studentnum, enroldate = @enroll WHERE studentid = @id";

			using (MySqlConnection Connection = _context.AccessDatabase())
			{

				Connection.Open();

				MySqlCommand Command = Connection.CreateCommand();
				Command.CommandText = query;
				Command.Parameters.AddWithValue("@fname", UpdatedStudent.StudentFName);
				Command.Parameters.AddWithValue("@lname", UpdatedStudent.StudentLName);
				Command.Parameters.AddWithValue("@studentnum", UpdatedStudent.StudentNumber);
				Command.Parameters.AddWithValue("@enroll", UpdatedStudent.EnrolDate);
				Command.Parameters.AddWithValue("@id", id);

				Command.ExecuteNonQuery();
				return id;
			}

		}
	}
}

