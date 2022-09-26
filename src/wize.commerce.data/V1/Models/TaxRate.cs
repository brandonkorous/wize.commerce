using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
    public class TaxRate : ITenantModel
    {
        public TaxRate()
        {
            TaxRateStates = new List<TaxRateStates>();
            ProductTaxRates = new List<ProductTaxRate>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaxRateId { get; set; }
        public string Description { get; set; }
        [Required]
        [DefaultValue(0.0)]
        public decimal Percentage { get; set; }
        public virtual List<TaxRateStates> TaxRateStates { get; set; }
        public virtual List<ProductTaxRate> ProductTaxRates { get; set; }
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
    }
}
