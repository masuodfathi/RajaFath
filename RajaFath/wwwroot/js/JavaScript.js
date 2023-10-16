// Get the form and submit button elements
const myForm = document.getElementById("createpost-form");
const submitButton = document.getElementById("CreatePost");

// Define a flag to track if the form is currently being submitted
let isSubmitting = false;

// Add a submit event listener to the form
myForm.addEventListener("submit", function (event) {
    // If the form is already being submitted, prevent further submissions
    if (isSubmitting) {
        event.preventDefault(); // Prevent the default form submission
    } else {
        // Set the flag to indicate that the form is being submitted
        isSubmitting = true;

        // Disable the submit button to prevent multiple clicks
        submitButton.disabled = true;
        // You can also change its text or style if you want
        // submitButton.textContent = "Submitting...";
        // submitButton.style.backgroundColor = "gray";

        // Here, you can perform any necessary form submission actions
        // For example, you can use AJAX to submit the form data asynchronously.
    }
});