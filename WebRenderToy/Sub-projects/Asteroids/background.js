var white = new color(255, 255, 255);
function background(num) {
    this.container = new Array();
    this.start = function () {
        this.container.splice(0, this.container.length);
        this.container.push(new rect(new vector2(0, 0), WIDTH, HEIGHT, new color(0, 0, 0)));
        for (var i = 0; i < num; i++) {
            this.container.push(new circle(new vector2(random(0, WIDTH), random(0, HEIGHT)), random(1, 3), white));
        }
    }
    this.draw = function () {
        for (var i = 0; i < this.container.length; i++) {
            this.container[i].draw(CTX);
        }
    }

}