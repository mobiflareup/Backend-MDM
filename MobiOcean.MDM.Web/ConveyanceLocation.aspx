<%@ Page Title="Conveyance Location Report" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="ConveyanceLocation.aspx.cs" Inherits="MobiOcean.MDM.Web.ConveyanceLocation" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">

                <div class="profile1" style="margin: 0px;">
                    Conveyance Location Report
                      <a href="ConveyanceReport.aspx" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><span>Back</span></a>
                            
                    <div class="clearfix"></div>
                </div>


                <div class="dataTables_length" id="datatable-editable_length" style="text-align: center">
                    <label>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </label>
                </div>
                <div class="row">

                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <%--<h3 class="panel-title">Device Latest location at 17:30pm,1 Dec 2015</h3> --%>
                            </div>
                            <div class="panel-body">
                                <%--<div id="map_canvas"></div>--%>
                                 <div id="map_canvas" style="height:500px;z-index:1;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

       <%--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCNqRD46c7aZ_EoOHJ83CxvuG99bRT7Dts&sensor=false"></script>--%>

    <script type="text/javascript">

        var data = JSON.parse('<%=getData() %>');
        var stops = [];
        $.each(data, function (i, location) {
            stops.push({ "Geometry": { "Latitude": location.Latitude, "Longitude": location.Longitude, "Desc": location.Location, "LogDateTime": location.LogDateTime } });
        });
        //var map = new window.google.maps.Map(document.getElementById("map_canvas"));

       
        
       
        if ($("#hiddenclientapi").val() == "GOOGLE") {

            var map = new window.google.maps.Map(document.getElementById("map_canvas"));
            var directionsDisplay = new window.google.maps.DirectionsRenderer({
                suppressMarkers: true,
                suppressInfoWindows: true,
                polylineOptions:
                {
                    strokeColor: "Green"
                }
            });
            var directionsService = new window.google.maps.DirectionsService();

            var iconImage = new google.maps.MarkerImage('GglImg/bus_marker.png',
              new google.maps.Size(20, 34),
              new google.maps.Point(0, 0),
              new google.maps.Point(9, 34));

            var iconShadow = new google.maps.MarkerImage('https://www.google.com/mapfiles/shadow50.png',
                new google.maps.Size(37, 34),
                new google.maps.Point(0, 0),
                new google.maps.Point(9, 34));

            var iconShape = {
                coord: [9, 0, 6, 1, 4, 2, 2, 4, 0, 8, 0, 12, 1, 14, 2, 16, 5, 19, 7, 23, 8, 26, 9, 30, 9, 34, 11, 34, 11, 30, 12, 26, 13, 24, 14, 21, 16, 18, 18, 16, 20, 12, 20, 8, 18, 4, 16, 2, 15, 1, 13, 0],
                type: 'poly'
            };
            Tour_startUp(stops);
            window.tour.loadMap(map, directionsDisplay);
            if (stops.length > 1)
                window.tour.calcRoute(directionsService, directionsDisplay);
        } else {
            if (stops.length > 0) {
                var center = [];
                center.push(stops[0]["Geometry"]["Latitude"]);
                center.push(stops[0]["Geometry"]["Longitude"]);
                LoadMap('map_canvas', center);
                Tour_startUp(stops);
            }
        }
       
       

    </script>
</asp:Content>

