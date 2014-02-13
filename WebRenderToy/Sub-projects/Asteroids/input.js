var keysDown = new Array();
ESC = 27, W = 87, CLICK = 0;
function input() {
    $(document).bind('keydown', function (e) {
        keysDown[e.which] = true;
    });
    $(document).bind('keyup', function (e) {
        keysDown[e.which] = false;
    });
    $(document).bind('mousedown', function (e) {
        keysDown[0] = true;
    });
    $(document).bind('mouseup', function (e) {
        keysDown[0] = false;
    });
}