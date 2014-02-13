function host() {
    this.currentScreen = null;
    this.navigate = function (n) {
        this._show(n);
    }
    this._show = function (screen) {
        this.currentScreen = screen;
        this.currentScreen.show();
    }
    this.update = function () {
        this.currentScreen.update();
    }
    this.draw = function () {
        this.currentScreen.draw();
    }
}