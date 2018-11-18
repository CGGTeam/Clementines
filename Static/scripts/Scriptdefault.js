
$(document).ready(function () {
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            filtrer();
        }
    });
    $('#contentBody_tbRecherche').bind('change', function () {

        filtrer();
    });
    $('#contentBody_cbTitre').bind('change', function () {

        filtrer();
    });
});

function filtrer() {
    var str = $("#contentBody_tbRecherche").val();
    var titre = $("#contentBody_cbTitre").is(':checked');
    var personne = $("#contentBody_cbPersonne").is(':checked');

    window.location = "../Pages/Accueil.aspx?Page=1&Filtre=" + str + "&Titre=" + titre + "&Personne=" + personne;
    return false;
}