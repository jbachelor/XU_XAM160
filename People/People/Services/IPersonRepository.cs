using System.Collections.Generic;
using People.Models;

namespace People.Services
{
    public interface IPersonRepository
    {
        string StatusMessage { get; set; }

        void AddNewPerson(string name);
        List<Person> GetAllPeople();
    }
}