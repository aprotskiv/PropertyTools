// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorSlider.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Represents a color slider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
#if AVALONIA
	using Avalonia.Media;
	using Avalonia.Styling;	
	using DependencyProperty = Avalonia.AvaloniaProperty;

#else
	using System.Windows;
	using System.Windows.Data;
    using System.Windows.Media;
	using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
#endif

	/// <summary>
	/// Represents a color slider.
	/// </summary>
	/// <remarks>Original code by Ury Jamshy, 21 July 2011.
	/// The Code Project Open License (CPOL)</remarks>
	public class ColorSlider : SliderEx
    {
#if AVALONIA
		// TODO: make AttachedProperty
		public bool SnapsToDevicePixels 
		{
			get
			{
				return UseLayoutRounding;
			}
			set
			{
				UseLayoutRounding = value;
			} 
		}
#endif

		/// <summary>
		/// Identifies the <see cref="LeftColor"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty LeftColorProperty =
#if AVALONIA
			DependencyProperty.Register<ColorSlider, Color?>(
				nameof(LeftColor),
				Colors.Black // default value				
#else
			DependencyProperty.Register(
				nameof(LeftColor),
				typeof(Color?),
				typeof(ColorSlider),
				new UIPropertyMetadata(Colors.Black)
#endif
		);

        /// <summary>
        /// Identifies the <see cref="RightColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RightColorProperty =
#if AVALONIA
			DependencyProperty.Register<ColorSlider, Color?>(
				nameof(RightColor),
				Colors.White // default value				
#else
			DependencyProperty.Register(
				nameof(RightColor),
				typeof(Color?),
				typeof(ColorSlider),
				new UIPropertyMetadata(Colors.White)
#endif
		);

        /// <summary>
        /// Initializes static members of the <see cref="ColorSlider" /> class.
        /// </summary>
        static ColorSlider()
        {
            

#if AVALONIA
			// Forces a brand new default ControlTheme for this control type
			ThemeProperty.OverrideDefaultValue<ColorSlider>(new ControlTheme(typeof(ColorSlider)));
#else
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ColorSlider), new FrameworkPropertyMetadata(typeof(ColorSlider)));
#endif

		}

		/// <summary>
		/// Gets or sets the left color.
		/// </summary>
		public Color? LeftColor
        {
            get
            {
                return (Color)this.GetValue(LeftColorProperty);
            }

            set
            {
                this.SetValue(LeftColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the right color.
        /// </summary>
        public Color? RightColor
        {
            get
            {
                return (Color?)this.GetValue(RightColorProperty);
            }

            set
            {
                this.SetValue(RightColorProperty, value);
            }
        }
    }
}