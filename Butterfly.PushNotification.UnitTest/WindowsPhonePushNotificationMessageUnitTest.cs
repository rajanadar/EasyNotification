
namespace Butterfly.PushNotification.UnitTest
{
    using System;
    using System.Linq;
    using System.Net;
    using Butterfly.PushNotification;
    using Butterfly.PushNotification.Resources;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WindowsPhonePushNotificationMessageUnitTest
    {
        private static string ChannelUri = "http://sn1.notify.live.net/throttledthirdparty/01.00/AAGzPwlglj32Tb0TrwbNIClIAgAAAAADAQAAAAQUZm52OjIzOEQ2NDJDRkI5MEVFMEQ";

        #region Private helper methods.

        private static void AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(PushNotificationSendResult result, ResultType resultType, bool completelyValid, bool validWithWarnings, string code = null)
        {
            Assert.AreEqual(result.OperationResult, resultType);
            Assert.AreEqual(result.ValidationResults.IsCompletelyValid, completelyValid);
            Assert.AreEqual(result.ValidationResults.IsValidWithWarnings, validWithWarnings);

            if (!String.IsNullOrWhiteSpace(code))
            {
                Assert.IsTrue(result.ValidationResults.Any(vr => vr.MessageCode == code));
            }
        }

        private static void AssertOperationResultValidationResultsMessageCodeOnWindowsPhoneCallbackRegistrationResponse(WindowsPhoneCallbackRegistrationResponse response, ResultType resultType, bool completelyValid, bool validWithWarnings, string code = null)
        {
            Assert.AreEqual(response.OperationResult, resultType);
            Assert.AreEqual(response.ValidationResults.IsCompletelyValid, completelyValid);
            Assert.AreEqual(response.ValidationResults.IsValidWithWarnings, validWithWarnings);

            if (!String.IsNullOrWhiteSpace(code))
            {
                Assert.IsTrue(response.ValidationResults.Any(vr => vr.MessageCode == code));
            }
        }

        private static WindowsPhoneTilePushNotificationMessage GetValidWindowsPhoneTilePushNotificationMessage()
        {
            var message = new WindowsPhoneTilePushNotificationMessage()
            {
                Count = 98,
                NotificationUri = WindowsPhonePushNotificationMessageUnitTest.ChannelUri,
                Title = "Tile",
            };

            return message;
        }

        private static WindowsPhoneToastPushNotificationMessage GetValidWindowsPhoneToastPushNotificationMessage()
        {
            var message = new WindowsPhoneToastPushNotificationMessage()
            {
                NotificationUri = WindowsPhonePushNotificationMessageUnitTest.ChannelUri,
                Text1 = "Toast Header",
                Text2 = "Toast Text",
            };

            return message;
        }

        private static WindowsPhoneRawPushNotificationMessage GetValidWindowsPhoneRawPushNotificationMessage()
        {
            var message = new WindowsPhoneRawPushNotificationMessage()
            {
                NotificationUri = WindowsPhonePushNotificationMessageUnitTest.ChannelUri,
                NameValues = new System.Collections.Generic.Dictionary<string, string>()
                {
                {"Value1", "Raw Header"},
                {"Value2", "Raw Text"},
                },
            };

            return message;
        }

        private static WindowsPhoneCallbackRegistrationRequest GetValidWindowsPhoneCallbackRegistrationRequest()
        {
            var request = new WindowsPhoneCallbackRegistrationRequest()
            {
                CallbackUri = "http://easynotification.codeplex.com/",
                NotificationUri = WindowsPhonePushNotificationMessageUnitTest.ChannelUri,
            };

            request.CallbackMessage = System.Text.Encoding.Unicode.GetBytes("CallbackMessagePayload");

            return request;
        }

        #endregion

        #region Push Notification Message Response Comments Test Cases

        private void ResponseComments_Test(HttpStatusCode httpStatusCode, PushNotificationStatus pushNotificationStatus, DeviceConnectionStatus deviceConnectionStatus, SubscriptionStatus subscriptionStatus, string value)
        {
            var result = new PushNotificationSendResult()
            {
                HttpStatusCode = httpStatusCode,
                PushNotificationStatus = pushNotificationStatus,
                DeviceConnectionStatus = deviceConnectionStatus,
                PushNotificationSubscriptionStatus = subscriptionStatus,
            };

            Assert.AreEqual(result.Comments, value);
        }

        [TestMethod]
        public void ResponseComments_200_Received_Connected_Active_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.OK, PushNotificationStatus.Received, DeviceConnectionStatus.Connected, SubscriptionStatus.Active, StringResources.ResponseComments_200_Received_Connected_Active);
        }

        [TestMethod]
        public void ResponseComments_200_Received_TempDisconnected_Active_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.OK, PushNotificationStatus.Received, DeviceConnectionStatus.TempDisconnected, SubscriptionStatus.Active, StringResources.ResponseComments_200_Received_TempDisconnected_Active);
        }

        [TestMethod]
        public void ResponseComments_200_QueueFull_Connected_Active_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.OK, PushNotificationStatus.QueueFull, DeviceConnectionStatus.Connected, SubscriptionStatus.Active, StringResources.ResponseComments_200_QueueFull_Connected_Active);
        }

        [TestMethod]
        public void ResponseComments_200_QueueFull_TempDisconnected_Active_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.OK, PushNotificationStatus.QueueFull, DeviceConnectionStatus.TempDisconnected, SubscriptionStatus.Active, StringResources.ResponseComments_200_QueueFull_TempDisconnected_Active);
        }

        [TestMethod]
        public void ResponseComments_200_Suppressed_Connected_Active_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.OK, PushNotificationStatus.Suppressed, DeviceConnectionStatus.Connected, SubscriptionStatus.Active, StringResources.ResponseComments_200_Suppressed_Connected_Active);
        }

        [TestMethod]
        public void ResponseComments_200_Suppressed_TempDisconnected_Active_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.OK, PushNotificationStatus.Suppressed, DeviceConnectionStatus.TempDisconnected, SubscriptionStatus.Active, StringResources.ResponseComments_200_Suppressed_TempDisconnected_Active);
        }

        [TestMethod]
        public void ResponseComments_400_NotApplicable_NotApplicable_NotApplicable_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.BadRequest, PushNotificationStatus.NotApplicable, DeviceConnectionStatus.NotApplicable, SubscriptionStatus.NotApplicable, StringResources.ResponseComments_400_NotApplicable_NotApplicable_NotApplicable);
        }

        [TestMethod]
        public void ResponseComments_401_NotApplicable_NotApplicable_NotApplicable_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.Unauthorized, PushNotificationStatus.NotApplicable, DeviceConnectionStatus.NotApplicable, SubscriptionStatus.NotApplicable, StringResources.ResponseComments_401_NotApplicable_NotApplicable_NotApplicable);
        }

        [TestMethod]
        public void ResponseComments_404_Dropped_Connected_Expired_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.NotFound, PushNotificationStatus.Dropped, DeviceConnectionStatus.Connected, SubscriptionStatus.Expired, StringResources.ResponseComments_404_Dropped_Connected_Expired);
        }

        [TestMethod]
        public void ResponseComments_404_Dropped_TempDisconnected_Expired_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.NotFound, PushNotificationStatus.Dropped, DeviceConnectionStatus.TempDisconnected, SubscriptionStatus.Expired, StringResources.ResponseComments_404_Dropped_TempDisconnected_Expired);
        }

        [TestMethod]
        public void ResponseComments_404_Dropped_Disconnected_Expired_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.NotFound, PushNotificationStatus.Dropped, DeviceConnectionStatus.Disconnected, SubscriptionStatus.Expired, StringResources.ResponseComments_404_Dropped_Disconnected_Expired);
        }

        [TestMethod]
        public void ResponseComments_405_NotApplicable_NotApplicable_NotApplicable_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.MethodNotAllowed, PushNotificationStatus.NotApplicable, DeviceConnectionStatus.NotApplicable, SubscriptionStatus.NotApplicable, StringResources.ResponseComments_405_NotApplicable_NotApplicable_NotApplicable);
        }

        [TestMethod]
        public void ResponseComments_406_Dropped_Connected_Active_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.NotAcceptable, PushNotificationStatus.Dropped, DeviceConnectionStatus.Connected, SubscriptionStatus.Active, StringResources.ResponseComments_406_Dropped_Connected_Active);
        }

        [TestMethod]
        public void ResponseComments_406_Dropped_TempDisconnected_Active_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.NotAcceptable, PushNotificationStatus.Dropped, DeviceConnectionStatus.TempDisconnected, SubscriptionStatus.Active, StringResources.ResponseComments_406_Dropped_TempDisconnected_Active);
        }

        [TestMethod]
        public void ResponseComments_412_Dropped_Inactive_NotApplicable_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.PreconditionFailed, PushNotificationStatus.Dropped, DeviceConnectionStatus.Inactive, SubscriptionStatus.NotApplicable, StringResources.ResponseComments_412_Dropped_Inactive_NotApplicable);
        }

        [TestMethod]
        public void ResponseComments_503_NotApplicable_NotApplicable_NotApplicable_Test()
        {
            this.ResponseComments_Test(HttpStatusCode.ServiceUnavailable, PushNotificationStatus.NotApplicable, DeviceConnectionStatus.NotApplicable, SubscriptionStatus.NotApplicable, StringResources.ResponseComments_503_NotApplicable_NotApplicable_NotApplicable);
        }

        #endregion

        #region Push Notification Message Validation test cases

        [TestMethod]
        public void WindowsPhonePushNotificationMessageNullTest()
        {
            PushNotificationMessage message = null; // it is interesting that an abstract class can be set to null. never knew this.

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, false, false, "0001");
        }

        [TestMethod]
        public void WindowsPhonePushNotificationMessageNotificationUriNullTest()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneToastPushNotificationMessage();
            message.NotificationUri = null;

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, false, false, "0010");
        }

        [TestMethod]
        public void WindowsPhonePushNotificationMessageX509CertificateNullTest()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneToastPushNotificationMessage();
            message.AuthenticationSettings = new WindowsPhoneAuthenticationSettings()
            {
                EnableAuthentication = true,
                X509Certificate = null,
            };

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, false, false, "0009");
        }

        [TestMethod]
        public void WindowsPhonePushNotificationMessageMessagePriorityNoneTest()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneToastPushNotificationMessage();
            message.MessagePriority = MessagePriority.None;

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, false, false, "0011");
        }

        [TestMethod]
        public void WindowsPhoneTilePushNotificationMessageCountGreaterThan99Test()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneTilePushNotificationMessage();
            message.Count = 100;

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, false, true, "0008");
        }

        [TestMethod]
        public void WindowsPhoneTilePushNotificationMessageBackBackgroundImageUriForWindowsPhone7_0Test()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneTilePushNotificationMessage();

            message.SupportWindowsPhone7_0 = true;
            message.BackBackgroundImageUri = "http://download-codeplex.sec.s-msft.com/download/Avatar.ashx?DownloadId=266566";

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, false, true, "0005");
        }

        [TestMethod]
        public void WindowsPhoneTilePushNotificationMessageBackTitleForWindowsPhone7_0Test()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneTilePushNotificationMessage();

            message.SupportWindowsPhone7_0 = true;
            message.BackTitle = "Back Title - Image";

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, false, true, "0006");
        }

        [TestMethod]
        public void WindowsPhoneTilePushNotificationMessageBackContentForWindowsPhone7_0Test()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneTilePushNotificationMessage();

            message.SupportWindowsPhone7_0 = true;
            message.BackContent = "Back Content - Profile Image";

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, false, true, "0007");
        }

        [TestMethod]
        public void WindowsPhoneToastPushNotificationMessageParamForWindowsPhone7_0Test()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneToastPushNotificationMessage();

            message.SupportWindowsPhone7_0 = true;
            message.Param = "/MainPage.xaml";

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, false, true, "0004");
        }

        [TestMethod]
        public void WindowsPhoneToastPushNotificationMessageParamLengthGreaterThan256Test()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneToastPushNotificationMessage();

            message.Param = "/MainPage.xaml?v=" + new String('a', 256);

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, false, false, "0003");
        }

        #endregion

        #region Sync Push Notification Message Sending Test Cases

        [TestMethod]
        public void SendWindowsPhoneRawPushNotificationMessageTest()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneRawPushNotificationMessage();
            
            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
        }

        [TestMethod]
        public void SendWindowsPhoneToastPushNotificationMessageTest()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneToastPushNotificationMessage();

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
        }

        [TestMethod]
        public void SendWindowsPhoneTilePushNotificationMessageTest()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneTilePushNotificationMessage();
            message.SupportWindowsPhone7_0 = true;

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
        }

        [TestMethod]
        public void SendWindowsPhone7RawPushNotificationMessageTest()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneRawPushNotificationMessage();
            message.SupportWindowsPhone7_0 = true;

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
        }

        [TestMethod]
        public void SendWindowsPhone7ToastPushNotificationMessageTest()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneToastPushNotificationMessage();
            message.SupportWindowsPhone7_0 = true;

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
        }

        [TestMethod]
        public void SendWindowsPhone7TilePushNotificationMessageTest()
        {
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneTilePushNotificationMessage();

            var result = PushNotifier.SendPushNotificationMessage(message);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
        }

        #endregion

        #region Async Push Notification Message Sending Test Cases

        [TestMethod]
        public void SendAsyncWindowsPhoneRawPushNotificationMessageTest()
        {
            var returned = false;
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneRawPushNotificationMessage();

            PushNotifier.SendPushNotificationMessageAsync(message, (result) =>
               {
                   WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
                   returned = true;
               });

            while (!returned) { System.Threading.Thread.Sleep(500); }
        }

        [TestMethod]
        public void SendAsyncWindowsPhoneToastPushNotificationMessageTest()
        {
            var returned = false;
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneToastPushNotificationMessage();

            PushNotifier.SendPushNotificationMessageAsync(message, (result) =>
                {
                    WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
                    returned = true;
                });

            while (!returned) { System.Threading.Thread.Sleep(500); }
        }

        [TestMethod]
        public void SendAsyncWindowsPhoneTilePushNotificationMessageTest()
        {
            var returned = false;
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneTilePushNotificationMessage();

            PushNotifier.SendPushNotificationMessageAsync(message, (result) =>
               {
                   WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
                   returned = true;
               });

            while (!returned) { System.Threading.Thread.Sleep(500); }
        }

        [TestMethod]
        public void SendAsyncWindowsPhone7RawPushNotificationMessageTest()
        {
            var returned = false;
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneRawPushNotificationMessage();
            message.SupportWindowsPhone7_0 = true;

            PushNotifier.SendPushNotificationMessageAsync(message, (result) =>
            {
                WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
                returned = true;
            });

            while (!returned) { System.Threading.Thread.Sleep(500); }
        }

        [TestMethod]
        public void SendAsyncWindowsPhone7ToastPushNotificationMessageTest()
        {
            var returned = false;
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneToastPushNotificationMessage();
            message.SupportWindowsPhone7_0 = true;

            PushNotifier.SendPushNotificationMessageAsync(message, (result) =>
            {
                WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
                returned = true;
            });

            while (!returned) { System.Threading.Thread.Sleep(500); }
        }

        [TestMethod]
        public void SendAsyncWindowsPhone7TilePushNotificationMessageTest()
        {
            var returned = false;
            var message = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneTilePushNotificationMessage();
            message.SupportWindowsPhone7_0 = true;

            PushNotifier.SendPushNotificationMessageAsync(message, (result) =>
            {
                WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnPushNotificationSendResult(result, ResultType.Success, true, true);
                returned = true;
            });

            while (!returned) { System.Threading.Thread.Sleep(500); }
        }

        #endregion

        #region Callback Request Validation test cases

        [TestMethod]
        public void WindowsPhoneCallbackRegistrationRequestNullTest()
        {
            WindowsPhoneCallbackRegistrationRequest request = null;

            var result = PushNotifier.RegisterCallback(request);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnWindowsPhoneCallbackRegistrationResponse(result, ResultType.Success, false, false, "0012");
        }

        [TestMethod]
        public void WindowsPhoneCallbackRegistrationRequestNotificationUriNullTest()
        {
            var request = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneCallbackRegistrationRequest();
            request.NotificationUri = null;

            var result = PushNotifier.RegisterCallback(request);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnWindowsPhoneCallbackRegistrationResponse(result, ResultType.Success, false, false, "0010");
        }

        [TestMethod]
        public void WindowsPhoneCallbackRegistrationRequestCallbackUriNullTest()
        {
            var request = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneCallbackRegistrationRequest();
            request.CallbackUri = null;

            var result = PushNotifier.RegisterCallback(request);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnWindowsPhoneCallbackRegistrationResponse(result, ResultType.Success, false, false, "0013");
        }

        [TestMethod]
        public void WindowsPhoneCallbackRegistrationRequestCallbackMessageNullTest()
        {
            var request = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneCallbackRegistrationRequest();
            request.CallbackMessage = null;

            var result = PushNotifier.RegisterCallback(request);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnWindowsPhoneCallbackRegistrationResponse(result, ResultType.Success, false, false, "0014");
        }

        [TestMethod]
        public void WindowsPhoneCallbackRegistrationRequestCallbackMessageLengthGreaterThan1024Test()
        {
            var request = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneCallbackRegistrationRequest();
            request.CallbackMessage = System.Text.Encoding.Unicode.GetBytes(new String('d', 1030));

            var result = PushNotifier.RegisterCallback(request);
            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnWindowsPhoneCallbackRegistrationResponse(result, ResultType.Success, false, false, "0015");
        }

        #endregion

        #region Sync Callback Request Sending Test Cases

        [TestMethod]
        public void SendWindowsPhoneCallbackRegistrationRequestTest()
        {
            var request = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneCallbackRegistrationRequest();
            var result = PushNotifier.RegisterCallback(request);

            WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnWindowsPhoneCallbackRegistrationResponse(result, ResultType.Success, true, true);
        }

        #endregion

        #region Async Callback Request Sending Test Cases

        [TestMethod]
        public void SendAsyncWindowsPhoneCallbackRegistrationRequestTest()
        {
            var returned = false;
            var request = WindowsPhonePushNotificationMessageUnitTest.GetValidWindowsPhoneCallbackRegistrationRequest();

            PushNotifier.RegisterCallbackAsync(request, (result) =>
            {
                WindowsPhonePushNotificationMessageUnitTest.AssertOperationResultValidationResultsMessageCodeOnWindowsPhoneCallbackRegistrationResponse(result, ResultType.Success, true, true);
                returned = true;
            });

            while (!returned) { System.Threading.Thread.Sleep(500); }
        }

        #endregion
    }
}
