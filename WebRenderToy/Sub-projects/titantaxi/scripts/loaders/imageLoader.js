IMGS = new Array();

function imageLoader() {
    this.start = function (end) {
        var xmldoc;
        var doc = "data/imageLink.xml"
        var im = importXML(doc);
        xmldoc = im.xmlDoc;
        var elements = xmldoc.getElementsByTagName('Image');
        var loaded = 0;
        for (var i = 0; i < elements.length; i++) {
            IMGS[elements[i].getAttribute('id')] = new Image();
            IMGS[elements[i].getAttribute('id')].src = elements[i].getAttribute('url');
            IMGS[elements[i].getAttribute('id')].onload = function () {
                loaded++;
                if (loaded == elements.length) {
                    end();
                }
            };
        }
    }
}