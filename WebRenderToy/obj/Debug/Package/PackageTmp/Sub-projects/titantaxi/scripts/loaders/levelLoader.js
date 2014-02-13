LVLS = new Array();

function levelLoader() {
    this.start = function (end) {
        var xmldoc;
        doc = "data/levelLink.xml";
        var im = importXML(doc);
        xmldoc = im.xmlDoc;
        var cout = -1;
        var elements = xmldoc.getElementsByTagName('level');
        for (var i = 0; i < elements.length; i++) {
            cout++;
            LVLS.push(new Array());
            doc = elements[i].getAttribute('url');
            im = importXML(doc);
            xmldoc = im.xmlDoc;
            LVLS[cout]["collisionPolygons"] = getPoly(xmldoc);
            LVLS[cout]["asteroids"] = getAst(xmldoc);
            LVLS[cout]["shipCenter"] = getSC(xmldoc);
        }
        end();
    }
}

function getPoly(x) {
    var r = new Array();
    var ele = x.getElementsByTagName('polygon');
    for (var i = 0; i < ele.length; i++) {
        var tx = 0.0, ty = 0.0, tv = new Array();
        var te = ele[i].getElementsByTagName('vertex');
        for (var j = 0; j < te.length; j++) {
            tv.push(new vector2(parseFloat(te[i].getAttribute('x')), parseFloat(te[i].getAttribute('y'))));
            tx += parseFloat(te[i].getAttribute('x'));
            ty += parseFloat(te[i].getAttribute('y'));
        }
        tx /= parseFloat(te.length);
        ty /= parseFloat(te.length);
        r.push(new tpoly(tx, ty, tv));
    }
    return r;
}

function getAst() {
}

function getSC(x) {
    var ele = x.getElementsByTagName('starting')[0];
    return new vector2(parseFloat(ele.getAttribute('x')), parseFloat(ele.getAttribute('y')));
}

function tpoly(a, b, v) {
    this.x = a;
    this.y = b;
    this.vertices = v;
}