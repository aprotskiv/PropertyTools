// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopyToClipboardText.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Represents a control that allows the user to copy text to clipboard.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;
	using TextCopy;

	/// <summary>
	/// Represents a control that allows the user to copy text to clipboard.
	/// </summary>
	public class CopyToClipboardText : Control
    {
        /// <summary>
        /// Identifies the <see cref="Text"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(CopyToClipboardText),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		/// <summary>
		/// Identifies the <see cref="TextWrapping"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(
			nameof(TextWrapping),
			typeof(TextWrapping),
			typeof(CopyToClipboardText), 
			new FrameworkPropertyMetadata(TextWrapping.NoWrap, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		/// <summary>
		/// Identifies the <see cref="IsReadOnly"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
			nameof(IsReadOnly),
			typeof(bool),
			typeof(CopyToClipboardText),
			new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		/// <summary>
		/// Identifies the <see cref="CopyToClipboardButtonContent"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty CopyToClipboardButtonContentProperty = DependencyProperty.Register(
            nameof(CopyToClipboardButtonContent),
            typeof(object),
            typeof(CopyToClipboardText),
            new PropertyMetadata("⧉"));

    
        /// <summary>
        /// Identifies the <see cref="CopyToClipboardButtonToolTip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CopyToClipboardButtonToolTipProperty = DependencyProperty.Register(
            nameof(CopyToClipboardButtonToolTip),
            typeof(object),
            typeof(CopyToClipboardText),
            new PropertyMetadata(null));

        /// <summary>
        /// Initializes static members of the <see cref="CopyToClipboardText" /> class.
        /// </summary>
        static CopyToClipboardText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CopyToClipboardText), new FrameworkPropertyMetadata(typeof(CopyToClipboardText)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyToClipboardText" /> class.
        /// </summary>
        public CopyToClipboardText()
        {
            this.CopyToClipboardCommand = new DelegateCommand(this.CopyToClipboard);
        }

        /// <summary>
        /// Gets or sets the browse command.
        /// </summary>
        /// <value>The browse command.</value>
        public ICommand CopyToClipboardCommand { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }

            set
            {
                this.SetValue(TextProperty, value);
            }
        }

		//
		// Summary:
		//     Gets or sets how the text box should wrap text.
		//
		// Returns:
		//     One of the System.Windows.TextWrapping values that indicates how the text box
		//     should wrap text. The default is System.Windows.TextWrapping.NoWrap.
		public TextWrapping TextWrapping
		{
			get
			{
				return (TextWrapping)GetValue(TextWrappingProperty);
			}
			set
			{
				SetValue(TextWrappingProperty, value);
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return (bool)this.GetValue(IsReadOnlyProperty);
			}
			set
			{
				this.SetValue(IsReadOnlyProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the content on the "Copy To Clipboard" button.
		/// </summary>
		public object CopyToClipboardButtonContent
        {
            get { return this.GetValue(CopyToClipboardButtonContentProperty); }
            set { this.SetValue(CopyToClipboardButtonContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ToolTip on the "browse" button.
        /// </summary>
        public object CopyToClipboardButtonToolTip
        {
            get { return this.GetValue(CopyToClipboardButtonToolTipProperty); }
            set { this.SetValue(CopyToClipboardButtonToolTipProperty, value); }
        }

		

		/// <summary>
		/// </summary>
		private void CopyToClipboard()        
		{
			ClipboardService.SetText(this.Text);
        }
    }
}