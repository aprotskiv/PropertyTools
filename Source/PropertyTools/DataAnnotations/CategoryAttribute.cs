// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryAttribute.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies the name of the category in which to group the property or event when displayed in a PropertyGrid control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
	using System;
    using System.Linq;

    /// <summary>
    /// Specifies the name of the category in which to group the property or event when displayed in a PropertyGrid control.
    /// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class CategoryAttribute : AbstractAttribute, IResourceClassAttribute
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryAttribute"/> class.
		/// </summary>
		/// <param name="category">The category.</param>
		public CategoryAttribute(string category)
		{
			this.Category = category;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryAttribute"/> class.
		/// </summary>
		/// <param name="category">The category.</param>
		/// <param name="tabSortIndex">The category sort index (tab scope).</param>
		/// <param name="groupSortIndex">The category sort index (group scope).</param>
		public CategoryAttribute(string category, uint tabSortIndex = 0, uint groupSortIndex = 0)
		{
			this.Category = category;
			this.TabSortIndex = tabSortIndex;
			this.GroupSortIndex = groupSortIndex;			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DescriptionAttribute"/> class.
		/// </summary>
		/// <param name="resourceClass">The resource class.</param>
		/// <param name="staticProperties">The array of static properties of resource class.</param>
		public CategoryAttribute(Type resourceClass, uint tabSortIndex = 0, uint groupSortIndex = 0, params string[] categorySegments)
			: this(resourceClasses : new[] { resourceClass }, tabSortIndex, groupSortIndex, categorySegments)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DescriptionAttribute"/> class.
		/// </summary>
		/// <param name="resourceClasses">The array or resource classes.</param>
		/// <param name="staticProperties">The array of static properties of resource class.</param>
		public CategoryAttribute(Type[] resourceClasses, uint tabSortIndex = 0, uint groupSortIndex = 0, params string[] categorySegments)
        {
            this.ResourceClasses = resourceClasses;
            this.Category = categorySegments.Length == 1 
                ? categorySegments[0] + "|" // first segment must end with '|'
                : string.Join("|", categorySegments);

            this.TabSortIndex = tabSortIndex;
            this.GroupSortIndex = groupSortIndex;
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>The category.</value>
        public virtual string Category { get; private set; }

		/// <summary>
		/// Gets the category sort index (tab scope)
		/// </summary>
		/// <value>The category sort index (tab scope).</value>
		public virtual uint? TabSortIndex { get; private set; }

		/// <summary>
		/// Gets the category sort index (group scope)
		/// </summary>
		/// <value>The category sort index (group scope).</value>
		public virtual uint? GroupSortIndex { get; private set; }

        /// <summary>
        /// Gets the resource class.
        /// </summary>
        /// <value>The resource class type.</value>
        public Type[] ResourceClasses { get; }
    }
}