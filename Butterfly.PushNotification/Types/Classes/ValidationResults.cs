// <copyright file="ValidationResults.cs" company="Butterfly">
// Copyright (c) Raja Nadar.  All rights reserved.
// </copyright>
// <summary>
// The 'ValidationResults' class.
// </summary>
namespace Butterfly.PushNotification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the list of Validation Messages.
    /// </summary>
    [Serializable]
    public partial class ValidationResults : List<ValidationResult>
    {
        /// <summary>
        /// Gets a value indicating whether any validation errors or warnings occured. Both errors and warnings are checked. To ignore warnings, please use the <see cref="IsValidWithWarnings" /> property.
        /// </summary>
        public bool IsCompletelyValid
        {
            get
            {
                var invalid = this.Any(vr => vr.ValidationMessageType == ValidationMessageType.Error || vr.ValidationMessageType == ValidationMessageType.Warning);
                return !invalid;
            }
        }

        /// <summary>
        /// Gets a value indicating whether any validation errors occured. This flag ignores warnings.
        /// </summary>
        public bool IsValidWithWarnings
        {
            get
            {
                var invalid = this.Any(vr => vr.ValidationMessageType == ValidationMessageType.Error);
                return !invalid;
            }
        }
    }
}
