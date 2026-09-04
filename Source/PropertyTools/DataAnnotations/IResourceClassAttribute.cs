// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IResourceClassAttribute.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies a resource class and its static property.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
    using System;

    /// <summary>
    /// Specifies a resource class.
    /// </summary>
    public interface IResourceClassAttribute
    {
        /// <summary>
        /// Gets the array of resource classes.
        /// </summary>
        /// <value>The array of resource class types.</value>
        Type[] ResourceClasses { get; }
    }
}