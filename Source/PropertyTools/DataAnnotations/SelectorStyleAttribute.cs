// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectorStyleAttribute.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies what control style a selector property should use.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
    using System;

    /// <summary>
    /// Specifies what control style a selector property should use.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SelectorStyleAttribute : AbstractAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorStyleAttribute" /> class.
        /// </summary>
        /// <param name="selectorStyle">The selector style.</param>
        public SelectorStyleAttribute(SelectorStyle selectorStyle)
        {
            this.SelectorStyle = selectorStyle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorStyleAttribute" /> class.
        /// </summary>
        /// <param name="selectorStyle">The selector style.</param>
        /// <param name="radioButtonsLimit">The limit. If the number of values exceeds the limit, a multiselect listbox will be used.</param>
        public SelectorStyleAttribute(SelectorStyle selectorStyle, int radioButtonsLimit)
        {
            this.SelectorStyle = selectorStyle;
            this.RadioButtonsLimit = radioButtonsLimit;
        }

        /// <summary>
        /// Gets the selector style.
        /// </summary>
        /// <value>The selector style.</value>
        public SelectorStyle SelectorStyle { get; private set; }

        /// <summary>
        /// Gets or sets the limiting number of values if the property can be shown with radio buttons (<see cref="SelectorStyle"/>)
        /// </summary>
        /// <value>
        /// The limit. If the number of values exceeds the limit, a multiselect listbox will be used. <para/>
        /// If is null the PropertyControlFactoryOptions.RadioButtonsLimit will be used.
        /// </value>
        public int? RadioButtonsLimit { get; set; }
    }
}