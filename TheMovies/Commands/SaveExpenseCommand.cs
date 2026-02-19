using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TheMovies.ViewModels;

namespace TheMovies.Commands
{
    public class SaveExpenseCommand : ICommand
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
                if (evm.SelectedExpense == null)
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
                if (evm.SelectedExpense.ValidateUserInput())
                {
                    if (evm.CheckSelectedExpenseInRepo()) //checks if the Expense is currently in the repository
                    {
                        // if the Expense is currently in the repository, update to the new values
                        evm.UpdateSelectedExpense();
                        evm.SaveExpensesToRepo();
                        MessageBox.Show("Udgifterne blev opdateret korrekt");
                    }
                    else // if the Expense is not currently in the repository, add it to repository
                    {
                        evm.AddNewExpenseToRepo();
                        evm.SaveExpensesToRepo();
                        MessageBox.Show("Udgifterne blev gemt korrekt");
                    }
                }
                else
                {
                    MessageBox.Show("Udfyld venligst alle felterne korrekt");
                }
            }
            else
            {
                throw new ArgumentException("Illegal parameter type");
            }
        }
    }
}
