SNDS = new Array();

function soundLoader() {
    this.start = function (end) {
        var xmldoc;
        var doc = "data/soundLink.xml"
        var im = importXML(doc);
        xmldoc = im.xmlDoc;
        var elements = xmldoc.getElementsByTagName('Sound');
        var loaded = 0;
    }
}