using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAZB_shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : MasterDetailPage
    {

        private SAZB_shared.Shared.MapViewModel viewModel;
        
        public StartPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            viewModel = new Shared.MapViewModel();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as StartPageMasterMenuItem;
            
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType, new[] { viewModel });
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}