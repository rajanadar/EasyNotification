// <copyright file="SubscriptionStatus.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'SubscriptionStatus' enumeration.
// </summary>
namespace Butterfly.PushNotification
{
    /// <summary>
    /// The subscription status of the device.
    /// </summary>
    public enum SubscriptionStatus
    {
        /// <summary>
        /// Default value OR 
        /// </summary>
        NotApplicable = 0,

        /// <summary>
        /// The subscription status is healthy.
        /// </summary>
        Active = 1,

        /// <summary>
        /// The subscription is invalid and is not present on the Push Notification Service. 
        /// Typically, new notifications should not be sent to this subscription, and the subscription state should be dropped.
        /// </summary>
        Expired = 2,
    }
}
