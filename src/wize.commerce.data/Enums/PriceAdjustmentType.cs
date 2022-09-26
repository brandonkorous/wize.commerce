using System;
using System.Collections.Generic;
using System.Text;

namespace wize.commerce.data.v1.Enums
{
    /// <summary>
    /// This determines the price adjustment for the Product Attribute and is applied to the base price for the product
    /// i.e.: 10.00 + (10.00 * 2%) or 10.00 + (2.00)
    /// </summary>
    public enum PriceAdjustmentType
    {
        Dollar,
        Percent
    }
}
