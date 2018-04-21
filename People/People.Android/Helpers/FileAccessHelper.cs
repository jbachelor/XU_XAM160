using System;
using System.IO;
using People.Droid.Helpers;
using People.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileAccessHelper))]
namespace People.Droid.Helpers
{
    public class FileAccessHelper : IFileAccessHelper
    {
        public string GetSQLiteDatabasePath(string databaseFileName)
        {
            string personalFolderPath = Environment.GetFolderPath(
                Environment.SpecialFolder.Personal);
            var dbPath = Path.Combine(personalFolderPath, databaseFileName);
            Console.WriteLine($"**** {this.GetType().Name}.{nameof(GetSQLiteDatabasePath)}:  returning:[{dbPath}]");
            return dbPath;
        }
    }
}
