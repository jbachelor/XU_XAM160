﻿using People.Helpers;
using People.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace People.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        IFileAccessHelper _fileAccessHelper;

        public DelegateCommand AddNewPersonCommand { get; set; }
        public DelegateCommand GetAllPeopleCommand { get; set; }

        private string _databasePath;
        public string DatabasePath
        {
            get { return _databasePath; }
            set { SetProperty(ref _databasePath, value); }
        }

        private string _personNameText;
        public string PersonNameText
        {
            get { return _personNameText; }
            set { SetProperty(ref _personNameText, value); }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set { SetProperty(ref _statusMessage, value); }
        }

        private ObservableCollection<Person> _people;
        public ObservableCollection<Person> People
        {
            get { return _people; }
            set { SetProperty(ref _people, value); }
        }

        public MainPageViewModel(INavigationService navigationService,
                                IFileAccessHelper fileAccessHelper)
            : base(navigationService)
        {
            Title = "People!";
			_fileAccessHelper = fileAccessHelper;

            AddNewPersonCommand = new DelegateCommand(OnAddNewPersonTapped);
            GetAllPeopleCommand = new DelegateCommand(OnGetAllPeopleTapped);

            DatabasePath = _fileAccessHelper.GetSQLiteDatabasePath(
                AppConstants.DATABASE_FILE_NAME);
        }

        private void OnGetAllPeopleTapped()
        {
            Console.WriteLine($"**** {this.GetType().Name}.{nameof(OnGetAllPeopleTapped)}");
        }

        private void OnAddNewPersonTapped()
        {
            Console.WriteLine($"**** {this.GetType().Name}.{nameof(OnAddNewPersonTapped)}");
        }
    }
}
