import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SendPushNotificationEditorComponent } from './send-push-notification-editor.component';

@NgModule({
    imports: [
        FormsModule
    ],
    declarations: [SendPushNotificationEditorComponent],
    entryComponents: [SendPushNotificationEditorComponent]
})

export class SendPushNotificationModule { }