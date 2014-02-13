Dispatch = new Array(new Array(), new Array());

function ctc(m, a, b) {
    var A = a.shape, B = b.shape;

    var normal = subv(b.position, a.position);

    var dist_sqr = normal.lengthSquare(), radius = A.radius + B.radius;

    if (dist_sqr >= (radius * radius)) {
        m.clear();
        return;
    }

    var distance = Math.sqrt(dist_sqr);

    //m.contacts.length = 1;

    if (distance == 0.0) {
        m.penetration = A.radius;
        m.normal = new vector2(1, 0);
        m.contacts[0] = a.position.clone();
    }
    else {
        m.penetration = radius - distance;
        m.normal = scalev(normal, (1.0 / distance));
        m.contacts[0] = addv(scalev(m.normal, A.radius), a.position);
    }
}

function ctp(m, a, b) {
    var A = a.shape;
    var B = b.shape;
    //m.contacts.length = 0;
    m.clear();
    var center = a.position.clone();
    center = B.u.transpose().transform(subv(center, b.position));

    var separation = -FLT_MAX, faceNormal = 0;
    for (var i = 0; i < B.m_vertices.length; i++) {
        var s = dotv(B.m_normals[i], subv(center, B.m_vertices[i]));

        if (s > A.radius)
            return;

        if (s > separation) {
            separation = s;
            faceNormal = i;
        }
    }
    var v1 = B.m_vertices[faceNormal].clone();
    var i2 = faceNormal + 1 < B.m_vertices.length ? faceNormal + 1 : 0;
    var v2 = B.m_vertices[i2].clone();


    if (separation < EPSILON) {
        //m.contacts.length = 1;
        m.normal = B.u.transform(B.m_normals[faceNormal]).scale(-1.0);
        m.contacts[0] = scalev(m.normal, A.radius).add(a.position);
        m.penetration = A.radius;
        return;
    }

    var dot1 = dotv(subv(center, v1), subv(v2, v1)), dot2 = dotv(subv(center, v2), subv(v1, v2));

    if (dot1 <= 0.0) {
        if (distanceSquare(center, v1) > A.radius * A.radius) {
            return;
        }
        //m.contacts.length = 1;

        var n = subv(v1, center);
        n = B.u.transform(n);
        n.normalize();
        m.normal = n;
        v1 = addv(B.u.transform(v1), b.position);
        m.contacts[0] = v1;
    }
    else if (dot2 <= 0.0) {
        if (distanceSquare(center, v2) > A.radius * A.radius) {
            return;
        }
        //m.contacts.length = 1;

        var n = subv(v2, center);
        v2 = addv(B.u.transform(v2), b.position);
        m.contacts[0] = v2;
        n = B.u.transform(n);
        n.normalize();
        m.normal = n;
    }
    else {
        var n = B.m_normals[faceNormal];
        if (dotv(subv(center, v1), n) > A.radius) {
            return;
        }
        n = B.u.transform(n);
        m.normal = scalev(n, -1.0);
        m.contacts[0] = addv(scalev(m.normal, A.radius), a.position);
        //m.contacts.length = 1;
    }
}

function ptc(m, a, b) {
    ctp(m, b, a);
    m.normal.scale(-1.0);
}

function findAxisLeastPenetration(faceIndex, A, B) {
    var bestDistance = -FLT_MAX;
    var bestIndex;
    for (var i = 0; i < A.m_vertexCount; ++i) {
        var n = A.m_normals[i];
        var nw = A.u.transform(n);
        var buT = B.u.transpose();
        n = buT.transform(nw);
        var s = B.getSupport(scalev(n, -1.0));
        var v = A.m_vertices[i].clone();
        v = addv(A.u.transform(v), A.body.position);
        v.subt(B.body.position);
        v = buT.tranform(v);

        var d = dotv(n, subv(s, v));

        if (d > bestDistance) {
            bestDistance = d;
            bestIndex = i;
        }
    }

    faceIndex = bestIndex;
    return { "bestDistance": bestDistance, "faceIndex": bestIndex }
}

function findIncidentFace(v, RefPoly, IncPoly, referenceIndex) {
    var referenceNormal = RefPoly.m_normals[referenceIndex].clone();
    referenceNormal = RefPoly.u.transform(referenceNormal); // To world space
    referenceNormal = IncPoly.u.Transpose().transform(referenceNormal); // To incident's model space
    var incidentFace = 0;
    var minDot = FLT_MAX;
    for (var i = 0; i < IncPoly.m_vertices.length; i++) {
        var dot = dotv(referenceNormal, IncPoly.m_normals[i]);
        if (dot < minDot) {
            minDot = dot;
            incidentFace = i;
        }
    }
    v[0] = addv(IncPoly.u.transform(IncPoly.m_vertices[incidentFace]), IncPoly.body.position);
    incidentFace = incidentFace + 1 >= IncPoly.m_vertices.length ? 0 : incidentFace + 1;
    v[1] = addv(IncPoly.u.transform(IncPoly.m_vertices[incidentFace]), IncPoly.body.position);
}

function clip(n, c, face) {
    var sp = 0;
    var out = new Array(face[0], face[1]);
    var d1 = Dot(n, face[0]) - c;
    var d2 = Dot(n, face[1]) - c;

    if (d1 <= 0.0) out[sp++] = face[0];
    if (d2 <= 0.0) out[sp++] = face[1];

    if (d1 * d2 < 0.0) // less than to ignore -0.0
    {
        var alpha = d1 / (d1 - d2);
        out[sp] = addv(face[0], scalev(subv(face[1], face[0]), alpha));
        sp++;
    }

    face[0] = out[0];
    face[1] = out[1];
    return sp;
}

function ptp(m, a, b) {
    var A = (a.shape);
    var B = (b.shape);
    m.contacts.splice(0, m.contacts.length);

    var faceA;
    var temp = findAxisLeastPenetration(faceA, A, B);
    faceA = temp.faceDistance;
    var penetrationA = temp.bestDistance;
    if (penetrationA >= 0.0)
        return;

    var faceB;
    var temp2 = findAxisLeastPenetration(faceB, B, A);
    faceB = temp2.faceDistance;
    var penetrationB = temp2.bestDistance;
    if (penetrationB >= 0.0)
        return;

    var referenceIndex;
    var flip; // Always point from a to b

    var RefPoly; // Reference
    var IncPoly; // Incident

    if (biasGreaterThan(penetrationA, penetrationB)) {
        RefPoly = A;
        IncPoly = B;
        referenceIndex = faceA;
        flip = false;
    }

    else {
        RefPoly = B;
        IncPoly = A;
        referenceIndex = faceB;
        flip = true;
    }
    var incidentFace = new Array();
    findIncidentFace(incidentFace, RefPoly, IncPoly, referenceIndex);
    var v1 = RefPoly.m_vertices[referenceIndex];
    referenceIndex = referenceIndex + 1 == RefPoly.m_vertices.length ? 0 : referenceIndex + 1;
    var v2 = RefPoly.m_vertices[referenceIndex];
    v1 = RefPoly.u.transform(v1).add(RefPoly.body.position);
    v2 = RefPoly.u.transform(v2).add(RefPoly.body.position);
    var sidePlaneNormal = subv(v2, v1);
    sidePlaneNormal.normalize();
    var refFaceNormal = new vector2(sidePlaneNormal.y, -sidePlaneNormal.x);
    var refC = dotv(refFaceNormal, v1);
    var negSide = -dotv(sidePlaneNormal, v1);
    var posSide = dotv(sidePlaneNormal, v2);
    if (clip(scalev(sidePlaneNormal, -1.0), negSide, incidentFace) < 2)
        return;
    if (clip(sidePlaneNormal, posSide, incidentFace) < 2)
        m.normal = flip ? scalev(refFaceNormal, -1.0) : refFaceNormal;

    var cp = 0;
    var separation = dotv(refFaceNormal, incidentFace[0]) - refC;
    if (separation <= 0.0) {
        m.contacts[cp] = incidentFace[0];
        m.penetration = -separation;
        ++cp;
    }
    else {
        m.penetration = 0;
    }

    separation = dotv(refFaceNormal, incidentFace[1]) - refC;
    if (separation <= 0.0) {
        m.contacts[cp] = incidentFace[1];

        m.penetration += -separation;
        ++cp;
        m.penetration /= cp;
    }
    //m.contacts.length = cp;
}

Dispatch[0][0] = ctc;
Dispatch[0][1] = ctp;
Dispatch[1][0] = ptc;
Dispatch[1][1] = ptp;
