using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
	public class Inventory : ITenantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        //[DefaultValue(-1)]
        public int Threshhold { get; set; }
        public int MinOrderQuantity { get; set; }
        public int MaxOrderQuantity { get; set; }
        public string OutOfStockNotice { get; set; }
        public Product Product { get; set; }
    }
}
