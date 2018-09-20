using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;

namespace MobiOcean.MDM.Web.Controller
{
    public class CategoryListController : ApiController
    {
        GingerboxSrch search;
        FeatureCategoryList featurebal, featurecategorylst;
        Solution solution;
        DataTable dt, dt1, dtSol;
        [ActionName("GetFeatureAndCategoryList")]
        public List<Solution> Get()
        {
            List<Solution> SolutionList = new List<Solution>();
            dtSol = new DataTable();
            featurebal = new FeatureCategoryList();
            dtSol = featurebal.GetSolutionName();
            foreach (DataRow row2 in dtSol.Rows)
            {
                solution = new Solution();
                solution.solutionId = Convert.ToInt32(row2["SolutionId"].ToString());
                solution.solutionName = row2["SolutionName"].ToString();



                List<FeatureCategoryList> lst = new List<FeatureCategoryList>();
                featurebal = new FeatureCategoryList();
                dt = new DataTable();
                featurebal.SolutionId = Convert.ToInt32(row2["SolutionId"].ToString());
                dt = featurebal.GetCategoryNameBySolutionId();
                foreach (DataRow row in dt.Rows)
                {
                    featurecategorylst = new FeatureCategoryList();
                    featurecategorylst.CategoryId = Convert.ToInt32(row["CategoryId"].ToString());
                    featurecategorylst.CategoryName = row["CategoryName"].ToString();
                    featurecategorylst.price = float.Parse(row["CloudPrice"].ToString());
                    featurecategorylst.android = Convert.ToInt32(row["Android"].ToString());
                    featurecategorylst.ios = Convert.ToInt32(row["IOS"].ToString());
                    featurecategorylst.buyNow = Convert.ToInt32(row["BuyNow"].ToString());
                    featurecategorylst.buyNowLink = row["BuyNowLink"].ToString();
                    dt1 = featurebal.GetFeatureAndCategoryListByCategoryId(Convert.ToInt32(row["CategoryId"].ToString()));
                    List<FeatureList> featurelist = new List<FeatureList>();
                    foreach (DataRow row1 in dt1.Rows)
                    {
                        FeatureList feature = new FeatureList
                        {
                            FeatureId = Convert.ToInt32(row1["FeatureId"].ToString()),
                            FeatureName = row1["FeatureName"].ToString()
                        };
                        featurelist.Add(feature);
                    }
                    featurecategorylst.feature = featurelist;
                    lst.Add(featurecategorylst);
                }
                solution.categoryList = lst;
                SolutionList.Add(solution);
            }
            return SolutionList;
        }
        [ActionName("GetSolutions")]
        public List<Solution> Get(int Id = 0)
        {
            List<Solution> SolutionList = new List<Solution>();
            dtSol = new DataTable();
            featurebal = new FeatureCategoryList();
            dtSol = featurebal.GetSolutionName();
            foreach (DataRow row2 in dtSol.Rows)
            {
                solution = new Solution();
                solution.solutionId = Convert.ToInt32(row2["SolutionId"].ToString());
                solution.solutionName = row2["SolutionName"].ToString();



                List<FeatureCategoryList> lst = new List<FeatureCategoryList>();
                featurebal = new FeatureCategoryList();
                dt = new DataTable();
                featurebal.SolutionId = Convert.ToInt32(row2["SolutionId"].ToString());
                dt = featurebal.GetCategoryNameBySolutionId();
                foreach (DataRow row in dt.Rows)
                {
                    featurecategorylst = new FeatureCategoryList();
                    featurecategorylst.CategoryId = Convert.ToInt32(row["CategoryId"].ToString());
                    featurecategorylst.CategoryName = row["CategoryName"].ToString();
                    featurecategorylst.price = float.Parse(row["CloudPrice"].ToString());
                    featurecategorylst.android = Convert.ToInt32(row["Android"].ToString());
                    featurecategorylst.ios = Convert.ToInt32(row["IOS"].ToString());
                    featurecategorylst.buyNow = Convert.ToInt32(row["BuyNow"].ToString());
                    featurecategorylst.buyNowLink = row["BuyNowLink"].ToString();
                    //dt1 = featurebal.GetFeatureAndCategoryListByCategoryId(Convert.ToInt32(row["CategoryId"].ToString()));
                    //List<FeatureList> featurelist = new List<FeatureList>();
                    //foreach (DataRow row1 in dt1.Rows)
                    //{
                    //    FeatureList feature = new FeatureList
                    //    {
                    //        FeatureId = Convert.ToInt32(row1["FeatureId"].ToString()),
                    //        FeatureName = row1["FeatureName"].ToString()
                    //    };
                    //    featurelist.Add(feature);
                    //}
                    //featurecategorylst.feature = featurelist;
                    lst.Add(featurecategorylst);
                }
                solution.categoryList = lst;
                SolutionList.Add(solution);
            }
            return SolutionList;
        }

        [HttpGet]
        public List<RecurringTypeModel> GetPaymentWebPageData1()
        {
            //PaymentWebPage1 paymentWebPagedata = new PaymentWebPage1();
            List<RecurringTypeModel> recurList = new List<RecurringTypeModel>();
            search = new GingerboxSrch();
            dt = new DataTable();
            dt = search.GetPaymentHeading();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    RecurringTypeModel recur = new RecurringTypeModel();
                    recur.Id = Convert.ToInt32(row["PHId"].ToString());
                    recur.Name = row["PHName"].ToString();
                    recur.TypeOfCost = Convert.ToInt32(row["TypeOfCost"].ToString());
                    recur.data = GetCategoryListByPHId(row["PHId"].ToString());
                    recurList.Add(recur);
                }
                return recurList;
            }
            return new List<RecurringTypeModel>();
        }
        private List<SolutionModel> GetCategoryListByPHId(string PHId)
        {

            search = new GingerboxSrch();
            dt = search.GetSolutionList(PHId);
            if (dt != null)
            {
                List<SolutionModel> SModel = new List<SolutionModel>();
                foreach (DataRow row in dt.Rows)
                {
                    SolutionModel sm = new SolutionModel();
                    sm.CategoryId = Convert.ToInt32(row["SolutionId"].ToString());
                    sm.CategoryName = row["SolutionName"].ToString();
                    sm.Features = GetCategoryList(row["SolutionId"].ToString());
                    SModel.Add(sm);
                }
                return SModel;
            }
            return new List<SolutionModel>();
        }
        private List<FeaturesModel> GetCategoryList(string SolutionId)
        {
            dt = new DataTable();
            dt = search.GetCategoryList(SolutionId);
            if (dt != null)
            {
                List<FeaturesModel> FModel = new List<FeaturesModel>();
                foreach (DataRow row in dt.Rows)
                {
                    FeaturesModel fm = new FeaturesModel();
                    fm.FeatureId = Convert.ToInt32(row["CategoryId"].ToString());
                    fm.FeatureName = row["CategoryName"].ToString();
                    fm.Price = row["CloudPrice"].ToString();
                    fm.IsAndroid = Convert.ToBoolean(Convert.ToInt16(row["Android"].ToString()));
                    fm.IsIOS = Convert.ToBoolean(Convert.ToInt16(row["IOS"].ToString()));
                    fm.IsBuyNow = Convert.ToBoolean(Convert.ToInt16(row["BuyNow"].ToString()));
                    fm.BuyNowLink = row["BuyNowLink"] != null ? row["BuyNowLink"].ToString() : null;
                    fm.PageLink = row["PageLink"] != null ? row["PageLink"].ToString() : null;
                    FModel.Add(fm);
                }
                return FModel;
            }
            return new List<FeaturesModel>();
        }
    }
}
