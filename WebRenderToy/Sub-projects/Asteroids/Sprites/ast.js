function asShape(c, r) {
    this.center = c;
    this.radius = r;
    this.color = white;
    this._points = new Array();
    var temp = new vector2(this.radius, 0);
    var num = random(10, 15);
    var mat = getMatrix2(toRadians(360.0 / num));
    this._points.push(temp.clone());
    for (var i = 0; i < num; i++) {
        temp = mat.transform(temp);
        this._points.push(scalev(temp, (Math.random() * 0.4) + 0.6));
    }
    this.draw = function (contxt) {
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
        contxt.fillStyle = this.color.getStyle();
        contxt.fill();
        contxt.closePath();
        contxt.restore();

    }

}

function asteroid() {
    this.center = new vector2(WIDTH / 2.0, HEIGHT * 0.8);
    this.type = "asteroid";
    this._shape = new asShape(new vector2(WIDTH / 2.0, HEIGHT * 0.8), WIDTH * 0.02);
    this.angle = 0;
    this.life = 20;
    this.mxLife = 20;
    this.shoot = 10;
    this.radius = this._shape.radius;
    this.update = function () {
        var temp = new vector2(this.center.x - MX, this.center.y - MY).normalize();
        this.angle = -toAngle(temp);
        if (keysDown[W]) {
            this.center.add(temp.scale(-20.0));
        }
        if (keysDown[CLICK]) {
            if (this.shoot >= 10) {
                this.shoot = 0;
                game._sprites.push(new sas(this.center.clone(), random(3, 8), random(30, 50), new vector2(MX, MY).subt(this.center).normalize()));
            }
            else {
                this.shoot++;
            }
        }
        else {
            this.shoot = 11;
        }
        this._shape.center = this.center.clone();
    }
    this.draw = function () {
        CTX.save();
        CTX.translate(this.center.x, this.center.y);
        CTX.rotate(toRadians(this.angle));
        CTX.translate(-this.center.x, -this.center.y);
        this._shape.draw(CTX);
        CTX.restore();
    }
}
