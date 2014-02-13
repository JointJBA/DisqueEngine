function progressBar(cen, w, h, fs) {
    this.center = cen;
    this.width = w;
    this.height = h;
    this.min = 0;
    this.max = 100;
    this.value = 0;
    this.fillStyle = fs;
    this.update = function (p) {
        this.value = p;
    }
    this.draw = function () {
        ctx.save();
        ctx.beginPath();
        ctx.rect(this.center.x - (this.width / 2.0), this.center.y - (this.height / 2.0), (this.value / this.max) * this.width, this.height);
        ctx.clip();
        ctx.beginPath();
        ctx.rect(this.center.x - (this.width / 2.0), this.center.y - (this.height / 2.0), this.width, this.height);
        ctx.fillStyle = this.fillStyle.getStyle(ctx);
        ctx.fill();
        ctx.restore();
    }
}