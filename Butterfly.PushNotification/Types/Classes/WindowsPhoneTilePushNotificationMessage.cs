// <copyright file="WindowsPhoneTilePushNotificationMessage.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'WindowsPhoneTilePushNotificationMessage' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents a Windows Phone Tile Notification Message.
    /// </summary>
    [Serializable]
    public partial class WindowsPhoneTilePushNotificationMessage : WindowsPhonePushNotificationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsPhoneTilePushNotificationMessage"/> class.
        /// </summary>
        public WindowsPhoneTilePushNotificationMessage()
        {
            this.Id = String.Empty;
        }

        /// <summary>
        /// Gets a value indicating the type of Push Notification.
        /// It is overridden to Tile Notification type.
        /// </summary>
        public override PushNotificationType PushNotificationType
        {
            get
            {
                return PushNotification.PushNotificationType.Tile;
            }
        }

        /// <summary>
        /// Gets or sets a value used to identify tiles in the case where there are multiple tiles pinned to the Start for a given app. 
        /// The default tile has an Id of “” or you can omit this attribute. 
        /// The Id of a tile also determines the page, and corresponding query string, to be navigated to when the user clicks on the tile.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the background image uri for this Tile Notification. 173 x 173 is recommended.
        /// </summary>
        public string BackgroundImageUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the count value for this Tile Notification.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the title text for this Tile Notification.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the back background image uri for this Tile Notification. 173 x 173 is recommended.
        /// Note: Supported on Windows Phone 7.1 and above only.
        /// </summary>
        public string BackBackgroundImageUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the back title text for this Tile Notification.
        /// Note: Supported on Windows Phone 7.1 and above only.
        /// </summary>
        public string BackTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the back content text for this Tile Notification.
        /// Note: Supported on Windows Phone 7.1 and above only.
        /// </summary>
        public string BackContent { get; set; }

        /// <summary>
        /// Creates the Xml Payload for the Tile Notification Message.
        /// </summary>
        /// <returns>The Byte form of the payload.</returns>
        public override byte[] CreatePayload()
        {
            var newXml = String.Empty;

            if (this.DeviceType == DeviceType.WindowsPhone7_1)
            {
                newXml = "<wp:BackTitle>" + this.BackTitle.ToValidXmlCharacters(this.EscapeInvalidCharacters) + "</wp:BackTitle>" +
                         "<wp:BackContent>" + this.BackContent.ToValidXmlCharacters(this.EscapeInvalidCharacters) + "</wp:BackContent>" +
                         "<wp:BackBackgroundImage>" + this.BackBackgroundImageUri.ToValidXmlCharacters(this.EscapeInvalidCharacters) + "</wp:BackBackgroundImage>";
            }

            var tileNotificationPayloadXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                             "<wp:Notification xmlns:wp=\"WPNotification\">" +
                                                 "<wp:Tile Id=\"" + this.Id ?? String.Empty + "\">" +
                                                     "<wp:BackgroundImage>" + this.BackgroundImageUri.ToValidXmlCharacters(this.EscapeInvalidCharacters) + "</wp:BackgroundImage>" +
                                                     "<wp:Count>" + this.Count + "</wp:Count>" +
                                                     "<wp:Title>" + this.Title.ToValidXmlCharacters(this.EscapeInvalidCharacters) + "</wp:Title>" + newXml +
                                                 "</wp:Tile> " +
                                             "</wp:Notification>";

            var tileNotificationPayload = Encoding.UTF8.GetBytes(tileNotificationPayloadXml);
            return tileNotificationPayload;
        }
    }
}
