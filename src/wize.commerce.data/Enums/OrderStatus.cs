using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace wize.commerce.data.v1.Enums
{
    [Description("Order Status")]
    public enum OrderStatus
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Customer")]
        Customer,
        [Description("Shipping")]
        Shipping,
        [Description("Shipping Options")]
        ShippingOptions,
        [Description("Billing")]
        Billing,
        [Description("Payment")]
        Payment,
        [Description("Review")]
        Review,
        [Description("Submitted")]
        Submitted,
        [Description("Cancelled")]
        Cancelled,
        [Description("Backorder")]
        Backordered,
        [Description("Processing")]
        Processing,
        [Description("Filled")]
        Filled,
        [Description("Shipped")]
        Shipped
    }
}
