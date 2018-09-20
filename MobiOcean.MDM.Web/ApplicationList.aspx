<%@ Page Title="Applciation List" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeBehind="ApplicationList.aspx.cs" Inherits="MobiOcean.MDM.Web.ApplicationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">


    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">

                <div class="profile1">&nbsp;&nbsp;Application List</div>

                <div class="dataTables_length" id="datatable-editable_length">
                    <label>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>

                    </label>
                    </div>
                    <div class="content padding-top-none">

                        <div class="row" style="text-align: center">
                            <div class=" form">
                                <div class="form-group ">

                                    <div class="col-lg-3">
                                        <label>
                                            By Device/User Name :
                                                                 <asp:DropDownList ID="ddlUserName" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group ">

                                    <div class="col-lg-3">
                                        <label>
                                            By Application Name : 
                                                                    <asp:TextBox ID="txtSrchApplication" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <div class="col-lg-3">
                                        <label>
                                            <br />
                                            <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="table-responsive">


                            <asp:GridView ID="grdAppMaster" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false"
                                ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Record found." DataKeyNames="ChatAppId"
                                OnPageIndexChanging="grdAppMaster_PageIndexChanging" Width="100%" PageSize="20" AllowPaging="true"
                                OnRowCancelingEdit="grdAppMaster_RowCancelingEdit" OnRowEditing="grdAppMaster_RowEditing"
                                OnRowUpdating="grdAppMaster_RowUpdating" OnRowDataBound="grdAppMaster_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("ChatAppId")%>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="App Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppName" runat="server" Text='<%#Eval("ChatApp")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Group Name" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupName" runat="server" Text='<%# string.IsNullOrEmpty(Eval("AppGroupName").ToString())?"---":Eval("AppGroupName").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="ElblGroupName" runat="server" Text='<%#Eval("ChatGroupId") %>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="EddlGroupName" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>

                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Manage Group" CommandName="Edit" ToolTip="Manage Group" CssClass="btn-link"></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="btn-link"
                                                Text="Update Group" ToolTip="Update Group" ValidationGroup="Update" />
                                            &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel" CssClass="btn-link"
                                                Text="Cancel" ToolTip="Canecl" />
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





