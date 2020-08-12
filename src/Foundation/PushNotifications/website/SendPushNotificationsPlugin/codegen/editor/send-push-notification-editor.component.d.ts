import { OnInit } from '@angular/core';
import { EditorBase } from '@sitecore/ma-core';
export declare class SendPushNotificationEditorComponent extends EditorBase implements OnInit {
    title: string;
    body: string;
    image: string;
    icon: string;
    cta: string;
    ngOnInit(): void;
    testNotification(): void;
    callCTA(): void;
    serialize(): any;
}
