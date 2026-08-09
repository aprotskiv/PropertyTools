// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HsvControl.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   The hsv control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
	using System.ComponentModel;

#if AVALONIA
	using Avalonia.Media;
	using Avalonia.Controls;
	using DependencyPropertyChangedEventArgs = Avalonia.AvaloniaPropertyChangedEventArgs;
	using DependencyProperty = Avalonia.AvaloniaProperty;
	using DependencyObject = Avalonia.AvaloniaObject;
	using Point = Avalonia.Point;
	using Avalonia.Controls.Primitives;
	using Avalonia.Input;
	using Avalonia.Styling;
#else
	using System.Windows;   
	using System.Windows.Controls;
	using System.Windows.Input;    
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using System.Windows.Threading;
	using DependencyProperty = System.Windows.DependencyProperty;
	using RoutedEventArgs = System.Windows.RoutedEventArgs;
	using Rect = System.Windows.Rect;
	using Point = System.Windows.Point;
	using Size = System.Windows.Size;
#endif
	/// <summary>
	/// The hsv control.
	/// </summary>
	/// <remarks>Original code by Ury Jamshy, 21 July 2011.
	/// The Code Project Open License (CPOL)</remarks>
#if !AVALONIA
	[TemplatePart(Name = PartThumb, Type = typeof(Thumb))]
#endif
	public class HsvControl :
#if AVALONIA
		TemplatedControl
#else
		Control
#endif
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
		/// Identifies the <see cref="Hue"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty HueProperty = 
#if AVALONIA
			DependencyProperty.Register<HsvControl, double>(
				nameof(Hue),
				0, // default value
				defaultBindingMode: Avalonia.Data.BindingMode.TwoWay
#else
			DependencyProperty.Register(
				nameof(Hue),
				typeof(double),
				typeof(HsvControl),
				new FrameworkPropertyMetadata(
					(double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHueChanged)
#endif
		);

		/// <summary>
		/// Identifies the <see cref="Saturation"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty SaturationProperty =
#if AVALONIA
			DependencyProperty.Register<HsvControl, double>(
				nameof(Saturation),
				0, // default value
				defaultBindingMode: Avalonia.Data.BindingMode.TwoWay
#else
			DependencyProperty.Register(
				nameof(Saturation),
				typeof(double),
				typeof(HsvControl),
				new FrameworkPropertyMetadata(
					(double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSaturationChanged)
#endif
			);

        /// <summary>
        /// Identifies the <see cref="SelectedColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedColorProperty =
#if AVALONIA
			DependencyProperty.Register<HsvControl, Color?>(
				nameof(SelectedColor),
				Colors.Transparent, // default value
				defaultBindingMode: Avalonia.Data.BindingMode.TwoWay
#else
			DependencyProperty.Register(
				nameof(SelectedColor),
				typeof(Color?),
				typeof(HsvControl),
				new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
#endif
			);

        /// <summary>
        /// Identifies the <see cref="Value"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
#if AVALONIA
			DependencyProperty.Register<HsvControl, double>(
				nameof(Value),
				0, // default value
				defaultBindingMode: Avalonia.Data.BindingMode.TwoWay
#else
			DependencyProperty.Register(
				nameof(Value),
				typeof(double),
				typeof(HsvControl),
				new FrameworkPropertyMetadata(
					(double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged)
#endif
		);

        /// <summary>
        /// The thumb name.
        /// </summary>
        private const string PartThumb = "PART_Thumb";

        /// <summary>
        /// The thumb transform.
        /// </summary>
        private readonly TranslateTransform thumbTransform = new TranslateTransform();

        /// <summary>
        /// The thumb.
        /// </summary>
        private Thumb thumb;

#pragma warning disable 649

        /// <summary>
        /// The within update flag.
        /// </summary>
        internal bool withinUpdate;
#pragma warning restore 649

        /// <summary>
        /// Initializes static members of the <see cref="HsvControl" /> class.
        /// </summary>
        static HsvControl()
        {
#if AVALONIA
			// Forces a brand new default ControlTheme for this control type
			ThemeProperty.OverrideDefaultValue<HsvControl>(new ControlTheme(typeof(HsvControl)));
#else
			DefaultStyleKeyProperty.OverrideMetadata(
				typeof(HsvControl), new FrameworkPropertyMetadata(typeof(HsvControl)));
#endif

			// Register Event Handler for the Thumb
#if AVALONIA
			Thumb.DragDeltaEvent.AddClassHandler<HsvControl>(OnThumbDragDeltaStatic);
#else
            EventManager.RegisterClassHandler(
                typeof(HsvControl), Thumb.DragDeltaEvent, new DragDeltaEventHandler(OnThumbDragDelta));
#endif

#if AVALONIA
			Thumb.DragCompletedEvent.AddClassHandler<HsvControl>(OnThumbDragCompletedStatic);
#else
            EventManager.RegisterClassHandler(
                typeof(HsvControl), Thumb.DragCompletedEvent, new DragCompletedEventHandler(OnThumbDragCompleted));
#endif
		}

		public HsvControl()
		{ 
#if AVALONIA
			this.PointerPressed += OnPointerPressed;
#endif
		}

		/// <summary>
		/// The on thumb drag completed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private static void OnThumbDragCompletedStatic(object sender,
#if AVALONIA
			VectorEventArgs
#else
			DragCompletedEventArgs 
#endif
			e)
        {
            ((HsvControl)sender).OnThumbDragCompleted(e);
        }

        /// <summary>
        /// The on thumb drag completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void OnThumbDragCompleted(
#if AVALONIA
			VectorEventArgs
#else
			DragCompletedEventArgs 
#endif
			sender)
        {
            var editableObject = this.DataContext as IEditableObject;
            if (editableObject != null)
            {
                editableObject.EndEdit();
            }
        }

		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs change)
		{
			base.OnPropertyChanged(change);

			if (change.Property == HueProperty)
			{
				OnHueChanged(this, change);
			}
			else if (change.Property == SaturationProperty)
			{
				OnSaturationChanged(this, change);
			}
			else if (change.Property == ValueProperty)
			{
				OnValueChanged(this, change);
			}
			else if (change.Property == TopLevel.ClientSizeProperty)
			{
				this.UpdateThumbPosition();
			}
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
        /// Gets or sets Saturation.
        /// </summary>
        public double Saturation
        {
            get
            {
                return (double)this.GetValue(SaturationProperty);
            }

            set
            {
                this.SetValue(SaturationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets SelectedColor.
        /// </summary>
        public Color? SelectedColor
        {
            get
            {
                return (Color?)this.GetValue(SelectedColorProperty);
            }

            set
            {
                this.SetValue(SelectedColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets Value.
        /// </summary>
        public double Value
        {
            get
            {
                return (double)this.GetValue(ValueProperty);
            }
            set
            {
                this.SetValue(ValueProperty, value);
            }
		}

		/// <summary>
		/// The on apply template.
		/// </summary>
#if AVALONIA
		protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
		{
			base.OnApplyTemplate(e);
			this.thumb = e.NameScope.Find(PartThumb) as Thumb;
#else
		public override void OnApplyTemplate()
		{
            base.OnApplyTemplate();
			this.thumb = this.GetTemplateChild(PartThumb) as Thumb;
#endif
            if (this.thumb != null)
            {
                this.UpdateThumbPosition();
                this.thumb.RenderTransform = this.thumbTransform;
            }
        }


				
#if AVALONIA
		private void OnPointerPressed(object sender, PointerPressedEventArgs e)
		{
			// must be Left Button 
			if (!e.Properties.IsLeftButtonPressed)
				return;
#else
		/// <summary>
		/// Invoked when an unhandled <see cref="E:System.Windows.UIElement.MouseLeftButtonDown" />�routed event is raised on this element. Implement this method to add class handling for this event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> that contains the event data. The event data reports that the left mouse button was pressed.</param>
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
#endif
			var editableObject = this.DataContext as IEditableObject;
            if (editableObject != null)
            {
                editableObject.BeginEdit();
            }

            if (this.thumb != null)
            {
                Point position = e.GetPosition(this);

                this.UpdatePositionAndSaturationAndValue(position.X, position.Y);

                // Initiate mouse event on thumb so it will start drag
                this.thumb.RaiseEvent(e);
            }

#if !AVALONIA
			base.OnMouseLeftButtonDown(e);
#endif
        }


#if !AVALONIA
		/// <summary>
		/// The on render size changed.
		/// </summary>
		/// <param name="sizeInfo">The size info.</param>
		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            this.UpdateThumbPosition();

            base.OnRenderSizeChanged(sizeInfo);
        }
#endif




		/// <summary>
		/// The on hue changed.
		/// </summary>
		/// <param name="relatedObject">The related object.</param>
		/// <param name="e">The e.</param>
		private static void OnHueChanged(DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            var hsvControl = relatedObject as HsvControl;
            if (hsvControl != null && !hsvControl.withinUpdate)
            {
                hsvControl.UpdateSelectedColor();
            }
        }

        /// <summary>
        /// The on saturation changed.
        /// </summary>
        /// <param name="relatedObject">The related object.</param>
        /// <param name="e">The e.</param>
        private static void OnSaturationChanged(DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            var hsvControl = relatedObject as HsvControl;
            if (hsvControl != null && !hsvControl.withinUpdate)
            {
                hsvControl.UpdateThumbPosition();
            }
        }

        /// <summary>
        /// The on thumb drag delta.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private static void OnThumbDragDeltaStatic(object sender,
#if AVALONIA
		VectorEventArgs
#else
		DragDeltaEventArgs 
#endif
		e)
        {
            var hsvControl = sender as HsvControl;
            if (hsvControl != null)
            {
                hsvControl.OnThumbDragDelta(e);
            }
        }

        /// <summary>
        /// The on value changed.
        /// </summary>
        /// <param name="relatedObject">The related object.</param>
        /// <param name="e">The e.</param>
        private static void OnValueChanged(DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            var hsvControl = relatedObject as HsvControl;
            if (hsvControl != null && !hsvControl.withinUpdate)
            {
                hsvControl.UpdateThumbPosition();
            }
        }

        /// <summary>
        /// Limit value to range (0 , max]
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="max">The max.</param>
        /// <returns>
        /// The limit value.
        /// </returns>
        private double LimitValue(double value, double max)
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > max)
            {
                value = max;
            }

            return value;
        }

        /// <summary>
        /// The on thumb drag delta.
        /// </summary>
        /// <param name="e">The e.</param>
        private void OnThumbDragDelta(
#if AVALONIA
			VectorEventArgs
#else
			DragDeltaEventArgs 
#endif
			e)
        {
            double offsetX = this.thumbTransform.X +
#if AVALONIA
				e.Vector.X
#else
				e.HorizontalChange
#endif
			;

			double offsetY = this.thumbTransform.Y +
#if AVALONIA
				e.Vector.Y
#else
				e.VerticalChange
#endif
			;

            this.UpdatePositionAndSaturationAndValue(offsetX, offsetY);
        }

#if AVALONIA
		private double ActualWidth => this.Width;
		private double ActualHeight => this.Height;
#endif

		/// <summary>
		/// The update position and saturation and value.
		/// </summary>
		/// <param name="positionX">The position x.</param>
		/// <param name="positionY">The position y.</param>
		private void UpdatePositionAndSaturationAndValue(double positionX, double positionY)
        {
            positionX = this.LimitValue(positionX, this.ActualWidth);
            positionY = this.LimitValue(positionY, this.ActualHeight);

            this.thumbTransform.X = positionX;
            this.thumbTransform.Y = positionY;

            this.Saturation = 100.0 * positionX / this.ActualWidth;
            this.Value = 100.0 * (1 - positionY / this.ActualHeight);

            this.UpdateSelectedColor();
        }

        /// <summary>
        /// The update selected color.
        /// </summary>
        private void UpdateSelectedColor()
        {
            this.SelectedColor = ColorHelper.HsvToColor(this.Hue / 360.0, this.Saturation / 100.0, this.Value / 100.0);

            // ColorUtils.FireSelectedColorChangedEvent(this, SelectedColorChangedEvent, oldColor, newColor);
        }

        /// <summary>
        /// The update thumb position.
        /// </summary>
        private void UpdateThumbPosition()
        {
            this.thumbTransform.X = this.Saturation * 0.01 * this.ActualWidth;
            this.thumbTransform.Y = (100 - this.Value) * 0.01 * this.ActualHeight;

            this.SelectedColor = ColorHelper.HsvToColor(this.Hue / 360.0, this.Saturation / 100.0, this.Value / 100.0);
        }

		
	}
}