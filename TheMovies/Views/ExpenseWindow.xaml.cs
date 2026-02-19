using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TheMovies.ViewModels;

namespace TheMovies.Views
{
    /// <summary>
    /// Interaction logic for ExpenseWindow.xaml
    /// </summary>
    public partial class ExpenseWindow : Window
    {
        RegExpenseViewModel evm = new RegExpenseViewModel();
        public ExpenseWindow()
        {
            InitializeComponent();
            DataContext = evm;
        }
    }
}
