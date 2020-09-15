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
    public class CommentListByUserViewModel : CollectionViewModelBase<CommentViewModel>
    {
        ApiRequester _apiRequester;
        private ICommand _addCommand;
        private int _userId;
        private string _user, _event;
        private DateTime _date;

        public CommentListByUserViewModel()
        {
            _apiRequester = new ApiRequester("http://localhost:51049/api/");
            //Messenger<CommentViewModel>.Instance.Register("DeleteUserComment", OnDeleteUserComment); // voir *
            Messenger<DeleteMessage<CommentViewModel>>.Instance.Register(OnDeleteUserComment);
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

        public ICommand AddCommand
        {
            get { return _addCommand ?? new RelayCommand(Add, CanAdd); }
        }

        private bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(User)
                && !string.IsNullOrWhiteSpace(Event)
                && !string.IsNullOrWhiteSpace(Date.ToString());
        }

        private void Add()
        {
            GetComment c = new GetComment();
            c = _apiRequester.Create(c, "comment");
            Items = LoadItems();
            //Items.Add(new CommentViewModel(c));
        }

        private void OnDeleteUserComment(DeleteMessage<CommentViewModel> message)
        {
            Items = LoadItems();
            Items.Remove(message.ViewModel);
        }

        //private void OnDeleteUserComment(CommentViewModel instance)  // *
        //{
        //    Items.Remove(instance);
        //}

        protected override ObservableCollection<CommentViewModel> LoadItems()
        {
            return new ObservableCollection<CommentViewModel>(_apiRequester.GetAll<GetComment>("comment/user/" + UserId).Select(c => new CommentViewModel(c)));
        }
    }
}
