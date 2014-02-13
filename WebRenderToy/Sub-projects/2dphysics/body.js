function body(shape_, x, y) {
    this.position = new vector2(x, y);
    this.velocity = new vector2(0, 0);
    this.angularVelocity = 0;
    this.torque = 0;
    this.orient = 0;
    this.force = new vector2(0, 0);
    this.i = null;
    this.ii = null;
    this.m = null;
    this.im = null;
    this.shape = shape_.clone();
    this.shape.body = this;
    this.shape.initialize();
    this.dfriction = 0.3;
    this.sfriction = 0.5;
    this.rest = 0.2;
    this.applyForce = function (f) {
        this.force.add(f);
    }
    this.applyImpulse = function (impulse, cv) {
        this.velocity.add(scalev(impulse, this.im));
        this.angularVelocity += this.ii * crossv(cv, impulse);
    }
    this.setStatic = function () {
        this.i = this.ii = this.m = this.im = 0;
    }
    this.setOrient = function (rad) {
        this.orient = rad;
        this.shape.setOrient(rad);
    }
}