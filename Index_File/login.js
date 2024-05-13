document.addEventListener("DOMContentLoaded", function() {
    var form = document.getElementById('loginForm');
    form.addEventListener('submit', function(event) {
        event.preventDefault(); // Prevent the form from submitting normally

        // Get values from form fields
        var password = document.getElementById('password').value;
        var role = document.getElementById('role').value; // Get selected role

        // Perform client-side validation
        if (!validatePassword(password)) {
            document.getElementById('passwordError').style.display = 'block';
            return;
        } else {
            document.getElementById('passwordError').style.display = 'none';
        }

        // Redirect based on user role
        if (role === 'student') {
            redirectToStudentPortal();
        } else if (role === 'teacher') {
            redirectToTeacherPortal();
        } else {
            alert('Invalid role selection.'); // Handle invalid role scenario
        }
    });
});

// Function to redirect to the student portal page
function redirectToStudentPortal() {
    window.location.href = 'Studerende.html';
}

// Function to redirect to the teacher portal page
function redirectToTeacherPortal() {
    window.location.href = 'Underviser.html';
}

// Function to validate password
function validatePassword(password) {
    var passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).{8,}$/;
    return passwordRegex.test(password);
}
