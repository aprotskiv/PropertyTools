﻿using PropertyTools.Wpf.Common;
using PropertyTools.Wpf.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PropertyTools.Wpf.Extensions
{
    public static class EnumPropertyExtensions
    {

        public static void TrySetEnumMetadata(this IPropertyItem pi, ILocalizableOperator localizedPropertyOperator)
        {
            var propertyType = pi.PropertyType;
            if (propertyType.IsEnumOrNullableEnum())
            {
                var enumType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

                pi.EnumMetadata.EnumDisplayNames = Enum.GetValues(enumType).Cast<object>()
                   .ToDictionary(x => x,
                    x =>
                    {
                        var fieldInfo = enumType.GetFields(BindingFlags.Public | BindingFlags.Static)
                            .FirstOrDefault(f => f.GetValue(null).Equals(x));

                        // System.ComponentModel.DisplayNameAttribute is not supported for fields (enum members)                           
                        var displayNameAttribute = fieldInfo.GetCustomAttribute(typeof(PropertyTools.DataAnnotations.DisplayNameAttribute))
                               as PropertyTools.DataAnnotations.DisplayNameAttribute;

                        var descriptionAttribute1 = fieldInfo.GetCustomAttribute(typeof(System.ComponentModel.DescriptionAttribute))
                               as System.ComponentModel.DescriptionAttribute;
                        var descriptionAttribute2 = fieldInfo.GetCustomAttribute(typeof(PropertyTools.DataAnnotations.DescriptionAttribute))
                               as PropertyTools.DataAnnotations.DescriptionAttribute;

                        var enumMemberDisplayName = displayNameAttribute?.DisplayName
                           ?? descriptionAttribute1?.Description
                           ?? descriptionAttribute2?.Description
                           ?? x.ToString();

                        return localizedPropertyOperator.GetLocalizedString(enumMemberDisplayName, enumType);
                    });

                if (propertyType.IsNullableEnum())
                {
                    pi.EnumMetadata.EnumDisplayNull = localizedPropertyOperator.GetLocalizedString(null, enumType);
                }
            }
        }


        /// <summary>
        /// Gets the values for the specified enumeration type.
        /// </summary>
        /// <param name="enumType">The enumeration type.</param>
        /// <param name="nullAtStart">Determines whether to place NULL value at first item or not (as last item). 
        /// Applicable only for Nullable enumerable type
        /// </param>
        /// <returns>A sequence of values.</returns>
        public static IEnumerable<object> GetEnumValues(this IPropertyItem pi, bool nullAtStart, bool browsableOnly = true)
        {
            if (!pi.PropertyType.IsEnumOrNullableEnum())
            {
                throw new InvalidOperationException($"The PropertyType ({pi.PropertyType.FullName}) must be enumerable type or nullable enumerable type.");
            }

            var enumType = pi.PropertyType;
            var ult = Nullable.GetUnderlyingType(enumType);
            var isNullable = ult != null;
            if (isNullable)
            {
                enumType = ult;
            }

            var enumValues = Enum.GetValues(enumType).Cast<object>().ToList();
            if (browsableOnly)
            {
                enumValues = enumValues.FilterOnBrowsableAttribute();
            }
            
            if (isNullable)
            {
                if (nullAtStart)
                {
                    enumValues.Insert(0, null);
                }
                else
                {
                    enumValues.Add(null);
                }
            }

            return enumValues;
        }
    }
}
