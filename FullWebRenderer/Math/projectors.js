Projection = matrix4.createPerspectivePOV(Math.toRadians(45), 1, 0.1, 1000000.0);

function projects(xy, z, xyOffset, zOffset, distance) {
    return ((distance * xy) / (z - zOffset)) + xyOffset;
}

function projectv(v) {
    var x = projects(v.x, v.z, Width / 2.0, ZOFFSET, DISTANCE);
    var y = projects(v.y, v.z, Height / 2.0, ZOFFSET, DISTANCE);
    return new vector2(x, y);
}

function projectm(v) {
    var vec = vector3.transformProjection(v, Projection);
    var res = new vector2(vec.x / vec.w, vec.y / vec.w);
    return res;
}

function isBad(n) {
    return (n == Number.NaN || n == Number.POSITIVE_INFINITY || n == Number.NEGATIVE_INFINITY);
}

function project(v) {
//    var x = v.x, y = v.y, z = v.z;
//    var vv = new vector2(((Ez * (x - Ex)) / (Ez + z)) / x, ((Ez * (y - Ey)) / (Ez + z)) / y);
//    if (isBad(vv.x)) {
//        vv.x = 0;
//    }
//    if (isBad(vv.y)) {
//        vv.y = 0;
//    }
//    return vv;
    return projectm(v);
}

function pointsProjector() {
    this.points = new list();
    this.draw = function (m) {
        var v;
        for (var i = 0; i < this.points.length(); i++) {
            v = vector3.transform(this.points.get(i).position, m);
            Canvas.drawPoint(projectv(vector3.transform(v, Camera)), this.points.get(i).color);
        }
    }
}

function linesProjector() {
    this.lines = new list();
    this.draw = function (m) {
        var v1, v2;
        for (var i = 0; i < this.lines.length(); i++) {
            v1 = vector3.transform(this.lines.get(i).pointA, m);
            v2 = vector3.transform(this.lines.get(i).pointB, m);
            Canvas.drawLine(projectv(vector3.transform(v1, Camera)), projectv(vector3.transform(v2, Camera)), this.lines.get(i).color);
        }
    }
}
