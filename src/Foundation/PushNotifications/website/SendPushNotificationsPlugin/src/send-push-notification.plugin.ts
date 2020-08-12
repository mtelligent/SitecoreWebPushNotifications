import { Plugin } from '@sitecore/ma-core';
import { SendPushNotificationActivity } from './send-push-notification-activity';
import { SendPushNotificationModuleNgFactory } from '../codegen/editor/send-push-notification-editor.module.ngfactory';
import { SendPushNotificationEditorComponent } from '../codegen/editor/send-push-notification-editor.component';

//To Update ensure you run:
// npm Install 
// npm run build
// copy compiled js file from dist folder to 
// sitecore/shell/client/Applications/MarketingAutomation/plugins/
// folder in project.

@Plugin({
    activityDefinitions: [
        {
            // The ID of the sitecore activity item that exists in the Master database under
            // sitecore/System/Settings/Analytics/Marketing Automation/Activity Types item
            id: '3D2CCC0A-5E13-421B-96F2-EE737319532F',
            activity: SendPushNotificationActivity,
            editorComponenet: SendPushNotificationEditorComponent,
            editorModuleFactory: SendPushNotificationModuleNgFactory
        }]
})

export default class SamplePlugin { }