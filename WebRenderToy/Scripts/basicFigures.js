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

function square(mn, s, col) {
    this.min = mn;
    this.side = s;
    this.color = col;
    this.draw = function (contxt) {
        contxt.beginPath();
        contxt.rect(this.min.x, this.min.y, this.side, this.side);
        contxt.fillStyle = 'rgb(' + this.color.r + ',' + this.color.g + ',' + this.color.b + ')';
        contxt.fill();
        contxt.closePath();
    }
}

function rect(mn, w, h, col) {
    this.min = mn;
    this.width = w;
    this.height = h;
    this.color = col;
    this.draw = function (contxt) {
        contxt.beginPath();
        contxt.rect(this.min.x, this.min.y, this.width, this.height);
        contxt.fillStyle = 'rgb(' + this.color.r + ',' + this.color.g + ',' + this.color.b + ')';
        contxt.fill();
        contxt.closePath();
    }
}

function specialCircle(c, r, co, sc) {
    this.position = c;
    this.radius = r;
    this.color = co;
    this.strokeColor = sc;
    this.draw = function (ctx) {
        ctx.save();
        ctx.beginPath();
        ctx.arc(this.position.x, this.position.y, this.radius, 0, Math.PI * 2,
                true);
        ctx.fillStyle = this.color.getStyle(ctx);
        ctx.shadowColor = this.strokeColor.getStyle(ctx);
        ctx.shadowBlur = 40;
        ctx.shadowOffsetX = 0;
        ctx.shadowOffsetY = 0;
        ctx.fill();
        ctx.closePath();
        ctx.restore();
    }
}