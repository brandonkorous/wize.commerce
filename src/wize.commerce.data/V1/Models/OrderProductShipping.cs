using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
    public class OrderProductShipping : ITenantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderProductShippingId { get; set; }
        public int OrderProductId { get; set; }
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
        public DateTime? Updated { get; set; }
        public string Mode { get; set; }
        public string Service { get; set; }
        public string TrackingCode { get; set; }
        public string Status { get; set; }
        public string Rate { get; set; }
        public string ListRate { get; set; }
        public string RetailRate { get; set; }
        public string Currency { get; set; }
        public string ListCurrency { get; set; }
        public string RetailCurrency { get; set; }
        [DefaultValue(0)]
        public int EstDeliveryDays { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool DeliveryDateGuaranteed { get; set; }
        [DefaultValue(0)]
        public int DeliveryDays { get; set; }
        public string Carrier { get; set; }
        public string ShipmentId { get; set; }
        public string CarrierAccountId { get; set; }
        public string OrderId { get; set; }
        public string RateId { get; set; }

        public virtual OrderProduct OrderProduct { get; set; }
    }
}
