using People.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace People.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        IFileAccessHelper _fileAccessHelper;

        private string _databasePath;
        public string DatabasePath
        {
            get { return _databasePath; }
            set { SetProperty(ref _databasePath, value); }
        }

        public MainPageViewModel(INavigationService navigationService,
                                IFileAccessHelper fileAccessHelper)
            : base(navigationService)
        {
            Title = "SQLite Intro";
			_fileAccessHelper = fileAccessHelper;

            DatabasePath = _fileAccessHelper.GetSQLiteDatabasePath(
                AppConstants.DATABASE_FILE_NAME);
        }
    }
}
