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
        /// Gets the resource class.
        /// </summary>
        /// <value>The resource class type.</value>
        Type ResourceClass { get; }
    }
}