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
    public class ReplyViewModel : EntityViewModelBase<GetReply>
    {
        ApiRequester _apiRequester;
        private ICommand _updateCommand, _deleteCommand, _detailsCommand;
        private DateTime _date;
        private string _content;
        //private ReplyWindow rw = new ReplyWindow();

        public ReplyViewModel(GetReply entity) : base(entity)
        {
            _apiRequester = new ApiRequester("http://localhost:51049/api/");
            Date = Entity.Date;
            Content = Entity.Content;
        }

        public int Id
        {
            get { return Entity.Id; }
        }

        public int UserId
        {
            get { return Entity.UserId; }
        }

        public int CommentId
        {
            get { return Entity.CommentId; }
        }

        public string User
        {
            get { return Entity.User; }
        }

        public string Comment
        {
            get { return Entity.Comment; }
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
            return Date != Entity.Date
                || Content != Entity.Content;
        }

        private void Update()
        {
            DateTime oldDate = Entity.Date;
            string oldContent = Entity.Content;

            Entity.Date = Date;
            Entity.Content = Content;

            UpdateCommand.RaiseCanExecuteChanged();

            if (!_apiRequester.Update(Entity, "reply/" + Id))
            {
                Entity.Date = oldDate;
                Entity.Content = oldContent;
            }

            //rw.Close();
        }

        private void Delete()
        {
            _apiRequester.Delete<GetReply>("reply/" + Id);
            //Messenger<ReplyViewModel>.Instance.Send("DeleteReply", this);
            Messenger<DeleteMessage<ReplyViewModel>>.Instance.Send(new DeleteMessage<ReplyViewModel>(this));
        }

        //private void Details()
        //{
        //    rw.DataContext = this;

        //    rw.Show();
        //}
    }
}
