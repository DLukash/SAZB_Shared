using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Xamarin.Forms;
using Microsoft.EntityFrameworkCore;
using Rg.Plugins.Popup.Services;
using SAZB_shared.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAZB_shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : Rg.Plugins.Popup.Pages.PopupPage
    {

        ObservableCollection<Landplot> resultListLandplots = new ObservableCollection<Landplot>();
        private MapViewModel general_mapViewModel;
        private MapView g_mapViev;

        public SearchPage()
        {
            InitializeComponent();
            general_mapViewModel = new MapViewModel();
        }

        public SearchPage(MapViewModel viewModel)
        {
            InitializeComponent();
            
            searchResults.ItemsSource = resultListLandplots;
            SearcCN.TextChanged += SearcCN_TextChanged;

            general_mapViewModel = viewModel;
        }


        public SearchPage(MapViewModel viewModel, MapView mapViev)
        {
            InitializeComponent();

            searchResults.ItemsSource = resultListLandplots;
            SearcCN.TextChanged += SearcCN_TextChanged;
            general_mapViewModel = viewModel;
            g_mapViev = mapViev;
        }

        async private void SearcCN_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((SearchBar)sender).Text;

            if (text.Length > 0)
            {

                //Add On more click event

                SearcOwner.IsVisible = false;
                SearchField.IsVisible = false;
                SearcLandlord.IsVisible = false;

                using (var db = new Offline_DB_Context())
                {
                    var results = await db.Landplots.Where(l => l.CadNumber.Replace(":", "").Contains(((SearchBar)sender).Text)).Take(10).ToListAsync();
                    resultListLandplots.Clear();
                    foreach (Landplot l in results)
                    {
                        resultListLandplots.Add(l);
                    }

                }
            }else
            {
                resultListLandplots.Clear();
                SearcOwner.IsVisible = true;
                SearchField.IsVisible = true;
                SearcLandlord.IsVisible = true;
            }
            
        }


        async private void On_back_cliked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAllAsync(true);
        }

        async private void On_cell_tapped(object sender, EventArgs e)
        {
            if (((TextCell)sender).CommandParameter is Landplot)
            {
                string action = await DisplayActionSheet("Оберіть дію", "Назад", null ,"Детально", "На карті");
                var landplot = ((TextCell)sender).CommandParameter as Landplot;

                switch (action)
                {
                    case "Детально":
                        {
                            using (var db = new Offline_DB_Context())
                            {
                                var rent_list = await db.Rents.Where(r => r.CadastrNumber == landplot.CadNumber.Replace(":", "")).ToListAsync();

                                if (rent_list.Count() > 0)
                                {
                                    PopupNavigation.Instance.PushAsync(new FeatureDetailsPopup(landplot.GetDataDict(), rent_list));
                                }
                                else PopupNavigation.Instance.PushAsync(new FeatureDetailsPopup(landplot.GetDataDict()));

                            }

                            break;
                        }
                    case "На карті":
                        {
                            ((FeatureLayer)general_mapViewModel.Map.OperationalLayers[1]).IsVisible = true;
                            ((FeatureLayer)general_mapViewModel.Map.OperationalLayers[1]).ClearSelection();

                            QueryParameters queryParams = new QueryParameters
                            {
                                WhereClause = String.Format("OBJECTID={0}", landplot.ObjectID)
                            };

                            ((FeatureLayer)general_mapViewModel.Map.OperationalLayers[1]).SelectFeaturesAsync(queryParams, Esri.ArcGISRuntime.Mapping.SelectionMode.New);
                            Envelope resultExtent = await ((FeatureLayer)general_mapViewModel.Map.OperationalLayers[1]).FeatureTable.QueryExtentAsync(queryParams);
                            Envelope resultExtent_1 = new Envelope(center: resultExtent.GetCenter(), width: resultExtent.Width + (resultExtent.Width * 0.5), height: resultExtent.Height + (resultExtent.Height * 0.5));

                            Viewpoint resultViewpoint = new Viewpoint(resultExtent_1);
                            g_mapViev.SetViewpointAsync(resultViewpoint);

                            PopupNavigation.Instance.PopAllAsync(true);
                            break;
                        }
                }
                    


            }
        }
    }
}