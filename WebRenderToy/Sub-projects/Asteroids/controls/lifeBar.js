function lifeBar (spos, fc, c) {
    this.startingPosition = spos;
    this.fillStyle = fc;
    this.count = c;
    this._rect = new rect(spos, WIDTH, 25, this.fillStyle);
    this.update = function (l) {
        this.count = l;
        this._rect.width = l * WIDTH;
    }
    this.draw = function () {
        this._rect.draw(CTX);
    }

}