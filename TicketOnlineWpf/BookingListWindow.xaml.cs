using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TicketOnlineWpf.ViewModels;

namespace TicketOnlineWpf
{
    /// <summary>
    /// Logique d'interaction pour BookingListWindow.xaml
    /// </summary>
    public partial class BookingListWindow : Window
    {
        public BookingListWindow(int id)
        {
            DataContext = new BookingListViewModel(id);   
            InitializeComponent();
        }
    }
}
