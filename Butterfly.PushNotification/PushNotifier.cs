// <copyright file="PushNotifier.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The '' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;
    using System.Net;

    /// <summary>
    /// Represents the worker class to send the actual notification messages.
    /// </summary>
    public static partial class PushNotifier
    {
        /// <summary>
        ///  Represents the worker method to send the actual notification message in an async manner.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="PushNotificationMessage"/>.</typeparam>
        /// <param name="pushNotificationMessage">The notification message to send.</param>
        /// <param name="callback">The callback delegate to call once the async operation completes.</param>
        public static void SendPushNotificationMessageAsync<T>(T pushNotificationMessage, Action<PushNotificationSendResult> callback) where T : PushNotificationMessage
        {
            Func<T, PushNotificationSendResult> method = PushNotifier.SendPushNotificationMessage;

            method.BeginInvoke(
                               pushNotificationMessage,
                               (asyncResult) =>
                               {
                                   var m = asyncResult.AsyncState as Func<T, PushNotificationSendResult>;

                                   var result = m.EndInvoke(asyncResult);
                                   callback(result);
                               },
                               method);
        }

        /// <summary>
        /// Represents the worker method to send the actual notification message.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="PushNotificationMessage"/>.</typeparam>
        /// <param name="pushNotificationMessage">The notification message to send.</param>
        /// <returns>The response of the Notification Send operation.</returns>
        public static PushNotificationSendResult SendPushNotificationMessage<T>(T pushNotificationMessage) where T : PushNotificationMessage
        {
            var pushNotificationSendResult = new PushNotificationSendResult()
            {
                OperationStartUtcDateTime = DateTime.UtcNow,
            };

            try
            {
                if (pushNotificationMessage != null)
                {
                    pushNotificationSendResult.DeviceType = pushNotificationMessage.DeviceType;
                    pushNotificationSendResult.PushNotificationType = pushNotificationMessage.PushNotificationType;
                }

                pushNotificationSendResult.ValidationResults = Validator.Validate(pushNotificationMessage);

                if (pushNotificationSendResult.ValidationResults.IsValidWithWarnings)
                {
                    switch (pushNotificationMessage.DeviceType)
                    {
                        case DeviceType.WindowsPhone7_1:
                        case DeviceType.WindowsPhone7_0:

                            var windowsPhonePushNotificationMessage = pushNotificationMessage as WindowsPhonePushNotificationMessage;

                            var pushNotificationWebRequest = (HttpWebRequest)WebRequest.Create(windowsPhonePushNotificationMessage.NotificationUri);

                            pushNotificationWebRequest.Method = "POST";
                            pushNotificationWebRequest.ContentType = "text/xml";

                            var messageId = Guid.NewGuid();

                            pushNotificationSendResult.MessageId = messageId;
                            pushNotificationWebRequest.Headers.Add("X-MessageID", messageId.ToString());

                            var windowsPhoneTargetHeader = windowsPhonePushNotificationMessage.PushNotificationType.ToWindowsPhoneTarget();
                            pushNotificationWebRequest.Headers["X-WindowsPhone-Target"] = windowsPhoneTargetHeader;

                            var notificationClass = windowsPhonePushNotificationMessage.MessagePriority.ToNotificationClass(windowsPhonePushNotificationMessage.PushNotificationType);
                            pushNotificationWebRequest.Headers.Add("X-NotificationClass", notificationClass);

                            var notificationPayload = windowsPhonePushNotificationMessage.CreatePayload();
                            pushNotificationWebRequest.ContentLength = notificationPayload.Length;

                            if (windowsPhonePushNotificationMessage.AuthenticationSettings != null && windowsPhonePushNotificationMessage.AuthenticationSettings.EnableAuthentication)
                            {
                                pushNotificationWebRequest.ClientCertificates.Add(windowsPhonePushNotificationMessage.AuthenticationSettings.X509Certificate);
                            }

                            using (var notificationRequestStream = pushNotificationWebRequest.GetRequestStream())
                            {
                                notificationRequestStream.Write(notificationPayload, 0, notificationPayload.Length);
                            }

                            using (var pushNotificationWebResponse = (HttpWebResponse)pushNotificationWebRequest.GetResponse())
                            {
                                PushNotifier.SetNotificationWebResponseValuesToResult(pushNotificationSendResult, pushNotificationWebResponse);
                            }

                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                pushNotificationSendResult.OperationResult = ResultType.Failed;
                pushNotificationSendResult.RawException = exception;

                var webException = exception as WebException;

                if (webException != null && webException.Response != null)
                {
                    var httpWebResponse = webException.Response as HttpWebResponse;

                    if (httpWebResponse != null)
                    {
                        PushNotifier.SetNotificationWebResponseValuesToResult(pushNotificationSendResult, httpWebResponse);
                    }
                }
            }
            finally
            {
                pushNotificationSendResult.OperationEndUtcDateTime = DateTime.UtcNow;
            }

            return pushNotificationSendResult;
        }

        /// <summary>
        ///  Represents the worker method to send the callback registration request in an async manner.
        /// </summary>
        /// <param name="windowsPhoneCallbackRegistrationRequest">The callback registration request that associates a callback URI and message with a subscription.</param>
        /// <param name="callback">The callback delegate to call once the async operation completes.</param>
        public static void RegisterCallbackAsync(WindowsPhoneCallbackRegistrationRequest windowsPhoneCallbackRegistrationRequest, Action<WindowsPhoneCallbackRegistrationResponse> callback)
        {
            Func<WindowsPhoneCallbackRegistrationRequest, WindowsPhoneCallbackRegistrationResponse> method = PushNotifier.RegisterCallback;

            method.BeginInvoke(
                               windowsPhoneCallbackRegistrationRequest,
                               (asyncResult) =>
                               {
                                   var m = asyncResult.AsyncState as Func<WindowsPhoneCallbackRegistrationRequest, WindowsPhoneCallbackRegistrationResponse>;

                                   var result = m.EndInvoke(asyncResult);
                                   callback(result);
                               },
                               method);
        }

        /// <summary>
        /// Represents the worker method to send the callback registration request.
        /// </summary>
        /// <param name="windowsPhoneCallbackRegistrationRequest">The callback registration request that associates a callback URI and message with a subscription.</param>
        /// <returns>The response of the Windows Phone Callback Registration Request.</returns>
        public static WindowsPhoneCallbackRegistrationResponse RegisterCallback(WindowsPhoneCallbackRegistrationRequest windowsPhoneCallbackRegistrationRequest)
        {
            var windowsPhoneCallbackRegistrationResponse = new WindowsPhoneCallbackRegistrationResponse()
            {
                OperationStartUtcDateTime = DateTime.UtcNow,
            };

            try
            {
                windowsPhoneCallbackRegistrationResponse.ValidationResults = Validator.Validate(windowsPhoneCallbackRegistrationRequest);

                if (windowsPhoneCallbackRegistrationResponse.ValidationResults.IsValidWithWarnings)
                {
                    var windowsPhoneCallbackRegistrationWebRequest = (HttpWebRequest)WebRequest.Create(windowsPhoneCallbackRegistrationRequest.NotificationUri);

                    windowsPhoneCallbackRegistrationWebRequest.Headers.Add("X-CallbackURI", windowsPhoneCallbackRegistrationRequest.CallbackUri);
                    windowsPhoneCallbackRegistrationWebRequest.Method = "POST";

                    windowsPhoneCallbackRegistrationWebRequest.ContentType = "application/*";
                    windowsPhoneCallbackRegistrationWebRequest.ContentLength = windowsPhoneCallbackRegistrationRequest.CallbackMessage.Length;

                    using (var notificationRequestStream = windowsPhoneCallbackRegistrationWebRequest.GetRequestStream())
                    {
                        notificationRequestStream.Write(windowsPhoneCallbackRegistrationRequest.CallbackMessage, 0, windowsPhoneCallbackRegistrationRequest.CallbackMessage.Length);
                    }

                    using (var windowsPhoneCallbackRegistrationWebResponse = (HttpWebResponse)windowsPhoneCallbackRegistrationWebRequest.GetResponse())
                    {
                        PushNotifier.SetCallbackRequestWebResponseValuesToResult(windowsPhoneCallbackRegistrationResponse, windowsPhoneCallbackRegistrationWebResponse);
                    }
                }
            }
            catch (Exception exception)
            {
                windowsPhoneCallbackRegistrationResponse.OperationResult = ResultType.Failed;
                windowsPhoneCallbackRegistrationResponse.RawException = exception;

                var webException = exception as WebException;

                if (webException != null && webException.Response != null)
                {
                    var httpWebResponse = webException.Response as HttpWebResponse;

                    if (httpWebResponse != null)
                    {
                        PushNotifier.SetCallbackRequestWebResponseValuesToResult(windowsPhoneCallbackRegistrationResponse, httpWebResponse);
                    }
                }
            }
            finally
            {
                windowsPhoneCallbackRegistrationResponse.OperationEndUtcDateTime = DateTime.UtcNow;
            }

            return windowsPhoneCallbackRegistrationResponse;
        }

        /// <summary>
        /// Sets the <see cref="HttpWebResponse"/> values of callback request into our <see cref="WindowsPhoneCallbackRegistrationResponse"/> object.
        /// </summary>
        /// <param name="windowsPhoneCallbackRegistrationResponse">The result of the Callback Request operation.</param>
        /// <param name="httpWebResponse">The web response of the Callback Request operation.</param>
        private static void SetCallbackRequestWebResponseValuesToResult(WindowsPhoneCallbackRegistrationResponse windowsPhoneCallbackRegistrationResponse, HttpWebResponse httpWebResponse)
        {
            windowsPhoneCallbackRegistrationResponse.HttpStatusCode = httpWebResponse.StatusCode;

            var subscriptionStatus = httpWebResponse.Headers["X-SubscriptionStatus"];
            var deviceConnectionStatus = httpWebResponse.Headers["X-DeviceConnectionStatus"];

            if (!String.IsNullOrWhiteSpace(subscriptionStatus))
            {
                windowsPhoneCallbackRegistrationResponse.CallbackSubscriptionStatus = subscriptionStatus.ToEnum<SubscriptionStatus>();
            }

            if (!String.IsNullOrWhiteSpace(deviceConnectionStatus))
            {
                windowsPhoneCallbackRegistrationResponse.DeviceConnectionStatus = deviceConnectionStatus.ToEnum<DeviceConnectionStatus>();
            }
        }

        /// <summary>
        /// Sets the <see cref="HttpWebResponse"/> values of notification into our <see cref="PushNotificationSendResult"/> object.
        /// </summary>
        /// <param name="pushNotificationSendResult">The result of the Notification Send operation.</param>
        /// <param name="httpWebResponse">The web response of the Notification Send operation.</param>
        private static void SetNotificationWebResponseValuesToResult(PushNotificationSendResult pushNotificationSendResult, HttpWebResponse httpWebResponse)
        {
            pushNotificationSendResult.HttpStatusCode = httpWebResponse.StatusCode;

            var pushNotificationStatus = httpWebResponse.Headers["X-NotificationStatus"];
            var pushNotificationSubscriptionStatus = httpWebResponse.Headers["X-SubscriptionStatus"];
            var deviceConnectionStatus = httpWebResponse.Headers["X-DeviceConnectionStatus"];

            if (!String.IsNullOrWhiteSpace(pushNotificationStatus))
            {
                pushNotificationSendResult.PushNotificationStatus = pushNotificationStatus.ToEnum<PushNotificationStatus>();
            }

            if (!String.IsNullOrWhiteSpace(pushNotificationSubscriptionStatus))
            {
                pushNotificationSendResult.PushNotificationSubscriptionStatus = pushNotificationSubscriptionStatus.ToEnum<SubscriptionStatus>();
            }

            if (!String.IsNullOrWhiteSpace(deviceConnectionStatus))
            {
                pushNotificationSendResult.DeviceConnectionStatus = deviceConnectionStatus.ToEnum<DeviceConnectionStatus>();
            }
        }
    }
}
