<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="MobiOcean.MDM.Web.CategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active div">

                <div class="profile1">&nbsp;&nbsp;Category User Mapping  </div>
                <br />


               <div class="row" style="text-align: center">
                    <div class=" form">
                        <div class="form-group col-lg-12">

                        <div class="col-md-6">
                            <label>
                                <b>Category Name :</b>
                                <br>
                                <asp:DropDownList ID="CategoryLt" AutoPostBack="true" OnSelectedIndexChanged="CategoryLt_SelectedIndexChanged" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList></label>
                        </div>

                        <div class="col-md-3">
                            <label>
                                License :
                    <asp:TextBox ID="txtLicense" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                            </label>
                        </div>

                        <div class="col-md-3">
                            <label>
                                Expiry Date :
                    <asp:TextBox ID="txtExpDate" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                            </label>
                        </div>

</div>
                    </div>
                </div>




                <div class="row">
                    <div class="col-sm-12" style="text-align: center">
                        <div class="dataTables_length" id="datatable-editable_length">
                            <label>
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="UserList" runat="server" GridLines="None" class="table mGrid" AutoGenerateColumns="false"
                        HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                        OnPageIndexChanging="UserList_PageIndexChanging" OnRowDataBound="UserList_RowDataBound"
                        Width="100%" PageSize="10" AllowPaging="true">
                        <Columns>
                            <%--  <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Select">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="UserEnbl_header" runat="server" AutoPostBack="true" OnCheckedChanged="UserEnbl_header_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="UserEnbl" runat="server" AutoPostBack="true" OnCheckedChanged="UserEnbl_CheckedChanged" Checked='<%#string.IsNullOrEmpty(Eval("Status").ToString())? false :Eval("Status").ToString() == "1" ?false : Eval("Status").ToString() == "0" ? true: false%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="UserName">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
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
