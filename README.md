
### **1. Project Title**
Jerad Beauregard Back-End Cumulative

---

### **2. Description**
This program allows the user to access information from a school database via a web interface.

---

### **3. Features**
- User can acesss a list of teachers via -> http://localhost:5016/TeacherPage/Lists
- User can search for a teacher by ID via
- User can find more information on a teacher by clicking one of the links via -> http://localhost:5016/TeacherPage/Lists
- User can see what courses are being taught by the selected teacher at the selected link
- User can search for a list of teachers by hiring range using the form accessed via -> http://localhost:5016/TeacherPage/Lists
- Note: The start date and end dates must be formatted like this to work -> example start date: 2014-06-10 00:00:00 example end date: 2016-08-05 00:00:00
- Note: That the start date must begin before the end date. There is no validity checking at this point.
- User can access a list of students via -> http://localhost:5016/StudentPage/Lists
- User can access more information on Selected students at the selected link
- User can search for a student by ID via -> http://localhost:5016/api/Student/findstudent/{id}
- User can access a list of courses via -> http://localhost:5016/Coursepage/lists
- User can access more information on courses at the selected course link
- User can Search for a course by ID via -> http://localhost:5016/api/Course/findcourse/{id}
- 

### **5. Installation**
- Must install and set up school database and insure that the connection data matches your local data (roots etc.)
