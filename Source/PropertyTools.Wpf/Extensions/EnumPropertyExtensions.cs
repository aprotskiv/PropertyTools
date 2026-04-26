using PropertyTools.Wpf.Common;
using PropertyTools.Wpf.Operators;
using System;
using System.Linq;
using System.Reflection;

namespace PropertyTools.Wpf.Extensions
{
    public static class EnumPropertyExtensions
    {
        public static void TrySetEnumMetadata(this IPropertyItem pi, ILocalizableOperator localizedPropertyOperator, 
            IEnumValuesFilterOperator enumValuesFilterOperator,
            object instance)
        {
            var propertyType = pi.PropertyType;
            if (propertyType.IsEnumOrNullableEnum())
            {
                var enumType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                var enumValues = enumValuesFilterOperator.GetEnumValues(pi, instance, browsableOnly: true);

                pi.EnumMetadata.EnumType = enumType;

                pi.EnumMetadata.EnumDisplayNames = enumValues
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

                        return localizedPropertyOperator.GetLocalizedString(enumMemberDisplayName, enumType, instanceType: instance?.GetType(), LocalizableResourceKind.Name);
                    });

                if (propertyType.IsNullableEnum())
                {
                    pi.EnumMetadata.IsNullableEnum = true;
                    pi.EnumMetadata.EnumDisplayNull = localizedPropertyOperator.GetLocalizedString(null, enumType, instanceType: instance?.GetType(), LocalizableResourceKind.Name);
                }
            }
        }
    }
}
