<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="reposrtssms.aspx.cs" Inherits="MobiOcean.MDM.Web.reposrtssms" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    <style>
        div div div li {
            display: inline;
        }

        div div div li {
            height: 25px;
            border: none;
            padding: 3px 15px 0 15px;
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active fm1">

                <li class="profile1"><i>
                    <span class="glyphicon glyphicon-envelope"></span></i>&nbsp;&nbsp;Message Log</li>

                <br>
                <div class="row">
                    <div class="col-md-4">
                        <label>Start Date: </label>
                        <div id="datepicker" class="input-group date" data-date-format="mm-dd-yyyy">
                            <input class="form-control" type="date" type="text">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        </div>
                    </div>


                    <div class="col-md-4">
                        <label>End Date: </label>
                        <div id="datepicker1" class="input-group date" data-date-format="mm-dd-yyyy">
                            <input class="form-control" type="date" type="text">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <br>
                        <button type="submit" class="btn btnd btncompt">Select Data Range</button>
                    </div>

                </div>
                <br>
                <br>

                <asp:Chart ID="Chart1" runat="server" BackColor="OrangeRed" Width="600px"
                    Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel"
                    BorderDashStyle="Solid" BackSecondaryColor="White"
                    BackGradientStyle="TopBottom" BorderWidth="2"
                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                    BackImageTransparentColor="White">
                    <Titles>
                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Last Seven Days InComing and OutGoing Messages" ForeColor="26, 59, 105"></asp:Title>
                    </Titles>
                    <%--   <legends>
                              <asp:Legend Enabled="True" IsTextAutoFit="false" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Docking="Bottom"></asp:Legend>
                          </legends>--%>
                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                    <%--  <series>
                            <asp:Series IsValueShownAsLabel="True" ChartArea="ChartArea1" Name="InComing" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" ></asp:Series>
                              <asp:Series IsValueShownAsLabel="True" ChartArea="ChartArea1" Name="OutGoing" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105"></asp:Series>
                              <asp:Series IsValueShownAsLabel="True" ChartArea="ChartArea1" Name="Default2" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" ></asp:Series>

                          </series>--%>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
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


                <div class="col-lg-5 col-md-6 col-md-offset-5 centered">
                    <li style="background-color: #418BEB"></li>
                    <li>OutGoing</li>
                    <li style="background-color: #FFB54F;"></li>
                    <li>InComing</li>

                </div>
                <br>
                <br>
            </div>
        </div>
    </div>

</asp:Content>

