window.addEventListener("load", function () {
var ws;

try {
    ws = new WebSocket("ws://localhost:49694/Handler1.ashx");

    ws.onerror = function () {
        console.log('Verbindung fehlgeschlagen');
    };

    ws.onopen = function () {
        console.log("verbunden, readyState: " + this.readyState);
    };

    ws.onmessage = function (e) {

    };

    ws.onclose = function () {
        console.log("Verbindung getrennt, readyState: " + this.readyState);
    };
}
catch (e) {

}

});