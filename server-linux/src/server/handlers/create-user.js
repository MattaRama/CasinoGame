const { PACKET_TYPE, PIN_LENGTH } = require('../constants.json');
const { ServerUser } = require('../user');
const { hasParams, send } = require('../util');

module.exports = {
    disabled: false,
    type: PACKET_TYPE.USER.CREATE,
    handle(req, ws) {
        if (!hasParams(req, ['firstName', 'lastName', 'pin'])) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PARAMETERS,
                recv: req
            });
            return;
        }

        // type checking
        if (typeof req.firstName !== 'string' ||
            typeof req.lastName !== 'string' ||
            typeof req.pin !== 'string'
        ) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PARAMETERS,
                recv: req
            });
            return;
        }

        // pin checking
        if (ServerUser.exists(req.pin)) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.PIN_IN_USE,
                recv: req
            });
            return;
        }

        // check for valid pin
        if (req.pin.length !== PIN_LENGTH) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PIN_FORMAT,
                recv: req
            });
            return;
        }
        for (var i = 0; i < req.pin.length; i++) {
            if (!("1234567890".includes(req.pin.charAt(i)))) {
                send(ws, {
                    type: PACKET_TYPE.ERRORS.INVALID_PIN_FORMAT,
                    recv: req
                });
                return;
            }
        }

        // setup account
        const user = ServerUser.createUser(req.firstName, req.lastName, req.pin);
        const name = user.getName();

        send(ws, {
            type: PACKET_TYPE.USER.CREATE,
            pin: user.getPin(),
            firstName: name.firstName,
            lastName: name.lastName,
            recv: req
        });
    }
};