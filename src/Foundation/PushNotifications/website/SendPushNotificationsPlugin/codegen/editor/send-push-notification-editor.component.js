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
exports.SendPushNotificationEditorComponent = void 0;
var core_1 = require("@angular/core");
var ma_core_1 = require("@sitecore/ma-core");
var SendPushNotificationEditorComponent = (function (_super) {
    __extends(SendPushNotificationEditorComponent, _super);
    function SendPushNotificationEditorComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SendPushNotificationEditorComponent.prototype.ngOnInit = function () {
        this.title = this.model ? this.model.title || "" : "";
        this.body = this.model ? this.model.body || "" : "";
        this.image = this.model ? this.model.image || "" : "";
        this.icon = this.model ? this.model.icon || "" : "";
        this.cta = this.model ? this.model.cta || "" : "";
    };
    SendPushNotificationEditorComponent.prototype.testNotification = function () {
        if (Notification.permission !== 'granted')
            Notification.requestPermission();
        var options = { icon: this.icon, image: this.image, body: this.body };
        var notification = new Notification(this.title, options);
        notification.onclick = this.callCTA;
    };
    SendPushNotificationEditorComponent.prototype.callCTA = function () {
        window.open(this.cta);
    };
    SendPushNotificationEditorComponent.prototype.serialize = function () {
        return {
            title: this.title,
            body: this.body,
            image: this.image,
            icon: this.icon,
            cta: this.cta
        };
    };
    SendPushNotificationEditorComponent.decorators = [
        { type: core_1.Component, args: [{
                    selector: 'sample-single-item-editor',
                    template: "\n    <section class=\"content\">\n      <div class=\"form-group\">\n        <div class=\"row readonly-editor\">\n            <div class=\"col-12\">\n                <label class=\"title\">Title</label>\n                <input type=\"text\" class=\"form-control\" [(ngModel)]=\"title\" />\n                <br/>\n            </div>\n            <div class=\"col-12\">\n                <label class=\"title\">Body</label>\n                <textarea class=\"form-control\" [(ngModel)]=\"body\" rows=\"5\"></textarea>\n                <br/>\n            </div>\n            <div class=\"col-12\">\n                <label class=\"title\">Image</label>\n                <input type=\"text\" class=\"form-control\" [(ngModel)]=\"image\" />\n                <br/>\n            </div>\n            <div class=\"col-12\">\n                <label class=\"title\">Icon</label>\n                <input type=\"text\" class=\"form-control\" [(ngModel)]=\"icon\" />\n                <br/>\n            </div>\n            <div class=\"col-12\">\n                <label class=\"title\">CTA</label>\n                <input type=\"text\" class=\"form-control\" [(ngModel)]=\"cta\" />\n                <br/>\n            </div>\n            <div class=\"col-12\">\n                <button class=\"btn btn-info\" (click)=\"testNotification()\" >Test Notification</button>\n                <br/>\n            </div>\n        </div>\n      </div>\n    </section>\n  ",
                    styles: [""]
                },] },
    ];
    return SendPushNotificationEditorComponent;
}(ma_core_1.EditorBase));
exports.SendPushNotificationEditorComponent = SendPushNotificationEditorComponent;
//# sourceMappingURL=send-push-notification-editor.component.js.map