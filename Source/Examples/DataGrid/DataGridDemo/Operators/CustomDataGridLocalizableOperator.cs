using DataGridDemo.Resources;
using PropertyTools.Wpf.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridDemo.Operators
{
    public class CustomDataGridLocalizableOperator : DefaultLocalizableOperator
    {
        public override string GetLocalizedString(string key, Type declaringType)
        {
            var value = key;

            var resourceKey = key;
            if (resourceKey != null)
            {
                if (declaringType != null)
                {
                    resourceKey = declaringType.FullName + "." + resourceKey;
                }

                var resourceValue = Translations.ResourceManager.GetString(resourceKey);
                if (resourceValue != null)
                {
                    value = resourceValue;
                }
            }

            return value;
        }
    }   
}