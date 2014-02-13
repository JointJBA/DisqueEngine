function world(s) {
    this._ship = s;
    this.update = function () {
        for (var i = 0; i < game._sprites.length; i++) {
            if (game._sprites[i].type == "projectile" && game._sprites[i].good) {
                var r = game._sprites;
                var temp = game._sprites[i].center, radius = game._sprites[i].radius;
                if (distance(temp, game._planet.center) < (radius + game._planet.radius)) {
                    game._planet.life--;
                    r.splice(r.indexOf(game._sprites[i]), 1);
                }
                if (temp.x - radius < 0) {
                    r.splice(r.indexOf(game._sprites[i]), 1);
                }
                if (temp.x + radius > WIDTH) {
                    r.splice(r.indexOf(game._sprites[i]), 1);
                }
                if (temp.y - radius < 0) {
                    r.splice(r.indexOf(game._sprites[i]), 1);
                }
                if (temp.y + radius > HEIGHT) {
                    r.splice(r.indexOf(game._sprites[i]), 1);
                }
            }
            else if (game._sprites[i].type == "projectile" && !game._sprites[i].good) {
                var r = game._sprites;
                var temp = game._sprites[i].center, radius = game._sprites[i].radius;
                if (distance(temp, game._ast.center) < (radius + game._ast.radius)) {
                    if (r[i].stype == "bullet") {
                        game._ast.life--;
                    }
                    else if (r[i].stype == "rocket") {
                        game._ast.life -= 2;
                    }
                    r.splice(r.indexOf(game._sprites[i]), 1);
                }
                if (temp.x - radius < 0) {
                    r.splice(r.indexOf(game._sprites[i]), 1);
                }
                if (temp.x + radius > WIDTH) {
                    r.splice(r.indexOf(game._sprites[i]), 1);
                }
                if (temp.y - radius < 0) {
                    r.splice(r.indexOf(game._sprites[i]), 1);
                }
                if (temp.y + radius > HEIGHT) {
                    r.splice(r.indexOf(game._sprites[i]), 1);
                }
            }
            if (distance(game._planet.center, game._ast.center) < (game._planet.radius + game._ast.radius)) {
                game._ast.life = 0;
            }
        }
        var temp = this._ship.center, radius = this._ship.radius;
        if (temp.x - radius < 0) {
            this._ship.center.x += radius - temp.x;
        }
        if (temp.x + radius > WIDTH) {
            this._ship.center.x -= ((temp.x + radius) - WIDTH);
        }
        if (temp.y - radius < 0) {
            this._ship.center.y += radius - temp.y;
        }
        if (temp.y + radius > HEIGHT) {
            this._ship.center.y -= ((temp.y + radius) - HEIGHT);
        }

    }
}