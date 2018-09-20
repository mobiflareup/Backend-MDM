<%@ Page Title="Device Info" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="DeviceInfo.aspx.cs" Inherits="MobiOcean.MDM.Web.DeviceInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server"> 


    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active">

                    <div class="profile1">&nbsp;&nbsp;Device Info</div><br />
                     
                    <div class="panel panel-primary" style="border-color:#ea6161;">
                                <div class="panel-heading text-left" style="background:#ea6161;border-color:#ea6161;">
                                    
                                   
                                    <div class="col-lg-6">
                                <label style="text-align: left">
                                    User/ Device Name 
                                                 <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </label>
                            </div>
                                    
                                    <div class="clearfix"></div>
                                </div> 
                   <div id="errorMsg" class="panel-body" visible="false" runat="server">
                        <br />
                        <asp:Label ID="lblMsg" runat="server" Style="text-align:center"></asp:Label>
                          <br /> <br />
                    </div>
                         <asp:FormView ID="fvDeviceInfo" runat="server" Style="width: 100%;">
                                <ItemTemplate>
                                <div class="panel-body ">
                                     <div class="col-lg-12" style="text-align:right">
                                        <h4>Last Updation at : <%#Eval("LogDateTime") %></h4>
                                    </div>
                                     <div class="col-lg-12 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>User Info</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>User Name :</b> <%#(!string.IsNullOrEmpty(Eval("UserName").ToString())) ? Eval("UserName"):"N/A"%> </p>
                                                </div>
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>Mobile No :</b> <%#(!string.IsNullOrEmpty(Eval("MobileNo1").ToString())) ? Eval("MobileNo1"):"N/A"%></p>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                    <div class="col-lg-12 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Device Info</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>Device Name :</b> <%#(!string.IsNullOrEmpty(Eval("DeviceName").ToString())) ? Eval("DeviceName"):"N/A" %></p>
                                                    <p><b>Device Manufacturer :</b> <%#(!string.IsNullOrEmpty(Eval("DeviceMaker").ToString())) ? Eval("DeviceMaker"):"N/A" %></p>
                                                    <p><b>OS Version :</b> <%#(!string.IsNullOrEmpty(Eval("OSVersion").ToString()))? Eval("OSVersion"):"N/A"%></p>
                                                    <p><b>Front Camera :</b> <%#(!string.IsNullOrEmpty(Eval("FrontCamera").ToString())) ? Eval("FrontCamera"):"N/A" %></p>
                                                    <p><b>IMEINo 1 :</b> <%#(!string.IsNullOrEmpty(Eval("IMEINo1").ToString()) )? Eval("IMEINo1"):"N/A"%></p>
                                                    <p><b>Bluetooth :</b> <%#(!string.IsNullOrEmpty(Eval("Bluetooth").ToString()))? Eval("Bluetooth"):"N/A" %></p>
                                                    <p><b>Sensors :</b> <%#(!string.IsNullOrEmpty((Eval("Sensors").ToString()))) ? Format(Eval("Sensors").ToString()):"N/A" %></p>
                                                </div>
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>Device Model :</b> <%#(!string.IsNullOrEmpty(Eval("DeviceModel").ToString())) ? Eval("DeviceModel"):"N/A"%></p>
                                                    <p><b>Client Version MDM :</b> <%#(!string.IsNullOrEmpty(Eval("ClientVersionMDM").ToString())) ? Eval("ClientVersionMDM"):"N/A" %></p>
                                                    <p><b>Processor :</b> <%#(!string.IsNullOrEmpty(Eval("Processor").ToString())) ? Eval("Processor"):"N/A" %></p>
                                                    <p><b>Rear Camera :</b> <%#(!string.IsNullOrEmpty(Eval("RearCamera").ToString())) ? Eval("RearCamera"):"N/A" %></p>
                                                    <p><b>IMEINo 2 :</b> <%#(!string.IsNullOrEmpty(Eval("IMEINo2").ToString())) ? Eval("IMEINo2"):"N/A" %></p>
                                                    <p><b>NFC :</b> <%#(!string.IsNullOrEmpty(Eval("NFC").ToString())) ? Eval("NFC"):"N/A" %></p>
                                                <p><b>Device Rooted? : </b> <%#(!string.IsNullOrEmpty(Eval("IsDeviceRouted").ToString())) ? Eval("IsDeviceRouted").ToString()=="1"?"Yes":"No":"N/A" %></p>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                     <div class="col-lg-12 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Battery Info</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>Battery Percent :</b> <%#(!string.IsNullOrEmpty(Eval("BatteryPercent").ToString())) ? Eval("BatteryPercent"):"N/A" %></p>
                                                    <p><b>Battery Health :</b> <%#(!string.IsNullOrEmpty(Eval("BatteryHealth").ToString()) )? Eval("BatteryHealth"):"N/A"%></p>
                                                </div>
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>Battery Status :</b> <%#(!string.IsNullOrEmpty(Eval("BatteryStatus").ToString())) ? Eval("BatteryStatus"):"N/A"%></p>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                     <div class="col-lg-12 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Memory Info</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>Internal Memory :</b> <%#(!string.IsNullOrEmpty(Eval("InternalMemory").ToString())) ? Eval("InternalMemory"):"N/A" %></p>
                                                    <p><b>External SD Card :</b> <%#(!string.IsNullOrEmpty(Eval("IsExternalSDCard").ToString())) ? Eval("IsExternalSDCard"):"N/A" %></p>
                                                    <p><b>Available RAM Size :</b> <%#(!string.IsNullOrEmpty(Eval("AvailableRAMSize").ToString())) ? Eval("AvailableRAMSize"):"N/A" %></p>
                                                </div>
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>External Memory :</b> <%#(!string.IsNullOrEmpty(Eval("ExternalMemory").ToString())) ? Eval("ExternalMemory"):"N/A"%></p>
                                                    <p><b>RAM Size :</b> <%#(!string.IsNullOrEmpty(Eval("RAMSize").ToString())) ? Eval("RAMSize"):"N/A" %></p>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                     <div class="col-lg-12 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Operator Info</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>Telecom Provider 1 :</b> <%#(!string.IsNullOrEmpty(Eval("TelecomProvider1").ToString()) )? Eval("TelecomProvider1"):"N/A" %></p>
                                                </div>
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>Telecom Provider 2 :</b> <%#(!string.IsNullOrEmpty(Eval("TelecomProvider2").ToString())) ? Eval("TelecomProvider2"):"N/A" %></p>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                     <div class="col-lg-12 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>SIM Card Info</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>SIM No 1 :</b> <%#(!string.IsNullOrEmpty(Eval("SIMNo1").ToString())) ? Eval("SIMNo1"):"N/A"%></p>
                                                    <p><b>Dual SIM :</b> <%#(!string.IsNullOrEmpty(Eval("IsDualSIM").ToString())) ? Eval("IsDualSIM"):"N/A" %></p>
                                                    <p><b>SIM2 Ready :</b> <%#(!string.IsNullOrEmpty(Eval("IsSIM2Ready").ToString())) ? Eval("IsSIM2Ready"):"N/A" %></p>
                                                </div>
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>SIM No 2 :</b> <%#(!string.IsNullOrEmpty(Eval("SIMNo2").ToString())) ? Eval("SIMNo2"):"N/A" %></p>
                                                    <p><b>SIM1 Ready :</b> <%#(!string.IsNullOrEmpty(Eval("IsSIM1Ready").ToString())) ? Eval("IsSIM1Ready"):"N/A" %></p>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                     <div class="col-lg-12 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Network Info</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>Network Strength :</b> <%#(!string.IsNullOrEmpty(Eval("NetworkStrength").ToString())) ? Eval("NetworkStrength"):"N/A"%></p>
                                                    <p><b>Connectivity Type :</b> <%#(!string.IsNullOrEmpty(Eval("ConnectivityType").ToString()) )? Eval("ConnectivityType"):"N/A" %></p>
                                                    <p><b>Connected To WiFi :</b> <%#(!string.IsNullOrEmpty(Eval("ConnectedToWiFi").ToString())) ? Eval("ConnectedToWiFi"):"N/A"%></p>
                                                    <p><b>Network Status :</b> <%#(!string.IsNullOrEmpty(Eval("NetworkStatus").ToString())) ? Eval("NetworkStatus"):"N/A"%></p>
                                                </div>
                                                <div class="col-lg-6  col-md-6 col-sm-6 col-xs-12">
                                                    <p><b>Connectivity Name :</b> <%#(!string.IsNullOrEmpty(Eval("ConnectivityName").ToString())) ? Eval("ConnectivityName"):"N/A" %></p>
                                                    <p><b>Roaming :</b> <%#(!string.IsNullOrEmpty(Eval("Roaming").ToString())) ? Eval("Roaming"):"N/A"%></p>
                                                    <p><b>Connected To Mobile :</b> <%#(!string.IsNullOrEmpty(Eval("ConnectedToMobile").ToString())) ? Eval("ConnectedToMobile"):"N/A"%></p>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>


                                </div>
                                     </ItemTemplate>

                                    </asp:FormView>
                            </div>
                                    

                    
                </div>
            </div>
        </div>
    
</asp:Content>

