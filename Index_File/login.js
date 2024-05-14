document.addEventListener("DOMContentLoaded", function() {
    // Retrieve teacher name from local storage
    var teacherName = localStorage.getItem('teacherName');

    // Update the teacher name in the header if it exists
    if (teacherName) {
        document.getElementById("teacherName").textContent = teacherName;
    } else {
        // Handle the case when the teacher name is not found
        console.error("Teacher name not found in local storage.");
    }
});

// Function for close application
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
        studentContent.innerHTML = "List of enrolled students goes here."; // Add the content here
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
        courseDetailContent.innerHTML = "Course details go here."; // Add course details here
    } else {
        courseDetailContent.style.display = "none";
        courseDetailContent.innerHTML = "";
    }
}

// Function for Personal info
function showPersonalInfo() {
    hideAllContentSections(); // Hide all content sections first
    var roomContent = document.getElementById("roomContent");
    if (roomContent.style.display === "none") {
        roomContent.style.display = "block";
        roomContent.style.textAlign = "right";
        roomContent.innerHTML = "Room information goes here."; // Add room information here
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
