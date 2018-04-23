using System.Collections.Generic;
using System.Threading.Tasks;
using People.Models;

namespace People.Services
{
    public interface IPersonRepository
    {
        string StatusMessage { get; set; }

        Task<int> AddNewPersonAsync(string name);
        Task<List<Person>> GetAllPeopleAsync();
        Task<Person> GetPersonById(int personId);
        Task<Address> GetFirstAddressByPersonId(int personId);
        Task<int> SaveAddressForPerson(Address newAddressForPerson);
    }
}