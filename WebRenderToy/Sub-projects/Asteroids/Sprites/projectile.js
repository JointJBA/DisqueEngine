function triangle(c, r) {
    this.center = c;
    this.radius = r;
    this.draw = function () {
        CTX.save();
        CTX.translate(this.center.x, this.center.y);
        CTX.beginPath();
        CTX.moveTo(this.radius, 0);
        CTX.lineTo(-this.radius, this.radius);
        CTX.lineTo(-this.radius, -this.radius);
        CTX.lineTo(this.radius, 0);
        CTX.fillStyle = white.getStyle();
        CTX.fill();
        CTX.closePath();
        CTX.restore();
    }
}

function sas(cen, rad, spd, dir) {
    this.type = "projectile";
    this.stype = "bullet";
    this.triangle = new triangle(cen, rad);
    this.center = cen;
    this.radius = rad;
    this.speed = spd;
    this.good = true;
    this.direction = dir;
    this.circle = new specialCircle(cen, rad, new color(255, 0, 0), new color(255, 0, 0));
    this._points = new Array();
    var temp = new vector2(this.radius, 0);
    var num = random(10, 15);
    var mat = getMatrix2(toRadians(360.0 / num));
    this._points.push(temp.clone());
    for (var i = 0; i < num; i++) {
        temp = mat.transform(temp);
        this._points.push(scalev(temp, (Math.random() * 0.4) + 0.6));
    }
    this.update = function () {
        this.center = this.center.add(this.direction.clone().scale(this.speed));
        this.circle.position = this.center.clone();
    }
    this.draw = function () {
        if (!this.good) {
            if (this.stype == "bullet") {
                this.circle.draw(CTX);
            }
            else if (this.stype == "rocket") {
                CTX.save();
                CTX.translate(this.center.x, this.center.y);
                CTX.rotate(-toAngle(this.direction));
                CTX.translate(-this.center.x, -this.center.y);
                this.triangle.draw();
                CTX.restore();
            }
        }
        else {
            var contxt = CTX;
            contxt.save();
            contxt.translate(this.center.x, this.center.y);
            contxt.beginPath();
            contxt.moveTo(this._points[0].x, this._points[0].y);
            for (var i = 1; i < num; i++) {
                contxt.lineTo(this._points[i].x, this._points[i].y);
            }
            contxt.lineTo(this._points[0].x, this._points[0].y);
            //        contxt.lineTo(-this.radius, this.radius);
            //        contxt.lineTo(-this.radius, 0);
            //        contxt.lineTo(this.radius, this.radius);
            //        contxt.lineTo(this.radius, 0);
            //        contxt.lineTo(0, -this.radius);
            contxt.fillStyle = white.getStyle();
            contxt.fill();
            contxt.closePath();
            contxt.restore();
        }
    }
}