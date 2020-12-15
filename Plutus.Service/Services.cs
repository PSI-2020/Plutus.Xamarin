
namespace Plutus
{
    public class Services
    {
        public FileManager FileManager { get; set; }
        public BudgetService BudgetService { get; set; }
        public CartService CartService { get; set; }
        public GoalService GoalService { get; set; }
        public HistoryService HistoryService { get; set; }
        public PaymentService PaymentService { get; set; }
        public SchedulerService SchedulerService { get; set; }
        public ShoppingService ShoppingService { get; set; }
        public StatisticsService StatisticsService { get; set; }
        public VerificationService VerificationService { get; set; }
        public CurrentInfoHolder CurrentInfoHolder { get; set; }

        public Services(string path)
        {
            FileManager = new FileManager(path);
            BudgetService = new BudgetService(FileManager);
            CartService = new CartService(FileManager);
            GoalService = new GoalService(FileManager);
            HistoryService = new HistoryService(FileManager);
            PaymentService = new PaymentService(FileManager);
            SchedulerService = new SchedulerService(FileManager);
            ShoppingService = new ShoppingService();
            StatisticsService = new StatisticsService(FileManager);
            VerificationService = new VerificationService();
            CurrentInfoHolder = new CurrentInfoHolder();
        }
    }
}
