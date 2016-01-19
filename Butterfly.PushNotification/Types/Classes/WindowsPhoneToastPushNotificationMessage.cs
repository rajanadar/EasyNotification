// <copyright file="WindowsPhoneToastPushNotificationMessage.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'WindowsPhoneToastPushNotificationMessage' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents a Windows Phone Toast Notification Message.
    /// </summary>
    [Serializable]
    public partial class WindowsPhoneToastPushNotificationMessage : WindowsPhonePushNotificationMessage
    {
        /// <summary>
        /// Gets a value indicating the type of Push Notification.
        /// It is overridden to Push Notification type.
        /// </summary>
        public override PushNotificationType PushNotificationType
        {
            get
            {
                return PushNotification.PushNotificationType.Toast;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the title text for this Toast Notification.
        /// </summary>
        public string Text1 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the content text for this Toast Notification.
        /// </summary>
        public string Text2 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating any additional values to be passed.
        /// It allows deep linking and contextual navigation to a page with querystring values if needed.
        /// Note: Supported on Windows Phone 7.1 and above only.
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// Creates the Xml Payload for the Toast Notification Message.
        /// </summary>
        /// <returns>The Byte form of the payload.</returns>
        public override byte[] CreatePayload()
        {
            var newXml = String.Empty;

            if (this.DeviceType == DeviceType.WindowsPhone7_1)
            {
                newXml = "<wp:Param>" + this.Param.ToValidXmlCharacters(this.EscapeInvalidCharacters) + "</wp:Param>";
            }

            var toastNotificationPayloadXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                               "<wp:Notification xmlns:wp=\"WPNotification\">" +
                                                  "<wp:Toast>" +
                                                       "<wp:Text1>" + this.Text1.ToValidXmlCharacters(this.EscapeInvalidCharacters) + "</wp:Text1>" +
                                                       "<wp:Text2>" + this.Text2.ToValidXmlCharacters(this.EscapeInvalidCharacters) + "</wp:Text2>" + newXml +
                                                  "</wp:Toast> " +
                                               "</wp:Notification>";

            var toastNotificationPayload = Encoding.UTF8.GetBytes(toastNotificationPayloadXml);
            return toastNotificationPayload;
        }
    }
}
