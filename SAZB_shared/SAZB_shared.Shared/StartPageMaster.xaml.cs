using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SAZB_shared.Shared;

namespace SAZB_shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPageMaster : ContentPage
    {
        public ListView ListView;

        public StartPageMaster()
        {
            InitializeComponent();
            BindingContext = new StartPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        public StartPageMaster(MapViewModel mapViewModel)
        {
            InitializeComponent();
            BindingContext = mapViewModel;
            ListView = MenuItemsListView;
        }

        class StartPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<StartPageMasterMenuItem> MenuItems { get; set; }

            public StartPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<StartPageMasterMenuItem>(new[]
           {
                new StartPageMasterMenuItem { Id = 0, Title = "Карта", TargetType = typeof(MapPage)},
                //new StartPageMasterMenuItem { Id = 1, Title = "Знайти", TargetType = typeof(SearchPage)},
                //new StartPageMasterMenuItem { Id = 2, Title = "Дашборд", TargetType = typeof(StartPageDetail)},
                //new StartPageMasterMenuItem { Id = 3, Title = "Page 4" },
                //new StartPageMasterMenuItem { Id = 4, Title = "Page 5" },
            });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}