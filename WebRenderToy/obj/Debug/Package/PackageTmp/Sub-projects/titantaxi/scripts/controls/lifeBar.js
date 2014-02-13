function lifeBar (spos, rad, fc, c) {
    this.startingPosition = spos;
    this.radius = rad;
    this.fillStyle = fc;
    this.count = c;
    this.update = function (l) {
        this.count = l;
    }
    this.draw = function () {
        for (var i = 0; i < this.count; i++) {
            drawCircle(this.startingPosition.x + this.radius * i * 2.2, this.startingPosition.y, this.radius, this.fillStyle);
        }
    }

}

function drawCircle(x, y, rad, fill) {
    ctx.save();
    ctx.beginPath();
    ctx.arc(x, y, rad, 0, Math.PI * 2,
                true);
    ctx.fillStyle = fill.getStyle();
    ctx.fill();
    ctx.closePath();
    ctx.restore();
}