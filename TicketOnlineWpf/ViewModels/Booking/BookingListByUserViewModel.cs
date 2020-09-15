using MVVMTools.Commands;
using MVVMTools.ViewModels;
using PatternTools.Mediator;
using PatternTools.Mediator.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TicketOnline.Models.Data.Repositories;
using TicketOnline.Models.Global;

namespace TicketOnlineWpf.ViewModels
{
    public class BookingListByUserViewModel : CollectionViewModelBase<BookingViewModel>
    {
        ApiRequester _apiRequester;
        private ICommand _addCommand;
        private int _userId, _ticketsPurchased;
        private string _user, _email, _event;
        private DateTime _purchaseDate;
        private double _amount;

        public BookingListByUserViewModel()
        {
            _apiRequester = new ApiRequester("http://localhost:51049/api/");
            //Messenger<BookingViewModel>.Instance.Register("DeleteBooking", OnDeleteBooking); // voir *
            Messenger<DeleteMessage<BookingViewModel>>.Instance.Register(OnDeleteBooking);
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Event
        {
            get { return _event; }
            set { _event = value; }
        }

        public DateTime PurchaseDate
        {
            get { return _purchaseDate; }
            set
            {
                if (_purchaseDate != value)
                {
                    _purchaseDate = value;
                    RaisePropertyChanged(nameof(PurchaseDate));
                }
            }
        }

        public int TicketsPurchased
        {
            get { return _ticketsPurchased; }
            set
            {
                if (_ticketsPurchased != value)
                {
                    _ticketsPurchased = value;
                    RaisePropertyChanged(nameof(TicketsPurchased));
                }
            }
        }

        public double Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    RaisePropertyChanged(nameof(Amount));
                }
            }
        }

        public ICommand AddCommand
        {
            get { return _addCommand ?? new RelayCommand(Add, CanAdd); }
        }

        private bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(User)
                && !string.IsNullOrWhiteSpace(Email)
                && !string.IsNullOrWhiteSpace(Event)
                && !string.IsNullOrWhiteSpace(PurchaseDate.ToString())
                && !string.IsNullOrWhiteSpace(Amount.ToString());
        }

        private void Add()
        {
            GetBooking c = new GetBooking();
            c = _apiRequester.Create(c, "booking");
            Items = LoadItems();
            //Items.Add(new BookingViewModel(c));
        }

        private void OnDeleteBooking(DeleteMessage<BookingViewModel> message)
        {
            Items = LoadItems();
            Items.Remove(message.ViewModel);
        }

        //private void OnDeleteBooking(BookingViewModel instance)  // *
        //{
        //    Items.Remove(instance);
        //}

        protected override ObservableCollection<BookingViewModel> LoadItems()
        {
            return new ObservableCollection<BookingViewModel>(_apiRequester.GetAll<GetBooking>("booking/user/" + UserId).Select(c => new BookingViewModel(c)));
        }
    }
}
