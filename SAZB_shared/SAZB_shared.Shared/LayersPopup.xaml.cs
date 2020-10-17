//using Android.OS;
using Rg.Plugins.Popup.Services;
using SAZB_shared.Shared;
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
    public partial class LayersPopup : Rg.Plugins.Popup.Pages.PopupPage
    {

        private MapViewModel mapModel;
        public LayersPopup()
        {
            InitializeComponent();
        }

        public LayersPopup(MapViewModel mapViewModel)
        {
            InitializeComponent();
            BindingContext = mapViewModel;
            mapModel = mapViewModel;


        }

        private void On_SwitchCell_changed(object sender, ToggledEventArgs args)
        {
            (((SwitchCell)sender).BindingContext as Esri.ArcGISRuntime.Mapping.FeatureLayer).IsVisible = args.Value;
        }

        private void Ok_button_Clicked(object sender, EventArgs e)
        {
            
            PopupNavigation.Instance.PopAllAsync(true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }


    }
}