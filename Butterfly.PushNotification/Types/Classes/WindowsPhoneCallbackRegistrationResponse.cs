// <copyright file="WindowsPhoneCallbackRegistrationResponse.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'WindowsPhoneCallbackRegistrationResponse' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;
    using System.Net;

    /// <summary>
    /// Denotes the response of the Windows Phone Callback Registration Request.
    /// </summary>
    [Serializable]
    public partial class WindowsPhoneCallbackRegistrationResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsPhoneCallbackRegistrationResponse"/> class.
        /// </summary>
        public WindowsPhoneCallbackRegistrationResponse()
        {
            this.OperationResult = ResultType.Success;
            this.DeviceConnectionStatus = PushNotification.DeviceConnectionStatus.NotApplicable;
            this.CallbackSubscriptionStatus = SubscriptionStatus.NotApplicable;

            this.ValidationResults = new ValidationResults();
        }

        /// <summary>
        /// Gets or sets the result of the Notification Send operation.
        /// </summary>
        public ResultType OperationResult { get; set; }

        /// <summary>
        /// Gets or sets the start date time of the operation.
        /// </summary>
        public DateTime OperationStartUtcDateTime { get; set; }

        /// <summary>
        /// Gets or sets the end date time of the operation.
        /// </summary>
        public DateTime OperationEndUtcDateTime { get; set; }

        /// <summary>
        /// Gets or sets the connection status of the device associated with the callback.
        /// </summary>
        public DeviceConnectionStatus DeviceConnectionStatus { get; set; }

        /// <summary>
        /// Gets or sets the subscription status of the device associated with the callback.
        /// </summary>
        public SubscriptionStatus CallbackSubscriptionStatus { get; set; }

        /// <summary>
        /// Gets or sets the list of Validation Messages.
        /// </summary>
        public ValidationResults ValidationResults { get; set; }

        /// <summary>
        /// Gets or sets the Exception object as-is returned from the Notification Web Call.
        /// </summary>
        public Exception RawException { get; set; }

        /// <summary>
        /// Gets or sets the 3 digit Code denoting the Notification Response Status of the web call.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
