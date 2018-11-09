var i = 1;
function CreateTxt() {
    var container = document.getElementById("moreTextboxs");
    var html = document.getElementById('moreTextboxs').innerHTML;
    if (i <= 8) 
        html = html + "<input id='film" + ++i + "' type='text' class='form-control' placeholder='Nom du film'>";
    else
        html = html + "<div class='alert alert - danger' role='alert'>"+
                            "Vous pouvez uniquement ajouter 10 DVDs à la foix!"+
                      "</div > ";
    container.innerHTML = html;
    return false;
}