<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AssignUser.aspx.cs" Inherits="MobiOcean.MDM.Web.AssignUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active div">
                <div class="profile1" style="margin: 0px;">
                    Assign User
                    <a class="btn btn-sky text-uppercase custom-add-profile pull-right" href="FileUpload.aspx"><i class="fa fa-backward"></i> Back</a>
                </div>
                <br />
                <br />
                <div class=" row">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="col-lg-12">
                                    <asp:TextBox ID="UserCode" runat="server" CssClass="form-control" placeholder="User Code"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="col-lg-12">
                                    <asp:TextBox ID="UserName" runat="server" CssClass="form-control" placeholder="User Name"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="col-lg-12">
                                    <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" placeholder="Branch"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="col-lg-12">
                                    <asp:TextBox ID="txtDept" runat="server" CssClass="form-control" placeholder="Department"></asp:TextBox>
                                </div>
                            </div>
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
                <div class="col-md-12">
                    <asp:Label ID="FileName" runat="server" Text="File Name:"></asp:Label>
                    <asp:Label ID="File" runat="server" Text="Test" Font-Bold="true"></asp:Label>
                </div>
                <br />
                <br />

                <div class="table-responsive">
                    <asp:GridView ID="grdUsr" runat="server" GridLines="None" class="table mGrid" AutoGenerateColumns="false"
                        HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                        OnPageIndexChanging="grdUsr_PageIndexChanging" Width="100%" PageSize="10" AllowPaging="true" OnRowDataBound="grdUsr_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
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
                                    <asp:CheckBox ID="User" runat="server" Checked='<%#string.IsNullOrEmpty(Eval("AssignUser").ToString() ) ? false: Eval("Permission").ToString() == "0" ? false : true   %>' OnCheckedChanged="User_CheckedChanged" AutoPostBack="true" />
                                    <%-- OnCheckedChanged="User_CheckedChanged" AutoPostBack="true"--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Permission">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Read" Text="Read" runat="server" OnCheckedChanged="Read_CheckedChanged" AutoPostBack="true" Checked='<%# string.IsNullOrEmpty(Eval("Permission").ToString())?false: Eval("Permission").ToString() == "1" ?true :Eval("Permission").ToString() == "3" ?true :Eval("Permission").ToString() == "5" ?true :Eval("Permission").ToString() == "7"?true:false%>' />
                                    &nbsp;
                                             <asp:CheckBox ID="Write" Text="Write" runat="server" OnCheckedChanged="Write_CheckedChanged" AutoPostBack="true" Checked='<%#string.IsNullOrEmpty(Eval("Permission").ToString())?false:Eval("Permission").ToString() == "2" || Eval("Permission").ToString() == "3" || Eval("Permission").ToString() == "6" || Eval("Permission").ToString() == "7"%>' />
                                    &nbsp;
                                            <asp:CheckBox ID="Modify" Text="Modify" runat="server" OnCheckedChanged="Modify_CheckedChanged" AutoPostBack="true" Checked='<%#string.IsNullOrEmpty(Eval("Permission").ToString())?false:Eval("Permission").ToString() == "4" || Eval("Permission").ToString() == "5" || Eval("Permission").ToString() == "6" ||Eval("Permission").ToString() == "7"%>' />
                                    &nbsp;   
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="UserCode">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("UserCode")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UserName">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Email Id">
                                <ItemTemplate>
                                    <asp:Label ID="EmailId" runat="server" Text='<%#Eval("EmailId")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="Country" runat="server" Text='<%#Eval("Country")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="ProfileImagePath">
                                        <ItemTemplate>
                                            <asp:Label ID="ProfileImagePath" runat="server" Text='<%#Eval("ProfileImagePath")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                    </asp:GridView>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="AssignUserList" runat="server" CssClass="btn btnd btncompt" Text="Assign User List" OnClick="AssignUserList_Click" />
                    &nbsp; &nbsp;
                            <asp:Button ID="Cancel" runat="server" CssClass="btn-danger" OnClick="Cancel_Click" Text="Cancel" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>


