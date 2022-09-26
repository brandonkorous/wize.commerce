using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
	public class ProductTraitOption : ITenantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductTraitOptionId { get; set; }
        [Required]
        public int ProductTraitId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Description = "This measurement is in inches.")]
        [DefaultValue(0.0)]
        public double Height { get; set; }
        [Display(Description = "This measurement is in inches.")]
        [DefaultValue(0.0)]
        public double Width { get; set; }
        [Display(Description = "This measurement is in inches.")]
        [DefaultValue(0.0)]
        public double Length { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}oz")]
        public double Weight { get; set; }
        [DefaultValue(0.0)]
        public double CostAdjustment { get; set; }
        [DefaultValue(0.0)]
        public double PriceAdjustment { get; set; }
        [DefaultValue(0)]
        [Required]
        [Display(Description = "This is the sort of the variations when displayed on a product.")]
        public int Sort { get; set; }
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

        public virtual ProductTrait ProductTrait { get; set; }
        //public virtual List<OrderProductTraitOption> OrderProducts { get; set; }
    }
}
