
function search() {
    var str = $("#tbNavSearch").val();
    console.log(str);
    window.location = "Pages/Accueil.aspx?Page=1&Filtre=" + str;
    return false;
}
