using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
	public class Discount : ITenantModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountId { get; set; }
        //[Index(IsUnique = true)]
        [StringLength(450)]
        public string Name { get; set; }
        public bool UsePercentage { get; set; }
        [DefaultValue(0.0)]
        public decimal Percentage { get; set; }
        [DefaultValue(0.0)]
        public decimal Amount { get; set; }
        public decimal MaximumAmount { get; set; }
        private DateTime? _startDate;
        [Display(Name = "Start Date", Description = "Format: MM/DD/YYYY, this will become active on or after this date.")]
        public DateTime? StartDate
        {
            get
            {
                return _startDate?.ToLocalTime();
            }
            set
            {
                if (!value.HasValue)
                {
                    _startDate = value;
                    return;
                }
                if (value.Value.Kind == DateTimeKind.Utc)
                {
                    _startDate = value;
                }
                else if (value.Value.Kind == DateTimeKind.Local)
                {
                    _startDate = value.Value.ToUniversalTime();
                }
                else
                {
                    _startDate = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
                }
            }
        }
        private DateTime? _endDate;
        [Display(Name = "End Date", Description = "Format: MM/DD/YYYY, this will be active before or upto this date.")]
        public DateTime? EndDate
        {
            get
            {
                return _endDate?.ToLocalTime();
            }
            set
            {
                if (!value.HasValue)
                {
                    _endDate = value;
                    return;
                }
                if (value.Value.Kind == DateTimeKind.Utc)
                {
                    _endDate = value;
                }
                else if (value.Value.Kind == DateTimeKind.Local)
                {
                    _endDate = value.Value.ToUniversalTime();
                }
                else
                {
                    _endDate = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
                }
            }
        }
        public bool RequiresCode { get; set; }
        [RegularExpression(pattern: "^[a-zA-Z0-9]*$", ErrorMessage = "Codes can only contain upper and lower case letters.")]
        public string Code { get; set; }
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

        //public virtual List<ProductCategoryDiscount> ProductCategoryDiscounts { get; set; }
        public virtual List<ProductDiscount> Products { get; set; } 
    }
}
