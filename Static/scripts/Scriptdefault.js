﻿
$(document).ready(function () {
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            var str = $("#contentBody_tbRecherche").val();
            window.location = "../Pages/Accueil.aspx?Page=1&Filtre=" + str;
            return false;
        }
    });
});