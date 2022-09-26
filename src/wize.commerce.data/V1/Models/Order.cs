using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.commerce.data.v1.Enums;
using wize.common.tenancy.Interfaces;
using wize.common.use.repository.Enums;

namespace wize.commerce.data.v1.Models
{
	public class Order : ITenantModel
    {
        public Order()
        {
            //Customer = new Customer();
            Products = new List<OrderProduct>();
            OrderId = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }
        [MaxLength(128)]
        public string UserId { get; set; }
        [MaxLength(128)]
        public string ShoppingCartId { get; set; }
        public string ShipToName { get; set; }
        public int? ShippingAddressId { get; set; }
        public int? BillingAddressId { get; set; }
        public string GiftMessage { get; set; }
        public string DiscountCode { get; set; }
        public double ItemTotal { get; set; }
        public double Total { get; set; }
        public double TotalDiscount { get; set; }
        public double TaxTotal { get; set; }
        public double TotalSurcharge { get; set; }
        public double TotalHandling { get; set; }
        public double TotalShipping { get; set; }
        public double GrandTotal { get; set; }
        private DateTime _created = DateTime.Now;
        [Required]
        public DateTime Created
        {
            get
            {
                return _created.ToLocalTime();
            }
            set
            {
                if (value.Kind == DateTimeKind.Utc)
                {
                    _created = value;
                }
                else if (value.Kind == DateTimeKind.Local)
                {
                    _created = value.ToUniversalTime();
                }
                else
                {
                    _created = DateTime.SpecifyKind(value, DateTimeKind.Utc);
                }
            }
        }
        [MaxLength(128)]
        public string UpdatedBy { get; set; }
        private DateTime _updated = DateTime.Now;
        [Required]
        public DateTime Updated { get { return _updated; } set { _updated = value; } }
        [MaxLength(128)]
        public string CreatedBy { get; set; }
        public OrderStatus OrderStatus { get; set; }
        [DefaultValue(false)]
        public bool Wholesale { get; set; }
        public string PaymentTokenId { get; set; }
        public string Last4 { get; set; }
        public string ShippingOrderId { get; set; }

        public virtual List<OrderProduct> Products { get; set; }
        [ForeignKey("ShippingAddressId")]
        public virtual Address ShippingAddress { get; set; }
        [ForeignKey("BillingAddressId")]
        public virtual Address BillingAddress { get; set; }

    }
}
