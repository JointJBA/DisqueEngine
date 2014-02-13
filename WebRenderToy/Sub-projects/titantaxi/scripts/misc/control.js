function control() {
    this.leftEvents = new Array();
    this.rightEvents = new Array();
    this.upEvents = new Array();
    this.downEvents = new Array();
    this.leftReleaseEvents = new Array();
    this.rightReleaseEvents = new Array();
    this.upReleaseEvents = new Array();
    this.downReleaseEvents = new Array();
    this.fireUp = function () {
        for (var i = 0; i < this.upEvents.length; i++) {
            this.upEvents[i]();
        }
    }
    this.fireDown = function () {
        for (var i = 0; i < this.downEvents.length; i++) {
            this.downEvents[i]();
        }
    }
    this.fireRight = function () {
        for (var i = 0; i < this.rightEvents.length; i++) {
            this.rightEvents[i]();
        }
    }
    this.fireLeft = function () {
        for (var i = 0; i < this.leftEvents.length; i++) {
            this.leftEvents[i]();
        }
    }
    this.fireReleaseUp = function () {
        for (var i = 0; i < this.upReleaseEvents.length; i++) {
            this.upReleaseEvents[i]();
        }
    }
    this.fireReleaseDown = function () {
        for (var i = 0; i < this.downReleaseEvents.length; i++) {
            this.downReleaseEvents[i]();
        }
    }
    this.fireReleaseRight = function () {
        for (var i = 0; i < this.rightReleaseEvents.length; i++) {
            this.rightReleaseEvents[i]();
        }
    }
    this.fireReleaseLeft = function () {
        for (var i = 0; i < this.leftReleaseEvents.length; i++) {
            this.leftReleaseEvents[i]();
        }
    }
}