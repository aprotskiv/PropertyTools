// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorToHexConverter.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Converts Color instances to hex string instances.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
    using System;
    using System.Globalization;    

#if AVALONIA
	using Avalonia.Media;
	using Avalonia.Data.Converters;
	using DependencyProperty = Avalonia.AvaloniaProperty;
#else
	using System.Windows;   	
	using System.Windows.Data;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;	
#endif

	/// <summary>
	/// Converts <see cref="Color" /> instances to hex <see cref="string" /> instances.
	/// </summary>
#if !AVALONIA
	[ValueConversion(typeof(Color), typeof(string))]
#endif
    public class ColorToHexConverter : IValueConverter
    {
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
            if (value == null)
            {
                return null;
            }

            var color = (Color)value;
            return color.ColorToHex();
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
            return ColorHelper.HexToColor((string)value);
        }
    }
}