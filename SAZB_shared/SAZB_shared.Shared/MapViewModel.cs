using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Security;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks;
using Esri.ArcGISRuntime.UI;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.IO;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using System.Globalization;


#if WINDOWS_UWP
using SAZB_shared.UWP;
#endif

#if __ANDROID__
using SAZB_shared.Droid;
#endif

namespace SAZB_shared.Shared
{
    /// <summary>
    /// Provides map data to an application
    /// </summary>
    public class MapViewModel : INotifyPropertyChanged
    {
        public MapViewModel()
        {
            //Initiate a menu
            //MenuInit();
            //Initiate  map
            MapInit();
            
            //Commands add
            //Map Toolbar commands
            ShowLayer = new Command(execute: ShowLayer_function);  //Show layers list popup
        }



        private Map _map = new Map(Basemap.CreateStreetsVector());

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Map Map
        {
            get { return _map; }
            set { _map = value; OnPropertyChanged(); }
        }

        async private void MapInit()
        {
            Map Map = new Map(Basemap.CreateTopographicVector());

            string mobileGeodatabaseFilePath = DependencyService.Get<GetGeoPackageePathService>().GetGeoPackageePath();

            GeoPackage mobileGeodatabase = await GeoPackage.OpenAsync(Path.Combine(mobileGeodatabaseFilePath, "offline_map.gpkg"));

            foreach (GeoPackageFeatureTable oneGeoPackageFeatureTable in mobileGeodatabase.GeoPackageFeatureTables)
            {
                // Create a FeatureLayer from the GeoPackageFeatureLayer.
                FeatureLayer myFeatureLayer = new FeatureLayer(oneGeoPackageFeatureTable);

                // Add the layer to the map.
                Map.OperationalLayers.Add(myFeatureLayer);
            }

            await Map.OperationalLayers[0].LoadAsync();
            Map.InitialViewpoint = new Viewpoint(Map.OperationalLayers[0].FullExtent);

           
            this.Map = Map;

        }


        /// <summary>
        /// Raises the <see cref="MapViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;


        // Manu items initiation and bind objects
        //private ObservableCollection<StartPageMasterMenuItem> _menuItems = new ObservableCollection<StartPageMasterMenuItem>();
        //public ObservableCollection<StartPageMasterMenuItem> MenuItems
        //{
        //    get { return _menuItems; }
        //    set
        //    {
        //        _menuItems = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private void MenuInit()
        //{
        //    MenuItems = new ObservableCollection<StartPageMasterMenuItem>(new[]
        //    {
        //        new StartPageMasterMenuItem { Id = 0, Title = "Карта", TargetType = typeof(MapPage)},
        //        new StartPageMasterMenuItem { Id = 1, Title = "Фільтр", TargetType = typeof(StartPageDetail)},
        //        new StartPageMasterMenuItem { Id = 2, Title = "Дашборд", TargetType = typeof(StartPageDetail)},
        //        //new StartPageMasterMenuItem { Id = 3, Title = "Page 4" },
        //        //new StartPageMasterMenuItem { Id = 4, Title = "Page 5" },
        //    });
        //}


        //Interface to load geodatabase depends on platform
        public interface IGeoPackageePath
        {
            string GetGeoPackageePath();
        }

        //Interface commands
        //MapPage toolbar commands and definitions

        public ICommand ShowLayer { private set; get; }

        private void ShowLayer_function()
        {
            PopupNavigation.Instance.PushAsync(new LayersPopup());
        }


        
    }
}
