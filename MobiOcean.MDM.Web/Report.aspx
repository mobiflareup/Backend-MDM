<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="Report.aspx.cs" Inherits="MobiOcean.MDM.Web.Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ScriptManager>--%>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 bhoechie-tab scrollbar" id="style-3">
                    <div class="force-overflow">

                        <div class="bhoechie-tab-content active div">

                            <!---<li style="width:20%; font-size:18px; list-style:none; float:right">Back</li>---->
                            <li class="profile1">Profile Detail</li>
                            <br />
                            <div class="row">
                                <div style="text-align: center; background-color: #2A368B; color: #FFFFFF" class="col-sm-6">
                                    <h4>Profile Name :
                                        <asp:Label ID="txtProfileName" runat="server"></asp:Label></h4>
                                </div>
                                <div class="col-sm-6" style="text-align: right">
                                    <asp:Button ID="btnManage" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Manage Feature" PostBackUrl="~/feature.aspx" />
                                    &nbsp;
                                        <asp:Button ID="btnHistory" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="History" PostBackUrl="~/ViewReportHstry.aspx" />
                                    &nbsp;
                                        <asp:Button ID="btnback" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Back" PostBackUrl="~/profilemaster.aspx" />
                                    <%--<asp:LinkButton ID="lnkbtnviewhistory" runat="server" CssClass="btn-link" CommandName="view" Text="View History"></asp:LinkButton>--%>
                                </div>
                                <br />
                                <br />




                            </div>
                            <br />
                            <div class="row">
                                <div class="table-responsive">
                                    <asp:DataList ID="dlReport" runat="server" Width="99.9%" GridLines="Horizontal" DataKeyField="ProfileFeatureMappingId" OnItemDataBound="dlReport_ItemDataBound" class="table mGrid" HeaderStyle-CssClass="protable" AlternatingItemStyle-BackColor="#ffffff">


                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <h4>
                                                        <asp:Label ID="lblFeatureName" runat="server" Text='<%#Eval("FeatureName") %>'></asp:Label></h4>
                                                </div>
                                                <div class="col-sm-4">
                                                    <b>Current Status:</b>
                                                    <asp:Label ID="lblIsEnable" runat="server" Text='<%#Eval("IsEnable").ToString()=="1"?"Enabled":"Disabled" %>'></asp:Label>
                                                </div>






                                            </div>
                                            <div class="row">
                                                <br />
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-2">&nbsp;</div>
                                                <div class="col-sm-8">
                                                    <%-- <div class="table-responsive" data-pattern="priority-columns">--%>
                                                    <asp:GridView ID="grdrpt" runat="server" class="table mGrid" HeaderStyle-CssClass="protable" GridLines="None"
                                                        DataKeyNames="ProfileFeatureTimingId" AutoGenerateColumns="false" EmptyDataText="No Schedule Available!">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Profile Id" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProfileFeatureMappingId" runat="server" Text='<%#Eval("ProfileFeatureTimingId")%>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Day">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFromDay" runat="server" Text='<%#Eval("FromDay").ToString()=="1"?"Monday":Eval("FromDay").ToString()=="2"?"Tuesday":Eval("FromDay").ToString()=="3"?"Wednesday":Eval("FromDay").ToString()=="4"?"Thursday":Eval("FromDay").ToString()=="5"?"Friday":Eval("FromDay").ToString()=="6"?"Saturday":"Sunday"%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="From Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFromTime" runat="server" Text='<%#Eval("FromTime")%>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="To Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblToTime" runat="server" Text='<%#Eval("ToTime")%>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDuration" runat="server" Text='<%#(string.IsNullOrEmpty(Eval("Duration").ToString()))?"---":Eval("Duration")%>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <%--</div>--%>
                                                <div class="col-sm-2">&nbsp;</div>

                                            </div>



                                        </ItemTemplate>

                                    </asp:DataList>
                                </div>

                            </div>
                            <!-- end: page -->
                        </div>
                        <!-- end Panel -->
                    </div>
                    <!-- container -->
                </div>
                <!-- content -->
                <!-- container -->
                </div>
                <!-- content -->
                
                </div>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- ============================================================== -->
        <!-- End Right content here -->
        <!-- ============================================================== -->
     
</asp:Content>
