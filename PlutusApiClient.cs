using Newtonsoft.Json;
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
        /*
        
        public async Task PostScheduledPaymentAsync(ScheduledPayment payment, string type) => await _httpClient.PostAsJsonAsync(_path + "/api/Scheduler/" + type, payment);
        public async Task ChangeScheduledPaymentStatusAsync(int index, string type, bool status) => await _httpClient.PutAsJsonAsync(_path + "/api/Scheduler/" + index + "/" + type + "/" + status, index);
        public async Task CheckPaymentsAsync() => await _httpClient.PatchAsync(_path + "/api/Scheduler/", null);
        
        */
        public async Task DeleteGoalAsync(int id) => await _httpClient.DeleteAsync(_path + "/api/Goals/" + id);
        public async Task DeleteBudgetAsync(int index) => await _httpClient.DeleteAsync(_path + "/api/Budgets/" + index);
        public async Task DeleteScheduledPaymentAsync(int index, string type) => await _httpClient.DeleteAsync(_path + "/api/Scheduler/" + index + "/" + type);

       

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

        public async Task SetAsMainGoalAsync(Goal goal)
        {
            var json = JsonConvert.SerializeObject(goal);
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
      

    }
}
