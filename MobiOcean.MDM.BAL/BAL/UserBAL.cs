using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.UserDALTableAdapters;
using MobiOcean.MDM.Infrastructure;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
/// <summary>
/// Summary description for UserBAL
/// </summary>
/// 

namespace MobiOcean.MDM.BAL.BAL
{
    public class UserBAL
    {

        tblUserDeviceTableAdapter tblUserDevice;
        tblUserTableAdapter tblUserAdapter;
        tblDeviceInfoTableAdapter tbldeviceinfo;



        public int RoleId { get; set; }
        public int DeviceId { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public string EmpCompanyId { get; set; }
        public string MobileNo { get; set; }
        public string APPId { get; set; }
        public string ClientCode { get; set; }


        public UserBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataTable GetUserByClientId()
        {
            tblUserAdapter = new tblUserTableAdapter();
            return tblUserAdapter.GetUserByClientId(ClientId);
        }
        public DataTable GetDeviceRegisterDetail()
        {
            tblUserDevice = new tblUserDeviceTableAdapter();
            return tblUserDevice.GetDeviceRegisterDetail(ClientId);
        }
        public DataTable GetBarnchNameAndNoOfUsers()
        {
            tblUserDevice = new tblUserDeviceTableAdapter();
            return tblUserDevice.GetBranchNameandCount(ClientId);
        }
        public DataTable GetDepartmentNameAndNoOfUsers()
        {
            tblUserDevice = new tblUserDeviceTableAdapter();
            return tblUserDevice.GetDepartmentNameAndNoOfUsers(ClientId);
        }

        public DataTable GetProfileNameAndNoOfUsers()
        {
            tblUserDevice = new tblUserDeviceTableAdapter();
            return tblUserDevice.GetProfileNameandCount(ClientId);
        }

        public DataTable GetUserDtlByUserId()
        {
            tblUserAdapter = new tblUserTableAdapter();
            return tblUserAdapter.GetUserDtlByUserId(UserId);
        }
        public DataTable GetProfileName()
        {
            tblUserAdapter = new tblUserTableAdapter();
            return tblUserAdapter.GetProfileByUserId(UserId);
        }

        public DataTable GetUserByRoleId()
        {
            tblUserAdapter = new tblUserTableAdapter();
            return tblUserAdapter.GetUserByRoleId(ClientId, RoleId);
        }
        public DataTable GetRptngManagerByUserId()
        {
            tblUserAdapter = new tblUserTableAdapter();
            return tblUserAdapter.GetRptngManagerByUserId(UserId);
        }

        public string ChkUser()
        {
            try
            {
                string AppId = "";
                tblUserDevice = new tblUserDeviceTableAdapter();
                DataTable dtUserDevice = tblUserDevice.GetAllUserDevice(MobileNo);
                if (dtUserDevice != null && dtUserDevice.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtUserDevice.Rows[0]["ClientId"].ToString()) == 244 && string.IsNullOrEmpty(dtUserDevice.Rows[0]["MobileNo2"].ToString()))
                    {
                        try
                        {
                            DataTable dtEngnr = new DataTable();
                            //Call API and get engineerID
                            dynamic Restdata = GetUpdateUserDetails(dtUserDevice.Rows[0]["MobileNo1"].ToString());
                            foreach (var data in Restdata)
                            {
                                foreach (var data_detail in data.Detail)
                                {
                                    int EnggId = data_detail.engrid;
                                    if (EnggId > 0)
                                    {
                                        tblUserAdapter = new tblUserTableAdapter();
                                        tblUserAdapter.UpdateGenusEngineerID(Convert.ToInt32(dtUserDevice.Rows[0]["UserId"].ToString()), EnggId.ToString());
                                        break;
                                    }
                                }

                            }
                        }
                        catch (Exception ex) { }
                    }
                }
                else
                {
                    DataTable dtEngnr = new DataTable();
                    //Call API and get engineerID
                    dynamic Restdata = GetUpdateUserDetails(MobileNo);
                    foreach (var data in Restdata)
                    {
                        foreach (var data_detail in data.Detail)
                        {
                            string EnggId = data_detail.engrid;
                            string EnggName = data_detail.engrname;
                            string EnggEmail = data_detail.engremail;
                            SaveUserAndDevice(244, EnggId, EnggName, MobileNo, "1", EnggEmail);
                        }
                    }
                }
                AppId = tblUserDevice.ChkUserNew(ClientCode, EmpCompanyId, MobileNo).ToString();
                return AppId;
            }
            catch (Exception)
            {
                return "0";
            }

        }
        public dynamic GetUpdateUserDetails(string MobileNo)
        {
            try
            {
                if (MobileNo != "")
                {
                    var client = new RestClient(Constant.GenusAPI + "GetEngineerDetail/?MobileNo=" + MobileNo, HttpVerb.GET);
                    string json = client.MakeRequest();
                    dynamic testObj = JArray.Parse(JsonConvert.DeserializeObject(json).ToString());
                    foreach (var data in testObj)
                    {
                        foreach (var data_detail in data.Detail)
                        {
                            int EnggId = data_detail.engrid;
                            if (EnggId > 0)
                                return testObj;
                            //tblUserAdapter = new tblUserTableAdapter();
                            //tblUserAdapter.UpdateGenusEngineerID(Convert.ToInt32(dtUserDevice.Rows[0]["UserId"].ToString()), dtEngnr.Rows[0]["EngnrID"].ToString());
                        }
                    }
                    return "Error";
                    //return "fail";

                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception)
            {
                return "Error";
            }
        }
        protected string SaveUserAndDevice(int ClientId, string EngnrID, string UserName, string MobileNo, string CountryId, string EmailId)
        {

            usrBAL user = new usrBAL();
            user.UserId = 0;
            user.ClientId = ClientId;
            user.UserCode = EngnrID;
            user.UserName = UserName;
            user.MobileNoList = MobileNo;
            user.MobileNo = MobileNo;
            user.CountryId = "1";
            user.EmailId = string.IsNullOrEmpty(EmailId) ? UserName.Replace(" ", "") + EngnrID + "@genus.in" : EmailId;
            user.DeptId = 10131;//Genus Dept
            user.RoleId = 4;//User
            user.EmpCompanyId = EngnrID;
            //user.Branch = Convert.ToInt32(dtBranch.SelectedValue.ToString());
            //user.Designation = Designation;
            user.Password = "Genus@" + MobileNo;
            user.RptMngrId = 10474;//
            user.DeviceOwnerShip = 1;
            user.Country = "1";
            user.LoggedBy = "1";
            DataTable dtUser = new DataTable();
            dtUser = user.InsertUserWithMultipleDevice();
            if (int.Parse(dtUser.Rows[0]["UserId"].ToString()) == -1)
            {
                return "-1";
            }
            else if (int.Parse(dtUser.Rows[0]["UserId"].ToString()) == 0)
            {
                return "0";
            }
            else
            {
                LoginBAL lgn = new LoginBAL();
                lgn.ClientId = ClientId;
                lgn.UserID = int.Parse(dtUser.Rows[0]["UserId"].ToString());
                lgn.RoleId = user.RoleId;
                lgn.DeptId = user.DeptId;
                lgn.UserName = user.UserName;
                lgn.LoginKey = GenPass(28, "");// GenRandomPassword("LoginKey",28);
                lgn.currentDateTime = DateTime.UtcNow.AddMinutes(330);
                lgn.InsertFirstLoginData();
                ClientBAL clientbal = new ClientBAL();
                DataTable dtclient = new DataTable();
                clientbal.ClientId = user.ClientId;
                dtclient = clientbal.GetClientByClientId();
                //if (UserRoleId <= 3 || Convert.ToInt32(dtclient.Rows[0]["IsPasswordVisible"].ToString()) > 0)
                //{
                //    SendMailBAL mail = new SendMailBAL();
                //    mail.UserRegister(lgn.UserName, 1, EmailId, lgn.LoginKey, dtclient.Rows[0]["ProductKey"].ToString(), dtclient.Rows[0]["ExpiryDate"].ToString(), ClientId, RoleId);
                //}
                //string[] MobileNos = MobileNoList.Split(',');
                //string[] CountryIds = CountryIdList.Split(',');
                string downloadlink = Constant.CommonAppUrl;//: Constant.AppDownloadUrl;
                SendSMSBAL sms = new SendSMSBAL();

                user = new usrBAL();
                user.DeviceId = 0;
                user.UserId = lgn.UserID;
                user.DeviceName = MobileNo;
                user.MobileNo1 = MobileNo;
                user.CountryId = "1";
                user.ClientId = ClientId;
                user.DeviceOwnerShip = 1;
                int DeviceId = Convert.ToInt32(user.InsertUserDeviceDataRaj1());
                if (DeviceId > 0)
                {
                    sms.sendMsgUsingSMS("Dear " + user.UserName + ", You have been registered on MobiOcean. Please use the below URL to download the MobiOcean APP: " + downloadlink + " . After downloading, please use the below info to activate the APP: Mobile No:" + MobileNo + " ", MobileNo, ClientId);//Client Code:" + dtclient.Rows[0]["ClientCode"].ToString() + "   User Code: " + UserCode + "
                }
                tblUserAdapter = new tblUserTableAdapter();
                tblUserAdapter.UpdateGenusEngineerID(Convert.ToInt32(dtUser.Rows[0]["UserId"].ToString()), EngnrID.ToString());
                return "1";
            }
        }
        public string GenPass(int Size = 6, string input = "")
        {
            Random myrandom = new Random();
            if (input == "")
                input = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ9876543210";
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
                int myNum = myrandom.Next(10000000, 100000000);
                pass = "btyutyuuyerewvb" + myNum + "yhdguyfgd";
            }
            return pass;
        }

        public string UpdateOTP(string OTP, string mobileNo)
        {
            tblUserDevice = new tblUserDeviceTableAdapter();
            return tblUserDevice.UpdateOTP(OTP, mobileNo).ToString();
        }

        public string GetRemainngDaysOfExpiryPwd()
        {
            try
            {
                tblUserAdapter = new tblUserTableAdapter();
                return tblUserAdapter.GetRemainngDaysOfExpiry(UserId).ToString();
            }
            catch (Exception)
            {
                return "0";
            }

        }

        public DataTable GetUserDeviceInfoByDeviceId()
        {
            try
            {

                tbldeviceinfo = new tblDeviceInfoTableAdapter();
                DataTable dt = tbldeviceinfo.GetDeviceInfoByDeviceId(DeviceId);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string ChkSimChange(string info, int DeviceId)
        {
            this.DeviceId = DeviceId;
            DataTable dtDeviceInfo = GetUserDeviceInfoByDeviceId();
            string oldSimNo = "";
            if (dtDeviceInfo.Rows.Count > 0)
            {
                oldSimNo = dtDeviceInfo.Rows[0]["SIMNo1"].ToString();
            }
            string newSimNo = "";
            int i = info.IndexOf('$');
            string offDateTime = info.Substring(i + 1);
            if (i != -1)
            {
                newSimNo = info.Substring(0, i);
            }

            if ((newSimNo == "" || newSimNo == oldSimNo) && offDateTime != "")
            {
                return "He/She switched off mobile at " + offDateTime + " .";
            }
            else if (newSimNo != oldSimNo)
            {
                //if (i != -1)
                //    return "New Sim Inserted/Mobile was switch off at : " + b + "";
                //else
                return "He/She inserted new SIM."; ;
            }
            else
            {
                return "0";
            }



        }

        public DataTable GetMobileNoByUserIdRaj()
        {
            tblUserDevice = new tblUserDeviceTableAdapter();
            return tblUserDevice.GetDeviceByUserIdRaj(UserId);
        }
        public int DeleteUserDeviceDtlsMobNo()
        {
            try
            {
                tblUserDevice = new tblUserDeviceTableAdapter();
                return tblUserDevice.DeleteUserDeviceDtlsMobNo(ClientId, DeviceId);
            }
            catch { return 0; }
        }
        public DataTable getDeviceDtlByAppId(string APPId)
        {
            tblUserDevice = new tblUserDeviceTableAdapter();
            return tblUserDevice.GetUserDeviceByAppId(APPId);
        }
        public DataTable GetOsVersion()
        {
            tbldeviceinfo = new tblDeviceInfoTableAdapter();
            return tbldeviceinfo.GetOsVersion(ClientId);
        }
        public DataTable CheckApiForMapsLoad(int ClientId)
        {
            DataTable dt = new DataTable();
            try
            {
                ThirdPartyApiTableAdapter ta = new ThirdPartyApiTableAdapter();

                return ta.GetClientApi(ClientId);
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
    }
    public class OTPClass
    {
        tblUserDeviceTableAdapter tblUserDevice;
        public string MobileNo { get; set; }
        public string OTP { get; set; }
        public string ClientCode { get; set; }
        public string ChkIsStuResgistered()
        {
            try
            {
                tblUserDevice = new tblUserDeviceTableAdapter();
                string AppId = tblUserDevice.ChkOTP(OTP, MobileNo).ToString();
                return AppId;
            }
            catch (Exception)
            {
                return "0";
            }

        }
        public OTPResponse OTPVerification()
        {
            OTPResponse otpResponse = new OTPResponse();
            try
            {
                tblUserDevice = new tblUserDeviceTableAdapter();

                DataTable dt = tblUserDevice.OTPVerification(OTP, MobileNo);
                if (dt != null & dt.Rows.Count > 0)
                {

                    otpResponse.appID = dt.Rows[0]["AppId"].ToString();
                    otpResponse.EngnrID = dt.Rows[0]["EngnrID"].ToString();
                }
                else
                {
                    otpResponse.appID = "0";
                    otpResponse.EngnrID = "0";
                }
                return otpResponse;
            }
            catch (Exception)
            {
                otpResponse.appID = "0";
                otpResponse.EngnrID = "0";
                return otpResponse;
            }

        }
    }
    public class OTPResponse
    {
        public string appID { get; set; }
        public string EngnrID { get; set; }
    }
}

