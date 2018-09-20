<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FirstLoginChanges.aspx.cs"
    Inherits="MobiOcean.MDM.Web.FirstLoginChanges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">


    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active div">
                <div class="profile1">&nbsp;&nbsp;Select the Feature which You want to try(Trial period is 10 days) </div>
                <br />

                <div class="table-responsive">
                    <asp:GridView ID="CategoryLt" runat="server" GridLines="None" class="table mGrid" AutoGenerateColumns="false"
                        HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                        Width="100%">
                        <Columns>

                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCatyId" runat="server" Text='<%#Eval("CategoryId")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Select">
                             <%--   <HeaderTemplate>
                                    <asp:CheckBox ID="CatyEnbl_header" runat="server" AutoPostBack="true" OnCheckedChanged="CatyEnbl_header_CheckedChanged" />
                                </HeaderTemplate>--%>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CatyEnbl" runat="server"  />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Feature">
                                <ItemTemplate>
                                    <asp:Label ID="lblCatyName" runat="server" Text='<%#Eval("CategoryName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>

                <div class="row">
                    <div class="col-sm-12">
                        <asp:Button ID="Assign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Apply" ValidationGroup="save" OnClick="ApplyChanges_Click" />
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
