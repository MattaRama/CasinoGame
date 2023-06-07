const Ws = require('ws');
const Net = require('net');
const fs = require('fs');
const { send, hasParams } = require('./util');
const { PACKET_TYPE, HANDLERS_PATH } = require('./constants.json')

// load handlers
const handlers = {};
const handlerPaths = fs.readdirSync(HANDLERS_PATH).filter(
    path => path.endsWith('.js')
);
for (var i = 0; i < handlerPaths.length; i++) {
    const handler = require(`${HANDLERS_PATH}${handlerPaths[i]}`);

    if (handler['type'] == null || handler['handle'] == null) {
        console.log(`[Handler Init] Failed to init "${handlerPaths[i]}"; missing handle() or type`);
        continue;
    }

    if (handler['disabled'] === true) {
        console.log(`[Handler Init] Skipped initialization of "${handlerPaths[i]}" (${handler['type']}); disabled`);
        continue;
    }

    console.log(`[Handler Init] Initialized "${handlerPaths[i]}" (${handler['type']})`);
    handlers[handler['type']] = handler;
}

// webserver setup
/*const server = new Ws.Server({
    port: 6978
});*/
const server = Net.createServer((soc) => {
    console.log(`Connection from ${soc.remoteAddress}`);
    soc.on('error', (err) => {
        console.log(`ERROR: ${err}`);
    });
    soc.on('ready', () => {
        console.log('ready');
    });
    soc.on('data', (buf) => {
        console.log(`got: ${buf.toString().trim()}`);

        // parse json
        var req;
        try {
            req = JSON.parse(buf.toString().trim());
        } catch {
            console.log('INVALID JSON');
            send(soc, {
                type: PACKET_TYPE.ERRORS.INVALID_JSON,
                recv: buf.toString().trim()
            });
            return;
        }

        // check packet for type field
        if (!hasParams(req, [ 'type' ])) {
            console.log('NO PACKET TYPE');
            send(soc, {
                type: PACKET_TYPE.ERRORS.NO_PACKET_TYPE,
                recv: req
            });
            return;
        }

        // handle request
        if (typeof req['type'] !== 'string') {
            console.log('INVALID PACKET TYPE');
            send(soc, {
                type: PACKET_TYPE.ERRORS.INVALID_PACKET_TYPE,
                recv: req
            });
            return;
        }

        const handler = handlers[req['type']];
        if (handler == null) {
            console.log('INVALID PACKET TYPE (2)');
            send(soc, {
                type: PACKET_TYPE.ERRORS.INVALID_PACKET_TYPE,
                recv: req
            });
            return;
        }

        console.log(req);

        handler.handle(req, soc);
    });
});

server.listen(6978);

/*server.on('listening', () => {
    console.log(`[Websocket] Server begun listening at unix epoch "${Date.now()}"`);
});

server.on('connection', (ws, im) => {
    ws.on('message', (rawData) => {
        // parse json
        var req;
        try {
            req = JSON.parse(rawData.toString().trim());
        } catch {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_JSON,
                recv: rawData.toString().trim()
            });
            return;
        }

        // check packet for type field
        if (!hasParams(req, [ 'type' ])) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.NO_PACKET_TYPE,
                recv: req
            });
            return;
        }

        // handle request
        if (typeof req['type'] !== 'string') {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PACKET_TYPE,
                recv: req
            });
            return;
        }

        const handler = handlers[req['type']];
        if (handler == null) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PACKET_TYPE,
                recv: req
            });
            return;
        }

        handler.handle(req, ws);
    });
});*/