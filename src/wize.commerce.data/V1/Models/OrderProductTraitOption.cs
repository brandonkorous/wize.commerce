using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
    public class OrderProductTraitOption : ITenantModel
    {
        [Key]
        [Column(Order = 0)]
        public Guid OrderId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int OrderProductId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ProductTraitId { get; set; }
        [Key]
        [Column(Order = 3)]
        public int ProductTraitOptionId { get; set; }
        
        public virtual Order Order { get; set; }
        public virtual OrderProduct Product { get; set; }
        public virtual ProductTrait Trait { get; set; }
        public virtual ProductTraitOption Option { get; set; }
    }
}
