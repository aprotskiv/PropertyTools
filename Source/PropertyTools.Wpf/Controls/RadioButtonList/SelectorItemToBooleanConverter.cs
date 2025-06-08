﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectorItemToBooleanConverter.cs" company="PropertyTools">
//   Copyright (c) 2025 PropertyTools contributors
// </copyright>
// <summary>
//   Represents a control that shows a list of radio buttons.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using PropertyTools.Wpf.Common;

    /// <summary>
    /// Enum to Boolean converter
    /// Usage 'Converter={StaticResource EnumToBooleanConverter}, ConverterParameter={x:Static value...}'
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class SelectorItemToBooleanConverter : IValueConverter
    {
        public ISelectorDefinition SelectorDefinition { get; set; }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <c>null</c>, the valid <c>null</c> value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null && parameter == null)
            {
                return true;
            }

            if (value == null || parameter == null)
            {
                return DependencyProperty.UnsetValue;
            }

            if (ReflectionExtensions.TryGetFieldOrPropertyValue(parameter, SelectorDefinition.SelectedValuePath, out object objTargetValue))
            {
                return value.Equals(objTargetValue);
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <c>null</c>, the valid <c>null</c> value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }

            try
            {
                bool boolValue = System.Convert.ToBoolean(value, culture);
                if (boolValue)
                {
                    if (parameter == null)
                    {
                        return parameter;
                    }

                    if (ReflectionExtensions.TryGetFieldOrPropertyValue(parameter, SelectorDefinition.SelectedValuePath, out object objTargetValue))
                    {
                        return objTargetValue;
                    }
                }
            }
            catch (ArgumentException)
            {
            }
            catch (FormatException)
            {
            }

            return Binding.DoNothing;
        }
    }
}
