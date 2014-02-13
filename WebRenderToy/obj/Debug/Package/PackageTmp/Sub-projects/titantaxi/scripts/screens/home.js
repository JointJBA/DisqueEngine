function home(hst) {
    this.chost = hst;
    this._buttons = new Array(
    new defaultButton(new vector2(width / 2, 280), 350, 60, "Play", function () {
        mainHost.navigate(levelScrn);
    }), new defaultButton(new vector2(width / 2, 350), 350, 60, "Settings", function () {
        mainHost.navigate(settingsScrn);
    }), new defaultButton(new vector2(width / 2, 420), 350, 60, "Help", function () {
        mainHost.navigate(helpScrn);
    }));
    this.show = function () { }
    this.update = function () {
        for (var i = 0; i < this._buttons.length; i++) {
            this._buttons[i].update();
        }
    }
    this.draw = function () {
        ctx.scale(0.5, 0.5);
        ctx.drawImage(IMGS["logo"], width - (699 / 2.0), 0);
        ctx.scale(2, 2);
        for (var i = 0; i < this._buttons.length; i++) {
            this._buttons[i].draw();
        }
    }
}