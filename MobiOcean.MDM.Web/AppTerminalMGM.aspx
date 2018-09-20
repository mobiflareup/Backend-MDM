<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AppTerminalMGM.aspx.cs" Inherits="MobiOcean.MDM.Web.AppTerminalMGM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">

                    <div class="bhoechie-tab-content active div">

                        <div class="profile1">&nbsp;&nbsp;Terminal List</div>
                        <br />
                        <div class=" row">
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="User Name"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtDeviceName" runat="server" CssClass="form-control" placeholder="Device Name"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" placeholder="Branch Name"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtDept" runat="server" CssClass="form-control" placeholder="Department Name"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12 text-center">
                                        <br />
                                        <asp:Button ID="Search" runat="server" OnClick="Search_Click" Text="Search" CssClass="btn btnd btncompt" />
                                    </div>
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
                            <asp:GridView ID="grdUsr" runat="server" GridLines="None" class="table mGrid" AutoGenerateColumns="false"
                                HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                                OnPageIndexChanging="grdUsr_PageIndexChanging" Width="100%" PageSize="10" AllowPaging="true" OnRowDataBound="grdUsr_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                            <asp:Label ID="lblClientId" runat="server" Text='<%#Eval("ClientId")%>'></asp:Label>
                                            <asp:Label ID="lblDeviceId" runat="server" Text='<%#Eval("DeviceId")%>'></asp:Label>
                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo1")%>'></asp:Label>
                                            <%--<asp:Label ID="lblAppMarketAssignId" runat="server" Text='<%#Eval("AppMarketAssignId")%>'></asp:Label>--%>
                                            <%-- <asp:Label ID="lblPackage" runat="server" Text='<%#Eval("AppPackage")%>'></asp:Label>
                                    <asp:Label ID="lblPath" runat="server" Text='<%#Eval("Path")%>'></asp:Label>
                                    <asp:Label ID="lblInstall" runat="server" Text='<%#Eval("AppInstall")%>'></asp:Label>
                                    <asp:Label ID="lblUpdate" runat="server" Text='<%#Eval("AppUpdate")%>'></asp:Label>
                                    <asp:Label ID="lblUnInstall" runat="server" Text='<%#Eval("AppUnInstall")%>'></asp:Label>
                                    <asp:Label ID="lblOsUpgrade" runat="server" Text='<%#Eval("OsUpgrade")%>'></asp:Label>
                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo1")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <%--<input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />--%>
                                            <asp:CheckBox ID="CheckAll" runat="server" OnCheckedChanged="CheckAll_CheckedChanged" AutoPostBack="true" />
                                            <%--OnCheckedChanged="CheckAll_CheckedChanged" AutoPostBack="true"--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="User" runat="server" OnCheckedChanged="User_CheckedChanged" AutoPostBack="true" Checked='<%#Eval("Status").ToString() == "1"  ? false:true %>' /><%--Checked='<%#Eval("Status").ToString() == "1"  ? false:true %>'--%>
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
                                    <asp:TemplateField HeaderText="Mobile No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobileNo1" runat="server" Text='<%#Eval("MobileNo1")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dept">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDept" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                            </asp:GridView>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnInstallNewApp" runat="server" CssClass="btn btnd btncompt" Text="Install New App" OnClick="btnInstallNewApp_Click" />
                            &nbsp; &nbsp;
                     <asp:Button ID="btnUpgradeAPP" runat="server" CssClass="btn btnd btncompt" Text="Upgrade App" OnClick="btnUpgradeAPP_Click" />
                            &nbsp; &nbsp;
                     <asp:Button ID="btnUnIstallApp" runat="server" CssClass="btn btnd btncompt" Text="UnInstall App" OnClick="btnUnIstallApp_Click" />
                            &nbsp; &nbsp;
                     <asp:Button ID="btnOsUpgrade" runat="server" CssClass="btn btnd btncompt" Text="Upgrade OS" OnClick="btnOsUpgrade_Click" />
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
                    <asp:Label ID="lblfinalOSId" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblfinalOSPath" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="Button2" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
                    <asp:Label ID="lblosUpgradetext" runat="server" Text="Are you sure you want to Delete?" ForeColor="White"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btncancelok" runat="server" Text="OK" OnClick="Yes_Click" />
                    <asp:Button ID="btnccl" runat="server" Text="Cancel" OnClick="No_Click" />
                </div>
            </asp:Panel>



            <asp:Button ID="dummy_BtnAsgnGp" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mp" runat="server" PopupControlID="myModal"
                PopupDragHandleControlID="dragi" TargetControlID="dummy_BtnAsgnGp" CancelControlID="btnclose"
                BackgroundCssClass="modalbackground">
            </asp:ModalPopupExtender>


            <asp:Panel runat="server" ID="myModal" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" class="modal-lg modal-md modal-xs">

                <div class="modal-content">
                    <div class="modal-header" id="dragi">
                        <div class="col-sm-6" style="text-align: left">
                            <asp:Label ID="lblGrpId" runat="server" Visible="false"></asp:Label>
                            <h4><b>
                                <asp:Label ID="lblGroupName" runat="server" Text="Select App List"></asp:Label></b></h4>
                        </div>
                        <div class="col-sm-6" style="text-align: right">
                            <asp:Button ID="btnclose" runat="server" Text="x" class="close btn btnd btncompt" Style="margin-top: 3px; margin-right: -15px;" />
                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="row" style="height: 250px; overflow: auto">
                            <div class="col-sm-12">
                                <asp:Label ID="lblClickId" runat="server" Visible="false"></asp:Label>
                                <div class="table-responsive" data-pattern="priority-columns">
                                    <asp:GridView ID="grdaddselected" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record found" ShowHeader="true" ShowHeaderWhenEmpty="true" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable" RowStyle-HorizontalAlign="Center">
                                        <Columns>

                                            <asp:TemplateField>
                                                <ItemTemplate>

                                                    <asp:Label ID="AlblAppId" runat="server" Text='<%#Eval("AppMarketId")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="AlbAppUrl" runat="server" Text='<%#Eval("ApkPath")%>' Visible="false"></asp:Label>
                                                    <asp:CheckBox runat="server" ID="AchkRow_Parents" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="AchkHeader_Parents" AutoPostBack="true" OnCheckedChanged="AchkHeader_Parents_CheckedChanged" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppName" runat="server" Text='<%#Eval("AppName")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Package">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppPackage" runat="server" Text='<%#string.IsNullOrEmpty(Eval("AppPackage").ToString())?"---":Eval("AppPackage")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Version">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppVersion" runat="server" Text='<%#string.IsNullOrEmpty(Eval("AppVersion").ToString())?"---":Eval("AppVersion")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <div class="col-sm-12" style="text-align: center">
                            <asp:Button ID="btnAppSubmit" runat="server" Text="Done" class="btn btnd btncompt" OnClick="btnAppSubmit_Click" />
                        </div>
                    </div>
                </div>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
