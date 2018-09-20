using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.NewsSubDALTableAdapters;
/// <summary>
/// Summary description for NewsSubBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class NewsSubBAL
    {

        tblDemoproductTableAdapter dpt;
        tblNewssubscriptionTableAdapter nst;
        private int _Id, _IsDemoDone;
        private string _EmailId;
        private int _IsSubscription;
        public int IsSubscription
        {
            get { return _IsSubscription; }
            set { _IsSubscription = value; }
        }
        public int IsDemoDone
        {
            get { return _IsDemoDone; }
            set { _IsDemoDone = value; }
        }
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public NewsSubBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable NewsSub()
        {
            nst = new tblNewssubscriptionTableAdapter();
            return nst.NewsSubInsert(_Id, _EmailId);
        }
        public int InsertDemoproduct()
        {
            dpt = new tblDemoproductTableAdapter();
            return dpt.InsertDemoproductDetail(_Id, _EmailId,Name,MobileNo, _IsDemoDone);
        }
        public DataTable GetDemoProductDtls()
        {
            dpt = new tblDemoproductTableAdapter();
            return dpt.GetDemoProductDtls();
        }
        public DataTable GetNewssubscriptionDtls()
        {
            nst = new tblNewssubscriptionTableAdapter();
            return nst.GetNewssubscriptionDtls();
        }
    }
}