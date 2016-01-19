// <copyright file="DeviceConnectionStatus.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'DeviceConnectionStatus' enumeration.
// </summary>
namespace Butterfly.PushNotification
{
    /// <summary>
    /// The connection status of the device.
    /// </summary>
    public enum DeviceConnectionStatus
    {
        /// <summary>
        /// Default value OR The Push Notification Service is unable to process the request.
        /// </summary>
        NotApplicable = 0,

        /// <summary>
        /// The last known status of the Device was that it was connected to the Push Notification Service.
        /// </summary>
        Connected = 1,

        /// <summary>
        /// The last known status of the Device was that it was disconnected from the Push Notification Service.
        /// </summary>
        Disconnected = 2,

        /// <summary>
        /// The Device is temporarily inaccessible due to coverage, wi-fi, network load etc.
        /// </summary>
        TempDisconnected = 3,

        /// <summary>
        /// The device is in an inactive state. 
        /// Typically a re-attempt to sending the request one time per hour at maximum is recommended, after receiving this error.
        /// </summary>
        Inactive = 4,
    }
}
