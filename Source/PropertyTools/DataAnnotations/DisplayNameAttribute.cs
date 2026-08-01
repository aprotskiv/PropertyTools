// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisplayNameAttribute.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies the display name for a property, event, or public void method which takes no arguments.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
    using System;

    /// <summary>
    /// Specifies the display name for a property or field (enum member).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DisplayNameAttribute : Attribute, IResourceStringAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameAttribute"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        public DisplayNameAttribute(string displayName)
        {
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameAttribute"/> class.
        /// </summary>
        /// <param name="resourceClass">The resource class.</param>
        /// <param name="staticPropertyForDisplayName">The static property name of resource class.</param>
        public DisplayNameAttribute(Type resourceClass, string staticPropertyForDisplayName)
        {
            this.ResourceClass = resourceClass;
            this.DisplayName = staticPropertyForDisplayName;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public virtual string DisplayName { get; private set; }

        /// <summary>
        /// Gets the resource class.
        /// </summary>
        /// <value>The resource class type.</value>
        public Type ResourceClass { get; }

        /// <summary>
        /// Gets the static property of resource class.
        /// </summary>
        /// <value>The static property name.</value>
        public string GetStaticProperty()
        {
            return DisplayName;
        }
    }
}