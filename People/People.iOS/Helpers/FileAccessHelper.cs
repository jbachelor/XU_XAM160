using System;
using System.IO;
using People.Helpers;
using People.iOS.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileAccessHelper))]
namespace People.iOS.Helpers
{
    public class FileAccessHelper : IFileAccessHelper
    {
        public string GetSQLiteDatabasePath(string databaseFileName)
        {
            string personalFolderPath = Environment.GetFolderPath(
                Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolderPath, "..", AppConstants.iOS_LIBRARY_NAME);

            if (!Directory.Exists(libraryFolder))
            {
                Directory.CreateDirectory(libraryFolder);
            }

            var dbPath = Path.Combine(libraryFolder, databaseFileName);
            Console.WriteLine($"**** {this.GetType().Name}.{nameof(GetSQLiteDatabasePath)}  returning:[{dbPath}]");
            return dbPath;
        }
    }
}
