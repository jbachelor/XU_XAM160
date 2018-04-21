using System.Collections.Generic;
using System.Threading.Tasks;
using People.Models;

namespace People.Services
{
    public interface IPersonRepository
    {
        string StatusMessage { get; set; }

        Task AddNewPerson(string name);
        Task<List<Person>> GetAllPeople();
    }
}