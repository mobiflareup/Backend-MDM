
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MobiOcean.MDM.DAL.DAL.CustomerDALTableAdapters;

/// <summary>
/// Summary description for CustomerBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class CustomerBAL
    {
        tblCustomerAssignmentTableAdapter assigncust;
        tblCustomerTableAdapter customer;
        TA_MasterTableAdapter taM;
        tblCustomerTempTableAdapter tempcust;
        TA_LocationDetailTableAdapter loc;
        AssignDailyCusTableAdapter AssignDal;
        public CustomerBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int AssignId { get; set; }
        public string EmployeeName { get; set; }
        public string appId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public int CreatedBy { get; set; }
        public string CustomerName { get; set; }
        public string CountryId { get; set; }
        public string MobileNo { get; set; }
        public string ALtMobileNo { get; set; }
        public string AltCountryId { get; set; }
        public string ContactPersion { get; set; }
        public string AltContactPersion { get; set; }
        public string EmailId { get; set; }
        public string AltEmailId { get; set; }
        public string Address { get; set; }
        public string AltAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string PinCode { get; set; }
        public string TinNumber { get; set; }
        public double ApprovedAmmount { get; set; }
        public string ApproverRemark { get; set; }
        public int IsInsert { get; set; }
        public int MasterID { get; set; }
        public int VisitId { get; set; }
        public int CustomerTempId { get; set; }
        public int IsVisited { get; set; }
        public int VisitDetailId { get; set; }
        public int userId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int status { get; set; }
        public int IsApproved { get; set; }
        public string Remark { get; set; }
        public DateTime currentDateTime { get; set; }
        public int isTodayList { get; set; }
        public string TaskDetail { get; set; }
        // private int _UserId, _ClientId, _CustomerId, _CreatedBy;

        //public int UserId
        //{
        //    get { return _UserId; }
        //    set { _UserId = value; }
        //}
        //public int ClientId
        //{
        //    get { return _ClientId; }
        //    set { _ClientId = value; }
        //}
        //public int CustomerId
        //{
        //    get { return _CustomerId; }
        //    set { _CustomerId = value; }
        //}
        //public int CreatedBy
        //{
        //    get { return _CreatedBy; }
        //    set { _CreatedBy = value; }
        //}
        public void UpdateAssignDailyCustomerAlertStatus()
        {
            AssignDal = new AssignDailyCusTableAdapter();
            AssignDal.UpdateAssignDailyCustomerAlertStatus(AssignId);
        }
        public int AssignCustomerToUser()
        {
            assigncust = new tblCustomerAssignmentTableAdapter();
            return Convert.ToInt32(assigncust.AssignCustomerToUser(UserId, CustomerId, ClientId, CreatedBy).ToString());
        }
        public int AssignCustDaily()
        {
            AssignDal = new AssignDailyCusTableAdapter();
            return (AssignDal.sp_AssignDailyList(ClientId, UserId, CustomerId, Date, Time, status, CreatedBy, currentDateTime));
        }
        public int AssignCustomerDaily()
        {
            AssignDal = new AssignDailyCusTableAdapter();
            return (AssignDal.sp_AssignDailyList1(ClientId, UserId, CustomerId, Date, Time, status, CreatedBy, currentDateTime, TaskDetail));
        }
        public int UnAssignCustomerToUser()
        {
            assigncust = new tblCustomerAssignmentTableAdapter();
            return Convert.ToInt32(assigncust.UnAssignCustomerToUser(UserId, CustomerId, ClientId, CreatedBy).ToString());
        }
        public DataTable GetAssignedCustomer()
        {
            assigncust = new tblCustomerAssignmentTableAdapter();
            return assigncust.GetAssignedCustomer(UserId);
        }
        public DataTable GetCustomerDetailsBasedOnIsTodayList()
        {
            assigncust = new tblCustomerAssignmentTableAdapter();
            return assigncust.GetCustomerDetailsBasedOnIsTodayList(UserId, isTodayList);
        }
        public int InsertCustomerDetails()
        {
            assigncust = new tblCustomerAssignmentTableAdapter();
            return Convert.ToInt32(assigncust.InsertCustomerDetails(CustomerId, ClientId, CustomerName, MobileNo, EmailId, ALtMobileNo, ContactPersion, AltContactPersion, AltEmailId, Address, AltAddress, Latitude, Longitude, City, District, state, country, PinCode, TinNumber, UserId).ToString());
        }

        public int UpdateAssignCustDaily()
        {
            AssignDal = new AssignDailyCusTableAdapter();
            return AssignDal.UpdateCustomerAssigDaily(Time, status, AssignId);
        }
        public int UpdateCustomerDailyTask()
        {
            AssignDal = new AssignDailyCusTableAdapter();
            return AssignDal.UpdateCustomerDailyTask(Time, status, TaskDetail, AssignId);
        }
        public int UpdateCustomerInTAMaster()
        {
            assigncust = new tblCustomerAssignmentTableAdapter();
            return Convert.ToInt32(assigncust.UpdateCustomerInTAMaster(UserId, VisitId, CustomerId, CustomerTempId, IsVisited).ToString());
        }
        public int UpdateCustomerInTAMasterRaj()
        {
            assigncust = new tblCustomerAssignmentTableAdapter();
            return Convert.ToInt32(assigncust.UpdateCustomerInTAMasterRaj(UserId, VisitId, CustomerId, CustomerTempId, IsVisited).ToString());
        }
        public int customerInsert()
        {
            try
            {
                customer = new tblCustomerTableAdapter();
                customer.inserttblCustomer(ClientId, CustomerName, MobileNo, ALtMobileNo, ContactPersion, AltContactPersion, EmailId, AltEmailId, Address, AltAddress, Latitude, Longitude, City, District, state, country, PinCode, TinNumber, UserId, CustomerId, IsInsert);
                return 1;
            }
            catch
            {
                return 0;
            }

        }
        public int customerInsertRaj()
        {
            try
            {
                customer = new tblCustomerTableAdapter();
                customer.inserttblCustomerRaj(ClientId, CustomerName, MobileNo, ALtMobileNo, ContactPersion, AltContactPersion, EmailId, AltEmailId, Address, AltAddress, Latitude, Longitude, City, District, state, country, PinCode, TinNumber, UserId, CustomerId, IsInsert, CountryId, AltCountryId);
                return 1;
            }
            catch
            {
                return 0;
            }

        }
        public DataTable CustomerDetails()
        {
            try
            {
                customer = new tblCustomerTableAdapter();
                return customer.GetCustomerdtl(ClientId);//CustomerId, ClientId, Name
            }
            catch
            {
                return null;
            }
        }

        public DataTable CustomerDetailsbyCustomerid()
        {
            try
            {
                customer = new tblCustomerTableAdapter();
                return customer.GetcustomerDetailBycustomerid(CustomerId);
            }
            catch
            {
                return null;
            }
        }
        public DataTable TempCustomerDetailsbyCustomerid()
        {
            try
            {
                customer = new tblCustomerTableAdapter();
                return customer.GettempCustomerdetailsbyCustomerid(CustomerTempId);
            }
            catch
            {
                return null;
            }
        }
        public DataTable TempCustomerDetailsbyClientId()
        {
            try
            {
                tempcust = new tblCustomerTempTableAdapter();
                return tempcust.GetTempCustomerByClientId(ClientId);
            }
            catch
            {
                return null;
            }
        }

        public int DeleteCustomerDetailsbyCustomerid()
        {
            try
            {
                customer = new tblCustomerTableAdapter();
                customer.DeleteCustomerDtl(CustomerId);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        //public DataTable GetTAMaster()
        //{
        //    taM = new TA_MasterTableAdapter();
        //    return taM.GetTAMasterDetails(ClientId);
        //}
        //public DataTable GetTAMasterDetailsByMasterId()
        //{
        //    taM = new TA_MasterTableAdapter();
        //    return taM.GetTAMDetailsByMasterId(ClientId, MasterID);
        //}
        public int UpdateApprovedstatus()
        {
            try
            {
                taM = new TA_MasterTableAdapter();
                taM.UpdateApprovedStatus(UserId, ApprovedAmmount, ApproverRemark, MasterID);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int UpdateApprovedandPaystatus()
        {
            try
            {
                taM = new TA_MasterTableAdapter();
                taM.UpdateApprovedandPaidStatus(UserId, ApprovedAmmount, ApproverRemark, MasterID);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int UpdatePaystatus()
        {
            try
            {
                taM = new TA_MasterTableAdapter();
                taM.UpdatePaidStatus(UserId, ApprovedAmmount, ApproverRemark, MasterID);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public DataTable GetAllCustomerFromtblCustomerTemp()
        {
            tempcust = new tblCustomerTempTableAdapter();
            return tempcust.GetCustomerbyCustomerTempId(CustomerTempId);
        }
        public int ApproveCustomer()
        {
            tempcust = new tblCustomerTempTableAdapter();
            return Convert.ToInt32(tempcust.ApproveCustomer(CustomerTempId).ToString());
        }
        public int UpdatetblCustomerTemp()
        {
            tempcust = new tblCustomerTempTableAdapter();
            return Convert.ToInt32(tempcust.UpdatetblCustomerTemp(ClientId, CustomerName, MobileNo, ALtMobileNo, ContactPersion, AltContactPersion, EmailId, AltEmailId, Address, AltAddress, Latitude, Longitude, City, District, state, country, PinCode, TinNumber, UserId, CustomerTempId).ToString());
        }
        public DataTable GetTA_MasterDetailsByUserId()
        {
            taM = new TA_MasterTableAdapter();
            return taM.GetTA_MasterDetailsByUserId(UserId);
        }
        public DataTable GetTA_VisitDetailByMasterId()
        {
            taM = new TA_MasterTableAdapter();
            return taM.GetTA_VisitDetailByMasterId(MasterID);
        }
        public DataTable GetTA_ExtraDetailByMasterId()
        {
            taM = new TA_MasterTableAdapter();
            return taM.GetTA_ExtraDetailByMasterId(MasterID);
        }

        public DataTable GetCustomerdtl()
        {
            customer = new tblCustomerTableAdapter();
            return customer.GetCustomerdtl(ClientId);
        }
        public DataTable GetLocationByVisitDetailId()
        {
            loc = new TA_LocationDetailTableAdapter();
            return loc.GetLocationByVisitDetailId(VisitDetailId);
        }

        public int AutoCustomerId { get; set; }

        public int AutotempCustomerId { get; set; }

        public DataTable GetCustomeridByName()
        {
            customer = new tblCustomerTableAdapter();
            return customer.GetCustomeridByName(CustomerName, ClientId);
        }

        public DataTable GetEmployeeidByContactNo()
        {
            customer = new tblCustomerTableAdapter();
            return customer.GetEmployeeIdByMobileNo(MobileNo, ClientId);
        }
        public void AssignDailyCutomerUpdate()
        {
            customer = new tblCustomerTableAdapter();
            customer.UpdateAssignCustomer(Remark, IsApproved, AssignId);
        }

    }
    public class tamasterdetail
    {
        public int masterid { get; set; }
        public int clientid { get; set; }
        public int userid { get; set; }
        public string logdate { get; set; }
        public double claimedamt { get; set; }
        public double approvedamt { get; set; }
        public string approverremark { get; set; }
        public int isapproved { get; set; }
        public int istripend { get; set; }
        public int ispaid { get; set; }
        public double totaldistance { get; set; }
        public string approvedby { get; set; }
        public List<tavisitdetail> tavisitdetail { get; set; }
        public List<taextradetail> taextradetail { get; set; }
    }
    public class tavisitdetail
    {
        public int visitdetailid { get; set; }
        public string fromdatetime { get; set; }
        public string todatetime { get; set; }
        public string visitdatetime { get; set; }
        public int customerid { get; set; }
        public int isvisited { get; set; }
        public int isreturn { get; set; }
        public string remark { get; set; }
        public string filepath { get; set; }
        public double claimedtravelamt { get; set; }
        public double approvedtravelamt { get; set; }
        public double rateperkm { get; set; }
        public double fromlat { get; set; }
        public double fromlong { get; set; }
        public string fromlocation { get; set; }
        public double tolat { get; set; }
        public double tolong { get; set; }
        public string tolocation { get; set; }
        public int modeoftravel { get; set; }
        public double totaldistance { get; set; }
        public string approverremark { get; set; }
    }
    public class taextradetail
    {
        public int extradetailid { get; set; }
        public string logdatetime { get; set; }
        public double claimedamt { get; set; }
        public double approvedamt { get; set; }
        public string approverremark { get; set; }
        public int isapproved { get; set; }
        public string remark { get; set; }
        public string filepath { get; set; }
        public string approvedby { get; set; }
    }
}
