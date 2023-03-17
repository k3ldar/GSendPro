// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var gSend = (function () {
    let _settings = {
        connState:'',
        socket: '',
        serverIp: '',
        updInterval: '',
        conInterval: '',
        sendUpdateRequest: false,
        isConnected: false,
        statusUpdateCode: '',
    }

    let that = {
        init: function (settings) {
            _settings = settings;

            if (_settings.isConnected == undefined)
                _settings.isConnected = false;


            $(document).ready(function () {
                that.updInterval = setInterval(function () {
                    if (_settings.isConnected == false) {
                        that.connectToServer();
                    }
                    else if (_settings.sendUpdateRequest == true) {
                        _settings.sendUpdateRequest = false;
                        _settings.socket.send(_settings.statusUpdateCode);
                    }
                }, 200);
            });
        },

        connectToMachine: function (id) {
            var machine = document.getElementById("mConn" + id);

            if (machine.classList.contains('bg-secondary')) {
                _settings.socket.send("mConnect:" + id);
            }
        },

        clearAlarm: function (id) {
            var machine = document.getElementById("mStat" + id);

            if (machine.classList.contains('bg-danger')) {
                _settings.socket.send("mClearAlm:" + id);
            }
        },

        connected: function () {
            _settings.isConnected = true;
            _settings.connState.innerText = "Server Connected";
            _settings.connState.classList.remove("bg-danger");
            _settings.connState.classList.add("bg-success");
        },

        disconnected: function () {
            _settings.isConnected = false;
            _settings.connState.innerText = "Server Not Connected";
            _settings.connState.classList.remove("bg-success");
            _settings.connState.classList.remove("bg-danger");
        },

        updateState: function () {
            if (!_settings.socket) {
                that.disable();
            } else {
                switch (_settings.socket.readyState) {
                    case WebSocket.CLOSED:
                        that.disconnected();
                        break;
                    case WebSocket.CLOSING:
                        that.disconnected();
                        break;
                    case WebSocket.CONNECTING:
                        that.disconnected();
                        break;
                    case WebSocket.OPEN:
                        that.connected();
                        break;
                    default:
                        that.disconnected();
                        break;
                }
            }
        },

        connectToServer: function () {
            _settings.socket = new WebSocket(_settings.serverIp);
            _settings.socket.onopen = function (event) {
                that.updateState();
                _settings.sendUpdateRequest = true;
            };
            _settings.socket.onclose = function (event) {
                that.updateState();
            };
            _settings.socket.onerror = that.updateState;
            _settings.socket.onmessage = function (event) {

                if (event.data != '') {
                    var jsonData = $.parseJSON(event.data);

                    if (jsonData.success && jsonData.request === 'mStatus') {
                        that.updateMachineStatus(jsonData.message);
                    }
                    else {

                    }


                }
                _settings.sendUpdateRequest = true;
            };
        },

        updateMachineStatus: function (arrMachines) {
            let i = 0;

            while (i < arrMachines.length) {
                let mc = document.getElementById("mConn" + arrMachines[i].Id);
                let ms = document.getElementById("mStat" + arrMachines[i].Id);
                let mcpu = document.getElementById("mCpu" + arrMachines[i].Id);

                mc.innerText = arrMachines[i].Connected;
                mcpu.innerText = arrMachines[i].CpuStatus;

                if (arrMachines[i].Connected) {
                    let stateClass = "bg-secondary";
                    let stateText = arrMachines[i].State;

                    if (ms.style.display === "none")
                        ms.style.display = "block";

                    switch (arrMachines[i].State) {
                        case "Undefined":
                            stateText = "Port Open";
                            stateClass = "bg-warning";
                            break;
                        case "Idle":
                            stateClass = "bg-primary";
                            break;
                        case "Run":
                            stateClass = "bg-success";
                            break;
                        case "Jog":
                            stateClass = "bg-success";
                            break;
                        case "Home":
                            stateClass = "bg-success";
                            break;
                        case "Sleep":
                            stateClass = "bg-warning";
                            break;
                        case "Hold":
                            stateClass = "bg-warning";
                            break;
                        case "Alarm":
                            stateClass = "bg-danger";
                            break;
                        case "Door":
                            stateClass = "bg-danger";
                            break;
                        case "Check":
                            stateClass = "bg-danger";
                            break;
                    }

                    ms.innerText = stateText;
                    ms.className = "";
                    ms.classList.add("btn");
                    ms.classList.add(stateClass);
                }
                else {
                    ms.innerText = "";
                    ms.style.display = "none";
                }
                i++;
            }
        },

        htmlEscape: function (str) {
            return str.toString()
                .replace(/&/g, '&amp;')
                .replace(/"/g, '&quot;')
                .replace(/'/g, '&#39;')
                .replace(/</g, '&lt;')
                .replace(/>/g, '&gt;');
        }
    };

    return that;
})();