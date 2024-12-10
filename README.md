
### **1. Project Title**
Jerad Beauregard Back-End Cumulative

---

### **2. Description**
This program allows the user to access information from a school database via a web interface.

The script file for the edit view of the TeacherPage folder can be found at:
cumulative-Jerad-Beauregard/wwwroot/js/Scripts/TeacherScripts/Edit.js

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
- User can Add a teacher through web interface at -> http:/localhost:5016/TeacherPage/New
- Note: Hire date must be formatted like this: 2014-06-10 00:00:00
- User can Delete a Teacher through web interface at -> http:/localhost:5016/TeacherPage/DeleteConfirm/{id}
- User can Add a student through web interface at -> http:/localhost:5016/StudentPage/New
- Note: Enroll Date must be formatted like this: 2018-06-18
- User can Delete a student through web interface at -> http:/localhost:5016/StudentPage/DeleteConfirm/{id}
- User can Add a course through the web interface at ->http:/localhost:5016/CoursePage/New
- Note Start date and end dates must be formatted like this: 2018-06-18
- User can Delete a course through the web interface at -> http:/localhost:5016/CoursePage/DeleteConfirm/{id}
- User can Update a teacher using Ajax through the web interface at -> http:/localhost:5016/TeacherPage/Edit/{id} 
- Note: Hire date must be formatted like this: 2014-06-10 00:00:00
- User can update a student through the web interface at -> http:/localhost:5016/StudentPage/Edit/{id}
- Note: Enroll Date must be formatted like this: 2018-06-18
- User can update a course through the web interface at ->  http:/localhost:5016/CoursePage/Edit/{id}
- Note Start date and end dates must be formatted like this: 2018-06-18

### **5. Installation**
- Must install and set up school database and insure that the connection data matches your local data (roots etc.)
