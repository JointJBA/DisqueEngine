function ship(c, w, h) {
    this.center = c;
    this.width = w;
    this.height = h;
    this.body = null;
    this._jet = new jet(60, 120);
    this._jetState = 1;
    this.reset = function () {
        this._jetState = 1;
    }
    this._pos = function () {
        return new vector2(this.center.x - (this.width / 2.0), this.center.y - (this.height / 2.0));
    }
    this.update = function () {
        this.center = this.body.position.clone();
        if (this._jetState > 1) {
            this.body.applyForce(new vector2(0, -100000));
        }
        this._jet.update(this.center, this._jetState);
    }
    this.draw = function () {
        this._jet.draw();
        var wsfactor = this.width / 35.0, hsfactor = this.height / 35.0;
        ctx.save();
        ctx.scale(wsfactor, hsfactor);
        ctx.drawImage(IMGS["ship"], this._pos().x * (1.0 / wsfactor), this._pos().y * (1.0 / hsfactor));
        ctx.restore();
    }
}
function setupShipControl(s) {
    shipControl.upEvents.push(function () {
        s._jetState = 4;
    });
    shipControl.upReleaseEvents.push(function () {
        s._jetState = 1;
    });
}


