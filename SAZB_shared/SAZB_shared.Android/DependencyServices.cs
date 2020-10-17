using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static SAZB_shared.Shared.MapViewModel;
using Xamarin.Forms;
using System.IO;
using Android;

[assembly: Dependency(typeof(SAZB_shared.Droid.GetGeoPackageePathService))]
namespace SAZB_shared.Droid
{
    public class GetGeoPackageePathService : IGeoPackageePath
    {
        public string GetGeoPackageePath()
        {
            return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
        }
    }
}