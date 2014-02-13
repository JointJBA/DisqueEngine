function storyScreen() {

    this.backButton2 = new defaultButton(new vector2(28, 28), 50, 50, "◀", function () {
        mainHost.navigate(home);
    });
    var textInst2 = new Array(
                new text("You are an asteroid.", new vector2(WIDTH * .5, HEIGHT * 0.2), white, 80),
                new text("But unlike your peers, you are sentient.", new vector2(WIDTH * .5, HEIGHT * .3), white, 45),
                new text("You are fed up with the Galactic Feds: killing all your friends just", new vector2(WIDTH * .5, HEIGHT * .4), white, 45),
                new text("because of the trajectories they're born with.", new vector2(WIDTH * .5, HEIGHT * .5), white, 45),
                new text("The hypocrites exercise Lockeian philosophy but do not hesistate", new vector2(WIDTH * .5, HEIGHT * .6), white, 45),
                new text("to encroach upon your natural rights.", new vector2(WIDTH * .5, HEIGHT * .7), white, 45),
                new text("But no more!", new vector2(WIDTH * .5, HEIGHT * .8), white, 45),
                new text("You decide to take things into your own hands...", new vector2(WIDTH * .5, HEIGHT * .9), white, 45)
        );
    this.update = function () {
        for (var i = 0; i < textInst2.length; i++) {
            textInst2[i].update();
        }
        this.backButton2.update();
    }
    this.draw = function () {
        for (var i = 0; i < textInst2.length; i++) {
            textInst2[i].draw();
        }
        this.backButton2.draw();
    }
}