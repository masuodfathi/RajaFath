
    // JavaScript to handle image selection
    document.addEventListener("DOMContentLoaded", function () {
        const imageRadios = document.querySelectorAll(".image-radio");
    const imageTiles = document.querySelectorAll(".image-tile");
    const selectedImageIdInput = document.getElementById("selected-image-id");

        imageTiles.forEach((tile, index) => {
            const imageElement = tile.querySelector("img");
            imageElement.addEventListener("click", (event) => {

                event.stopPropagation();
                
                // Set the corresponding radio button as checked
                imageRadios[index].checked = true;

                // Get the selected image's ID from the data-image-id attribute
                const selectedImageId = tile.getAttribute("data-image-id");
                selectedImageIdInput.value = selectedImageId;

                // Unselect all images
                imageTiles.forEach((t) => {
                    t.classList.remove("selected");
                });

                // Select the clicked image
                tile.classList.add("selected");
            });
        });
    });
