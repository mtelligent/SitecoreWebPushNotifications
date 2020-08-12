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
var ma_core_1 = require("@sitecore/ma-core");
var SendPushNotificationActivity = /** @class */ (function (_super) {
    __extends(SendPushNotificationActivity, _super);
    function SendPushNotificationActivity() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SendPushNotificationActivity.prototype.getVisual = function () {
        var subTitle = this.isDefined ? 'Count: ' + this.editorParams.count : '';
        var cssClass = this.isDefined ? '' : 'undefined';
        // The CSS class can either be marketing-action or other-element.
        return "\n      <div class=\"marketing-action " + cssClass + "\">\n        <span class=\"icon\">\n          <img src=\"/~/icon/OfficeWhite/32x32/gearwheels.png\" />\n        </span>\n        <p class=\"text with-subtitle\" title=\"Sample single item\">\n          Send Web Push Notification\n          <small class=\"subtitle\" title=\"" + subTitle + "\">" + subTitle + "</small>\n        </p>\n      </div>\n    ";
    };
    Object.defineProperty(SendPushNotificationActivity.prototype, "isDefined", {
        get: function () {
            /*
              The editorParams property value is the object that is serialized from the activity editor.
              If the activity is undefined, the editorParams property will evaluate to {}.
            */
            return Boolean(this.editorParams.count);
        },
        enumerable: true,
        configurable: true
    });
    return SendPushNotificationActivity;
}(ma_core_1.SingleItem));
exports.SendPushNotificationActivity = SendPushNotificationActivity;
//# sourceMappingURL=send-push-notification-activity.js.map