<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="APKUpload.aspx.cs" Inherits="MobiOcean.MDM.Web.APKUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">
                    <div class="bhoechie-tab-content active div">
                        <div class="profile1" style="margin: 0px;">
                            APK Upload
                        </div>
                        <br />
                        <br />
                        <div class=" row">

                            <div class="col-md-12">

                                <div class="form-group">
                                    <div class="col-md-6">
                                        <label for="bname" class="control-label col-lg-4">Version Name : </label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv"  runat="server" ControlToValidate="txtName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-6">
                                        <label for="bname" class="control-label col-lg-4">Upload APK : </label>
                                        <div class="col-lg-8">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ControlToValidate="FileUpload1" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-12 text-center">
                                        <asp:Button ID="btnSaveForm" runat="server" Text="Save" CssClass="btn btnd btncompt" OnClick="btnAdd_Click" ValidationGroup="upload" />&nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                        <br /><br />
                        <div class="table-responsive">
                            <asp:GridView ID="grdAPK" runat="server" GridLines="None" class="table mGrid" AutoGenerateColumns="false"
                                HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                                OnPageIndexChanging="grdAPK_PageIndexChanging" Width="100%" PageSize="10" AllowPaging="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("Version_Id")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Version Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVersionName" runat="server" Text='<%#Eval("Version_No")%>'></asp:Label>
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


</asp:Content>

