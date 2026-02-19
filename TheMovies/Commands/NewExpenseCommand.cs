using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TheMovies.ViewModels;

namespace TheMovies.Commands
{
    public class NewExpenseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            bool result = true;

            if (parameter is RegExpenseViewModel evm)
            {
                if (evm.SelectedExpense != null && evm.CheckSelectedExpenseInRepo() == false)
                {
                    result = false;
                }
            }

            return result;
        }

        public void Execute(object parameter)
        {
            if (parameter is RegExpenseViewModel evm)
            {
                evm.CreateExpense();
            }
            else
            {
                throw new ArgumentException("Illegal parameter type");
            }
        }
    }
}
