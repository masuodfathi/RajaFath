
    $(document).ready(function () {
        // Initialize arrays to store selected and available tags
        var selectedTagsId = [];
    var availableTagsId = [];

    // Function to update the available tags dropdown
    function updateAvailableTags() {
        $('#AvailableTags').empty();
    $.each(availableTags, function (i, tag) {
        $('#AvailableTags').append($('<option>', {
            value: tag,
            text: tag
        }));
            });
        }

    // Function to update the selected tags dropdown
    function updateSelectedTags() {
        $('#SelectedTagIds').empty();
    $.each(selectedTagsId, function (i, tag) {
        $('#SelectedTagIds').append($('<option>', {
            value: tag,
            text: tag
        }));
            });
        }

    // Handle tag input and selection
    $('#AddTagButton').on('click', function () {
            var newTag = $('#NewTag').val().trim();
    if (newTag !== '' && selectedTagsId.indexOf(newTag) === -1) {
        // Add the new tag to the selected tags
        selectedTagsId.push(newTag);
    updateSelectedTags();

    // Add the new tag to the available tags (for database insertion)
    availableTagsId.push(newTag);
    updateAvailableTags();

    // Clear the new tag input
    $('#NewTag').val('');
            }
        });

    // Initially, populate available tags (you can retrieve this from your ViewModel)
    availableTags = @Html.Raw(Json.Serialize(Model.AvailableTags));
    updateAvailableTags();

    // Populate selected tags (you can retrieve this from your ViewModel)
    selectedTagsId = @Html.Raw(Json.Serialize(Model.SelectedCategoryIds));
    updateSelectedTags();
    });
