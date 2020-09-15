using MVVMTools.Commands;
using MVVMTools.ViewModels;
using PatternTools.Mediator;
using PatternTools.Mediator.Messages;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;
using TicketOnline.Interfaces;
using TicketOnline.Models.Data;
using TicketOnline.Models.Data.Repositories;

namespace TicketOnlineWpf.ViewModels
{
    public class UserViewModel : EntityViewModelBase<User>
    {
        IUserEventRepository<User> _eventRequester;
        private ICommand _updateCommand, _deleteCommand, _detailsCommand, _bookingsCommand;
        private string _lastName, _firstName, _screenName, _email, _passwd, _address;
        private bool _isActive, _isAdmin;
        //private UserWindow uw = new UserWindow();

        public UserViewModel(User entity) : base(entity)
        {
            _eventRequester = new UserRequester("http://localhost:51049/api/");
            LastName = Entity.LastName;
            FirstName = Entity.FirstName;
            ScreenName = Entity.ScreenName;
            Email = Entity.Email;
            //Passwd = Entity.Passwd;
            Address = Entity.Address;
            IsActive = Entity.IsActive;
            IsAdmin = Entity.IsAdmin;
        }

        public int Id
        {
            get { return Entity.Id; }
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

        //public string Passwd
        //{
        //    get { return _passwd; }
        //    set
        //    {
        //        Set(ref _passwd, value);

        //        //if (_passwd != value)
        //        //{
        //        //    _passwd = value;
        //        //    RaisePropertyChanged(nameof(Passwd));
        //        //}
        //    }
        //}

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

        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete));
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new RelayCommand(Update, CanUpdate));
            }
        }

        public ICommand DetailsCommand
        {
            get
            {
                return _detailsCommand ?? (_detailsCommand = new RelayCommand(Details));
            }
        }

        public ICommand BookingsCommand
        {
            get
            {
                return _bookingsCommand ?? (_bookingsCommand = new RelayCommand(Bookings));
            }
        }

        private bool CanUpdate()
        {
            return LastName != Entity.LastName
                || FirstName != Entity.FirstName
                || ScreenName != Entity.ScreenName
                || Email != Entity.Email
                //|| Passwd != Entity.Passwd
                || Address != Entity.Address
                || IsActive != Entity.IsActive
                || IsAdmin != Entity.IsAdmin;
        }

        private void Update()
        {
            string oldLastName = Entity.LastName;
            string oldFirstName = Entity.FirstName;
            string oldScreenName = Entity.ScreenName;
            string oldEmail = Entity.Email;
            //string oldPasswd = Entity.Passwd;
            string oldAddress = Entity.Address;
            bool oldIsActive = Entity.IsActive;
            bool oldIsAdmin = Entity.IsAdmin;

            Entity.LastName = LastName;
            Entity.FirstName = FirstName;
            Entity.ScreenName = ScreenName;
            Entity.Email = Email;
            //Entity.Passwd = Passwd;
            Entity.Address = Address;
            Entity.IsActive = IsActive;
            Entity.IsAdmin = IsAdmin;

            UpdateCommand.RaiseCanExecuteChanged();

            if (!_eventRequester.Update(Id, Entity))
            {
                Entity.LastName = oldLastName;
                Entity.FirstName = oldFirstName;
                Entity.ScreenName = oldScreenName;
                Entity.Email = oldEmail;
                //Entity.Passwd = oldPasswd;
                Entity.Address = oldAddress;
                Entity.IsActive = oldIsActive;
                Entity.IsAdmin = oldIsAdmin;
            }

            //uw.Close();
        }

        private void Delete()
        {
            _eventRequester.Delete(Id);
            //Messenger<UserViewModel>.Instance.Send("DeleteUser", this);
            Messenger<DeleteMessage<UserViewModel>>.Instance.Send(new DeleteMessage<UserViewModel>(this));
        }

        private void Details()
        {
            UserWindow uw = new UserWindow();
            uw.DataContext = this;

            uw.Show();
        }

        private void Bookings()
        {
            BookingListByUserWindow blbuw = new BookingListByUserWindow();
            blbuw.DataContext = this;

            blbuw.Show();
        }
    }
}