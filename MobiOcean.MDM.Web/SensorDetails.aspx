<%@ Page Title="Sensor Details" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="SensorDetails.aspx.cs" Inherits="MobiOcean.MDM.Web.SensorDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    <style type="text/css">
        .modalBackgroundTemp {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active">

                <div class="profile1" style="margin: 0px;">
                    Sensor Details
                         <div class="clearfix"></div>
                </div>

               
               <br />
                <div class="form">
                    <div class="form-group">

                        <div class="col-lg-6" style="text-align: center">
                            <label>
                                User Name :
                                 <asp:TextBox ID="txtUname" runat="server" CssClass="form-control"></asp:TextBox>
                            </label>
                        </div>
                    </div>
                    <div class="form-group">

                        <div class="col-lg-6" style="text-align:center ">
                            <label>
                                Device Name :
                                 <asp:TextBox ID="txtDname" runat="server" CssClass="form-control"></asp:TextBox>
                            </label>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                    <div class="form-group">

                        <div class="col-lg-6" style="text-align:center ">
                            <label>
                                Sensor Name :
                                 <asp:TextBox ID="txtSname" runat="server" CssClass="form-control"></asp:TextBox>
                            </label>
                        </div>
                    </div>

                    <div class="form-group ">

                        <div class="col-lg-6" style="text-align:center;vertical-align:middle ">
                            <label>
                                <br />
                                <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click" />
                            </label>
                        </div>
                        <div class="clearfix"></div>
                    </div>


                </div>


                <hr />

                <div class="table-responsive">
                    <asp:GridView ID="grdwifisensor" runat="server" DataKeyNames="SensorEnableId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                        PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        EmptyDataText="No record found." OnPageIndexChanging="grdwifisensor_PageIndexChanging" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("SensorEnableId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblbId" runat="server" Text='<%#string.IsNullOrEmpty(Eval("UserName").ToString())?"---":Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Device Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbldId" runat="server" Text='<%#string.IsNullOrEmpty(Eval("DeviceName").ToString())?"---":Eval("DeviceName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sensor Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSensorName" runat="server" Text='<%#string.IsNullOrEmpty(Eval("SensorName").ToString())?"---":Eval("SensorName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Log Date Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%#string.IsNullOrEmpty(Eval("LogDateTime").ToString())?"---":Format(Eval("LogDateTime").ToString())%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("IsSensor").ToString()=="1"?"Enable":"Disable"%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                    </asp:GridView>

                </div>
                <!-- train section -->
            </div>

        </div>


    </div>
</asp:Content>

