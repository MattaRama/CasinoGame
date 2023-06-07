const fs = require('fs');
const { DataManager } = require('../datamanager')
const { DATABASE_PATH } = require('./constants.json')

class ServerUser extends DataManager {
    constructor(pin) {
        super(`${DATABASE_PATH}users/${pin}.json`);
    }

    getPin() {
        return this.get().pin;
    }

    getName() {
        const raw = this.get();
        return {
            firstName: raw.firstName,
            lastName: raw.lastName
        };
    }

    getTokens() {
        return this.get().totals.tokens;
    }
    
    // for when a player deposits their physical chips for digital chips
    deposit(quantity) {
        const raw = this.get();
        raw.totals.totalDeposit += quantity;
        raw.totals.tokens += quantity;
        this.set(raw);
        return true;
    }

    // for when a player withdraws their digital chips for physical chips
    withdraw(quantity) {
        const raw = this.get();
        if (quantity > raw.totals.tokens) {
            return false;
        }
        
        raw.totals.tokens -= quantity;
        raw.totals.totalWithdraw += quantity;
        this.set(raw);
        return true;
    }

    // for when a player WINS a bet
    win(quantity) {
        const raw = this.get();
        raw.totals.tokens += quantity;
        raw.totals.totalWon += quantity;
        this.set(raw);
        return true;
    }

    // for when a player LOSES a bet
    // NOTE: this is not negative-protected. A bet should NEVER be larger
    //       than the player's balance 
    loss(quantity) {
        const raw = this.get();
        raw.totals.tokens -= quantity;
        raw.totals.totalLost += quantity;
        this.set(raw);
        return true;
    }

    getTotals() {
        return this.get().totals;
    }

    static exists(pin) {
        return fs.existsSync(`${DATABASE_PATH}users/${pin}.json`);
    }

    static createUser(firstName, lastName, pin) {
        const rawUser = {
            firstName,
            lastName,
            pin,
            totals: {
                tokens: 0,
                totalWon: 0,
                totalLost: 0,
                totalDeposit: 0,
                totalWithdraw: 0
            }
        };

        const user = new ServerUser(pin);
        user.set(rawUser);

        return user;
    }
}

module.exports = { ServerUser };