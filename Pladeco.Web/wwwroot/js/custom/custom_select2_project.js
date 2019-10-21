/* ------------------------------------------------------------------------------

* ---------------------------------------------------------------------------- */

document.addEventListener('DOMContentLoaded', function () {


    $(document).ready(function () {
        $("#TypologyID").change(function () {
            $("#StageID").empty();
            $.ajax({
                type: 'POST',
                url: 'GetStagesAsync',
                dataType: 'json',
                data: { typologyID: $("#TypologyID").val() },
                success: function (stages) {
                    debugger;
                    $("#StageID").append('<option value=""></option>');
                    $.each(stages, function (i, stage) {
                        $("#StageID").append('<option value="'
                            + stage.id + '">'
                            + stage.name + '</option>');
                    });
                },
                error: function (ex) {
                    alert('Failed to retrieve stages.' + ex);
                }
            });
            return false;
        })
    });
});
