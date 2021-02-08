using Newtonsoft.Json;
using Plutus.WebService;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Plutus
{
    public enum DataType
    {
        [Description("Storage/income.xml")]
        Income,
        [Description("Storage/expenses.xml")]
        Expense,
        [Description("Storage/monthlyIncome.xml")]
        MonthlyIncome,
        [Description("Storage/monthylExpenses.xml")]
        MonthlyExpenses,
        [Description("Storage/goals.xml")]
        Goals,
        [Description("Storage/budgets.xml")]
        Budgets,
        [Description("Storage/carts.xml")]
        Carts
    }

    public class PlutusApiClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _path = "https://aspnet-ybkkj2yjkwqhk.azurewebsites.net";

        public async Task DeleteGoalAsync(int id) => await _httpClient.DeleteAsync(_path + "/api/Goals/" + id);
        public async Task DeleteBudgetAsync(int index) => await _httpClient.DeleteAsync(_path + "/api/Budgets/" + index);
        public async Task DeleteScheduledPaymentAsync(int index, string type) => await _httpClient.DeleteAsync(_path + "/api/Scheduler/" + index + "/" + type);

        public async Task PostPaymentAsync(Payment payment, string type)
        {
            var json = JsonConvert.SerializeObject(payment);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            await _httpClient.PostAsync(_path + "/api/Payment/" + type, httpContent);
        }
        public async Task EditPaymentAsync(Payment payment, int index)
        {
            var json = JsonConvert.SerializeObject(payment);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            await _httpClient.PutAsync(_path + "/api/Payment/" + index, httpContent);
        }

        public async Task DeletePaymentAsync(int id)
        {
            await _httpClient.DeleteAsync(_path + "/api/Payment/" + id);
        }

        public async Task<List<HistoryElement>> GetHistoryAsync(int index, int page, int perPage, Filters filter)
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/History/" + index + "/" + page + "/" + perPage + "/" + filter.Used + "/" + filter.NameFiter + "/" + filter.NameFiterString + "/" + filter.ExpFlag + "/" + filter.IncFlag + "/" + filter.AmountFilter + "/" + filter.AmountFrom + "/" + filter.AmountTo + "/" + filter.DateFilter + "/" + filter.DateFrom + "/" + filter.DateTo);
            var history = JsonConvert.DeserializeObject<List<HistoryElement>>(response);
            return history;
        }

        public async Task<int> GetPageCount(int index, int perPage, Filters filter)
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/History/" + index + "/" + perPage + "/" + filter.Used + "/" + filter.NameFiter + "/" + filter.NameFiterString + "/" + filter.ExpFlag + "/" + filter.IncFlag + "/" + filter.AmountFilter + "/" + filter.AmountFrom + "/" + filter.AmountTo + "/" + filter.DateFilter + "/" + filter.DateFrom + "/" + filter.DateTo);
            var pageCount = JsonConvert.DeserializeObject<int>(response);
            return pageCount;
        }

        public async Task<List<Payment>> GetPaymentsAsync(string type)
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/Payment/" + type);
            var payments = JsonConvert.DeserializeObject<List<Payment>>(response);
            return payments;
        }

        public async Task<decimal> GetAllCategoriesSpent(string type)
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/Statistics/" + type);
            var spent = JsonConvert.DeserializeObject<decimal>(response);
            return spent;
        }

        public async Task<decimal> GetCategorySpent(string type, string category)
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/Statistics/" + type + "/" + category);
            var spent = JsonConvert.DeserializeObject<decimal>(response);
            return spent;
        }

        public async Task ChangeScheduledPaymentAsync(ScheduledPayment payment, int index, string type)
        {
            var json = JsonConvert.SerializeObject(payment);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            await _httpClient.PutAsync(_path + "/api/Scheduler/edit/" + index + "/" + type, httpContent);
        }

        public async Task PostScheduledPaymentAsync(ScheduledPayment payment, string type)
        {
            var json = JsonConvert.SerializeObject(payment);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            await _httpClient.PostAsync(_path + "/api/Scheduler/" + type, httpContent);
        }

        public async Task ChangeScheduledPaymentStatusAsync(int index, string type, bool status)
        {
            var json = JsonConvert.SerializeObject(index);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            await _httpClient.PutAsync(_path + "/api/Scheduler/" + index + "/" + type + "/" + status, httpContent);
        }

        public async Task<double> GetBudgetSpentAsync(int id)
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/Budgets/" + id + "/spent");
            var spent = JsonConvert.DeserializeObject<double>(response);
            return spent;
        }
        public async Task<double> GetBudgetLeftToSpendAsync(int id)
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/Budgets/" + id + "/left");
            var spent = JsonConvert.DeserializeObject<double>(response);
            return spent;
        }

        public async Task PostBudgetAsync(Budget budget)
        {
            var json = JsonConvert.SerializeObject(budget);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            await _httpClient.PostAsync(_path + "/api/Budgets/", httpContent);
        }
        public async Task EditGoalAsync(int id, Goal newGoal)
        {
            var json = JsonConvert.SerializeObject(newGoal);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            await _httpClient.PutAsync(_path + "/api/Goals/edit/" + id, httpContent);
        }

        public async Task SetAsMainGoalAsync(int id)
        {
            var json = JsonConvert.SerializeObject(id);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            await _httpClient.PutAsync(_path + "/api/Goals/", httpContent);
        }

        public async Task PostGoalAsync(Goal goal)
        {
            var json = JsonConvert.SerializeObject(goal);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            await _httpClient.PostAsync(_path + "/api/Goals/", httpContent);
        }

        public async Task<List<Goal>> GetGoalsAsync()
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/Goals/");
            var goals = JsonConvert.DeserializeObject<List<Goal>>(response);
            return goals;
        }
        public async Task<string> GetGoalInsightsAsync(int index, string dailyOrMonthly)
        {
            var response = await _httpClient.GetAsync(_path + "/api/Goals/" + index + "/" + dailyOrMonthly);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
        }

        public async Task<string> GetBudgetAsync(int index)
        {
            var response = await _httpClient.GetAsync(_path + "/api/Budgets/" + index);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
        }
        public async Task<List<Budget>> GetBudgetsListAsync()
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/Budgets/list");
            var budgets = JsonConvert.DeserializeObject<List<Budget>>(response);
            return budgets;
        }
        public async Task<List<Payment>> GetBudgetStatsAsync(int index)
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/Budgets/" + index + "/stats");
            var budgets = JsonConvert.DeserializeObject<List<Payment>>(response);
            return budgets;
        }
        public async Task<string> GetScheduledPaymentAsync(int index, string type)
        {
            var response = await _httpClient.GetAsync(_path + "/api/Scheduler/" + index + "/" + type);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
        }
        public async Task<List<ScheduledPayment>> GetAllScheduledPaymentsAsync(string type)
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/Scheduler/" + type);
            var scheduledPayments = JsonConvert.DeserializeObject<List<ScheduledPayment>>(response);
            return scheduledPayments;
        }
        public async Task DeleteCartAsync(int id) => await _httpClient.DeleteAsync(_path + "/api/Carts/" + id);
        public async Task DeleteCartExpenseAsync(int id, int eId) => await _httpClient.DeleteAsync(_path + "/api/Carts/" + id + "/" + eId);
        public async Task PostCartChargeAsync(int index) => await _httpClient.PostAsync(_path + "/api/Carts/Charge/" + index, null);
        public async Task PostRenameCartAsync(int id, string name)
        {
            var json = JsonConvert.SerializeObject("Empty");
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            var resp = await _httpClient.PostAsync(_path + "/api/Carts/" + id + "/" + name, httpContent);
        }
        public async Task PostNewCartAsync(string name)
        {
            var json = JsonConvert.SerializeObject("empty body");
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            var resp = await _httpClient.PostAsync(_path + "/api/Carts/" + name, httpContent);
        }
        public async Task PostNewCartExpenseAsync(int id, CartExpense expense)
        {
            var json = JsonConvert.SerializeObject(expense);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            var resp = await _httpClient.PostAsync(_path + "/api/Carts/" + id + "/add", httpContent);
        }
        public async Task PostEditCartExpenseAsync(int id, CartExpense expense)
        {
            var json = JsonConvert.SerializeObject(expense);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            var resp = await _httpClient.PostAsync(_path + "/api/Carts/" + id + "/edit", httpContent);
        }
        public async Task PostChargeShoppingAsync(List<ShoppingExpense> bag)
        {
            var json = JsonConvert.SerializeObject(bag);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            var resp = await _httpClient.PostAsync(_path + "/api/Shopping", httpContent);
        }

        public async Task<List<CartInfo>> GetCartNamesAsync()
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/Carts/");
            var cartNames = JsonConvert.DeserializeObject<List<CartInfo>>(response);
            return cartNames;
        }
        public async Task<List<CartExpense>> GetCartExpensesAsync(int index)
        {
            var response = await _httpClient.GetStringAsync(_path + "/api/Carts/Payments/" + index);
            var cartNames = JsonConvert.DeserializeObject<List<CartExpense>>(response);
            return cartNames;
        }

    }
}
