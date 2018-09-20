<%@ Page Title="Regular Contacts" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="Regular.aspx.cs" Inherits="MobiOcean.MDM.Web.Regular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">


        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">

                <div class="bhoechie-tab-content active">

                    <div class="profile1">&nbsp;&nbsp;Regular Contacts</div>

                    <div class="dataTables_length" id="datatable-editable_length" style="text-align: center">
                        <label>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>

                        </label>
                        </div>
                        <div class="content padding-top-none">

                            <div class="row" style="text-align: center">
                                <div class=" form">
                                    <div class="form-group ">
                                        <div class="col-lg-8">
                                            <div class="col-lg-6">
                                                <label>
                                                    Device / User Name :
                                                                   <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </label>
                                            </div>
                                            <div class="col-lg-6">
                                                <label>
                                                    Sync Date Time :
                                                                    <asp:DropDownList ID="ddlSyncDateTime" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                </label>
                                            </div>
                                            <div class="col-lg-6">
                                                <label>
                                                    Contact Name :
                                                                    <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </label>
                                            </div>
                                            <div class="col-lg-6">
                                                <label>
                                                    Mobile No :
                                             <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <br />
                                            <br />
                                            <label>
                                                <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                            </label>
                                        </div>
                                        <div class="col-lg-12">
                                            <br />
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
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Person Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactName" runat="server" Text='<%#Eval("Contact_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("Contact_Mobile_No1")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sync Date Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLogDateTime" runat="server" Text='<%#Eval("LogDateTime")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Whitelist">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsWhiteList" runat="server" Text='<%#Eval("IsWhiteList").ToString()=="1"?"Yes":"No"%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEWhiteList" runat="server" Visible="false" Text='<%#Eval("IsWhiteList")%>'></asp:Label>
                                                <asp:RadioButtonList ID="rbtnIsWhiteList" runat="server" RepeatDirection="Vertical">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn-link" Text="Edit" CommandName="Edit" ToolTip="Edit">
                                            <i><img src="image/edit.png"></i></asp:LinkButton>
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
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                                </asp:GridView>

                            </div>
                        </div>
                    
                </div>
            </div>
        </div>
  
</asp:Content>


