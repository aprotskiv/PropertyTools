// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextWrappingAttribute.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
/// Specifies the text wrapping.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
    using System;
	using System.Reflection;

	/// <summary>
	/// Specifies the text wrapping.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class TextWrappingAttribute : AbstractAttribute
	{
		public TextWrappingAttribute(TextWrapping textWrapping)
		{
			this.TextWrapping = textWrapping;
		}

		/// <summary>
		/// Gets or sets the text wrapping.
		/// </summary>
		public TextWrapping TextWrapping { get; set; }
	}


	/// <summary>
	/// Corresponds to System.Windows.TextWrapping enum
	/// </summary>
	[Obfuscation]
	public enum TextWrapping
	{
		//
		// Summary:
		//     Line-breaking occurs if the line overflows beyond the available block width.
		//     However, a line may overflow beyond the block width if the line breaking algorithm
		//     cannot determine a line break opportunity, as in the case of a very long word
		//     constrained in a fixed-width container with no scrolling allowed.
		WrapWithOverflow,
		//
		// Summary:
		//     No line wrapping is performed.
		NoWrap,
		//
		// Summary:
		//     Line-breaking occurs if the line overflows beyond the available block width,
		//     even if the standard line breaking algorithm cannot determine any line break
		//     opportunity, as in the case of a very long word constrained in a fixed-width
		//     container with no scrolling allowed.
		Wrap
	}


}