<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="DemoProduct.aspx.cs" Inherits="MobiOcean.MDM.Web.DemoProduct" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">


    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">

                <div class="profile1">&nbsp;&nbsp;Demo Product</div>

                <div class="dataTables_length" id="datatable-editable_length">
                    <label>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>

                    </label>
                    </div>
                    <div class="content padding-top-none">

                        <div class="row" style="text-align: center">
                            <div class=" form">

                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            From Date:
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy" PopupButtonID="txtFromDate" />
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            To Date:
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:CalendarExtender ID="ce2" runat="server" TargetControlID="txtToDate" Format="dd MMM yyyy" PopupButtonID="txtToDate" />
                                        </label>
                                    </div>
                                </div>


                                <div class="form-group ">
                                    <div class="col-lg-4">
                                        <label>
                                            <br />
                                            <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="table-responsive">


                            <asp:GridView ID="grdDemoproduct" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No reocrd found." AllowPaging="true" PageSize="20" OnPageIndexChanging="grdDemoproduct_PageIndexChanging" GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Demo Done">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFeedback" runat="server" Text='<%#(Eval("IsDemoDone").ToString()=="0"?"No":"Yes")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblEIsDemoDone" runat="server" Text='<%#(Eval("IsDemoDone"))%>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlIsDemoDone" runat="server">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DateTime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateTime" runat="server" Text='<%#Format(Eval("CreationDate").ToString())%>'></asp:Label>
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
    </div>

</asp:Content>

