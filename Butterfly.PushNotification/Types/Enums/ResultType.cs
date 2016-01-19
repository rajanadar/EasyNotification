// <copyright file="ResultType.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'ResultType' enumeration.
// </summary>
namespace Butterfly.PushNotification
{
    /// <summary>
    /// The Result of any operation.
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// we don't know yet.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The operation succeeded.
        /// </summary>
        Success = 1,

        /// <summary>
        /// The operation has been queued. The success or failure of the operation cannot be determined at this point.
        /// However the queueing was successful.
        /// </summary>
        Queued = 2,

        /// <summary>
        /// The operation failed.
        /// </summary>
        Failed = 3,
    }
}
