using System;
using System.Collections.Generic;
using System.Linq;

namespace Plutus
{
    public enum ExpenseCategories
    {
        Groceries,
        Restaurant,
        Clothes,
        Transport,
        Health,
        School,
        Bills,
        Entertainment,
        Other
    }

    public enum IncomeCategories
    {
        Salary,
        Gift,
        Investment,
        Sale,
        Rent,
        Other
    }

    public class StatisticsService
    {
        private readonly FileManager _fileManager;
        public StatisticsService(FileManager fm) => _fileManager = fm;
        public string GenerateExpenseStatistics()
        {
            var manager = _fileManager;
            var list = manager.ReadPayments("Expense");
            if (!list.Any()) return "No expense data found!";

            var data = "Expense statistics:\r\n\r\n";
            var total = list.Sum(x => x.Amount);
            var sums = new Dictionary<string, double>();

            foreach (var category in Enum.GetNames(typeof(ExpenseCategories)))
            {
                sums.Add(category, list.Where(x => x.Category == category).Sum(x => x.Amount));
            }

            foreach (var category in Enum.GetNames(typeof(ExpenseCategories)))
            {
                var percent = total == 0
                    ? " (" + string.Format("{0:0.00}", 0) + "%)"
                    : " (" + string.Format("{0:0.00}", sums[category] / total * 100) + "%)";
                data += category + " " + string.Format("{0:0.00}", sums[category]) + percent + "\r\n";
            }
            return data;
        }

        public string GenerateIncomeStatistics()
        {
            var manager = _fileManager;
            var list = manager.ReadPayments("Income");
            if (!list.Any()) return "No income data found!";

            var data = "Income statistics: \r\n\r\n";
            var total = list.Sum(x => x.Amount);
            var sums = new Dictionary<string, double>();

            foreach (var category in Enum.GetNames(typeof(IncomeCategories)))
            {
                sums.Add(category, list.Where(x => x.Category == category).Sum(x => x.Amount));
            }

            foreach (var category in Enum.GetNames(typeof(IncomeCategories)))
            {
                var percent = total == 0
                    ? " (" + string.Format("{0:0.00}", 0) + "%)"
                    : " (" + string.Format("{0:0.00}", sums[category] / total * 100) + "%)";
                data += category + " " + string.Format("{0:0.00}", sums[category]) + percent + "\r\n";
            }
            return data;
        }
    }
}
