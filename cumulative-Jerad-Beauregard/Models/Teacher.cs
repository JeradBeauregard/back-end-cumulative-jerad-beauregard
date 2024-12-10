// TEACHER OBJECT
// MATCHES TO COLUMNS OF TEACHER DATA IN TEACHER TABLE OF SCHOOL DB


namespace cumulative_Jerad_Beauregard.Models
{

	/// <summary>
	/// Represents Teacher entity
	/// 
	/// Matches teacher data from teacher table in school database
	/// 
	/// TeacherId == The id and primary key of the teacher data
	/// TeacherFName == The teacher's first name
	/// TeacherLName == The teacher's last name
	/// EmployeeNumber == The teacher's employee number.
	/// HireDate == The date the teacher was hired
	/// Salary == The salary of the teacher as a decimal
	/// PhoneNumber == The phone number of the teacher
	/// </summary>
	public class Teacher
	{

		public int TeacherId { get; set; }

		public string? TeacherFName { get; set; }

		public string? TeacherLName { get; set; }

		public string? EmployeeNumber { get; set; }

		public DateTime HireDate { get; set; }

		public decimal? Salary {  get; set; }	

		public string? PhoneNumber { get; set; }
	}
}
