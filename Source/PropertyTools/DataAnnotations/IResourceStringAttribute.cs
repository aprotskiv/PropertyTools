// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IResourceStringAttribute.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies a resource class and its static property to obtain resource string value.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
    /// <summary>
    /// Specifies a resource class and its static property.
    /// </summary>
    public interface IResourceStringAttribute : IResourceClassAttribute
    {
        /// <summary>
        /// Gets the static property of resource class.
        /// </summary>
        /// <value>The static property name.</value>
        string GetStaticProperty();
    }
}