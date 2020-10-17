using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAZB_shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeatureDetailsPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public FeatureDetailsPopup()
        {
            InitializeComponent();
        }

        public FeatureDetailsPopup(Dictionary<string, string> feature)
        {
            InitializeComponent();
            LayersView.ItemsSource = feature;
        }

        public FeatureDetailsPopup(Dictionary<string, string> feature, List<Rent> rent_list)
        {
            InitializeComponent();
            LayersView.ItemsSource = feature;

            Button button = new Button
            {
                Text = String.Format("Знайдено {0} записів про оренду", rent_list.Count)

            };

            button.Clicked += async (s,e) => 
            {
                var landlords = (from x in rent_list select x.Landlord).ToList();
                var answer_landlords = await DisplayActionSheet(title: "Оберіть поле:", cancel: "Відмінити", destruction: "Ок", buttons: landlords.ToArray());

                if (landlords.Contains(answer_landlords))
                {
                    PopupNavigation.Instance.PushAsync(new FeatureDetailsPopup(rent_list[landlords.IndexOf(answer_landlords)].GetDataDict()));
                }

            };

            stackLayout.Children.Insert(1, button);

        }

        private void Ok_button_Clicked(object sender, EventArgs e)
        {

            PopupNavigation.Instance.PopAsync(true);
        }
    }
}