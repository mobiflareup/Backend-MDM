<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobiPayment.aspx.cs" Inherits="MobiOcean.MDM.Web.Web.MobiPayment" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!doctype html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="Generator" content="EditPlus®">
    <meta name="Author" content="">
    <meta name="Keywords" content="">
    <meta name="Description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="css/custom1.css">
    <link rel="stylesheet" href="css/custom-responsive.css">
    <%--    <link rel="stylesheet" href="web/css/custom-responsive.css">--%>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <%--<script src="js/respond.js"></script>--%>
    <script src="js/custom-script.js"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <title>Payment</title>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            margin: 0px;
        }

        .popup-title {
            padding: 15px 10px;
            background: #293987;
            color: #fff;
            font-weight: bold;
        }

        .popup1 {
            padding: 10px;
            margin: auto;
            position: absolute !important;
        }
    </style>

</head>
<body>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <nav class="navbar navbar-default">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand" href="<%=MobiOcean.MDM.BAL.Model.Constant.MobiURL %>">
                        <img src="images/logo.png" style="width: 285px; height: 95px"></a>
                </div>
                <div class="pull-right" style="margin-top: 50px;">
                    <h2>Welcome
                        <asp:Label ID="LoginUser" runat="server"></asp:Label></h2>
                </div>
            </div>
        </nav>
        <div class="container-fluid">
            <div class="row">
               <%--<asp:Label ID="lbltest" runat="server" ></asp:Label>--%>
                <br>
                <div class="container">
                    <div class="row">
                        <div class="order-details">
                            <p>
                                <span class="glyphicon glyphicon-shopping-cart"></span>&nbsp;&nbsp;<strong>Order Details</strong>
                                <%--<button type="button" class="btn btn-primary pull-right custom-btn-login">Edit </button>--%>
                                <asp:Button ID="btnEdit" runat="server" class="btn btn-primary pull-right custom-btn-login" Text="Edit" OnClick="btnEdit_Click" />
                            </p>
                            <div class="clearfix"></div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="grdPayment" runat="server" AutoGenerateColumns="false" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No record found." CssClass="table custom-table" HeaderStyle-Font-Bold="true">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("CategoryId")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Solution Set">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSoln" runat="server" Text='<%#Eval("CategoryName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price(Rs.)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrice" runat="server" Text='<%#Eval("Price")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="License/Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLicense" runat="server" Text='<%#Eval("License")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Duration">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDuration" runat="server" Text='<%#Eval("Duration")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Total Price(Rs.)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalPrice1" runat="server" Text='<%#Eval("TotalPrice")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-xs-12 pull-right ">
                            <div class="form-group">
                                <label for="name">Promo Code:</label>
                                <asp:TextBox ID="txtpromocode" runat="server" placeholder="Enter promo code" class="form-control" Style="display: inline-block; width: 50%;"></asp:TextBox>
                                <asp:Button ID="btnapply" CssClass="btn btn-primary" runat="server" Text="Apply" OnClick="btnapply_Click" />
                                <asp:Image ID="imgapplied" runat="server" ImageUrl="~/images/activate.png" Visible="false" ToolTip="Applied" />
                                <asp:Image ID="imgnotapplied" runat="server" ImageUrl="~/image/b_drop.png" Visible="false" ToolTip="Not Applied" />
                                <asp:LinkButton ID="lnkbtnview" runat="server" OnClick="lnkbtnview_Click" Visible="false" ToolTip="Details"><i><img src="../images/details-icon-png-24.png" class="iconview" Style="width:23px;height:23px;"></i></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnremove" runat="server" OnClick="lnkbtnremove_Click" Visible="false" ToolTip="Remove PromoCode"><i><img src="../image/Delete.png" class="iconview" Style="width:23px;height:23px;"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="row" style="text-align: center">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    <br>

                    <div class="row">
                        <div class="bill-form">
                            <div class="col-lg-6" style="border-right: 1px solid #ccc;">
                                <h4><strong>Details:</strong></h4>
                                <br>
                                <div class="billing-info">
                                    <form class="form-horizontal custom-bill-info-form" role="form">
                                        <div class="col-md-12 form-group custom-form-group">
                                            <asp:Label ID="lblUserName" runat="server" class="control-label custom-label col-sm-12"></asp:Label>

                                        </div>
                                        <div class="col-md-12 form-group custom-form-group">
                                            <asp:Label ID="lblEmailId" runat="server" class="control-label custom-label col-sm-12"></asp:Label>

                                        </div>
                                        <div class="col-md-12 form-group custom-form-group">

                                            <asp:Label ID="lblContNo" runat="server" class="control-label custom-label col-sm-12"></asp:Label>

                                        </div>
                                        <div class="col-md-12 form-group custom-form-group">

                                            <asp:Label ID="lblAddr" runat="server" class="control-label custom-label col-sm-12"></asp:Label>

                                        </div>
                                        <div class="col-md-12 form-group custom-form-group">

                                            <asp:Label ID="lblPin" runat="server" class="control-label custom-label col-sm-12"></asp:Label>

                                        </div>
                                        <br>
                                        <div class="col-md-12 form-group custom-form-group">
                                            <div class="col-md-6">
                                                <label>State* :</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" CssClass="form-control" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                <asp:CompareValidator ID="cmpState" runat="server" ControlToValidate="ddlState" ValueToCompare="0" Operator="NotEqual" ValidationGroup="Save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12 form-group custom-form-group">
                                            <div class="col-md-6">
                                                <label>GSTIN/UIN of the Taxpayer :</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtGSTNo" runat="server" CssClass="form-control" placeholder="Enter GSTIN/UIN" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <br />
                                            <br />
                                            <br />
                                        </div>
                                        <div class="col-md-12 form-group custom-form-group">
                                            <div class="col-md-6">
                                                <label>City :</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="Enter City"></asp:TextBox>
                                            </div>
                                            <br />
                                            <br />
                                            <br />
                                        </div>
                                        <div class="col-md-12 form-group custom-form-group">
                                            <div class="col-md-6">
                                                <label>Address :</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address" TextMode="MultiLine" Width="230" Height="75"></asp:TextBox>
                                            </div>
                                            <br />
                                            <br />
                                            <br />
                                        </div>
                                        <br />
                                        <br />
                                        <div class="form-group custom-form-group">
                                            <label class="control-label custom-label col-sm-12" for="usr">Send Order Confirmation Email :</label>
                                            <div class="col-sm-6 ">
                                                <asp:TextBox ID="txtConfEmail" runat="server" CssClass="form-control" placeholder="example@xyz.xyz"></asp:TextBox>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>
                            <div class="col-lg-6 pull-right">
                                <h4><strong>Payment Details:</strong></h4>
                                <br>
                                <form class="form-horizontal custom-form2 custom-form-lg" role="form">
                                    <div class="form-group custom-form-group">
                                        <label class="control-label custom-label col-sm-4 text-left" for="email">Total Price(Rs.) </label>
                                        <div class="col-sm-6" style="padding-top: 7px;">
                                            :&nbsp;<asp:Label ID="lbltotalprice" runat="server" class="control-label custom-label text-left"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group custom-form-group" id="dcprice" runat="server" visible="false">
                                        <label class="control-label custom-label col-sm-4 text-left" for="email">Discount(Rs.) </label>
                                        <div class="col-sm-6" style="padding-top: 7px;">
                                            :&nbsp;<asp:Label ID="lbldiscountedprice" runat="server" class="control-label custom-label text-left"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group custom-form-group" id="labelservicetax" runat="server" visible="false">
                                        <label class="control-label custom-label col-sm-4 text-left" for="email">Service Tax(Rs.)- @<% =MobiOcean.MDM.BAL.Model.Constant.servicetax %>%</label>
                                        <div class="col-sm-6" style="padding-top: 7px;">
                                            :&nbsp;<asp:Label ID="lblservicetax" runat="server" class="control-label custom-label text-left"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group custom-form-group" id="Div1" runat="server" visible="false">
                                        <label class="control-label custom-label col-sm-4 text-left" for="email">CGST(Rs.)- @<% =MobiOcean.MDM.BAL.Model.Constant.CGST %>%</label>
                                        <div class="col-sm-6" style="padding-top: 7px;">
                                            :&nbsp;<asp:Label ID="lblCGST" runat="server" class="control-label custom-label text-left"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group custom-form-group" id="Div2" runat="server" visible="false">
                                        <label class="control-label custom-label col-sm-4 text-left" for="email">SGST(Rs.)- @<% =MobiOcean.MDM.BAL.Model.Constant.CGST %>%</label>
                                        <div class="col-sm-6" style="padding-top: 7px;">
                                            :&nbsp;<asp:Label ID="lblSGST" runat="server" class="control-label custom-label text-left"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group custom-form-group" id="Div3" runat="server" visible="false">
                                        <label class="control-label custom-label col-sm-4 text-left" for="email">IGST(Rs.)- @<% =MobiOcean.MDM.BAL.Model.Constant.IGST %>%</label>
                                        <div class="col-sm-6" style="padding-top: 7px;">
                                            :&nbsp;<asp:Label ID="lblIGST" runat="server" class="control-label custom-label text-left"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group custom-form-group">
                                        <label class="control-label custom-label col-sm-4 text-left" for="usr">Sub Total(Rs.) </label>
                                        <div class="col-sm-6 " style="padding-top: 7px;">
                                            :&nbsp;<asp:Label ID="lblsubtotal" runat="server" class="control-label custom-label text-left"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                </form>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="row" style="text-align: center">
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            </div>


                            <div class="text-center payment-btn">
                                <asp:ImageButton ID="imgDownloadPDF" runat="server" OnClick="imgDownloadPDF_Click" ImageUrl="~/images/images.png" Width="30" Height="22" ValidationGroup="Save" ToolTip="Performa Invoice"></asp:ImageButton>
                                <asp:Button ID="btnProcessToPayment" runat="server" class="btn btn-success custon-btn-proceed" Text="Proceed To Payment" OnClick="btnProcessToPayment_Click" ValidationGroup="Save" />
                                <asp:Button ID="btnProcessToPaymentcancel" runat="server" class="btn btn-success custon-btn-proceed" Text="Cancel" OnClick="btnProcessToPaymentcancel_Click" />
                            </div>
                        </div>
                    </div>
                    <br>
                </div>
            </div>
        </div>

        <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" Style="visibility: hidden" />
        <div class="container-fluid" style="background: #222; color: #fff; padding: 20px;">
            <div class="row">
                <p class="text-center">&copy; 2017</p>
            </div>
        </div>
        <!-- ModalPopupExtender -->
        <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="popup1">
            <div class="modal-dialog modal-lg modalPopup">
                <%--<div class="modal-header">--%>
                <h4 class="popup-title text-center">Promo Code Details</h4>

                <%-- </div>--%>
                <div class="modal-body">
                    <div class="col-lg-2">
                        <div class="" style="border: 1px solid #aaa;">
                            <p class="text-center" style="padding: 25px;">
                                <asp:Label ID="lbldiscount" runat="server"></asp:Label>
                            </p>
                            <p class="text-center" style="background: #1cafa1; color: #fff; padding: 10px;">DEAL</p>
                        </div>
                        <br>
                    </div>
                    <div class="col-lg-10">
                        <h3><strong>Terms And Condition</strong></h3>
                        <br>
                        <p>
                            <asp:Label ID="lbltermsandcondition" runat="server"></asp:Label>
                        </p>
                        <br>
                        <p>
                            <asp:Label ID="lblpromocodedtl" runat="server"></asp:Label>
                            <br />
                            <br />
                            <div class="table-responsive">
                                <table class="table" style="background: #e2e2e2;">
                                    <thead>
                                        <tr>
                                            <th>Mobiocean Promo Code</th>
                                            <th>Upto
                                                <asp:Label ID="lbldiscountamount" runat="server"></asp:Label></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="newuser" runat="server" visible="false">
                                            <td>User</td>
                                            <td>
                                                <asp:Label ID="lblisnewuser" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Categories</td>
                                            <td>
                                                <asp:Label ID="lblCategories" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr id="duration" runat="server" visible="false">
                                            <td>No Of Durations</td>
                                            <td>
                                                <asp:Label ID="lblnoofdurations" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr id="licenses" runat="server" visible="false">
                                            <td>No Of Licenses</td>
                                            <td>
                                                <asp:Label ID="lblnooflicenses" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr id="minimumamt" runat="server" visible="false">
                                            <td>Minimum Amount(Rs.)</td>
                                            <td>
                                                <asp:Label ID="lblminimumamount" runat="server"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </p>
                    </div>

                    <div class="clearfix">
                    </div>

                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnClose" runat="server" Text="Ok" class="btn btn-sm btn-success" />
                </div>
            </div>

        </asp:Panel>




        <script>
            $(document).ready(function () {
                $(".amount-due").click(function () {
                    $(".bill-form").slideToggle();
                });
            });
        </script>
    </form>
</body>
</html>

