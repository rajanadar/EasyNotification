// <copyright file="ValidationResult.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'ValidationResult' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;

    /// <summary>
    /// The response to a validation operation.
    /// </summary>
    [Serializable]
    public partial class ValidationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        public ValidationResult()
        {
            this.ValidationMessageType = ValidationMessageType.Error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class with the given message.
        /// </summary>
        /// <param name="message">The validation message.</param>
        public ValidationResult(string message)
            : this()
        {
            this.MessageWithCode = message;
        }

        /// <summary>
        /// Gets or sets the type of Validation message.
        /// </summary>
        public ValidationMessageType ValidationMessageType { get; set; }

        /// <summary>
        /// Gets the Message Code to identify the message.
        /// </summary>
        public string MessageCode
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.MessageWithCode))
                {
                    var code = this.MessageWithCode.Substring(0, this.MessageWithCode.IndexOf("_"));
                    return code;
                }

                return String.Empty;
            }
        }

        /// <summary>
        /// Gets the Message without the code.
        /// </summary>
        public string Message
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.MessageWithCode))
                {
                    var index = this.MessageWithCode.IndexOf("_");

                    if (index > 0)
                    {
                        var message = this.MessageWithCode.Substring(index + 1, this.MessageWithCode.Length);
                        return message;
                    }
                }

                return String.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the validation message itself.
        /// </summary>
        internal string MessageWithCode { get; set; }
    }
}
