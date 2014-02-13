Loaded = false;
Max = 0;
var current = 0;
Ez = 800;
Ex = 400;
Ey = 400;
DISTANCE = 1000;
ZOFFSET = 100;

function log(d) {
    console.log(d);
}

function look(p, t) {
    var f = t.sub(p);
    var si = f.cross(new vector3(0, 1, 0));
    var u = f.cross(si);
    return matrix4.createLookAt(p, t, u);
}

Math.toRadians = function (degrees) {
    return degrees * (Math.PI / 180.0);
}

Math.Load = function (success, c) {
    function load() {
        current++;
        if (current == Max) {
            MainCanvas = c.getContext('2d');
            Height = c.height;
            Width = c.width;
            //Camera = matrix4.translate(new vector3(0, 0, 0));
            Camera = matrix4.scale(new vector3(5, 5, 5)).mult(matrix4.translate(new vector3(2, 2, 0)));
            //            Camera = look(new vector3(5, 5, 5), new vector3(0, 0, 0));
            success();
        }
    }
    function loadScript(s) {
        Max++;
        $.getScript(s, function (d, t, j) {
            load();
        });
    }

    //    function loadScript(s, f) {
    //        Max++;
    //        $.getScript(s, function (d, t, j) {
    //            load();
    //            f();
    //        });
    //    }
    loadScript("../Math/vector2.js"); //
    loadScript("../Math/vector3.js"); //
    loadScript("../Math/matrix4.js"); //
    loadScript("../Math/color.js"); //
    loadScript("../Math/vertex.js"); //
    loadScript("../Math/line.js"); //
    loadScript("../Math/list.js"); //
    loadScript("../Math/world.js"); //
    loadScript("../Math/projectors.js");
    loadScript("../Math/objects.js"); //
}