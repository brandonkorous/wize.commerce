using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.commerce.data.v1.Enums;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
	public class ProductTrait : ITenantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductTraitId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int TraitId { get; set; }
        public string Prompt { get; set; }
        public int Sort { get; set; }
        public bool Required { get; set; }
        [DefaultValue(PriceAdjustmentType.Dollar)]
        public PriceAdjustmentType AdjustmentType { get; set; }
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

        public virtual Product Product { get; set; }
        public virtual Trait Trait { get; set; }
        public virtual List<ProductTraitOption> TraitOptions { get; set; }
    }
}
