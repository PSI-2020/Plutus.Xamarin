using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plutus.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditHistoryPage : ContentPage
    {
        private readonly PlutusApiClient _plutusApiClient;
        private readonly int _previousPageIndex;

        public EditHistoryPage(PlutusApiClient plutusApi, int index)
        {
            _plutusApiClient = plutusApi;
            _previousPageIndex = index;
            InitializeComponent();
            
        }
        protected override void OnAppearing()
        {
            dataPicker.SelectedIndex = _previousPageIndex;
            base.OnAppearing();
        }

        private void SelectedItemChanged(object sender, EventArgs e)
        {
            var index = dataPicker.SelectedIndex;
            LoadDetailsAsync(index);
        }

        private async void LoadDetailsAsync(int index)
        {
            data.Children.Clear();
            data.RowDefinitions.Clear();
            _ = scroll.ScrollToAsync(data, ScrollToPosition.Start, false);

            var list = await _plutusApiClient.GetHistoryAsync(index);
            if (list != null)
            {
                var i = 0;

                foreach (var payment in list)
                {
                    var currentPayment = new Payment
                    {
                        Date = payment.Date.ConvertToInt(),
                        Name = payment.Name,
                        Amount = payment.Amount,
                        Category = payment.Category
                    };

                    var type = payment.Type;
                    var currentType = DataType.Income;

                    if (type == "Exp.")
                    {
                       currentType = DataType.Expense;
                    }

                    var deleteButton = new Button
                    {
                        Text = "-",
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.White,
                        BackgroundColor = Color.FromHex("#864F48"),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        Padding = 0,
                        WidthRequest = 20,
                        HeightRequest = 15,
                        CornerRadius = 50,
                        Margin = new Thickness(5, 2, 2, 0)
                    };
                    deleteButton.Clicked += async (s, e) =>
                    {
                       
                        await _plutusApiClient.DeletePaymentAsync(currentPayment,currentType);
                        LoadDetailsAsync(index);

                    };
                    var editButton = new Button
                    {
                        Text = "edit",
                        TextTransform = TextTransform.Lowercase,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Color.Transparent,
                        TextColor = Color.White,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        Padding = 0,
                        FontSize = 8,
                        WidthRequest = 60,
                        HeightRequest = 20

                    };
                    editButton.Clicked += (s, e) =>
                    {
                         var editScheduledPaymentPage = new EditPaymentPage(currentPayment, currentType, _plutusApiClient);
                         NavigationPage.SetHasNavigationBar(editScheduledPaymentPage, false);
                         Navigation.PushAsync(editScheduledPaymentPage);
                    };

                    var delete = GetStack(i);
                    delete.Children.Add(deleteButton);
                    var edit = GetStack(i);
                    edit.Children.Add(editButton);
                    data.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
                    data.Children.Add(delete, 0, i);
                    data.Children.Add(PaymentLabel(payment.Date.ToString("yyyy-MM-dd"), i), 1, i);
                    data.Children.Add(PaymentLabel(payment.Name, i), 2, i);
                    data.Children.Add(PaymentLabel(payment.Amount.ToString("C2"), i), 3, i);
                    data.Children.Add(PaymentLabel(payment.Category, i), 4, i);
                    data.Children.Add(PaymentLabel(payment.Type, i), 5, i);
                    data.Children.Add(edit, 6, i);

                    i++;
                }

                data.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1) });
                data.Children.Add(new BoxView() { BackgroundColor = Color.FromHex("8D8B86") }, 0, i);
            }
        }

        private StackLayout GetStack(int index)
        {
            var stack = new StackLayout();
            if (index % 2 == 0)
            {
                stack.BackgroundColor = Color.FromHex("DCD5C9");
            }
            else
            {
                stack.BackgroundColor = Color.FromHex("CDC7BC");
            }

            return stack;
        }

        private Label PaymentLabel(string info, int index)
        {
            var label = new Label()
            {
                FontSize = 12,
                Text = info,
                TextColor = Color.FromHex("8E897E"),
                FontFamily = "LilitaOne",
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center

            };
            if (index % 2 == 0)
            {
                label.BackgroundColor = Color.FromHex("DCD5C9");
            }
            else
            {
                label.BackgroundColor = Color.FromHex("CDC7BC");
            }

            return label;
        }

        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            var page = new MainPage(_plutusApiClient);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);

        }
        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        


    }
}