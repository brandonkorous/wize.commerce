using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
    [Table("ProductCategoryRelations")]
    public class CategoryRelation : ITenantModel
    {
        [Key]
        [Column("ParentProductCategoryId", Order = 0)]
        public int ParentCategoryId { get; set; }
        [Key]
        [Column("ChildProductCategoryId", Order = 1)]
        public int ChildCategoryId { get; set; }

        public Category Parent { get; set; }
        public Category Child { get; set; }
    }
}
