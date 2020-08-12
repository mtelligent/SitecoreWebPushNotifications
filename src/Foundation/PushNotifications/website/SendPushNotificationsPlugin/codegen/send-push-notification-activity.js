"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
exports.SendPushNotificationActivity = void 0;
var ma_core_1 = require("@sitecore/ma-core");
var SendPushNotificationActivity = (function (_super) {
    __extends(SendPushNotificationActivity, _super);
    function SendPushNotificationActivity() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SendPushNotificationActivity.prototype.getVisual = function () {
        var subTitle = this.isDefined ? this.editorParams.title : '';
        var cssClass = this.isDefined ? '' : 'undefined';
        return "\n      <div class=\"marketing-action " + cssClass + "\">\n        <span class=\"icon\">\n          <img src=\"/~/icon/officewhite/32x32/message.png\" />\n        </span>\n        <p class=\"text with-subtitle\" title=\"Send Web Push Notification\">\n          Send Web Push Notification\n          <small class=\"subtitle\" title=\"" + subTitle + "\">" + subTitle + "</small>\n        </p>\n      </div>\n    ";
    };
    Object.defineProperty(SendPushNotificationActivity.prototype, "isDefined", {
        get: function () {
            return Boolean(this.editorParams.title);
        },
        enumerable: false,
        configurable: true
    });
    return SendPushNotificationActivity;
}(ma_core_1.SingleItem));
exports.SendPushNotificationActivity = SendPushNotificationActivity;
//# sourceMappingURL=send-push-notification-activity.js.map