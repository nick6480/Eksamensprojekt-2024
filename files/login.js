document.addEventListener("DOMContentLoaded", function () {
    var form = document.getElementById('loginForm');
    form.addEventListener('submit', function (event) {
        event.preventDefault(); // Forhindrer formularen i at blive sendt normalt

        // Hent værdier fra formularfelterne
        var email = document.getElementById('email').value;
        var password = document.getElementById('password').value;
        var role = document.getElementById('role').value;

        // Lav et objekt med brugeroplysningerne
        var userData = {
            email: email,
            password: password,
            role: role
        };

        // Send dataene til serveren ved hjælp af en POST-anmodning
        fetch('http://localhost:8000/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userData)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.text(); // Hent svarteksten
            })
            .then(data => {
                // Håndter serverens svar her, f.eks. omdirigere brugeren baseret på svaret
                if (role === 'student') {
                    redirectToStudentPortal();
                } else if (role === 'teacher') {
                    redirectToTeacherPortal();
                } else {
                    alert('Invalid role selection.');
                }
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
                // Håndter eventuelle fejl, f.eks. visning af en fejlbesked til brugeren
            });
    });
});

