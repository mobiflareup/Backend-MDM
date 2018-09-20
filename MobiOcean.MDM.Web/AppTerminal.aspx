<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AppTerminal.aspx.cs" Inherits="MobiOcean.MDM.Web.AppTerminal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active div">
                <div class="profile1" style="margin: 0px;">
                    Assign User
                    <asp:Label CssClass="text-uppercase pull-right" ID="txtHeading" runat="server" ></asp:Label>
                </div>
                <br />
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
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
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
                                    <asp:Label ID="lblAppMarketAssignId" runat="server" Text='<%#Eval("AppMarketAssignId")%>'></asp:Label>
                                    <asp:Label ID="lblPackage" runat="server" Text='<%#Eval("AppPackage")%>'></asp:Label>
                                    <asp:Label ID="lblPath" runat="server" Text='<%#Eval("Path")%>'></asp:Label>
                                    <asp:Label ID="lblInstall" runat="server" Text='<%#Eval("AppInstall")%>'></asp:Label>
                                    <asp:Label ID="lblUpdate" runat="server" Text='<%#Eval("AppUpdate")%>'></asp:Label>
                                    <asp:Label ID="lblUnInstall" runat="server" Text='<%#Eval("AppUnInstall")%>'></asp:Label>
                                    <asp:Label ID="lblOsUpgrade" runat="server" Text='<%#Eval("OsUpgrade")%>'></asp:Label>
                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo1")%>'></asp:Label>
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
                                    <asp:CheckBox ID="User" runat="server" Checked='<%#Eval("Status").ToString() == "1"  ? false:true   %>' OnCheckedChanged="User_CheckedChanged" AutoPostBack="true" />
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
                    &nbsp; &nbsp;
                    <asp:Button ID="Cancel" runat="server" CssClass="btn btn-danger btncompt" OnClick="Cancel_Click" Text="Cancel" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>


