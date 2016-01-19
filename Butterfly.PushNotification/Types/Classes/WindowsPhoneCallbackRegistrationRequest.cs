// <copyright file="WindowsPhoneCallbackRegistrationRequest.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'WindowsPhoneCallbackRegistrationRequest' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;

    /// <summary>
    /// Represents the callback registration request that associates a callback URI and message with a subscription.
    /// </summary>
    [Serializable]
    public partial class WindowsPhoneCallbackRegistrationRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsPhoneCallbackRegistrationRequest"/> class.
        /// </summary>
        public WindowsPhoneCallbackRegistrationRequest()
        {
            this.CallbackMessage = new byte[1024];
            this.EnableValidation = true;
        }

        /// <summary>
        /// Gets or sets the Push Notification System Channel Uri to send the Notification.
        /// </summary>
        public string NotificationUri { get; set; }

        /// <summary>
        /// Gets a value indicating the callback uri. This value is the notification uri appended by the '/callback' string.
        /// </summary>
        public string PushNotificationCallbackServiceUri
        {
            get
            {
                var result = String.Empty;

                if (!String.IsNullOrWhiteSpace(this.NotificationUri))
                {
                    result = this.NotificationUri + "/callback";
                }

                return result;
            }
        }

        /// <summary>
        /// Gets or sets the URI to post the set callback request to.
        /// </summary>
        public string CallbackUri { get; set; }

        /// <summary>
        /// Gets or sets the callback message payload to send to the Push Notification Service. 
        /// It is an opaque blob that the Push Notification Service posts to the provided callback URI when an associated event triggers. 
        /// The maximum allowed size for this payload is 1 KB.
        /// </summary>
        public byte[] CallbackMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether validation rules need to be run on these properties prior to sending out the registration request.
        /// Before turning it off, please consider the trade-off between validation time with clear error messages vs. cryptic messages and web request failures.
        /// Default value is true.
        /// </summary>
        public bool EnableValidation { get; set; }
    }
}
