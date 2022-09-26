using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
	public class ShoppingCartItem : ITenantModel
    {
        public ShoppingCartItem()
        {
            ItemOptions = new List<ShoppingCartItemOption>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShoppingCartItemId { get; set; }
        public string ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
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

        public virtual Product Product { get; set; }
        public virtual List<ShoppingCartItemOption> ItemOptions { get; set; }
        public double Price { get; set; }
        public double MSRP { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public double TotalPrice
        {
            get {
                double price = Price;
                if (ItemOptions != null)
                {
                    foreach (var item in ItemOptions)
                    {
                        if (item.Option != null)
                            price += item.Option.PriceAdjustment;
                    }
                }
                return price;
            }
        }
        public double TotalMSRP
        {
            get
            {
                double msrp = MSRP;
                if(ItemOptions != null)
                {
                    foreach(var item in ItemOptions)
                    {
                        if (item.Option != null)
                            msrp += item.Option.PriceAdjustment;
                    }
                }
                return msrp;
            }
        }

        public double TotalLinePrice
        {
            get
            {
                return TotalPrice * Quantity;
            }
        }
        public double TotalLineMSRP
        {
            get
            {
                return TotalMSRP * Quantity;
            }
        }

        public double TotalHeight
        {
            get
            {
                double height = Height;
                foreach (var item in ItemOptions)
                {
                    if (item.Option != null)
                        height += item.Option.Height;
                }
                return height;
            }
        }

        public double TotalWidth
        {
            get
            {
                double width = Width;
                foreach (var item in ItemOptions)
                {
                    if (item.Option != null)
                        width += item.Option.Width;
                }
                return width;
            }
        }

        public double TotalLength
        {
            get
            {
                double length = Length;
                foreach (var item in ItemOptions)
                {
                    if (item.Option != null)
                        length += item.Option.Length;
                }
                return length;
            }
        }

        public double TotalWeight
        {
            get
            {
                double weight = Weight;
                foreach (var item in ItemOptions)
                {
                    if (item.Option != null)
                        weight += item.Option.Weight;
                }
                return weight;
            }
        }
    }
}
