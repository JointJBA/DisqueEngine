function circle(pos, rad, col) {
    this.position = pos;
    this.radius = rad;
    this.color = col;
    this.draw = function (ctx) {
        ctx.save();
        ctx.beginPath();
        ctx.arc(this.position.x, this.position.y, this.radius, 0, Math.PI * 2,
                true);
        ctx.fillStyle = this.color.getStyle();
        ctx.fill();
        ctx.closePath();
        ctx.restore();
    }
}

function square(mn, sps, s, col) {
    this.min = mn;
    this.spos = sps;
    this.side = s;
    this.color = col;
    this.draw = function (contxt) {
        contxt.beginPath();
        contxt.rect(this.min.x, this.min.y, s, s);
        contxt.fillStyle = 'rgb(' + this.color.r + ',' + this.color.g + ',' + this.color.b + ')';
        contxt.fill();
        contxt.closePath();
    }
}