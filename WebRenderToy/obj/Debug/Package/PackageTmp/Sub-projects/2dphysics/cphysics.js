EPSILON = 0.0001;
dt = 0.01666666754;
gscale = 5.0;
gravity = new vector2(0, 10.0 * gscale);

function equal(a, b) {
    return Math.abs(a - b) <= EPSILON;
}

function testDraw(ctx, b) {
    if (b.shape.getType() == 0) {
        ctx.save();
        ctx.translate(b.position.x, b.position.y);
        ctx.rotate(b.orient);
        ctx.translate(-b.position.x, -b.position.y);
        ctx.beginPath();
        ctx.arc(b.position.x, b.position.y, b.shape.radius, 0, Math.PI * 2,
                true);
        ctx.closePath();
        ctx.stroke();
        ctx.beginPath();
        ctx.moveTo(b.position.x, b.position.y);
        ctx.lineTo(b.position.x, b.position.y - b.shape.radius);
        ctx.closePath();
        ctx.stroke();
        ctx.restore();
    }
    else {
        ctx.save();
        ctx.beginPath();
        var be = addv(b.position, b.shape.u.transform(b.shape.m_vertices[0]));
        ctx.moveTo(be.x, be.y);
        for (var i = 1; i < b.shape.m_vertices.length; i++) {
            be = addv(b.position, b.shape.u.transform(b.shape.m_vertices[i]));
            ctx.lineTo(be.x, be.y);
        }
        ctx.closePath();
        ctx.stroke();
        ctx.restore();
    }
}

function drawContact(ctx, p) {
        ctx.beginPath();
        ctx.arc(p.x, p.y, 2.0, 0, Math.PI * 2, true);
        ctx.closePath();
        ctx.fill();
        ctx.stroke();
}

//ctx.translate(b.position.x, b.position.y);
//ctx.rotate(b.orient);
//ctx.translate(-b.position.x, -b.position.y);
//ctx.beginPath();
//ctx.arc(b.position.x, b.position.y, b.shape.radius, 0, Math.PI * 2,
//                true);
//ctx.closePath();
//ctx.stroke();
//ctx.beginPath();
//ctx.moveTo(b.position.x, b.position.y);
//ctx.lineTo(b.position.x, b.position.y - b.shape.radius);
//ctx.closePath();
//ctx.stroke();