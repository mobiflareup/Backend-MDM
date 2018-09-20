using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiOcean.MDM.BAL.Model
{
    public class CustomAppModel
    {
        public int appTypeId { get; set; }
        public string appName { get; set; }
        public string packageName { get; set; }
        public string downloadUrl { get; set; }
    }
}
