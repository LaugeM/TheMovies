using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TheMovies.Models
{
    public class ValuableRepository
    {
        private List<IValuable> valuables { get; set; } = new();

        //Returns an empty Expense
        public Expense CreateExpense()
        {
            Expense newExpense = new Expense()
            {
                ExpAmount = 0.0,
                Date = DateTime.Now
            };

            return newExpense;
        }


        //Used for updating by the Update method for updating the selected expense in the repository
        public void AddExpense(DateTime date, double expAmount, string note, int id)
        {
            if (!(date == null) &&
                expAmount > 0 &&
                !string.IsNullOrEmpty(note) &&
                id >= 0)
            {
                Expense newExpense = new Expense(id)
                {
                    Date = date,
                    ExpAmount = expAmount,
                    Note = note,
                };

                // Add to valuables list
                valuables.Add(newExpense);
            }
            else
            {
                throw (new ArgumentException("Not all arguments are valid"));
            }
        }


        //Used by the Load method
        public void AddExpense(DateTime date, double expAmount, string note)
        {
            Expense newExpense = null;

            if (!(date == null) &&
                expAmount > 0 &&
                !string.IsNullOrEmpty(note))
            {
                newExpense = new Expense()
                {
                    Date = date,
                    ExpAmount = expAmount,
                    Note = note,
                };

                // Add to valuables list
                valuables.Add(newExpense);
            }
            else
            {
                throw (new ArgumentException("Not all arguments are valid"));
            }
        }

        //Used to update an Expense in the repository
        public void UpdateExpense(int id, DateTime date, double expAmount, string note)
        {
            // Find the valuable in the internal valuables list with same ID as the 'id'-parameter
            IValuable foundValuable = this.GetValuable(id);

            if (foundValuable != null && foundValuable is Expense foundExpense)
            {
                if (date != null &&
                    expAmount >= 0 &&
                    !string.IsNullOrEmpty(note))
                {
                    // Update only changed properties for this expense
                    if (foundExpense.Date != date)
                        foundExpense.Date = date;
                    if (foundExpense.ExpAmount != expAmount)
                        foundExpense.ExpAmount = expAmount;
                    if (foundExpense.Note != note)
                        foundExpense.Note = note;
                }
                else
                    throw (new ArgumentException("Not all arguments for expense are valid"));
            }
            else
                throw (new ArgumentException("expense with id = " + id + " not found"));
        }

        //Used to update an Income in the repository
        public void UpdateIncome(int id, string initials, DateTime date, double incAmount, string service, bool paymentStatus)
        {
            // Find the valuable in the internal valuables list with same ID as the 'id'-parameter
            IValuable foundValuable = this.GetValuable(id);

            if (foundValuable != null && foundValuable is Income foundIncome)
            {
                if (date != null &&
                    incAmount >= 0 &&
                    !string.IsNullOrEmpty(service))
                {
                    // Update only changed properties for this expense
                    if (foundIncome.Initials != initials)
                        foundIncome.Initials = initials;
                    if (foundIncome.Date != date)
                        foundIncome.Date = date;
                    if (foundIncome.IncAmount != incAmount)
                        foundIncome.IncAmount = incAmount;
                    if (foundIncome.Service != service)
                        foundIncome.Service = service;
                    if (foundIncome.PaymentStatus != paymentStatus)
                        foundIncome.PaymentStatus = paymentStatus;
                }
                else
                    throw (new ArgumentException("Not all arguments for income are valid"));
            }
            else
                throw (new ArgumentException("expense with id = " + id + " not found"));
        }

        public Income CreateIncome()
        {
            Income newIncome = new Income()
            {
                IncAmount = 0.0,
                Date = DateTime.Now
            };

            return newIncome;
        }

        //Used by the Load method
        public void AddIncome(string initials, DateTime date, double incAmount, string service, bool paymentStatus)
        {
            Income newIncome = null;

            if (!string.IsNullOrEmpty(initials) &&
                !(date == null) &&
                incAmount > 0 &&
                !string.IsNullOrEmpty(service) &&
                !(paymentStatus == null))
            {
                newIncome = new Income()
                {
                    Initials = initials,
                    Date = date,
                    IncAmount = incAmount,
                    Service = service,
                    PaymentStatus = paymentStatus
                };

                // Add to valuables list
                valuables.Add(newIncome);
            }
            else
            {
                throw (new ArgumentException("Not all arguments are valid"));
            }
        }

        //Used for updating by the Update method for updating the selected income in the repository
        public void AddIncome(string initials, DateTime date, double incAmount, string service, bool paymentStatus, int id)
        {
            if (!string.IsNullOrEmpty(initials) &&
                !(date == null) &&
                incAmount > 0 &&
                !string.IsNullOrEmpty(service) &&
                !(paymentStatus == null) &&
                id >= 0)
            {
                Income newIncome = new Income(id)
                {
                    Initials = initials,
                    Date = date,
                    IncAmount = incAmount,
                    Service = service,
                    PaymentStatus = paymentStatus
                };

                // Add to valuables list
                valuables.Add(newIncome);
            }
            else
            {
                throw (new ArgumentException("Not all arguments are valid"));
            }
        }

        //Removes a valuable from the repository
        public void Remove(int id)
        {
            // Find the person in the internal persons list with same Id as the 'id'-parameter
            IValuable foundValuable = this.GetValuable(id);

            if (foundValuable != null)
                valuables.Remove(foundValuable);
            else
                throw (new ArgumentException("Valuable with id = " + id + " not found"));
        }

        //Gets a valuable from the repository
        public IValuable GetValuable(int id)
        {
            IValuable result = null;
            foreach (IValuable iv in valuables)
            {
                if (iv.ID == id)
                {
                    result = iv;
                    break;
                }
            }
            return result;
        }

        //Checks if a valuable exists in the repository
        public bool CheckValuableExistence(int id)
        {
            bool result = true;

            if (GetValuable(id) == null) { result = false; }
            return result;
        }

        public List<IValuable> GetAll()
        {
            return valuables;
        }

        // Was supposed to be used for UC#3 & UC#4
        //
        //public double GetTotalValue()
        //{
        //    double TotalValue = 0;
        //    foreach (IValuable iv in valuables)
        //    {
        //        TotalValue += iv.GetValue();
        //    }
        //    return TotalValue;
        //}

        public int Count()
        {
            return valuables.Count();
        }


        public void SaveExpenses()
        {
            save("ExpensePersistence.txt");
        }

        public void SaveIncomes()
        {
            save("IncomePersistence.txt");
        }

        private void save(string FileName)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(FileName))
                {
                    foreach (IValuable iv in valuables)
                    {
                        if (iv is Income income)
                        {
                            streamWriter.WriteLine($"INCOME;{income.Initials};{income.Date};{income.IncAmount};{income.Service};{income.PaymentStatus}");
                        }

                        if (iv is Expense expense)
                        {
                            streamWriter.WriteLine($"EXPENSE;{expense.Date};{expense.ExpAmount};{expense.Note}");
                        }
                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show($"Exeption {e.Message}");
            }

        }

        public void Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    string input = streamReader.ReadLine();
                    while (input != null)
                    {
                        string[] valuableArray = input.Split(';');
                        switch (valuableArray[0])
                        {
                            case "INCOME":
                                this.AddIncome(
                                    valuableArray[1],
                                    DateTime.Parse(valuableArray[2]),
                                    double.Parse(valuableArray[3]),
                                    valuableArray[4],
                                    bool.Parse(valuableArray[5]));
                                break;
                            case "EXPENSE":
                                this.AddExpense(
                                    DateTime.Parse(valuableArray[1]),
                                    double.Parse(valuableArray[2]),
                                    valuableArray[3]);
                                break;
                            default:
                                break;
                        }
                        input = streamReader.ReadLine();
                    }
                }
            }
        }

    }
}