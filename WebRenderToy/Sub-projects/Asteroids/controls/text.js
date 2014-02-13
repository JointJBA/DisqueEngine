function text(t, cen, fc, fs) {
    this.center = cen.clone();
    this.c = t;
    this.fontStyle = fc;
    this.fontSize = fs;
    this.width = textDimensions(TESTER, this.c, this.fontSize, FONT).width;
    this.height = textDimensions(TESTER, this.c, this.fontSize, FONT).height;
    this.update = function () {
        var dim = textDimensions(TESTER, this.c, this.fontSize, FONT);
        this.width = dim.width;
        this.height = dim.height;
    }
    this.draw = function () {
        CTX.save();
        CTX.fillStyle = this.fontStyle.getStyle();
        CTX.font = this.fontSize + "px " + FONT;
        CTX.fillText(this.c, this.center.x - (this.width / 2.0), this.center.y + (this.height / 4.0));
        CTX.restore();
    }
}