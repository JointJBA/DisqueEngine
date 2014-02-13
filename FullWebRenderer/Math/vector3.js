function vector3(_x, _y, _z) {
    this.x = _x;
    this.y = _y;
    this.z = _z;
    this.w = 1;
    this.length = function () {
        return Math.sqrt(Math.pow(this.x, 2) + Math.pow(this.y, 2) + Math.pow(this.z, 2));
    }
    this.normalize = function () {
        var l = this.length();
        this.x = this.x / l;
        this.y = this.y / l;
        this.z = this.z / l;
    }
    this.add = function (v) {
        return new vector3(this.x + v.x, this.y + v.y, this.z + v.z);
    }
    this.sub = function (v) {
        return new vector3(this.x - v.x, this.y - v.y, this.z - v.z);
    }
    this.mult = function (s) {
        return new vector3(this.x * s, this.y * s, this.z * s);
    }
    this.dot = function (v) {
        return this.x * v.x + this.y * v.y + this.z * v.z;
    }
    this.cross = function (v) {
        return new vector3((this.y * v.z) - (v.y * this.z), (this.z * v.x) - (v.z * this.x), (this.x * v.y) - (v.x * this.y));
    }
    this.toString = function () {
        return "[" + this.x + ", " + this.y + ", " + this.z + "]";
    }
}
vector3.length = function (v) {
    return v.length();
}
vector3.length = function (va, vb) {
    return vector3.length(va.sub(vb));
}
vector3.normalize = function (v) {
    var vr = new vector3(v.x, v.y, v.z);
    vr.normalize();
    return vr;
}
vector3.add = function (va, vb) {
    return va.add(vb);
}
vector3.sub = function (va, vb) {
    return va.sub(vb);
}
vector3.mult = function (v, s) {
    return v.mult(s);
}
vector3.dot = function (va, vb) {
    return va.dot(vb);
}
vector3.cross = function (va, vb) {
    return va.cross(vb);
}