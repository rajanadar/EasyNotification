// <copyright file="PushNotificationMessage.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'PushNotificationMessage' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;

    /// <summary>
    /// The abstract base class for a Push Notification message.
    /// The sub-classes can make it specific for a type of device.
    /// </summary>
    [Serializable]
    public abstract partial class PushNotificationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationMessage"/> class.
        /// </summary>
        public PushNotificationMessage()
        {
            this.EnableValidation = true;
            this.EscapeInvalidCharacters = true;
        }

        /// <summary>
        /// Gets the type of push notification. It is abstract and needs to be implemented by the appropriate sub-classes.
        /// </summary>
        public abstract PushNotificationType PushNotificationType { get; }

        /// <summary>
        /// Gets the type of device to receiving push notification. It is abstract and needs to be implemented by the appropriate sub-classes.
        /// </summary>
        public abstract DeviceType DeviceType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether validation rules need to be run on this Notification Message properties prior to sending out the notifications.
        /// Before turning it off, please consider the trade-off between validation time with clear error messages vs. cryptic messages and notification failures.
        /// Default value is true.
        /// </summary>
        public bool EnableValidation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the properties of this notification message needs to be encoded.
        /// If the value is set to true, then the library takes care of encoding the message values.
        /// If the value is set to false, then the calling application needs to encode the values, else notification errors may happen.
        /// Either ways, the receiving application needs to have decoding logic.
        /// If the caller is already escaping the values, please set this flag to false.
        /// The default value is true.
        /// </summary>
        public bool EscapeInvalidCharacters { get; set; }

        /// <summary>
        /// Gets or sets the Push Notification System Channel Uri to send the Notification.
        /// </summary>
        public string NotificationUri { get; set; }

        /// <summary>
        /// Creates the notification payload xml. It is abstract and needs to be implemented by the appropriate sub-classes.
        /// </summary>
        /// <returns>The byte form of the notification payload xml.</returns>
        public abstract byte[] CreatePayload();
    }
}
