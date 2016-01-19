// <copyright file="MessagePriority.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'MessagePriority' enumeration.
// </summary>
namespace Butterfly.PushNotification
{
    /// <summary>
    ///  Determines the priority of the notification message by specifying one of three batching intervals.
    ///  Translates to the X-NotificationClass header with a value, based on the type of Notification.
    /// </summary>
    public enum MessagePriority
    {
        /// <summary>
        /// we don't know yet.
        /// </summary>
        None = 0,

        /// <summary>
        /// The notification is sent as soon as possible.
        /// </summary>
        Realtime = 1,

        /// <summary>
        /// The notification is delivered within 450 seconds.
        /// </summary>
        Priority = 2,

        /// <summary>
        /// The notification is delivered within 900 seconds.
        /// </summary>
        Regular = 3,
    }
}
