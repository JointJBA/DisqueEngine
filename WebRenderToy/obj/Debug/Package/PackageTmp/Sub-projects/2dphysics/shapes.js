function circle(r) {
    this.radius = r;
    this.body = null;

    this.initialize = function () {
        this.computeMass(1.0);
    }
    this.computeMass = function (density) {
        this.body.m = Math.PI * this.radius * this.radius * density;
        this.body.im = this.body.m > 0.0 ? (1.0 / this.body.m) : 0.0;
        this.body.i = this.body.m * this.radius * this.radius;
        this.body.ii = this.body.i > 0.0 ? (1.0 / this.body.i) : 0.0;
    }
    this.setOrient = function () {
    }
    this.getType = function () {
        return 0;
    }
    this.clone = function () {
        return new circle(this.radius);
    }
}

function polygon() {
    this.body = null;
    this.u = new matrix2(0, 0, 0, 0);
    this.m_vertices = new Array();
    this.m_normals = new Array();
    this.getType = function () {
        return 1;
    }
    this.initialize = function () {
        this.computeMass(1.0);
    }
    this.setOrient = function (radians) {
        this.u.set(radians);
    }
    this.computeMass = function (density) {
        var c = new vector2(0, 0);
        var area = 0.0, I = 0.0, k_inv3 = 1.0 / 3.0;
        for (var i1 = 0; i1 < this.m_vertices.length; i1++) {
            var p1 = this.m_vertices[i1].clone();
            var i2 = i1 + 1 < this.m_vertices.length ? i1 + 1 : 0;
            var p2 = this.m_vertices[i2].clone();
            var D = crossv(p1, p2);
            var triangleArea = 0.5 * D;

            area += triangleArea;

            c.add(scalev(addv(p1, p2), triangleArea * k_inv3));

            var intx2 = p1.x * p1.x + p2.x * p1.x + p2.x * p2.x;
            var inty2 = p1.y * p1.y + p2.y * p1.y + p2.y * p2.y;
            I += (0.25 * k_inv3 * D) * (intx2 + inty2);
        }

        c.scale(1.0 / area);

        for (var i = 0; i < this.m_vertices.length; i++) {
            this.m_vertices[i].subt(c);
        }

        this.body.m = density * area;
        this.body.im = (this.body.m > 0.0) ? 1.0 / this.body.m : 0.0;
        this.body.i = I * density;
        this.body.ii = (this.body.i > 0.0) ? 1.0 / this.body.i : 0.0;
    }
    this.setBox = function (hw, hh) {
        this.m_vertices[0] = new vector2(-hw, -hh);
        this.m_vertices[1] = new vector2(hw, -hh);
        this.m_vertices[2] = new vector2(hw, hh);
        this.m_vertices[3] = new vector2(-hw, hh);
        this.m_normals[0] = new vector2(0.0, -1.0);
        this.m_normals[1] = new vector2(1.0, 0.0);
        this.m_normals[2] = new vector2(0.0, 1.0);
        this.m_normals[3] = new vector2(-1.0, 0.0);
    }
    this.set = function (vertices) {
        var count = vertices.length;
        var rightMost = 0, highestXCoord = vertices[0].x;
        for (var i = 1; i < count; i++) {
            var x = vertices[i].x;
            if (x > highestXCoord) {
                highestXCoord = x;
                rightMost = i;
            }
            else if (x == highestXCoord) {
                if (vertices[i].y < vertices[rightMost].y) {
                    rightMost = i;
                }
            }
        }
        var hull = new Array();
        var indexHull = rightMost;
        var outCount = 0;

        for (; ; ) {
            console.log("wtf");
            hull[outCount] = indexHull;
            var nextHullIndex = 0;
            for (var i = 1; i < count; i++) {
                if (nextHullIndex == indexHull) {
                    nextHullIndex = i;
                    continue;
                }
                var e1 = subv(vertices[nextHullIndex], vertices[hull[outCount]]);
                var e2 = subv(vertices[i], vertices[hull[outCount]]);
                var c = crossv(e1, e2);
                if (c < 0.0) {
                    nextHullIndex = i;
                }

                if (c == 0.0 && e2.lengthSquare() > e1.lengthSquare()) {
                    nextHullIndex = i;
                }

                outCount++;
                indexHull = nextHullIndex;
                if (nextHullIndex == rightMost) {
                    break;
                }
            }
            for (var i = 0; i < outCount; i++) {
                this.m_vertices[i] = vertices[hull[i]].clone();
            }

            for (var i1 = 0; i1 < outCount; i1++) {
                var i2 = i1 + 1 < outCount ? i1 + 1 : 0;
                var face = subv(this.m_vertices[i2], this.m_vertices[i1]);
                this.m_normals[i1] = new vector2(face.y, -face.x);
                this.m_normals[i1].normalize();
            }
        }
    }
    this.getSupport = function (dir) {
        var bestProjection = -(3400000000000000);
        var bestVertex = new vector2(0, 0);

        for (var i = 0; i < this.m_vertices.length; i++) {
            var v = this.m_vertices[i];
            var projection = dotv(v, dir);

            if (projection > bestProjection) {
                bestVertex = v;
                bestProjection = projection;
            }
        }
        return bestProjection;
    }
    this.clone = function () {
        var poly = new polygon();
        poly.u = this.u.clone();
        for (var i = 0; i < this.m_vertices.length; i++) {
            poly.m_vertices[i] = this.m_vertices[i].clone();
            poly.m_normals[i] = this.m_normals[i].clone();
        }
        return poly;
    }
}