const { DataManager } = require('../../datamanager');
const { PACKET_TYPE, PIN_LENGTH, DATABASE_PATH } = require('../constants.json');
const { ServerUser } = require('../user');
const { hasParams, send } = require('../util');

module.exports = {
    type: PACKET_TYPE.DATABASE.GET,
    handle: (req, ws) => {
        if (!hasParams(req, ['key'])) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PARAMETERS,
                recv: req
            });
            return;
        }

        if (req.key == null) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PARAMETERS,
                recv: req
            });
            return;
        }

        var dbman = new DataManager(`${DATABASE_PATH}store.json`);
        var obj = dbman.get();

        send(ws, {
            type: PACKET_TYPE.DATABASE.GET,
            recv: req,
            key: req.key,
            value: obj[req.key]
        });
    }
};