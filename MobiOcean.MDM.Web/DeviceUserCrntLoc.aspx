<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DeviceUserCrntLoc.aspx.cs" Inherits="MobiOcean.MDM.Web.DeviceUserCrntLoc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <asp:UpdatePanel ID="up1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">
                    <!-- flight section -->
                    <div class="bhoechie-tab-content active">

                        <div class="profile1" style="margin: 0px;">
                            User Current Location Report
                        <a href="UserCurrentLocation.aspx" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-eye custom-table-fa"></i>&nbsp;&nbsp;<span>Show On Table</span></a>

                            <%--                    <%if (!IsPostBack)
                        {
                            %><asp:HiddenField ID="HiddenField1" runat="server" OnDataBinding="HiddenField1_Load" />
                    
                    <%
                            HiddenField1.Value = "s";
                            HiddenField1.DataBind(); } %>--%>

                            <div class="clearfix"></div>
                        </div>

                        <div class="dataTables_length" id="datatable-editable_length" style="text-align: center">
                            <label>
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </label>
                        </div>
                        <br />

                        <br />
                        <!-- Start content -->





                        <div class="row" style="text-align: center">
                            <div class=" form">

                                <div class="form-group ">
                                    <div class="col-lg-8">
                                        <div class="col-lg-6">
                                            <label>
                                                By User :
                                             <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged"></asp:DropDownList>
                                            </label>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>
                                                By Device :
                                                                    
                                                <asp:TextBox ID="txtDeviceName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </label>
                                        </div>

                                    </div>

                                    <%--<div class="col-lg-4">
                                                                    <br />
                                                                </div>--%>
                                    <div class="col-lg-4">
                                        <br />
                                        <br />
                                        <label>
                                            <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                        </label>
                                    </div>
                                    <div class="col-lg-12">
                                        <br />
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div style="text-align: justify; vertical-align: top; margin-left: 5px; margin-right: 10px;">
                                                <asp:Label runat="server" ID="lblManFields" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-lg-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <%--<h3 class="panel-title">Device Latest location at 17:30pm,1 Dec 2015</h3> --%>
                                                </div>
                                                <div class="panel-body">
                                                    <%-- <div id="map_canvas"></div>--%>
                                                    <div id="map" style="height: 500px; z-index: 1;"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            var center = ['21.0000', '78.0000']
            LoadMap('map', center);
            var dt =<%=ShowOnMaps() %>;
            PointMap(dt);
        });
        function PointMap(dt) {
            RemoveMarkersFromArray();
            var myLogDateTime = "";
            var isFirstTime = 1;
            $.each(dt, function (i, d) {
                //if (i != 0) {
                //    myLogDateTime = myLogDateTime + ",<br>" + d["LogDateTime"];
                //}
                //else {
                myLogDateTime = d["LogDateTime"];
                //}
                ShowOnMapCrntLoc(isFirstTime, myLogDateTime, d["Location"], d["Latitude"], d["Longitude"],0,d["UserName"]);
                isFirstTime = 0;
            });
            showAllMarkersOnWindow();
        }
        function initialize() {
            var pts = [];
            var center = ['21.0000', '78.0000']
            LoadMap('map', center);
        }
    </script>
</asp:Content>
