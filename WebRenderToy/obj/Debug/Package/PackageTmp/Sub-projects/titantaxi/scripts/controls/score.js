function score(pos, fs, fc) {
    this._text = new text("00000000", pos, fc, fs);
    this.update = function (s) {
        this._text.c = s;
        while (this._text.c.length != 8) {
            this._text.c = this._text.c + "0";
        }
        this._text.update();
    }
    this.draw = function () {
        this._text.draw();
    }
}