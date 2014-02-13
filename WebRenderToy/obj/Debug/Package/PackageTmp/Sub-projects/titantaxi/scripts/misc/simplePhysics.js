function intersection(f1, f2, norm, d) {
    this.fig1 = f1;
    this.fig2 = f2;
    this.normal = norm;
    this.depth = d;
}

function figure(ps, rad, s) {
    this.pos = ps;
    this.radius = rad;
    this.stat = s;
    this.intersect = function (f2) {
        if (!this.stat || !f2.stat) {
            var d = distance(this.pos, f2.pos);
            var s = this.radius + f2.radius;
            if (d < s) {
                return { 'hasCollision': true, 'inters': new intersection(this, f2, this.pos.clone().subt(f2.pos)) }
            }
            else
                return { 'hasCollision': false }
        }
        else {
            return { 'hasCollision': false }
        }
    }
}

function sprite(ct, m, ap, v, fig) {
    this.center = ct;
    this.velocity = v;
    this.acceleration = new vector2(0, 0);
    this.mass = m;
    this.figure = fig;
    this.apply = ap;
    this.update = function () {
        this.center.add(this.velocity);
        this.velocity.add(this.acceleration);
    }
}