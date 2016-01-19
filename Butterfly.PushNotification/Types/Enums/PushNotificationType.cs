// <copyright file="PushNotificationType.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'PushNotificationType' enumeration.
// </summary>
namespace Butterfly.PushNotification
{
    /// <summary>
    /// The type of push notification.
    /// </summary>
    public enum PushNotificationType
    {
        /// <summary>
        /// we don't know yet.
        /// </summary>
        None = 0,

        /// <summary>
        /// A push notification to display as toast.
        /// </summary>
        Toast = 1,

        /// <summary>
        /// A push notification to update one or more elements of a tile.
        /// </summary>
        Tile = 2,

        /// <summary>
        /// A push notification to perform an update to a tile that does not involve UI.
        /// </summary>
        Raw = 3,
    }
}
