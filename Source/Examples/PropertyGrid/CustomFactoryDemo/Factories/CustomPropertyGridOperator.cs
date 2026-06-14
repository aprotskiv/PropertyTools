// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyItemFactory.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Provides a custom property item factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CustomFactoryDemo
{
    using PropertyTools.Wpf;
    using PropertyTools.Wpf.Operators;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Provides a custom property item factory.
    /// </summary>
    public class CustomPropertyGridOperator : PropertyGridOperator
    {
        protected override PropertyItem CreateCore(PropertyDescriptor pd, PropertyDescriptorCollection properties)
        {
            // Check if the property is decorated with an "ImportantAttribute"
            var ia = pd.GetFirstAttributeOrDefault<ImportantAttribute>();
            if (ia != null)
            {
                // Create a custom PropertyItem instance
                return new ImportantPropertyItem(pd, properties);
            }

            return base.CreateCore(pd, properties);
        }

        protected override string GetDisplayName(PropertyDescriptor pd, Type declaringType, object instance)
        {
            // Use the property name as display name - this will be passed to the GetLocalizedString later
            return pd.Name;
        }

        public override string GetLocalizedString(string key, Type declaringType, Type instanceType, LocalizableResourceKind resourceKind)
        {
            // Add a star to show that we have handled this
            // A localization mechanism can be used to localize the strings
            return key + "*";
        }
    }
}