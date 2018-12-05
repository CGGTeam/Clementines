var i = 2;
function CreateTxt() {
    var container = document.getElementById("moreTextboxs");

    $("#contentBody_tbNomFilm_btnEnregistrer").attr('value', 'Enregistrer les films');
    if (i <= 9)
        $("#contentBody_tbNomFilm_film" + i++).show();
    else if (i === 10) {
        $("#contentBody_tbNomFilm_film" + i++).show();

        $("#btnMoreTb").hide();
    }
    
    return false;
}
function search() {
    var str = $("#tbNavSearch").val();
    console.log(str);
    window.location = "Pages/Accueil.aspx?Page=1&Filtre=" + str;
    return false;
}
