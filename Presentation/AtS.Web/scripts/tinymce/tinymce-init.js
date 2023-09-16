tinymce.init({
    selector: "textarea",
    entity_encoding: "raw",
    language: 'el',
    plugins: 'lists image link table code codesample visualblocks quickbars powerpaste',

    menubar: 'edit view insert format tools table help',
    toolbar: "undo redo removeformat | blocks | bold italic underline strikethrough | align numlist bullist | lineheight outdent indent | link image table | code codesample visualblocks",
    default_link_target: '_blank',

    powerpaste_allow_local_images: false,
    powerpaste_word_import: 'prompt',
    powerpaste_html_import: 'prompt',

    relative_urls: false,
    link_list: [
        { title: tinyMceFiles[0], value: '../content/files/geoponia.pdf' },
        { title: tinyMceFiles[1], value: '../content/files/kalliergia-ampelou.pdf' },
        { title: tinyMceFiles[2], value: '../content/files/epitrapezies-poikilies-ampelou.pdf' }

    ],
    image_list: [
        { title: 'My page 1', value: 'https://www.tiny.cloud' },
        { title: 'My page 2', value: 'http://www.moxiecode.com' },
        { title: 'My pdf 1', value: 'http://www.moxiecode.com/kati.pdf' },
        { title: 'My pdf 2', value: 'http://www.moxiecode.com/katiallo.pdf' }
    ],
    quickbars_insert_toolbar: false,
    quickbars_selection_toolbar: 'bold italic | quicklink h2 h3 blockquote'
    
});