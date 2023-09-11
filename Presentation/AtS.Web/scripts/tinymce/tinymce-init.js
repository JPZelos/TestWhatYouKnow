tinymce.init({
    selector: "textarea",
    entity_encoding: "raw",
    plugins: 'lists image link table code codesample',
    menubar: 'edit view insert format tools table help',
    toolbar: "undo redo removeformat | bold italic underline strikethrough | align numlist bullist | lineheight outdent indent | link image table | code codesample",
    link_list: [
        { title: 'Το σπουδαίο pdf', value: '/content/images/323472711.pdf' },
        { title: 'My page 2', value: 'http://www.moxiecode.com' },

    ],
    image_list: [
        { title: 'My page 1', value: 'https://www.tiny.cloud' },
        { title: 'My page 2', value: 'http://www.moxiecode.com' },
        { title: 'My pdf 1', value: 'http://www.moxiecode.com/kati.pdf' },
        { title: 'My pdf 2', value: 'http://www.moxiecode.com/katiallo.pdf' }
    ],

});