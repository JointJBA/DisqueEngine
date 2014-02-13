function list() {
    var current = 0;
    this.conts = [];
    this.add = function (o) {
        this.conts[current] = o;
        current++;
    }
    this.get = function(i)
    {
        return this.conts[i];
    }
    this.length = function () {
        return this.conts.length;
    }
}