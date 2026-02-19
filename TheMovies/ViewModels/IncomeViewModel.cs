using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TheMovies.Models;

namespace TheMovies.ViewModels
{
    public class IncomeViewModel : SuperClassViewModel
    {
        private Income income;

        private string _initials = string.Empty;
        public string Initials
        {
            get => _initials;
            set
            {
                if (_initials == value) return;
                _initials = value;
                OnPropertyChanged();
            }
        }

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

        private double _incAmount;
        public double IncAmount
        {
            get => _incAmount;
            set
            {
                if (_incAmount == value) return;
                _incAmount = value;
                OnPropertyChanged();
            }
        }

        private string _service = string.Empty;
        public string Service
        {
            get => _service;
            set
            {
                if (_service == value) return;
                _service = value;
                OnPropertyChanged();
            }
        }

        private bool _paymentStatus;
        public bool PaymentStatus
        {
            get => _paymentStatus;
            set
            {
                if (_paymentStatus == value) return;
                _paymentStatus = value;
                OnPropertyChanged();
            }
        }

        private static readonly Brush ValidBrush = Brushes.White;
        private static readonly Brush InvalidBrush = Brushes.MistyRose;

        private Brush _initialsBackground = ValidBrush;
        public Brush InitialsBackground
        {
            get => _initialsBackground;
            set
            {
                if (_initialsBackground == value) return;
                _initialsBackground = value;
                OnPropertyChanged();
            }
        }

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

        private Brush _incAmountBackground = ValidBrush;
        public Brush IncAmountBackground
        {
            get => _incAmountBackground;
            set
            {
                if (_incAmountBackground == value) return;
                _incAmountBackground = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<KeyValuePair<bool, string>> PaymentStatusOptions { get; } =
        new[]
        {
                new KeyValuePair<bool, string>(true, "Betalt"),
                new KeyValuePair<bool, string>(false, "Ikke Betalt")
        };

        public IncomeViewModel(Income income)
        {
            this.income = income;
            Initials = income.Initials;
            Date = income.Date;
            IncAmount = income.IncAmount;
            Service = income.Service;
            PaymentStatus = income.PaymentStatus;

        }
        public bool ValidateUserInput()
        {
            bool initalsCorrect = ValidateInitials();
            bool dateCorrect = ValidateDateTime();
            bool incAmountCorrect = ValidateIncAmount();

            return initalsCorrect && dateCorrect && incAmountCorrect;
        }

        public bool ValidateInitials()
        {
            bool initialsValid = true;
            if (string.IsNullOrWhiteSpace(Initials))
            {
                InitialsBackground = InvalidBrush;
                initialsValid = false;
            }
            else
            {
                InitialsBackground = ValidBrush;
            }
            return initialsValid;
        }

        public bool ValidateDateTime()
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

        public bool ValidateIncAmount()
        {
            bool incAmountValid = true;
            if (IncAmount == 0)
            {
                IncAmountBackground = InvalidBrush;
                incAmountValid = false;
            }
            else
            {
                IncAmountBackground = ValidBrush;
            }
            return incAmountValid;
        }

        public void AddIncomeToRepo(ValuableRepository valuableRepo)
        {
            valuableRepo.AddIncome(Initials, Date, IncAmount, Service, PaymentStatus, income.ID);
        }


        public void DeleteIncome(ValuableRepository valuableRepo)
        {
            valuableRepo.Remove(income.ID);

        }
        public bool CheckIncomeInRepo(ValuableRepository valuableRepo)
        {
            return valuableRepo.CheckValuableExistence(income.ID);
        }

        public void UpdateIncomeInRepo(ValuableRepository valuableRepo)
        {
            valuableRepo.UpdateIncome(income.ID, Initials, Date, IncAmount, Service, PaymentStatus);
        }
    }
}


