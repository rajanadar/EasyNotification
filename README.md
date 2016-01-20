# EasyNotification

## *A .NET Push Notification Helper Library for Windows Phone.*

### <a href="https://www.nuget.org/packages/EasyNotification" target="_blank">https://www.nuget.org/packages/EasyNotification</a> 

**'Easy Notification'** is a C# 4.0 Push Notification helper library that provides a simple interface to send **Toast, Tile, Raw** and other notifications to Windows Phone devices. 

* As an app developer, creating and sending notifications is a tedious process, because of the Http requests/responses involved.
* This library abstracts all this out to provide a clean and simple interface so that you can quickly integrate notification features into your app.

Using the library is as simple as creating a message object and calling a method.

```cs
var toastMessage = new WindowsPhoneToastPushNotificationMessage();
var result = PushNotifier.SendPushNotificationMessage(toastMessage);
```

**Common Features:**

* **Send Tile, Toast & Raw Notifications to WP 7.0 & WP 7.x & WP 8 Devices.**
* **Has Validations in-built. You can turn it on/off.**
* **Abstracts the http & xml implementation details by strongly typed objects.**
* **Returns the appropriate response in a strongly typed manner.**
* **Supports Windows Phone 7.0 & Windows Phone 7.1. & Windows Phone 8 devices.**

**Special Features:**

- **Provides suggestions on next steps as per [MSDN](https://msdn.microsoft.com/library/windows/apps/ff941100(v=vs.105).aspx), based on the response.**
- **Supports synchronous & asynchronous methods to send notifications.**
- **Supports authenticated notifications using X509 Certificates. (not many libraries do this.)**
- **Supports Callback Registration Requests. (not many libraries out there do this.)**
- **Supports Localization. Just drop in the appropriate resource files.**
- **In-built error handling so that you always get the response. (with raw exception included)**

**Versioning Scheme:**

- **Major Version.Feature Version.Bug Fix (e.g. 1.0.0)**
- **Bug fixes will not be breaking changes.**
- **Feature changes/Major Version changes may not be necessarily breaking changes.** I'll try to avoid it as much as possible.

they say that **'if at first you don't succeed, call it version 1.0.'** alright, point taken.

feel free to create a [new work item](https://github.com/rajanadar/EasyNotification/issues/new) if you see any issues.
