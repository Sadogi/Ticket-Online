using MVVMTools.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MVVMTools.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //public event PropertyChangingEventHandler PropertyChanging;

        //protected void RaisePropertyChanging(string propertyName)
        //{
        //    PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        //}

        //protected void RaisePropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        public ViewModelBase()
        {
            Type viewModelType = GetType();
            IEnumerable<PropertyInfo> propertyInfos = viewModelType.GetProperties().Where(pi => pi.PropertyType.Equals(typeof(ICommand)) || pi.PropertyType.GetInterfaces().Contains(typeof(ICommand)));

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                ICommand command = propertyInfo.GetMethod.Invoke(this, null) as ICommand;

                if (!(command is null))
                {
                    PropertyChanged += (s, e) => command.RaiseCanExecuteChanged();
                }
            }
        }
    }
}
