# Web Push Notifications for Sitecore

Web Push Notifications are a powerful way to engage with your visitors. When not abused, you can implement campaigns that are 3X - 5X more effective than Email Campaigns.

The Web Push Notification for Sitecore Module brings this capability directly into Marketing Automation, allowing you to implement complex targeted campaigns that can improve visitor engagement.

## Installing the Module

The Releases tab has a number of files needed to deploy the module. A Sitecore Package installs the main pieces to your local sitecore instance while a separate xDB zip file can be unzipped to the root of your local xDB instance. 

## Configuring the Module

To facilitate secure communications, you need to specify VAPID keys. Luckily there are a number of free online generators such as: https://vapidkeys.com/

Once you have them, you'll need to configure them into an instance of "Push Notification Configuration", a Foundational Template that can be used to store these settings. If you are using SXA, this item will automatically be created in the settings folder when the "Push Notification" feature is activated.

## Collecting Subscriptions

We recommend the double opt in process of subscribing visitors, as prompting to subscribe on page load is a bad user experience. We give you a number of ways to manage the subscription process:

### Push Subscriptions Facet

Subscription details are managed in a custom xDB facet called  "PushSubscriptions". Subscriptions are collected in a dictionary based on device Id. This allows for contacts to manage subscriptions for every browser, device they use to access the site.

### API

To register a new subscription, you can manually post to our Subscribe endpoint:

```
/api/sf/1.0/pushNotification/subscribe
```

It expects the following JSON object posted to the message body:

```javascript
{
	subscription : {
		endpoint: "", //endpoint returned from the browser subscription registration
		keys: {
			"" : "", //Keys returned from the browser subscription registration
			"" : "" //Keys returned from the browser subscription registration
		}
	},
	goalId: "", //Guid Id of the Goal to Trigger on Submission
	configId: "" //Guid of the instance of the Push Notification Configuration Item where your VAPID keys have been configured.
}
```

### JavaScript Library

To make it easier to manage the subscription process, we have provided a JavaScript library that makes subscriptions seamles. The source file is included in the package here:

```
/content/Feature/PushNotification/PushNotification.js
```

It includes a global object/class with a single exposed subscribe method:

SF.PushNotfications.subscribe(publicKey, goalId, configId)

It requires the VAPID public key (needed to drive the subscription), the goalId and configId needed to submit to the Rest API.

### SXA Subscribe Button

If you are using SXA, you can enable the "Push Notifications" feature, which will add a "Subscribe Button" button to the Toolbox. This component supports variants, so it can be customized to meet your UI/UX needs for creating the proper call to action.

In Experience Editor, it provides a custom experience button that allows you to specify which Goal to Trigger. It will pull the VAPID details from the Site Configuration automatically.

The SXA Feature will automatically add everything needed to the site, including a Base Theme that includes the needed JavaScript file references.

## Sending Notifications

Sending Notifications can be managed through Sitecore's Marketing Automation tool. Create a new campaign that listens for the goal triggered by the Subscription.

Once enrolled, we recommend adding the visitor to a list. These Lists can be leveraged for targeted blasts the same way they could be used for email blasts.

Note that you may want to consider enabling indexing of anonymous contacts in order to see the proper counts of your lists.

To send a notification, drag the "Send Push Notification" Activity from the "Marketing Actions" group. Here you can specify Title, Body, Image, Icon and CTA. Note that not all browsers and devices support all properties. You can use the "Test Notification" button to preview how it will look.

Use relative url's for Image, Icon and CTA, starting with "/" as browser security models do not allow push notifications to leverage these from other domains. 

For CTA, consider appending campaign querystring parameters for better tracking the effectiveness of the notification.

When creating campaigns for this purpose, consider using the "Online/Automation programs/Web Push Notification automation" channel to properly populate analytics when activated.

Note that when activated, the custom activity will send the notification to every subscription registered for each enrolled contact. So if a contact have subscriptions on mobile and multiple desktop browsers, they will get the notification on each device/browser. (As long as they have subscribed on each of the devices/browsers)

Note that the custom activity has been built to avoid putting the automation plan into a failure state. Any exceptions are logged to the Marketing Automation logs, and will continue on.

## Personalization

You may want to hide the subscribe button based on whether they have subscribed. (Or if they are using an unsupported device such as ios).

To support this we have included two Personalization conditions: "Has Subscriptions" and "Has Subscription on Current Device" which can be applied to the provided SXA Subscribe button component or your own custom components.

To support Decision Point Activities in Marketing Automation Plans, a similar xDB personalization rule "Has Subscriptions" has been provided. This could be leveraged in omnichannel campaigns that send both Email and Push Notifications.

## Extending the Service Worker

When the subscription is created, a Service worked is also registered. This service worker waits for notifications, and displays them.

Located here: 

```
/content/Feature/PushNotifications/service-worker.js
```

The current implementation is pretty straight forward, managing the notification and click support needs.

You may consider updating this implementation to send analytics data for Views to your web analytics tool of choice for performance analysis.

You could also modify the logic to fetch alternative sourcing for the notification messaging, implementing your own endpoints to retreive the proper title, body, icon and image to be displayed.