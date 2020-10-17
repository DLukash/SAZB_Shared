using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAZB_shared.Shared.MapViewModel;
using System.IO;
using Xamarin.Forms;
using System.Reflection;

[assembly: Dependency(typeof(SAZB_shared.UWP.GetGeoPackageePathService))]
namespace SAZB_shared.UWP

{   

    public class GetGeoPackageePathService : IGeoPackageePath
    {
        public string GetGeoPackageePath()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}
