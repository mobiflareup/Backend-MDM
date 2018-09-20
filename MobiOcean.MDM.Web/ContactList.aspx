<%@ Page Title="All Contacts" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ContactList.aspx.cs" Inherits="MobiOcean.MDM.Web.ContactList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ScriptManager>--%>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->

        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <!-- flight section -->
                <div class="bhoechie-tab-content active">

                    <div class="profile1">&nbsp;&nbsp;Address Book</div>

                    <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <div class="dataTables_length" id="datatable-editable_length">
                                    <label>
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>

                            <div class="row" style="text-align: center">
                                <div class=" form">
                                    <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                Device/User Name :
                                                                   <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                Sync Date Time :
                                                                    <asp:DropDownList ID="ddlSyncDateTime" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                Contact Name :
                                                                    <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                Mobile No :
                                                                     <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                White List :
                                                                    <asp:DropDownList ID="ddlWhiteList" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                                        <asp:ListItem Text="All" Value="100"></asp:ListItem>
                                                                        <asp:ListItem Text="White List" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Black List" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                <br />
                                                <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                            </label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <br />


                            <div class="table-responsive" data-pattern="priority-columns">
                                <asp:GridView ID="grdContact" runat="server" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                    PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                    EmptyDataText="No record found." OnPageIndexChanging="grdContact_PageIndexChanging" Width="100%"
                                    OnRowCancelingEdit="grdContact_RowCancelingEdit" OnRowEditing="grdContact_RowEditing"
                                    OnRowUpdating="grdContact_RowUpdating" OnRowDataBound="grdContact_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%#Eval("Contact_Id")%>'></asp:Label>
                                                <asp:Label ID="lblDeviceId" runat="server" Text='<%#Eval("DeviceId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Device Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox id="txtUserName" runat="server" Text='<%#Eval("UserName")%>' cssclass="TxtBox"></asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Person Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactName" runat="server" Text='<%#Eval("Contact_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox id="txtUserName" runat="server" Text='<%#Eval("UserName")%>' cssclass="TxtBox"></asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("Contact_Mobile_No1")%>'></asp:Label>
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:Label ID="lblEProfileId" runat="server" Text='<%#Eval("ProfileId")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlProfileName" runat="server" cssclass="form-control" AppendDataBoundItems="true"></asp:DropDownList>                                              
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sync DateTime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLogDateTime" runat="server" Text='<%#Eval("LogDateTime")%>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox id="txtUserName" runat="server" Text='<%#Eval("UserName")%>' cssclass="TxtBox"></asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="White List">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsWhiteList" runat="server" Text='<%#Eval("IsWhiteList").ToString()=="1"?"Yes":"No"%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEWhiteList" runat="server" Visible="false" Text='<%#Eval("IsWhiteList")%>'></asp:Label>
                                                <asp:RadioButtonList ID="rbtnIsWhiteList" runat="server" RepeatDirection="Vertical">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <%--<asp:DropDownList ID="ddlIsEnable" runat="server" cssclass="form-control">
                                                    <asp:ListItem Value="0" >Disabled</asp:ListItem>
                                                    <asp:ListItem Value="1" >Enabled</asp:ListItem>
                                                </asp:DropDownList>--%>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn-link" CommandName="Edit" ToolTip="Edit"><i><img src="image/edit.png"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="btn-link"
                                                    Text="Update" ToolTip="Update" ValidationGroup="Update"><i  class="fa fa-save"></i></asp:LinkButton>
                                                &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel" CssClass="btn-link"
                                                Text="Cancel" ToolTip="Canecl"><i  class="fa fa-close"></i></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn-link" Text="Delete" OnClientClick="return confirm('The Group will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                           
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateField>--%>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    <%--</form>--%>
</asp:Content>
