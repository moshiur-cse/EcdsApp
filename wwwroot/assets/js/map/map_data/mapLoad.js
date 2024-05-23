/* 
 * @license
 * This JS is proprietary and protected by copyright law.
 * Unauthorized copying of this file, via any medium, is strictly prohibited.
 */


var boundary;
var geojson;
var mymap; /*= L.map('mapid').setView([cLat, cLong], mapZoom);*/
var cLat = 23.777176;
var cLong = 90.399452;
var mapZoom = 7;
var index = 1;
var adminName = "";
var legend = L.control({ position: 'bottomright' });
var mapAttrib = "Map data © <a href='https://www.cegisbd.com/' target='_blank'>CEGIS</a>";




function mapLoad(mapDivId, adminCode) {

    if (adminCode.length == 6) {
        boundary = upazila;
        for (var i = 0; i < 544; i++) {
            if (boundary.features[i].properties.upaz_code == adminCode) {
                mapZoom = 11;
                cLat = boundary.features[i].properties.CNT_LAT;
                cLong = boundary.features[i].properties.CNT_LONG;
                index = i;
                adminName = boundary.features[i].properties.upaz_name;
                break;
            }
        }
    } else {
        boundary = district;
        for (var i = 0; i < 64; i++) {
            if (boundary.features[i].properties.dist_code == adminCode) {
                mapZoom = 9;
                cLat = boundary.features[i].properties.CNT_LAT;
                cLong = boundary.features[i].properties.CNT_LONG;
                index = i;
                adminName = boundary.features[i].properties.dist_name;
                break;
            }
        }

    }

    mymap = L.map(mapDivId).setView([cLat, cLong], mapZoom);

    L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        minZoom: 9,
        maxZoom: 11,
        id: mapDivId,
        attribution: mapAttrib
    }).addTo(mymap);



    L.control.scale().addTo(mymap);
    L.control.zoom().addTo(mymap);

    mymap.setView([cLat, cLong], mapZoom);
    //$("#map_cord_info").html(cLat.toFixed(8) + ", " + cLong.toFixed(8));

    //mymap.on("mousemove",
    //    function (evt) {
    //        $("#map_cord_info").html(evt.latlng.lat.toFixed(8) + ", " + evt.latlng.lng.toFixed(8));
    //    });

    geojson = L.geoJson(boundary.features[index], {
        style: style,
    }).addTo(mymap);

    L.latlngGraticule({
        showLabel: true,
        zoomInterval: [
            { start: 2, end: 3, interval: 30 },
            { start: 4, end: 4, interval: 10 },
            { start: 5, end: 7, interval: 5 },
            { start: 8, end: 10, interval: 1 }
        ]
    }).addTo(mymap);

    mymap.zoomControl.remove();
    majorRiverShowHide();
    displayLabel();
    displayLegend();
}


function style(feature) {
    return {
        fillColor: '#A9D4A5',
        weight: 1,
        opacity: 1,
        color: '#000000',
        dashArray: '1',
        fillOpacity: 1
    };
}

function majorRiverShowHide() {
    //var majorRiverLayer;
    var layerPath = "../js/maps/map_data/mjr_river.json";

    var majorRiverLayer = L.geoJson(null, {
        style: function (feature) {
            return {
                stroke: false,
                color: "#0EBFE9",
                weight: 1,
                fillOpacity: 0.8
            };
        }
    });//.addTo(mymap);

    $.getJSON(layerPath, function (LayerInfo) {
        if (!LayerInfo) return;
        majorRiverLayer.addData(LayerInfo);
    }).done(function (e) {
        mymap.addLayer(majorRiverLayer);
    })
}

function displayLabel() {
    var mapLabels = L.layerGroup();
    var label = L.marker([cLat, cLong], {
        icon: L.divIcon({
            className: 'label mapLabel',
            html: adminName,
            iconSize: [200, 80],
        })
    });
    mapLabels.addLayer(label);
    mymap.addLayer(mapLabels);
}

function displayLegend() {
    mymap.removeLayer(legend);
    var div = L.DomUtil.create('div', 'legend_info');
    div.innerHTML += '<p>Legend</p>';

    legend.onAdd = function (map) {
            div.innerHTML += '<p>Major River<i style="background:#0EBFE9;"></i> </p>'+
                '<p>' + adminName+'<i style="background:#A9D4A5"></i> </p>';
            return div;
        };
    
        
 
    legend.addTo(mymap);
}



