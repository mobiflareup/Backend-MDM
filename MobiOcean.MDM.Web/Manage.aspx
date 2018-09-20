<%@ Page Title="Manage & View" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="MobiOcean.MDM.Web.Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="profile1">Manage & View</div>
            <br />
            <br />
            <div class="row">
                <div class="col-lg-3">

                    <div class="daswit admindashb" style="width: 100%; border: 2px solid #747580; min-width: 205px;">
                        <a href="UserMaster.aspx">
                            <img src="image/manage.png" onmouseover="this.src='image/manage-hover.png'" onmouseout="this.src='image/manage.png'" /></a><br/>
                    <a href="UserMaster.aspx" ><p style="background:#747580;color:#fff;padding:10px 0px;font-weight:bold;margin:0px;"><span class="spandash"> USER DETAILS</span></p></a>
               
                    </div>
                    <br />
                </div>
                <div class="col-lg-3">

                    <div class="admindashb daswit" style="width: 100%; border: 2px solid #30be80; min-width: 205px;">
                        <a href="UserDeviceModel.aspx">
                            <img src="image/asset.png" onmouseover="this.src='image/asset-hover.png'" onmouseout="this.src='image/asset.png'" /></a><br/>
                         <a href="UserDeviceModel.aspx" ><p style="background:#30be80;color:#fff;padding:10px 0px;font-weight:bold;margin:0px;"><span class="spandash"> ASSET TRACKING</span></p></a>
                    </div>
                    <br />
                </div>
                <div class="col-lg-3">

                    <div class="admindashb daswit" style="width: 100%; border: 2px solid #fc6217; min-width: 205px;">
                        <a href="SosReport.aspx">
                            <img src="image/sosda.png" onmouseover="this.src='image/sosda-hover.png'" onmouseout="this.src='image/sosda.png'" /></a><br/>
                        <a href="SosReport.aspx" ><p style="background:#fc6217;color:#fff;padding:10px 0px;font-weight:bold;margin:0px;"><span class="spandash"> SOS</span></p></a>
                    </div>
                    <br />
                </div>
               
                <div class="col-lg-3">
                    <div class="admindashb daswit" style="width: 100%; border: 2px solid #6b75ec; min-width: 205px;">
                        <a href="CustomerDetails.aspx">
                            <img src="image/customerDetails.png" onmouseover="this.src='image/customerDetails-hover.png'" onmouseout="this.src='image/customerDetails.png'" /></a><br>
                      <a href="CustomerDetails.aspx" ><p style="background:#6b75ec;color:#fff;padding:10px 0px;font-weight:bold;margin:0px;"><span class="spandash"> CUSTOMER DETAILS</span></p></a>
                    </div>
                    <br />
                </div>
                <div class="clearfix"></div>
                <br />
                <br />

            </div>

        </div>
    </div>

</asp:Content>

