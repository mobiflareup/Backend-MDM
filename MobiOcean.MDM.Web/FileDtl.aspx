<%@ Page Title="SOS Files" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="FileDtl.aspx.cs" Inherits="MobiOcean.MDM.Web.FileDtl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">
                <div class="profile1" style="margin: 0px;">
                    SOS Files
                         <div class="clearfix"></div>
                </div>


                <!-- Start content -->
                <div class="dataTables_length" id="datatable-editable_length" style="text-align: center">
                    <label>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    </label>
                </div>



                <div class="row" style="text-align: center">
                    <div class=" form">

                        <div class="form-group ">

                            <div class="col-lg-6">
                                <label>
                                    By User / Device Name :
                                                                   <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-6">
                                <label>
                                    From Date :
                                                                    <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <%-- <asp:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFrmdate" Format="dd MMM yyyy" PopupButtonID="txtFrmdate" />--%>
                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-6">
                                <label>
                                    To Date :
                                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <%--<asp:CalendarExtender ID="ce2" runat="server" TargetControlID="txtTodate" Format="dd MMM yyyy" PopupButtonID="txtTodate" />--%>
                                </label>
                            </div>
                        </div>


                        <div class="form-group ">

                            <div class="col-lg-6">
                                <label>
                                    <br />
                                    <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                </label>
                            </div>
                        </div>

                    </div>
                </div>
                <br />
                <hr />

                <div class="table-responsive">
                    <asp:GridView ID="grdFileDtl" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None"
                        AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                        OnPageIndexChanging="grdFileDtl_PageIndexChanging" AllowPaging="true" PageSize="20">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Bind("FileId") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Bind("UserName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Device Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceName" runat="server" Text='<%#Bind("DeviceName") %>'></asp:Label>
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
                                    <asp:LinkButton ID="lnkbntDownload" runat="server" CssClass="btn-link" Text="Download" OnClick="lnkbtnDownload_Click"></asp:LinkButton>
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
    <script>
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrmDate],[id$=txtToDate]").datepick({
                    dateFormat: 'dd M yyyy'
                });
                $('#style-3').scroll(function () {
                    $("[id$=txtFrmDate],[id$=txtToDate]").datepick("hide");
                });
            });
        }
    </script>

</asp:Content>



