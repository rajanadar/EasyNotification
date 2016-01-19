// <copyright file="Extensions.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The Extension Methods class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Security;
    using Butterfly.PushNotification.Resources;

    /// <summary>
    /// Represents the set of extension methods.
    /// </summary>
    internal static partial class Extensions
    {
        /// <summary>
        /// The Dictionary to get the Push Notification Service Response Comments based on the response codes &amp; statuses.
        /// Please refer to the MSDN Link: http://msdn.microsoft.com/en-us/library/ff941100(VS.92).aspx for more details.
        /// </summary>
        private static Dictionary<string, string> pushNotificationServiceResponseComments = new Dictionary<string, string>()
                                                                                                           {
                                                                                                               { Extensions.GetKey(HttpStatusCode.OK, PushNotificationStatus.Received, DeviceConnectionStatus.Connected, SubscriptionStatus.Active), StringResources.ResponseComments_200_Received_Connected_Active },
                                                                                                               { Extensions.GetKey(HttpStatusCode.OK, PushNotificationStatus.Received, DeviceConnectionStatus.TempDisconnected, SubscriptionStatus.Active), StringResources.ResponseComments_200_Received_TempDisconnected_Active },
                                                                                                               { Extensions.GetKey(HttpStatusCode.OK, PushNotificationStatus.QueueFull, DeviceConnectionStatus.Connected, SubscriptionStatus.Active), StringResources.ResponseComments_200_QueueFull_Connected_Active },
                                                                                                               { Extensions.GetKey(HttpStatusCode.OK, PushNotificationStatus.QueueFull, DeviceConnectionStatus.TempDisconnected, SubscriptionStatus.Active), StringResources.ResponseComments_200_QueueFull_TempDisconnected_Active },
                                                                                                               { Extensions.GetKey(HttpStatusCode.OK, PushNotificationStatus.Suppressed, DeviceConnectionStatus.Connected, SubscriptionStatus.Active), StringResources.ResponseComments_200_Suppressed_Connected_Active },
                                                                                                               { Extensions.GetKey(HttpStatusCode.OK, PushNotificationStatus.Suppressed, DeviceConnectionStatus.TempDisconnected, SubscriptionStatus.Active), StringResources.ResponseComments_200_Suppressed_TempDisconnected_Active },
                                                                                                               { Extensions.GetKey(HttpStatusCode.BadRequest, PushNotificationStatus.NotApplicable, DeviceConnectionStatus.NotApplicable, SubscriptionStatus.NotApplicable), StringResources.ResponseComments_400_NotApplicable_NotApplicable_NotApplicable },
                                                                                                               { Extensions.GetKey(HttpStatusCode.Unauthorized, PushNotificationStatus.NotApplicable, DeviceConnectionStatus.NotApplicable, SubscriptionStatus.NotApplicable), StringResources.ResponseComments_401_NotApplicable_NotApplicable_NotApplicable },
                                                                                                               { Extensions.GetKey(HttpStatusCode.NotFound, PushNotificationStatus.Dropped, DeviceConnectionStatus.Connected, SubscriptionStatus.Expired), StringResources.ResponseComments_404_Dropped_Connected_Expired },
                                                                                                               { Extensions.GetKey(HttpStatusCode.NotFound, PushNotificationStatus.Dropped, DeviceConnectionStatus.TempDisconnected, SubscriptionStatus.Expired), StringResources.ResponseComments_404_Dropped_TempDisconnected_Expired },
                                                                                                               { Extensions.GetKey(HttpStatusCode.NotFound, PushNotificationStatus.Dropped, DeviceConnectionStatus.Disconnected, SubscriptionStatus.Expired), StringResources.ResponseComments_404_Dropped_Disconnected_Expired },
                                                                                                               { Extensions.GetKey(HttpStatusCode.MethodNotAllowed, PushNotificationStatus.NotApplicable, DeviceConnectionStatus.NotApplicable, SubscriptionStatus.NotApplicable), StringResources.ResponseComments_405_NotApplicable_NotApplicable_NotApplicable },
                                                                                                               { Extensions.GetKey(HttpStatusCode.NotAcceptable, PushNotificationStatus.Dropped, DeviceConnectionStatus.Connected, SubscriptionStatus.Active), StringResources.ResponseComments_406_Dropped_Connected_Active },
                                                                                                               { Extensions.GetKey(HttpStatusCode.NotAcceptable, PushNotificationStatus.Dropped, DeviceConnectionStatus.TempDisconnected, SubscriptionStatus.Active), StringResources.ResponseComments_406_Dropped_TempDisconnected_Active },
                                                                                                               { Extensions.GetKey(HttpStatusCode.PreconditionFailed, PushNotificationStatus.Dropped, DeviceConnectionStatus.Inactive, SubscriptionStatus.NotApplicable), StringResources.ResponseComments_412_Dropped_Inactive_NotApplicable },
                                                                                                               { Extensions.GetKey(HttpStatusCode.ServiceUnavailable, PushNotificationStatus.NotApplicable, DeviceConnectionStatus.NotApplicable, SubscriptionStatus.NotApplicable), StringResources.ResponseComments_503_NotApplicable_NotApplicable_NotApplicable },
                                                                                                           };

        /// <summary>
        /// The Dictionary to get the 'X-WindowsPhone-Target' value based on the type of push notification.
        /// </summary>
        private static Dictionary<PushNotificationType, string> windowsPhoneTargetHeaderDictionary = new Dictionary<PushNotificationType, string>()
                                                                                                           {
                                                                                                               { PushNotificationType.Raw, String.Empty },
                                                                                                               { PushNotificationType.Tile, "token" },
                                                                                                               { PushNotificationType.Toast, "toast" },
                                                                                                           };

        /// <summary>
        /// The Dictionary to get the 'X-NotificationClass' value based on the type of push notification and the message priority.
        /// </summary>
        private static Dictionary<PushNotificationType, Dictionary<MessagePriority, string>> notificationClassHeaderDictionary = new Dictionary<PushNotificationType, Dictionary<MessagePriority, string>>()
                                                                                                                                     {
                                                                                                                                         { PushNotificationType.Tile, new Dictionary<MessagePriority, string>()
                                                                                                                                                                         {
                                                                                                                                                                            { MessagePriority.Realtime, "1" },
                                                                                                                                                                            { MessagePriority.Priority, "11" },
                                                                                                                                                                            { MessagePriority.Regular, "21" },
                                                                                                                                                                         }
                                                                                                                                         },
                                                                                                                                         { PushNotificationType.Toast, new Dictionary<MessagePriority, string>()
                                                                                                                                                                          {
                                                                                                                                                                             { MessagePriority.Realtime, "2" },
                                                                                                                                                                             { MessagePriority.Priority, "12" },
                                                                                                                                                                             { MessagePriority.Regular, "22" },
                                                                                                                                                                          }
                                                                                                                                         },
                                                                                                                                         { PushNotificationType.Raw, new Dictionary<MessagePriority, string>()
                                                                                                                                                                        {
                                                                                                                                                                           { MessagePriority.Realtime, "3" },
                                                                                                                                                                           { MessagePriority.Priority, "13" },
                                                                                                                                                                           { MessagePriority.Regular, "23" },
                                                                                                                                                                        }
                                                                                                                                         },
                                                                                                                                     };

        /// <summary>
        /// Converts the string value to the required enumeration value of type <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="enumValue">The string value of the enum.</param>
        /// <returns>The converted enum value of type <see cref="T"/>.</returns>
        public static T ToEnum<T>(this string enumValue)
        {
            var result = (T)Enum.Parse(typeof(T), enumValue, true);
            return result;
        }

        /// <summary>
        /// Converts the enumeration value to the equivalent 'X-WindowsPhone-Target' http header value required by the MPNS.
        /// </summary>
        /// <param name="pushNotificationType">The enumeration value to be converted.</param>
        /// <returns>The converted 'X-WindowsPhone-Target' value'.</returns>
        public static string ToWindowsPhoneTarget(this PushNotificationType pushNotificationType)
        {
            var result = Extensions.windowsPhoneTargetHeaderDictionary[pushNotificationType];
            return result;
        }

        /// <summary>
        /// Converts the enumeration value of <see cref="MessagePriority"/> to the equivalent 'X-NotificationClass' http header value required by the MPNS based on the type of push notification.
        /// </summary>
        /// <param name="messagePriority">The priority of the message.</param>
        /// <param name="pushNotificationType">The type of push notification.</param>
        /// <returns>The converted 'X-NotificationClass' value'.</returns>
        public static string ToNotificationClass(this MessagePriority messagePriority, PushNotificationType pushNotificationType)
        {
            var result = Extensions.notificationClassHeaderDictionary[pushNotificationType][messagePriority];
            return result;
        }

        /// <summary>
        /// Converts the name values into xml element values.
        /// </summary>
        /// <param name="nameValues">The dictionary of name values.</param>
        /// <param name="escapeInvalidCharacters">Indicates if the invalid characters should be escaped.</param>
        /// <returns>The concated name value pairs in xml format.</returns>
        public static string ToRawNotificationElementXml(this Dictionary<string, string> nameValues, bool escapeInvalidCharacters)
        {
            var result = String.Empty;

            foreach (var entry in nameValues)
            {
                result += String.Format(CultureInfo.InvariantCulture, "<{0}>{1}</{0}>", entry.Key, entry.Value.ToValidXmlCharacters(escapeInvalidCharacters));
            }

            return result;
        }

        /// <summary>
        /// Converts the value into an encoded form safe for notifications.
        /// </summary>
        /// <param name="value">The value to be encoded.</param>
        /// <param name="escapeInvalidCharacters">Indicates if the invalid characters should be escaped.</param>
        /// <returns>The encoded value.</returns>
        public static string ToValidXmlCharacters(this string value, bool escapeInvalidCharacters)
        {
            if (escapeInvalidCharacters && !String.IsNullOrWhiteSpace(value))
            {
                var escapedValue = SecurityElement.Escape(value);
                return escapedValue;
            }

            return value;
        }

        /// <summary>
        /// Gets a value indicating the possible set of reasons for the current response (Http Status + Notification Status + Device Connection Status + Subscription Status)/
        /// This message may also suggest the next steps in many cases.
        /// </summary>
        /// <param name="httpStatusCode">The 3 digit Code denoting the Notification Response Status of the web call.</param>
        /// <param name="pushNotificationStatus">The status of the notification received by the Microsoft Push Notification Service.</param>
        /// <param name="deviceConnectionStatus">The connection status of the device.</param>
        /// <param name="subscriptionStatus">The subscription status of the device.</param>
        /// <returns>The suggestion as a message.</returns>
        public static string GetComments(HttpStatusCode httpStatusCode, PushNotificationStatus pushNotificationStatus, DeviceConnectionStatus deviceConnectionStatus, SubscriptionStatus subscriptionStatus)
        {
            var message = String.Empty;
            var key = Extensions.GetKey(httpStatusCode, pushNotificationStatus, deviceConnectionStatus, subscriptionStatus);

            if (Extensions.pushNotificationServiceResponseComments.ContainsKey(key))
            {
                message = Extensions.pushNotificationServiceResponseComments[key];
            }

            return message;
        }

        /// <summary>
        /// Gets a composite key concatenating the values.
        /// </summary>
        /// <param name="httpStatusCode">The 3 digit Code denoting the Notification Response Status of the web call.</param>
        /// <param name="pushNotificationStatus">The status of the notification received by the Microsoft Push Notification Service.</param>
        /// <param name="deviceConnectionStatus">The connection status of the device.</param>
        /// <param name="subscriptionStatus">The subscription status of the device.</param>
        /// <returns>The resource key.</returns>
        private static string GetKey(HttpStatusCode httpStatusCode, PushNotificationStatus pushNotificationStatus, DeviceConnectionStatus deviceConnectionStatus, SubscriptionStatus subscriptionStatus)
        {
            var key = String.Format(CultureInfo.InvariantCulture, "ResponseComments_{0}_{1}_{2}_{3}", httpStatusCode, pushNotificationStatus, deviceConnectionStatus, subscriptionStatus);
            return key;
        }
    }
}
