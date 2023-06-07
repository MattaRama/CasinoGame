const { PACKET_TYPE, PIN_LENGTH } = require('../constants.json');
const { ServerUser } = require('../user');
const { hasParams, send } = require('../util');

module.exports = {
    type: PACKET_TYPE.USER.TRANSACTION,
    disabled: false,
    handle(req, ws) {
        if (!hasParams(req, ['pin', 'quantity', 'physicalCurrency'])) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PARAMETERS,
                recv: req
            });
            return;
        }

        if (typeof req.quantity !== 'number' || typeof req.physicalCurrency !== 'boolean') {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PARAMETERS,
                recv: req
            });
            return;
        }

        // check for valid pin
        if (!ServerUser.exists(req.pin)) {
            send(ws, {
                type: PACKET_TYPE.ERRORS.INVALID_PIN,
                recv: req
            });
            return;
        }

        // check for valid amount
        const user = new ServerUser(req.pin);
        const isWithdrawal = req.quantity < 0;
        if (isWithdrawal ? (req.quantity > user.getTokens()) : false) { // TODO: fix this dumpsterfire
            send(ws, {
                type: PACKET_TYPE.ERRORS.USER.OVERDRAFT,
                recv: req
            });
            return;
        }

        // transaction
        var quantity = Math.abs(req.quantity);
        if (isWithdrawal) {
            if (req.physicalCurrency) {
                user.withdraw(quantity);
            } else {
                user.loss(quantity);
            }
        } else {
            if (req.physicalCurrency) {
                user.deposit(quantity);
            } else {
                user.win(quantity);
            }
        }
        //(isWithdrawal ? (req.physicalCurrency ? user.withdraw : user.loss) : (req.physicalCurrency ? user.deposit : user.win))(Math.abs(req.quantity));
        send(ws, {
            type: PACKET_TYPE.USER.TRANSACTION,
            recv: req
        });
    }
};