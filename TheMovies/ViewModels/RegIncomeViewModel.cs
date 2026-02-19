using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TheMovies.Commands;
using TheMovies.Models;

namespace TheMovies.ViewModels
{
    public class RegIncomeViewModel : SuperClassViewModel
    {
        private ValuableRepository incomeRepo;
        public ObservableCollection<IncomeViewModel> IncomeVM { get; set; }

        public IEnumerable<KeyValuePair<bool, string>> PaymentStatusOptions { get; } =
        new[]
        {
                new KeyValuePair<bool, string>(true, "Betalt"),
                new KeyValuePair<bool, string>(false, "Ikke Betalt")
        };

        public RegIncomeViewModel()
        {
            incomeRepo = new ValuableRepository();
            incomeRepo.Load("IncomePersistence.txt");
            IncomeVM = new ObservableCollection<IncomeViewModel>();
            InitializeIncomeVM();
        }
        public void InitializeIncomeVM()
        {
            try
            {
                foreach (IValuable iv in incomeRepo.GetAll())
                {
                    if (iv is Income income)
                    {
                        IncomeViewModel IVM = new(income);
                        IncomeVM.Add(IVM);
                    }
                }
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }
        }
        private IncomeViewModel _selectedIncome;
        public IncomeViewModel SelectedIncome
        {
            get
            {
                return _selectedIncome;
            }
            set
            {
                if (_selectedIncome == value) return;
                _selectedIncome = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewIncomeCommand { get; } = new NewIncomeCommand();
        public ICommand DeleteIncomeCommand { get; } = new DeleteIncomeCommand();
        public ICommand SaveIncomeCommand { get; } = new SaveIncomeCommand();

        //used by the NewIncomeCommand to create a new temporary income
        public void CreateIncome()
        {
            Income income = incomeRepo.CreateIncome();
            IncomeViewModel ivm = new(income);
            IncomeVM.Add(ivm);
            SelectedIncome = IncomeVM.Last();
        }

        //Used when the user clicks the save button but the income's ID is not found in the repository
        public void AddNewIncomeToRepo()
        {
            SelectedIncome.AddIncomeToRepo(incomeRepo);
        }

        //Used when the user clicks the save button and the income's ID is found in the repository
        public void UpdateSelectedIncome()
        {
            SelectedIncome.UpdateIncomeInRepo(incomeRepo);
        }

        //Used by by the save button after an income has either been updated or created
        public void SaveIncomeToRepo()
        {
            incomeRepo.SaveIncomes();
        }

        //used when the user clicks the save button. Checks if the selected income's id is found in the repository
        public bool CheckSelectedIncomeInRepo()
        {
            return SelectedIncome.CheckIncomeInRepo(incomeRepo);
        }

        //Used when the user clicks the delete button.
        public void DeleteSelectedIncome()
        {
            if (SelectedIncome == null) return;
            SelectedIncome.DeleteIncome(incomeRepo);
            IncomeVM.Remove(SelectedIncome);
            SelectedIncome = IncomeVM.FirstOrDefault();
            incomeRepo.SaveIncomes();
        }
    }
}
