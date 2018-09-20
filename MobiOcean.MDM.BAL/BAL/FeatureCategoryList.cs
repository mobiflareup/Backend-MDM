using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.FeatureDALTableAdapters;

/// <summary>
/// Summary description for FeatureCategoryList
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class FeatureCategoryList
    {
        tblFeatureCategoryTableAdapter tblfeature;
        tblSolutionCategoryTableAdapter tblSolution;
        private int _CategoryId;
        private string _CategoryName;
        public List<FeatureList> feature { get; set; }
        public int SolutionId { get; set; }
        public float price { get; set; }
        public int android { get; set; }
        public int ios { get; set; }
        public int buyNow { get; set; }
        public string buyNowLink { get; set; }
        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        public FeatureCategoryList()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //public DataTable GetFeatureAndCategoryList()
        //{
        //    tblfeature = new tblFeatureCategoryTableAdapter();
        //    return tblfeature.GetFeatureAndCategoryList();
        //}
        public DataTable GetCategoryName()
        {
            tblfeature = new tblFeatureCategoryTableAdapter();
            return tblfeature.GetCategoryName();
        }
        public DataTable GetCategoryNameBySolutionId()
        {
            tblfeature = new tblFeatureCategoryTableAdapter();
            return tblfeature.GetCategoryNameBySolutionId(SolutionId);
        }
        public DataTable GetSolutionName()
        {
            tblSolution = new tblSolutionCategoryTableAdapter();
            return tblSolution.GetSolutionName();
        }
        public DataTable GetFeatureAndCategoryListByCategoryId(int catid)
        {
            tblfeature = new tblFeatureCategoryTableAdapter();
            return tblfeature.GetFeatureByCategoryId(catid);
        }


    }
    public class FeatureList
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
    }

    public class Solution
    {
        public int solutionId { get; set; }
        public string solutionName { get; set; }
        public List<FeatureCategoryList> categoryList { get; set; }
    }
}