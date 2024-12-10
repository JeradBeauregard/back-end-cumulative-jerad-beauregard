// COURSE OBJECT
// MATCHES TO COLUMNS OF COURSE DATA IN COURSE TABLE OF SCHOOL DB

namespace cumulative_Jerad_Beauregard.Models
{

	/// <summary>
	/// Represents Course entity
	/// 
	/// Matches Course data from courses table in school database
	/// 
	/// CourseId == the id and primary key of the course data
	/// CourseCode == the course's code-- for searchability
	/// TeahcerId == the id and primary key od the teacher of the course. This is a foreign key to the teachers table
	/// StartDate == the date the course starts
	/// FinishDate == the date the course ends
	/// CourseName == the name of the course
	/// </summary>
	public class Course
	{

		public int CourseId { get; set; }

		public string CourseCode { get; set; }

		public int TeacherId { get; set; }

		public DateOnly StartDate { get; set; }

		public DateOnly FinishDate { get; set; }

		public string CourseName { get; set; }

	}
}
