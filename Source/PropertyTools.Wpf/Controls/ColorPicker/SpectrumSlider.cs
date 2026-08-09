// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpectrumSlider.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   The spectrum slider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{

#if AVALONIA
	using Avalonia;
	using Avalonia.Controls.Primitives;
	using Avalonia.Data;
	using Avalonia.Media;
	using Avalonia.Styling;
	using DependencyObject = Avalonia.AvaloniaObject;
	using DependencyProperty = Avalonia.AvaloniaProperty;
	using DependencyPropertyChangedEventArgs = Avalonia.AvaloniaPropertyChangedEventArgs;

#else
	using System.Windows;
	using System.Windows.Data;
    using System.Windows.Media;
	using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
#endif
	/// <summary>
	/// The spectrum slider.
	/// </summary>
	/// <remarks>Original code by Ury Jamshy, 21 July 2011.
	/// The Code Project Open License (CPOL)</remarks>
	public class SpectrumSlider : SliderEx
	{
		/// <summary>
		/// Identifies the <see cref="Hue"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty HueProperty =
#if AVALONIA
			DependencyProperty.Register<SpectrumSlider, double>(
				nameof(Hue),
				0, // default value
				defaultBindingMode: Avalonia.Data.BindingMode.TwoWay
#else
			DependencyProperty.Register(
				nameof(Hue),
				typeof(double),
				typeof(SpectrumSlider),
				new FrameworkPropertyMetadata(
					(double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHuePropertyChanged)
#endif
		);


		/// <summary>
		/// The within changing flag.
		/// </summary>
		private bool withinChanging;

		/// <summary>
		/// Initializes static members of the <see cref="SpectrumSlider" /> class.
		/// </summary>
		static SpectrumSlider()
		{
#if AVALONIA
			// Forces a brand new default ControlTheme for this control type
			ThemeProperty.OverrideDefaultValue<SpectrumSlider>(new ControlTheme(typeof(SpectrumSlider)));
#else
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SpectrumSlider), new FrameworkPropertyMetadata(typeof(SpectrumSlider)));
#endif
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SpectrumSlider" /> class.
		/// </summary>
		public SpectrumSlider()
		{
			this.SetBackground();

#if AVALONIA
			this.ValueChanged += OnValueChanged;
#endif
		}

		/// <summary>
		/// Gets or sets Hue.
		/// </summary>
		public double Hue
		{
			get
			{
				return (double)this.GetValue(HueProperty);
			}

			set
			{
				this.SetValue(HueProperty, value);
			}
		}

		/// <summary>
		/// The on value changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
#if AVALONIA
		private void OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
#else
		protected override void OnValueChanged(double oldValue, double newValue)
        {
			base.OnValueChanged(oldValue, newValue);
#endif

			if (!this.withinChanging &&
#if AVALONIA
				BindingOperations.GetBindingExpressionBase(this, HueProperty) == null // no binding
#else
				!BindingOperations.IsDataBound(this, HueProperty)
#endif
				)
			{
				this.withinChanging = true;
#if AVALONIA
				this.Hue = e.NewValue;
#else
				this.Hue = newValue;
#endif
				this.withinChanging = false;
			}
		}

#if AVALONIA
		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs change)
		{
			base.OnPropertyChanged(change);

			if (change.Property == HueProperty)
			{
				OnHuePropertyChanged(this, change);
			}
		}
#endif

		/// <summary>
		/// The on hue property changed.
		/// </summary>
		/// <param name="relatedObject">The related object.</param>
		/// <param name="e">The e.</param>
		private static void OnHuePropertyChanged(DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
		{
			var spectrumSlider = relatedObject as SpectrumSlider;
			if (spectrumSlider != null && !spectrumSlider.withinChanging)
			{
				spectrumSlider.withinChanging = true;

				var hue = (double)e.NewValue;
				spectrumSlider.Value = hue;

				spectrumSlider.withinChanging = false;
			}
		}

		/// <summary>
		/// The set background.
		/// </summary>
		private void SetBackground()
		{
			var backgroundBrush = new LinearGradientBrush
			{
				StartPoint = new
#if AVALONIA
						RelativePoint(0.5, 1, RelativeUnit.Relative),
#else
						Point(0.5, 1),
#endif
				EndPoint = new
#if AVALONIA
						RelativePoint(0.5, 0, RelativeUnit.Relative)
#else
						Point(0.5, 0)
#endif
			};

			const int SpectrumColorCount = 30;

			Color[] spectrumColors = ColorHelper.GetSpectrumColors(SpectrumColorCount);
			for (int i = 0; i < SpectrumColorCount; ++i)
			{
				double offset = i * 1.0 / SpectrumColorCount;
				var gradientStop = new GradientStop(spectrumColors[i], offset);
				backgroundBrush.GradientStops.Add(gradientStop);
			}

			this.Background = backgroundBrush;
		}
	}
}