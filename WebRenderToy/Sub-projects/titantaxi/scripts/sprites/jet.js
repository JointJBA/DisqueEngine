function particle(pos, life) {
    this.position = pos;
    this.lifeTime = life;
    this.draw = function () {
        ctx.beginPath();
        ctx.rect(this.position.x, this.position.y, 1, 1);
        ctx.fillStyle = "rgb(255,255,255)";
        ctx.fill();
        ctx.closePath();
    }
}

function jet(m, mx) {
    this.min = m;
    this.max = mx;
    this._particles = new Array();
    this._createParticle = function (c, st) {
        var p = new particle(c.clone(), random(10, 15 + st * 5));
        this._particles.push(p);
    }
    this._updateParticle = function (p, n, nx) {
        if (this._particles[p].lifeTime <= 0) {
            this._particles.splice(p, 1);
            return -1;
        }
        else {
            this._particles[p].lifeTime--;
            var v = toVector(random(n, nx));
            this._particles[p].position.add(v);
            return 0;
        }
    }
    this.update = function (s, st) {
        for (var i = 0; i < st * 5; i++) {
            this._createParticle(s, st);
        }
        for (var i = 0; i < this._particles.length; i++) {
            i -= this._updateParticle(i, this.min, this.max);
        }
    }
    this.stop = function () {
        this._particles.splice(0, this._particles.length);
    }
    this.draw = function () {
        for (var i = 0; i < this._particles.length; i++) {
            this._particles[i].draw();
        }
    }
}