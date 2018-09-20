<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="AddArea.aspx.cs" Inherits="MobiOcean.MDM.Web.AddArea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">   
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <!-- Start content -->
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab">
        <div class="bhoechie-tab-content active div">
            <div class="profile1" style="margin: 0px;">
                          Add Area
                            <div class="clearfix"></div>
                        </div>
            <br />
            <br />
            <div class=" form">
                <div class="col-lg-7">
                    <div class="form-group ">
                        <label for="bname" class="control-label col-lg-4">Area Name* : </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtArea" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtArea" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group ">
                        <label for="lastname" class="control-label col-lg-4">Location* : </label>
                        <div class="col-lg-8">
                            
                                <input type="text" id="txtLocation" name="AreaLocation" class="form-control AreaLocation" placeholder="Enter a location" onkeydown="if (event.keyCode == 13) {return false;}" /> <!-- onfocus="getplaceonfocus();"  onkeyup="getGeofenceLoc();" -->
                            <br />

                        </div>
                    </div>
                    <div class="form-group ">
                        <label for="lastname" class="control-label col-lg-4">Radius(In meter)* :</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtRadius" runat="server" CssClass="form-control" placeholder="Enter a Radius"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                ControlToValidate="txtRadius" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="Regex2" runat="server" ValidationExpression="((\d+)((\.\d{1,15})?))$"
                                ErrorMessage="Wrong!" ControlToValidate="txtRadius" ValidationGroup="save" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-4"></label>
                        <div class="col-lg-8">
                            <asp:Label ID="Label1" runat="server"></asp:Label>

                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-lg-offset-4 col-lg-6">
                            <asp:Button ID="btnAssign" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Draw" ValidationGroup="save" OnClick="Draw_Click" />
                            <asp:Button ID="Button1" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnC_Click" />
                        </div>
                    </div>
                </div>
                <br /><br />
                <div class="form-group" style="text-align: center">
                    <br />
                    <div class="col-lg-12">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12">
                         <div id="map" style="height:500px;z-index:1;"></div>
                    </div>
                    <br /><br />
                    <div class="form-group">
                        <div class="col-lg-offset-2 col-lg-7" style="text-align: center">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save" OnClick="btnsave_Click" ValidationGroup="save" Visible="false" />
                            <asp:Button ID="btnC" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" Visible="false" />
                        </div>
                    </div>
                    <div id="info" style="visibility: hidden"></div>
                </div>
                <asp:HiddenField ID="hidden" runat="server" />
            <asp:HiddenField ID="hdnStartAddress" runat="server" />
            <asp:HiddenField ID="hdnStartLat" runat="server" />
            <asp:HiddenField ID="hdnStartLong" runat="server" />
            <asp:HiddenField ID="hdnRadius" runat="server" />

            </div>

        </div>
    </div>

   

    <script>
        $(document).ready(function () {
            document.getElementById('txtLocation').value = document.getElementById('<%=hdnStartAddress.ClientID %>').value;
        });
    </script>


    <script type="text/javascript">
        var pts = [], radius;
        function initialize() {
            var data = JSON.parse('<%=getdata() %>');
            var center = [data[0].Latiude, data[0].Longitude]
            LoadMap('map', center);
            radius = data[0].Radius;
            pts.push(data[0].Latiude, data[0].Longitude);
            DrawCircle(pts, radius);
            
        }
         $(document).ready(function () {
            
             $('.AreaLocation').keyup(function () {

                var TxtBoxid = $(this).attr('id');
                var HiddenAddr = document.getElementById('<%=hdnStartAddress.ClientID %>').id;
                var HiddenLat = document.getElementById('<%=hdnStartLat.ClientID %>').id;
                var HiddenLng = document.getElementById('<%=hdnStartLong.ClientID %>').id;
                document.getElementById('<%=hdnRadius.ClientID%>').value = 100;
                GetLocationFromApi(TxtBoxid, HiddenLat, HiddenLng, HiddenAddr);
            });
           
        });

    </script>
    
</asp:Content>
