const clearModule = require('clear-module');
const fs = require('fs');

class DataManager {
    path;

    constructor(path) {
        this.path = path;    
    }

    get() {
        clearModule(this.path);
        return require(this.path);
    }

    exists() {
        return fs.existsSync(this.path);
    }

    set(obj) {
        fs.writeFileSync(this.path, JSON.stringify(
            obj,
            null,
            4
        ));
    }
}

module.exports = { DataManager };