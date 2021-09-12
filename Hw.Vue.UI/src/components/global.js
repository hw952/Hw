export default {
    wsUrl: "wss://localhost:5001/",
    getWsUrl: function() {
        return this.wsUrl + `?sid=${new Date().getTime()}`;
    },
    createWebSocket() {
        let i = 0;
        var ws = new WebSocket(`wss://localhost:5001/?sid=${new Date().getTime()}`);
        ws.onopen = function() {
            console.log("open");
            ws.send("hello");
        }
        ws.onmessage = function(e) {
            console.log(e.data);
            i++;
            console.log(i);
        };
        ws.onclose = function() {
            console.log("close");
        }
        ws.onerror = function(e) {
            console.log(e);
        }
    }
}