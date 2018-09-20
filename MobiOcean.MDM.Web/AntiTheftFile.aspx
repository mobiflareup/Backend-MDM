<%@ Page Title="File Details" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="AntiTheftFile.aspx.cs" Inherits="MobiOcean.MDM.Web.AntiTheftFile" %>

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

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <!-- flight section -->
                <div class="bhoechie-tab-content active">

                    <div class="profile1">Anti-theft File Details</div>


                    <div class="dataTables_length" id="datatable-editable_length">
                        <label>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>

                        </label>
                        </div>
                        <div class="content padding-top-none">


                            <div class="row" style="text-align: center">
                                <div class=" form">
                                    <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                                    <div class="form-group ">

                                        <div class="col-lg-3">
                                            <label>
                                                User/Device Name :
                                                                   <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group ">

                                        <div class="col-lg-3">
                                            <label>
                                                From Date :
                                                                    <asp:TextBox ID="txtFrmdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFrmdate" Format="dd MMM yyyy" PopupButtonID="txtFrmdate" />
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group ">

                                        <div class="col-lg-3">
                                            <label>
                                                To Date :
                                                                    <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:CalendarExtender ID="ce2" runat="server" TargetControlID="txtTodate" Format="dd MMM yyyy" PopupButtonID="txtTodate" />
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


                            <%--<asp:Button ID="btnshowonmap" runat="server" Text="Show On Map" Class="btn btn-primary waves-effect waves-light" OnClick="btnshowonmap_Click" />--%>



                            <div class="table-responsive">


                                <asp:GridView ID="grdFileDtl" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" OnPageIndexChanging="grdFileDtl_PageIndexChanging" AllowPaging="true" PageSize="20">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%#Bind("FileId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Device Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName").ToString()==""?"---":Eval("DeviceName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFileName" runat="server" Text='<%#Bind("FTPFileName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File Path" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFilePath" runat="server" Text='<%#Bind("FilePath") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Log Date Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLogDateTime" runat="server" Text='<%#Bind("LogDateTime") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocation" runat="server" Text='<%#Bind("Location") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFileType" runat="server" Text='<%#Eval("IsAudio").ToString()=="1"?"Audio":"Video" %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbntDownload" runat="server" CssClass="btn-link" Text="Download" CommandName="Download" OnClick="lnkbtnDownload_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                                </asp:GridView>

                            </div>
                            <div class="row" style="text-align: right">
                                <asp:Button ID="btnsavetopdf" runat="server" CssClass="btn btnd btncompt" Text="Save To Pdf" align="right" OnClick="btnsavetopdf_Click" />
                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btnd btncompt" Text="Print" OnClick="btnPrint_Click" />
                                <asp:Button ID="btnSendtomail" runat="server" CssClass="btn btnd btncompt" Text="Send to Mail" OnClick="btnSendtomail_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    <%--</form>--%>
</asp:Content>
