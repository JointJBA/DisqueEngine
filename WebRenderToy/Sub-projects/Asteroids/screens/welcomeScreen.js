function welcomeScreen() {
    this._text = new text("Asteroids", new vector2(WIDTH / 2.0, HEIGHT / 6.0), new color(255, 255, 255), HEIGHT * 0.23);
    this._playButton = new defaultButton(new vector2(WIDTH * 0.2, 0.9 * HEIGHT), WIDTH * 0.25, HEIGHT * 0.1667, "Play", function () {
        mainHost.navigate(game);
    });
    this._instButton = new defaultButton(new vector2(WIDTH * 0.5, 0.9 * HEIGHT), WIDTH * 0.25, HEIGHT * 0.1667, "Instructions", function () {
        mainHost.navigate(inst);
    });
    this._setsButton = new defaultButton(new vector2(WIDTH * 0.8, 0.9 * HEIGHT), WIDTH * 0.25, HEIGHT * 0.1667, "Story", function () {
        mainHost.navigate(story);
    });
    this.update = function () {
        this._text.fontSize = HEIGHT * 0.23;
        this._text.center = new vector2(WIDTH / 2.0, HEIGHT / 6.0);
        this._text.update();
        this._playButton.update();
        this._instButton.update();
        this._setsButton.update();
    }
    this.draw = function () {
        this._text.draw();
        this._playButton.draw();
        this._instButton.draw();
        this._setsButton.draw();
    }
}