using System;
using System.Collections.Generic;
using People.Models;
using SQLite;

namespace People.Services
{
    public class PersonRepository
    {
        private SQLiteConnection _sqliteConnection;

        public string StatusMessage { get; set; }

        public PersonRepository(string dbPath)
        {
            // TODO: Initialize a new SQLiteConnection
            _sqliteConnection = new SQLiteConnection(dbPath);
            // TODO: Create the Person table
            _sqliteConnection.CreateTable<Person>();
        }

        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrWhiteSpace(name))
                    throw new Exception("Name cannot be blank.");

                // TODO: insert a new person into the Person table
                result = _sqliteConnection.Insert(new Person { Name = name });
                StatusMessage = $"{result} record(s) added [Name: {name}]";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to add [{name}]. Error: {ex.Message}";
            }

        }

        public List<Person> GetAllPeople()
        {
            // TODO: return a list of people saved to the Person table in the database
            return null;
        }
    }
}
