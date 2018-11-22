function changeIcon(id) {
    console.log(id)

    var span = document.getElementById(id);
    if (span.classList.contains("glyphicon-chevron-down")) {
        span.classList.add('glyphicon-chevron-up');
        span.classList.remove('glyphicon-chevron-down');
    }
    else {
        span.classList.add('glyphicon-chevron-down');
        span.classList.remove('glyphicon-chevron-up');
    }
}