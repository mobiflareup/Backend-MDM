
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MobiOcean.MDM.Web
{
    public partial class DecodeSms : System.Web.UI.Page
    {
        string CalledFrmPh, msgBody;
        RstDateTime cons;
        MDMSrvcRef mdmsrvcref;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CalledFrmPh = Request.QueryString["msisdn"];
                msgBody = Request.QueryString["sms"];
                ParsePayload(CalledFrmPh, msgBody);
                Response.Write("OK");
            }
            catch (Exception)
            {
                Response.Write("Fail");
            }
        }


        protected void ParsePayload(string FrmPH, string msgBody)
        {
            if (FrmPH.Length == 12)
            {
                FrmPH = FrmPH.Substring(2);
            }
            try
            {

                while (msgBody.IndexOf("  ") > -1)
                {
                    msgBody = msgBody.Replace("  ", " ");
                }


                string payLoad = msgBody;

                #region----- Msg Parsing --------------
                if (payLoad.IndexOf(" RT ") >= 0)
                {
                    #region------ Parse Reset Date Time Msg-----------
                    //----- GBox set as GCNPC0001 RT 1 1234567890
                    cons = new RstDateTime();
                    payLoad = payLoad.Substring(payLoad.IndexOf(" RT ") + 4);
                    int countryId = Convert.ToInt32(payLoad.Substring(0, 1).Trim());
                    cons.countryId = countryId;
                    payLoad = payLoad.Substring((payLoad.IndexOf(" ") + 1)).Trim();
                    string MobNo = payLoad.Substring(0, payLoad.IndexOf("")).Trim();
                    cons.StuMob = payLoad;


                    try
                    {
                        mdmsrvcref = new MDMSrvcRef();
                        mdmsrvcref.ResetDate(cons);
                        
                    }
                    finally
                    {
                        mdmsrvcref = null;
                    }

                    #endregion
                }
                else if (payLoad.IndexOf(" SC ") >= 0)
                {
                    #region------ Parse Sim Change Msg-----------
                    //----- GBox set as GCNDC0001 SC AAP-70 1111111111
                    //SimChangeCntrlr objsimchnage=new SimChangeCntrlr();
                    DocBAL objdocvariable = new DocBAL();
                    payLoad = payLoad.Substring(payLoad.IndexOf(" SC ") + 4);

                    string AndroidAppId = payLoad.Substring(0, payLoad.IndexOf(" ")).Trim();
                    // objsimchnage.AndroidAppId = AndroidAppId;
                    objdocvariable.AppId = AndroidAppId;
                    string NewMobNo = FrmPH;
                    // objsimchnage.NewMobNo = NewMobNo;
                    string NewSimNo = payLoad.Substring((payLoad.IndexOf(" ") + 1)).Trim();
                    // objsimchnage.NewSimNo = NewSimNo;
                    objdocvariable.Remarks = NewSimNo + "$";
                    objdocvariable.dateTime = DateTime.UtcNow.AddMinutes(Constant.addMinutes).ToString("dd-MMM-yyyy HH:mm");
                    objdocvariable.functionalityId = 10;


                    try
                    {
                        mdmsrvcref = new MDMSrvcRef();
                        mdmsrvcref.MobileOnOffDecode(objdocvariable);
                        //  mdmsrvcref.SimChangeDecode(objsimchnage);
                    }
                    finally
                    {
                        mdmsrvcref = null;
                    }
                    #endregion
                }
                else if (payLoad.ToUpper().IndexOf(" MO ") >= 0)
                {

                    DocBAL objdocvariable = new DocBAL();
                    payLoad = payLoad.Substring(payLoad.IndexOf(" MO ") + 4);
                    string[] ary = payLoad.Split(' ');

                    string AndroidAppId = "", date_time = "", msg = "";

                    try
                    {
                        for (int idx = 0; idx < ary.Length; idx++)
                        {
                            if (idx == 0)
                            {
                                AndroidAppId = ary[idx];

                            }
                            else if (idx == 4 || idx == 3)
                            {
                                date_time = date_time + ary[idx] + " ";

                            }
                            else
                            {
                                msg = msg + ary[idx] + " ";
                            }
                        }
                        objdocvariable.AppId = AndroidAppId;
                        objdocvariable.dateTime = date_time;
                        objdocvariable.Remarks = msg;
                        objdocvariable.functionalityId = 10;
                    }
                    catch (Exception) { }

                    try
                    {
                        mdmsrvcref = new MDMSrvcRef();
                        
                        mdmsrvcref.MobileOnOffDecode(objdocvariable);
                    }
                    finally
                    {
                        mdmsrvcref = null;
                    }
                }
                #region----LTCollegeVisit----
                //GBox set as LT <appId>,<Latitude>,<Longitude>,<CellId>,<LAC>,<MCC>,<MNC>,<IsOutTime>,<CollegeId>,<Forgot>,<LogDateTime>,<Location>
                //GBox set as LT MDM-30391,13.0823353,80.2754473,0,0,0,0,0,160,0,21-Nov-2017 12:35
                //GBox set as LT MDM-30391,13.0823353,80.2754473,0,0,0,0,1,160,0,21-Nov-2017 12:35,vadapalani
                else if (payLoad.ToUpper().IndexOf(" LT ") >= 0)
                {
                    string location = "";
                    double lat = 0.0, longi = 0.0, cellId = 0.0, lac = 0.0, mnc = 0.0, mcc = 0.0;
                    LTCollegeVisitBAL obj = new LTCollegeVisitBAL();
                    payLoad = payLoad.Substring(payLoad.IndexOf(" LT ") + 4);
                    string[] ary = payLoad.Split(',');
                    try
                    {
                        obj.appId = ary[0];
                        try
                        {
                            if (!string.IsNullOrEmpty(ary[1]))
                            {
                                lat = Convert.ToDouble(ary[1]);
                                obj.Latitude = lat.ToString();
                            }
                        }
                        catch (Exception)
                        {
                            obj.Latitude = "";
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(ary[2]))
                            {
                                longi = Convert.ToDouble(ary[2]);
                                obj.Longitude = longi.ToString();
                            }
                        }
                        catch (Exception)
                        {
                            obj.Longitude = "";
                        }
                        try
                        {
                            cellId = Convert.ToDouble(ary[3]);
                            obj.CellId = Convert.ToInt32(cellId);
                        }
                        catch (Exception)
                        {
                            obj.CellId = Convert.ToInt32(cellId);
                        }
                        try
                        {
                            lac = Convert.ToDouble(ary[4]);
                            obj.LAC = Convert.ToInt32(lac);
                        }
                        catch (Exception)
                        {
                            obj.LAC = Convert.ToInt32(lac);
                        }
                        try
                        {
                            mcc = Convert.ToDouble(ary[5]);
                            obj.MCC = Convert.ToInt32(mcc);
                        }
                        catch (Exception)
                        {
                            obj.MCC = Convert.ToInt32(mcc);
                        }
                        try
                        {
                            mnc = Convert.ToDouble(ary[6]);
                            obj.MNC = Convert.ToInt32(mnc);
                        }
                        catch (Exception)
                        {
                            obj.MNC = Convert.ToInt32(mnc);
                        }
                        obj.IsOutTime = Convert.ToInt32(ary[7]);
                        obj.CollegeId = Convert.ToInt32(ary[8]);
                        obj.Forgot = Convert.ToInt32(ary[9]);
                        obj.LogDateTime = ary[10];
                        try
                        {
                            if (!string.IsNullOrEmpty(ary[11]))
                            {
                                for (int idx = 11; idx < ary.Length; idx++)
                                {
                                    location = location + ary[idx] + ", ";
                                }
                                location = location.Substring(0, location.Length - 2);
                            }
                            obj.Location = location;
                        }
                        catch (Exception ex)
                        {
                            obj.Location = location;
                        }
                    }
                    catch (Exception ex) { }

                    try
                    {
                        mdmsrvcref = new MDMSrvcRef();

                        mdmsrvcref.LTCollegeVisit(obj);
                    }
                    finally
                    {
                        mdmsrvcref = null;
                    }
                }
                #endregion
                //else if (payLoad.IndexOf(" GL") >= 0)
                //{
                //    #region------ Location -----------
                //    objlocation = new Location();
                //    string Time, cellId1 = "", locationAreaCode1 = "", mobileCountryCode1 = "", mobileNetworkCode1 = "";
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" GL") + 3);

                //    int LocReq = Convert.ToInt32(payLoad.Substring(0, payLoad.IndexOf(" ")).Trim());
                //    objlocation.LocReq = LocReq;
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //    string AndroidAppId = payLoad.Substring(0, (payLoad.IndexOf(" "))).Trim();
                //    objlocation.AndroidAppId = AndroidAppId;
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);

                //    string LocSource = "";


                //    if (payLoad.IndexOf("~") > 0)
                //    {
                //        LocSource = payLoad.Substring(0, (payLoad.IndexOf("~"))).Trim();
                //        objlocation.LocSource = LocSource;
                //        payLoad = payLoad.Substring(payLoad.IndexOf("~") + 2);
                //    }
                //    else
                //    {
                //        LocSource = payLoad.Substring(0, (payLoad.IndexOf(" "))).Trim();
                //        objlocation.LocSource = LocSource;
                //        payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //    }


                //    int isWithInGeoFence = 2;
                //    try
                //    {
                //        isWithInGeoFence = Convert.ToInt32(payLoad.Substring(0, payLoad.IndexOf(" ")).Trim());
                //        objlocation.IsWithInGeoFence = isWithInGeoFence;
                //        payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //    }
                //    catch (Exception ex) { }

                //    string Lat = payLoad.Substring(0, (payLoad.IndexOf(" "))).Trim();
                //    objlocation.Lat = Lat;
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //    string Lng = payLoad.Substring(0, (payLoad.IndexOf(" "))).Trim();
                //    objlocation.Long = Lng;
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //    //string date = payLoad.Trim();
                //    string date = payLoad.Substring(0, (payLoad.IndexOf(" "))).Trim();
                //    objlocation.Date = date;
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //    if (payLoad.IndexOf(" ") == -1)
                //    {
                //        Time = payLoad.Trim();
                //    }
                //    else
                //    {
                //        Time = payLoad.Substring(0, (payLoad.IndexOf(" "))).Trim();

                //        payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //        cellId1 = payLoad.Substring(0, (payLoad.IndexOf(" "))).Trim();
                //        objlocation.cellId1 = cellId1;
                //        payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //        locationAreaCode1 = payLoad.Substring(0, (payLoad.IndexOf(" "))).Trim();
                //        objlocation.locationAreaCode1 = locationAreaCode1;
                //        payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //        mobileCountryCode1 = payLoad.Substring(0, (payLoad.IndexOf(" "))).Trim();
                //        objlocation.mobileCountryCode1 = mobileCountryCode1;
                //        payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //        mobileCountryCode1 = payLoad.Trim();
                //        objlocation.mobileNetworkCode1 = mobileCountryCode1;

                //        }

                //    #endregion


                //    try
                //    {
                //        mdmsrvcref = new MDMSrvcRef();
                //       // mdmsrvcref.GingerboxDemodecoding1();
                //        mdmsrvcref.LocationDecodeSms(objlocation);
                //    }
                //    finally
                //    {
                //        mdmsrvcref = null;
                //    }
                //}

                //else if (payLoad.IndexOf(" AL ") >= 0)
                //{
                //    #region------ Parse -----------
                //    objappstatus = new ApplicationStatus();
                //    string AppStatus = "1";
                //    objappstatus.AppStatus = AppStatus;
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" AL ") + 4);

                //    string AndroidAppId = payLoad.Substring(0, payLoad.IndexOf(" ")).Trim();
                //    objappstatus.AndroidAppId = AndroidAppId;
                //    string date = payLoad.Substring((payLoad.IndexOf(" ") + 1)).Trim();
                //    objappstatus.Date = date;
                //    #endregion

                //    try
                //    {
                //        mdmsrvcref = new MDMSrvcRef();
                //        //mdmsrvcref.GingerboxDemodecoding1();
                //        mdmsrvcref.AppStatusDecode(objappstatus);
                //    }
                //    finally
                //    {
                //        mdmsrvcref = null;
                //    }
                //}


                //else if (payLoad.IndexOf(" AA ") >= 0)
                //{
                //    #region------ Parse Add Application (installed in phone) -----------

                //    ChatAppList objchatapp = new ChatAppList();

                //    List<ChatAppLst> chtapplst = new List<ChatAppLst>();
                //    //List<ChatAppList> chtapplst = new List<ChatAppList>();
                //     ChatAppLst chapp = new ChatAppLst
                //     {
                //         ChatApp = objchatapp.ToString(),
                //         AppIndx = Convert.ToInt32(objchatapp),

                //      };
                //     chtapplst.Add(chapp);
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" AA ") + 4);

                //    string SimNo = payLoad.Substring(0, payLoad.IndexOf(" ")).Trim();
                //    objchatapp.Sim_No = SimNo;
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //    string appIndex = (payLoad.Substring(0, (payLoad.IndexOf(" "))));
                //    chapp.AppIndx = int.Parse(appIndex);
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //    string AppName = payLoad;
                //    chapp.ChatApp = AppName;
                //    string Seq = "L";

                //    #endregion

                //    try
                //    {
                //        mdmsrvcref = new MDMSrvcRef();
                //        mdmsrvcref.InstallAppDecode(objchatapp);
                //    }
                //    finally
                //    {
                //        mdmsrvcref = null;
                //    }
                //}

                //else if (payLoad.IndexOf(" RA ") >= 0)
                //{
                //    #region------ Parse Remove Application (installed in phone) -----------
                //    AppUnistallStatus objuninstall = new AppUnistallStatus();

                //    payLoad = payLoad.Substring(payLoad.IndexOf(" RA ") + 4);

                //    string appIndex = payLoad.Substring(0, (payLoad.IndexOf(" ")));
                //    objuninstall.appIdx = Convert.ToInt32(appIndex);
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //    int StuId = Convert.ToInt32(payLoad);
                //    objuninstall.Stdnt_Id = StuId;

                //    #endregion


                //    try
                //    {
                //        mdmsrvcref = new MDMSrvcRef();
                //        mdmsrvcref.UninstallAppDecode(objuninstall);
                //    }
                //    finally
                //    {
                //        mdmsrvcref = null;
                //    }
                //}

                //else if (payLoad.ToLower().IndexOf("location") >= 0)
                //{
                //    #region------ Parse Get Junior/child location -----------


                //    payLoad = payLoad.Trim();
                //    payLoad = payLoad.Substring(payLoad.IndexOf("location")).Trim();
                //    string JuniorMob = payLoad.Substring(payLoad.IndexOf(" ") + 1).Trim();
                //    string SeniorMob = FrmPH;
                //    string Gp = "1";
                //    #endregion

                //    try
                //    {
                //        mdmsrvcref = new MDMSrvcRef();
                //        mdmsrvcref.GingerboxDemodecoding1();
                //    }
                //    finally
                //    {
                //        mdmsrvcref = null;
                //    }
                //}
                //else if (payLoad.ToUpper().IndexOf(" LM ") >= 0)
                //{

                //    DocVariable20 objdocvariable20 = new DocVariable20();
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" LM ") + 4);
                //    string[] ary = payLoad.Split(' ');

                //    string AndroidAppId = "", date_time = "", msg = "";

                //    try
                //    {
                //        for (int idx = 0; idx < ary.Length; idx++)
                //        {
                //            if (idx == 0)
                //            {
                //                AndroidAppId = ary[idx];
                //                objdocvariable20.AndroidAppId = AndroidAppId;
                //            }
                //            else if (idx == 1 || idx == 2)
                //            {
                //                date_time = date_time + ary[idx] + " ";
                //                objdocvariable20.dateTime = date_time;
                //            }
                //            else
                //            {
                //                msg = msg + ary[idx] + " ";

                //            }
                //        }
                //    }
                //    catch (Exception ex) { }

                //    try
                //    {
                //        mdmsrvcref = new MDMSrvcRef();
                //       // mdmsrvcref.GingerboxDemodecoding1();
                //        mdmsrvcref.LMDecode(objdocvariable20); 
                //    }
                //    finally
                //    {
                //        mdmsrvcref = null;
                //    }
                //}
                //else if (payLoad.ToUpper().IndexOf(" GS ") >= 0)
                //{

                //    GPSReport objgpsrpt = new GPSReport();
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" GS ") + 4);
                //    string[] ary = payLoad.Split(' ');

                //    string AndroidAppId = "", date_time = "", msg = "";

                //    try
                //    {
                //        for (int idx = 0; idx < ary.Length; idx++)
                //        {
                //            if (idx == 0)
                //            {
                //                AndroidAppId = ary[idx];
                //                objgpsrpt.AndroidAppId = AndroidAppId;
                //            }
                //            else if (idx == 2)
                //            {
                //                date_time = date_time + ary[idx] + " ";
                //                objgpsrpt.DateTime = date_time;
                //            }
                //            else if (idx == 3)
                //            {
                //                date_time = date_time + ary[idx] + "";
                //                objgpsrpt.DateTime = date_time;
                //            }
                //            else
                //            {
                //                msg = msg + ary[idx] + " ";
                //            }
                //        }
                //    }
                //    catch (Exception ex) { }

                //    try
                //    {
                //        mdmsrvcref = new MDMSrvcRef();
                //        mdmsrvcref.GPSStatusDecode(objgpsrpt);
                //    }
                //    finally
                //    {
                //        mdmsrvcref = null;
                //    }
                //}
                //else if (payLoad.ToUpper().IndexOf(" VN ") >= 0)
                //{
                //    string AndroidAppId = "", date_time = "", msg = "";
                //    VrsnReport objvrsnrpt = new VrsnReport();
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" VN ") + 4);
                //    if (payLoad.ToUpper().IndexOf(" GR ") == -1)
                //    {
                //        string[] ary = payLoad.Split(' ');



                //        try
                //        {
                //            for (int idx = 0; idx < ary.Length; idx++)
                //            {
                //                if (idx == 0)
                //                {
                //                    AndroidAppId = ary[idx];
                //                    objvrsnrpt.AndroidAppId = AndroidAppId;
                //                }
                //                else if (idx == 2)
                //                {
                //                    date_time = date_time + ary[idx] + " ";
                //                    objvrsnrpt.DateTime = date_time;
                //                }
                //                else if (idx == 3)
                //                {
                //                    date_time = date_time + ary[idx] + "";
                //                    objvrsnrpt.DateTime = date_time;
                //                }
                //                else
                //                {
                //                    msg = msg + ary[idx] + " ";
                //                }
                //            }
                //        }

                //        catch (Exception ex) { }
                //    }
                //    else
                //    {

                //        try
                //        {
                //            AndroidAppId = payLoad.Substring(0, payLoad.IndexOf(" ")).Trim();
                //            payLoad = payLoad.Substring(payLoad.IndexOf(" ") + 1);
                //            msg = payLoad.Substring(0, payLoad.IndexOf("~")).Trim();
                //            payLoad = payLoad.Substring(payLoad.IndexOf("~") + 2);
                //            date_time = payLoad.Substring(0);

                //        }
                //        catch (Exception ex) { }


                //    }

                //    try
                //    {
                //        mdmsrvcref = new MDMSrvcRef();
                //        mdmsrvcref.AppVersionNoDecode(objvrsnrpt);
                //    }
                //    finally
                //    {
                //        mdmsrvcref = null;
                //    }
                //}

                //else if (payLoad.ToUpper().IndexOf(" LI ") >= 0)
                //{

                //    DocVariable19 objdocdovariableli = new DocVariable19();
                //    payLoad = payLoad.Substring(payLoad.IndexOf(" LI ") + 4);
                //    string[] ary = payLoad.Split(' ');

                //    string AndroidAppId = "", date_time = "", msg = "", Lat = "", Lng = "";
                //    int functionalityId = 0;

                //    try
                //    {
                //        for (int idx = 0; idx < ary.Length; idx++)
                //        {
                //            if (idx == 0)
                //            {
                //                AndroidAppId = ary[idx];
                //                objdocdovariableli.AndroidAppId = AndroidAppId;
                //            }
                //            else if (idx == 1)
                //            {
                //                Lat = ary[idx];
                //                objdocdovariableli.Lat = Lat;
                //                // date_time = date_time + ary[idx] + " ";
                //            }
                //            else if (idx == 2)
                //            {
                //                Lng = ary[idx];
                //                objdocdovariableli.Long = Lng;
                //                // date_time = date_time + ary[idx] + " ";
                //            }
                //            else if (idx == 4 || idx == 3)
                //            {
                //                date_time = date_time + ary[idx] + " ";
                //                objdocdovariableli.dateTime = date_time;
                //            }
                //            else if (idx == 5)
                //            {
                //                functionalityId = Convert.ToInt32(ary[idx]);
                //                objdocdovariableli.functionalityId = functionalityId;
                //            }
                //            else
                //            {
                //                msg = msg + ary[idx] + "";
                //            }
                //        }
                //    }
                //    catch (Exception ex) { }

                //    try
                //    {
                //        mdmsrvcref = new MDMSrvcRef();
                //        //mdmsrvcref.GingerboxDemodecoding1();

                //        mdmsrvcref.LIDecode(objdocdovariableli);
                //    }
                //    finally
                //    {
                //        mdmsrvcref = null;
                //    }
                //}
                //else if (payLoad.ToUpper().IndexOf(" IS ") >= 0)
                //{
                //}
                //else if (payLoad.ToUpper().IndexOf(" OI ") >= 0)
                //{
                //}


                #endregion
            }
            finally
            {

            }

        }
    }
}
