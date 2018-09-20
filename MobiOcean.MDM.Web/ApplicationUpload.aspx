<%@ Page Title="Add APP" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ApplicationUpload.aspx.cs" Inherits="MobiOcean.MDM.Web.ApplicationUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    <link href="Content/css/ApkStylee.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">
                <div class="profile1"><asp:Label ID="lblAppName" runat="server"></asp:Label> Application</div>
                <br />
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <br />
                <div class="col-md-12">
                    <div class="box1 box-danger box-mch-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Base Info </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group" id="DivApkUpload" runat="server">
                                <label class="col-sm-1 control-label">Apk Upload <i>*</i></label>
                                <div class="col-sm-5">
                                    <asp:FileUpload ID="APKUpload" runat="server" Text="Select File"  CssClass="btn btnd btncompt waves-effect waves-light" /> 
                                    <asp:HiddenField ID="FileNameHidden" runat="server" value=""/>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="APKUpload" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>--%>
                                    
                                </div>
                                 <div class="col-sm-4">
                                <asp:Button ID="StartUpload" runat="server" Text="Upload File" OnClick="StartUpload_Click"  CssClass="btn btnd btncompt waves-effect waves-light" />
                                     </div>
                                <div class="col-sm-3 checkbox">
                                    <label>
                                        <asp:CheckBox ID="AutoPushCheck" runat="server" />
                                        <%--<input type="checkbox" name="push" value="1"/> --%>
                                        Auto Push</label>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">App Name* </label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtAppName" runat="server" CssClass="form-control" placeholder="Please Enter App Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtAppName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                    <%--<input type="text" class="form-control app-name" name="name" value="" placeholder="Please Enter App Name "/>--%>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                           <%-- <div class="form-group">
                                <label class="col-sm-1 control-label">Use Status </label>
                                <div class="col-sm-11 radio">
                                    <label>
                                        <asp:RadioButton ID="UseStatusEnable" runat="server" GroupName="UseStatus" Checked="true"></asp:RadioButton>
                                       
                                        Enable
                                    </label>
                                    <label>
                                        <asp:RadioButton ID="UseStatusDisable" runat="server" GroupName="UseStatus"></asp:RadioButton>
                                       
                                        Disable
                                    </label>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">Charge Status </label>
                                <div class="col-sm-11 radio">
                                    <label>
                                        <asp:RadioButton ID="ChargeStatusFree" runat="server" GroupName="ChargeStatus" Checked="true"></asp:RadioButton>

                                    
                                        Free
                                    </label>
                                    <label>
                                        <asp:RadioButton ID="ChargeStatusFreeCharge" runat="server" GroupName="ChargeStatus"></asp:RadioButton>
                                       
                                        Charge
                                    </label>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">App Price </label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtAppPrice" runat="server" CssClass="form-control" placeholder="Please Enter App Price"></asp:TextBox>
                                  
                                </div>
                            </div>--%>
                            <div class="clearfix"></div>
                            <div class="form-group" id="DivImageUpload" runat="server">
                                <label class="col-sm-1 control-label">Images <i>*</i></label>
                                <div class="col-sm-5">
                                    <asp:FileUpload ID="ImageUpload" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ImageUpload" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />
                            </div>
                            <div class="clearfix"></div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">Developer <i>*</i></label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtDeveloper" runat="server" CssClass="form-control" placeholder="Please Enter Developer"></asp:TextBox>
                                    <%--<input type="text" class="form-control app-developer" name="developer" value="" placeholder="Please Enter Developer " />--%>
                                </div>
                                <label class="col-sm-1 control-label">App Type <i>*</i></label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="app_cate_id" runat="server" class="form-control">
                                        <asp:ListItem Text="Please Choose App Type " Value=""></asp:ListItem>
                                        <asp:ListItem Text="Restaurant" Value="Restaurant"></asp:ListItem>
                                        <asp:ListItem Text="Finance" Value="Finance"></asp:ListItem>
                                        <asp:ListItem Text="Security" Value="Security"></asp:ListItem>
                                        <asp:ListItem Text="Tourism" Value="Tourism"></asp:ListItem>
                                        <asp:ListItem Text="Hotel" Value="Hotel"></asp:ListItem>
                                        <asp:ListItem Text="Logistical" Value="Logistical"></asp:ListItem>
                                        <asp:ListItem Text="Commodity" Value="Commodity"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">App Introduce </label>
                                <div class="col-sm-11">
                                    <asp:TextBox ID="txtIntroduce" TextMode="multiline" Rows="4" CssClass="form-control" runat="server" placeholder="Please Enter App Introduce （1-200 character） " /><%--Columns="50"--%>
                                    <%--<textarea class="form-control app-description" rows="4" name="description" placeholder="Please Enter App Introduce （1-200 character） "></textarea>--%>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="box1 box-danger">
                        <div class="box-header with-border">
                            <h3 class="box-title">App Info </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <label class="col-sm-1 control-label">App Package </label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtAppPackage" runat="server" CssClass="form-control" placeholder="Please Enter App Package"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAppPackage" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                    <%--<input type="text" class="form-control app-com-name" name="package" value="" placeholder="Please Enter App Package " />--%>
                                </div>
                                <label class="control-label col-sm-1">App Version </label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtAppVersion" runat="server" CssClass="form-control" placeholder="Please Enter AppVersion" OnTextChanged="txtAppVersion_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAppVersion" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lblversion" runat="server"></asp:Label>
                                    <%--<input type="text" class="form-control app-version" name="version" value="" placeholder="Please Enter AppVersion " />--%>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="form-group">

                                <label class="control-label col-sm-1">App Size </label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtAppSize" runat="server" CssClass="form-control" placeholder="Please Enter AppSize"></asp:TextBox>
                                    <%--<input type="text" class="form-control app-size" name="size" value="" placeholder="Please Enter AppSize " />--%>
                                </div>
                                <br />
                                <br />
                            </div>
                            <div class="clearfix"></div>
                            <div class="form-group">
                                <label class="control-label col-sm-1">Remark </label>
                                <div class="col-sm-11">
                                    <asp:TextBox ID="txtRemark" TextMode="multiline" Rows="4" CssClass="form-control" runat="server" placeholder="Plx Enter Remark（1-200 characters） " /><%--Columns="50"--%>
                                    <%--<textarea rows="4" class="form-control app-remark" name="remark" placeholder="Plx Enter Remark（1-200 characters） "></textarea>--%>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <asp:Button ID="btnsubmit" CssClass="btn btn-lg btn-success pull-right ajax-submit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="upload" />
                    </div>
                     <div class="col-sm-6">
                         <asp:Button ID="btnCancel" CssClass="btn btn-lg btn-danger pull-left ajax-submit" runat="server" Text="Cancel" OnClick="btnCancel_Click"  />
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>


