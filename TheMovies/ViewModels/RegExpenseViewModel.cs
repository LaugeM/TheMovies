using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheMovies.Commands;
using TheMovies.Models;

namespace TheMovies.ViewModels
{
    public class RegExpenseViewModel : SuperClassViewModel
    {
        private ValuableRepository expenseRepo;
        public ObservableCollection<ExpenseViewModel> ExpenseVM { get; set; }

        public RegExpenseViewModel()
        {
            expenseRepo = new ValuableRepository();
            expenseRepo.Load("ExpensePersistence.txt");
            ExpenseVM = new ObservableCollection<ExpenseViewModel>();
            InitializeExpenseVM();
        }

        public void InitializeExpenseVM()
        {
            try
            {
                foreach (IValuable iv in expenseRepo.GetAll())
                {
                    if (iv is Expense expense)
                    {
                        ExpenseViewModel EVM = new(expense);
                        ExpenseVM.Add(EVM);
                    }
                }
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommand NewExpenseCommand { get; } = new NewExpenseCommand();
        public ICommand SaveExpenseCommand { get; } = new SaveExpenseCommand();
        public ICommand DeleteExpenseCommand { get; } = new DeleteExpenseCommand();


        private ExpenseViewModel _selectedExpense;
        public ExpenseViewModel SelectedExpense
        {
            get
            {
                return _selectedExpense;
            }
            set
            {
                if (_selectedExpense == value) return;
                _selectedExpense = value;
                OnPropertyChanged();
            }
        }


        public void CreateExpense()
        {
            Expense expense = expenseRepo.CreateExpense();
            ExpenseViewModel EVM = new(expense);
            ExpenseVM.Add(EVM);
            SelectedExpense = ExpenseVM.Last();
        }

        public void AddNewExpenseToRepo()
        {
            SelectedExpense.AddExpenseToRepo(expenseRepo);
        }

        public void SaveExpensesToRepo()
        {
            expenseRepo.SaveExpenses();
        }

        public bool CheckSelectedExpenseInRepo()
        {
            return SelectedExpense.CheckExpenseInRepo(expenseRepo);
        }

        public void DeleteSelectedExpense()
        {
            if (SelectedExpense == null) return;
            SelectedExpense.DeleteExpense(expenseRepo);
            ExpenseVM.Remove(SelectedExpense);
            SelectedExpense = ExpenseVM.FirstOrDefault();
            expenseRepo.SaveExpenses();
        }

        public void UpdateSelectedExpense()
        {
            SelectedExpense.UpdateExpenseInRepo(expenseRepo);
        }
    }
}
