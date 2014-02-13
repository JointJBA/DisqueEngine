EPSILON = 0.0001;
FLT_MAX = Number.MAX_VALUE - 1;
function vector2(x, y) {
    this.x = x;
    this.y = y;
    this.add = function (v) {
        this.x += v.x;
        this.y += v.y;
        return this;
    }
    this.addn = function (xx, yy) {
        this.x += xx;
        this.y += yy;
        return this;
    }
    this.subt = function (v) {
        this.x -= v.x;
        this.y -= v.y;
        return this;
    }
    this.scale = function (s) {
        this.x *= s;
        this.y *= s;
        return this;
    }
    this.dot = function (v) {
        return this.x * v.x + this.y * v.y;
    }
    this.lengthSquare = function () {
        return Math.pow(this.x, 2) + Math.pow(this.y, 2);
    }
    this.length = function () {
        return Math.sqrt(this.lengthSquare());
    }
    this.normalize = function () {
        var len = this.length();
        if (len > EPSILON) {
            this.x /= len;
            this.y /= len;
        }
        return this;
    }
    this.clone = function () {
        return new vector2(this.x, this.y);
    }
}

function addv(a, b) {
    return new vector2(a.x + b.x, a.y + b.y);
}

function subv(a, b) {
    return new vector2(a.x - b.x, a.y - b.y);
}

function scalev(a, b) {
    return new vector2(a.x * b, a.y * b);
}

function dotv(a, b) {
    return a.x * b.x + a.y * b.y;
}

function crossv(a, v) {
    if (typeof (a) == "number") {
        return new vector2(-a * v.y, a * v.x);
    }
    else if (typeof (v) == "number") {
        return new vector2(-v * a.y, v * a.x);
    }
    return a.x * v.y - a.y * v.x;
}

function distance(a, b) {
    return Math.sqrt(distanceSquare(a, b));
}

function distanceSquare(a, b) {
    return (Math.pow(a.x - b.x, 2) + Math.pow(a.y - b.y, 2));
}

function reflect(vec, norm) {
    var res = vec.clone();
    var f = (vec.x * norm.x) + (vec.y * norm.y);
    res.x -= 2.0 * f * norm.x;
    res.y -= 2.0 * f * norm.y;
    return res;
}

function color(r, g, b) {
    this.r = r;
    this.g = g;
    this.b = b;
    this.getStyle = function () {
        return 'rgb(' + this.r + ',' + this.g + ',' + this.b + ')';
    }
}

function random(min, max) {
    return parseInt(Math.random() * (max - min) + min);
}

function GetLocalMousePosition(elementID, mouseX, mouseY) {
    var offset = $('#' + elementID).offset();
    var x = mouseX - offset.left;
    var y = mouseY - offset.top;
    return new vector2(x, y);
}

function roundRectangle(ctx, x, y, width, height, radius, lw, fill, stroke) {
    if (typeof stroke == "undefined") {
        stroke = true;
    }
    if (typeof radius === "undefined") {
        radius = 5;
    }
    ctx.beginPath();
    ctx.moveTo(x + radius, y);
    ctx.lineTo(x + width - radius, y);
    ctx.quadraticCurveTo(x + width, y, x + width, y + radius);
    ctx.lineTo(x + width, y + height - radius);
    ctx.quadraticCurveTo(x + width, y + height, x + width - radius, y + height);
    ctx.lineTo(x + radius, y + height);
    ctx.quadraticCurveTo(x, y + height, x, y + height - radius);
    ctx.lineTo(x, y + radius);
    ctx.quadraticCurveTo(x, y, x + radius, y);
    ctx.closePath();
    if (stroke) {
        ctx.lineWidth = lw;
        ctx.stroke();
    }
    if (fill) {
        ctx.fill();
    }
}

function textDimensions(dv, txt, fs, ff) {
    dv.style.fontSize = fs + "px";
    dv.style.fontFamily = ff;
    dv.innerHTML = txt;
    return { 'width': (dv.clientWidth + 1), 'height': (dv.clientHeight + 1) }
}

function toRadians(ang) {
    return ang * (Math.PI / 180.0);
}

function toDegrees(rad) {
    return rad * (180.0 / Math.PI);
}

function toVector(ang) {
    var radian = toRadians(ang);
    return new vector2(Math.cos(radian), Math.sin(radian));
}

function toAngle(vec) {
    var ang = toDegrees(Math.atan2(vec.x, vec.y));
    if (ang >= 90 && ang <= 180)
        return ang - 90;
    else if (ang <= 90 && ang >= 0)
        return ang + 180;
    else
        return -ang + 180;
}

function linearGradient(p, w, h, cs) {
    this.point = p;
    this.width = w;
    this.height = h;
    this.colorStops = cs;
    this.getStyle = function (ctx) {
        var grd = ctx.createLinearGradient(this.point.x, this.point.y, this.width, this.height);
        for (var i = 0; i < this.colorStops.length; i++) {
            grd.addColorStop(i, this.colorStops[i].getStyle());
        }
        return grd;
    }
}

function radialGradient(p0, r0, p1, r1, cs) {
    this.point0 = p0;
    this.radius0 = r0;
    this.point1 = p1;
    this.radius1 = r1;
    this.colorStops = cs;
    this.getStyle = function (ctx) {
        var grd = ctx.createRadialGradient(this.point0.x, this.point0.y, this.radius0, this.point1.x, this.point1.y, this.radius1);
        for (var i = 0; i < this.colorStops.length; i++) {
            grd.addColorStop(i, this.colorStops[i].getStyle());
        }
        return grd;
    }
}

function min(a, b) {
    return a < b ? a : b;
}

function minv(a, b) {
    return new vector2(min(a.x, b.x), min(a.y, b.y));
}

function maxv(a, b) {
    return new vector2(max(a.x, b.x), max(a.y, b.y));
}

function matrix2(a, b, c, d) {
    this.m =
    [
    [a, b],
    [c, d]
    ];
    this.set = function (radians) {
        var c = Math.cos(radians);
        var s = Math.sin(radians);
        this.m[0][0] = c; this.m[0][1] = -s;
        this.m[1][0] = s; this.m[1][1] = c;
    }
    this.abs = function () {
        return new matrix2(this.m[0][0], this.m[0][1], this.m[1][0], this.m[1][1]);
    }
    this.axisX = function () {
        return new vector2(this.m[0][0], this.m[1][0]);
    }
    this.axisY = function () {
        return new vector2(this.m[0][1], this.m[1][1]);
    }
    this.transpose = function () {
        return new matrix2(this.m[0][0], this.m[1][0], this.m[0][1], this.m[1][1]);
    }
    this.transform = function (rhs) {
        return new vector2(this.m[0][0] * rhs.x + this.m[0][1] * rhs.y, this.m[1][0] * rhs.x + this.m[1][1] * rhs.y);
    }
    this.mult = function (rhs) {
        return new matrix2(
     this.m[0][0] * rhs.m[0][0] + this.m[0][1] * rhs.m[1][0],
     this.m[0][0] * rhs.m[0][1] + this.m[0][1] * rhs.m[1][1],
     this.m[1][0] * rhs.m[0][0] + this.m[1][1] * rhs.m[1][0],
     this.m[1][0] * rhs.m[0][1] + this.m[1][1] * rhs.m[1][1]
    );
    }
    this.toString = function () {
        return this.m[0][0].toString() + ", " + this.m[0][1].toString() + ", " + this.m[1][0].toString() + ", " + this.m[1][1].toString();
    }
    this.clone = function () {
        return new matrix2(this.m[0][0], this.m[0][1], this.m[1][0], this.m[1][1]);
    }

}

function getMartix2(radians) {
    var m = new matrix2(0, 0, 0, 0);
    m.set(radians);
    return m;
}

function biasGreaterThan(a, b) {
    var k_biasRelative = 0.95;
    var k_biasAbsolute = 0.01;
    return a >= b * k_biasRelative + a * k_biasAbsolute;
}