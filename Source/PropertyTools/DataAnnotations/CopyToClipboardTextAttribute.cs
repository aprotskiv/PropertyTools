// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopyToClipboardTextAttribute.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies that the property is copy to clipboard text.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
    using System;

	/// <summary>
	/// Specifies that the property is copy to clipboard text.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class CopyToClipboardTextAttribute : AbstractAttribute
	{
	}
}