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
    public class ReplyListByUserViewModel : CollectionViewModelBase<ReplyViewModel>
    {
        ApiRequester _apiRequester;
        private ICommand _addCommand;
        private int _userId;
        private string _user, _comment;
        private DateTime _date;

        public ReplyListByUserViewModel()
        {
            _apiRequester = new ApiRequester("http://localhost:51049/api/");
            //Messenger<ReplyViewModel>.Instance.Register("DeleteUserReply", OnDeleteUserReply); // voir *
            Messenger<DeleteMessage<ReplyViewModel>>.Instance.Register(OnDeleteUserReply);
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

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
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
                && !string.IsNullOrWhiteSpace(Comment)
                && !string.IsNullOrWhiteSpace(Date.ToString());
        }

        private void Add()
        {
            GetReply c = new GetReply();
            c = _apiRequester.Create(c, "reply/user/" + UserId);
            Items = LoadItems();
            //Items.Add(new ReplyViewModel(c));
        }

        private void OnDeleteUserReply(DeleteMessage<ReplyViewModel> message)
        {
            Items = LoadItems();
            Items.Remove(message.ViewModel);
        }

        //private void OnDeleteUserReply(ReplyViewModel instance)  // *
        //{
        //    Items.Remove(instance);
        //}

        protected override ObservableCollection<ReplyViewModel> LoadItems()
        {
            return new ObservableCollection<ReplyViewModel>(_apiRequester.GetAll<GetReply>("reply/user/" + UserId).Select(c => new ReplyViewModel(c)));
        }
    }
}
