var back = new background(random(150, 300));
var home, inst, game, mainHost, story;
function gameLoop() {
    back.start();
    home = new welcomeScreen();
    inst = new instructionScreen();
    game = new gameScreen();
    story = new storyScreen();
    mainHost = new host(home);
    this.screen = mainHost;
    this.update = function () {
        this.screen.update();
    }
    this.draw = function () {
        back.draw();
        this.screen.draw();
    }
}