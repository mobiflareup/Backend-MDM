using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiOcean.MDM.BAL.Model
{
    public class LocationModel
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string location { get; set; }
        public int isLatLongFromCellId { get; set; }
    }
}
