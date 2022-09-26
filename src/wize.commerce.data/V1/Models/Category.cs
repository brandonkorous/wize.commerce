using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
    [Table("ProductCategories")]
	public class Category : ITenantModel
    {
        public Category()
        {
            Parents = new List<CategoryRelation>();
            Children = new List<CategoryRelation>();
            Products = new List<ProductCategory>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProductCategoryId")]
        public int CategoryId { get; set; }
        //public int? ParentCategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SafeTitle { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
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
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public virtual List<CategoryRelation> Parents { get; set; }
        public virtual List<CategoryRelation> Children { get; set; }

        public virtual List<ProductCategory> Products { get; set; }

    }

}
