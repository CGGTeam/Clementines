$(document).ready(function () {
    $("#filter").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#contentBody_table tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
});

function UserDeleteConfirmation() {
    return confirm("Êtes vous sûr de vouloir supprimer cet utilisateur?");
}