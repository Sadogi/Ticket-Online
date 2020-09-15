using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MVVMTools.ViewModels
{
    public abstract class CollectionViewModelBase<TViewModel> : ViewModelBase
        where TViewModel : ViewModelBase
    {
        private ObservableCollection<TViewModel> _items;
        private TViewModel _selectedItem;

        public ObservableCollection<TViewModel> Items
        {
            get
            {
                return _items ?? (_items = LoadItems());
            }

            set
            {
                Set(ref _items, value);
                //if (_items != value)
                //{
                //    _items = value;
                //    RaisePropertyChanged(nameof(Items));
                //}
            }
        }

        public TViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                Set(ref _selectedItem, value);
                //if (_selectedItem != value)
                //{
                //    _selectedItem = value;
                //    RaisePropertyChanged(nameof(SelectedItem));
                //}
            }
        }

        protected abstract ObservableCollection<TViewModel> LoadItems();
    }
}