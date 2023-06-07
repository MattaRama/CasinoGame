const { PACKET_TYPE, PIN_LENGTH } = require('../constants.json');
const { ServerUser } = require('../user');
const { hasParams, send } = require('../util');

module.exports = {
    type: PACKET_TYPE.USER.GET_DATA,
    disabled: false,
    handle(req, ws) {
        if (!hasParams(req, ['pin'])) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PARAMETERS,
                recv: req
            });
            return;
        }

        if (typeof req.pin !== 'string' || !ServerUser.exists(req.pin)) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PIN,
                recv: req
            });
            return;
        }

        const user = new ServerUser(req.pin);
        send(ws, {
            type: PACKET_TYPE.USER.GET_DATA,
            data: user.get(),
            recv: req
        });
    }
};