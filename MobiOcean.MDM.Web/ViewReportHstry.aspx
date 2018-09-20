<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ViewReportHstry.aspx.cs" Inherits="MobiOcean.MDM.Web.ViewReportHstry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" Runat="Server">
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
                                <h1 class="pull-left page-title">Profile Detail</h1>
                            </div>
                            <div class="col-sm-2 pull-right">
                                <h3 style="display: inline-block;">
                                    <button title="" data-placement="left" data-toggle="tooltip" class="btn btn-default circleicon colorgrey" type="button" data-original-title="Add SubAdmin"><i class="fa fa-user-plus"></i></button>
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
                                     <div class="col-sm-4">
                                    Profile Name:  <asp:Label ID="lblProfileName"  runat="server" ></asp:Label>
                                         </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlCreationDate" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCreationDate_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                         <div class="col-sm-4" style="text-align:right">
                                        <asp:Button ID="btnback" runat="server" CssClass="btn btn-info waves-effect waves-light" Text="Back" PostBackUrl="~/Report.aspx" />
                                    </div>
                                   <%-- <div class="col-sm-4">
                                        <asp:LinkButton ID="lnkbtnviewhistory" runat="server" CssClass="btn-link" CommandName="view" Text="View History"></asp:LinkButton>
                                    </div>--%>
                                </div>
                                <div class="row">
                                    <br />
                                </div>
                                <div class="row">
                                    <div class="table-responsive" data-pattern="priority-columns">
                                        <asp:DataList ID="dlReport" runat="server" Width="99.9%" DataKeyField="ProfileFeatureMappingId" OnItemDataBound="dlReport_ItemDataBound" class="table table-small-font table-bordered table-striped">


                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <h4>
                                                            <asp:Label ID="lblFeatureName" runat="server" Text='<%#Eval("FeatureName") %>'></asp:Label></h4>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <b>Current Status:</b>
                                                        <asp:Label ID="lblIsEnable" runat="server" Text='<%#Eval("IsEnable").ToString()=="1"?"Enabled":"Disabled" %>'></asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <b>Notification:</b>
                                                        <asp:Label ID="lblNotification" runat="server" Text='<%#Eval("NotificationOn").ToString()=="1"?"On":"Off" %>'></asp:Label>
                                                    </div>
                                                    <%--<div  class="col-sm-10"><br /></div>--%>
                                                    <div class="col-sm-4">
                                                        <b>Log:</b>
                                                        <asp:Label ID="lblLogon" runat="server" Text='<%#Eval("LogOn").ToString()=="1"?"On":"Off" %>'></asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <b>Auto Sync:</b>
                                                        <asp:Label ID="lblAutoSync" runat="server" Text='<%#Eval("AutoSyncOn").ToString()=="1"?"On":"Off" %>'></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <br />
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-2">&nbsp;</div>
                                                    <div class="col-sm-8">
                                                        <div class="table-responsive" data-pattern="priority-columns">
                                                            <asp:GridView ID="grdrpt" runat="server" class="table table-small-font table-bordered table-striped mGrid"
                                                                DataKeyNames="ProfileFeatureTimingId" AutoGenerateColumns="false" EmptyDataText="No Schedule Available!">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Profile Id" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblProfileFeatureMappingId" runat="server" Text='<%#Eval("ProfileFeatureTimingId")%>'></asp:Label>
                                                                        </ItemTemplate>

                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Day">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFromDay" runat="server" Text='<%#Eval("FromDay").ToString()=="1"?"Monday":Eval("FromDay").ToString()=="2"?"Tuesday":Eval("FromDay").ToString()=="3"?"Wednesday":Eval("FromDay").ToString()=="4"?"Thursday":Eval("FromDay").ToString()=="5"?"Friday":Eval("FromDay").ToString()=="6"?"Saturday":"Sunday"%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="From Time">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFromTime" runat="server" Text='<%#Eval("FromTime")%>'></asp:Label>
                                                                        </ItemTemplate>

                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="To Time">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblToTime" runat="server" Text='<%#Eval("ToTime")%>'></asp:Label>
                                                                        </ItemTemplate>

                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Duration">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDuration" runat="server" Text='<%#(string.IsNullOrEmpty(Eval("Duration").ToString()))?"---":Eval("Duration")%>'></asp:Label>
                                                                        </ItemTemplate>

                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status").ToString()=="0"?"Enabled":"Disabled"%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">&nbsp;</div>

                                                </div>



                                            </ItemTemplate>

                                        </asp:DataList>
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
    <%--</form>--%>
</asp:Content>
