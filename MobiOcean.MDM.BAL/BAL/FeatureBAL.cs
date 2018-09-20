using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.FeatureDALTableAdapters;
/// <summary>
/// Summary description for FeatureBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class FeatureBAL
    {
        tblFeatureTableAdapter ftr;
        tblFeatureTableAdapter tafeature;
        tblFeatureCategoryTableAdapter featurectgry;
        DataTable dt;
        private int _FeatureId;
        private int _CategoryId, _DeviceId;
        private string _FeatureCode;
        private string _FeatureName;
        private string _FeatureDesc;
        private int _Status;
        private string _LoggedBy;
        private string _RowVer;
        private string _APPId, _CategoryCode, _CategoryName, _cid;
        public int ClientId { get; set; }
        public string SolutionId { get; set; }
        public string APPId
        {
            get { return _APPId; }
            set { _APPId = value; }
        }
        public int DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        public int FeatureId
        {
            get { return _FeatureId; }
            set { _FeatureId = value; }
        }
        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        public string FeatureCode
        {
            get { return _FeatureCode; }
            set { _FeatureCode = value; }
        }
        public string FeatureName
        {
            get { return _FeatureName; }
            set { _FeatureName = value; }
        }
        public string FeatureDesc
        {
            get { return _FeatureDesc; }
            set { _FeatureDesc = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string LoggedBy
        {
            get { return _LoggedBy; }
            set { _LoggedBy = value; }
        }
        public string RowVer
        {
            get { return _RowVer; }
            set { _RowVer = value; }
        }
        public string CategoryCode
        {
            get { return _CategoryCode; }
            set { _CategoryCode = value; }
        }
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        public string cid
        {
            get { return _cid; }
            set { _cid = value; }
        }
        public FeatureBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetFeature()
        {
            try
            {
                tafeature = new tblFeatureTableAdapter();
                return tafeature.GetfeatureByAppId(_APPId);
            }
            catch (Exception)
            {
                dt = new DataTable();
                return dt;
            }
            finally
            {
                tafeature = null;
            }
        }
        public DataTable GetFeatureUpdate()
        {
            try
            {
                tafeature = new tblFeatureTableAdapter();
                return tafeature.GetFeatureUpdate(_DeviceId, _APPId);
            }
            catch (Exception)
            {
                dt = new DataTable();
                return dt;
            }
            finally
            {
                tafeature = null;
            }
        }
        public DataTable getdata()
        {
            ftr = new tblFeatureTableAdapter();
            dt = new DataTable();
            try
            {
                dt = ftr.GetData();
                return dt;
            }
            finally
            {
                dt = null;
                ftr = null;
            }
        }
        public DataTable getdata1()
        {
            ftr = new tblFeatureTableAdapter();
            dt = new DataTable();
            try
            {
                dt = ftr.GetData1();
                return dt;
            }
            finally
            {
                dt = null;
                ftr = null;
            }
        }
        public string InsertUpdateFeatureDtls()
        {
            ftr = new tblFeatureTableAdapter();
            return ftr.InsertUpdateFeatureDtls(_FeatureId, _FeatureCode, _FeatureName, _FeatureDesc, _CategoryId).ToString();
        }
        public int DeleteFeature()
        {
            ftr = new tblFeatureTableAdapter();
            return ftr.DeleteQuery(_FeatureId);
        }
        public DataTable GetCategoryName()
        {
            featurectgry = new tblFeatureCategoryTableAdapter();
            return featurectgry.GetCategoryName();
        }
        public DataTable GetfeatureName()
        {
            ftr = new tblFeatureTableAdapter();
            return ftr.GetFeatureName(_FeatureId);
        }
        public DataTable GetImageUrl()
        {
            featurectgry = new tblFeatureCategoryTableAdapter();
            return featurectgry.GetData();
        }
        public DataTable GetActiveSolutions()
        {
            featurectgry = new tblFeatureCategoryTableAdapter();
            return featurectgry.GetActiveSolutions2(ClientId, SolutionId);
        }
        public int IsAttendanceEnable()
        {
            try
            {
                featurectgry = new tblFeatureCategoryTableAdapter();
                return Convert.ToInt32(featurectgry.IsAttendanceEnable(ClientId).ToString());
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public int IsTransportEnable()
        {
            try
            {
                featurectgry = new tblFeatureCategoryTableAdapter();
                return Convert.ToInt32(featurectgry.IsTransportEnable(ClientId).ToString());
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public string calculateprice(string categoryids, int duration, int noofsolution)
        {
            dt = new DataTable();
            featurectgry = new tblFeatureCategoryTableAdapter();
            dt = featurectgry.GetData();            
            float price = 0;
            string[] catid = categoryids.Split(',');
            for (int i = 0; i < catid.Count(); i++)
            {
                catid[i] = catid[i].Trim();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["CategoryId"].ToString() == catid[i])
                    {
                        price += float.Parse(dt.Rows[j]["CloudPrice"].ToString()) * duration * noofsolution * 30;
                        break;
                    }
                }
            }
            return price.ToString();
        }
        //public int CheckFeature(int clientid,string categoryid)
        //{
        //    try
        //    {
        //        featurectgry = new tblFeatureCategoryTableAdapter();
        //        return Convert.ToInt32(featurectgry.CheckFeature(clientid, categoryid).ToString());
        //    }
        //    catch(Exception)
        //    {
        //        return 0;
        //    }
        //}

        public int CheckFeature(int clientid, string categoryid)
        {
            try
            {
                featurectgry = new tblFeatureCategoryTableAdapter();
                return Convert.ToInt32(featurectgry.CheckFeature1(clientid, categoryid).ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public DataTable GetNewActiveSolutions()
        {
            featurectgry = new tblFeatureCategoryTableAdapter();
            return featurectgry.GetNewActiveSolutions(ClientId, SolutionId);
        }
        public DataTable GetSelectedCategory()
        {
            featurectgry = new tblFeatureCategoryTableAdapter();
            return featurectgry.GetSelectedCategory(cid);
        }


    }
}