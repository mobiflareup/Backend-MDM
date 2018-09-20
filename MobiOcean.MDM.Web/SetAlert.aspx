<%@ Page Title="Set Alerts" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeBehind="SetAlert.aspx.cs" Inherits="MobiOcean.MDM.Web.SetAlert" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">


        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12  bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">

                <div class="bhoechie-tab-content active div">

                    <div class="profile1">Set Alerts</div>
                    <br/>
                    <div class="table-responsive">
                        <asp:GridView ID="GridDetails" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None"
                            AutoGenerateColumns="False" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." Width="100%" OnRowDataBound="GridDetails_RowDataBound" OnRowCommand="GridDetails_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Alert Type">
                                    <ItemTemplate>

                                        <asp:Label ID="lablAlertType" runat="server" Text='<%#Eval("AlertType") %>'></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Alert Type Id" Visible="false">
                                    <ItemTemplate>

                                        <asp:Label ID="labAlertTypeId" runat="server" Text='<%#Eval("AlertTypeId") %>'></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Enable">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="IsEmailChkBx" runat="server" CssClass="toggleswitch" data-on-text="Yes" data-off-text="No" data-size="small" Visible="false" Checked="false" />
                                        <asp:ImageButton ID="btnyes" runat="server" ImageAlign="Middle" ImageUrl="~/image/bigYesNew.png" Visible="False" CommandName="Yes" />
                                        <asp:ImageButton ID="btnNo" runat="server" ImageAlign="Middle" ImageUrl="~/image/BigNoNew.png" Visible="False" CommandName="No" />
                                        <asp:Label ID="lblenable" runat="server" Text='<%#Eval("IsEmail") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <div>
                        <asp:Button ID="btnsubmit" runat="server" Text="Apply Changes" CssClass="btn btnd btncompt" OnClick="btnsubmit_Click" />
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btnd btncompt" OnClick="btncancel_Click" OnClientClick="return confirm('Are you sure you want to leave the page?');" />
                    </div>
                </div>
            </div>
        </div>
     
</asp:Content>
