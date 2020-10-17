using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAZB_shared
{

    public class StartPageMasterMenuItem
    {
        public StartPageMasterMenuItem()
        {
            TargetType = typeof(StartPageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}