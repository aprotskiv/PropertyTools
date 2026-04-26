// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEnumValuesFilterOperator.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf.Operators
{
    using PropertyTools.DataAnnotations;
    using PropertyTools.Wpf.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IEnumValuesFilterOperator
    {
        /// <summary>
        /// Gets the values for the specified enumeration type.
        /// </summary>
        /// <param name="enumType">The enumeration type.</param>
        /// <param name="nullAtStart">Determines whether to place NULL value at first item or not (as last item). 
        /// Applicable only for Nullable enumerable type
        /// </param>
        /// <returns>A sequence of values.</returns>
        IEnumerable<Enum> GetEnumValues(IPropertyItem pi, object instance, bool browsableOnly = true);


        /// <summary>
        /// Gets the values for the specified enumeration type.
        /// </summary>
        /// <param name="enumType">The enumeration type.</param>
        /// <param name="nullAtStart">Determines whether to place NULL value at first item or not (as last item). 
        /// Applicable only for Nullable enumerable type
        /// </param>
        /// <returns>A sequence of values.</returns>
        IEnumerable<object> GetEnumValuesWithNullEntry(IPropertyItem pi, object instance, bool nullAtStart, bool browsableOnly = true);
    }
}