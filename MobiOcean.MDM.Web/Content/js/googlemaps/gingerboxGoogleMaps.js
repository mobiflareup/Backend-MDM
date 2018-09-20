var map = null;
var bermudaTriangle, circle;
var infoWindowOpenClose = { "infoWindowOpen": 1, "infoWindowClose": 2 }
var isDraggableMarker = { "Yes": 1, "No": 2 }
var isContentHeaderOrNot = { "Yes": 1, "No": 2 }
var MarkerArray = [];
var bounds = new google.maps.LatLngBounds();
var MapLoad = 3;
var AutoSuggest = 4;
var RouteDraw = 5;
function LoadMap(id, center)
{
    
    var myLatLng = new google.maps.LatLng(center[0], center[1]);
    var mapOptions = {
        zoom: 12,
        center: myLatLng,
        mapTypeId: google.maps.MapTypeId.RoadMap
    };
    map = new google.maps.Map(document.getElementById(id), mapOptions);
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

            var infoWindow = new google.maps.InfoWindow();

            for (var i = 0; i < markers.length; i++) {
                var data = markers[i];
                var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    title: data.title,
                    icon: data.marker
                });

                //Attach click event to the marker.
                (function (marker, data) {

                    google.maps.event.addListener(marker, "click", function (e) {
                        if (data.description != "surveillance camera") {

                            //Wrap the content inside an HTML DIV in order to set height and width of InfoWindow.
                            infoWindow.setContent("<b><i>Location Details</i></b><br><b>Time    :</b><br>" + data.description + "<br><b>Address :</b><br>" + data.title + "<br><b>Mobile No :</b><br>" + data.MobileNo + "");
                            infoWindow.open(map, marker);
                        }
                        else {
                            window.open('http://59.181.96.250:220/', '_blank');
                            // window.location = "http://59.181.96.250:220/";
                        }
                    });
                })(marker, data);
            }
}
function create_polygons_editable(pts) {
    var triangleCoords = [];
    for(i = 0; i < pts.length; i++)
    {
        triangleCoords.push(new google.maps.LatLng(pts[i]["0"], pts[i]["1"]));
    }
    // Construct the polygon
    bermudaTriangle = new google.maps.Polygon({
        paths: triangleCoords,
        draggable: true,
        editable: true,
        strokeColor: '#FF0000',
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: '#FF0000',
        fillOpacity: 0.35
    });

    bermudaTriangle.setMap(map);
    google.maps.event.addListener(bermudaTriangle, "dragend", getPolygonCoords);
    google.maps.event.addListener(bermudaTriangle.getPath(), "insert_at", getPolygonCoords);
    google.maps.event.addListener(bermudaTriangle.getPath(), "remove_at", getPolygonCoords);
    google.maps.event.addListener(bermudaTriangle.getPath(), "set_at", getPolygonCoords);
}
function DrawLine() {

}
function DrawCircle(pts, radius) {
    var myLatLng = new google.maps.LatLng(pts["0"], pts["1"]);
    var myradius = parseInt(radius);
     circle = new google.maps.Circle({
        center: myLatLng,
        map: map,
        draggable: true,
        editable: true,
        radius: myradius,
        fillColor: '#FF6600',
        fillOpacity: 0.3,
        strokeColor: "#FFF",
        strokeWeight: 0         
    });
    google.maps.event.addListener(circle, 'radius_changed', onCircleComplete);
    google.maps.event.addListener(circle, 'center_changed', onCircleComplete);
}
function DrawPolygon() {

}
function DrawRectangle() {

}
function DrawMarker() {

}
function getPolygonCoords()
{
    var len = bermudaTriangle.getPath().getLength();
    var htmlStr = "";
    for (var i = 0; i < len; i++) {
        htmlStr += bermudaTriangle.getPath().getAt(i).toUrlValue(5) + ",";
    }
    document.getElementById('info').innerHTML = htmlStr;
    document.getElementById('ContentBody_hidden').value = htmlStr;

}
function onCircleComplete() {
    document.getElementById('ContentBody_hdnStartLat').value = circle.getCenter().lat();
    document.getElementById('ContentBody_hdnStartLong').value = circle.getCenter().lng();
    document.getElementById('ContentBody_hdnRadius').value = circle.getRadius();
    document.getElementById('ContentBody_txtRadius').value = document.getElementById('ContentBody_hdnRadius').value;
}  
function ShowOnMap(isFirstTime, Time, Address, Latitude, Longitude, ImgId) {

    if (Latitude != '0.0') {
        var imgName = "";
        if (ImgId == 0) {
            imgName = 'Map_greenMarker'
        }
        else {
            imgName = 'Map_RedMarker';
        }
        var toolTiptext = Address;

        var Content = '<b><i>Location Details</i></b><br>' +
                '<b>Time    :</b><br>' + Time + '<br>' +
                '<b>Address :</b><br>' + Address + '<br>';


        if (isFirstTime == 1) {
            var mapOptions = {
                zoom: 9,
                center: new google.maps.LatLng(Latitude, Longitude),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById('map'), mapOptions);

        }

        var objImage = CreateImageObject(imgName, 24, 24, 10, 12);
        var objLatLng = CreateLatLngObject(Latitude, Longitude);
        var mapCustomer = AddMarkerOnMap(objImage, objLatLng, 24, 24, toolTiptext, Content, infoWindowOpenClose.infoWindowClose, isDraggableMarker.No, isContentHeaderOrNot.No);

    }

}
function ShowOnMapCrntLoc(isFirstTime, Time, Address, Latitude, Longitude, ImgId,UserName) {

    if (Latitude != '0.0') {
        var imgName = "";
        if (ImgId == 0) {
            imgName = 'Map_greenMarker'
        }
        else {
            imgName = 'Map_RedMarker';
        }
        var toolTiptext = UserName;

        var Content = '<b><i>Location Details</i></b><br>' +
            '<b>Name    :</b>' + UserName + '<br>' +
                '<b>Time    :</b>' + Time + '<br>' +
                '<b>Address :</b>' + Address + '<br>';


        if (isFirstTime == 1) {
            var mapOptions = {
                zoom: 9,
                center: new google.maps.LatLng(Latitude, Longitude),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById('map'), mapOptions);

        }

        var objImage = CreateImageObject(imgName, 24, 24, 10, 12);
        var objLatLng = CreateLatLngObject(Latitude, Longitude);
        var mapCustomer = AddMarkerOnMap(objImage, objLatLng, 24, 24, toolTiptext, Content, infoWindowOpenClose.infoWindowClose, isDraggableMarker.No, isContentHeaderOrNot.No);

    }

}
function showAllMarkersOnWindow() {
    if (MarkerArray.length > 1) {
        for (var i in MarkerArray) {
            bounds.extend(MarkerArray[i].getPosition());
        }
        map.fitBounds(bounds);
    }
}
function CreateImageObject(ImageName, sizeX, sizeY, anchorX, anchorY) {
    var image = new google.maps.MarkerImage("images/" + ImageName + ".png",
            new google.maps.Size(sizeX, sizeY),         // This marker is 20 pixels wide by 32 pixels tall.
            new google.maps.Point(0, 0),                // The origin for this image is 0,0.
            new google.maps.Point(anchorX, anchorY));   // The anchor for this image is the base of the flagpole at 0,32.
    return image;
}
function CreateLatLngObject(Latitude, Longitude) {
    var latlng = new google.maps.LatLng(parseFloat(Latitude), parseFloat(Longitude));
    return latlng;
}
function AddMarkerOnMap(objImage, objLatLng, objsizeX, objsizeY, toolTiptext, infoWindowContent, infoWindowOpenClose, isDraggable, isContent) {
    var dragFlag = true;

    if (isDraggable == 2) dragFlag = false;
    var marker = new google.maps.Marker
                ({
                    position: objLatLng,
                    title: toolTiptext,
                    icon: objImage,
                    size: google.maps.Size(objsizeX, objsizeY),
                    draggable: dragFlag,
                    map: map
                });

    var ContentHeader = '';
    if (isContent == 1) {
    }
    else if (isContent == 2) {
        ContentHeader = infoWindowContent;
    }
    if (infoWindowOpenClose == 1) {
        if (infoWindow) infoWindow.close();
        infoWindow = new google.maps.InfoWindow({ content: ContentHeader, position: objLatLng, maxWidth: 200 });
        infoWindow.open(map, marker);
    }
    else if (infoWindowOpenClose == 2) {
        infoWindow = new google.maps.InfoWindow({ content: ContentHeader, position: objLatLng, maxWidth: 200 });
    }

    google.maps.event.addListener(marker, "mouseover", function (event) {//            
        if (infoWindow) infoWindow.close();
        infoWindow = new google.maps.InfoWindow({ content: ContentHeader, position: marker.position, maxWidth: 200 });
        infoWindow.open(map, marker);
    });

    google.maps.event.addListener(marker, "click", function (event) {//            
        map.setZoom((map.getZoom()) + 5);
        map.setCenter(marker.getPosition());
    });
    google.maps.event.addListener(marker, "rightclick", function (event) {//            
        map.setZoom((map.getZoom()) - 5);
        map.setCenter(marker.getPosition());
    });

    MarkerArray.push(marker);




    return marker;
}
function RemoveMarkersFromArray() {
    if (MarkerArray) {
        for (var i = 0; i < MarkerArray.length; i++) {
            MarkerArray[i].setMap(null);
        }
    }
    MarkerArray = new Array();
}
function GetLocationFromApi(TxtBoxid, HiddenLat, HiddenLng, HiddenAddr) {
    var Places = new google.maps.places.Autocomplete(document.getElementById(TxtBoxid));
    google.maps.event.addListener(Places, 'place_changed', function () {
        var place = Places.getPlace();
        document.getElementById(HiddenAddr).value = place.formatted_address;
        document.getElementById(HiddenLat).value = place.geometry.location.lat();
        document.getElementById(HiddenLng).value = place.geometry.location.lng();
    });
    addCount(AutoSuggest);
}
function Tour_startUp(stops) {
            if (stops.length == 1) {
                bootbox.alert("<h5>No route found for this locations.");
            }
            if (!window.tour) window.tour = {
                updateStops: function (newStops) {
                    stops = newStops;
                },
                loadMap: function (map, directionsDisplay) {
                    var myOptions = {
                        zoom: 13,
                        center: new window.google.maps.LatLng(stops[0].Geometry.Latitude, stops[0].Geometry.Longitude), // default to London
                        mapTypeId: window.google.maps.MapTypeId.ROADMAP
                    };
                    map.setOptions(myOptions);
                    directionsDisplay.setMap(map);
                },
                calcRoute: function (directionsService, directionsDisplay) {
                    var batches = [];
                    var itemsPerBatch = 10; // google API max = 10 - 1 start, 1 stop, and 8 waypoints
                    var itemsCounter = 0;
                    var wayptsExist = stops.length > 0;

                    while (wayptsExist) {
                        var subBatch = [];
                        var subitemsCounter = 0;

                        for (var j = itemsCounter; j < stops.length; j++) {
                            subitemsCounter++;
                            subBatch.push({
                                location: new window.google.maps.LatLng(stops[j].Geometry.Latitude, stops[j].Geometry.Longitude),
                                stopover: true
                            });
                            if (subitemsCounter == itemsPerBatch)
                                break;
                        }

                        itemsCounter += subitemsCounter;
                        batches.push(subBatch);
                        wayptsExist = itemsCounter < stops.length;
                        // If it runs again there are still points. Minus 1 before continuing to
                        // start up with end of previous tour leg
                        itemsCounter--;
                    }

                    // now we should have a 2 dimensional array with a list of a list of waypoints
                    var combinedResults;
                    var unsortedResults = [{}]; // to hold the counter and the results themselves as they come back, to later sort
                    var directionsResultsReturned = 0;

                    for (var k = 0; k < batches.length; k++) {
                        var lastIndex = batches[k].length - 1;
                        var start = batches[k][0].location;
                        var end = batches[k][lastIndex].location;

                        // trim first and last entry from array
                        var waypts = [];
                        waypts = batches[k];
                        waypts.splice(0, 1);
                        waypts.splice(waypts.length - 1, 1);

                        var request = {
                            origin: start,
                            destination: end,
                            waypoints: waypts,
                            travelMode: window.google.maps.TravelMode.DRIVING
                        };
                        (function (kk) {
                            directionsService.route(request, function (result, status) {
                                if (status == window.google.maps.DirectionsStatus.OK) {

                                    var unsortedResult = { order: kk, result: result };
                                    unsortedResults.push(unsortedResult);

                                    directionsResultsReturned++;

                                    if (directionsResultsReturned == batches.length) // we've received all the results. put to map
                                    {
                                        // sort the returned values into their correct order
                                        unsortedResults.sort(function (a, b) { return parseFloat(a.order) - parseFloat(b.order); });
                                        var count = 0;
                                        for (var key in unsortedResults) {
                                            if (unsortedResults[key].result != null) {
                                                if (unsortedResults.hasOwnProperty(key)) {
                                                    if (count == 0) // first results. new up the combinedResults object
                                                        combinedResults = unsortedResults[key].result;
                                                    else {
                                                        // only building up legs, overview_path, and bounds in my consolidated object. This is not a complete
                                                        // directionResults object, but enough to draw a path on the map, which is all I need
                                                        combinedResults.routes[0].legs = combinedResults.routes[0].legs.concat(unsortedResults[key].result.routes[0].legs);
                                                        combinedResults.routes[0].overview_path = combinedResults.routes[0].overview_path.concat(unsortedResults[key].result.routes[0].overview_path);

                                                        combinedResults.routes[0].bounds = combinedResults.routes[0].bounds.extend(unsortedResults[key].result.routes[0].bounds.getNorthEast());
                                                        combinedResults.routes[0].bounds = combinedResults.routes[0].bounds.extend(unsortedResults[key].result.routes[0].bounds.getSouthWest());
                                                    }
                                                    count++;
                                                }
                                            }
                                        }
                                        directionsDisplay.setDirections(combinedResults);
                                        var legs = combinedResults.routes[0].legs;

                                        var path = combinedResults.routes[0].overview_path;

                                        for (var i = 0; i < legs.length; i++) {
                                            var markerletter = "A".charCodeAt(0);
                                            markerletter += i;
                                            markerletter = String.fromCharCode(markerletter);
                                            createMarker(directionsDisplay.getMap(), legs[i].start_location, stops[i].Geometry.Desc, "<b>LogDateTime : </b>" + stops[i].Geometry.LogDateTime, 'A', (i + 1));
                                        }
                                        // Marker for start point
                                        var i = legs.length;
                                        var markerletter = "A".charCodeAt(0);
                                        markerletter += i;
                                        markerletter = String.fromCharCode(markerletter);
                                        //marker for End Point
                                        createMarker(directionsDisplay.getMap(), legs[legs.length - 1].end_location, stops[legs.length].Geometry.Desc, "<b>LogDateTime : </b>" + stops[legs.length].Geometry.LogDateTime, 'B', (i + 1));
                                        //polyline[0].setMap(map);
                                    }
                                }
                            });
                        })(k);
                    }
                }
            };
            if (window.tour) window.tour = {
                updateStops: function (newStops) {
                    stops = newStops;
                },
                loadMap: function (map, directionsDisplay) {
                    var myOptions = {
                        zoom: 13,
                        center: new window.google.maps.LatLng(stops[0].Geometry.Latitude, stops[0].Geometry.Longitude), // default to London
                        mapTypeId: window.google.maps.MapTypeId.ROADMAP
                    };
                    map.setOptions(myOptions);
                    directionsDisplay.setMap(map);
                },
                calcRoute: function (directionsService, directionsDisplay) {
                    var batches = [];
                    var itemsPerBatch = 10; // google API max = 10 - 1 start, 1 stop, and 8 waypoints
                    var itemsCounter = 0;
                    var wayptsExist = stops.length > 0;

                    while (wayptsExist) {
                        var subBatch = [];
                        var subitemsCounter = 0;

                        for (var j = itemsCounter; j < stops.length; j++) {
                            subitemsCounter++;
                            subBatch.push({
                                location: new window.google.maps.LatLng(stops[j].Geometry.Latitude, stops[j].Geometry.Longitude),
                                stopover: true
                            });
                            if (subitemsCounter == itemsPerBatch)
                                break;
                        }
                        itemsCounter += subitemsCounter;
                        batches.push(subBatch);
                        wayptsExist = itemsCounter < stops.length;
                        itemsCounter--;
                    }
                    var combinedResults;
                    var unsortedResults = [{ }]; 
                    var directionsResultsReturned = 0;

                    for (var k = 0; k < batches.length; k++) {
                        var lastIndex = batches[k].length - 1;
                        var start = batches[k][0].location;
                        var end = batches[k][lastIndex].location;

                        // trim first and last entry from array
                        var waypts = [];
                        waypts = batches[k];
                        waypts.splice(0, 1);
                        waypts.splice(waypts.length - 1, 1);

                        var request = {
                            origin: start,
                            destination: end,
                            waypoints: waypts,
                            travelMode: window.google.maps.TravelMode.DRIVING
                        };
                        (function (kk) {
                            directionsService.route(request, function (result, status) {
                                if (status == window.google.maps.DirectionsStatus.OK) {

                                    var unsortedResult = { order: kk, result: result };
                                    unsortedResults.push(unsortedResult);

                                    directionsResultsReturned++;

                                    if (directionsResultsReturned == batches.length)
                                    {
                                        unsortedResults.sort(function (a, b) { return parseFloat(a.order) - parseFloat(b.order); });
                                        var count = 0;
                                        for (var key in unsortedResults) {
                                            if (unsortedResults[key].result != null) {
                                                if (unsortedResults.hasOwnProperty(key)) {
                                                    if (count == 0)
                                                        combinedResults = unsortedResults[key].result;
                                                    else {
                                                        combinedResults.routes[0].legs = combinedResults.routes[0].legs.concat(unsortedResults[key].result.routes[0].legs);
                                                        combinedResults.routes[0].overview_path = combinedResults.routes[0].overview_path.concat(unsortedResults[key].result.routes[0].overview_path);

                                                        combinedResults.routes[0].bounds = combinedResults.routes[0].bounds.extend(unsortedResults[key].result.routes[0].bounds.getNorthEast());
                                                        combinedResults.routes[0].bounds = combinedResults.routes[0].bounds.extend(unsortedResults[key].result.routes[0].bounds.getSouthWest());
                                                    }
                                                    count++;
                                                }
                                            }
                                        }
                                        directionsDisplay.setDirections(combinedResults);
                                        var legs = combinedResults.routes[0].legs;

                                        var path = combinedResults.routes[0].overview_path;

                                        for (var i = 0; i < legs.length; i++) {
                                            var markerletter = "A".charCodeAt(0);
                                            markerletter += i;
                                            markerletter = String.fromCharCode(markerletter);
                                            createMarker(directionsDisplay.getMap(), legs[i].start_location, stops[i].Geometry.Desc, "<b>LogDateTime : </b>" + stops[i].Geometry.LogDateTime, 'A', (i + 1));
                                        }
                                       
                                        var i = legs.length;
                                        var markerletter = "A".charCodeAt(0);
                                        markerletter += i;
                                        markerletter = String.fromCharCode(markerletter);
                                        createMarker(directionsDisplay.getMap(), legs[legs.length - 1].end_location, stops[legs.length].Geometry.Desc, "<b>LogDateTime : </b>" + stops[legs.length].Geometry.LogDateTime, 'B', (i + 1));
                                    }
                                }
                            });
                        })(k); 0
                    }
                }
            };
            addCount(RouteDraw);
}
function getMarkerImage(iconStr) {
    if ((typeof (iconStr) == "undefined") || (iconStr == null)) {
        iconStr = "red";
    }

    if (iconStr == 'A') {

        if (!icons[iconStr]) {
            icons[iconStr] = new google.maps.MarkerImage("https://www.google.com/mapfiles/dd-start.png",
            // This marker is 20 pixels wide by 34 pixels tall.
            new google.maps.Size(20, 34),
            // The origin for this image is 0,0.
            new google.maps.Point(0, 0),
            // The anchor for this image is at 6,20.
            new google.maps.Point(9, 34));
        }

    }
    if (iconStr == 'B') {
        if (!icons[iconStr]) {
            icons[iconStr] = new google.maps.MarkerImage("https://www.google.com/mapfiles/dd-end.png",
            // This marker is 20 pixels wide by 34 pixels tall.
            new google.maps.Size(20, 34),
            // The origin for this image is 0,0.
            new google.maps.Point(0, 0),
            // The anchor for this image is at 6,20.
            new google.maps.Point(9, 34));
        }


    }
    return icons[iconStr];
}
function createMarker(map, latlng, label, html, color, markerNumber) {
    var infowindow = new google.maps.InfoWindow();
    var contentString = '<b>' + label + '</b><br>' + html;
    var marker = new google.maps.Marker({
        position: latlng,
        map: map,
        shadow: iconShadow,
        icon: 'https://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + markerNumber + '|FE6256|000000',
        shape: iconShape,
        title: label,
        draggable: true,
        zIndex: Math.round(latlng.lat() * -100000) << 5
    });
    marker.myname = label;
    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(contentString);
        infowindow.open(map, marker);
    });
    return marker;
}
function markerPoint(markers) {
   
    var infoWindow = new google.maps.InfoWindow();

    for (var i = 0; i < markers.length; i++) {
        var data = markers[i];
        var myLatlng = new google.maps.LatLng(data.lat, data.lng);
        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            title: data.title,
            icon: data.marker
        });

        
        (function (marker, data) {

            google.maps.event.addListener(marker, "click", function (e) {
                //Wrap the content inside an HTML DIV in order to set height and width of InfoWindow.
                infoWindow.setContent("<b><i>Location Details</i></b><br><b>Time :</b><br>" + data.description + "<br><b>Address :</b> <br>" + data.title + "");
                infoWindow.open(map, marker);
            });
        })(marker, data);
    }
}
