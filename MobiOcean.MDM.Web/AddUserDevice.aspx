<%@ Page Title="Add User Device" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="AddUserDevice.aspx.cs" Inherits="MobiOcean.MDM.Web.AddUserDevice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" Runat="Server">
     <%--<form id="form1" runat="server">--%>
       <%--<%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
     </asp:ScriptManager>--%>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active">
                <li class="profile1"><i>
                    <img src="image/plus-4.png" class="iconview"></i>&nbsp;&nbsp;Add User Device</li>
                <br />
                <br />
                <div class="content padding-top-none">
                    <div class="panel-body table-rep-plugin">
                        <div class=" form">
                            <div class="col-lg-7">
                                <div class="form-group ">
                                    <label for="company" class="control-label col-lg-4">User Name* : </label>
                                    <div class="col-lg-8">
                                         <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true" ></asp:DropDownList>
                                        <br />
                                    </div>
                                </div>
                                <div class="form-group ">
                                    <label for="firstname" class="control-label col-lg-4">Device Name* : </label>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtDeviceName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                    ControlToValidate="txtDeviceName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group ">
                                    <label for="username" class="control-label col-lg-4">Mobile No* : </label>
                                    <div class="col-lg-8">
                                       <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="fmob" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="txtMobileNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="save"
                                ControlToValidate="txtMobileNo" ErrorMessage="Must be 10 digits" ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group ">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>    
                                    </div>
                                </div>
                                <div class="form-group ">
                                    <div class="col-lg-10">
                                        <asp:Label ID="lblpopmsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-offset-2 col-lg-12">
                                        <asp:Button ID="BtnSave" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save" OnClick="btnsave_Click" ValidationGroup="save" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <%--</form>--%>
</asp:Content>

