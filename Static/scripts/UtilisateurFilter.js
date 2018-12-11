$(document).ready(function () {
    $("#filter").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#contentBody_table_Utilisateur tr").not('tr th').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
});