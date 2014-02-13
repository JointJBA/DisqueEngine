function Canvas() {
}

Canvas.drawPoint = function (p, c) {
    var contxt = MainCanvas;
    contxt.fillStyle = 'rgb(' + c.r + ',' + c.g + ',' + c.b + ')';
    contxt.fillRect(p.x, p.y, 1, 1);
}

Canvas.drawLine = function (a, b, c) {
    var contxt = MainCanvas;
    contxt.strokeStyle = 'rgb(' + c.r + ',' + c.g + ',' + c.b + ')';
    contxt.beginPath();
    contxt.moveTo(a.x, a.y);
    contxt.lineTo(b.x, b.y);
    contxt.stroke();
}

Canvas.clear = function () {
    MainCanvas.clearRect(0, 0, Width, Height);
}

function world() {
    this.objs = new list();
    this.draw = function () {
        Canvas.clear();
        for (var i = 0; i < this.objs.length(); i++)
            this.objs.get(i).draw();
    }
}