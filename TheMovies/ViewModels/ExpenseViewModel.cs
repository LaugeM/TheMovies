using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TheMovies.Models;

namespace TheMovies.ViewModels
{
    public class ExpenseViewModel : SuperClassViewModel
    {
        private Expense expense;

        private DateTime _date = DateTime.UtcNow;
        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date == value) return;
                _date = value;
                OnPropertyChanged();
            }
        }

        private double _expAmount;
        public double ExpAmount
        {
            get => _expAmount;
            set
            {
                if (_expAmount == value) return;
                _expAmount = value;
                OnPropertyChanged();
            }
        }

        private string _note = string.Empty;
        public string Note
        {
            get => _note;
            set
            {
                if (_note == value) return;
                _note = value;
                OnPropertyChanged();
            }
        }

        private static readonly Brush ValidBrush = Brushes.White;
        private static readonly Brush InvalidBrush = Brushes.MistyRose;

        private Brush _dateBackground = ValidBrush;
        public Brush DateBackground
        {
            get => _dateBackground;
            set
            {
                if (_dateBackground == value) return;
                _dateBackground = value;
                OnPropertyChanged();
            }
        }

        private Brush _expAmountBackground = ValidBrush;
        public Brush ExpAmountBackground
        {
            get => _expAmountBackground;
            set
            {
                if (_expAmountBackground == value) return;
                _expAmountBackground = value;
                OnPropertyChanged();
            }
        }

        private Brush _noteBackground = ValidBrush;
        public Brush NoteBackground
        {
            get => _noteBackground;
            set
            {
                if (_noteBackground == value) return;
                _noteBackground = value;
                OnPropertyChanged();
            }
        }

        public ExpenseViewModel(Expense expense)
        {
            this.expense = expense;
            Date = expense.Date;
            ExpAmount = expense.ExpAmount;
            Note = expense.Note;
        }

        private bool ValidateDateTime()
        {
            bool dateTimeValid = true;
            if (Date == null)
            {
                DateBackground = InvalidBrush;
                dateTimeValid = false;
            }
            else
            {
                DateBackground = ValidBrush;
            }
            return dateTimeValid;
        }

        private bool ValidateExpAmount()
        {
            bool expAmountValid = true;
            if (ExpAmount == 0)
            {
                ExpAmountBackground = InvalidBrush;
                expAmountValid = false;
            }
            else
            {
                ExpAmountBackground = ValidBrush;
            }
            return expAmountValid;
        }

        private bool ValidateNote()
        {
            bool NoteValid = true;
            if (string.IsNullOrWhiteSpace(Note))
            {
                NoteBackground = InvalidBrush;
                NoteValid = false;
            }
            else
            {
                NoteBackground = ValidBrush;
            }
            return NoteValid;
        }

        public bool ValidateUserInput()
        {
            bool dateCorrect = ValidateDateTime();
            bool expAmountCorrect = ValidateExpAmount();
            bool noteCorrect = ValidateNote();

            return dateCorrect && expAmountCorrect && noteCorrect;
        }

        //Used to add our expense to the repository
        public void AddExpenseToRepo(ValuableRepository valuableRepo)
        {
            valuableRepo.AddExpense(Date, ExpAmount, Note, expense.ID);
        }

        public void DeleteExpense(ValuableRepository valuableRepo)
        {
            valuableRepo.Remove(expense.ID);
        }

        public bool CheckExpenseInRepo(ValuableRepository valuableRepo)
        {
            return valuableRepo.CheckValuableExistence(expense.ID);
        }

        public void UpdateExpenseInRepo(ValuableRepository valuableRepo)
        {
            valuableRepo.UpdateExpense(expense.ID, Date, ExpAmount, Note);
        }
    }
}
