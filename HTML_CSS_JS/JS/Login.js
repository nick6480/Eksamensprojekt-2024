document.addEventListener("DOMContentLoaded", function() {
    var form = document.getElementById('loginForm');
    form.addEventListener('submit', function(event) {
        event.preventDefault(); // Prevent the form from submitting normally

        // Get values from form fields
        var email = document.getElementById('email').value;
        var password = document.getElementById('password').value;
        var role = document.getElementById('role').value; // Get selected role

        // Extract name from email
        var name = extractNameFromEmail(email);

        // Perform client-side validation
        if (!validatePassword(password)) {
            document.getElementById('passwordError').style.display = 'block';
            return;
        } else {
            document.getElementById('passwordError').style.display = 'none';
        }

        // Redirect based on user role
        if (role === 'student') {
            redirectToStudentPortal(name);
        } else if (role === 'teacher') {
            redirectToTeacherPortal(name);
        } else {
            alert('Invalid role selection.'); // Handle invalid role scenario
        }
    });
});

// Function to extract name from email
function extractNameFromEmail(email) {
    // Assuming the email format is "name@example.com"
    var name = email.substring(0, email.indexOf('@'));
    // Capitalize the first letter of the name
    name = name.charAt(0).toUpperCase() + name.slice(1);
    return name;
}

// Function to redirect to the student portal page
function redirectToStudentPortal(name) {
    localStorage.setItem('studentName', name); // Store name in local storage
    window.location.href = 'Studerende.html';
}

// Function to redirect to the teacher portal page
function redirectToTeacherPortal(name) {
    localStorage.setItem('teacherName', name); // Store name in local storage
    window.location.href = 'Underviser.html';
}

// Function to validate password
function validatePassword(password) {
    var passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).{8,}$/;
    return passwordRegex.test(password);
}
