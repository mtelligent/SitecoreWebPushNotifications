import { Component, OnInit } from '@angular/core';
import { EditorBase } from '@sitecore/ma-core';




@Component({
    selector: 'sample-single-item-editor',
    template: `
    <section class="content">
      <div class="form-group">
        <div class="row readonly-editor">
            <div class="col-12">
                <label class="title">Title</label>
                <input type="text" class="form-control" [(ngModel)]="title" />
                <br/>
            </div>
            <div class="col-12">
                <label class="title">Body</label>
                <textarea class="form-control" [(ngModel)]="body" rows="5"></textarea>
                <br/>
            </div>
            <div class="col-12">
                <label class="title">Image</label>
                <input type="text" class="form-control" [(ngModel)]="image" />
                <br/>
            </div>
            <div class="col-12">
                <label class="title">Icon</label>
                <input type="text" class="form-control" [(ngModel)]="icon" />
                <br/>
            </div>
            <div class="col-12">
                <label class="title">CTA</label>
                <input type="text" class="form-control" [(ngModel)]="cta" />
                <br/>
            </div>
            <div class="col-12">
                <button class="btn btn-info" (click)="testNotification()" >Test Notification</button>
                <br/>
            </div>
        </div>
      </div>
    </section>
  `,
    // CSS styles are omitted for brevity.
    styles: [``]
})

export class SendPushNotificationEditorComponent extends EditorBase implements OnInit {

    /**
    * Count property is bound to the input element
    */
    title: string;
    body: string;
    image: string;
    icon: string;
    cta: string;

    /**
    * A component lifecycle hook.
    * Called whenever the model property or any other property in the component changes.
    */
    ngOnInit(): void {
        this.title = this.model ? this.model.title || "" : "";
        this.body = this.model ? this.model.body || "" : "";
        this.image = this.model ? this.model.image || "" : "";
        this.icon = this.model ? this.model.icon || "" : "";
        this.cta = this.model ? this.model.cta || "" : "";
    }

    testNotification() {

        if (Notification.permission !== 'granted')
            Notification.requestPermission();

        let options: NotificationOptions = { icon : this.icon, image : this.image, body : this.body }

        var notification = new Notification(this.title, options);

        notification.onclick = this.callCTA;

    }

    callCTA() {
        window.open(this.cta);
    }

    /**
    * Called when the changes in the editor are applied.
    * @returns an object that contains the values of the parameters of the activity,
    * where each property corresponds to a parameter key.
    */
    serialize(): any {
        return {
            title: this.title,
            body: this.body,
            image: this.image,
            icon: this.icon,
            cta: this.cta
        };
    }
}