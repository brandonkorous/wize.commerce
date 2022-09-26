using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
    public class TaxRateStates : ITenantModel
    {
        [Key]
        [Column(Order = 0)]
        public int TaxRateId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int StateId { get; set; }

        public virtual TaxRate TaxRate { get; set; }
        public virtual State State { get; set; }
    }
}
