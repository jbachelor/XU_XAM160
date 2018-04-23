using System;
using People.Helpers;
using SQLite;

namespace People.Models
{
    [Table(AppConstants.ADDRESS_TABLE_NAME)]
    public class Address
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public int PersonId { get; set; }

        [MaxLength(250)]
        public string StreetAddress { get; set; }

        [MaxLength(250)]
        public string StreetAddressLine2 { get; set; }

        [MaxLength(250)]
        public string City { get; set; }

        [MaxLength(250)]
        public string State { get; set; }

        [MaxLength(250)]
        public string Zipcode { get; set; }

		public override string ToString()
		{
            return $"Id={Id}, PersonId={PersonId}, Street={StreetAddress}, Street-2={StreetAddressLine2}, City={City}, State={State}, Zip={Zipcode}";
		}
	}
}
