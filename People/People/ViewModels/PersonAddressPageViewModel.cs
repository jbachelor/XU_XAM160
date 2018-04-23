using System;
using People.Helpers;
using People.Models;
using People.Services;
using Prism.Commands;
using Prism.Navigation;

namespace People.ViewModels
{
    public class PersonAddressPageViewModel : ViewModelBase
    {
        private IPersonRepository _personRepository;

        public DelegateCommand SaveCommand { get; set; }

        private Person _thisPerson;
        public Person ThisPerson
        {
            get { return _thisPerson; }
            set { SetProperty(ref _thisPerson, value); }
        }

        private Address _thisPersonAddress;
        public Address ThisPersonsAddress
        {
            get { return _thisPersonAddress; }
            set { SetProperty(ref _thisPersonAddress, value); }
        }

        public PersonAddressPageViewModel(INavigationService navigationService, IPersonRepository personRepository)
            : base(navigationService)
        {
            _personRepository = personRepository;
            SaveCommand = new DelegateCommand(OnSaveTapped);
        }

        private async void OnSaveTapped()
        {
            Console.WriteLine($"**** {this.GetType().Name}.{nameof(OnSaveTapped)}");

            await _personRepository.SaveAddressForPerson(ThisPersonsAddress);

            await NavigationService.GoBackAsync(null, false, true);
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (parameters.ContainsKey(AppConstants.PERSON_NAV_KEY))
            {
                ThisPerson = parameters[AppConstants.PERSON_NAV_KEY] as Person;

                ThisPersonsAddress = await _personRepository.GetFirstAddressByPersonId(ThisPerson.Id);

                if (ThisPersonsAddress == null)
                {
                    ThisPersonsAddress = new Address
                    {
                        PersonId = ThisPerson.Id
                    };
                }
            }
        }
    }
}
