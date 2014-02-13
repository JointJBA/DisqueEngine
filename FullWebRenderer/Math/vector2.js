function vector2(_x, _y) {
    this.x = _x;
    this.y = _y;
    this.length = function () {
        return Math.sqrt(Math.pow(this.x, 2) + Math.pow(this.y, 2));
    }
    this.normalize = function () {
        var l = this.length();
        this.x = this.x / l;
        this.y = this.y / l;
    }
    this.add = function (v) {
        return new vector2(this.x + v.x, this.y + v.y);
    }
    this.sub = function (v) {
        return new vector2(this.x - v.x, this.y - v.y);
    }
    this.mult = function (s) {
        return new vector2(this.x * s, this.y * s);
    }
    this.dot = function (v) {
        return this.x * v.x + this.y * v.y;
    }
    this.toString = function () {
        return "[" + this.x + ", " + this.y + "]";
    }
}
vector2.length = function (v) {
    return v.length();
}
vector2.length = function (va, vb) {
    return vector2.length(va.sub(vb));
}
vector2.normalize = function (v) {
    var vr = new vector2(v.x, v.y);
    vr.normalize();
    return vr;
}
vector2.add = function (va, vb) {
    return va.add(vb);
}
vector2.sub = function (va, vb) {
    return va.sub(vb);
}
vector2.mult = function (v, s) {
    return v.mult(s);
}
vector2.dot = function (va, vb) {
    return va.dot(vb);
}