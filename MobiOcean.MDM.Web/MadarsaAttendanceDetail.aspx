<%@ Page Title="Madarsa Attendance Details" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="MadarsaAttendanceDetail.aspx.cs" Inherits="MobiOcean.MDM.Web.MadarsaAttendanceDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <!-- ============================================================== -->
    <!-- Start right Content here -->
    <!-- ============================================================== -->

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active div">
                <div class="profile1">&nbsp;&nbsp;Madarsa Attendance Details</div>

                <br />
                <div class="table-responsive">
                    <asp:GridView ID="grdAttendance" runat="server" DataKeyNames="AttendanceId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                        PageSize="10" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        EmptyDataText="No record found." OnPageIndexChanging="grdAttendance_PageIndexChanging" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("AttendanceId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Madarsa Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserCode" runat="server" Text='<%#Eval("MadarsaName").ToString()==""?"---":Eval("MadarsaName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ParaTeacher Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("ParaTeacherName").ToString()==""?"---":Eval("ParaTeacherName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("Datetime").ToString()==""?"---":Eval("Datetime","{0:dd MMM yyyy HH:mm}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Latitude">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("Latitude").ToString()==""?"---":Eval("Latitude")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Longitude">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreated" runat="server" Text='<%#Eval("Longitude").ToString()==""?"---":Eval("Longitude")%>'></asp:Label>
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
