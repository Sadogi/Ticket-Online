using MVVMTools.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnlineWpf.ViewModels
{
    public class MainViewModel
    {
        private ICommand _eventListCommand, _userListCommand, _commentListByUserCommand, _commentListCommand, _bookingListByUserCommand, _bookingListCommand;

        public ICommand EventListCommand
        {
            get
            {
                return _eventListCommand ?? (_eventListCommand = new RelayCommand(EventList));
            }
        }

        public ICommand UserListCommand
        {
            get
            {
                return _userListCommand ?? (_userListCommand = new RelayCommand(UserList));
            }
        }

        public ICommand CommentListByUserCommand
        {
            get
            {
                return _commentListByUserCommand ?? (_commentListByUserCommand = new RelayCommand(CommentListByUser));
            }
        }

        //public ICommand CommentListCommand
        //{
        //    get
        //    {
        //        return _commentListCommand ?? (_commentListCommand = new RelayCommand(CommentList));
        //    }
        //}

        public ICommand BookingListByUserCommand
        {
            get
            {
                return _bookingListByUserCommand ?? (_bookingListByUserCommand = new RelayCommand(BookingListByUser));
            }
        }

        //public ICommand BookingListCommand
        //{
        //    get
        //    {
        //        return _bookingListCommand ?? (_bookingListCommand = new RelayCommand(BookingList));
        //    }
        //}

        private void EventList()
        {
            EventListWindow elw = new EventListWindow();
            elw.Show();
        }

        private void UserList()
        {
            UserListWindow ulw = new UserListWindow();
            ulw.ShowDialog();
        }

        private void CommentListByUser()
        {
            CommentListByUserWindow clbuw = new CommentListByUserWindow();
            clbuw.Show();
        }

        //private void CommentList()
        //{
        //    CommentListWindow clw = new CommentListWindow();
        //    clw.Show();
        //}

        private void BookingListByUser()
        {
            BookingListByUserWindow blbuw = new BookingListByUserWindow();
            blbuw.Show();
        }

        //private void BookingList()
        //{
        //    BookingListWindow blw = new BookingListWindow();
        //    blw.Show();
        //}       
    }
}
