using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TheMovies.ViewModels;

namespace TheMovies.Commands
{
    public class SaveIncomeCommand : ICommand
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
                if (ivm.SelectedIncome == null)
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
                if (ivm.SelectedIncome.ValidateUserInput())
                {
                    if (ivm.CheckSelectedIncomeInRepo()) //checks if the Income is currently in the repository
                    {
                        // if the Income is currently in the repository, update to the new values
                        ivm.UpdateSelectedIncome();
                        ivm.SaveIncomeToRepo();
                        MessageBox.Show("Indtægterne blev opdateret korrekt");
                    }
                    else // if the Income is not currently in the repository, add it to repository
                    {
                        ivm.AddNewIncomeToRepo();
                        ivm.SaveIncomeToRepo();
                        MessageBox.Show("Indtægterne blev gemt korrekt");
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

