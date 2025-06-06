﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadioButtonList.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Represents a control that shows a list of radio buttons.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    using PropertyTools.DataAnnotations;
    using PropertyTools.Wpf.Common;

    /// <summary>
    /// Represents a control that shows a list of radio buttons (for enumeration values).
    /// </summary>
    [TemplatePart(Name = PartPanel, Type = typeof(StackPanel))]
    public class RadioButtonList : RadioButtonSelector
    {
        /// <summary>
        /// Identifies the <see cref="DescriptionConverter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DescriptionConverterProperty = DependencyProperty.Register(
            nameof(DescriptionConverter),
            typeof(IValueConverter),
            typeof(RadioButtonList),
            new UIPropertyMetadata(new EnumDescriptionConverter()));

        /// <summary>
        /// Identifies the <see cref="EnumType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty EnumTypeProperty = DependencyProperty.Register(
            nameof(EnumType),
            typeof(Type),
            typeof(RadioButtonList),
            new UIPropertyMetadata(null, ValueChanged));

        /// <summary>
        /// Gets or sets the description converter.
        /// </summary>
        /// <value>The description converter.</value>
        public IValueConverter DescriptionConverter
        {
            get
            {
                return (IValueConverter)this.GetValue(DescriptionConverterProperty);
            }

            set
            {
                this.SetValue(DescriptionConverterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the type of the enumeration.
        /// </summary>
        /// <value>The type of the enumeration.</value>
        public Type EnumType
        {
            get
            {
                return (Type)this.GetValue(EnumTypeProperty);
            }

            set
            {
                this.SetValue(EnumTypeProperty, value);
            }
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        protected override void UpdateContent()
        {
            if (this.panel == null)
            {
                return;
            }

            this.panel.Children.Clear();

            var enumType = this.EnumType;
            if (enumType != null)
            {
                var ult = Nullable.GetUnderlyingType(enumType);
                if (ult != null)
                {
                    enumType = ult;
                }
            }

            if (this.Value != null)
            {
                enumType = this.Value.GetType();
            }

            if (enumType == null || !typeof(Enum).IsAssignableFrom(enumType))
            {
                return;
            }

            var enumValues = Enum.GetValues(enumType).FilterOnBrowsableAttribute().ToList();

            // if the type is nullable, add the null value
            if (Nullable.GetUnderlyingType(enumType) != null)
            {
                enumValues.Add(null);
            }

            var converter = new EnumToBooleanConverter { EnumType = enumType };

            foreach (var itemValue in enumValues)
            {
                object content;
                if (itemValue != null)
                {
                    content = this.DescriptionConverter.Convert(
                        itemValue,
                        typeof(string),
                        null,
                        CultureInfo.CurrentUICulture);
                }
                else
                {
                    content = "-";
                }

                var rb = new RadioButton
                {
                    Content = content,
                    Padding = this.ItemPadding,
                };

                var binding = CreateBindingFromOptionEnableByAttribute(itemValue, enumType);
                if (binding != null)
                {
                    rb.SetBinding(UIElement.IsEnabledProperty, binding);
                }

                var isCheckedBinding = new Binding(nameof(this.Value))
                {
                    Converter = converter,
                    ConverterParameter = itemValue,
                    Source = this,
                    Mode = BindingMode.TwoWay
                };

                rb.SetBinding(ToggleButton.IsCheckedProperty, isCheckedBinding);

                rb.SetBinding(MarginProperty, new Binding(nameof(this.ItemMargin)) { Source = this });

                this.panel.Children.Add(rb);
            }
        }

        /// <summary>
        /// Creates a data binding for a WPF control based on the <see cref="OptionEnableByAttribute"/> applied to an enum value.
        /// </summary>
        /// <param name="itemValue">The value of the enum item to create the binding for.</param>
        /// <param name="enumType">The type of the enum containing the item.</param>
        /// <returns>
        /// A data binding instance if the enum item has an <see cref="OptionEnableByAttribute"/>; otherwise, null.
        /// </returns>
        private Binding CreateBindingFromOptionEnableByAttribute(object itemValue, Type enumType)
        {
            var itemName = itemValue?.ToString();
            if (itemName == null)
            {
                return null;
            }

            var fieldInfo = enumType.GetField(itemName);
            if (fieldInfo == null)
            {
                return null;
            }

            var attribute = fieldInfo.GetCustomAttribute<OptionEnableByAttribute>();
            if (attribute != null)
            {
                // Create and return the binding using the property name from the attribute
                return new Binding(attribute.PropertyName)
                {
                    Source = this.DataContext
                };
            }

            return null;
        }
    }
}
