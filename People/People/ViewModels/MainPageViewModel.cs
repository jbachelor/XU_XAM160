using People.Helpers;
using People.Models;
using People.Services;
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
        private IPersonRepository _personRepository;

        public DelegateCommand AddNewPersonCommand { get; set; }
        public DelegateCommand GetAllPeopleCommand { get; set; }

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

        public MainPageViewModel(INavigationService navigationService, IPersonRepository personRepository)
            : base(navigationService)
        {
            _personRepository = personRepository;
            Title = "People!";

            AddNewPersonCommand = new DelegateCommand(OnAddNewPersonTapped);
            GetAllPeopleCommand = new DelegateCommand(OnGetAllPeopleTapped);
        }

        private async void OnGetAllPeopleTapped()
        {
            Console.WriteLine($"**** {this.GetType().Name}.{nameof(OnGetAllPeopleTapped)}");

            People = new ObservableCollection<Person>(await _personRepository.GetAllPeople());
        }

        private async void OnAddNewPersonTapped()
        {
            Console.WriteLine($"**** {this.GetType().Name}.{nameof(OnAddNewPersonTapped)}");

            await _personRepository.AddNewPerson(PersonNameText);
            PersonNameText = string.Empty;
        }
    }
}
