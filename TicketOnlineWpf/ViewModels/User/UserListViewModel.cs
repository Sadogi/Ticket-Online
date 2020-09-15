using MVVMTools.Commands;
using MVVMTools.ViewModels;
using PatternTools.Mediator;
using PatternTools.Mediator.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TicketOnline.Interfaces;
using TicketOnline.Models.Data;
using TicketOnline.Models.Data.Repositories;

namespace TicketOnlineWpf.ViewModels
{
    public class UserListViewModel : CollectionViewModelBase<UserViewModel>
    {
        IUserEventRepository<User> _userRequester;
        private ICommand _addCommand;
        private string _lastName, _firstName, _screenName, _email, _passwd, _address;
        private bool _isActive, _isAdmin;

        public UserListViewModel()
        {
            _userRequester = new UserRequester("http://localhost:51049/api/");
            //Messenger<UserViewModel>.Instance.Register("DeleteUser", OnDeleteUser); // voir *
            Messenger<DeleteMessage<UserViewModel>>.Instance.Register(OnDeleteUser);
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                Set(ref _lastName, value);
                //if (_lastName != value)
                //{
                //    _lastName = value;
                //    RaisePropertyChanged(nameof(LastName));
                //}
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                Set(ref _firstName, value);

                //if (_firstName != value)
                //{
                //    _firstName = value;
                //    RaisePropertyChanged(nameof(FirstName));
                //}
            }
        }

        public string ScreenName
        {
            get { return _screenName; }
            set
            {
                Set(ref _screenName, value);

                //if (_screenName != value)
                //{
                //    _screenName = value;
                //    RaisePropertyChanged(nameof(ScreenName));
                //}
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                Set(ref _email, value);

                //if (_email != value)
                //{
                //    _email = value;
                //    RaisePropertyChanged(nameof(Email));
                //}
            }
        }

        public string Passwd
        {
            get { return _passwd; }
            set
            {
                Set(ref _passwd, value);

                //if (_passwd != value)
                //{
                //    _passwd = value;
                //    RaisePropertyChanged(nameof(Passwd));
                //}
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                Set(ref _address, value);

                //if (_address != value)
                //{
                //    _address = value;
                //    RaisePropertyChanged(nameof(Address));
                //}
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                Set(ref _isActive, value);

                //if (_isActive != value)
                //{
                //    _isActive = value;
                //    RaisePropertyChanged(nameof(IsActive));
                //}
            }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                Set(ref _isAdmin, value);

                //if (_isAdmin != value)
                //{
                //    _isAdmin = value;
                //    RaisePropertyChanged(nameof(IsAdmin));
                //}
            }
        }

        public ICommand AddCommand
        {
            get { return _addCommand ?? new RelayCommand(Add, CanAdd); }
        }

        private bool CanAdd()
        {
            //return !string.IsNullOrWhiteSpace(ScreenName)
            //    && !string.IsNullOrWhiteSpace(Email)
            //    && !string.IsNullOrWhiteSpace(Passwd);
            return true;
        }

        private void Add()
        {
            User u = new User(LastName, FirstName, ScreenName, Email, Passwd, Address);
            u = _userRequester.Create(u);
            Items = LoadItems();
            //Items.Add(new UserViewModel(u));

            LastName = FirstName = ScreenName = Email = Passwd = Address = null;
        }

        private void OnDeleteUser(DeleteMessage<UserViewModel> message)
        {
            Items = LoadItems();
            Items.Remove(message.ViewModel);
        }

        //private void OnDeleteUser(UserViewModel instance)  // *
        //{
        //    Items.Remove(instance);
        //}

        protected override ObservableCollection<UserViewModel> LoadItems()
        {
            return new ObservableCollection<UserViewModel>(_userRequester.GetAll().Select(u => new UserViewModel(u)));
        }
    }
}
