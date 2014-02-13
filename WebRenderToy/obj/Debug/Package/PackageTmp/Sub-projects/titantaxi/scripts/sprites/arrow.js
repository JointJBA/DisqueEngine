arrown = "arrow";
function arrow() {
    this.position = new vector2(0, 0);
    this.draw = function () {
        ctx.drawImage(IMGS[arrown], 0 ,0);
    }
}