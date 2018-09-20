<%@ Page Title="Secure Communication" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="PWCcommuniction.aspx.cs" Inherits="MobiOcean.MDM.Web.PWCcommuniction" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">

                    <div class="bhoechie-tab-content active div">

                         <div class="profile1" style="margin: 0px;">
                    Secure Communication
                    

                    <div class="clearfix"></div>
                </div>

                       
                        <br />
                        <div class="row"  style="text-align: center">
                           <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <br />
                            <asp:GridView ID="gridmsg"  GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable" runat="server" AllowPaging="false"
                                 ShowHeader="true" ShowHeaderWhenEmpty="true"  Visible="false"></asp:GridView>
                        </div>
                        <br>
                       
                            <div class="table-responsive">
                                <asp:GridView ID="grdUser" runat="server" DataKeyNames="LomentId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                    AllowPaging="false" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                    EmptyDataText="No record found." OnRowDataBound="grdUser_RowDataBound" OnPageIndexChanging="grdUser_PageIndexChanging">

                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Regiter">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="AchkRow_Parents" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="AchkRow_Parents" runat="server" AutoPostBack="true" OnCheckedChanged="AchkRow_Parents_CheckedChanged" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="AchkHeader_Parents" runat="server" AutoPostBack="true" OnCheckedChanged="Achk_CheckedChanged" />
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LomentId Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLomentId" runat="server" Text='<%#Eval("LomentId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblEmailid" runat="server" Text='<%#Eval("Emailid")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                                <asp:Label ID="lblMobileno" runat="server" Text='<%#Eval("MobileNo")%>' Visible="false"></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Keys">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkeys" runat="server" Text='<%#string.IsNullOrEmpty(Eval("Keys").ToString())?"---":Eval("Keys")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ISChanged Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblISChecked" runat="server" Text="0"></asp:Label>
                                                <asp:Label ID="lblIsP" runat="server" Text="0"></asp:Label>
                                                <asp:Label ID="lblIsW" runat="server" Text="0"></asp:Label>
                                                <asp:Label ID="lblIsC" runat="server" Text="0"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IsRegistered" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="IsPeanut" runat="server" Text='<%#Eval("Peanut")%>'></asp:Label>
                                                <asp:Label ID="IsWalnut" runat="server" Text='<%#Eval("Walnut")%>'></asp:Label>
                                                <asp:Label ID="IsCashew" runat="server" Text='<%#Eval("Cashew")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IsRegistered" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpwd" runat="server" Text='<%#Eval("Password")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Peanut">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="AchkRow_Parents1" AutoPostBack="true" OnCheckedChanged="AchkRow_Parents1_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UnLink Peanut Device">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUnLinkPeanut" runat="server" Text="Unlink Peanut Device" OnClick="lnkUnLinkPeanut_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Walnut">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="AchkRow_Parents2" AutoPostBack="true" OnCheckedChanged="AchkRow_Parents2_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UnLink Walnut Device">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUnLinkWalnut" runat="server" Text="Unlink Walnut Device" OnClick="lnkUnLinkWalnut_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cashew">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="AchkRow_Parents3" AutoPostBack="true" OnCheckedChanged="AchkRow_Parents3_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UnLink Cashew Device">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUnLinkCashew" runat="server" Text="Unlink Cashew Device" OnClick="lnkUnLinkCashew_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Button ID="Assign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Apply" ValidationGroup="save" OnClick="ApplyChanges_Click" />
                                    <asp:Button ID="Cancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="Cancel_Click" />
                                </div>
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
</asp:Content>

