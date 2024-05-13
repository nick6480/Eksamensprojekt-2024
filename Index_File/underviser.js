
document.addEventListener("DOMContentLoaded", function() {
    // Retrieve student name from localStorage
    var studentName = localStorage.getItem('studentName');
    // Update the student name in the header
    document.getElementById("studentName").textContent = studentName;
});
// Function for close applikation
function closeApp() {
    window.close();
}

// Function for enrolled students
function enrolledStudents() {
    hideAllContentSections(); // Hide all content sections first
    var studentContent = document.getElementById("Studentcontent");
    if (studentContent.style.display === "none") {
        studentContent.style.display = "block";
        studentContent.style.textAlign = "right";
        studentContent.innerHTML = "List of enrolled students goes here."; // Legg til innholdet her
    } else {
        studentContent.style.display = "none";
        studentContent.innerHTML = "";
    }
}

// Function for courseDetail
function courseDetail() {
    hideAllContentSections(); // Hide all content sections first
    var courseDetailContent = document.getElementById("courseDetailContent");
    if (courseDetailContent.style.display === "none") {
        courseDetailContent.style.display = "block";
        courseDetailContent.style.textAlign = "right";
        courseDetailContent.innerHTML = "Course details go here."; // Legg til kursdetaljer her
    } else {
        courseDetailContent.style.display = "none";
        courseDetailContent.innerHTML = "";
    }
}

// Function for Person info
function showPersonalInfo() {
    hideAllContentSections(); // Hide all content sections first
    var roomContent = document.getElementById("roomContent");
    if (roomContent.style.display === "none") {
        roomContent.style.display = "block";
        roomContent.style.textAlign = "right";
        roomContent.innerHTML = "Room information goes here."; // Legg til rominformasjon her
    } else {
        roomContent.style.display = "none";
        roomContent.innerHTML = "";
    }
}

function hideAllContentSections() {
    // Hide all content sections
    document.getElementById("roomContent").style.display = "none";
    document.getElementById("courseDetailContent").style.display = "none";
    document.getElementById("Studentcontent").style.display = "none";
}

// Function to check personal information
function checkPersonalInfo() {
    // You can implement the logic to retrieve and display personal information here
    var instructorInfo = {
        name: "John Doe",
        email: "john.doe@example.com",
        role: "Instructor"
    };

    // Display personal information in an alert (you can customize this according to your UI)
    var infoString = "Name: " + instructorInfo.name + "\nEmail: " + instructorInfo.email + "\nRole: " + instructorInfo.role;
    alert("Personal Information:\n" + infoString);
}
