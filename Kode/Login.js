document.addEventListener("DOMContentLoaded", function() {
    var form = document.getElementById('loginForm');
    form.addEventListener('submit', function(event) {
        event.preventDefault(); // Prevent the form from submitting normally

        // Get values from form fields
        var email = document.getElementById('email').value;
        var password = document.getElementById('password').value;
        var role = document.getElementById('role').value; // Get selected role

        // Perform client-side validation
        if (!validatePassword(password)) {
            document.getElementById('passwordError').style.display = 'block';
            return;
        } else {
            document.getElementById('passwordError').style.display = 'none';
        }

        // Check the role associated with the email
        var storedRole = checkUserRole(email);

        // If the stored role is not compatible with the selected role in the form, deny access
        if (storedRole !== role) {
            alert('You are not authorized to access this role.');
            return;
        }

        // Redirect based on user role
        if (role === 'student') {
            redirectToStudentPortal(email);
        } else if (role === 'teacher') {
            redirectToTeacherPortal(email);
        } else {
            alert('Invalid role selection.'); // Handle invalid role scenario
        }
    });
});

// Function to redirect to the student portal page
function redirectToStudentPortal(studentEmail) {
    localStorage.setItem('studentEmail', studentEmail);
    window.location.href = 'Studerende.html';
}

// Function to redirect to the teacher portal page
function redirectToTeacherPortal(teacherEmail) {
    localStorage.setItem('teacherEmail', teacherEmail);
    window.location.href = 'Underviser.html';
}

// Function to check the role associated with the email
function checkUserRole(email) {
    // This function would typically involve a server-side call to your database
    // Here, I'll just use a simple example with hardcoded values
    var studentEmails = ["student1@example.com", "student2@example.com"]; // Example list of student emails
    var teacherEmails = ["teacher1@example.com", "teacher2@example.com"]; // Example list of teacher emails

    if (studentEmails.includes(email)) {
        return 'student';
    } else if (teacherEmails.includes(email)) {
        return 'teacher';
    } else {
        return 'unknown'; // Or handle the case where the email is not found
    }
}

// Function to validate password
function validatePassword(password) {
    var passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).{8,}$/;
    return passwordRegex.test(password);
}
