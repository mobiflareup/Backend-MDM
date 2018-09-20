using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;
using MobiOcean.MDM.DAL.DAL.ClientDALTableAdapters;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.Infrastructure;
/// <summary>
/// Summary description for ClientBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class ClientBAL
    {
        tblClientTableAdapter clienttbl;
        sp_InsertClientManager1TableAdapter clientMngr1;
        tblMenuTableAdapter menu;
        tblStateTableAdapter state;
        DataTable dt;
        LoginBAL lgn;
        UserBAL usrbal;
        SendMailBAL sendMailBal;        

        private int _ClientId, _UserId, _RoleId, _DeptId;
        private string _ClientCode, _EmpCompanyId, _ClientName;
        private string _Address;
        private string _EmailId;
        private string _PhoneNo;
        private string _FaxNo;
        private string _Website;
        private string _ManagerName;
        private string _ManagerContactNo;
        private string _LogoFilepath;
        private int _Status;
        private string _LoggedBy, _UserName, _MobileNo, _Password, _ConfirmPassword;
        private string _RowVer;
        private int _NoOfEmployees;
        private string _TypeOfIndustry;
        private string _Designation;
        private int _CountryId;
        private string _FirstN, _LastN, _EmpId;
        private int? _PartnerId;
        private string _AppTypeIdList;
        public string AppTypeIdList
        {
            get { return _AppTypeIdList; }
            set { _AppTypeIdList = value; }
        }

        public DateTime currentDateTime { get; set; }
        public string ProductKey { get; set; }
        public int IsAgreeTerms { get; set; }
        public int? PartnerId
        {
            get { return _PartnerId; }
            set { _PartnerId = value; }
        }
        public int CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
        public string FirstN
        {
            get { return _FirstN; }
            set { _FirstN = value; }
        }
        public string LastN
        {
            get { return _LastN; }
            set { _LastN = value; }
        }
        public string EmpId
        {
            get { return _EmpId; }
            set { _EmpId = value; }
        }
        public int NoOfEmployees
        {
            get { return _NoOfEmployees; }
            set { _NoOfEmployees = value; }
        }



        public string TypeOfIndustry
        {
            get { return _TypeOfIndustry; }
            set { _TypeOfIndustry = value; }
        }
        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        public int DeptId
        {
            get { return _DeptId; }
            set { _DeptId = value; }
        }
        public string EmpCompanyId
        {
            get { return _EmpCompanyId; }
            set { _EmpCompanyId = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; }
        }
        public string ClientCode
        {
            get { return _ClientCode; }
            set { _ClientCode = value; }
        }
        public string ClientName
        {
            get { return _ClientName; }
            set { _ClientName = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        public string PhoneNo
        {
            get { return _PhoneNo; }
            set { _PhoneNo = value; }
        }
        public string FaxNo
        {
            get { return _FaxNo; }
            set { _FaxNo = value; }
        }
        public string Website
        {
            get { return _Website; }
            set { _Website = value; }
        }
        public string ManagerName
        {
            get { return _ManagerName; }
            set { _ManagerName = value; }
        }
        public string ManagerContactNo
        {
            get { return _ManagerContactNo; }
            set { _ManagerContactNo = value; }
        }
        public string LogoFilepath
        {
            get { return _LogoFilepath; }
            set { _LogoFilepath = value; }
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

        public string URL { get; set; }
        public ClientBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int IsMenuByClientId()
        {
            menu = new tblMenuTableAdapter();
            return Convert.ToInt16(menu.IsMenuByClientId(ClientId, RoleId, URL));
        }
        public DataTable getdata()
        {
            clienttbl = new tblClientTableAdapter();
            dt = new DataTable();
            try
            {
                dt = clienttbl.GetData();
                return dt;
            }
            finally
            {
                dt = null;
                clienttbl = null;
            }
        }
        public string InsertClient()
        {
            clienttbl = new tblClientTableAdapter();
            return clienttbl.InsertClientDtls(_ClientId, _ClientCode, _ClientName, _Address, _EmailId, _ManagerName, _ManagerContactNo, _LogoFilepath).ToString();
        }
        public int DeleteClientDtls()
        {
            clienttbl = new tblClientTableAdapter();
            return clienttbl.DeleteAllUser(_ClientId);
        }
        public DataTable GetClientByClientId()
        {
            clienttbl = new tblClientTableAdapter();
            dt = new DataTable();
            try
            {
                dt = clienttbl.GetAppliedSubscriptionByClientId(_ClientId);
                //dt = clienttbl.GetClientByClientId(_ClientId);
                return dt;
            }
            finally
            {
                dt = null;
                clienttbl = null;
            }
        }

        public DataTable spCountUserByClient()
        {
            clienttbl = new tblClientTableAdapter();
            dt = new DataTable();
            try
            {
                dt = clienttbl.spCountUserByClient(_ClientId);
                return dt;
            }
            finally
            {
                dt = null;
                clienttbl = null;
            }
        }
        public string InsertClientManager()
        {
            try
            {
                clientMngr1 = new sp_InsertClientManager1TableAdapter();
                dt = new DataTable();
                try
                {
                    _ClientCode = _ClientName.Replace(" ", "");
                    _ClientCode = _ClientCode.Substring(0, 3);
                }
                catch (Exception)
                {
                    _ClientCode = _ClientName.Replace(" ", "");
                }
                _ClientCode = _ClientCode + currentDateTime.ToString("MMyy");
                dt = clientMngr1.sp_InsertClientManager1(_ClientCode, _ClientName, _Address, _EmailId, _UserName, _MobileNo, Convert.ToString(_EmpCompanyId), _Password, _NoOfEmployees, _TypeOfIndustry, _Designation, _FirstN, _LastN, _CountryId, IsAgreeTerms, _PartnerId);//
                if (dt.Rows[0]["Result"].ToString() == "Registered Successfully")
                {
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    string ExpiryDate = currentDateTime.AddDays(Constant.addTrialDaysForMail).ToString(Constant.DateFormat);
                    ProductKey = "";
                    AddUser(UserId, ClientId, ProductKey, ExpiryDate);
                    // GenerateProductKey(ClientId, UserId);
                    SendMailSalesEmail(_ClientCode, _ClientName, _Address, _EmailId, _UserName, _MobileNo, Convert.ToString(_EmpCompanyId), _Password, _NoOfEmployees, _TypeOfIndustry, _Designation, _FirstN, _LastN, _CountryId, IsAgreeTerms, _PartnerId);//                     //AddUser(UserId, ClientId);
                                                                                                                                                                                                                                                             //AddUser(UserId, ClientId);
                                                                                                                                                                                                                                                             //AddMobiMoveSchool(UserId, ClientId);
                    return "You have registered succesfully. Please go to your mail and activate your account.";
                }
                else
                {
                    return dt.Rows[0]["Result"].ToString();
                }
            }
            catch (Exception ex)
            {
                return "Registration not completetd " + ex.Message;
            }

        }

        private void SendMailSalesEmail(string _ClientCode, string _ClientName, string _Address, string _EmailId, string _UserName, string _MobileNo, string v, string _Password, int _NoOfEmployees, string _TypeOfIndustry, string _Designation, string _FirstN, string _LastN, int _CountryId, int isAgreeTerms, int? _PartnerId)
        {
            string subject = "";
            string mailBody = "";
            try
            {
                SendMail mail = new SendMail();
                subject = "MobiOcean New Registration Notification";
                mailBody = @"Dear Team, <br/><br/>
                            New registering with " + Constant.CompanyName + ".<br/><br/> Client Code : " + _ClientCode +
                                "ClientName :" + _ClientName + "<br/>Address : " + _Address + "<br/>EmailId : " + _EmailId + "<br/>UserName : " + _UserName + "<br/>MobileNo : " + _MobileNo + "<br/>No Of Employees : " + _NoOfEmployees + "<br/>TypeOfIndustry : " + _TypeOfIndustry + "<br/>Designation : " + _Designation + "<br/><br/><br/>This is an auto-generated e-mail, please do not reply to this message!<br/> <br/>   Best Regards, <br/> MobiOcean Team";

                mail.SendEmail(Constant.salesEmail, subject, mailBody, ClientId, Constant.developerEmail);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            { }
        }
        private int AddMobiMoveSchool(int userId, int clientId)
        {
            try
            {
                DataTable dtclient = new DataTable();
                _ClientId = clientId;
                //SELECT        ClientId, ClientCode, ClientName, Address, EmailId, PhoneNo, FaxNo, Website, ManagerName, ManagerContactNo, LogoFilepath, Status, CreatedBy, CreationDate, 
                //                 UpdatedBy, UpdationDate, RowVer, DeviceId, ProductKey, ExpiryDate, IsTrial, SubscriptionType, IsFirstLogin, IsKeyVerified, NumberOfEmployees, TypeOfClient, 
                //                 Designation, Password, IsTrialUsed, FirstName, LastName, CountryId, UserId, IsAgreeTerm, EmployeeCount, DateDiff(day,GetUTCDate(),ExpiryDate) As expirydatecount
                dtclient = GetClientByClientId();
                AddClientModel addClientModel = new AddClientModel()
                {
                    ClientCode = dtclient.Rows[0]["ClientCode"].ToString(),
                    Address = dtclient.Rows[0]["Address"].ToString(),
                    ClientName = dtclient.Rows[0]["ClientName"].ToString(),
                    ContactPersonName = dtclient.Rows[0]["ManagerName"].ToString(),
                    ClientEmailId = dtclient.Rows[0]["EmailId"].ToString(),
                    Phone = dtclient.Rows[0]["ManagerContactNo"].ToString(),
                    password = dtclient.Rows[0]["Password"].ToString(),
                    CountryId = string.IsNullOrEmpty(dtclient.Rows[0]["CountryId"].ToString()) ? 1 : Convert.ToInt32(dtclient.Rows[0]["CountryId"].ToString()),
                };
                JavaScriptSerializer javascriptSerializer = new JavaScriptSerializer();
                string postData = javascriptSerializer.Serialize(addClientModel);
                //postData="{\"ClientCode\":\"MOb0916243\",\"ClientName\":\"MObiMove\",\"ContactPersonName\":\"Saurabh Aggarwal\",\"ClientEmailId\":\"sourabh.agg2009@gmail.com\",\"Phone\":\"9602904048\",\"password\":\"Admin@123\",\"CountryId\":1}";
                RestClient restClient = new RestClient(Constant.MobiMoveSchool + "school/Register", HttpVerb.POST, postData, 1);
                restClient.MakeRequest();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int UpdateClientProfile()
        {
            clientMngr1 = new sp_InsertClientManager1TableAdapter();
            try
            {
                return Convert.ToInt32(clientMngr1.UpdateClientProfile1(_ClientId, _FirstN, _LastN, _Designation, _NoOfEmployees, _TypeOfIndustry, _EmpId, _EmailId, _MobileNo, _Address, Convert.ToInt32(_LoggedBy)).ToString());
            }
            catch
            {
                return 0;
            }
        }
        //public void GenerateProductKey(int ClientId, int UserId)
        //{
        //    clienttbl = new tblClientTableAdapter();
        //    string ProductKey = GenPass("SubscriptionProductionKey");
        //    string ExpiryDate = currentDateTime.AddDays(Constant.addTrialDays).ToString(Constant.DateFormat);
        //    clienttbl.UpdateProductKey(ClientId, ProductKey, ExpiryDate, 1, 1);

        //    AddUser(UserId, ClientId, ProductKey, ExpiryDate);
        //    Subscribe = new SubscribeBAL();
        //    Subscribe.CategoryIdList = Constant.CategoryIds;
        //    Subscribe.ClientId = ClientId;
        //    Subscribe.NoOfLicense = 10;
        //    Subscribe.Duration = Constant.addTrialDays;
        //    Subscribe.ProductKey = ProductKey;
        //    Subscribe.TotalAmount = 0;
        //    Subscribe.TotalPaid = 0;
        //    Subscribe.UserId = UserId;
        //    Subscribe.EmailId = _EmailId;
        //    Subscribe.Address = _Address;
        //    foreach (string obj in Constant.CategoryIds.Split(','))
        //    {
        //        Subscribe.categoryDuration += Constant.addTrialDays + ",";
        //        Subscribe.categoryNoOfLicense += 10 + ",";
        //        Subscribe.CategoryTotalAmount += 0.00 + ",";
        //        Subscribe.PricePerUnit += 0.00 + ",";
        //    }
        //    Subscribe.categoryDuration = Subscribe.categoryDuration.TrimEnd(',');
        //    Subscribe.categoryNoOfLicense = Subscribe.categoryNoOfLicense.TrimEnd(',');
        //    Subscribe.CategoryTotalAmount = Subscribe.CategoryTotalAmount.TrimEnd(',');
        //    Subscribe.PricePerUnit = Subscribe.PricePerUnit.TrimEnd(',');
        //    int res = Subscribe.InsertSubscriptionDtls();
        //    if (res > 0)
        //    {
        //        clienttbl.ChkProductKeybysub3(_ClientId, ProductKey);
        //    }

        //}
        public void UpdateProductKey(int ClientId, out string ProductKey)
        {
            clienttbl = new tblClientTableAdapter();
            ProductKey = GenPass("SubscriptionProductionKey");
            string ExpiryDate = currentDateTime.AddDays(Constant.addTrialDays).ToString(Constant.DateFormat);
            clienttbl.UpdateProductKey(ClientId, ProductKey, ExpiryDate, 1, 1);
        }
        public int ChkProductKeybysub3(int ClientId, string ProductKey)
        {
            clienttbl = new tblClientTableAdapter();
            int res = Convert.ToInt32(clienttbl.ChkProductKeybysub3(_ClientId, ProductKey));
            return res;
        }
        private string GenPass(string Uname)
        {
            Random myrandom = new Random();
            int Size = 40;
            string input = "abcdefg01234hijklmnopqrstuvwxyz0123456789ABCDEFGHghijklmnoIJKLMNOPQRSTUVWXYZ98765NOPQRS43210";
            string pass = "";
            try
            {

                StringBuilder builder = new StringBuilder();
                char ch;
                for (int i = 0; i < Size; i++)
                {
                    ch = input[myrandom.Next(0, input.Length)];
                    builder.Append(ch);
                }
                pass = builder.ToString();
            }
            catch (Exception)
            {
                int myNum = myrandom.Next(1000, 10000);
                pass = "btyutyuuyerewvb" + myNum + "yhdguyf54564gd";
            }

            return pass;
        }
        public int ChkProductKey()
        {
            clienttbl = new tblClientTableAdapter();
            int res = Convert.ToInt32(clienttbl.ChkProductKeybysub1(_ClientId, ProductKey).ToString());
            return res;

        }

        public void AddUser(int UserId, int ClientId, string productKey, string ExpiryDate)
        {
            try
            {
                usrbal = new UserBAL();
                lgn = new LoginBAL();
                dt = new DataTable();
                usrbal.UserId = UserId;
                dt = usrbal.GetUserDtlByUserId();
                if (dt.Rows.Count > 0)
                {
                    lgn.UserID = UserId;
                    lgn.ClientId = ClientId;
                    lgn.RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"].ToString());
                    lgn.DeptId = Convert.ToInt32(dt.Rows[0]["DeptId"].ToString());
                    lgn.UserName = dt.Rows[0]["UserName"].ToString();
                    lgn.EmailId = dt.Rows[0]["EmailId"].ToString();
                    lgn.LoginKey = GenPass("ghdsjgfgdshjkg");
                    lgn.currentDateTime = DateTime.UtcNow.AddMinutes(Constant.addMinutes);
                    lgn.InsertFirstLoginData();
                    if (lgn.RoleId == 2)
                    {
                        sendMailBal = new SendMailBAL();
                        sendMailBal.UserRegister(lgn.UserName, 1, lgn.EmailId, lgn.LoginKey, productKey, ExpiryDate, ClientId, lgn.RoleId);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public int ActualCal(int clientId, int UserId, int ActualId)
        {
            clienttbl = new tblClientTableAdapter();
            return Convert.ToInt32(clienttbl.ActualCalculation(ActualId, clientId, UserId));
        }

        public DataTable updateheaderlogo()
        {
            clienttbl = new tblClientTableAdapter();
            try
            {
                return clienttbl.updateheaderlogo(_ClientId, _LogoFilepath);
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetAllStates()
        {
            state = new tblStateTableAdapter();
            return state.GetAllStates();
        }
        public DataTable GetClientCustomApp()
        {
            GetClientCustomAppListTableAdapter getclientcutomapp = new GetClientCustomAppListTableAdapter();
            return getclientcutomapp.GetClientCustomAppList(UserId, ClientId);
        }
        public DataTable GetMobioceanAppTypes()
        {
            clienttbl = new tblClientTableAdapter();
            return clienttbl.GetMobioceanAppTypes(ClientId);
        }
        public int AssignCustomAppToClient()
        {
            clienttbl = new tblClientTableAdapter();
            return Convert.ToInt32(clienttbl.AssignCustomAppToClient(AppTypeIdList, ClientId));
        }
    }
    public class AddClientModel
    {

        public string ClientCode { get; set; }

        public string ClientName { get; set; }

        public string ContactPersonName { get; set; }

        public string ClientEmailId { get; set; }

        public string Phone { get; set; }

        public int CountryId { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public string AreaName { get; set; }

        public int IsSchool { get; set; }
        public string password { get; set; }
    }
}
