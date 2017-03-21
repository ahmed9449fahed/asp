var myimage = document.getElementById("myfotos");


var imagearray = ["http://www.workinentertainment.com/blog/wp-content/uploads/2014/12/job-seacrhing.jpg",
                    "http://www.lonestarpropertysolutions.com/wp-content/uploads/2015/07/jobs2.jpg",
                    "http://www.iworkministry.org/wp-content/uploads/2014/05/Jobs-Logo.png",
                    "http://imaginafrica.net/wp-content/uploads/2015/02/jobnig.jpg"];
var imageindex = 0;

function changeimage() {

    //setAlpha(imagearray[imageindex],800);
    document.body.style.backgroundImage = 'url(' + imagearray[imageindex] + ')';

    //myimage.setAttribute("style.backgroundImage", imagearray[imageindex]);
    imageindex++;
    if (imageindex >= imagearray.length) {
        imageindex = 0;
    }

}


var intervalhandle = setInterval(changeimage, 2000);
myimage.onclick = function () {
    clearInterval(intervalhandle);
}

function setAlpha(element, value) {
    // Browserabhaengige Transparenz fuer Ueberblendung
    var opacity = value / 100;
    if (document.body.style.filter !== null) { element.style.filter = "Alpha(opacity=" + value + ")"; }  // IE
    if (document.body.style.opacity !== null) { element.style.opacity = opacity; }                   // Opera, Chrome, Firefox, Safari
    if (document.body.style.KhtmlOpacity !== null) { element.style.KhtmlOpacity = opacity; }         // alte Safaris
    if (document.body.style.MozOpacity !== null) { element.style.MozOpacity = opacity; }             // alte Firefox
}