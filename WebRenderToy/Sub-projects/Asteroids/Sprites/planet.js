function planet() {
    this.type = "planet";
    this.radius = WIDTH * 0.05;
    this.center = new vector2(WIDTH / 2.0, HEIGHT / 2.0);
    this._circle = new specialCircle(
    new vector2(WIDTH / 2.0, HEIGHT / 2.0),
    WIDTH * 0.05,
    new color(234, 121, 5), new color(0, 0, 255));
    this.life = 20;
    this.mxLife = 20;
    this.angle = 0;
    this.shoot = 0;
    this._draw = function () {
        this._circle.draw(CTX);
    }
    this.update = function () {
        this.angle++;
        if (this.shoot % 30 == 0) {
            var tmat = getMatrix2(toRadians(this.angle));
            var temp = new sas(this.center.clone(), 5, 25, tmat.transform(new vector2(1, 0)));
            temp.good = false;
            game._sprites.push(temp);
        }
        else if (this.shoot % 500 == 0) {
            var ang = 0;
            for (var i = 0; i < 6; i++) {
                var tmat = getMatrix2(toRadians(ang));
                var temp = new sas(this.center.clone(), 5, 25, tmat.transform(new vector2(1, 0)));
                temp.good = false;
                game._sprites.push(temp);
                ang += 60;
            }
        }
        if (this.shoot % 30 == 0) {
            var temp = new sas(this.center.clone(), 5, 20, game._ast.center.clone().subt(this.center).normalize());
            temp.stype = "rocket";
            temp.good = false;
            game._sprites.push(temp);

        }
        this.shoot++;
    }
    this.draw = function () {
        this._draw();
    }
}