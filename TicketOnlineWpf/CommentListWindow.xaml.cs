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
    /// Logique d'interaction pour CommentListWindow.xaml
    /// </summary>
    public partial class CommentListWindow : Window
    {
        public CommentListWindow(int id)
        {
            DataContext = new CommentListViewModel(id);
            InitializeComponent();
        }
    }
}
