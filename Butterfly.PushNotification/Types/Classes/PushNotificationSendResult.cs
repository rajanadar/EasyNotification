// <copyright file="PushNotificationSendResult.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'PushNotificationSendResult' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;
    using System.Net;

    /// <summary>
    /// Denotes the response of the Notification Send operation.
    /// </summary>
    [Serializable]
    public partial class PushNotificationSendResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationSendResult"/> class.
        /// </summary>
        public PushNotificationSendResult()
        {
            this.OperationResult = ResultType.Success;

            this.PushNotificationType = PushNotification.PushNotificationType.None;
            this.DeviceType = PushNotification.DeviceType.None;
            this.PushNotificationStatus = PushNotification.PushNotificationStatus.NotApplicable;
            this.DeviceConnectionStatus = PushNotification.DeviceConnectionStatus.NotApplicable;
            this.PushNotificationSubscriptionStatus = PushNotification.SubscriptionStatus.NotApplicable;

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
        /// Gets or sets the notification message ID associated with the response. This can be used to uniquely identify the Notification.
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// Gets or sets the type of push notification.
        /// </summary>
        public PushNotificationType PushNotificationType { get; set; }

        /// <summary>
        /// Gets or sets the type of device to receiving push notification.
        /// </summary>
        public DeviceType DeviceType { get; set; }

        /// <summary>
        /// Gets or sets the status of the notification received by the Microsoft Push Notification Service.
        /// </summary>
        public PushNotificationStatus PushNotificationStatus { get; set; }

        /// <summary>
        /// Gets or sets the connection status of the device.
        /// </summary>
        public DeviceConnectionStatus DeviceConnectionStatus { get; set; }

        /// <summary>
        /// Gets or sets the subscription status of the device.
        /// </summary>
        public SubscriptionStatus PushNotificationSubscriptionStatus { get; set; }

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

        /// <summary>
        /// Gets a value indicating the possible set of reasons for the current response (Http Status + Notification Status + Device Connection Status + Subscription Status)/
        /// This message may also suggest the next steps in many cases.
        /// Please refer to the MSDN Link: http://msdn.microsoft.com/en-us/library/ff941100(VS.92).aspx for more details.
        /// </summary>
        public string Comments
        {
            get
            {
                var comments = Extensions.GetComments(this.HttpStatusCode, this.PushNotificationStatus, this.DeviceConnectionStatus, this.PushNotificationSubscriptionStatus);
                return comments;
            }
        }
    }
}
