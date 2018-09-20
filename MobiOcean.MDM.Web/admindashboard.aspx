<%@ Page Title="Admin DashBoard" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="admindashboard.aspx.cs" Inherits="MobiOcean.MDM.Web.admindashboard" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--<style> html, body, #map {margin: 0;padding: 0;width: 100%;height: 100%;} </style>--%>
<%--    <style type="text/css">
        .info-div {
            padding: 10px;
            font-size: 13px;
        }
    </style>--%>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab " id="style-3">
        <div class="force-overflow">
            <div class="profile1">
                 &nbsp;&nbsp;Dashboard
            </div>
            <div class="bhoechie-tab-content active div" >
                 <%-- <%if (Convert.ToInt32(Session["ClientId"].ToString()) == 264 || Convert.ToInt32(Session["ClientId"].ToString()) == 208)
                      {%>
                <br />
                <div class="profile2 col-lg-4">
                    &nbsp;&nbsp;SOS View

                </div>
                <div class="col-md-8"><br /></div>
                <div class="row">

                    <div class="col-lg-12">
                        <br /> <br />
                        <div class="table-responsive">
                           
                            <div id="map" style="height:500px;z-index:1;">

                            </div>
                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    var markers = JSON.parse('<%=ShowOnMap()%>');
                    window.onload = function () {
                        var center = [];
                        center.push(markers[0].lat, markers[0].lng)
                        LoadMap('map', center);
                        LoadMap1(markers);
                    }
                 </script>
                 <% } %>--%>

                <br/>
                <div class="row">

                    <div class="col-md-3 admindashb">
                        <div class="admindash" style="background:#747580;color:#fff;padding:4px 0px;font-weight:bold;">NO. of Devices :
                            <asp:Label ID="lbltotal" runat="server"></asp:Label></div>

                        <asp:Chart ID="Chart1" runat="server" Style="width: 100%;border: 2px solid #747580;">
                            <Legends>
                                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" LegendStyle="Table" Name="XPointMember" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Series1"></asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>

                    </div>

                    
                    <div class="col-md-3 admindashb">
                        <div class="admindash" style="background:#30be80;color:#fff;padding:4px 0px;font-weight:bold;">NO. of Branches : <asp:Label ID="lblBranchCnt" runat="server"></asp:Label></div>
                        <asp:Chart ID="Chart2" runat="server" Style="width: 100%;border: 2px solid #30be80;">
                            <Legends>
                                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Table" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Default" />
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea2"></asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>

                    </div>
                    <div class="col-md-3 admindashb">
                        <div class="admindash" style="background:#fc6217;color:#fff;padding:4px 0px;font-weight:bold;">NO. of Departments : <asp:Label ID="lbldepartcnt" runat="server"></asp:Label></div>

                        <asp:Chart ID="Chart3" runat="server" Style="width: 100%;;border: 2px solid #fc6217;">
                            <Legends>
                                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Table" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Default" />
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea3"></asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>

                    <div class="col-md-3 admindashb">
                        <div class="admindash" style="background:#6b75ec;color:#fff;padding:4px 0px;font-weight:bold;">NO. of Profiles : <asp:Label ID="lblProfile" runat="server"></asp:Label></div>
                        <asp:Chart ID="Chart4" runat="server" Style="width: 100%;border: 2px solid #6b75ec;">
                            <Legends>
                                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Table" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Default" />
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea4"></asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>

                </div>
                <br/>
                <br/><br/>
                <div class="row">
                    <div class="col-lg-3">
                        
                <div class="daswit admindashb" style="width: 100%;border: 2px solid #747580;min-width:205px;">
                    <a href="UserMaster.aspx">
                        <img src="image/manage.png" onmouseover="this.src='image/manage-hover.png'" onmouseout="this.src='image/manage.png'" style="padding:35px;" /></a><br/>
                    <a href="UserMaster.aspx" ><p style="background:#747580;color:#fff;padding:10px 0px;font-weight:bold;margin:0px;"><span class="spandash">USER DETAILS</span></p></a>
                </div><br />
                    </div>
                    <div class="col-lg-3">
                        
                <div class="admindashb daswit" style="width: 100%;border: 2px solid #30be80;min-width:205px;">
                    <a href="UserDeviceModel.aspx">
                        <img src="image/asset.png" onmouseover="this.src='image/asset-hover.png'" onmouseout="this.src='image/asset.png'"  style="padding:35px;"/></a><br/>
                   <a href="UserDeviceModel.aspx"><p  style="background:#30be80;color:#fff;padding:10px 0px;font-weight:bold;margin:0px;"> <span class="spandash">ASSET TRACKING</span></p></a>
                </div><br />
                    </div>
                    <div class="col-lg-3">
                        
                    <div class="admindashb daswit" style="width: 100%;border: 2px solid #fc6217;min-width:205px;" >
                    <a href="SosReport.aspx" >
                        <img src="image/sosda.png" onmouseover="this.src='image/sosda-hover.png'" onmouseout="this.src='image/sosda.png'"  style="padding:35px;"/></a><br/>
                   <a href="SosReport.aspx"> <p style="background:#fc6217;color:#fff;padding:10px 0px;font-weight:bold;margin:0px;"><span class="spandash">SOS</span> </p> </a>                     
                </div><br />
                    </div>
                    <div class="col-lg-3">
                        
                <div class="admindashb daswit"  style="width: 100%;border: 2px solid #6b75ec;min-width:205px;">
                    <a href="WifiSensorDetails.aspx">
                      
                        <img src="image/wifigrey.png" onmouseover="this.src='image/wifiblue.png'" onmouseout="this.src='image/wifigrey.png'"  style="padding:35px;"/></a><br/>
                    <a href="WifiSensorDetails.aspx"><p style="background:#6b75ec;color:#fff;padding:10px 0px;font-weight:bold;margin:0px;"><span class="spandash">WIFI SENSOR</span></p></a>
                    </div><br />
                    </div>
                    <div class="clearfix"></div><br /><br />

                </div>
          
            </div>
        </div>
    </div>

    
</asp:Content>

