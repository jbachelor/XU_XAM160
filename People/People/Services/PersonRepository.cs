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
        }

        public async Task AddNewPerson(string name)
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
                Console.WriteLine($"**** {this.GetType().Name}.{nameof(AddNewPerson)}:  {StatusMessage}");
            }
        }

        public async Task<List<Person>> GetAllPeople()
        {
            // TODO: return a list of people saved to the Person table in the database
            List<Person> people = new List<Person>();
            try
            {
                people = await _sqliteConnection.Table<Person>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve people:  {ex.Message}";
            }

            Console.WriteLine($"**** {this.GetType().Name}.{nameof(GetAllPeople)}:  Returning {people.Count} people.");
            return people;
        }
    }
}
