ClassicEditor
    .create(document.querySelector('textarea#content'), {
        language: 'fa',
        height: '800px'
    })
    .catch(error => {
        console.error(error);
    });