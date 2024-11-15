// TEACHER OBJECT
// MATCHES TO COLUMNS OF TEACHER DATA IN TEACHER TABLE OF SCHOOL DB


namespace cumulative_Jerad_Beauregard.Models
{
	public class Teacher
	{

		public int TeacherId { get; set; }

		public string? TeacherFName { get; set; }

		public string? TeacherLName { get; set; }

		public string? EmployeeNumber { get; set; }

		public DateTime HireDate { get; set; }

		public decimal? Salary {  get; set; }	
	}
}
