using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiOcean.MDM.BAL.Model
{
    public class RecurringTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeOfCost { get; set; }
        public List<SolutionModel> data { get; set; }
    }
    public class SolutionModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<FeaturesModel> Features { get; set; }
    }
    public class FeaturesModel
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
        public string Price { get; set; }
        public bool IsAndroid { get; set; }
        public bool IsIOS { get; set; }
        public bool IsBuyNow { get; set; }
        public string BuyNowLink { get; set; }
        public string PageLink { get; set; }
    }
    public class OneTimeDataModel
    {
        public int OneTimeId { get; set; }
        public string DeviceModel { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
    }
    public class ActualDataModel
    {
        public int ActualId { get; set; }
        public string Service { get; set; }
        public string Price { get; set; }
    }
}
