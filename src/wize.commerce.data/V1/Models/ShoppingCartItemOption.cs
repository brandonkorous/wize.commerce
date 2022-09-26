using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
    public class ShoppingCartItemOption : ITenantModel
    {
        [Key]
        [Column(Order = 0)]
        public int ShoppingCartItemId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ProductTraitId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int? ProductTraitOptionId { get; set; }
        public virtual ShoppingCartItem ShoppingCartItem { get; set; }
        public virtual ProductTrait Trait { get; set; }
        public virtual ProductTraitOption Option { get; set; }
    }
}
