// STUDENT OBJECT
// MATCHES TO COLUMNS OF STUDENT DATA IN STUDENT TABLE OF SCHOOL DB

namespace cumulative_Jerad_Beauregard.Models
{
	public class Student
	{
		public int StudentId { get; set; }

		public string StudentFName { get; set; }

		public string StudentLName { get; set; }

		public string StudentNumber { get; set; }

		public DateOnly EnrolDate { get; set; }
	}
}
