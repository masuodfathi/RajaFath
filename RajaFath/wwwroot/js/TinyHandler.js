tinymce.init({
    selector: 'textarea#Content',
    menubar: false,
    directionality: 'rtl',
    height: '70vh',
    branding: false,
    language:'fa',
    plugins: 'anchor autolink image link lists media searchreplace table visualblocks wordcount directionality code',
    toolbar: 'code | undo redo | customImagePicker blocks fontfamily fontsize | ltr rtl | bold italic underline strikethrough | link table  | align lineheight | tinycomments | checklist numlist bullist indent outdent',
    relative_urls: false,
    images_upload_base_path: '/images',
    setup: function (editor) {
        editor.ui.registry.addButton('customImagePicker', {
            text: 'Insert Image',
            onAction: function () {
                // Open the image selection/upload modal here
                // You can use JavaScript/jQuery to trigger the modal's display
                $('#which-target').val("Tiny");
                $('#myModal').modal('show');
            }
        });
    },
    tinycomments_mode: 'embedded',
    tinycomments_author: 'Author',
    ai_request: (request, respondWith) => respondWith.string(() => Promise.reject("See docs to implement AI Assistant"))
});