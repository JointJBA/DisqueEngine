function button(c, w, h, t, bc, bcc, sbcc, r, unc) {
    this.center = c.clone();
    this.width = w;
    this.height = h;
    this.content = t;
    this.borderColor = bc;
    this.backgroundColor = bcc;
    this.sbackgroundColor = sbcc;
    this.radius = r;
    this._pos = function () {
        return new vector2(this.center.x - (this.width / 2.0), this.center.y - (this.height / 2.0));
    }
    this._inter = new interactive(this._pos(), this.width, this.height, this);
    this._inter.mouseUp = function () {
        var temp = this.owner.backgroundColor;
        this.owner.backgroundColor = this.owner.sbackgroundColor;
        this.owner.sbackgroundColor = temp;
        this.owner.click();
    }
    this._inter.mouseDown = function () {
        var temp = this.owner.backgroundColor;
        this.owner.backgroundColor = this.owner.sbackgroundColor;
        this.owner.sbackgroundColor = temp;
    }
    this.click = function () {
        unc();
    }
    this.update = function () {
        this._inter.update();
        this.content.center = this.center.clone();
        this.content.update();
    }
    //    this._drawText = function () {
    //        var dim = textDimensions(TESTER, this.text, this.fontSize, FONT);
    //        CTX.fillStyle = this.fontColor.getStyle();
    //        CTX.font = this.fontSize + "px " + FONT;
    //        CTX.fillText(this.text, this.center.x - (dim.width / 2.0), this.center.y + (dim.height / 4.0));
    //    }
    this.draw = function () {
        CTX.save();
        CTX.fillStyle = this.backgroundColor.getStyle();
        CTX.strokeStyle = this.borderColor.getStyle();
        roundRectangle(CTX, this._pos().x, this._pos().y, this.width, this.height, this.radius, 5, true, true);
        CTX.restore();
        this.content.draw();
        //this._drawText();
    }
}

function defaultButton(c, w, h, t, func) {
    return new button(c, w, h, new text(t, c, new color(255, 255, 255), 30), new color(255, 255, 255), new color(51, 51, 51), new color(153, 153, 153), 5, func);
}