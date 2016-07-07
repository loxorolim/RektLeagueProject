(function () {
    $("#my-file-selector").change(function () {
        $("#input-filename").val(this.files[0].name);
        readURL(this);
    });

    function readURL(input) {

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $("#imagePreview").attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
})();

