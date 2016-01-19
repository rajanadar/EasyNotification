// <copyright file="WindowsPhonePushNotificationMessage.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'WindowsPhonePushNotificationMessage' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;

    /// <summary>
    /// The abstract base class for a Windows Phone Push Notification message.
    /// </summary>
    [Serializable]
    public abstract partial class WindowsPhonePushNotificationMessage : PushNotificationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsPhonePushNotificationMessage"/> class.
        /// </summary>
        public WindowsPhonePushNotificationMessage()
        {
            this.SupportWindowsPhone7_0 = false;
            this.MessagePriority = PushNotification.MessagePriority.Realtime;

            this.AuthenticationSettings = new WindowsPhoneAuthenticationSettings();
        }

        /// <summary>
        /// Gets or sets the priority of the notification message by specifying one of three batching intervals.
        /// Translates to the X-NotificationClass header with a value, based on the type of Notification.
        /// Default value is 'Realtime'. i.e. Notification is sent as soon as possible.
        /// </summary>
        public MessagePriority MessagePriority { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if backward compatibility to a pre-mango (7.1) Windows Phone needs to be accounted for, while sending the notification.
        /// </summary>
        public bool SupportWindowsPhone7_0 { get; set; }

        /// <summary>
        /// Gets the Device type as Windows Phone 7. 1 or 7.0 based on the <see cref="SupportWindowsPhone7_0"/> value.
        /// </summary>
        public override DeviceType DeviceType
        {
            get
            {
                if (this.SupportWindowsPhone7_0)
                {
                    return PushNotification.DeviceType.WindowsPhone7_0;
                }

                return DeviceType.WindowsPhone7_1;
            }
        }

        /// <summary>
        /// Gets or sets the Authentication Settings to send a secure notification to the Windows Phone Device. 
        /// </summary>
        public WindowsPhoneAuthenticationSettings AuthenticationSettings { get; set; }
    }
}
