<%@ Page Title="User DashBoard" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="UserDashBoard.aspx.cs" Inherits="MobiOcean.MDM.Web.UserDashBoard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="cnthesd" runat="server" ContentPlaceHolderID="ContentHead">

    <style>
        .reportcal li {
            display: inline;
        }

        .reportcal li {
            height: 25px;
            border: none;
            padding: 3px 15px 0 15px;
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
    <style type="text/css">
        td.actions {
            text-align: center;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">
                    <!-- flight section -->
                    <div class="bhoechie-tab-content active">
                        <div class="profile1">&nbsp;&nbsp;User Dashboard</div>
                    </div>

                    <h3>Device List </h3>
                    <br />
                    <div class="row" style="text-align: center">
                        <div class="row">
                            <div class="col-lg-6">
                                <div style="text-align: center;">
                                    <b style="font-size: 20px">Device Info</b>
                                </div>
                                <div>
                                    <br />
                                    <br />
                                    <br />
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="grddashboard" runat="server" AllowPaging="true" AutoGenerateColumns="false" CssClass="table mGrid" EmptyDataText="No reocrd found." GridLines="None"
                                        HeaderStyle-CssClass="protable" OnPageIndexChanging="grddashboard_PageIndexChanging" PageSize="20" ShowHeader="true" ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("DeviceId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Device Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIsEnable" runat="server" Text='<%#Eval("IsAppInstalled").ToString()=="0"?"Installed":"Not Installed"%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profile Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfileName" runat="server" Text='<%#string.IsNullOrEmpty(Eval("ProfileName").ToString())? "---" : Eval("IsEnable").ToString()=="0"?"---": Eval("ProfileName").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLastLocation" runat="server" Text='<%#!string.IsNullOrEmpty(Eval("Location").ToString())?Eval("Location"):"---"%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbView" runat="server" CssClass="btn-link" OnClick="lbView_Click" ToolTip="View"><i><img src="image/View.png" class="iconview"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        </Columns>
                                        <PagerStyle CssClass="dataTables_paginate paging_simple_numbers pagination-ys" HorizontalAlign="Right" />
                                    </asp:GridView>
                                </div>
                            </div>


                            <div class="col-lg-6">
                                <div style="text-align: center;">
                                    <b style="font-size: 20px">Alert Info</b>
                                </div>
                                <div class="col-lg-6" style="text-align: left">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>

                                <div style="text-align: right">
                                    <asp:Button ID="btnSaveChanges" class="btn btnd btncompt" runat="server" Text="Apply Changes" OnClick="btnSaveChanges_Click" />
                                </div>
                                <br />
                                <div class="table-responsive">
                                    <asp:GridView ID="grdAlert" runat="server" DataKeyNames="UserId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                        PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No record found." OnPageIndexChanging="grdAlert_PageIndexChanging" Width="100%" OnRowDataBound="grdAlert_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("AlertId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alert Text">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAlertText" runat="server" Text='<%#Eval("AlertText")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Read" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIsRead" runat="server" Text='<%#Eval("IsRead")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateTime" runat="server" Text='<%#Eval("LogDateTime")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mark as Read">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkbox" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">

                            <div class="col-lg-6">
                                <asp:Chart ID="Chart1" runat="server" BackColor="#F48857" Style="width: 100%"
                                    Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel"
                                    BorderDashStyle="Solid" BackSecondaryColor="White"
                                    BackGradientStyle="TopBottom" BorderWidth="2"
                                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                    BackImageTransparentColor="White">
                                    <Titles>
                                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Last 7 InComing and OutGoing Calls Days" ForeColor="White"></asp:Title>
                                    </Titles>
                                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                    <Series>
                                    </Series>

                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="#F48857" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                            <AxisY2 Enabled="False"></AxisY2>
                                            <AxisX2 Enabled="False"></AxisX2>
                                            <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                                            <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" ArrowStyle="Triangle">
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                            </AxisY>
                                            <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False" ArrowStyle="Triangle">
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IsStaggered="false" />
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>

                                </asp:Chart>
                                <div class="reportcal">
                                    <li style="background-color: #418BEB"></li>
                                    <li>InComing</li>
                                    <li style="background-color: #FFB54F;"></li>
                                    <li>OutGoing</li>
                                </div>
                            </div>



                            <div class="col-lg-6">
                                <asp:Chart ID="Chart2" runat="server" BackColor="#4DB754" Style="width: 100%"
                                    Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel"
                                    BorderDashStyle="Solid" BackSecondaryColor="White"
                                    BackGradientStyle="TopBottom" BorderWidth="2"
                                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                    BackImageTransparentColor="White">
                                    <Titles>
                                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Last 7 InComing and OutGoing SMS Days" ForeColor="White"></asp:Title>
                                    </Titles>
                                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                    <Series>
                                    </Series>

                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="#4DB754" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                            <AxisY2 Enabled="False"></AxisY2>
                                            <AxisX2 Enabled="False"></AxisX2>
                                            <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                                            <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" ArrowStyle="Triangle">
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                            </AxisY>
                                            <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False" ArrowStyle="Triangle">
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IsStaggered="false" />
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>

                                </asp:Chart>
                                <div class="reportcal">
                                    <li style="background-color: #418BEB"></li>
                                    <li>OutGoing</li>
                                    <li style="background-color: #FFB54F;"></li>
                                    <li>InComing</li>
                                </div>

                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Chart ID="Chart3" runat="server" BackColor="#2A368B" Style="width: 100%"
                                    Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel"
                                    BorderDashStyle="Solid" BackSecondaryColor="White"
                                    BackGradientStyle="TopBottom" BorderWidth="2"
                                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                    BackImageTransparentColor="White">
                                    <Titles>
                                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Last 7 App Using Days" ForeColor="White"></asp:Title>
                                    </Titles>
                                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                    <Series>
                                    </Series>

                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="#2A368B" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                            <AxisY2 Enabled="False"></AxisY2>
                                            <AxisX2 Enabled="False"></AxisX2>
                                            <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                                            <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" ArrowStyle="Triangle">
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                            </AxisY>
                                            <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False" ArrowStyle="Triangle">
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IsStaggered="false" />
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>

                                </asp:Chart>
                                <div class="reportcal">
                                    <li style="background-color: #418BEB"></li>
                                    <li>Duration</li>
                                </div>
                            </div>


                            <div class="col-lg-6">
                               <%-- <div id="map_canvas" ></div>--%>
                                  <div id="map" style="height:300px;z-index:1;"></div>
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                        </div>

                        <br />
                        <div class="row">
                            <div class="col-lg-12">


                                <div class="row">
                                    <div class="col-lg-12">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        var markers = JSON.parse('<%=ShowOnMap()%>');
        var center = [];
        window.onload = function () {
            center.push(markers[0].lat, markers[0].lng);
            center.push(markers[0].lat, markers[0].lng);
            LoadMap("map", center);
            markerPoint(markers);
        }
    </script>
    <script type="text/ecmascript">
        function HideLabel() {
            var seconds = 7;
            setTimeout(function () {
                document.getElementById("<%=lblmessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>

</asp:Content>


