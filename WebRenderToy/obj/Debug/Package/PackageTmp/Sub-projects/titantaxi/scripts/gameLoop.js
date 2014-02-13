FONT = "Arial";
var gameInterval, mainHost, welcomeScreen, levelScrn, settingsScrn, helpScrn, level, shipControl;
function startGame() {
    startupGame();
}
function startupGame() {
    showLoadingFull();
    var imgload = new imageLoader();
    imgload.start(s2);
}

function s2() {
    var lvlload = new levelLoader();
    lvlload.start(cont);
}

function cont() {
    input();
    shipControl = new control();
    mainHost = new host();
    welcomeScreen = new home(mainHost);
    levelScrn = new level(mainHost);
    settingsScrn = new settingsScreen(mainHost);
    helpScrn = new helpScreen(mainHost);
    mainHost.navigate(welcomeScreen);
    gameInterval = setInterval(function () {
        ctx.clearRect(0, 0, width, height);
        mainHost.update();
        drawBack();
        mainHost.draw();
    }, (1000.0 / 24.0));
}

function drawBack() {
    ctx.drawImage(IMGS["background"], 0, 0);
}
