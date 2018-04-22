using System;
using People.Helpers;
using SQLite;

namespace People.Models
{
    [Table(AppConstants.PEOPLE_TABLE_NAME)]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250), Unique]
        public string Name { get; set; }

		public override string ToString()
		{
            return $"Id={Id}, Name={Name}";
		}
	}
}
