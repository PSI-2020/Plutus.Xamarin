using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Plutus
{


    public class FileManager
    {
        private readonly string _directoryPath;
        private readonly string _income;
        private readonly string _expenses;
        private readonly string _monthlyIncome;
        private readonly string _monthlyExpenses;
        private readonly string _goals;
        private readonly string _budgets;
        private readonly string _carts;
        public readonly string fontPathMaconodo;
        public readonly string fontPathLilita;
        
        public FileManager(string home)
        {
            _directoryPath = home;
            _income = _directoryPath + "income.xml";
            _expenses = _directoryPath + "expenses.xml";
            _monthlyIncome = _directoryPath + "monthlyIncome.xml";
            _monthlyExpenses = _directoryPath + "monthylExpenses.xml";
            _goals = _directoryPath + "goals.xml";
            _budgets = _directoryPath + "budgets.xml";
            _carts = _directoryPath + "carts.xml";
           // fontPathMaconodo = _directoryPath + "/True GUI/GUI resources/Macondo.ttf";
           // fontPathLilita = _directoryPath + "/True GUI/GUI resources/LilitaOne.ttf";
        }
        public string GetFilePath(string type)
        {
            return type switch
            {
                "Income" => _income,
                "Expense" => _expenses,
                "MonthlyIncome" => _monthlyIncome,
                "MonthlyExpenses" => _monthlyExpenses,
                _ => null,
            };
        }

        public List<Payment> ReadPayments(string type)
        {
            var serializer = new XmlSerializer(typeof(List<Payment>));

            try
            {
                using (var stream = File.OpenRead(GetFilePath(type)))
                {
                    return serializer.Deserialize(stream) as List<Payment>;
                }
            }
            catch
            {
                return new List<Payment>();
            }
        }
        public void EditPayment(Payment payment, Payment newPayment, string type)
        {
            var serializer = new XmlSerializer(typeof(List<Payment>));
            var list = ReadPayments(type);
            list[list.IndexOf(payment)] = newPayment;
            type = GetFilePath(type);

            File.WriteAllText(type, "");
            using (var stream = File.OpenWrite(type))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void AddPayment(Payment payment, string type)
        {
            var serializer = new XmlSerializer(typeof(List<Payment>));
            var list = ReadPayments(type);
            CheckDuplicates(list, payment);
            type = GetFilePath(type);

            list.Add(payment);
            using (var stream = File.OpenWrite(type))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void CheckDuplicates(List<Payment> list, Payment payment)
        {
            var duplicates = list.Where(x => payment.Equals(x));
            if(duplicates.Any()) Debug.Print("Found " + duplicates.Count() + " duplicate payments.");
        }

        public List<Goal> ReadGoals()
        {
            var serializer = new XmlSerializer(typeof(List<Goal>));

            try
            {
                using (var stream = File.OpenRead(_goals))
                {
                    return serializer.Deserialize(stream) as List<Goal>;
                }
            }
            catch
            {
                return new List<Goal>();
            }

        }

        public void AddGoal(string name, string amount, DateTime dueDate)
        {
            var serializer = new XmlSerializer(typeof(List<Goal>));
            var list = ReadGoals();

            var newAmount = double.Parse(amount);
            var goal = new Goal(name, newAmount, dueDate);

            list.Add(goal);
            using (var stream = File.OpenWrite(_goals))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void UpdateGoals(List<Goal> list)
        {
            var serializer = new XmlSerializer(typeof(List<Goal>));
            File.WriteAllText(_goals, "");
            using (var stream = File.OpenWrite(_goals))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void AddBudget(Budget budget)
        {
            var serializer = new XmlSerializer(typeof(List<Budget>));
            var list = LoadBudget();

            list.Add(budget);
            using (var stream = File.OpenWrite(_budgets))
            {
                serializer.Serialize(stream, list);
            }

        }

        public List<Budget> LoadBudget()
        {
            var serializer = new XmlSerializer(typeof(List<Budget>));

            try
            {
                using (var stream = File.OpenRead(_budgets))
                {
                    return serializer.Deserialize(stream) as List<Budget>;
                }
            }
            catch
            {
                return new List<Budget>();
            }
        }

        public void UpdateBudgets(List<Budget> list)
        {
            var serializer = new XmlSerializer(typeof(List<Budget>));
            File.WriteAllText(_budgets, "");
            using (var stream = File.OpenWrite(_budgets))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void AddScheduledPayment(ScheduledPayment payment, string type)
        {
            var serializer = new XmlSerializer(typeof(List<ScheduledPayment>));
            var list = LoadScheduledPayments(type);

            list.Add(payment);
            using (var stream = File.OpenWrite(GetFilePath(type)))
            {
                serializer.Serialize(stream, list);
            }

        }

        public List<ScheduledPayment> LoadScheduledPayments(string type)
        {
            var serializer = new XmlSerializer(typeof(List<ScheduledPayment>));

            try
            {
                using (var stream = File.OpenRead(GetFilePath(type)))
                {
                    return serializer.Deserialize(stream) as List<ScheduledPayment>;
                }
            }
            catch
            {
                return new List<ScheduledPayment>();
            }
        }

        public void UpdateScheduledPayments(List<ScheduledPayment> list, string type)
        {
            var serializer = new XmlSerializer(typeof(List<ScheduledPayment>));
            File.WriteAllText(GetFilePath(type), "");
            using (var stream = File.OpenWrite(GetFilePath(type)))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void SaveCarts(XElement carts) => carts.Save(_carts);

        public XElement LoadCarts()
        {
            if (!File.Exists(_carts)) return null;
            try
            {
                var carts = XElement.Load(_carts);
                return carts;
            }
            catch
            {
                return new XElement("Corrupted", "true");
            }
        }
    }
}
