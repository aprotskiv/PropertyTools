using System;

namespace PropertyTools.Wpf.Operators
{
    public interface ILocalizableOperator
    {
        /// <summary>
        /// Gets the localized description.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="declaringType">The declaring type of property.</param>
        /// <param name="instanceType">The type of instance what contains property item.</param>
        /// <returns>
        /// The localized description.
        /// </returns>
        string GetLocalizedDescription(string key, Type declaringType, Type instanceType);


        /// <summary>
        /// Gets the localized string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="declaringType">The declaring type of property.</param>
        /// <param name="instanceType">The type of instance what contains property item.</param>
        /// <returns>
        /// The localized string.
        /// </returns>
        string GetLocalizedString(string key, Type declaringType, Type instanceType);
    }
}