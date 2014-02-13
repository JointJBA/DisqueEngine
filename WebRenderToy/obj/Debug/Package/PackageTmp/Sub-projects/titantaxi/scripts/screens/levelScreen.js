function levelScreen(hst) {
    this._buttons = new Array(new defaultButton(new vector2(23.0, 23.0), 40.0, 40.0, "<", function () {
        mainHost.navigate(welcomeScreen);
    }));
    this.update = function () {
        for (var i = 0; i < this._buttons.length; i++) {
            this._buttons[i].update();
        }
    }
    this.draw = function () {
        for (var i = 0; i < this._buttons.length; i++) {
            this._buttons[i].draw();
        }
    }
}