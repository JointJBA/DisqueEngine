function getArray() {
    var ret = new Array(4);
    for (var i = 0; i < 4; i++) {
        ret[i] = [0, 0, 0, 0];
    }
    return ret;
}

function matrix4(d) {
    this.data = d;
    this.get = function (r, c) {
        return this.data[r][c];
    }
    this.add = function (m) {
        var d = getArray();
        for (var c = 0; c < 4; c++)
            for (var r = 0; r < 4; r++) {
                d[r][c] = this.get(r, c) + m.get(r, c);
            }
        return new matrix4(d);
    }
    this.sub = function (m) {
        var d = getArray();
        for (var c = 0; c < 4; c++)
            for (var r = 0; r < 4; r++) {
                d[r][c] = this.get(r, c) - m.get(r, c);
            }
        return new matrix4(d);
    }
    this.scale = function (f) {
        var d = getArray();
        for (var c = 0; c < 4; c++)
            for (var r = 0; r < 4; r++) {
                d[r][c] = this.get(r, c) * f;
            }
        return new matrix4(d);
    }
    this.mult = function (m) {
        var result = getArray();
        for (var r = 0; r < 4; r++)
            for (var c = 0; c < 4; c++) {
                var sum = 0;
                for (var s = 0; s < 4; s++) {
                    sum += this.data[r][s] * m.data[s][c];
                }
                result[r][c] = sum;
            }
        return new matrix4(result);
    }
    this.transpose = function () {
        var result = getArray();
        for (var r = 0; r < 4; r++)
            for (var c = 0; c < 4; c++)
                result[c][r] = this.data[r][c];
        return new matrix4(result);
    }
    this.toString = function () {
        var res = "";
        for (var r = 0; r < 4; r++) {
            res += "[ ";
            for (var c = 0; c < 4; c++) {
                res += this.data[r][c] + " ";
            }
            res += "]";
        }
        return res;
    }
}

matrix4.identity = function () {
    var d = getArray();
    d[0][0] = 1;
    d[1][1] = 1;
    d[2][2] = 1;
    d[3][3] = 1;
    return new matrix4(d);
}

matrix4.add = function (m1, m2) {
    return m1.add(m2);
}

matrix4.sub = function (m1, m2) {
    return m1.sub(m2);
}

matrix4.scale = function (m, s) {
    return m.scale(s);
}

matrix4.mult = function (m1, m2) {
    return m1.mult(m2);
}

matrix4.rotate = function (axis, radians) {
    var x = axis.x;
    var y = axis.y;
    var z = axis.z;
    var num = Math.sin(radians);
    var num2 = Math.cos(radians);
    var num3 = x * x;
    var num4 = y * y;
    var num5 = z * z;
    var num6 = x * y;
    var num7 = x * z;
    var num8 = y * z;
    var result = getArray();
    result[0][0] = num3 + num2 * (1.0 - num3);
    result[0][1] = num6 - num2 * num6 + num * z;
    result[0][2] = num7 - num2 * num7 - num * y;
    result[0][3] = 0.0;
    result[1][0] = num6 - num2 * num6 - num * z;
    result[1][1] = num4 + num2 * (1.0 - num4);
    result[1][2] = num8 - num2 * num8 + num * x;
    result[1][3] = 0.0;
    result[2][0] = num7 - num2 * num7 + num * y;
    result[2][1] = num8 - num2 * num8 - num * x;
    result[2][2] = num5 + num2 * (1.0 - num5);
    result[2][3] = 0.0;
    return new matrix4(result);
}

matrix4.scale = function (scale) {
    var d = getArray();
    d[0][0] = scale.x;
    d[1][1] = scale.y;
    d[2][2] = scale.z;
    d[3][3] = 1;
    return new matrix4(d);
}

matrix4.translate = function (pos) {
    var id = matrix4.identity();
    id.data[0][3] = pos.x;
    id.data[1][3] = pos.y;
    id.data[2][3] = pos.z;
    return id;
}

matrix4.createLookAt = function (cam, targ, up) {
    var v1 = vector3.normalize(cam.sub(targ));
    var v2 = vector3.normalize(up.cross(v1));
    var v3 = v1.cross(v2);
    var d = [
    [v2.x, v3.x, v1.x, 0],
    [v2.y, v3.y, v1.y, 0],
    [v2.z, v3.z, v1.z, 0],
    [-v2.dot(cam), -v3.dot(cam), -v1.dot(cam), 1]];
    return new matrix4(d);
}

matrix4.createPerspectivePOV = function (fov, ar, npd, fpd) {
    var res = getArray();
    var f1 = 1.0 / Math.tan(fov * 0.5);
    var f9 = f1 / ar;
    res[0][0] = f9;
    var f8 = 0.0;
    res[0][3] = 0.0;
    var f7 = f8;
    res[0][2] = f8;
    res[0][1] = f7;
    res[1][1] = f1;
    var f6 = 0.0;
    res[1][3] = 0.0;
    var f5 = f6;
    res[1][2] = f6;
    res[0][1] = f5;
    var f4 = 0.0;
    res[2][1] = 0.0;
    res[2][0] = f4;
    res[2][2] = fpd / (npd - fpd);
    res[2][3] = -1.0;
    var f3 = 0.0;
    res[3][3] = 0.0;
    var f2 = f3;
    res[3][1] = f3;
    res[3][0] = f2;
    res[3][2] = npd * fpd / (npd - fpd);
    return new matrix4(res);
}

matrix4.createPerspectiveProjection = function (fov, aspect, znear, zfar) {
    var xymax = znear * Math.tan(fov * (Math.PI / 360.0));
    var ymin = -xymax;
    var xmin = -xymax;
    var width = xymax - xmin;
    var height = xymax - ymin;
    var depth = zfar - znear;
    var q = -(zfar + znear) / depth;
    var qn = -2.0 * (zfar * znear) / depth;
    var w = 2.0 * znear / width;
    w = w / aspect;
    var h = 2.0 * znear / height;
    var d = [
    [w, 0, 0, 0],
    [0, h, 0, 0],
    [0, 0, q, qn],
    [0, 0, -1, 0]];
    return new matrix4(d);
}

matrix4.createPerspectiveProjections = function (fov, ar, n, f) {
    var s = 1.0 / (Math.tan(fov * 0.5 * (Math.PI / 180.0)));
    var fdn = -(f / (f - n));
    var fmn = -(f * n / (f - n));
    var d = [
    [s, 0, 0, 0],
    [0, s, 0, 0],
    [0, 0, fdn, -1],
    [0, 0, fmn, 0]];
    return new matrix4(d);
}


vector3.transform = function (v, m) {
    return new vector3(
                m.data[0][0] * v.x + m.data[0][1] * v.y + m.data[0][2] * v.z + m.data[0][3],
                m.data[1][0] * v.x + m.data[1][1] * v.y + m.data[1][2] * v.z + m.data[1][3],
                m.data[2][0] * v.x + m.data[2][1] * v.y + m.data[2][2] * v.z + m.data[2][3]);
}

vector3.transformProjection = function (v, m) {
    var ret = new vector3(
                m.data[0][0] * v.x + m.data[0][1] * v.y + m.data[0][2] * v.z + m.data[0][3],
                m.data[1][0] * v.x + m.data[1][1] * v.y + m.data[1][2] * v.z + m.data[1][3],
                m.data[2][0] * v.x + m.data[2][1] * v.y + m.data[2][2] * v.z + m.data[2][3]);
    ret.w = m.data[3][0] * v.x + m.data[3][1] * v.y + m.data[3][2] * v.z + m.data[3][3];
    return ret;
}

vector3.transformDirection = function (v, m) {
    return new vector3(
                m.data[0][0] * v.x + m.data[0][1] * v.y + m.data[0][2] * v.z,
                m.data[1][0] * v.x + m.data[1][1] * v.y + m.data[1][2] * v.z,
                m.data[2][0] * v.x + m.data[2][1] * v.y + m.data[2][2] * v.z);
}