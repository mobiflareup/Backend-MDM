﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="smsReport.aspx.cs" Inherits="MobiOcean.MDM.Web.smsReport" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <script>
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrmDate]").datepick({
                    dateFormat: 'dd M yyyy'
                });
                $("[id$=txtToDate]").datepick({
                    dateFormat: 'dd M yyyy'
                });
                $('#style-3').scroll(function () {
                    $("[id$=txtFrmDate],[id$=txtToDate]").datepick("hide");
                });

            });

        }
    </script>
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ScriptManager>--%>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->

        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <!-- flight section -->
                <div class="bhoechie-tab-content active">

                    <li class="profile1"><i>
                        <img src="image/CS.png" class="iconview"></i>&nbsp;&nbsp;Call Log</li>
                    <!-- Start content -->




                    <%-- <div class="row">
                                    <div class="col-sm-12" style="text-align: center">
                                        <div class="dataTables_length" id="datatable-editable_length">
                                            <label>
                                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                            </label>
                                        </div>
                                    </div>
                                </div>--%>
                    <div class="content padding-top-none">


                        <div class="row" style="text-align: center">
                            <div class=" form">
                                <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                                <div class="form-group ">

                                    <div class="col-lg-3">
                                        <label>
                                            By User/Device : 
                                                                 <asp:DropDownList ID="ddlUserName" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group ">

                                    <div class="col-lg-3">
                                        <label>
                                            By Phone No : 
                                                                   <asp:TextBox ID="txtSrchNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group ">

                                    <div class="col-lg-3">
                                        <label>
                                            From Date :
                                                                    <asp:TextBox ID="txtFrmDate" runat="server" class="form-control" ></asp:TextBox>
                                           <%-- <asp:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFrmDate" Format="dd MMM yyyy" PopupButtonID="txtFrmDate" />--%>
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group ">

                                    <div class="col-lg-3">

                                        <div class="row">
                                            <div class="col-sm-9">
                                                <label>From Time : </label>
                                            </div>

                                            <div class="col-sm-4">
                                                <label>
                                                    <asp:DropDownList ID="ddlFromHour" runat="server" CssClass="form-control" Width="68px">
                                                        <asp:ListItem>HH</asp:ListItem>
                                                        <asp:ListItem>00</asp:ListItem>
                                                        <asp:ListItem>01</asp:ListItem>
                                                        <asp:ListItem>02</asp:ListItem>
                                                        <asp:ListItem>03</asp:ListItem>
                                                        <asp:ListItem>04</asp:ListItem>
                                                        <asp:ListItem>05</asp:ListItem>
                                                        <asp:ListItem>06</asp:ListItem>
                                                        <asp:ListItem>07</asp:ListItem>
                                                        <asp:ListItem>08</asp:ListItem>
                                                        <asp:ListItem>09</asp:ListItem>
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>11</asp:ListItem>
                                                        <asp:ListItem>12</asp:ListItem>
                                                        <asp:ListItem>13</asp:ListItem>
                                                        <asp:ListItem>14</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>16</asp:ListItem>
                                                        <asp:ListItem>17</asp:ListItem>
                                                        <asp:ListItem>18</asp:ListItem>
                                                        <asp:ListItem>19</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>21</asp:ListItem>
                                                        <asp:ListItem>22</asp:ListItem>
                                                        <asp:ListItem>23</asp:ListItem>
                                                    </asp:DropDownList></label>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>
                                                    <asp:DropDownList ID="ddlFromMin" runat="server" CssClass="form-control" Width="72px">
                                                        <asp:ListItem>MM</asp:ListItem>
                                                        <asp:ListItem>00</asp:ListItem>
                                                        <asp:ListItem>01</asp:ListItem>
                                                        <asp:ListItem>02</asp:ListItem>
                                                        <asp:ListItem>03</asp:ListItem>
                                                        <asp:ListItem>04</asp:ListItem>
                                                        <asp:ListItem>05</asp:ListItem>
                                                        <asp:ListItem>06</asp:ListItem>
                                                        <asp:ListItem>07</asp:ListItem>
                                                        <asp:ListItem>08</asp:ListItem>
                                                        <asp:ListItem>09</asp:ListItem>
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>11</asp:ListItem>
                                                        <asp:ListItem>12</asp:ListItem>
                                                        <asp:ListItem>13</asp:ListItem>
                                                        <asp:ListItem>14</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>16</asp:ListItem>
                                                        <asp:ListItem>17</asp:ListItem>
                                                        <asp:ListItem>18</asp:ListItem>
                                                        <asp:ListItem>19</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>21</asp:ListItem>
                                                        <asp:ListItem>22</asp:ListItem>
                                                        <asp:ListItem>23</asp:ListItem>
                                                        <asp:ListItem>24</asp:ListItem>
                                                        <asp:ListItem>25</asp:ListItem>
                                                        <asp:ListItem>26</asp:ListItem>
                                                        <asp:ListItem>27</asp:ListItem>
                                                        <asp:ListItem>28</asp:ListItem>
                                                        <asp:ListItem>29</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                        <asp:ListItem>31</asp:ListItem>
                                                        <asp:ListItem>32</asp:ListItem>
                                                        <asp:ListItem>33</asp:ListItem>
                                                        <asp:ListItem>34</asp:ListItem>
                                                        <asp:ListItem>35</asp:ListItem>
                                                        <asp:ListItem>36</asp:ListItem>
                                                        <asp:ListItem>37</asp:ListItem>
                                                        <asp:ListItem>38</asp:ListItem>
                                                        <asp:ListItem>39</asp:ListItem>
                                                        <asp:ListItem>40</asp:ListItem>
                                                        <asp:ListItem>41</asp:ListItem>
                                                        <asp:ListItem>42</asp:ListItem>
                                                        <asp:ListItem>43</asp:ListItem>
                                                        <asp:ListItem>44</asp:ListItem>
                                                        <asp:ListItem>45</asp:ListItem>
                                                        <asp:ListItem>46</asp:ListItem>
                                                        <asp:ListItem>47</asp:ListItem>
                                                        <asp:ListItem>48</asp:ListItem>
                                                        <asp:ListItem>49</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                        <asp:ListItem>51</asp:ListItem>
                                                        <asp:ListItem>52</asp:ListItem>
                                                        <asp:ListItem>53</asp:ListItem>
                                                        <asp:ListItem>54</asp:ListItem>
                                                        <asp:ListItem>55</asp:ListItem>
                                                        <asp:ListItem>56</asp:ListItem>
                                                        <asp:ListItem>57</asp:ListItem>
                                                        <asp:ListItem>58</asp:ListItem>
                                                        <asp:ListItem>59</asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group ">

                                    <div class="col-lg-3">
                                        <label>
                                            To Date :
                                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ></asp:TextBox>
                                           <%-- <asp:CalendarExtender ID="ce2" runat="server" TargetControlID="txtToDate" Format="dd MMM yyyy" PopupButtonID="txtToDate" />--%>
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group ">

                                    <div class="col-lg-3">

                                        <div class="row">
                                            <div class="col-sm-9">
                                                <label>To Time : </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>
                                                    <asp:DropDownList ID="ddlToHour" runat="server" CssClass="form-control" Width="68px">
                                                        <asp:ListItem>HH</asp:ListItem>
                                                        <asp:ListItem>00</asp:ListItem>
                                                        <asp:ListItem>01</asp:ListItem>
                                                        <asp:ListItem>02</asp:ListItem>
                                                        <asp:ListItem>03</asp:ListItem>
                                                        <asp:ListItem>04</asp:ListItem>
                                                        <asp:ListItem>05</asp:ListItem>
                                                        <asp:ListItem>06</asp:ListItem>
                                                        <asp:ListItem>07</asp:ListItem>
                                                        <asp:ListItem>08</asp:ListItem>
                                                        <asp:ListItem>09</asp:ListItem>
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>11</asp:ListItem>
                                                        <asp:ListItem>12</asp:ListItem>
                                                        <asp:ListItem>13</asp:ListItem>
                                                        <asp:ListItem>14</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>16</asp:ListItem>
                                                        <asp:ListItem>17</asp:ListItem>
                                                        <asp:ListItem>18</asp:ListItem>
                                                        <asp:ListItem>19</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>21</asp:ListItem>
                                                        <asp:ListItem>22</asp:ListItem>
                                                        <asp:ListItem>23</asp:ListItem>
                                                    </asp:DropDownList></label>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>
                                                    <asp:DropDownList ID="ddlToMin" runat="server" CssClass="form-control" Width="72px">
                                                        <asp:ListItem>MM</asp:ListItem>
                                                        <asp:ListItem>00</asp:ListItem>
                                                        <asp:ListItem>01</asp:ListItem>
                                                        <asp:ListItem>02</asp:ListItem>
                                                        <asp:ListItem>03</asp:ListItem>
                                                        <asp:ListItem>04</asp:ListItem>
                                                        <asp:ListItem>05</asp:ListItem>
                                                        <asp:ListItem>06</asp:ListItem>
                                                        <asp:ListItem>07</asp:ListItem>
                                                        <asp:ListItem>08</asp:ListItem>
                                                        <asp:ListItem>09</asp:ListItem>
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>11</asp:ListItem>
                                                        <asp:ListItem>12</asp:ListItem>
                                                        <asp:ListItem>13</asp:ListItem>
                                                        <asp:ListItem>14</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>16</asp:ListItem>
                                                        <asp:ListItem>17</asp:ListItem>
                                                        <asp:ListItem>18</asp:ListItem>
                                                        <asp:ListItem>19</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>21</asp:ListItem>
                                                        <asp:ListItem>22</asp:ListItem>
                                                        <asp:ListItem>23</asp:ListItem>
                                                        <asp:ListItem>24</asp:ListItem>
                                                        <asp:ListItem>25</asp:ListItem>
                                                        <asp:ListItem>26</asp:ListItem>
                                                        <asp:ListItem>27</asp:ListItem>
                                                        <asp:ListItem>28</asp:ListItem>
                                                        <asp:ListItem>29</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                        <asp:ListItem>31</asp:ListItem>
                                                        <asp:ListItem>32</asp:ListItem>
                                                        <asp:ListItem>33</asp:ListItem>
                                                        <asp:ListItem>34</asp:ListItem>
                                                        <asp:ListItem>35</asp:ListItem>
                                                        <asp:ListItem>36</asp:ListItem>
                                                        <asp:ListItem>37</asp:ListItem>
                                                        <asp:ListItem>38</asp:ListItem>
                                                        <asp:ListItem>39</asp:ListItem>
                                                        <asp:ListItem>40</asp:ListItem>
                                                        <asp:ListItem>41</asp:ListItem>
                                                        <asp:ListItem>42</asp:ListItem>
                                                        <asp:ListItem>43</asp:ListItem>
                                                        <asp:ListItem>44</asp:ListItem>
                                                        <asp:ListItem>45</asp:ListItem>
                                                        <asp:ListItem>46</asp:ListItem>
                                                        <asp:ListItem>47</asp:ListItem>
                                                        <asp:ListItem>48</asp:ListItem>
                                                        <asp:ListItem>49</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                        <asp:ListItem>51</asp:ListItem>
                                                        <asp:ListItem>52</asp:ListItem>
                                                        <asp:ListItem>53</asp:ListItem>
                                                        <asp:ListItem>54</asp:ListItem>
                                                        <asp:ListItem>55</asp:ListItem>
                                                        <asp:ListItem>56</asp:ListItem>
                                                        <asp:ListItem>57</asp:ListItem>
                                                        <asp:ListItem>58</asp:ListItem>
                                                        <asp:ListItem>59</asp:ListItem>
                                                    </asp:DropDownList></label>

                                            </div>

                                        </div>
                                    </div>
                                </div>


                                <div class="form-group ">

                                    <div class="col-lg-3">
                                        <label>
                                            BY Direction : 
                                                                 <asp:DropDownList ID="ddlDirection" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                     <asp:ListItem Text="All" Value="100"></asp:ListItem>
                                                                     <asp:ListItem Text="Incoming" Value="1"></asp:ListItem>
                                                                     <asp:ListItem Text="Outgoing" Value="0"></asp:ListItem>
                                                                 </asp:DropDownList>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group" style="text-align: center;">

                                    <div class="col-lg-3">
                                        <div class="col-sm-9">
                                            <label>
                                                <br />
                                                <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                            </label>
                                        </div>
                                    </div>


                                </div>
                            </div>


                            <br />

                            <div class="row">
                                <div class="col-sm-12" style="text-align: center">
                                    <div class="dataTables_length" id="datatable-editable_length">
                                        <label>
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </label>
                                    </div>
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
                                    <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="InComing and OutGoing Calls" ForeColor="26, 59, 105"></asp:Title>
                                </Titles>
                                <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                <Series>
                                </Series>

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


                            <div class="col-lg-5 col-md-6 col-md-offset-5 centered reportcal">
                                <li style="background-color: #418BEB"></li>
                                <li>InComing</li>
                                <li style="background-color: #FFB54F;"></li>
                                <li>OutGoing</li>
                            </div>

                            <br />
                            <br />
                            <br />


                            <div class="table-responsive">
                                <asp:GridView ID="grdmsgh" runat="server" class="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" OnPageIndexChanging="grdmsgh_PageIndexChanging" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" PageSize="20" AllowPaging="true" OnRowDataBound="grdmsgh_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%#Eval("SMSLogId")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Device Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName").ToString()==""?"---":Eval("DeviceName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Phone No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumber" runat="server" Text='<%#MyFormat(Eval("SmsFrom").ToString(),Eval("SmsTo").ToString(),Eval("IsIncoming").ToString())%>'></asp:Label>
                                                <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("CallTo").ToString()%>'></asp:Label>--%>
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:TextBox id="txtNumber" runat="server" Text='<%#Eval("CallFrom")%>' cssclass="TxtBox"></asp:TextBox>                                                                                             
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start DateTime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStartTime" runat="server" Text='<%#Convert.ToDateTime(Eval("LogDateTime").ToString()).ToString("dd-MMM-yyyy HH:mm")%>'></asp:Label>
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:TextBox id="txtDateTime" runat="server" Text='<%#Eval("StartTime")%>' cssclass="TxtBox"></asp:TextBox>                                              
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End DateTime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEndTime" runat="server" Text='<%#Convert.ToDateTime(Eval("LogDateTime").ToString()).ToString("dd-MMM-yyyy HH:mm")%>'></asp:Label>
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:TextBox id="txtDateTime" runat="server" Text='<%#Eval("StartTime")%>' cssclass="TxtBox"></asp:TextBox>                                              
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Duration (HH:MM:SS)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContent" runat="server" Text='<%#Eval("MessageText")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location")%>'></asp:Label>
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>
                                                <asp:TextBox id="txtLocation" runat="server" Text='<%#Eval("Location")%>' cssclass="TxtBox"></asp:TextBox>                                              
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Direction">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDirection" runat="server" Text='<%#Eval("IsIncoming").ToString() == "1" ? "Incoming":"Outgoing" %>'>                                                                                                    
                                                </asp:Label>
                                            </ItemTemplate>
                                            <%--  <EditItemTemplate>
                                            <asp:TextBox id="txtDirection" runat="server" Text='<%#Eval("IsIncomingCall")%>' cssclass="TxtBox"></asp:TextBox>                                                                                        
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                                </asp:GridView>

                            </div>
                            <div class="row" style="text-align: right">
                                <asp:Panel runat="server" ID="MessagePnl" Height="160px" CssClass="msgpopup" Visible="false">

                                    <div class="modal-body" style="text-align: center; color: green;">
                                        <asp:Button ID="btnccl" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
                                        <asp:RadioButton ID="RbtnYou" GroupName="Group1" Text="Send To Yourself" Value="Yes" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;
                                    <asp:RadioButton ID="RbtnOther" GroupName="Group1" Text="Send To Other" Value="No" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                                        <br />
                                        <asp:Label ID="lblmessage" runat="server" Text="Mail To :" Style="margin: 0px auto" ForeColor="Black"></asp:Label>
                                        <asp:TextBox ID="txtMailTo" runat="server" ForeColor="Black"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server"
                                            ControlToValidate="txtMailTo" ErrorMessage="Required!" ValidationGroup="mailsend"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                            ControlToValidate="txtMailTo" Display="Dynamic" ErrorMessage="Enter Valid Email-Id"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="mailsend" ForeColor="Red"></asp:RegularExpressionValidator>
                                        <br />
                                        <asp:Label ID="lblerrorMailTo" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="modal-footer" style="text-align: center;">
                                        <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btnd btncompt" OnClick="Send_Click" ValidationGroup="mailsend" />&nbsp;
                            <asp:Button ID="CancelMail" runat="server" CssClass="btn btnd btncompt" Text="Cancel" OnClick="CancelMail_Click" />
                                    </div>


                                </asp:Panel>
                            </div>
                            <div class="row" style="text-align: right">
                                <asp:Button ID="btnsavetopdf" runat="server" CssClass="btn btnd btncompt" Text="Save To Pdf" OnClick="btnsavetopdf_Click" />
                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btnd btncompt" Text="Print" OnClick="btnPrint_Click" />
                                <asp:Button ID="btnSendtomail" runat="server" CssClass="btn btnd btncompt" Text="Send to Mail" OnClick="btnSendtomail_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
