using SAZB_shared.Shared;
using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Esri.ArcGISRuntime.Data;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
//using Android.OS;

namespace SAZB_shared
{
    public partial class MapPage : ContentPage
    {

        public MapPage()
        {
            InitializeComponent();
            BindingContext = new MapViewModel();

        }

        public MapPage(MapViewModel mapViewModel)
        {
            InitializeComponent();
            BindingContext = mapViewModel;

            ShowLayersTbItem.Clicked += (s, e) =>
            {
                PopupNavigation.Instance.PushAsync(new LayersPopup(mapViewModel));
            };

            ShowSearchForm.Clicked += (s, e) =>
            {
                PopupNavigation.Instance.PushAsync(new SearchPage(mapViewModel, mapViev));
                
            };

            mapViev.GeoViewTapped += MapViev_GeoViewTapped;



        }

        async private void MapViev_GeoViewTapped(object sender, Esri.ArcGISRuntime.Xamarin.Forms.GeoViewInputEventArgs e)
        {
            IReadOnlyList<IdentifyLayerResult> identifyResults = await mapViev.IdentifyLayersAsync(e.Position, 5, false, 20);

            if (identifyResults.Count > 0)
            {
                List<string> layer_list = new List<string>();

                foreach (var x in identifyResults)
                {
                    switch (x.LayerContent.Name)
                    {
                        case "Fields": layer_list.Add(String.Format("Поля - ({1} шт.)", x.LayerContent.Name, x.GeoElements.Count)); break;
                        case "Landplots": layer_list.Add(String.Format("Ділянки - ({1} шт.)", x.LayerContent.Name, x.GeoElements.Count)); break;
                    }
                }

                var answer = await DisplayActionSheet(title: "Оберіть слой:", cancel: "Відмінити", destruction: "Ок", buttons: layer_list.ToArray());

                if (layer_list.Contains(answer))
                {
                    var elements = identifyResults[layer_list.IndexOf(answer)].GeoElements;

                    switch (((string)answer).Contains("Поля"))
                    {

                        case true:
                            {
                                var oids = from x in elements select (long)x.Attributes["T1.dbo.KernelForecast.ObjectID"];
 
                                using (var db = new Offline_DB_Context())
                                {

                                    var fields = await db.Fields.Where(f => oids.Contains(f.ObjectID)).ToListAsync();
                                    
                                    var selected_files = from x in fields select String.Format("{0} - ({1} га)", x.Name, x.S);
                                    var answer_field = await DisplayActionSheet(title: "Оберіть поле:", cancel: "Відмінити", destruction: "Ок", buttons: selected_files.ToArray());

                                    if (selected_files.Contains(answer_field))
                                    {

                                        PopupNavigation.Instance.PushAsync(new FeatureDetailsPopup(fields[selected_files.IndexOf(answer_field)].GetDataDict()));
                                        
                                    }
                                }
                                break;
                            }
                        case false:
                            {
                                var oids = from x in elements select (long)x.Attributes["OBJECTID"];
                                
                                using (var db = new Offline_DB_Context())
                                {
                                    var landplots = await db.Landplots.Where(l => oids.Contains(l.ObjectID)).ToListAsync();
                                    var selected_landplots = from x in landplots select String.Format("{0} - ({1} га)", x.CadNumber, x.Square);
                                    var answer_landplots = await DisplayActionSheet(title: "Оберіть поле:", cancel: "Відмінити", destruction: "Ок", buttons: selected_landplots.ToArray());

                                    if (selected_landplots.Contains(answer_landplots))
                                    {
                                        Landplot landplot = landplots[selected_landplots.IndexOf(answer_landplots)];
                                        var rent_list = await db.Rents.Where(r => r.CadastrNumber == landplot.CadNumber.Replace(":", "")).ToListAsync();

                                        if (rent_list.Count() > 0)
                                        {
                                            PopupNavigation.Instance.PushAsync(new FeatureDetailsPopup(landplots[selected_landplots.IndexOf(answer_landplots)].GetDataDict(), rent_list));
                                        }
                                        else PopupNavigation.Instance.PushAsync(new FeatureDetailsPopup(landplots[selected_landplots.IndexOf(answer_landplots)].GetDataDict()));

                                    }

                                }

                                    break;
                            }
                    }
                    //List<string> elements_list = new List<string>();

                    //var elements = identifyResults[layer_list.IndexOf(answer)].GeoElements;

                    //foreach (var element in elements)
                    //{
                    //    elements_list.Add(element.Attributes.ToString());
                    //}

                    //var answer_feature = await DisplayActionSheet(title: "Оберіть елемент:", cancel: "Відмінити", destruction: "Ок", buttons: elements_list.ToArray());


                    //if (elements_list.Contains(answer_feature))
                    //{
                    //    PopupNavigation.Instance.PushAsync(new FeatureDetailsPopup(elements[elements_list.IndexOf(answer_feature)]));

                    //}


                    }
                
            }


        }




        // Map initialization logic is contained in MapViewModel.cs
    }
}
