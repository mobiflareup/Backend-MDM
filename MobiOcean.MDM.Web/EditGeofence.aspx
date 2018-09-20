<%@ Page Title="Edit GeoFence" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="EditGeofence.aspx.cs" Inherits="MobiOcean.MDM.Web.EditGeofence" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

   
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="bhoechie-tab-content active div">

           <div class="profile1" style="margin: 0px;">
                           Edit Route
                            <div class="clearfix"></div>
                        </div>
                <br />

            
            <div class=" form">
                <div class="col-lg-7">

                    <div class="form-group ">
                        <label for="firstname" class="control-label col-lg-4">Route Code* :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group ">
                        <label for="company" class="control-label col-lg-4">Route Name* : </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtMobileNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                           
                        </div>
                    </div>
                    <div class="form-group ">
                        <label for="firstname" class="control-label col-lg-4">Description :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>


                    <div class="form-group ">

                        <div class="col-lg-7">
                            <br />
                        </div>
                    </div>


                </div>

                <div class="row">
                    <div class="col-lg-12">
                       
                         <div id="map" style="height:500px;z-index:1;"></div>

                    </div>
                </div>
                <div class="form-group ">

                    <div class="col-lg-7">
                        <br />
                    </div>
                </div>
                <div class="form-group ">
                   
                    <div class="col-lg-7">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </div>


                <div class="form-group">
                    <div class="col-lg-offset-2 col-lg-7" style="text-align: center">
                        <asp:Button ID="btnAssign" runat="server" CssClass="btn btnd btncompt" Text="Update" OnClick="btnSave_Click" ValidationGroup="save" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </div>
                <div id="info" style="visibility: hidden"></div>
            </div>

            <asp:HiddenField ID="hidden" runat="server" />
        </div>

    </div>


    <script type="text/javascript">
          function initialize() {
            var pts = [];
            var data = JSON.parse('<%=getdata() %>');
            var center = [data[0].Latiude, data[0].Longitude]
            LoadMap('map', center);
            for (i = 0; i < data.length; i++) {
                pts.push([data[i]["Latiude"], data[i]["Longitude"]]);
            }
            create_polygons_editable(pts);
          }

        
     
    </script>

</asp:Content>

