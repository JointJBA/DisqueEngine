function pointBox(min, max, c) {
    this.proj = new pointsProjector();
    this.proj.points.add(new vertex(min, c));
    this.proj.points.add(new vertex(max, c));
    this.proj.points.add(new vertex(new vector3(max.x, min.y, min.z), c));
    this.proj.points.add(new vertex(new vector3(min.x, max.y, min.z), c));
    this.proj.points.add(new vertex(new vector3(min.x, min.y, max.z), c));
    this.proj.points.add(new vertex(new vector3(max.x, max.y, min.z), c));
    this.proj.points.add(new vertex(new vector3(min.x, max.y, max.z), c));
    this.proj.points.add(new vertex(new vector3(max.x, min.y, max.z), c));
    this.transformation = matrix4.identity();
    this.draw = function () {
        this.proj.draw(this.transformation);
    }
}

function pointSphere(radius, res, c) {
    this.proj = new pointsProjector();
    var r = 360.0 * res;
    var diff = 360.0 / r;
    var p = new vector3(radius, 0, 0);
    for (var x = 0; x < r; x++) {
        var np = vector3.transform(p, matrix4.rotate(new vector3(0, 1, 0), Math.toRadians(x * diff)));
        for (var y = 0; y < r; y++) {
            np = vector3.transform(np, matrix4.rotate(new vector3(0, 0, 1), Math.toRadians(y * diff)));
            this.proj.points.add(new vertex(np, c));
        }
    }
    this.transformation = matrix4.identity();
    this.draw = function () {
        this.proj.draw(this.transformation);
    }
}

function lineRectangle(min, max, c) {
    this.proj = new linesProjector();
    this.proj.lines.add(new line(new vector3(min.x, min.y, 0), new vector3(max.x, min.y, 0), c));
    this.proj.lines.add(new line(new vector3(min.x, min.y, 0), new vector3(min.x, max.y, 0), c));
    this.proj.lines.add(new line(new vector3(max.x, max.y, 0), new vector3(max.x, min.y, 0), c));
    this.proj.lines.add(new line(new vector3(max.x, max.y, 0), new vector3(min.x, max.y, 0), c));
    this.transformation = matrix4.identity();
    this.draw = function () {
        this.proj.draw(this.transformation);
    }
}

function xyz() {
    this.proj = new linesProjector();
    this.proj.lines.add(new line(new vector3(-20, 0, 0), new vector3(20, 0, 0), new color(255, 0, 0)));
    this.proj.lines.add(new line(new vector3(0, -20, 0), new vector3(0, 20, 0), new color(0, 255, 0)));
    this.proj.lines.add(new line(new vector3(0, 0, -20), new vector3(0, 0, 20), new color(0, 0, 255)));
    this.transformation = matrix4.identity();
    this.draw = function () {
        this.proj.draw(this.transformation);
    }
}