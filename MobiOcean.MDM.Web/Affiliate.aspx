<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="Affiliate.aspx.cs" Inherits="MobiOcean.MDM.Web.Affiliate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ScriptManager>--%>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">

                <div class="profile1">&nbsp;&nbsp;Affiliate Details</div>

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
                                                                <asp:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy" PopupButtonID="txtFromDate"/>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                           To Date:
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:CalendarExtender ID="ce2" runat="server" TargetControlID="txtToDate" Format="dd MMM yyyy" PopupButtonID="txtToDate"/>
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


                            <asp:GridView ID="grdCont" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false"
                                ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Record found." DataKeyNames="PartnerId"
                                OnPageIndexChanging="grdCont_PageIndexChanging" Width="100%" PageSize="20" AllowPaging="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("PartnerId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompany_Name" runat="server" Text='<%# Eval("CompanyName")%>'></asp:Label>
                                        </ItemTemplate>                                      
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>                                  
                                   <asp:TemplateField HeaderText="Industry">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTypeOfIndustry" runat="server" Text='<%#Eval("Industry")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Details")%>'></asp:Label>
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
    <%--</form>--%>
                                      
</asp:Content>
