<%@ Page Title="Add GeoFence" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AddGeoFence.aspx.cs" Inherits="MobiOcean.MDM.Web.AddGeoFence" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    

    <!-- Start content -->
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active div">

                <div class="profile1" style="margin: 0px;">
                           Add Route  
                    <div class="clearfix"></div>
                        </div>
                <br />

               
                <div class=" form">
                    <div class="col-lg-7">

                        <div class="form-group ">
                            <label for="firstname" class="control-label col-lg-4">Route Code* :</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" PlaceHolder="Route Code"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label for="company" class="control-label col-lg-4">Route Name* : </label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" PlaceHolder="Route Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="txtMobileNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                
                            </div>
                        </div>
                        <div class="form-group ">
                            <label for="firstname" class="control-label col-lg-4">Description :</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" PlaceHolder="Description"></asp:TextBox>
                                <br />
                            </div>
                        </div>

                        <div class="form-group ">
                            <label for="firstname" class="control-label col-lg-4">Start Location* :</label>
                            <div class="col-lg-8">
                                
 
                                <input type="text" id="txtPlaces" name="startlocation" class="form-control StartLocation" placeholder="Start Location" onkeydown="if (event.keyCode == 13) {return false;}" /><!-- onkeyup="getplaceonfocus();" -->
                                 
                                 <div id="result1"></div>
                                        <div id="suggestdetail1" style="display: block;" ></div>
                                     </div>
                               <div class="clearfix"></div>

                            </div>
                        
                        <div class="form-group ">
                            <label for="firstname" class="control-label col-lg-4">Destination* :</label>
                            <div class="col-lg-8">
                                     <input type="text" id="txtPlaces1" name="deslocation" class="form-control EndLocation" placeholder="Destination" onkeydown="if (event.keyCode == 13) {return false;}" /><!-- onkeyup="getplace();" -->
                                         <div id="result"></div>
                                        <div id="suggestdetail" style="display: block;" ></div>
                                </div>

                            </div>
                        </div>

                        
                        <div class="form-group ">
                            <label for="firstname" class="control-label col-lg-4"></label>
                            <div class="col-lg-8 text-center">
                                <br />
                                <asp:Button ID="Draw" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Draw Route" OnClick="Draw_Click" />
                                <asp:Button ID="btnCancl" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnCancl_Click" />
                            </div>
                        </div>

                        <div class="form-group ">

                            <div class="col-lg-7">
                                <br />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="form-group" style="text-align: center">

                        <div class="col-lg-12">

                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <br />
                             <div id="map" style="height:500px;z-index:1;"></div>
                            
                        </div>
                        <div class="form-group">
                            <div class="col-lg-offset-2 col-lg-7" style="text-align: center">
                                <asp:Button ID="btnAssign" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save" OnClick="btnSave_Click" ValidationGroup="save" Visible="false" />
                                <asp:Button ID="btnCanceldraw" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnCancel_Click" Visible="false" />
                            </div>
                        </div>
                        <div id="info" style="visibility: hidden"></div>
                    </div>

                </div>
                 <asp:HiddenField ID="hidden" runat="server" />
                <asp:HiddenField ID="hdnStartAddress" runat="server" />
                <asp:HiddenField ID="hdnStartLat" runat="server" />
                <asp:HiddenField ID="hdnStartLong" runat="server" />
                <asp:HiddenField ID="hdnEndAddress" runat="server" />
                <asp:HiddenField ID="hdnEndLat" runat="server" />
                <asp:HiddenField ID="hdnEndLong" runat="server" />
            </div>

        </div>
   
    <script>
        $(document).ready(function () {
            document.getElementById('txtPlaces1').value = document.getElementById('<%=hdnEndAddress.ClientID %>').value;
            document.getElementById('txtPlaces').value = document.getElementById('<%=hdnStartAddress.ClientID %>').value;
        });
    </script>
    <script type="text/javascript">

        function initialize() {
            var pts = [];
            var data = JSON.parse('<%=getdata() %>');
            var center = [data[0].Latiude, data[0].Longitude]
            LoadMap('map', center);
            for (i = 0; i < data.length; i++) {
                pts.push([data[i].Latiude, data[i].Longitude]);
            }
            create_polygons_editable(pts);
        }
        $(document).ready(function () {
            $('.StartLocation').keyup(function () {
                var TxtBoxid = $(this).attr('id');
                var HiddenStartAddr = document.getElementById('<%=hdnStartAddress.ClientID %>').id;
                var HiddenStartLat = document.getElementById('<%=hdnStartLat.ClientID %>').id;
                var HiddenStartLng = document.getElementById('<%=hdnStartLong.ClientID %>').id;
                GetLocationFromApi(TxtBoxid, HiddenStartLat, HiddenStartLng, HiddenStartAddr);
            });
            $('.EndLocation').keyup(function () {
                var TxtBoxid = $(this).attr('id');
                var HiddenEndAddr = document.getElementById('<%=hdnEndAddress.ClientID %>').id;
                var HiddenEndLat = document.getElementById('<%=hdnEndLat.ClientID %>').id;
                var HiddenEndLng = document.getElementById('<%=hdnEndLong.ClientID %>').id;
                GetLocationFromApi(TxtBoxid, HiddenEndLat, HiddenEndLng, HiddenEndAddr);
            });
           
        });
    </script>
   
</asp:Content>

