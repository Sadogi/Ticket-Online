using MVVMTools.Commands;
using MVVMTools.ViewModels;
using PatternTools.Mediator;
using PatternTools.Mediator.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketOnline.Interfaces;
using TicketOnline.Models.Data;
using TicketOnline.Models.Data.Repositories;
using TicketOnlineWpf.Utils;

namespace TicketOnlineWpf.ViewModels
{
    public class EventViewModel : EntityViewModelBase<Event>
    {
        IUserEventRepository<Event> _eventRequester;
        private ICommand _updateCommand, _deleteCommand, _detailsCommand, _bookingsCommand, _commentsCommand;
        private string _name, _type, _organizer, _description, _location;
        private DateTime? _date;
        private int? _tickets;
        private double? _price;
        //private EventWindow ew = new EventWindow();

        public EventViewModel(Event entity) : base(entity)
        {
            _eventRequester = new EventRequester("http://localhost:51049/api/");
            Name = Entity.Name;
            Type = Entity.Type;
            Organizer = Entity.Organizer;
            Date = Entity.Date;
            Location = Entity.Location;            
            Tickets = Entity.Tickets;
            Price = Entity.Price;
            Description = Entity.Description;
        }

        public int Id
        {
            get { return Entity.Id; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
                //if (_name != value)
                //{
                //    _name = value;
                //    RaisePropertyChanged(nameof(Name));
                //}
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                Set(ref _type, value);
                //if (_type != value)
                //{
                //    _type = value;
                //    RaisePropertyChanged(nameof(Type));
                //}
            }
        }

        public string Organizer
        {
            get { return _organizer; }
            set
            {
                Set(ref _organizer, value);
                //if (_organizer != value)
                //{
                //    _organizer = value;
                //    RaisePropertyChanged(nameof(Organizer));
                //}
            }
        }
                                
        public DateTime? Date
        {
            get { return _date; }
            set
            {
                Set(ref _date, value);
                //if (_date != value)
                //{
                //    _date = value;
                //    RaisePropertyChanged(nameof(Date));
                //}
            }
        }

        public string Location
        {
            get { return _location; }
            set
            {
                Set(ref _location, value);
                //if (_location != value)
                //{
                //    _location = value;
                //    RaisePropertyChanged(nameof(Location));
                //}
            }
        }

        public int? Tickets
        {
            get { return _tickets; }
            set
            {
                Set(ref _tickets, value);
                //if (_tickets != value)
                //{
                //    _tickets = value;
                //    RaisePropertyChanged(nameof(Tickets));
                //}
            }
        }

        public double? Price
        {
            get { return _price; }
            set
            {
                Set(ref _price, value);
                //if (_price != value)
                //{
                //    _price = value;
                //    RaisePropertyChanged(nameof(Price));
                //}
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                Set(ref _description, value);
                //if (_description != value)
                //{
                //    _description = value;
                //    RaisePropertyChanged(nameof(Description));
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

        public ICommand CommentsCommand
        {
            get
            {
                return _commentsCommand ?? (_commentsCommand = new RelayCommand(Comments));
            }
        }

        private bool CanUpdate()
        {
            return Name != Entity.Name
                || Type != Entity.Type
                || Organizer != Entity.Organizer
                || Date != Entity.Date
                || Location != Entity.Location                
                || Tickets != Entity.Tickets
                || Price != Entity.Price
                || Description != Entity.Description;
        }

        private void Update()
        {            
            string oldName = Entity.Name;
            string oldType = Entity.Type;
            string oldOrganizer = Entity.Organizer;
            DateTime? oldDate = Entity.Date;
            string oldLocation = Entity.Location;            
            int? oldTickets = Entity.Tickets;
            double? oldPrice = Entity.Price;
            string oldDescription = Entity.Description;

            Entity.Name = Name;
            Entity.Type = Type;
            Entity.Organizer = Organizer;
            Entity.Date = Date;
            Entity.Location = Location;            
            Entity.Tickets = Tickets;
            Entity.Price = Price;
            Entity.Description = Description;

            _eventRequester.Update(Id, Entity);
            UpdateCommand.RaiseCanExecuteChanged();

            if (!_eventRequester.Update(Id, Entity))
            {
                Entity.Name = oldName;
                Entity.Type = oldType;
                Entity.Organizer = oldOrganizer;
                Entity.Date = oldDate;
                Entity.Location = oldLocation;                
                Entity.Tickets = oldTickets;
                Entity.Price = oldPrice;
                Entity.Description = oldDescription;
            }

            EventWindow eventWindow = App.Current.Windows.OfType<EventWindow>().FirstOrDefault();
            eventWindow.Close();
            //ew.Close();
        }

        private void Delete()
        {
            _eventRequester.Delete(Id);
            //Messenger<EventViewModel>.Instance.Send("DeleteEvent", this);
            Messenger<DeleteMessage<EventViewModel>>.Instance.Send(new DeleteMessage<EventViewModel>(this));
        }

        private void Details()
        {
            EventWindow ew = new EventWindow();
            ew.DataContext = this;

            ew.Show();
        }

        private void Bookings()
        {
            BookingListWindow blw = new BookingListWindow(Id);
            //blw.DataContext = this;

            blw.Show();
        }

        private void Comments()
        {
            CommentListWindow clw = new CommentListWindow(Id);
            //blw.DataContext = this;

            clw.Show();
        }
    }
}