<%@ Page Title="Location on Map" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="DeviceLocation.aspx.cs" Inherits="MobiOcean.MDM.Web.DeviceLocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="cnthesd" runat="server" ContentPlaceHolderID="ContentHead">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">


    <asp:UpdatePanel ID="up1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>


            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">
                    <!-- flight section -->
                    <div class="bhoechie-tab-content active">

                        <div class="profile1" style="margin: 0px;">
                            Location Report
                        <a href="UserLocationList.aspx" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-eye custom-table-fa"></i>&nbsp;&nbsp;<span>Show On Table</span></a>
                            <div class="clearfix"></div>
                        </div>


                        <div class="dataTables_length" id="datatable-editable_length" style="text-align: center">
                            <label>
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </label>
                        </div>



                        <div class="row" style="text-align: center">
                            <div class=" form">

                                <div class="form-group ">
                                    <div class="col-lg-8">
                                        <div class="col-lg-6">
                                            <label>
                                                User :
                                          <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged"></asp:DropDownList>
                                            </label>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>
                                                Device :
                                                                        <asp:TextBox ID="txtDeviceName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </label>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>
                                                From Date :
                                                                        <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control"></asp:TextBox>
                                               
                                            </label>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>
                                                To Date :
                                                                        <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control"></asp:TextBox>
                                                
                                            </label>
                                        </div>

                                    </div>


                                    <div class="col-lg-4">
                                        <br />
                                        <br />
                                        <label>
                                            <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
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
                                                    <div id="map" style="height:500px;z-index:1;"></div>
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
   <%-- <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false"></script>
    <script src="Scripts/GoogleMap.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            //initialize('21.0000', '78.0000');
            //intialize();
            var center = ['21.0000', '78.0000']
            LoadMap('map', center);
        });
           function initialize() {
            var pts = [];
            var center = ['21.0000', '78.0000']
            LoadMap('map', center);
        }
    </script>
     <script>
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrmDt],[id$=txtToDt]").datepick({
                    dateFormat: 'dd M yyyy'
                });
                $('#style-3').scroll(function () {
                    $("[id$=txtFrmDt],[id$=txtToDt]").datepick("hide");
                });
            });
        }
    </script>
</asp:Content>
