DEFAULTLIFESTART = 5;

function level(hst) {
    this.host = hst;
    this.score = 0;
    this.fuel = 100;
    this._controls = new Array();
    this._score = new score(new vector2(60, 24), 24, new color(255, 0, 0))
    this._bar = new progressBar(new vector2(60, 44), this._score._text.width, 10, new linearGradient(new vector2(0, 0), this._score._text.width, this._score._text.height, new Array(new color(255, 0, 0), new color(0, 255, 0))));
    this._bar.value = 100;
    this._lifeBar = new lifeBar(new vector2(15, 64), 8, new color(65, 86, 45), DEFAULTLIFESTART);
    this._currentLevel = 0;
    this._loading = false;
    this._lives = DEFAULTLIFESTART;
    this._gameState = true;
    this._pauseText = new text("GAME PAUSED", new vector2(width / 2.0, height / 2.0), new color(255, 255, 255), 60);
    this._levelItems = new Array();
    this._collisionPolygons = new Array();
    this._scene = new scene(1.0 / 30.0, 10);
    this._ship = new ship(new vector2(-2362, -23918), 35.0, 35.0);
    this._exitButton = new defaultButton(new vector2(width - 23, 69), 40.0, 40.0, "◀", function () {
        mainHost.navigate(welcomeScreen);
    });
    this._gameButton = new defaultButton(new vector2(width - 23, 23), 40.0, 40.0, "||", function () {
        if (levelScrn._gameState) {
            levelScrn._gameState = false;
            levelScrn._gameButton.content.c = "▶";
        }
        else {
            levelScrn._gameState = true;
            levelScrn._gameButton.content.c = "||";
        }
    });
    this.show = function () {
        this.start(1);
    }
    this.start = function (lvl) {
        this._currentLevel = lvl;
        this.score = 0;
        this._lives = DEFAULTLIFESTART;
        this._gameState = true;
        this._gameButton.content.c = "||";
        this._createLevel(lvl - 1);
    }
    this._createLevel = function (l) {
        this._loading = true;
        this._levelItems.splice(0, this._levelItems.length);
        this._collisionPolygons.splice(0, this._collisionPolygons.length);
        this._scene.clear();
        var cpol = LVLS[l]["collisionPolygons"], temp, t;
        for (var i = 0; i < cpol.length; i++) {
            temp = new polygon();
            t = cpol[i];
            temp.set(t.vertices);
            this._collisionPolygons.push(this._scene.add(temp, t.x, t.y));
        }
        this._ship.reset();
        this._ship.center = LVLS[l]["shipCenter"].clone();
        this._ship.body = this._scene.add(new circle(this._ship.width / 2.0, this._ship.center.x, this._ship.center.y));
        cpol = LVLS[l]["asteroids"];
        for (var i = 0; i < cpol.length; i++) {
            temp = new asteroid(cpol[i].center, cpol[i].vertices);
            this._levelItems.push(temp);
        }
    }
    this._updateGame = function () {
        this._scene.step();
        for (var i = 0; i < this._levelItems.length; i++) {
            this._levelItems[i].update();
        }
        this._ship.update();
    }
    this._drawGame = function () {
        for (var i = 0; i < this._levelItems.length; i++) {
            this._levelItems[i].draw();
        }
        this._ship.draw();
    }
    this.transition = function () {
    }
    this.update = function () {
        for (var i = 0; i < this._controls.length; i++) {
            this._controls[i].update();
        }
        this._lifeBar.update(this._lives);
        this._score.update(this.score);
        this._bar.update(this.fuel);
        this._gameButton.update();
        if (this._gameState) {
            this._updateGame();
        }
        else {
            this._pauseText.update(ctx);
            this._exitButton.update(ctx);
        }
    }
    this.draw = function () {
        if (!this._loading) {
            for (var i = 0; i < this._controls.length; i++) {
                this._controls[i].draw();
            }
            this._drawGame();
            this._score.draw();
            this._bar.draw();
            this._lifeBar.draw();
            this._gameButton.draw();
        }
        else {
            showLevelLoadingFull();
        }
        if (!this._gameState) {
            this._pauseText.draw();
            this._exitButton.draw();
        }
    }
}