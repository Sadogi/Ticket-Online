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
    public class CommentListViewModel : CollectionViewModelBase<CommentViewModel>
    {
        ApiRequester _apiRequester;
        private ICommand _addCommand;
        private int _eventId;
        private string _user, _event, _content;
        private DateTime _date;  

        public CommentListViewModel(int id) 
        {
            this.EventId = id;
            _apiRequester = new ApiRequester("http://localhost:51049/api/");
            //Messenger<CommentViewModel>.Instance.Register("DeleteComment", OnDeleteComment); // voir *
            Messenger<DeleteMessage<CommentViewModel>>.Instance.Register(OnDeleteComment);
        }

        public int EventId
        {
            get { return _eventId; }
            set { _eventId = value; }
        }

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string Event
        {
            get { return _event; }
            set { _event = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    RaisePropertyChanged(nameof(Date));
                }
            }
        }

        public string Content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    RaisePropertyChanged(nameof(Content));
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
                && !string.IsNullOrWhiteSpace(Event)
                && !string.IsNullOrWhiteSpace(Date.ToString())
                && !string.IsNullOrWhiteSpace(Content);
        }

        private void Add()
        {
            GetComment c = new GetComment();
            c = _apiRequester.Create(c, "comment");
            Items = LoadItems();
            //Items.Add(new CommentViewModel(c));
        }

        private void OnDeleteComment(DeleteMessage<CommentViewModel> message)
        {
            Items = LoadItems();
            Items.Remove(message.ViewModel);
        }

        //private void OnDeleteComment(CommentViewModel instance)  // *
        //{
        //    Items.Remove(instance);
        //}

        protected override ObservableCollection<CommentViewModel> LoadItems()
        {
            return new ObservableCollection<CommentViewModel>(_apiRequester.GetAll<GetComment>("comment/event/" + EventId).Select(c => new CommentViewModel(c)));
        }
    }
}
