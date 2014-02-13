function manifold(a, b) {
    this.A = a;
    this.B = b;
    this.penetration = 0;
    this.normal = null;
    this.contacts = new Array();
    //this.contacts.length = 0;
    this.e = 0;
    this.df = 0;
    this.sf = 0;
    this.solve = function () {
        Dispatch[this.A.shape.getType()][this.B.shape.getType()](this, this.A, this.B);
    }
    this.initialize = function () {
        this.e = min(this.A.rest, this.B.rest);
        this.sf = Math.sqrt(this.A.sfriction * this.A.sfriction);
        this.df = Math.sqrt(this.A.dfriction * this.A.dfriction);

        for (var i = 0; i < this.contacts.length; i++) {
            var ra = subv(this.contacts[i], this.A.position);
            var rb = subv(this.contacts[i], this.B.position);

            var rv = subv(addv(this.B.velocity, crossv(this.B.angularVelocity, rb)), subv(this.A.velocity, crossv(this.A.angularVelocity, ra)));
            if (rv.lengthSquare() < scalev(gravity, dt).lengthSquare() + EPSILON) {
                this.e = 0.0;
            }
        }
    }
    this.applyImpulse = function () {
        if (equal(this.A.im + this.B.im, 0)) {
            this.infiniteMassCorrection();
            return;
        }
        for (var i = 0; i < this.contacts.length; i++) {
            var ra = subv(this.contacts[i], this.A.position);
            var rb = subv(this.contacts[i], this.B.position);
            var rv = addv(this.B.velocity, crossv(this.B.angularVelocity, rb));
            rv = subv(rv, subv(this.A.velocity, crossv(this.A.angularVelocity, ra)));
            var contactVel = dotv(rv, this.normal);
            if (contactVel > 0)
                return;

            var raCrossN = crossv(ra, this.normal);
            var rbCrossN = crossv(rb, this.normal);
            var invMassSum = this.A.im + this.B.im + Math.pow(raCrossN, 2) * this.A.ii + Math.pow(rbCrossN, 2) * this.B.ii;

            var j = -(1.0 + this.e) * contactVel;
            j /= invMassSum;
            j /= parseFloat(this.contacts.length);
            var impulse = scalev(this.normal, j);
            this.A.applyImpulse(scalev(impulse, -1), ra);
            this.B.applyImpulse(scalev(impulse, 1), rb);

            //friction
            rv = addv(this.B.velocity, crossv(this.B.angularVelocity, rb));
            rv = subv(rv, subv(this.A.velocity, crossv(this.A.angularVelocity, ra)));
            var t = subv(rv, scalev(this.normal, dotv(rv, this.normal)));
            t.normalize();
            var jt = -dotv(rv, t);
            jt /= invMassSum;
            jt /= parseFloat(this.contacts.length);

            if (equal(jt, 0.0))
                return;

            var tangentImpulse = new vector2(0, 0);
            if (Math.abs(jt) < j * this.sf) {
                tangentImpulse = scalev(t, jt);
            }
            else {
                tangentImpulse = scalev(t, -j * this.df);
            }
            this.A.applyImpulse(scalev(tangentImpulse, -1), ra);
            this.B.applyImpulse(tangentImpulse, rb);
        }
    }
    this.positionalCorrection = function () {
        var k_slop = 0.05, percent = 0.4;
        var correction = scalev(this.normal, (Math.max(this.penetration - k_slop, 0.0) / (this.A.im + this.B.im)) * percent);
        this.A.position.subt(scalev(correction, this.A.im));
        this.B.position.add(scalev(correction, this.B.im));
    }
    this.infiniteMassCorrection = function () {
        this.A.velocity.x = this.A.velocity.y = this.B.velocity.x = this.B.velocity.y = 0;
    }
    this.clear = function () {
        this.contacts.splice(0, this.contacts.length);
    }
}