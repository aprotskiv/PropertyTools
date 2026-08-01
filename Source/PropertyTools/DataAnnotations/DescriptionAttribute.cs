// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DescriptionAttribute.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies a description for a property, event or class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
    using System;

    /// <summary>
    /// Specifies a description for a property, event or class.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : AbstractAttribute, IResourceStringAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DescriptionAttribute"/> class.
        /// </summary>
        /// <param name="resourceClass">The resource class.</param>
        /// <param name="staticPropertyForDescription">The static property name of resource class.</param>
        public DescriptionAttribute(Type resourceClass, string staticPropertyForDescription)
        {
            this.ResourceClass = resourceClass;
            this.Description = staticPropertyForDescription;
        }

        /// <summary>
        /// Gets the description stored in this attribute.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; private set; }

        /// <summary>
        /// Gets the resource class.
        /// </summary>
        /// <value>The resource class type.</value>
        public Type ResourceClass { get; }

        public string GetStaticProperty()
        {
            return Description;
        }
    }
}