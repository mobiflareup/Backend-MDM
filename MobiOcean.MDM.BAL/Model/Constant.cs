using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Constant
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class Constant
    {
        public static string URL = "https://admin.mobiocean.com/";//"http://localhost/MobiOcean.MDM/";//
        public static string MobiURL = "https://mobiocean.com/";//  
        public static string Home = "https://mobiocean.com/";// 
        public static string MobiMove = "https://mobi-move.com/";
        public static string PaymentURL = "https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction";
        public static string MobiMoveSchool = "https://mobi-move.com/";
        public static string SecureSMSAndroidLink = "https://play.google.com/store/apps/details?id=com.loment.peanut.mobile&hl=en";
        public static string SecureEmailAndroidLink = "https://play.google.com/store/apps/details?id=com.loment&hl=en";
        public static string SecureMessengerAndroidLink = "https://play.google.com/store/apps/details?id=com.loment.cashew&hl=en";
        public static string PaymentReturnURL = URL + "ccavResponseHandler.aspx";
        public static string AppDownloadUrl = MobiURL + "download.php";
        public static string LTAppDownloadUrl = "https://play.google.com/store/apps/details?id=com.gingerbox.travelallowance";
        public static string CommonAppUrl = "https://play.google.com/store/apps/details?id=com.mobiocean.common";// URL + "download";
        public static string AndroidFilePath = URL + "Files/Android_Files";
        public static string FilePath = "Files/UploadFiles/";

        public static string GenusAPI = "http://220.227.2.173/mobioceantest/mobservice.svc/";//"http://220.227.2.173/mobiocean/mobservice.svc/";//

        //PayU
        public static string MERCHANT_KEY =  "fIBB8fDn";//"rjQUPktU";//
        public static string SALT =  "SX4VvVlqXf";//"e5iIg1jwi8";//
        public static string PAYU_BASE_URL =  "https://secure.payu.in";//"https://test.payu.in";//
        //public static string TestMERCHANT_KEY = "rjQUPktU";//
        //public static string TestSALT = "e5iIg1jwi8";//
        //public static string TestPAYU_BASE_URL = "https://test.payu.in";//
        public static string hashSequence = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
        public static string PayUPaymentReturnURL = URL + "web/ResponseHandling";
        public static string PayUPaymentReturnURLNew = URL + "web/PayResponse";
        //PayU End
        public static string FolderPath = "Files/";
        public static string APKFolderPath = "APK/";
        public static string PublicAPKFolderPath = "PublicAPK/";
        public static string MobileNoLength = "10";
        public static int addMinutes = 330;

        public static string DateFormat = "dd-MMM-yyyy";
        public static string DateTimeFormat = "dd-MMM-yyyy HH:mm";

        public static string CompanyName = "Mobi Ocean Mobility Software Solutions LLP";
        public static string CompanyShortName = "Mobi Ocean Mobility";
        public static string CategoryIds = "2,5,6,7,8,10,22,24,25";
        public static int addTrialDays = 0;
        public static int addTrialDaysForMail = 10;

        public static int isservicetax = 1;
        public static int servicetax = 15;

        public static int CGST = 9;
        public static int SGST = 9;
        public static int IGST = 18;
        public static int SID = 6; //New Delhi Id in database


        public static string merchant_id = "106160";
        public static string workingKey = "543FE4902D135367387F6FA5B9ED5261";
        public static string strAccessCode = "AVVX66DH43AP80XVPA";

        public static string TollFree = @"+91-11-46104530";
        public static string infoEmail = "info@mobiocean.com";
        public static string supportEmail = "support@mobiocean.com";
        public static string enquiryEmail = "sales@mobiocean.com";
        public static string salesEmail = "sales@mobiocean.com";
        public static string accountEmail = "account@mobiocean.com";
        public static string developerEmail = "sourabh.agg2009@gmail.com";
        public static string ceoAndMDEmail = "Kamal.Rana@mobiocean.com,subhash.bansal@mobiocean.com";

        //TABLES NAMES
        public const int tblFileDetails = 1;
        public const int tblDeviceLocation_tblDeviceLocationGeoFence = 2;
        public const int Attendence = 3;
        public const int tblConveyanceDetails = 4;
        public const int TA_VisitDetail = 5;
        public const int tblCallLog = 6;
        public const int tblSMSLogs = 7;
        public const int AndroidBtnPressDtls = 8;

        //APIs NAME
        public const int GeoFence = 1;
        public const int CellToLatLong = 2;
        public const int MapLoad = 3;
        public const int AutoSuggest = 4;
        public const int RouteDraw = 5;

        public static int GetMapIsUsed(string ApiType)
        {
            return ApiType.ToLower() == "google" ? 0 : 1;
        }
        public const int OTPLength = 6;
        public const string LocationNotFound = "Location not found";
        public const int VodaFoneService = 1;
    }
}

