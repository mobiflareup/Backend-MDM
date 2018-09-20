var map = null, poly, polygons = [], currentDiameter, circle, marker;
var infoWindowOpenClose = { "infoWindowOpen": 1, "infoWindowClose": 2 }
var isDraggableMarker = { "Yes": 1, "No": 2 }
var isContentHeaderOrNot = { "Yes": 1, "No": 2 }
var MarkerArray = [];
var markers = [];
var MapLoad = 3;
var AutoSuggest = 4;
var RouteDraw = 5;
function LoadMap(id, centrePts, editable, zoomControl, hybrid) {
    var center = new L.LatLng(centrePts[0], centrePts[1]);
    map = new MapmyIndia.Map(id, { center: center, editable: true, zoomControl: true, hybrid: false });
    addCount(MapLoad);
}
function addCount(Type) {

    $.ajax({
        url: 'api/GetAck/ApiCount?ClientId=' + $("#ClientIdApi").val() + '&Type=' + Type + '&IsUsed=' + $("#hiddenclientapi").val() + '&UserId=' + $("#UserIdApi").val(),
        error: function (res) {
            //console.log(JSON.stringify(res));
        },
        dataType: 'json',
        success: function (data) {
            //alert(data);
        },
        type: 'GET'
    });
}
function LoadMap1(markers) {
    var popup = "", str = "";
    var center = new L.latLng(markers[0].lat, markers[0].lng);
    for (var i = 0; i < markers.length; i++) {
        var data = markers[i];
        var pt1 = new L.latLng(data.lat, data.lng);
        str = data.marker;
        str = str.replace("image/", "");
        str = str.replace(".png", "");
        var objImage = CreateImageObject(str, 24, 24, 10, 12);
        var mk = addMarker(pt1, objImage, "Hey There", false)
        if (data.description != "surveillance camera")
        {
        popup = mk.bindPopup("<div class='info-div'><b><i>Location Details</i></b><br><b>Time    :</b><br>" + data.description + "<br><b>Address :</b><br>" + data.title + "<br><b>Mobile No :</b><br>" + data.MobileNo + "<div>");
        }
    }
    mk.on("click", function (e) {

        if (data.description != "surveillance camera") {
            popup;
        } else {
            window.open('http://59.181.96.250:220/', '_blank');
        }
    });

    
}
function create_polygons_editable(pts) {

    var patArr = [];
    for (var i = 0; i < pts.length; i++) {
        patArr.push(new L.LatLng(pts[i][0], pts[i][1]));
    }
    if (!poly) {
        poly = new L.polygon(patArr, { color: 'red', draggable: true }).addTo(map);
        poly.enableEdit();
        polygons.push(poly);
    }
    //var data = map.fitBounds(poly.getBounds());

}
function DrawLine(pts) {

}
function DrawCircle(pts, radius) {
    if (currentDiameter)
        map.removeLayer(currentDiameter);
    lat = pts["0"];
    lon = pts["1"];
    if (lat != '' && lon != '' && radius != '') {
        if (checkValidLatlong(parseFloat(lat), parseFloat(lon))) {
            if (marker)
                marker.setLatLng([lat, lon]);
            currentDiameter = L.circle([lat, lon], {
                color: 'pink',
                fillColor: '#FFC0CB',
                fillOpacity: 0.5,
                radius: radius
            });
            circle = currentDiameter.addTo(map);
            circle.enableEdit();
            circle.on('dragend', function (event) {
                var position = event.target.getLatLng();
                document.getElementById('ContentBody_hdnStartLat').value = position.lat;
                document.getElementById('ContentBody_hdnStartLong').value = position.lng;
                document.getElementById('ContentBody_hdnRadius').value = circle.getRadius();
                document.getElementById('ContentBody_txtRadius').value = circle.getRadius();//document.getElementById('ContentBody_hdnRadius').value;
                var center = [position.lat, position.lng];
                DrawCircle(center, document.getElementById('ContentBody_txtRadius').value);
            });
            circle.on("editable:vertex:dragend", function (e) {
                document.getElementById('ContentBody_hdnRadius').value = circle.getRadius();
                document.getElementById('ContentBody_txtRadius').value = circle.getRadius();
            });

            map.fitBounds(currentDiameter.getBounds());
        }
    }
    else {
        //document.getElementById('result').innerHTML = "Insufficient parameters";
        alert("Insufficient parameters");
    }
}
function DrawCircles(pts, radius) {
    if (currentDiameter)
        map.removeLayer(currentDiameter);
    lat = pts["0"];
    lon = pts["1"];
    if (lat != '' && lon != '' && radius != '') {
        if (checkValidLatlong(parseFloat(lat), parseFloat(lon))) {
            if (marker)
                marker.setLatLng([lat, lon]);
            currentDiameter = L.circle([lat, lon], {
                color: 'pink',
                fillColor: '#FFC0CB',
                fillOpacity: 0.5,
                radius: radius
            });
            circle = currentDiameter.addTo(map);
            circle.enableEdit();
            circle.on('dragend', function (event) {
                var position = event.target.getLatLng();
                //document.getElementById('ContentBody_hdnStartLat').value = position.lat;
                //document.getElementById('ContentBody_hdnStartLong').value = position.lng;
                // document.getElementById('ContentBody_hdnRadius').value = circle.getRadius();
                //document.getElementById('ContentBody_txtRadius').value = circle.getRadius();//document.getElementById('ContentBody_hdnRadius').value;
                var center = [position.lat, position.lng];
                DrawCircle(center, document.getElementById('ContentBody_txtRadius').value);
            });
            circle.on("editable:vertex:dragend", function (e) {
                document.getElementById('ContentBody_hdnRadius').value = circle.getRadius();
                document.getElementById('ContentBody_txtRadius').value = circle.getRadius();
            });

            map.fitBounds(currentDiameter.getBounds());
        }
    }
    else {
        //document.getElementById('result').innerHTML = "Insufficient parameters";
        alert("Insufficient parameters");
    }
}
function DrawPolygon(pts) {

}
function DrawRectangle(pts) {

}
function DrawMarker(centre) {

}
function checkValidLatlong(lat, lon) {
    /*check for input as valid lat lon*/
    if (lat >= 180 || lat <= -180 || lon >= 180 || lon <= -180) {
        //document.getElementById('result').innerHTML = "Invalid Lat lon values";
        alert("Invalid Lat lon values");
        return false;
    } else {
        return true;
    }
}
$(document).ready(function () {
    $("#ContentBody_btnAssign").click(function () {
        var FPTS = "";
        for (i = 0; i < poly._latlngs['0'].length; i++) {
            FPTS += +poly._latlngs['0'][i]['lat'] + "," + poly._latlngs['0'][i]['lng'] + ",";
        }
        document.getElementById('info').innerHTML = FPTS;
        document.getElementById('ContentBody_hidden').value = FPTS;
    })
});
function ShowOnMap(isFirstTime, Time, Address, Latitude, Longitude, ImgId) {

    if (Latitude != '0.0') {
        var imgName = "";
        if (ImgId == 0) {
            imgName = 'Map_greenMarker'
        }
        else {
            imgName = 'Map_RedMarker';
        }
        var toolTiptext = "Hey there!";

        var Content = '<b><i>Location Details</i></b><br>' +
                '<b>Time    :</b><br>' + Time + '<br>' +
                '<b>Address :</b><br>' + Address + '<br>';


        if (isFirstTime == 1) {

            var center = new L.LatLng(Latitude, Longitude);
            map = new MapmyIndia.Map('map', { center: center, editable: true, zoomControl: true, hybrid: false });
            addCount(MapLoad);
        }

        var objImage = CreateImageObject(imgName, 24, 24, 10, 12);
        var objLatLng = CreateLatLngObject(Latitude, Longitude);
        var mapCustomer = AddMarkerOnMap(objImage, objLatLng, 24, 24, toolTiptext, Content, infoWindowOpenClose.infoWindowClose, isDraggableMarker.No, isContentHeaderOrNot.No);

    }

}
function showAllMarkersOnWindow() {
    if (MarkerArray.length > 1) {
        for (var i in MarkerArray) {
           //bounds(MarkerArray[i].getPosition());
        }
       //map.fitBounds(bounds);
    }
}
function CreateImageObject(ImageName, sizeX, sizeY, anchorX, anchorY) {
    return icon = L.divIcon({
        className: 'my-div-icon',
        //html: "<img class='map_marker'  src=" + "'https://maps.mapmyindia.com/images/2.png'>" + '<span class="my-div-span">' + 1 + '</span>',
        html: "<img class='map_marker'  src=" + "images/" + ImageName + ".png>",
        //iconSize: [10, 10],
        //popupAnchor: [12, -10]
        iconSize: [sizeX, sizeY],
        popupAnchor: [anchorX, anchorY]
    });
}
function CreateImageObjectCount(ImageName, sizeX, sizeY, anchorX, anchorY, count) {
    return icon = L.divIcon({
        className: 'my-div-icon',
        html: "<img class='map_marker'  src=" + "'https://maps.mapmyindia.com/images/2.png'>" + '<span style="position: absolute;left:1.5em;right: 1em;top:0.9em;bottom:3em; font-size:10px;font-weight:bold;width: 1px; color:black;" class="my-div-span">' + count + '</span>',
        //html: "<img class='map_marker'  src=" + "images/" + ImageName + ".png>",
        //iconSize: [10, 10],
        //popupAnchor: [12, -10]
        iconSize: [sizeX, sizeY],
        popupAnchor: [anchorX, anchorY]
    });
}
function CreateLatLngObject(Latitude, Longitude) {
    var latlng = new L.latLng(Latitude, Longitude);
    return latlng;
}
function AddMarkerOnMap(objImage, objLatLng, objsizeX, objsizeY, toolTiptext, infoWindowContent, infoWindowOpenClose, isDraggable, isContent) {
    var dragFlag = true;
    if (isDraggable == 2) dragFlag = false;
    var pts = objLatLng;
    var postion = new L.LatLng(pts["lat"], pts["lng"]);
    var mk = addMarker(postion, objImage, toolTiptext, dragFlag);/*call the add marker function woith the position and title*/
    markers.push(mk);
   // map.setView(mk.getLatLng(), 8);

    var ContentHeader = '';
    if (isContent == 1) {
    }
    else if (isContent == 2) {
        ContentHeader = infoWindowContent;
    }
    if (infoWindowOpenClose == 1) {
        if (infoWindow) infoWindow.close();
        //infoWindow = new google.maps.InfoWindow({ content: ContentHeader, position: objLatLng, maxWidth: 200 });
        //infoWindow.open(map, marker);

    }
    else if (infoWindowOpenClose == 2) {
        // infoWindow = new google.maps.InfoWindow({ content: ContentHeader, position: objLatLng, maxWidth: 200 });

    }

    mk.on("click", function (e) {
        mk.bindPopup(ContentHeader);
    });
    mk.on("mouseover", function (e) {
        mk.bindPopup(ContentHeader);
    });
    mk.on("rightclick", function (e) {
        mk.bindPopup(ContentHeader);
    });

    MarkerArray.push(marker);

    return marker;
}
function addMarker(position, icon, title, draggable) {

    if (icon == '') {
        var mk = new L.Marker(position, { draggable: draggable, title: title });
        mk.bindPopup(title);
    } else {
        var mk = new L.Marker(position, { icon: icon, draggable: draggable, title: title });
        mk.bindPopup(title);
    }
    map.addLayer(mk);
    return mk;
}
function RemoveMarkersFromArray() {
    if (MarkerArray) {
        for (var i = 0; i < MarkerArray.length; i++) {
            MarkerArray[i].setMap(null);
        }
    }
    MarkerArray = new Array();
}
function GetLocationFromApi(TxtBoxid, Lat, Lng, Addr) {
    
    $("#" + TxtBoxid).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: 'api/GetAck/MapMyIndia?area=' + $("#" + TxtBoxid).val() + "&Id=" + $("#ClientIdApi").val(),
                dataType: "json",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    addCount(AutoSuggest);
                    var jsondata = JSON.parse(data);
                    result_string = '<div class="details-header">Auto Suggested Pois</div><div style="font-size: 13px"><ul class = "details-list">';
                    if (jsondata.responseCode == 200) {
                        var m = (jsondata.results);
                        var c = 0;
                        var array = $.map(jsondata.results, function (item) {
                            var param = '';
                            var address = item["addr"];
                            if (c == 0) {
                                param = (c + "|" + item["x"] + "|" + item["y"] + "|" + item["type"] + "|" + item["addr"] + "|" + item["place_id"]);
                            } else {
                                (param = c + "|" + item["place_id"] + "|" + item["type"] + "|" + item["addr"]);
                            }
                            c = c + 1;
                            result_string += '<li class="m_img">' + address + '</li>';
                            return {
                                label: item["addr"],
                                url: param,
                                x: item["x"],
                                y: item["y"]
                            }
                        });
                        response(array);
                    }

                },
                error: function (response) {

                },
                failure: function (response) {

                }
            });
        },
        select: function (e, i) {
            document.getElementById(Addr).value = i.item.value;
            $.ajax({
                url: 'api/GetAck/MapMyIndia?area=' + i.item.value+'&Id=' + $("#ClientIdApi").val(),
                dataType: "json",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    addCount(AutoSuggest);
                    var jsondata = JSON.parse(data);
                    $.map(jsondata.results, function (item) {
                        if (item["x"] && item["y"]) {
                            //alert()
                            //document.getElementById(Lat).value = i.item.y;
                            // document.getElementById(Lng).value = i.item.x;
                            document.getElementById(Lat).value = item["y"];
                            document.getElementById(Lng).value = item["x"];
                        }
                    });
                },
                error: function (response) {
                    //alert(JSON.stringify(response))
                }
            });
            
        },
        minLength: 1
    });
}
var via_points = "";
function Tour_startUp(stops) {
    var pts1 = [];
    var popup = "";
    for(i=0;i<stops.length;i++){
        var latlog = CreateLatLngObject(stops[i]["Geometry"]["Latitude"], stops[i]["Geometry"]["Longitude"]);
        var objImage = CreateImageObjectCount("Map_RedMarker", 24, 24, 10, 12, i);
        var mk = addMarker(latlog, objImage, "Hey There", true)
        popup = mk.bindPopup("<b><i>Location Details</i></b><br><b>Address : </b>" + stops[i]["Geometry"]["Desc"] + "<br/><b>LogDateTime : </b>" + stops[i]["Geometry"]["LogDateTime"] + "<br>");
        pts1.push(latlog);
    }
    mk.on("click", function (e) {
        popup;
    });
    var s = '';
    var d = '';
    s = pts1[0]['lat'];
    s += "," + pts1[0]['lng'];
    d = pts1[pts1.length - 1]['lat'];
    d += "," + pts1[pts1.length - 1]['lng'];
    for (i = 0; i < pts1.length; i++) {
        if (i != 0 && i != pts1.length) {
            via_points += pts1[i]['lat'] + "," + pts1[i]['lng'] + "|";
        }
    }
    $.ajax({
        url: 'api/GetAck/MapMyIndiaRoute?start=' + s + '&destination=' + d + '&via=' + via_points + '&Id=' + $("#ClientIdApi").val(),
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            addCount(RouteDraw);
            var jsondata = JSON.parse(result);
            if (jsondata.responseCode == 200) {
                route_api_result(jsondata.results);
            }
        }
    });
}
function createPolyLine(pts1) {
    if (pts1.length >= 2) {
        var poly1param = {
            color: "blue",
            weight: 4,
            opacity: 0.5
        };
        var poly1 = new L.Polyline(pts1, poly1param);
        map.addLayer(poly1);
    }
}
var p = 1, c = 0;
function addMarkers(position, icon, title, i)
{
    var icons = L.divIcon({
        className: 'my-div-icon',
        //html: "<img class='map_marker' src=" + "'https://maps.mapmyindia.com/images/2.png'>" + '<span class="my-div-span">' + p + '</span>',
        html: "<img style='position:relative;width:34px;height:48px' src=" + "'https://maps.mapmyindia.com/images/2.png'>" + '<span style="position: absolute;left:1.5em;right: 1em;top:0.9em;bottom:3em; font-size:10px;font-weight:bold; width: 4px; color:black;" class="my-div-span">' + i + '</span>',
        iconSize: [10, 10],
        popupAnchor: [12, -10]
    });
    var event_div = document.getElementById("event-log");
    var mk = new L.Marker(position, { icon: icons, draggable: true, title: title });
    map.addLayer(mk);
    p++;
}
var pathArrdir = [];
var polyList = [];
var polys = [];
var calDistance = [];
var totalDistance = "";
function route_api_result(data) {
    if (data.trips.duration != 0) {
        var alternate_route1_text = "";
        var alternate_route2_text = "";
        var direct_route = 'Route';
        alternate_route = data.alternatives;
        if (typeof alternate_route[0] != 'undefined') { /***get first alternative route***/
            var duration1 = alternate_route[0].duration;/**time in seconds*************/
            var hours1 = Math.floor(duration1 / 3600);
            duration1 %= 3600;
            var minutes1 = Math.floor(duration1 / 60);
            var total_time1 = (hours1 >= 1 ? hours1 + " hrs " : '') + (minutes1 >= 1 ? minutes1 + " min" : '');
            var length1 = (alternate_route[0].length) / 1000;
            alternate_route1_text = '<td ><div style="padding:5px 5px 5px 15px;color:#000;border-left:1px solid #ddd;cursor:pointer"\n\
                                                    onclick="document.getElementById(\'direct_advices\').style.display=\'none\';\n\
                                                    document.getElementById(\'alternatives_advices\').style.display=\'inline-block\';alternative_route(0)">\n\
                                                    <span style="font-size:13px;padding:2px 0 20px 0;color:#222">Route 2</span><br>\n\
                                                    <span style="font-size:11px;line-height:16px;color:#555">' + total_time1 + '<br>' + length1.toFixed(1) + ' km</div></td>';
            direct_route = 'Route 1';
        }
        /***get second alternative route***/
        if (typeof alternate_route[1] != 'undefined') {
            var duration2 = alternate_route[1].duration;/**time in seconds*************/
            var hours2 = Math.floor(duration2 / 3600);
            duration2 %= 3600;
            var minutes2 = Math.floor(duration2 / 60);
            var total_time2 = (hours2 >= 1 ? hours2 + " hrs " : '') + (minutes2 >= 1 ? minutes2 + " min" : '');
            var length2 = (alternate_route[1].length) / 1000;
            alternate_route2_text = '<td ><div style="padding:5px 5px 5px 15px;color:#000;border-left:1px solid #ddd;cursor:pointer" \n\
                                                    onclick="document.getElementById(\'direct_advices\').style.display=\'none\';\n\
                                                    document.getElementById(\'alternatives_advices\').style.display=\'inline-block\';alternative_route(1)">\n\
                                                    <span style="font-size:13px;padding:2px 0 20px 0;color:#222">Route 3</span><br>\n\
                                                    <span style="font-size:11px;line-height:16px;color:#555">' + total_time2 + '<br>' + length2.toFixed(1) + ' km</div></td>';
        }
        /***check & display alternative route option*****/
        var show_pts = '';
        var way = data.trips[0];
        var way1 = data.trips[1];
        var total_time;
        var length;
        if (via_points == "") {
            var trips = data.trips;
            var duration = way.duration;/**time in seconds*************/
            var hours = Math.floor(duration / 3600);
            duration %= 3600;
            var minutes = Math.floor(duration / 60);
            total_time = (hours >= 1 ? hours + " hrs " : '') + (minutes >= 1 ? minutes + " min" : '');
            length = (way.length) / 1000;
            
            var pts = decode_path(way.pts);; if (show_pts) console.log(pts);
            var advices = way.advices; /****advice & display **************/
        } else {
            
            /*******if via points is provided use trip[0] & trip[1] also************/
            //var duration = way.duration + way1.duration;/**time in seconds*************/
            //var hours = Math.floor(duration / 3600);
            //duration %= 3600;
            //var minutes = Math.floor(duration / 60);
            //total_time = (hours >= 1 ? hours + " hrs " : '') + (minutes >= 1 ? minutes + " min" : '');
            //length = (way.length + way1.length) / 1000;
            //var pts = decode_path(way.pts).concat(decode_path(way1.pts));/****points trip[0] & trip[1] to display **************/
            //var advices = way.advices.concat(way1.advices); /****advice trip[0] & trip[1] to display **************/
            var duration = way.duration; length = way.length; var pts = decode_path(way.pts); var advices = way.advices;
            var via_arr = via_points.split('|');
            for (i = 1; i < via_arr.length; i++) {
                duration += data.trips[i].duration;/**time in seconds*************/
                length += data.trips[i].length;
                pts = pts.concat(decode_path(data.trips[i].pts));/****points trip[0] & trip[1] to display **************/
                advices = advices.concat(data.trips[i].advices); /****advice trip[0] & trip[1] to display **************/
                calDistance.push(data.trips[i].length);
            }
            length = length / 1000;
            totalDistance = length.toString();
            var hours = Math.floor(duration / 3600); duration %= 3600;
            var minutes = Math.floor(duration / 60);
            total_time = (hours >= 1 ? hours + " hrs " : '') + (minutes >= 1 ? minutes + " min" : '');
            if (show_pts) console.log(pts);
        }
        /***********display advices***********/
        direct_route_info = '<table width="100%"><tr><td ><div style="padding:5px;cursor:pointer;background:#f7f7f7" \n\
                                                onclick="document.getElementById(\'direct_advices\').style.display=\'inline-block\';\n\
                                                document.getElementById(\'alternatives_advices\').style.display=\'none\';removeMapLayer(\'alternate\')\n\
                                                draw_polyline(\'direct\', pathArrdir);"><span style="font-size:13px;padding:2px 0 20px 0;color:#222">' + direct_route +
                '</span><br><span style="font-size:11px;line-height:16px">' + total_time + '<br>' + length.toFixed(1)
                + ' km</span></div></td>' + alternate_route1_text + alternate_route2_text + '</tr></table>';
        advice_direct_route = '<span style="font-size:13px;padding-left:5px">' + direct_route + '</span><table width="100%" align="center">';
        var num_rec = 1;
        var distance;
        var go = "";
        advices.forEach(function (advice) {
            var icon = advice.icon_id;
            var meters = advice.meters;
            var distance_meters = meters - distance;
            distance = meters;
            var advice_meters = (distance_meters >= 1000 ? (distance_meters / 1000).toFixed(1) + " km " : distance_meters + " mts ")
            var text = advice.text;
            if (meters != 0) {
                go = "<br>Go " + advice_meters;
                advice_direct_route += go + '</td></tr>';
            }
            var advice_pt = advice.pt;
            advice_direct_route += '<tr onclick="show_route_details(' + advice_pt.lat + ',' + advice_pt.lng + ',\'' + text + '\')"\n\
                                                    style="cursor:pointer;"><td valign="top" style="padding:5px 0px 5px 0px"><img src="https://api.mapmyindia.com/images/step_'
                    + icon + '.png" width="30px"></td><td style="padding:5px;border-top: 1px solid #e9e9e9;">' + text;
        });
        /***********display path***********/
        var pathArr = [];
        pts.forEach(function (pt) {
            pathArrdir.push(new L.LatLng(pt[0], pt[1]));
        });
        
        draw_polyline("direct", pathArrdir);/***********draw polyline***/
    } else {
      
        //remove_start_end_markersList();/***remove if any existing marker***/
    }
}

function draw_polyline(route, pathArr) {
  
    if (polys[route] in polyList) {
        polyList.pop(polys[route]);
        map.removeLayer(polys[route]);
    }
    var polyline_color = 'blue';
    if (route == 'direct') {
        if (polys[route] in polyList) {
            polyList.pop(polys[route]);
            map.removeLayer(polys[route]);
            var polyline_color = 'orange';
        }
    }
    polys[route] = new L.Polyline(pathArr, {
        color: polyline_color,
        weight: 6,
        opacity: 0.5,
        smoothFactor: 1
    });
    polyList.push(polys[route]);
    map.addLayer(polys[route]);
}
function decode_path(encoded) {
    if (encoded != 'undefined') {
        var pts = [];
        var index = 0, len = encoded.length;
        var lat = 0, lng = 0;
        while (index < len) {
            var b, shift = 0, result = 0;
            do {
                b = encoded.charAt(index++).charCodeAt(0) - 63;
                result |= (b & 0x1f) << shift;
                shift += 5;
            } while (b >= 0x20);

            var dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
            lat += dlat;
            shift = 0;
            result = 0;
            do {
                b = encoded.charAt(index++).charCodeAt(0) - 63;
                result |= (b & 0x1f) << shift;
                shift += 5;
            } while (b >= 0x20);
            var dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
            lng += dlng;
            pts.push([lat / 1E6, lng / 1E6]);
        }
        return pts;
    } else {
        return '';
    }
}
;
Array.max = function (array) {
    return Math.max.apply(Math, array);
};
Array.min = function (array) {
    return Math.min.apply(Math, array);
};
function markerPoint(data) {
    var position = [];
    var str = "";
    var popup = "";
    for (var i = 0; i < data.length; i++) {
        var position = new L.LatLng(data[i]["lat"], data[i]["lng"]);
        if (data[i]["marker"])
        {
        str = data[i]["marker"].replace("images/", "");
        str = str.replace(".png", "");
        }
        var objImage = CreateImageObject(str == "" ? "Map_RedMarker" : str, 24, 24, 10, 12);
        var mk = addMarker(position, objImage, "Hey There", false)
        if (data[i]["marker"])
        {
            popup = mk.bindPopup("<b><i>Location Details</i></b><br><b>Name : </b>" + data[i]["Name"] + "<br/><b>Mobile No : </b>" + data[i]["MobileNo"] + "<br><b>Time : </b>" + data[i]["description"] + "<br><b>Address :</b> <br>" + data[i]["title"] + "");
        }
        else
        {
            popup = mk.bindPopup("<b><i>Location Details</i></b><br><b>Time :</b><br>" + data[i]["description"] + "<br><b>Address :</b> <br>" + data[i]["title"] + "");
        }
    }
    mk.on("click", function (e)
    {
        popup;
    });
    
}


