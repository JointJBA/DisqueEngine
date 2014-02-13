function color(_r, _g, _b) {
    this.r = _r;
    this.g = _g;
    this.b = _b;
    this.toString = function () {
        return "[" + this.r + ", " + this.g + ", " + this.b + "]";
    }
}