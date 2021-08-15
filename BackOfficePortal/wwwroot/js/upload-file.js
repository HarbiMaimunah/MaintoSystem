function UploadFile() {
    debugger
    var fileUpload = $("#File").get(0);
    var files = fileUpload.files;
    var fileData = new FormData();

    for (var i = 0; i < files.length; i++) {
        fileData.append("files", files[i]);
    }


    

    $.ajax({
        url: 'https://localhost:44370/Home/PostFile',
        type: 'POST',
        data: fileData,
        processData: false,
        contentType: false,
        cache: false,
        success: function (data) {
            alert("File Successfuly uploaded");
        }
    });

}