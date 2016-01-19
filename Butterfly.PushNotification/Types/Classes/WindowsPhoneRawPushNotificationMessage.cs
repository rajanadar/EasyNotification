// <copyright file="WindowsPhoneRawPushNotificationMessage.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'WindowsPhoneRawPushNotificationMessage' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a Windows Phone Raw Notification Message.
    /// </summary>
    [Serializable]
    public partial class WindowsPhoneRawPushNotificationMessage : WindowsPhonePushNotificationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsPhoneRawPushNotificationMessage"/> class.
        /// </summary>
        public WindowsPhoneRawPushNotificationMessage()
        {
            this.NameValues = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets a value indicating the type of Push Notification.
        /// It is overridden to Raw Notification type.
        /// </summary>
        public override PushNotificationType PushNotificationType
        {
            get
            {
                return PushNotification.PushNotificationType.Raw;
            }
        }

        /// <summary>
        /// Gets or sets a value for the name value pairs in the Raw Notification Payload.
        /// </summary>
        public Dictionary<string, string> NameValues { get; set; }

        /// <summary>
        /// Creates the Xml Payload for the Raw Notification Message.
        /// </summary>
        /// <returns>The Byte form of the payload.</returns>
        public override byte[] CreatePayload()
        {
            var elementXml = this.NameValues.ToRawNotificationElementXml(this.EscapeInvalidCharacters);

            var rawNotificationPayloadXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                               "<root>" + elementXml + "</root>";

            var rawNotificationPayload = Encoding.UTF8.GetBytes(rawNotificationPayloadXml);
            return rawNotificationPayload;
        }
    }
}
