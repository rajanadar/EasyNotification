// <copyright file="WindowsPhoneAuthenticationSettings.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'WindowsPhoneAuthenticationSettings' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Represents the Authentication Settings to send a secure notification to a Windows Phone Device.
    /// </summary>
    [Serializable]
    public partial class WindowsPhoneAuthenticationSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsPhoneAuthenticationSettings"/> class.
        /// </summary>
        public WindowsPhoneAuthenticationSettings()
        {
            this.EnableAuthentication = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the notification needs to be sent in an authenticated manner.
        /// If true, the system will look for the X509 Certificate to be sent along with the Notification request.
        /// </summary>
        public bool EnableAuthentication { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="X509Certificate"/> to be sent as part of the notification request.
        /// The presence of this certificate helps MPNS to verify the authenticity of the caller and send secure notifications to the device.
        /// </summary>
        public X509Certificate X509Certificate { get; set; }
    }
}
