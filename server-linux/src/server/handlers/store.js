const { DataManager } = require('../../datamanager');
const { PACKET_TYPE, PIN_LENGTH, DATABASE_PATH } = require('../constants.json');
const { ServerUser } = require('../user');
const { hasParams, send } = require('../util');

module.exports = {
    type: PACKET_TYPE.DATABASE.STORE,
    handle: (req, ws) => {
        if (!hasParams(req, ['key', 'value'])) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PARAMETERS,
                recv: req
            });
            return;
        }

        if (req.key == null || req.value == null) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PARAMETERS,
                recv: req
            });
            return;
        }

        var dbman = new DataManager(`${DATABASE_PATH}store.json`);
        var obj = dbman.get();
        obj[req.key] = req.value;
        dbman.set(obj);

        send(ws, {
            type: PACKET_TYPE.DATABASE.STORE,
            recv: req
        });
    }
};