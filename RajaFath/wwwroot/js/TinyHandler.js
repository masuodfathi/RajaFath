tinymce.init({
    selector: 'textarea#Content',
    menubar: false,
    directionality: 'rtl',
    height: '70vh',
    branding: false,
    language:'fa',
    plugins: 'tinycomments mentions anchor autolink charmap codesample emoticons image link lists media searchreplace table visualblocks wordcount checklist mediaembed casechange export formatpainter pageembed permanentpen footnotes advtemplate advtable advcode editimage tableofcontents mergetags powerpaste typography inlinecss directionality',
    toolbar: 'code|undo redo | blocks fontfamily fontsize | ltr rtl | bold italic underline strikethrough | link image media table mergetags | align lineheight | tinycomments | checklist numlist bullist indent outdent | emoticons charmap | removeformat',
    tinycomments_mode: 'embedded',
    tinycomments_author: 'Author',
    ai_request: (request, respondWith) => respondWith.string(() => Promise.reject("See docs to implement AI Assistant"))
});