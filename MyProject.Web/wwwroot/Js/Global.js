var apiServer = "http://localhost:8030";

//js构造函数
function Elem(id) {
    this.elem = document.getElementById(id);
}

Elem.prototype.html = function (val) {
    var elem = this.elem;
    if (val) {
        elem.innerHTML = val;
        return this;    // 链式编程
    } else {
        return elem.innerHTML;
    }
};

Elem.prototype.on = function (type, fn) {
    var elem = this.elem;
    elem.addEventListener(type, fn);
};