// <copyright file="PushNotificationStatus.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'PushNotificationStatus' enumeration.
// </summary>
namespace Butterfly.PushNotification
{
    /// <summary>
    /// The status of the notification received by the Microsoft Push Notification Service.
    /// </summary>
    public enum PushNotificationStatus
    {
        /// <summary>
        /// Default value OR The Push Notification Service is unable to process the request.
        /// </summary>
        NotApplicable = 0,

        /// <summary>
        /// The notification request was accepted.
        /// </summary>
        Received = 1,

        /// <summary>
        /// The Push Notification Service queue is full.
        /// </summary>
        QueueFull = 2,

        /// <summary>
        /// The push notification was received and dropped by the Push Notification Service.
        /// </summary>
        Suppressed = 3,

        /// <summary>
        /// The subscription is invalid and is not present on the Push Notification Service.
        /// </summary>
        Dropped = 4,
    }
}
