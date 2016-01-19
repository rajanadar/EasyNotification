// <copyright file="Validator.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The '' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;
    using Butterfly.PushNotification.Resources;

    /// <summary>
    /// Represents a class to validate the notification messages.
    /// </summary>
    internal static partial class Validator
    {
        /// <summary>
        /// Validates the notification messages for the rules set forth by the Notification System. This may not guarantee that each and every rule will be picked up. e.g. the total message size is not done based on individual fields.
        /// But the basic ruls will be checked per field. This will eliminate a lot of trivial issues. 
        /// </summary>
        /// <typeparam name="T">A type of <see cref="PushNotificationMessage" />.</typeparam>
        /// <param name="pushNotificationMessage">The notification message of type <see cref="PushNotificationMessage" />.</param>
        /// <returns>The list of Validation Messages.</returns>
        public static ValidationResults Validate<T>(T pushNotificationMessage) where T : PushNotificationMessage
        {
            var vrs = new ValidationResults();

            if (pushNotificationMessage == null)
            {
                vrs.Add(new ValidationResult(StringResources.Code_0001));
                return vrs;
            }

            if (pushNotificationMessage.EnableValidation)
            {
                if (pushNotificationMessage.PushNotificationType != PushNotificationType.Raw
                    && pushNotificationMessage.PushNotificationType != PushNotificationType.Tile
                    && pushNotificationMessage.PushNotificationType != PushNotificationType.Toast)
                {
                    vrs.Add(new ValidationResult(StringResources.Code_0002));
                }

                if (String.IsNullOrWhiteSpace(pushNotificationMessage.NotificationUri))
                {
                    vrs.Add(new ValidationResult(StringResources.Code_0010));
                }

                switch (pushNotificationMessage.DeviceType)
                {
                    case DeviceType.WindowsPhone7_1:
                    case DeviceType.WindowsPhone7_0:

                        Validator.ValidateWindowsPhonePushNotificationMessage(pushNotificationMessage as WindowsPhonePushNotificationMessage, vrs);

                        if (vrs.IsValidWithWarnings)
                        {
                            switch (pushNotificationMessage.PushNotificationType)
                            {
                                case PushNotificationType.Tile:

                                    Validator.ValidateWindowsPhoneTilePushNotificationMessage(pushNotificationMessage as WindowsPhoneTilePushNotificationMessage, vrs);
                                    break;

                                case PushNotificationType.Toast:

                                    Validator.ValidateWindowsPhoneToastPushNotificationMessage(pushNotificationMessage as WindowsPhoneToastPushNotificationMessage, vrs);
                                    break;

                                case PushNotificationType.Raw:

                                    Validator.ValidateWindowsPhoneRawPushNotificationMessage(pushNotificationMessage as WindowsPhoneRawPushNotificationMessage, vrs);
                                    break;
                            }
                        }

                        break;
                }
            }

            return vrs;
        }

        /// <summary>
        /// Validates the notification messages for the rules set forth by the Notification System. This may not guarantee that each and every rule will be picked up. e.g. the total message size is not done based on individual fields.
        /// But the basic ruls will be checked per field. This will eliminate a lot of trivial issues. 
        /// </summary>
        /// <param name="windowsPhoneCallbackRegistrationRequest">The request of type <see cref="WindowsPhoneCallbackRegistrationRequest" />.</param>
        /// <returns>The list of Validation Messages.</returns>
        public static ValidationResults Validate(WindowsPhoneCallbackRegistrationRequest windowsPhoneCallbackRegistrationRequest)
        {
            var vrs = new ValidationResults();

            if (windowsPhoneCallbackRegistrationRequest == null)
            {
                vrs.Add(new ValidationResult(StringResources.Code_0012));
                return vrs;
            }

            if (windowsPhoneCallbackRegistrationRequest.EnableValidation)
            {
                if (String.IsNullOrWhiteSpace(windowsPhoneCallbackRegistrationRequest.NotificationUri))
                {
                    vrs.Add(new ValidationResult(StringResources.Code_0010));
                }

                if (String.IsNullOrWhiteSpace(windowsPhoneCallbackRegistrationRequest.CallbackUri))
                {
                    vrs.Add(new ValidationResult(StringResources.Code_0013));
                }

                if (windowsPhoneCallbackRegistrationRequest.CallbackMessage == null)
                {
                    vrs.Add(new ValidationResult(StringResources.Code_0014));
                }
                else
                {
                    if (windowsPhoneCallbackRegistrationRequest.CallbackMessage.Length > 1024)
                    {
                        vrs.Add(new ValidationResult(StringResources.Code_0015));
                    }
                }
            }

            return vrs;
        }

        /// <summary>
        /// Validates the notification messages for the rules pertaining to a common Windows Phone Notification Message.
        /// </summary>
        /// <param name="windowsPhonePushNotificationMessage">The notification message of type <see cref="windowsPhonePushNotificationMessage" />.</param>
        /// <param name="vrs">The Validation Results.</param>
        private static void ValidateWindowsPhonePushNotificationMessage(WindowsPhonePushNotificationMessage windowsPhonePushNotificationMessage, ValidationResults vrs)
        {
            if (windowsPhonePushNotificationMessage.AuthenticationSettings != null 
                && windowsPhonePushNotificationMessage.AuthenticationSettings.EnableAuthentication
                && windowsPhonePushNotificationMessage.AuthenticationSettings.X509Certificate == null)
            {
                vrs.Add(new ValidationResult(StringResources.Code_0009));
                return;
            }

            if (windowsPhonePushNotificationMessage.MessagePriority == MessagePriority.None)
            {
                vrs.Add(new ValidationResult(StringResources.Code_0011));
            }
        }

        /// <summary>
        /// Validates the Windows Phone Tile Notification Message for the rules set forth by the Notification System.
        /// </summary>
        /// <param name="windowsPhoneTilePushNotificationMessage">The notification message to be validated.</param>
        /// <param name="vrs">The uber validation results.</param>
        private static void ValidateWindowsPhoneTilePushNotificationMessage(WindowsPhoneTilePushNotificationMessage windowsPhoneTilePushNotificationMessage, ValidationResults vrs)
        {
            if (windowsPhoneTilePushNotificationMessage.DeviceType != DeviceType.WindowsPhone7_1)
            {
                if (!String.IsNullOrWhiteSpace(windowsPhoneTilePushNotificationMessage.BackBackgroundImageUri))
                {
                    vrs.Add(new ValidationResult()
                    {
                        ValidationMessageType = ValidationMessageType.Warning,
                        MessageWithCode = StringResources.Code_0005,
                    });
                }

                if (!String.IsNullOrWhiteSpace(windowsPhoneTilePushNotificationMessage.BackTitle))
                {
                    vrs.Add(new ValidationResult()
                    {
                        ValidationMessageType = ValidationMessageType.Warning,
                        MessageWithCode = StringResources.Code_0006,
                    });
                }

                if (!String.IsNullOrWhiteSpace(windowsPhoneTilePushNotificationMessage.BackContent))
                {
                    vrs.Add(new ValidationResult()
                    {
                        ValidationMessageType = ValidationMessageType.Warning,
                        MessageWithCode = StringResources.Code_0007,
                    });
                }
            }

            if (windowsPhoneTilePushNotificationMessage.Count > 99)
            {
                vrs.Add(new ValidationResult()
                {
                    ValidationMessageType = ValidationMessageType.Warning,
                    MessageWithCode = StringResources.Code_0008,
                });
            }
        }

        /// <summary>
        /// Validates the Windows Phone Toast Notification Message for the rules set forth by the Notification System.
        /// </summary>
        /// <param name="windowsPhoneToastPushNotificationMessage">The notification message to be validated.</param>
        /// <param name="vrs">The uber validation results.</param>
        private static void ValidateWindowsPhoneToastPushNotificationMessage(WindowsPhoneToastPushNotificationMessage windowsPhoneToastPushNotificationMessage, ValidationResults vrs)
        {
            if (windowsPhoneToastPushNotificationMessage.DeviceType != DeviceType.WindowsPhone7_1 && !String.IsNullOrWhiteSpace(windowsPhoneToastPushNotificationMessage.Param))
            {
                vrs.Add(new ValidationResult()
                {
                    ValidationMessageType = ValidationMessageType.Warning,
                    MessageWithCode = StringResources.Code_0004,
                });
            }

            if (windowsPhoneToastPushNotificationMessage.DeviceType == DeviceType.WindowsPhone7_1 && !String.IsNullOrWhiteSpace(windowsPhoneToastPushNotificationMessage.Param))
            {
                if (windowsPhoneToastPushNotificationMessage.Param.ToValidXmlCharacters(windowsPhoneToastPushNotificationMessage.EscapeInvalidCharacters).Length > 256)
                {
                    vrs.Add(new ValidationResult(StringResources.Code_0003));
                }
            }
        }

        /// <summary>
        /// Validates the Windows Phone Raw Notification Message for the rules set forth by the Notification System.
        /// </summary>
        /// <param name="windowsPhoneRawPushNotificationMessage">The notification message to be validated.</param>
        /// <param name="vrs">The uber validation results.</param>
        private static void ValidateWindowsPhoneRawPushNotificationMessage(WindowsPhoneRawPushNotificationMessage windowsPhoneRawPushNotificationMessage, ValidationResults vrs)
        {
            // i don't what rules apply for a whimsical payload. probably just the total size should not be a crazy number.
        }
    }
}
