import { SingleItem } from '@sitecore/ma-core';

export class SendPushNotificationActivity extends SingleItem {

    getVisual(): string {
        const subTitle = this.isDefined ? this.editorParams.title : '';
        const cssClass = this.isDefined ? '' : 'undefined';

        // The CSS class can either be marketing-action or other-element.
        return `
      <div class="marketing-action ${cssClass}">
        <span class="icon">
          <img src="/~/icon/officewhite/32x32/message.png" />
        </span>
        <p class="text with-subtitle" title="Send Web Push Notification">
          Send Web Push Notification
          <small class="subtitle" title="${subTitle}">${subTitle}</small>
        </p>
      </div>
    `;
    }

    get isDefined(): boolean {
        /*
          The editorParams property value is the object that is serialized from the activity editor.
          If the activity is undefined, the editorParams property will evaluate to {}.
        */
        return Boolean(this.editorParams.title);
    }
}