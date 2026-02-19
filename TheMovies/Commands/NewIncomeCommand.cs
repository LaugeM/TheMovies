using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TheMovies.ViewModels;
using TheMovies.ViewModels;

namespace TheMovies.Commands
{
    public class NewIncomeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            bool result = true;

            if (parameter is RegIncomeViewModel ivm)
            {
                if (ivm.SelectedIncome != null && ivm.CheckSelectedIncomeInRepo() == false)
                {
                    result = false;
                }
            }
            return result;
        }

        public void Execute(object parameter)
        {
            if (parameter is RegIncomeViewModel ivm)
            {
                ivm.CreateIncome();
            }
            else
            {
                throw new ArgumentException("Illegal parameter type");
            }
        }
    }
}
