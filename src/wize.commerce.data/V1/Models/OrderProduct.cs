using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
	public class OrderProduct : ITenantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderProductId { get; set; }
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TaxRate { get; set; }
        public double SubTotal { get; set; }
        public double Discount { get; set; }
        public double ShippingRate { get; set; }
        public double LineSubTotal { get; set; }
        public string ShipmentId { get; set; }
        public string RateId { get; set; }
        public string TrackingCode { get; set; }
        public string TrackingUrl { get; set; }
        public string LabelUrl { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual List<OrderProductTraitOption> TraitOptions { get; set; }
        public virtual List<OrderProductShipping> Shipping { get; set; }
        public double ItemPrice()
        {
            double price = 0.0;
            price = Product.BasePrice;
            if (TraitOptions != null)
            {
                foreach (var item in TraitOptions)
                {
                    price += item.Option.PriceAdjustment;
                }
            }
            return price;
        }
        public double ItemWholesalePrice()
        {
            double price = 0.0;
            price = Product.WholesalePrice;
            price += Product.Surcharge;
            foreach (var item in TraitOptions)
            {
                price += item.Option.PriceAdjustment;
            }
            return price;
        }

        public double ItemHeight()
        {
            double height = 0.0;
            height = Product.Height;
            foreach (var item in TraitOptions)
            {
                height += item.Option.Height;
            }
            
            return height;
        }

        public double ItemWidth()
        {
            double width = 0.0;
            width = Product.Width;
            foreach (var item in TraitOptions)
            {
                width += item.Option.Width;
            }
            return width;
        }

        public double ItemLength()
        {
            double length = 0.0;
            length = Product.Length;
            foreach (var item in TraitOptions)
            {
                length += item.Option.Length;
            }
            return length;
        }

        public double ItemWeight()
        {
            double weight = 0.0;
            weight = Product.Weight;
            foreach (var item in TraitOptions)
            {
                weight += item.Option.Weight;
            }
            return weight;
        }
    }
}
