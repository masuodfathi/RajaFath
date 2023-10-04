
    // JavaScript to handle image selection
document.addEventListener("DOMContentLoaded", function () {
    
    
    handleImageSelection();
    handleUploadImage();
    handleModalButton();
});

var handleModalButton = () => {
    var cancelButton = document.getElementById("modal-cancel");
    var approveButton = document.getElementById("modal-approve");
    cancelButton.addEventListener("click", () => {
        const selectedImageIdInput = document.getElementById("selected-image-id");
        selectedImageIdInput.value = "";
    });
}

var handleUploadImage = () => {
        const imageUploadForm = document.getElementById("image-upload-form");

        imageUploadForm.addEventListener("submit", function (e) {
            e.preventDefault();
            const formData = new FormData(this);
            fetch("/Admin/UploadImage", {
                method: "POST",
                body: formData,
            })
                .then((response) => response.json())
                .then((data) => {
                    if (data.success) {

                        // After updating, switch to Tab 1
                        switchToTab1();
                        // Update Tab 1 content with the newly uploaded image
                        updateTab1(data.id, data.imageUrl);
                        imageUploadForm.reset();
                        

                        
                    } else {
                        // Handle error case
                        console.error("Image upload failed.");
                    }
                })
                .catch((error) => {
                    console.error("Image upload error:", error);
                });



        });
    }

var switchToTab1 = () => {
    const tab1Link = document.querySelector('[href="#tab1"]');
    tab1Link.click();
}

var updateTab1 = (newImageId, imageUrl) => {
    // Create a new image element
    const imageLabel = document.createElement("label");
    imageLabel.className = "image-tile";
    imageLabel.setAttribute("data-image-id", newImageId);

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
    handleImageSelection();
    imgElement.click();
    setSelectedInput(imageLabel);
    
};
const handleImageSelection = () => {
    const imageRadios = document.querySelectorAll(".image-radio");
    const imageTiles = document.querySelectorAll(".image-tile");
    //const selectedImageIdInput = document.getElementById("selected-image-id");

    const imageDetails = {
        imageTitle: "",
        imageDescription: ""
        // Add more properties as needed
    };

    imageTiles.forEach((tile, index) => {
        const imageElement = tile.querySelector("img");
        const featureimage = document.getElementById("feature-image");
        imageElement.addEventListener("click", (event) => {

            event.stopPropagation();

            // Set the corresponding radio button as checked
            imageRadios[index].checked = true;

            // Get the selected image's ID from the data-image-id attribute
            //const selectedImageId = tile.getAttribute("data-image-id");
            //selectedImageIdInput.value = selectedImageId;
            setSelectedInput(tile);

            // Unselect all images
            imageTiles.forEach((t) => {
                t.classList.remove("selected");
            });

            // Select the clicked image
            tile.classList.add("selected");
            var clickedImageSrc = imageElement.src;
            
            featureimage.src = clickedImageSrc;
        });
    });
}
var setSelectedInput = (tile) => {
    const selectedImageIdInput = document.getElementById("selected-image-id");
    const selectedImageId = tile.getAttribute("data-image-id");
    selectedImageIdInput.value = selectedImageId;
}