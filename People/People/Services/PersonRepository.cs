using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using People.Helpers;
using People.Models;
using SQLite;

namespace People.Services
{
    public class PersonRepository : IPersonRepository
    {
        private SQLiteAsyncConnection _sqliteConnection;
        private IFileAccessHelper _fileAccessHelper;

        public string StatusMessage { get; set; }

        public PersonRepository(IFileAccessHelper fileAccessHelper)
        {
            _fileAccessHelper = fileAccessHelper;

            _sqliteConnection = new SQLiteAsyncConnection(
                _fileAccessHelper.GetSQLiteDatabasePath(AppConstants.DATABASE_FILE_NAME));

            CreateTablesSynchronously();
        }

        /// <summary>
        /// This method is made to be synchronous so that we can be sure the tables exist before running operations against them.
        /// </summary>
        private void CreateTablesSynchronously()
        {
            _sqliteConnection.CreateTableAsync<Person>().Wait();
            _sqliteConnection.CreateTableAsync<Address>().Wait();
        }

        public async Task<int> AddNewPersonAsync(string name)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrWhiteSpace(name))
                    throw new Exception("Name cannot be blank.");

                result = await _sqliteConnection.InsertAsync(new Person { Name = name });
                StatusMessage = $"{result} record(s) added [Name: {name}]";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to add [{name}]. Error: {ex.Message}";
            }
            finally
            {
                Console.WriteLine($"**** {this.GetType().Name}.{nameof(AddNewPersonAsync)}:  {StatusMessage}");
            }
            return result;
        }

        public async Task<int> SaveAddressForPerson(Address newAddressForPerson)
        {
            int addressId = 0;
            try
            {
                if (newAddressForPerson.PersonId <= 0)
                {
                    throw new Exception("Person id must be assigned before saving an address.");
                }

                var existingAddress = await _sqliteConnection.Table<Address>()
                                                             .Where(a => a.PersonId == newAddressForPerson.PersonId)
                                                             .FirstOrDefaultAsync();

                if (existingAddress == null)
                {
                    addressId = await _sqliteConnection.InsertAsync(newAddressForPerson);
                }
                else
                {
                    await _sqliteConnection.UpdateAsync(newAddressForPerson);
                }

                StatusMessage = $"Added or updated address for personId {newAddressForPerson.PersonId}:  {newAddressForPerson}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to save new address for personId {newAddressForPerson.PersonId}:  {ex.Message}";
            }

            Console.WriteLine($"**** {this.GetType().Name}.{nameof(SaveAddressForPerson)}:  {StatusMessage}");

            return addressId;
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            List<Person> people = new List<Person>();
            try
            {
                people = await _sqliteConnection.Table<Person>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve people:  {ex.Message}";
            }

            Console.WriteLine($"**** {this.GetType().Name}.{nameof(GetAllPeopleAsync)}:  Returning {people.Count} people.");
            return people;
        }

        public async Task<Person> GetPersonById(int personId)
        {
            Person personFetchedFromDatabase = null;
            try
            {
                personFetchedFromDatabase = await _sqliteConnection.FindAsync<Person>(personId);
                StatusMessage = $"Returning [{personFetchedFromDatabase}]";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve person with id=[{personId}].  EXCEPTION:  {ex.Message}";
            }

            Console.WriteLine($"**** {this.GetType().Name}.{nameof(GetPersonById)}:  {StatusMessage}");
            return personFetchedFromDatabase;
        }

        public async Task<Address> GetFirstAddressByPersonId(int personId)
        {
            Address addressForPerson = null;
            try
            {
                addressForPerson = await
                    _sqliteConnection.Table<Address>()
                                     .Where(a => a.PersonId == personId).FirstOrDefaultAsync();
                StatusMessage = $"Retrieved address for personId [{personId}]:  {addressForPerson}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve address for personId [{personId}]";
            }

            Console.WriteLine($"**** {this.GetType().Name}.{nameof(GetFirstAddressByPersonId)}:  {StatusMessage}");

            return addressForPerson;
        }
    }
}
