<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="Feedback.aspx.cs" Inherits="MobiOcean.MDM.Web.Feedback" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ScriptManager>--%>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->
         <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 bhoechie-tab scrollbar" id="style-3">  
        <div class="content-page">
            <!-- Start content -->
            <div class="content padding-top-none">
                <!-- Page-Title -->
                <div class="container whitebg padding-top-20">
                    <div class="row margin-none">
                        <div class="col-sm-12">
                            <div class="col-sm-10">
                                <h1 class="pull-left page-title">Feedback</h1>
                            </div>
                            <div class="col-sm-2 pull-right">
                                <h3 style="display: inline-block;">
                                    <%--<button title="" data-placement="left" data-toggle="tooltip" class="btn btn-default circleicon colorgrey" type="button" data-original-title="Add SubAdmin"><i class="fa fa-user-plus"></i></button>--%>
                                </h3>
                            </div>
                        </div>
                    </div>
                </div>

                <%--<div class="container margin-top-20 margin-bottom-20">
				<div class="row margin-none">
					<div class="col-sm-12 col-md-12 col-lg-12">
					 <div id='chart_div'></div>  
					</div>
				</div>
			</div>--%>
                <!-- Start content -->
                <div class="content m-t-20">
                    <div class="container">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="m-b-30">
                                            <%--<a id="addToTable" href="AssignGroupToUser.aspx" class="btn btn-primary waves-effect waves-light">Assign Group to User <i class="fa fa-plus"></i></a>--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="panel-group panel-group-joined" id="accordion-test">
                                            
                                                <div id="collapseTwo" class="panel-collapse collapse in">
                                                    
                                                        <div class="row">
                                                            <div class="col-sm-12" style="text-align: center">
                                                                <div class="dataTables_length" id="datatable-editable_length">
                                                                    <label>
                                                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>

                                                                    </label>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row" style="text-align: center">
                                                            <div class=" form">
                                                                <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>

                                                                <div class="form-group ">

                                                                    <div class="col-lg-4">
                                                                        <label>
                                                                            From Date :
                                                                    <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFrmDate" Format="dd MMM yyyy" PopupButtonID="txtFrmDate" />
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group ">

                                                                    <div class="col-lg-4">
                                                                        <label>
                                                                            To Date :
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
                                                                <div class="col-lg-10">
                                                                    <br />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="table-responsive" data-pattern="priority-columns">

                                                            <asp:GridView ID="grdFeedback" runat="server" class="table table-small-font table-bordered table-striped mGrid" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdFeedback_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("FeedbackId")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
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
                                                                    <asp:TemplateField HeaderText="Feedback">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFeedback" runat="server" Text='<%#Eval(("Feedback"))%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Feedback DateTime">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Format(Eval("CreationDate").ToString())%>'></asp:Label>
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
                                    </div>
                                </div>
                                <!-- end: page -->
                            </div>
                            <!-- end Panel -->
                        </div>
                        <!-- container -->
                    </div>
                    <!-- content -->
                    <!-- container -->
                </div>
                <!-- content -->

            </div>
        </div>
        </div>
            
	<!-- ============================================================== -->
        <!-- End Right content here -->
        <!-- ============================================================== -->
    <%--</form>--%>
</asp:Content>

