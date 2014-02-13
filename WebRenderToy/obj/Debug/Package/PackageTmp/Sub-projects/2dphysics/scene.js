function integrateForces(b, dt) {
    if (b.im == 0.0)
        return;
    b.velocity.add(scalev(addv(scalev(b.force, b.im), gravity), (dt / 2.0)));
    b.angularVelocity += b.torque * b.ii * (dt / 2.0);
}

function integrateVelocity(b, dt) {
    if (b.im == 0.0)
        return;
    b.position.add(scalev(b.velocity, dt));
    b.orient += b.angularVelocity * dt;
    b.setOrient(b.orient);
    integrateForces(b, dt);
}

function scene(dt, iter) {
    this.m_dt = dt;
    this.m_iterations = iter;
    this.bodies = new Array();
    this.contacts = new Array();
    this.add = function (shape, x, y) {
        var b = new body(shape, x, y);
        this.bodies.push(b);
        return b;
    }
    this.clear = function () {
        this.bodies.splice(0, this.bodies.length);
        this.contacts.splice(0, this.contacts.length);
    }
    this.step = function () {
//        this.contacts.splice(0, this.contacts.length);
//        for (var i = 0; i < this.bodies.length; i++) {
//            var A = this.bodies[i];
//            for (var j = i + 1; j < this.bodies.length; j++) {
//                var B = this.bodies[j];
//                if (A.im == 0 && B.im == 0) {
//                    continue;
//                }
//                var m = new manifold(A, B);
//                m.solve();
//                if (m.contacts.length > 0) {
//                    this.contacts.push(m);
//                }
//            }
//        }

        for (var i = 0; i < this.bodies.length; i++) {
            integrateForces(this.bodies[i], this.m_dt);
        }

//        for (var i = 0; i < this.contacts.length; i++) {
//            this.contacts[i].initialize();
//        }

//        for (var j = 0; j < this.m_iterations; j++) {
//            for (var i = 0; i < this.contacts.length; i++) {
//                this.contacts[i].applyImpulse();
//            }
//        }
        for (var i = 0; i < this.bodies.length; i++) {
            integrateVelocity(this.bodies[i], this.m_dt);
        }

//        for (var i = 0; i < this.contacts.length; i++) {
//            this.contacts[i].positionalCorrection();
//        }

        for (var i = 0; i < this.bodies.length; i++) {
            this.bodies[i].force.x = this.bodies[i].force.y = 0;
            this.bodies[i].torque = 0;
        }
    }
}