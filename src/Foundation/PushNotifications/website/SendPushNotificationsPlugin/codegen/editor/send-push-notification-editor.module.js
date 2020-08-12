"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SendPushNotificationModule = void 0;
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var send_push_notification_editor_component_1 = require("./send-push-notification-editor.component");
var SendPushNotificationModule = (function () {
    function SendPushNotificationModule() {
    }
    SendPushNotificationModule.decorators = [
        { type: core_1.NgModule, args: [{
                    imports: [
                        forms_1.FormsModule
                    ],
                    declarations: [send_push_notification_editor_component_1.SendPushNotificationEditorComponent],
                    entryComponents: [send_push_notification_editor_component_1.SendPushNotificationEditorComponent]
                },] },
    ];
    return SendPushNotificationModule;
}());
exports.SendPushNotificationModule = SendPushNotificationModule;
//# sourceMappingURL=send-push-notification-editor.module.js.map