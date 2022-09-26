using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.data.v1.Models
{
	public class Address : ITenantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }
        /// <summary>
        /// Primary Street Address
        /// </summary>
        [Required]
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public static bool operator ==(Address a, Address b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;

            if (((object)a == null) || ((object)b == null))
                return false;

            return a.Street1 == b.Street1 &&
                    a.Street2 == b.Street2 &&
                    a.City == b.City &&
                    a.State == b.State &&
                    a.PostalCode == b.PostalCode &&
                    a.Country == b.Country;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Address))
                return false;

            return this == (Address)obj;
        }
        public static bool operator !=(Address a, Address b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return ($"{this.Street1}|{this.Street2}|{this.City}|{this.State}|{this.PostalCode}|{this.Country}").GetHashCode();
        }
        //public EasyPost.Address EasyAddress()
        //{
        //    EasyPost.Address address = new EasyPost.Address();
        //    address.street1 = this.Street1;
        //    address.street2 = this.Street2;
        //    address.city = this.City;
        //    address.state = this.State;
        //    address.zip = this.PostalCode;
        //    address.country = this.Country;
        //    return address;
        //}
    }
}
