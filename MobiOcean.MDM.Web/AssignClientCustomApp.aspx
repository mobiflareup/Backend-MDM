<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AssignClientCustomApp.aspx.cs" Inherits="MobiOcean.MDM.Web.AssignClientCustomApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <asp:Label ID="HiddenField" runat="server" Visible="false"></asp:Label>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active div">
                <div class="profile1">
                    &nbsp;&nbsp;Assign Client Custom App
                     <asp:Button ID="btnBack" runat="server" CssClass="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Back" OnClick="btnBack_Click"></asp:Button>
                </div>

                <br />


                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-sm-6">
                            <asp:Label ID="lblCC" runat="server" Font-Bold="true" Font-Size="Medium" Text="Client Code :"></asp:Label>
                            <asp:Label ID="lblClientCode" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-6">
                            <asp:Label ID="lblCN" runat="server" Font-Bold="true" Font-Size="Medium" Text="Client Name :"></asp:Label>
                            <asp:Label ID="lblClientName" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <br />
                <div style="text-align: center">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <br />
                <div class="table-responsive">
                    <asp:GridView ID="grdClient" runat="server" GridLines="None" class="table mGrid" AutoGenerateColumns="false"
                        HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." OnRowDataBound="grdClient_RowDataBound"
                        Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblAppTypeId" runat="server" Text='<%#Eval("AppTypeId") %>'></asp:Label>
                                    <asp:Label ID="lblIsActive" runat="server" Text='<%#Eval("IsActive") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblCheckId" runat="server" Text="0" Visible="false"></asp:Label>
                                    <asp:CheckBox runat="server" ID="AchkRow_Parents" AutoPostBack="true" OnCheckedChanged="AchkRow_Parents_CheckedChanged" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" ID="AchkHeader_Parents" AutoPostBack="true" OnCheckedChanged="AchkHeader_Parents_CheckedChanged" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="App Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblAppName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                    </asp:GridView>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnAssigned" runat="server" ValidationGroup="Save" CssClass="btn btnd btncompt" Text="Save" OnClick="btnAssigned_Click" />
                    &nbsp; &nbsp;                                   
                </div>


            </div>
        </div>
    </div>
</asp:Content>
