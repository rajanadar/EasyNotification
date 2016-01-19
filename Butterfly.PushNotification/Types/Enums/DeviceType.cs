// <copyright file="DeviceType.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'DeviceType' enumeration.
// </summary>
namespace Butterfly.PushNotification
{
    /// <summary>
    /// The type of device to receiving push notification.
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// we don't know yet.
        /// </summary>
        None = 0,

        /// <summary>
        /// Windows Phone 7.0, the pre-mango version. if you're not sure about the version, go for the safe value of WindowsPhone7_0.
        /// </summary>
        WindowsPhone7_0 = 1,

        /// <summary>
        /// Windows Phone 7.1 or 7.5, the mango version. if you're not sure about the version, go for the safe value of WindowsPhone7_0.
        /// </summary>
        WindowsPhone7_1 = 2,
    }
}
