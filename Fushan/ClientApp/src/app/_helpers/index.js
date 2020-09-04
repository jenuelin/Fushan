var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    Object.defineProperty(o, k2, { enumerable: true, get: function() { return m[k]; } });
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __exportStar = (this && this.__exportStar) || function(m, exports) {
    for (var p in m) if (p !== "default" && !Object.prototype.hasOwnProperty.call(exports, p)) __createBinding(exports, m, p);
};
define(["require", "exports", "./error.interceptor", "./jwt.interceptor", "./fake-backend"], function (require, exports, error_interceptor_1, jwt_interceptor_1, fake_backend_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    //export * from './auth.guard';
    __exportStar(error_interceptor_1, exports);
    __exportStar(jwt_interceptor_1, exports);
    __exportStar(fake_backend_1, exports);
});
//# sourceMappingURL=index.js.map