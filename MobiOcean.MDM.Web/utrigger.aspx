<%@ Page Title="Remote Triggers" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="utrigger.aspx.cs" Inherits="MobiOcean.MDM.Web.utrigger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    <style type="text/css">
        td.actions {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">


        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <!-- flight section -->
                <div class="bhoechie-tab-content active">

                    <div class="profile1">
                       &nbsp;&nbsp;Remote Triggers</div>

                    <div class="dataTables_length" id="datatable-editable_length" style="text-align: center">
                        <label>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>

                        </label>
                        </div>
                        <div class="content padding-top-none">

                            <div class="row" style="text-align: right">
                              
                            </div>
                            <br />


                            <div class="panel panel-primary" style="border-color:#ea6161;">
                                <div class="panel-heading text-left" style="background:#ea6161;border-color:#ea6161;">
                                    
                                    <div class="col-lg-8">
                                        <h3>Remote triggers</h3>
                                    </div>
                                    <div class="col-lg-4">
                                    <label style="text-align: left">
                                        Device / User Name 
                                                 <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </label>
                                </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body ">
                                    <div class="col-lg-12">
                                        <div class="col-lg-8">
                                            
                                                    <p class="wraping text-left">
                                                        Lost your phone or suspect it’s stolen? Want to find out where your employee is this minute?<br>
                                                        This is where you can control your smartphone or tablet from afar via MobiOcean remote triggers.<br>
                                                        Please note that these triggers take 30 seconds to one minute to complete the action.
                                                    </p>
                                        </div>
                                        <div class="col-lg-4">
                                            
                                        <div class="col-lg-7">
                                            <asp:TextBox ID="txtPin" runat="server" CssClass="form-control" Enabled="false" MaxLength="4"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required!" ForeColor="Red" ControlToValidate="txtPin" ValidationGroup="Change"></asp:RequiredFieldValidator>
                                                    
                                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txtPin" ValidationExpression="^[0-9]{4}$" ErrorMessage="Enter a 4 digit number!" ForeColor="Red" ValidationGroup="Change" />
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPin" FilterType="Numbers" />
                                        </div>
                                        <div class="col-lg-5">
                                            <asp:Button ID="btnChangePin" runat="server" CssClass="btn btnd btncompt" OnClick="btnChangePin_Click" Text="Save" Visible="false" ValidationGroup="Change" />
                                                   
                                                    <asp:Button ID="btnPin" runat="server" CssClass="btn btnd btncompt" OnClick="btnPin_Click1" Text="Change Pin" />
                                        </div>
                                        <div class="clearfix"></div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div><hr />
                                    <div class="col-lg-6 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Lock The Smartphone Or Tablet</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body text-left">
                                                
                                                    <div class="">
                                            
                                                                <p class="wraping text-left">
                                                                    Remotely lock this device if it is missing so that it can't be used by criminals or even<br>
                                                                    mischievous friends. When you find it, simply unlock it by entering your PIN in the device
                                                                </p>
                                                    </div>
                                                    <div class="">
                                            

                                                                <asp:Button ID="btnLock" runat="server" CssClass="btn btn-sky pull-right" Text="Lock device" OnClick="btnLock_Click" OnClientClick="return confirm('Are you sure want to continue?');" />

                                                    </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Device Siren</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body text-left">
                                              
                                                    <div class="">
                                            
                                                                <p class="wraping text-left">
                                                        Trigger an ear-splitting siren to disturb thieves or help you track down your missing device.<br>
                                                        De-activate it by entering your PIN on the device.
                                                                </p>
                                                    </div>
                                                    <div class="">
                                            

                                                    <asp:Button ID="btnSiren" runat="server" CssClass="btn btn-sky pull-right" Text="Trigger siren" OnClick="btnSiren_Click" OnClientClick="return confirm('Are you sure want to continue?');" />

                                                    </div>
                                            </div>
                                        </div>
                                    </div><div class="clearfix"></div>
                                    <div class="col-lg-6 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Delete Device Contact</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body text-left">
                                             
                                                    <div class="">
                                            
                                                                <p class="wraping text-left">
                                                        Delete the Contact of the smartphone if it is gone for good. This will delete all your Contacts<br>
                                                        and can't be reversed, so please be very sure you want to do this.
                                                                </p>
                                                    </div>
                                                    <div class="">
                                                        <asp:Button ID="btncontact" runat="server" CssClass="btn btn-sky pull-right" Text="Delete Contact" OnClick="btncontact_Click" OnClientClick="return confirm('Are you sure want to continue?');" />
                                                    </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Delete Device SMS</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body text-left">
                                             
                                                    <div class="">
                                            
                                                                <p class="wraping text-left">
                                                        Delete the SMS of the smartphone or tablet if it is gone for good. This will delate all your messages,<br>
                                                        and can't be reversed, so please be very sure you want to do this.
                                                                </p>
                                                    </div>
                                                    <div class="">
                                                        <asp:Button ID="btnSMS" runat="server" CssClass="btn btn-sky pull-right" Text="Delete SMS" OnClick="btnSMS_Click" OnClientClick="return confirm('Are you sure want to continue?');" />
                                                    </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-lg-6 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Delete Device Data</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body text-left">
                                               
                                                    <div class="">
                                            
                                                                <p class="wraping text-left">
                                                        Delete the data off the smartphone or tablet if it is gone for good. This will perform a factory reset<br>
                                                        and can't be reversed, so please be very sure you want to do this
                                                                </p>
                                                    </div>
                                                    <div class="">
                                                        <asp:Button ID="btnDeleteDeviceData" runat="server" CssClass="btn btn-sky pull-right" Text="Delete device data" OnClick="btnDeleteDeviceData_Click" OnClientClick="return confirm('Are you sure want to continue?');" />
                                                    </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Factory Reset</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body text-left">
                                              
                                                    <div class="">
                                            
                                                                <p class="wraping text-left">Force the device to Factory Reset.
                                                                </p>
                                                    </div>
                                                    <div class="">
                                                        <asp:Button ID="btnFactoryReset" runat="server" CssClass="btn btn-sky pull-right" Text="Factory Reset" OnClick="btnFactoryReset_Click" OnClientClick="return confirm('Are you sure want to continue?');" />
                                                    </div>
                                            </div>
                                        </div>
                                    </div> 
                                    <div class="clearfix"></div>
                                    <div class="col-lg-6  col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Force Sync</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body text-left">
                                              
                                                    <div class="">
                                            
                                                                <p class="wraping text-left">Force the device to Sync.</p>
                                                    </div>
                                                    <div class="">
                                                        <asp:Button ID="btnforcesync" runat="server" CssClass="btn btn-sky  pull-right" Text="Force Sync" OnClick="forcesync_Click" OnClientClick="return confirm('Are you sure want to continue?');" />
                                                    </div>
                                            </div>
                                        </div><div class="clearfix"></div>
                                    </div>
                                    <div class="col-lg-6 col-xs-12">
                                         <div class="panel panel-danger">
                                            <div class="panel-heading text-left">
                                                <div class="text-left">
                                                     <h4>Location Device</h4>
                                                </div>
                                            </div>
                                            <div class="panel-body text-left">
                                              
                                                    <div class="">
                                            
                                                                <p class="wraping text-left">Get Last Location of your Device 
                                                         <asp:Label ID="lblLastLocation" runat="server" ForeColor="Green"></asp:Label><br />
                                                        <asp:Label ID="lblLasttime" runat="server" ForeColor="Green"></asp:Label>
                                                  </p>
                                                    </div>
                                                    <div class="">
                                                        <asp:Button ID="btnLoction" runat="server" CssClass="btn btn-sky  pull-right" Text="Get Location" OnClick="btnLoction_Click" OnClientClick="return confirm('Are you sure want to continue?');" />
                                                    </div>
                                            </div>
                                        </div><div class="clearfix"></div>
                                    </div>

                                    
                                    <div class="clearfix"></div>
                                </div>
                            </div>

                            <!---->

                        </div>
                    
                </div>
            </div>
        </div>
    <%--</form>--%>

</asp:Content>
