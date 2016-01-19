// <copyright file="ValidationMessageType.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'ValidationMessageType' enumeration.
// </summary>
namespace Butterfly.PushNotification
{
    /// <summary>
    /// The type of validation message.
    /// </summary>
    public enum ValidationMessageType
    {
        /// <summary>
        /// we don't know yet.
        /// </summary>
        None = 0,

        /// <summary>
        /// This is a validation error and needs to be attended to.
        /// </summary>
        Error = 1,

        /// <summary>
        /// This is a validation warning and may or may not be serious. But it's good to have a look.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// This is just information. It can give additional context but should not block any workflow.
        /// </summary>
        Information = 3,
    }
}
