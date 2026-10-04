// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SafeIndexBinding.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Prevents runtime XAML binding failure (IndexOutOfRangeException)
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
	using System;
	using System.Collections;
	using System.Globalization;
	using System.Windows;
	using System.Windows.Data;


	/// <summary>
	/// Prevents runtime XAML binding failure (IndexOutOfRangeException)
	/// </summary>
	public class SafeIndexBinding : Binding
	{
		static readonly SafeIndexBindingConverter converter = new SafeIndexBindingConverter();

		public SafeIndexBinding(int index)
		{
			if (index < 0)
				throw new ArgumentOutOfRangeException(nameof(index));

			Path = new PropertyPath(".");
			Mode = BindingMode.OneWay;
			ConverterParameter = index;
			Converter = converter;
		}

		internal class SafeIndexBindingConverter : IValueConverter
		{
			public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			{
				if (parameter is int index)
				{
					if (value is IList source && index < source.Count)
					{
						return source[index];
					}
				}

				return DependencyProperty.UnsetValue;
			}

			public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			{
				throw new NotImplementedException();
			}
		}
	}

}