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
using TicketOnline.Models.Data.Mappers;
using TicketOnline.Models.Data.Repositories;

namespace TicketOnlineWpf.ViewModels
{
    public class EventListViewModel : CollectionViewModelBase<EventViewModel>
    {
        IUserEventRepository<Event> _eventRequester;
        private ICommand _addCommand;
        private string _name, _type, _organizer, _description, _location;
        private DateTime? _date;
        private int? _tickets;
        private double? _price;

        public EventListViewModel() 
        {
            _eventRequester = new EventRequester("http://localhost:51049/api/");
            //Messenger<EventViewModel>.Instance.Register("DeleteEvent", OnDeleteEvent); // voir *
            Messenger<DeleteMessage<EventViewModel>>.Instance.Register(OnDeleteEvent);
        }

        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
                //if (_name != value)
                //{
                //    RaisePropertyChanging(nameof(Name));
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
                //    RaisePropertyChanging(nameof(Type));
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
                //    RaisePropertyChanging(nameof(Organizer));
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
                //    RaisePropertyChanging(nameof(Date));
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
                //    RaisePropertyChanging(nameof(Location));
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
                //    RaisePropertyChanging(nameof(Tickets));
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
                //    RaisePropertyChanging(nameof(Price));
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
                //    RaisePropertyChanging(nameof(Description));
                //    _description = value;
                //    RaisePropertyChanged(nameof(Description));
                //}
            }
        }

        public ICommand AddCommand 
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(Add, CanAdd)); }
        }

        private bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Type)
                && !string.IsNullOrWhiteSpace(Organizer);
        }

        private void Add()
        {
            Event e = new Event(Name, Type, Organizer, Date, Location, Tickets, Price, Description);
            e = _eventRequester.Create(e);
            Items = LoadItems();
            //Items.Add(new EventViewModel(e));

            Name = Type = Organizer = Location = Description = null;
            Date = null;
            Price = Tickets = null;
        }

        private void OnDeleteEvent(DeleteMessage<EventViewModel> message)
        {
            Items = LoadItems();
            Items.Remove(message.ViewModel);
        }

        //private void OnDeleteEvent(EventViewModel instance)  // *
        //{
        //    Items.Remove(instance);
        //}

        protected override ObservableCollection<EventViewModel> LoadItems()
        {
            return new ObservableCollection<EventViewModel>(_eventRequester.GetAll().Select(e => new EventViewModel(e)));
        }
    }
}
