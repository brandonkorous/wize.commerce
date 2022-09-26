using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
	public class UserOrders : ITenantModel
    {
        [Key]
        public Guid UserId { get; set; }
        //public virtual List<Order> Orders { get; set; }
    }
}
