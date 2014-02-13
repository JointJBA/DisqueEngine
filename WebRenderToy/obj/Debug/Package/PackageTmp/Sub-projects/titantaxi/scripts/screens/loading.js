function showLoadingFull() {
    var grd = ctx.createRadialGradient(width / 2.0, height / 2.0, 0, width / 2.0, height / 2.0, width);
    grd.addColorStop(0, "rgb(46,52,61)");
    grd.addColorStop(1, "rgb(15,18,20)");
    ctx.fillStyle = grd;
    ctx.fillRect(0, 0, width, height);
    ctx.fillStyle = "white";
    ctx.font = "bold 16px " + FONT;
    ctx.fillText("Loading some stuff...", width / 2.65, (height / 2.0) - 8);
}

function showLevelLoadingFull() {
}