<%@ Page Title="Location" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="MobiOcean.MDM.Web.Location" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="ch" runat="server" ContentPlaceHolderID="ContentHead">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
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
                    <li class="profile1"><i>
                        <img src="image/CS.png" class="iconview"></i>&nbsp;&nbsp;Location Report</li>
                    <br />
                    <br />
                    <!-- Start content -->
                    <div class="content padding-top-none">


                        <div class="row" style="text-align: center">
                            <div class=" form">
                                <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                                <div class="form-group ">
                                    <div class="col-lg-8">
                                        <div class="col-lg-6">
                                            <label>
                                                User :
                                                                   <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                            </label>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>
                                                Device :
                                                                    <%--<asp:DropDownList ID="ddlProfileName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>--%>
                                                <asp:TextBox ID="txtDeviceName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </label>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>
                                                From Date :
                                                                    <%--<asp:DropDownList ID="ddlProfileName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>--%>
                                                <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFrmDt" Format="dd MMM yyyy" PopupButtonID="txtFrmDt" />
                                            </label>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>
                                                To Date :
                                                                    <%--<asp:DropDownList ID="ddlProfileName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>--%>
                                                <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:CalendarExtender ID="ce2" runat="server" TargetControlID="txtToDt" Format="dd MMM yyyy" PopupButtonID="txtToDt" />
                                            </label>
                                        </div>
                                    </div>

                                    <%--<div class="col-lg-4">
                                                                    <br />
                                                                </div>--%>
                                    <div class="col-lg-4">
                                        <br />
                                        <br />
                                        <label>
                                            <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                            <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="btn btnd btncompt" OnClick="btnprint_Click" />
                                        </label>
                                    </div>
                                    <div class="col-lg-12">
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="table-responsive">
                            <asp:GridView ID="grdUser" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" OnPageIndexChanging="grdUser_PageIndexChanging" AllowPaging="true" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("DeviceLocId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
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
                                    <asp:TemplateField HeaderText="Mobile No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("MobileNo")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Latitude" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLat" runat="server" Text='<%#Eval("Latitude")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Longitude" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLongi" runat="server" Text='<%#Eval("Longitude")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLogDate" runat="server" Text='<%#Eval("LogDate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLogTime" runat="server" Text='<%#Eval("LogTime")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location Source">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocationSource" runat="server" Text='<%#Eval("LocationSource")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Called By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrvcCalledBy" runat="server" Text='<%#Eval("SrvcCalledBy")%>'></asp:Label>
                                        </ItemTemplate>
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
