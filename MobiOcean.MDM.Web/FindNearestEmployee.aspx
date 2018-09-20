<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="FindNearestEmployee.aspx.cs" Inherits="MobiOcean.MDM.Web.FindNearestEmployee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="cnthesd" runat="server" ContentPlaceHolderID="ContentHead">
   <%-- <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD1mre7vCLF2JZM2qqfe5zmUPmjDliVs7s&sensor=false"></script>--%>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <div class="bhoechie-tab-content active div">

                    <div class="profile1">Find Nearest Employee</div>
                    
                    <br />

                    
                    <div class="row" style="text-align: center">

                        <div class=" form">
                            <div class="form-group ">

                                <div class="col-lg-4">
                                    <label>
                                        Radius (Km) :
                                                               <asp:TextBox ID="txtRadius" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                            </div>
                            <div class="form-group ">

                                <div class="col-lg-4">
                                    <label>
                                        Duration :
                                                               <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                            </div>

                            <div class="form-group ">

                                <div class="col-lg-4">
                                    <label>
                                        <br />
                                        <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click" />
                                    </label>
                                </div>
                            </div>


                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12" style="text-align: center">
                            <div class="dataTables_length" id="datatable-editable_length">
                                <label>
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </label>
                            </div>
                        </div>
                    </div>


                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="grdUser" runat="server" DataKeyNames="UserId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                            PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No record found." OnPageIndexChanging="grdUser_PageIndexChanging" Width="100%" OnRowDataBound="grdUser_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserCode" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                    </ItemTemplate>


                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Device Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("DeviceName")%>'></asp:Label>
                                    </ItemTemplate>


                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Mobile No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo")%>'></asp:Label>
                                    </ItemTemplate>


                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("Location")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="LogDateTime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreationDate" runat="server" Text='<%#MyFormat(Eval("LogDateTime").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                        </asp:GridView>

                    </div>

                    <div class="row">

                        <div class="col-lg-12">

                            <div id="map_canvas"></div>


                        </div>
                    </div>

                    <!-- train section -->




                </div>
            </div>
        </div>
 <script type="text/javascript">
        var markers = JSON.parse('<%=ShowOnMap()%>');
        var center = [];
        window.onload = function () {
                center.push(markers[0].lat);
                center.push(markers[0].lng);
                LoadMap("map_canvas", center);
                markerPoint(markers);
        }
    </script>
</asp:Content>
