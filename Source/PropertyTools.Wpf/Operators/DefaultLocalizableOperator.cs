// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultLocalizableOperator.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf.Operators
{
    using System;

    /// <summary>
    /// Provides a default implementation of a localizable operator that supports customizable localization of strings
    /// and descriptions.
    /// </summary>
    /// <remarks>This class allows for the delegation of localization logic to a custom operator by calling
    /// <see cref="UseLocalizableOperator"/>. If no custom operator is set, it returns the provided key as the localized
    /// value. This is useful as a fallback or base implementation for localization scenarios.</remarks>
    public class DefaultLocalizableOperator : ILocalizableOperator, ICustomLocalizableOperator
    {
        private ILocalizableOperator customLocalizableOperator;

        /// <inheritdoc/>        
        public void UseLocalizableOperator(ILocalizableOperator value)
        {
            if (value == this)
            {
                throw new ArgumentException("Cannot use itself as custom operator");
            }

            this.customLocalizableOperator = value;
        }

        /// <summary>
        /// Gets the localized description.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="declaringType">Type of the declaring.</param>
        /// <param name="instanceType">The type of instance what contains property item.</param>
        /// <param name="resourceClass">The resource class.</param>
        /// <returns>
        /// The localized description.
        /// </returns>
        public virtual string GetLocalizedDescription(string key, Type declaringType, Type instanceType, Type[] resourceClasses)
        {
            if (this.customLocalizableOperator != null)
            {
                return this.customLocalizableOperator.GetLocalizedDescription(key, declaringType, instanceType, resourceClasses);
            }

            return key;
        }

        /// <summary>
        /// Gets the localized string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="instanceType">The type of instance what contains property item.</param>
        /// <param name="resourceKind">The kind of localizable resource.</param>
        /// <param name="resourceClasses">The resource class.</param>
        /// <returns>
        /// The localized string.
        /// </returns>
        public virtual string GetLocalizedString(string key, Type declaringType, Type instanceType, LocalizableResourceKind resourceKind,
            Type[] resourceClasses = null)
		{
            if (this.customLocalizableOperator != null)
            {
                return this.customLocalizableOperator.GetLocalizedString(key, declaringType, instanceType, resourceKind, resourceClasses);
            }

            return key;
        }

        /// <summary>
        /// Determines whether the <see cref="customLocalizableOperator"/> is set or not.
        /// </summary>        
        protected bool HasLocalizableOperator() => this.customLocalizableOperator != null;
    }
}