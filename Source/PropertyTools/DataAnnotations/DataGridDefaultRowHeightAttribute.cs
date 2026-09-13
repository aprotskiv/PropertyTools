// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataGridDefaultRowHeightAttribute.cs" company="PropertyTools">
//   Copyright (c) 2025 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies the default heights of the DataGrid Row.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
    using System;

    /// <summary>
    /// Specifies the default heights of the DataGrid Row.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DataGridDefaultRowHeightAttribute : AbstractAttribute
    {
        public DataGridDefaultRowHeightAttribute()
        { 
        }

        public DataGridDefaultRowHeightAttribute(int pixels)
        {
            this.Pixels = pixels;
        }

        public bool IsAuto() => Pixels == null;
        
        public int? Pixels { get; set; }
    }
}