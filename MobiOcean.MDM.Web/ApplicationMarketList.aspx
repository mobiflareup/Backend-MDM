<%@ Page Title="Application Market List" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ApplicationMarketList.aspx.cs" Inherits="MobiOcean.MDM.Web.ApplicationMarketList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    <style>
        .img-50 {
            width: 50px;
        }

        .modalBackgroundTemp {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">

                    <div class="bhoechie-tab-content active div">

                        <div class="profile1">&nbsp;&nbsp;App Store

                            <asp:Button ID="addApk" runat="server" class="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Add APP" OnClick="addApk_Click" />
                        </div>
                        <br />
                        <div class="row" style="text-align: center">
                            <div class=" form">

                                <div class="col-md-3">
                                    <label>
                                        By App Name :
                                 <asp:TextBox ID="txtSrchAppName" runat="server" class="form-control"></asp:TextBox>
                                    </label>
                                </div>

                                <div class="col-md-3">
                                    <label>
                                        By Package Name :
                              <asp:TextBox ID="txtSrchPackage" runat="server" class="form-control"></asp:TextBox>
                                    </label>
                                </div>

                                <div class="col-md-3">
                                    <label>
                                        By Developer :
                              <asp:TextBox ID="txtSrchDeveloper" runat="server" class="form-control"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <br />
                                    <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />

                                </div>

                            </div>


                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <div class="dataTables_length" id="datatable-editable_length">
                                    <label>
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="grdapplst" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None"
                                AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                                OnPageIndexChanging="grdapplst_PageIndexChanging"
                                AllowPaging="true" DataKeyNames="AppMarketId" PageSize="20" Width="100%" OnRowDeleting="grdapplst_RowDeleting">
                                <%--// OnRowDataBound="grdapplst_RowDataBound" OnRowCommand="grdapplst_RowCommand" OnRowEditing="grdapplst_RowEditing" OnRowCancelingEdit="grdapplst_RowCancelingEdit" OnRowUpdating="grdapplst_RowUpdating" --%>

                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppMarketId" runat="server" Text='<%#Eval("AppMarketId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Images">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Image ID="resultImage" runat="server" CssClass="img-rounded img-50" ImageUrl='<%# (string.IsNullOrEmpty(Eval("ImagesPath").ToString()) ? "~/image/logo.png" : "~/PublicAPK/Images/"+Eval("ImagesPath").ToString()) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="App Name">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppName" runat="server" Text='<%#Eval("AppName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Package">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPackageName" runat="server" Text='<%#Eval("AppPackage")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Type">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppType" runat="server" Text='<%#Eval("AppType")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Version">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppVersion" runat="server" Text='<%#Eval("AppVersion")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Developer">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateTime" runat="server" Text='<%#Eval("Developer")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Enable">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <%#(bool)Eval("UseStatus")?"<i class='fa fa-check green'></i></i>":"<i class='fa fa-times-circle red'></i>"%>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("AppPrice")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Device Mapped">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeviceMapping" runat="server" Text='<%#string.IsNullOrEmpty(Eval("DeviceType").ToString())?"NA": Eval("DeviceType").ToString() == "0"?"All Device":string.IsNullOrEmpty(Eval("DeviceTypeName").ToString())?"NA":Eval("DeviceTypeName")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="btn btn-xs btn-default single-delete" OnClick="lnkbtnEdit_Click"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                            <asp:LinkButton ID="lblInstall" runat="server" CssClass="btn btn-xs btn-default" OnClick="lblInstall_Click"><i class="fa fa-code-fork"></i> Install App</asp:LinkButton>
                                            <asp:LinkButton ID="lblUgrade" runat="server" CssClass="btn btn-xs btn-default" OnClick="lblUgrade_Click"><i class="fa fa-code-fork"></i> Upgrade App</asp:LinkButton>
                                            <asp:LinkButton ID="lblUnInstall" runat="server" CssClass="btn btn-xs btn-default" OnClick="lblUnInstall_Click"><i class="fa fa-code-fork"></i> UnInstall App</asp:LinkButton>
                                            <%--<asp:LinkButton ID="lnkbtnInfo" runat="server" CssClass="btn btn-xs btn-default single-delete" OnClick="lnkbtnInfo_Click"><i class="fa fa-info"></i>App Info</asp:LinkButton>--%>
                                            <%--<asp:LinkButton ID="lnkbtnDvMap" runat="server" CssClass="btn btn-xs btn-default" OnClick="lnkbtnDvMap_Click"><i class="fa fa-code-fork"></i>Device Maping</asp:LinkButton>
                                            <asp:LinkButton ID="lblAssignDevice" runat="server" CssClass="btn btn-xs btn-default" OnClick="lblAssignDevice_Click"><i class="fa fa-code-fork"></i>Assign Device</asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" ToolTip="Delete" CssClass="btn btn-xs btn-default single-delete" OnClick="lnkbtnDelete_Click"><i class="fa fa-trash-o"></i> Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <center>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divProcessing">
                            <asp:Image runat="server" ID="progressImg2" ImageUrl="~/images/Processing.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </center>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="dummydelete" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="mpdelete" runat="server" PopupControlID="DeleteMessagePnl"
                TargetControlID="dummydelete" CancelControlID="btnccl"
                BackgroundCssClass="modalbackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="DeleteMessagePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">

                <div class="modal-body" style="text-align: center;">
                    <asp:Label ID="lblfinalAppMarketId" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="Button2" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
                    <asp:Label ID="Label2" runat="server" Text="Are you sure you want to Delete?" ForeColor="White"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btncancelok" runat="server" Text="OK" OnClick="Yes_Click" />
                    <asp:Button ID="btnccl" runat="server" Text="Cancel" OnClick="No_Click" />
                </div>
            </asp:Panel>

            <asp:Button ID="dummyManage" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mpManage" runat="server" PopupControlID="myModal"
                PopupDragHandleControlID="dragi" TargetControlID="dummyManage" CancelControlID="btnclose1"
                BackgroundCssClass="modalbackground">
            </asp:ModalPopupExtender>


            <asp:Panel runat="server" ID="myModal" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" class="modal-lg modal-md modal-xs">

                <div class="modal-content">
                    <div class="modal-header" id="dragi">
                        <div class="col-sm-6" style="text-align: left">
                            <h4><b>Device Mapping</b></h4>
                        </div>
                        <div class="col-sm-6" style="text-align: right">
                            <asp:Button ID="btnclose1" runat="server" Text="x" class="close btn btnd btncompt" Style="margin-top: 3px; margin-right: -15px;" />
                        </div>
                    </div>
                    <%--  <div class="modal-header">
                    </div>--%>

                    <div class="modal-body">
                        <div class="row">
                            <%--style="height: 250px; overflow: auto"--%>
                            <asp:Label ID="lblAppMarketIdMapping" runat="server" Visible="false"></asp:Label>
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-4 text-center">
                                        <asp:Label ID="Label1" runat="server">Select Device : </asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="dtDeviceType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4  text-center">
                                        <asp:Button ID="btnDeviceMapping" runat="server" class="btn btnd btncompt" Text="Submit" OnClick="btnDeviceMapping_Click" />
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>
                    <div class="modal-footer">
                    </div>
                </div>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

  
</asp:Content>

