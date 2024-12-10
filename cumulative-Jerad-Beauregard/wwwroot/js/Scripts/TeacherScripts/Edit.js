window.onload = function () {

    // form variables from document

    var formhandle = document.forms.updateteacher

 



    formhandle.addEventListener("submit", function (event) {

       event.preventDefault();

        var id = formhandle.id.value;
        var firstName = formhandle.firstname.value;
        var lastName = formhandle.lastname.value;
        var employeeNum = formhandle.employeenumber.value;
        var hireDate = formhandle.hiredate.value;
        var salary = formhandle.salary.value;
        var phoneNumber = formhandle.phonenumber.value;

        var teacherData = {
            TeacherId: id,
            TeacherFName : firstName,
            TeacherLName : lastName,
            EmployeeNumber : employeeNum,
            HireDate : hireDate,
            Salary: salary,
            PhoneNumber: phoneNumber
        }

        var nameError = document.getElementById("nameError");
        var errorCount = 0;

        if (firstName === "") {

            nameError.style.display = "inline";
            errorCount++
        }


        // send using ajax

        if (errorCount === 0) {
            fetch('/TeacherPage/UpdateAjax', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(teacherData)
            })
                .then(function (response) {
                    if (response.ok) {
                        return response.json();
                    }
                    else {
                        formhandle.firstname.style.background = "red";
                    }

                })
                .then(function (result) {
                    console.log('success', result);
                    window.location.href = `/TeacherPage/Show/${id}`;
                })


        }

        
      
    })

    







}