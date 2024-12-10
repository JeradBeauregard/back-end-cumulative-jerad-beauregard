// STUDENT OBJECT
// MATCHES TO COLUMNS OF STUDENT DATA IN STUDENT TABLE OF SCHOOL DB



namespace cumulative_Jerad_Beauregard.Models
{

	/// <summary>
	/// Represents Student entity
	/// 
	/// Matches student data from students table in school database
	/// 
	/// StudentId == the id and primary key of the student data
	/// StudentFName == The student's first name
	/// StudentLName == The student's Last name
	/// StudentNumber == The Students student number
	/// EnrolDate == The date the student enrolled
	/// </summary>
	public class Student
	{
		public int StudentId { get; set; }

		public string StudentFName { get; set; }

		public string StudentLName { get; set; }

		public string StudentNumber { get; set; }

		public DateOnly EnrolDate { get; set; }
	}
}
