using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheMovies.ViewModels;

namespace TheMovies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel mvm = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = mvm;
        }

        private void RegExpense_Button_Click(object sender, RoutedEventArgs e)
        {
            var MovieWindow = new Views.MovieWindow();
            {
                MovieWindow.Owner = this;
                MovieWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            ;
            bool? result = MovieWindow.ShowDialog();

            if (result == true)
            {
            }

        }

        private void RegIncome_Button_Click(object sender, RoutedEventArgs e)
        {
            var regIncomeWindow = new Views.RegIncomeWindow();
            {
                regIncomeWindow.Owner = this;
                regIncomeWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            ;
            bool? result = regIncomeWindow.ShowDialog();

            if (result == true)
            {
            }
        }
    }
}