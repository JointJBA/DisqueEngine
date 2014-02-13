function interactive(s, w, h, o) {
    this.pos = s;
    this.width = w;
    this.height = h;
    this.owner = o;
    this.ins = false;
    this.enter = function () { }
    this.leave = function () { }
    this.mouseUp = function () { }
    this.mouseDown = function () { }
    this.update = function () {
        if ((MX > this.pos.x && MX < (this.pos.x + this.width)) && (MY > this.pos.y && MY < (this.pos.y + this.height))) {
            if (!this.ins) {
                this.ins = true;
                this.enter();
            }
            this._updateState();
        }
        else {
            if (this.ins) {
                this.ins = false;
                this.leave();
            }
        }
    }
    this._State = true;
    this._updateState = function () {
        if (STATE && !this._State) {
            this._State = true;
            this.mouseUp();
        }
        else if (!STATE && this._State) {
            this._State = false;
            this.mouseDown();
        }
    }
}