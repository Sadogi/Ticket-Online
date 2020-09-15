using System;
using System.Collections.Generic;
using System.Text;

namespace PatternTools.Mediator.Messages
{
    public class DeleteMessage<T>
    {
        public T ViewModel { get; private set; }

        public DeleteMessage(T viewModel)
        {
            if((ViewModel = viewModel) != null)
                ViewModel = viewModel;

            else
                throw new ArgumentNullException(nameof(viewModel));

            //try
            //{
            //    ViewModel = viewModel;
            //}

            //catch 
            //{ 
            //    throw new ArgumentNullException(nameof(viewModel)); 
            //}
        }
    }
}
