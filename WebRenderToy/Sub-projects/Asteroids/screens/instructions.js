//    new text("Controls", new vector2(WIDTH / 2.0, HEIGHT * 0.1), white, 30),
//    new text("Forward     " + spaceBT + upKey, new vector2(WIDTH / 2.0 - 80, HEIGHT * 0.1 + 30), white, 30),
//    new text("Backward" + spaceBT + downKey, new vector2(WIDTH / 2.0 - 50, HEIGHT * 0.1 + 62), white, 30),
//    new text("Strafe Left" + spaceBT + sLeft, new vector2(WIDTH / 2.0 - 140, HEIGHT * 0.1 + 93), white, 30)
var spaceBT = "              ";
var upKey = "Arrow Key Up";
var downKey = "Arrow Key Down";
var rRight = "Arrow Key Right";
var rLeft = "Arrow Key Left";
var sRight = "X";
var sLeft = "Z";
var shoot = "Space-bar";
function instructionScreen() {
    var textInst = new Array(
                new text("Controls", new vector2(WIDTH * .5, HEIGHT * 0.2), white, 60),
                new text("Move Forward" + spaceBT + "W", new vector2(WIDTH * .4, HEIGHT * .4), white, 50),
                new text("Point" + spaceBT + "Aim", new vector2(WIDTH * .476, HEIGHT * .5), white, 50),
                new text(" Click" + spaceBT + "Shoot", new vector2(WIDTH * .484, HEIGHT * .6), white, 50),
                new text("KILL THEM ALL", new vector2(WIDTH * .5, HEIGHT * .8), white, 50)
        );
    this._button = new defaultButton(new vector2(28, 28), 50, 50, "◀", function () {
        mainHost.navigate(home);
    });
    this.update = function () {
        this._button.update();
        for (var i = 0; i < textInst.length; i++) {
            textInst[i].update();
        }
    }
    this.draw = function () {
        this._button.draw();
        for (var i = 0; i < textInst.length; i++) {
            textInst[i].draw();
        }
    }
}