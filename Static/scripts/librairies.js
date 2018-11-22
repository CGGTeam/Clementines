function changeIcon(id) {
    console.log(id)

    var span = document.getElementById(id);
    if (span.classList.contains("glyphicon-chevron-down")) {
        document.getElementById("MyElement").classList.add('glyphicon-chevron-up');
        document.getElementById("MyElement").classList.remove('glyphicon-chevron-down');
    }
    else {
        document.getElementById("MyElement").classList.add('glyphicon-chevron-down');
        document.getElementById("MyElement").classList.remove('glyphicon-chevron-up');
    }
}