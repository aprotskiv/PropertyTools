﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Column.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies the name of a property that provides columns for a data grid.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;

namespace PropertyTools.DataAnnotations
{
    /// <summary>
    /// Defines a column for displaying an item collection. Typically used with <see cref="ColumnsPropertyAttribute"/>.
    /// </summary>
    public class Column : IColumnSelectorDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Column" /> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public Column(string propertyName)
        {
            this.PropertyName = propertyName;
            this.Width = "Auto";
            this.Alignment = 'C';
            this.IsReadOnly = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column" /> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="header">The header.</param>
        /// <param name="itemsSourcePropertyName">Name of the item source property.</param>
        public Column(string propertyName, string header, string itemsSourcePropertyName = null)
            : this(propertyName)
        {
            this.Header = header;
            this.ItemsSourcePropertyName_ColumnsPropertyOwner = itemsSourcePropertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column" /> class.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="header">The header.</param>
        /// <param name="formatString">The format string.</param>
        /// <param name="width">The width.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="isReadOnly">The columns is read only if set to <c>true</c>.</param>
        /// <param name="itemsSourcePropertyName">Name of the items source property.</param>
        public Column(
            string propertyName,
            string header,
            string formatString,
            string width = "Auto",
            char alignment = 'C',
            bool isReadOnly = false,
            string itemsSourcePropertyName = null)
            : this(propertyName, header, itemsSourcePropertyName)
        {
            this.FormatString = formatString;
            this.Width = width;
            this.Alignment = alignment;
            this.IsReadOnly = isReadOnly;
        }

        /// <summary>
        /// Gets or sets the alignment (L, R, C or S).
        /// </summary>
        /// <value>The alignment.</value>
        public char Alignment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the column values are read only (overriding default binding mode).
        /// </summary>
        /// <value><c>true</c> if the column is read only; otherwise, <c>false</c>.</value>
        /// <remarks>If this property is set to <c>true</c>, the binding mode will be one-way also for properties with setters.</remarks>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the index of the column.
        /// </summary>
        /// <value>The index of the column.</value>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// Gets or sets the format string.
        /// </summary>
        /// <value>The format string.</value>
        public string FormatString { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the items source property (ColumnsProperty owner instance)
        /// </summary>
        /// <value>The name of the item source property (ColumnsProperty owner context).</value>
        public string ItemsSourcePropertyName_ColumnsPropertyOwner { get; set; }

        /// <summary>
        /// Gets or sets the width ("Auto", "0.5*" etc. are ok).
        /// </summary>
        /// <value>The width.</value>
        public string Width { get; set; }

        #region  IColumnSelectorDefinition properties

        /// <inheritdoc/>
        public string ItemsSourceProperty_DataGridItem { get; set; }

        /// <inheritdoc/>
        public IEnumerable ItemsSource { get; set; }

        /// <inheritdoc/>
        public string DisplayMemberPath { get; set; }

        /// <inheritdoc/>
        public string SelectedValuePath { get; set; }

        #endregion
    }

    /// <remarks>
    ///  Similar to PropertyTools.Wpf.Common.ISelectorDefinition    
    /// </remarks>    
    public interface IColumnSelectorDefinition
    {
        /// <summary>
        /// Gets or sets the items source property (DataGrid item instance)
        /// </summary>
        /// <value>
        /// The items source property (DataGrid item context)
        /// </value>        
        string ItemsSourceProperty_DataGridItem { get; set; }


        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>
        /// The items source.
        /// </value>
        IEnumerable ItemsSource { get; set; }

        /// <summary>
        /// Gets or sets the display member path.
        /// </summary>
        /// <value>
        /// The display member path.
        /// </value>
        string DisplayMemberPath { get; set; }

        /// <summary>
        /// Gets or sets the selected value path.
        /// </summary>
        /// <value>
        /// The selected value path.
        /// </value>
        string SelectedValuePath { get; set; }

        /// <summary>
        /// Indicates whether to display or not the display member text (from <see cref="DisplayMemberPath"/>) in cases:<para/> 
        /// 1) when NULL item selected. <para/>
        ///     For example, <see cref="System.Windows.Controls.Primitives.Selector.SelectedValue"/> is NULL, 
        ///     but <see cref="System.Windows.Controls.Primitives.Selector.SelectedItem"/> is NOT null <para/> 
        /// 2) no item selected <para/>
        ///     For example, <see cref="System.Windows.Controls.Primitives.Selector.SelectedItem"/> is NULL<para/>         
        /// </summary>
        bool DisplayTextForNullItem { get; set; }
    }
}
