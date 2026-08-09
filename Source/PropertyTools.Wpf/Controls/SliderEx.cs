// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SliderEx.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Represents a slider that calls IEditableObject.BeginEdit/EndEdit when thumb dragging.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
    using System.ComponentModel;    

#if AVALONIA	
	using Avalonia.Controls;
	using DependencyPropertyChangedEventArgs = Avalonia.AvaloniaPropertyChangedEventArgs;
	using DependencyProperty = Avalonia.AvaloniaProperty;
	using DependencyObject = Avalonia.AvaloniaObject;
	using Avalonia.Input;
#else
	using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
#endif

	/// <summary>
	/// Represents a slider that calls IEditableObject.BeginEdit/EndEdit when thumb dragging.
	/// </summary>
	public class SliderEx : Slider
    {
        /// <summary>
        /// The on thumb drag completed.
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnThumbDragCompleted(
#if AVALONIA
			VectorEventArgs
#else
			DragCompletedEventArgs 
#endif
			e)
        {
            base.OnThumbDragCompleted(e);
            var editableObject = this.DataContext as IEditableObject;
            if (editableObject != null)
            {
                editableObject.EndEdit();
            }
        }

		/// <summary>
		/// The on thumb drag started.
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnThumbDragStarted(
#if AVALONIA
			VectorEventArgs
#else
			DragCompletedEventArgs 
#endif
			e)
        {
            base.OnThumbDragStarted(e);
            var editableObject = this.DataContext as IEditableObject;
            if (editableObject != null)
            {
                editableObject.BeginEdit();
            }
        }
    }
}