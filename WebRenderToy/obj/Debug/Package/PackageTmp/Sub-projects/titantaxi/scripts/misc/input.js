function input() {
    var isMobile = navigator.userAgent.match(/(iPhone|iPod|iPad|Android)/);
    if (!isMobile) {
        $(document).keypress(function (e) {
            if (e.which == 105) {
                shipControl.fireUp();
            }
            if (e.which == 107) {
                shipControl.fireDown();
            }
            if (e.which == 106) {
                shipControl.fireLeft();
            }
            if (e.which == 108) {
                shipControl.fireRight();
            }
        });
        $(document).keyup(function (e) {
            if (e.which == 73) {
                shipControl.fireReleaseUp();
            }
            if (e.which == 75) {
                shipControl.fireReleaseDown();
            }
            if (e.which == 74) {
                shipControl.fireReleaseLeft();
            }
            if (e.which == 76) {
                shipControl.fireReleaseRight();
            }
        });
    }
    else {
    }
}