using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkerClient.ViewModel
{
    class RelayCommand : ICommand
    {
        private Action toExecute;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action actionToExecute)
        {
            toExecute = actionToExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (toExecute != null)
                toExecute();
        }
    }
}
