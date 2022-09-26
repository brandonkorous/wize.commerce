using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.V1.Models
{
    public class ProductImage : ITenantModel
    {
        [Key]
        [Column(Order = 0)]
        public int ProductId { get; set; }
        [Key]
        [Column(Order = 1)]
        public Guid ImageId { get; set; }
    }
}
