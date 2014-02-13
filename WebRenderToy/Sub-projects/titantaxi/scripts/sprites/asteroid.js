asteroidColor = new color(100, 100, 100);
function asteroid(c, ver) {
    this.center = c.clone();
    this.vertices = ver;
    this.update = function () {
    }
    this.draw = function () {
        ctx.save();
        ctx.beginPath();
        var be = addv(this.position, this.vertices[0]);
        ctx.moveTo(be.x, be.y);
        for (var i = 1; i < b.vertices.length; i++) {
            be = addv(this.position, this.vertices[i]);
            ctx.lineTo(be.x, be.y);
        }
        ctx.closePath();
        ctx.fillStyle = asteroidColor.getStyle();
        ctx.fill();
        ctx.stroke();
        ctx.restore();
    }
}