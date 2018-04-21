using System;

namespace People.Helpers
{
    public interface IFileAccessHelper
    {
        string GetSQLiteDatabasePath(string databaseFileName);
    }
}
