function host(scrn) {
    this._screen = scrn;
    this.navigate = function (nscrn) {
        this._screen = nscrn;
    }
    this.update = function () {
        this._screen.update();
    }
    this.draw = function () {
        this._screen.draw();
    }
}