document.addEventListener("DOMContentLoaded", function() {
    // Retrieve student name from localStorage
    var studentName = localStorage.getItem('studentName');
    // Update the student name in the header
    document.getElementById("studentName").textContent = studentName;
});

function showCourses() {
    // Hide all content sections
    hideAllContentSections();
    
    // Show courses content
    document.getElementById("coursesContent").innerHTML = "Der er ingen kurser endnu.";
    document.getElementById("coursesContent").style.display = "block";
    document.getElementById("coursesContent").style.textAlign = "right"; // Align content to the right
}

function showLocation() {
    // Hide all content sections
    hideAllContentSections();
    
    // Show location content
    document.getElementById("locationContent").innerHTML = "Der er ingen Lokaler";
    document.getElementById("locationContent").style.display = "block";
    document.getElementById("locationContent").style.textAlign = "right"; // Align content to the right
}

function showPersonalInfo() {
    // Hide all content sections
    hideAllContentSections();
    
    // Show personal info content
    var personalInfoContent = document.getElementById("personalInfoContent");
    personalInfoContent.innerHTML = "Der er ingen person-info";
    personalInfoContent.style.display = "block";
    personalInfoContent.style.position = "fixed";
    personalInfoContent.style.bottom = "20px";
    personalInfoContent.style.left = "20px";
    personalInfoContent.style.textAlign = "left";
}

function hideAllContentSections() {
    // Hide all content sections
    document.getElementById("coursesContent").style.display = "none";
    document.getElementById("locationContent").style.display = "none";
    document.getElementById("personalInfoContent").style.display = "none";
}

function closeApp() {
    window.close();
}
