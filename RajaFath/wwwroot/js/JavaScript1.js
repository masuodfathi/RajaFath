
    document.addEventListener("DOMContentLoaded", function () {
        const imageUploadForm = document.getElementById("image-upload-form");
        
        const selectedImage = document.getElementById("selected-image");
        const selectedImageTitle = document.getElementById("selected-image-title");
        const selectedImageDescription = document.getElementById("selected-image-description");

    imageUploadForm.addEventListener("submit", function (e) {
            e.preventDefault();


            // Function to switch to Tab 1
            var switchToTab1 = () => {
                const tab1Link = document.querySelector('[href="#tab1"]');
                tab1Link.click();
            }

    // Function to update Tab 1 content
        function updateTab1(newImageId, imageUrl) {
            // Create a new image element
            const imageLabel = document.createElement("label");
            imageLabel.className = "image-tile selected";
            imageLabel.setAttribute("data-image-id",newImageId);

            const radioInput = document.createElement("input");
            radioInput.type = "radio";
            radioInput.className = "image-radio";
            radioInput.name = "selected-image";
            radioInput.id = `image-radio-${newImageId}`;
            radioInput.value = newImageId;

            const imgElement = document.createElement("img");
            imgElement.src = imageUrl;
            imgElement.alt = "Newly Uploaded Image";
            imgElement.className = "modal-image";

            // Append the new image to the list
            imageLabel.appendChild(radioInput);
            imageLabel.appendChild(imgElement);
            const imageList = document.querySelector(".image-grid");
            imageList.appendChild(imageLabel);

            // Mark the new image as selected
            radioInput.addEventListener("change", function () {
            // Handle image selection here if needed
        });
    }

    // Use FormData to send the image to the server
    const formData = new FormData(this);

    fetch("/Admin/UploadImage", {
        method: "POST",
    body: formData,
            })
                .then((response) => response.json())
                .then((data) => {
                    if (data.success) {

        // Update Tab 1 content with the newly uploaded image
        updateTab1(data.id, data.imageUrl);

    // After updating, switch to Tab 1
    switchToTab1();
    const newImageId = data.id;
    const newImageElement = document.getElementById(`image-radio-${newImageId}`);
    newImageElement.click();
    // Display the uploaded image and its details
    

    // Show the selected image container
    

                        // Consider it as a FeatureImage
                        // You may update the IsFeatureImage property in your database
                    } else {
        // Handle error case
        console.error("Image upload failed.");
                    }
                })
                .catch((error) => {
        console.error("Image upload error:", error);
                });
        });
    });
