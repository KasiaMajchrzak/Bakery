"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Connection = /** @class */ (function () {
    function Connection() {
        this.url = 'http://localhost:5000/api/';
    }
    Connection.prototype.GetUrl = function () {
        return this.url;
    };
    return Connection;
}());
exports.Connection = Connection;
//# sourceMappingURL=Connection.js.map