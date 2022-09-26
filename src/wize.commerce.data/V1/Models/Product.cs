using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
	public class Product : ITenantModel
    {
        public Product()
        {
            //Files = new List<ProductProductFile>();
            Traits = new List<ProductTrait>();
            TaxRates = new List<ProductTaxRate>();
            ProductViews = new List<ProductView>();
            //Inventory = new Inventory();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public int? InventoryId { get; set; }
        [Display(Description = "Alternate Id.  Usually the product number with the vendor")]
        public string AlternateId { get; set; }
        [Required]
        [Description("Description Data Model")]
        [Display(Description = "Display Data Model")]
        public string Name { get; set; }
        [Description("The Name of the product for display")]
        public string DisplayName { get; set; }
        [Required]
        [Display(Description = "This is the name of the page that will be used to create linking and direct access.  This field needs to be standard characters with no special characters.  Replace all [spaces] with - [hyphens].")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Spaces are not allowed.")]
        public string SafeName { get; set; }
        public string SKU { get; set; }
        public string ManufacturerItemNumber { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        [DefaultValue(0)]
        [Display(Description="This is used to handle the order in which products are listed.")]
        public int Sort { get; set; }
        [Display(Description = "This measurement is in inches.")]
        [DefaultValue(1.0)]
        [Range(0.1, Double.MaxValue)]
        [Required]
        public double Height { get; set; }
        [Display(Description = "This measurement is in inches.")]
        [DefaultValue(1.0)]
        [Range(0.1, Double.MaxValue)]
        [Required]
        public double Width { get; set; }
        [Display(Description = "This measurement is in inches.")]
        [DefaultValue(1.0)]
        [Range(0.1, Double.MaxValue)]
        [Required]
        public double Length { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}oz")]
        [DefaultValue(1.0)]
        [Range(0.1, Double.MaxValue)]
        [Required]
        public double Weight { get; set; }
        [DefaultValue(0.0)]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double BaseCost { get; set; }
        [DefaultValue(0.0)]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double BasePrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double WholesalePrice { get; set; }
        [DefaultValue(0.0)]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double MSRP { get; set; }
        [DefaultValue(0.0)]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double Surcharge { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double Handling { get; set; }
        [DefaultValue(false)]
        public bool FreeShipping { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public DateTime DateAvailable { get; set; }
        public bool Published { get; set; }
        public bool AllowGiftMessage { get; set; }
        [Display(Name = "Admin Notes")]
        [DisplayName("Admin Notes 2")]
        public string AdminNotes { get; set; }
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
        public string CreatedBy  { get; set; }

        //SEO
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }

        [ForeignKey("InventoryId")]
        public Inventory Inventory { get; set; }

        public virtual List<ProductCategory> Categories { get; set; }
        public virtual List<ProductTrait> Traits { get; set; }

        public virtual List<ProductFile> Files { get; set; }
        public virtual List<ProductTaxRate> TaxRates { get; set; }
        //public virtual List<Review> Reviews { get; set; }

        public virtual List<OrderProduct> OrderProducts { get; set; }
        public virtual List<ProductView> ProductViews { get; set; }
        //public virtual List<ProductDiscount> ProductDiscounts { get; set; }
        public virtual List<ProductDiscount> Discounts { get; set; }

    }
}
