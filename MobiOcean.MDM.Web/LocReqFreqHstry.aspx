<%@ Page Title="Loc Req Freq History" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="LocReqFreqHstry.aspx.cs" Inherits="MobiOcean.MDM.Web.LocReqFreqHstry" %>
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
        <div class="content-page">
            <!-- Start content -->
            <div class="content padding-top-none">
                <!-- Page-Title -->
                <div class="container whitebg padding-top-20">
                    <div class="row margin-none">
                        <div class="col-sm-12">
                            <div class="col-sm-10">
                                <h1 class="pull-left page-title">Location Frequency History</h1>
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
                                    <div class="col-sm-12">
                                        <div class="m-b-30" style="text-align:right">
                                            <asp:Button ID="btnBackToLocationMgmt" runat="server" Text="Back" CssClass="btn btn-primary waves-effect waves-light" OnClick="btnBackToLocationMgmt_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="panel-group panel-group-joined" id="accordion-test">
                                            <div class="panel panel-default margin-top-20">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion-test" href="#collapseTwo">Location Frequency History
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="collapseTwo" class="panel-collapse collapse in">
                                                    <div class="panel-body table-rep-plugin">                                                       
                                                       <div class="table-responsive" data-pattern="priority-columns">

                                                            <asp:GridView ID="grdHstry" runat="server" class="table table-small-font table-bordered table-striped mGrid" AutoGenerateColumns="false"
                                                                EmptyDataText="No Record Found" ShowHeader="true" ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdHstry_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("LocReqFreqHstryId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Group Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGroupName" runat="server" Text='<%#Eval("GrouppName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Strt Time">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStrtTime" runat="server" Text='<%#Eval("StrtTime") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="End Time">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEndTime" runat="server" Text='<%#Eval("EndTime") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Time Interval(In Minutes)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTime" runat="server" Text='<%#Eval("LocReqFrequency") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Frequency">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblfrequency" runat="server" Text='<%#Eval("ttlNoOfReq") %>'></asp:Label>
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
        <!-- ============================================================== -->
        <!-- End Right content here -->
        <!-- ============================================================== -->
<%--    <%--</form>--%>--%>
</asp:Content>


