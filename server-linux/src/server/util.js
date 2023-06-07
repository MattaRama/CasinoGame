function send(ws, json) {
    ws.write(JSON.stringify(json, null, 4));
}

function hasParams(obj, props) {
    for (var i = 0; i < props.length; i++) {
        var pointer = obj;
        var split = props[i].split('.');
        for (var j = 0; j < split.length; j++) {
            if (pointer[split[j]] === undefined) {
                return false;
            }
            pointer = obj[split[j]];
        }
    }

    return true;
}

module.exports = { send, hasParams };