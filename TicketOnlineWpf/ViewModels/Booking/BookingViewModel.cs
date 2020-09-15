using MVVMTools.Commands;
using MVVMTools.ViewModels;
using PatternTools.Mediator;
using PatternTools.Mediator.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using TicketOnline.Models.Data.Repositories;
using TicketOnline.Models.Global;

namespace TicketOnlineWpf.ViewModels
{
    public class BookingViewModel : EntityViewModelBase<GetBooking>
    {
        ApiRequester _commentRequester;
        private ICommand _updateCommand, _deleteCommand, _detailsCommand;
        private DateTime _purchaseDate;
        private int _ticketsPurchased;
        private double _amount;
        //private BookingWindow bw = new BookingWindow();

        public BookingViewModel(GetBooking entity) : base(entity)
        {
            _commentRequester = new ApiRequester("http://localhost:51049/api/");
            PurchaseDate = Entity.PurchaseDate;
            TicketsPurchased = Entity.TicketsPurchased;
            Amount = Entity.Amount;
        }

        public int Id
        {
            get { return Entity.Id; }
        }

        public int UserId
        {
            get { return Entity.UserId; }
        }

        public int EventId
        {
            get { return Entity.EventId; }
        }

        public string User
        {
            get { return Entity.User; }
        }

        public string Email
        {
            get { return Entity.Email; }
        }

        public string Event
        {
            get { return Entity.Event; }
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
                    RaisePropertyChanged(nameof(TicketsPurchased));
                }
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

        //public ICommand DetailsCommand
        //{
        //    get
        //    {
        //        return _detailsCommand ?? (_detailsCommand = new RelayCommand(Details));
        //    }
        //}

        private bool CanUpdate()
        {
            return PurchaseDate != Entity.PurchaseDate
                || TicketsPurchased != Entity.TicketsPurchased
                || Amount != Entity.Amount;
        }

        private void Update()
        {
            DateTime oldPurchaseDate = Entity.PurchaseDate;
            int oldTicketsPurchased = Entity.TicketsPurchased;
            double oldAmount = Entity.Amount;

            Entity.PurchaseDate = PurchaseDate;
            Entity.TicketsPurchased = TicketsPurchased;
            Entity.Amount = Amount;

            _commentRequester.Update(Entity, "booking/" + Id);
            UpdateCommand.RaiseCanExecuteChanged();

            if (!_commentRequester.Update(Entity, "booking/" + Id))
            {
                Entity.PurchaseDate = oldPurchaseDate;
                Entity.TicketsPurchased = oldTicketsPurchased;
                Entity.Amount = oldAmount;
            }

            //bw.Close();
        }

        private void Delete()
        {
            _commentRequester.Delete<GetBooking>("booking/" + Id);
            //Messenger<BookingViewModel>.Instance.Send("DeleteBooking", this);
            Messenger<DeleteMessage<BookingViewModel>>.Instance.Send(new DeleteMessage<BookingViewModel>(this));
        }

        //private void Details()
        //{
        //    bw.DataContext = this;

        //    bw.Show();
        //}
    }
}