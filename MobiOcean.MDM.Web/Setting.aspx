<%@ Page Title="Setting-&-Configuration" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="MobiOcean.MDM.Web.Setting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">
                    <div class="bhoechie-tab-content active div">

                        <div class="profile1">Setting-&-Configuration </div>
                        <br />
                        <br />

                        <div class="row">
                            <ul class="nav nav-tabs">
                                <li class="active sdmtm" id="Chng" runat="server"><a data-toggle="tab" href="#" id="flip1">CHANGE PASSWORD</a></li>
                                <%if (Convert.ToInt32(Session["Role"].ToString()) != 4)
                                    { %>
                                <li class="sdmtm" id="Subsc" runat="server"><a data-toggle="tab" href="#" id="flip2">SUBSCRIPTION DETAIL</a></li>
                                <li class="sdmtm" id="AddAdmin" runat="server" visible="false"><a data-toggle="tab" href="#" id="flip3">ADD ADMIN</a></li>
                                <li class="sdmtm" id="subcribe" runat="server"><a data-toggle="tab" href="#" id="flip4">SUBSCRIBE</a></li>
                                <%} %>
                            </ul>
                        </div>

                        <div class="row">
                            <asp:Button ID="btnForm" runat="server" OnClick="btnForm_Click" Style="display: none" />
                            <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Style="display: none" />
                            <asp:Button ID="btnAddAdmin" runat="server" OnClick="btnAddAdmin_Click" Style="display: none" />
                            <asp:Button ID="btnsubcrib" runat="server" OnClick="btnsubcrib_Click" Style="display: none" />
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <asp:Label ID="lblpopmsg" runat="server"></asp:Label>
                        </div>
                        <div class="tab-content">

                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

                                <asp:View ID="Tab1" runat="server">
                                    <div id="home" class="tab-pane fade in active">
                                        <div class="row">
                                            <div class="col-lg-12">

                                                <div class=" form">
                                                    <div class="col-lg-7">
                                                        <div class="form-group ">
                                                            <label for="bname" class="control-label col-lg-4">Old Password* : </label>
                                                            <div class="col-lg-8">
                                                                <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                    ControlToValidate="txtOldPassword" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                        </div>
                                                        <div class="form-group ">
                                                            <label for="firstname" class="control-label col-lg-4">New Password* : </label>
                                                            <div class="col-lg-8">
                                                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                                <asp:BalloonPopupExtender ID="BalloonPopupExtender3" TargetControlID="txtNewPassword" UseShadow="true"
                                                                    DisplayOnFocus="true" Position="TopRight" BalloonPopupControlID="pswd_info" BalloonStyle="Rectangle"
                                                                    runat="server" />
                                                                <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator3" runat="server" ValidationGroup="save"
                                                                    ErrorMessage="Password must be Minimum 8 characters long with at least one numeric, one upper case character, One lower case character and one special character."
                                                                    ForeColor="Red" ControlToValidate="txtNewPassword" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}">

                                                                </asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                    ControlToValidate="txtNewPassword" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <label for="lastname" class="control-label col-lg-4">Confirm Password* : </label>
                                                            <div class="col-lg-8">

                                                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ControlToValidate="txtConfirmPassword" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                <asp:CompareValidator ID="cv1" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword"
                                                                    ErrorMessage="Not Match..!" ForeColor="Red" ValidationGroup="save"></asp:CompareValidator>
                                                            </div>
                                                        </div>


                                                        <div class="form-group">
                                                            <label class="control-label col-lg-4"></label>
                                                            <div class="col-lg-8">
                                                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-lg-offset-4 col-lg-6">
                                                                <asp:Button ID="btnChange" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Change" ValidationGroup="save" OnClick="btnChange_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-5">
                                                    </div>


                                                </div>

                                            </div>
                                        </div>

                                    </div>

                                </asp:View>

                                <asp:View ID="Tab2" runat="server">

                                    <div class="row">

                                        <div class="col-md-8" style="text-align: left;">

                                            <div class=" form">
                                                <div class="form-group ">
                                                    <div class="col-md-6">
                                                        <label for="bname">No of Licence(S) : </label>

                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtNoOfLicense" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <div class="col-md-6">
                                                        <label for="firstname">Time Period(In Month) : </label>

                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtTimePeriod" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <div class="col-md-6">
                                                        <label for="lastname">Remaining Time Period(In Days) : </label>

                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtRemainingTimePeriod" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <div class="col-md-6">
                                                        <label for="lastname">Total No of SMS : </label>

                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtSmsCount" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-4" style="text-align: center;">

                                            <asp:Button ID="btnBuy" runat="server" CssClass="btn btnd btncompt waves-effect" Visible="false" Text="Buy More Licence" />
                                            <br />
                                            <br />
                                            <asp:Button ID="btnExtDur" runat="server" CssClass="btn btnd btncompt waves-effect" Visible="false" Text="Extend Duration" />
                                            <br />
                                            <br />
                                            <br />


                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" style="text-align: center;">
                                            <div style="text-align: center;">
                                                <b style="font-size: 20px">Select Solution From The List.</b>
                                            </div>
                                            <br />
                                            <div class="row" style="height: 200px; overflow: auto">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grdSoln" runat="server" AutoGenerateColumns="false" CssClass="table mGrid" EmptyDataText="No record found." GridLines="None" HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Category Id" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCategoryId" runat="server" Text='<%#Eval("CategoryId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Solutions">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSolns" runat="server" Text='<%#Eval("CategoryName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkboxRows_Parents" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkboxHeader_Parents" runat="server" AutoPostBack="true" OnCheckedChanged="chkbox_CheckedChanged" />
                                                                </HeaderTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDuration" runat="server" Text='<%#Eval("CategoryName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>

                                                </div>
                                            </div>

                                            <br />
                                            <asp:Button ID="btnPurchase" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Purchase More Solutions" OnClick="btnPurchase_Click" />
                                        </div>
                                        <div class="col-md-6" style="text-align: center;">
                                            <div style="text-align: center;">
                                                <b style="font-size: 20px">Active Solution Set :</b>
                                            </div>
                                            <br />

                                            <div class="row" style="height: 200px; overflow: auto">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grdActiveSoln" runat="server" AutoGenerateColumns="false" CssClass="table mGrid" EmptyDataText="No record found." GridLines="None" HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Active Solutions">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("CategoryName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                    <div class="col-md-12" style="text-align: center;">
                                        <b style="font-size: 20px;">Billing History</b>
                                    </div>
                                    <br />
                                    <br />
                                    <br />

                            <div class="form-group ">
                                <div class="col-lg-3">
                                    <br />
                                    <b>Select Range Form :</b>
                                </div>
                            </div>
                                    <div class="form-group ">
                                        <div class="col-lg-3">
                                            <label>
                                                From Date :
                                                            <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <div class="col-lg-3">
                                            <label>
                                                To Date :
                                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <div class="col-lg-3">
                                            <br />
                                            <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Search" OnClick="btnSrch_Click" />
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="form-group ">
                                            <div class="row" style="height: 200px; overflow: auto">
                                                <div class="table-responsive">

                                                    <asp:GridView ID="grdBillingHstry" runat="server" AutoGenerateColumns="false" CssClass="table mGrid" EmptyDataText="No record found." GridLines="None" HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Category Id" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSubscriptionId" runat="server" Text='<%#Eval("SubscriptionId") %>'></asp:Label>
                                                                    <asp:Label ID="lblCity" runat="server" Text='<%#Eval("City") %>'></asp:Label>
                                                                    <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label>
                                                                    <asp:Label ID="lblGSTNo" runat="server" Text='<%#Eval("GSTNo") %>'></asp:Label>
                                                                    <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId") %>'></asp:Label>
                                                                    <asp:Label ID="lblDiscountAmount" runat="server" Text='<%#Eval("DiscountAmount") %>'></asp:Label>
                                                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                                                    <asp:Label ID="lblCGST" runat="server" Text='<%#Eval("CGST") %>'></asp:Label>
                                                                    <asp:Label ID="lblSGST" runat="server" Text='<%#Eval("SGST") %>'></asp:Label>
                                                                    <asp:Label ID="lblIGST" runat="server" Text='<%#Eval("IGST") %>'></asp:Label>
                                                                    <asp:Label ID="lblSubTotal" runat="server" Text='<%#Eval("SubTotal") %>'></asp:Label>
                                                                    <asp:Label ID="lblCreatedtime" runat="server" Text='<%#Eval("CreationDate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Invoice">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lkbtndwnPDF" runat="server" Text='<%#Eval("InvoiceNo") %>' OnClick="lkbtndwnPDF_Click" ToolTip="Download Invoice PDF" ForeColor="Green"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDataPaid" runat="server" Text='<%#MyFormat(Eval("CreationDate").ToString()) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount Paid">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmt" runat="server" Text='<%#Eval("SubTotal") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Download Invoice" Visible="false">
                                                                <ItemTemplate>
                                                                    <img class="iconview" src="image/pdf-icon.png"></img>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <br />
                                    <br />






                                </asp:View>
                                <asp:View ID="Tab3" runat="server">
                                    <div id="Div2" class="tab-pane fade  in active">
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                </div>
            </div>
                </div>
            <asp:Panel ID="pswd_info" runat="server">
                <h6><u><b>PASSWORD POLICY</b></u><br />
                    1. Minimum 8 Characters.<br />
                    2. At Least One Numeric.<br />
                    3. One Upper Case Character.<br />
                    4. One Lower Case Character.<br />
                    5. One Special Character.<br />
                </h6>
            </asp:Panel>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>


    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(function () {
                $("#flip1").bind("click", function () {
                    document.getElementById('<%= btnForm.ClientID %>').click();
                });
                $("#flip2").bind("click", function () {
                    document.getElementById('<%= btnExcel.ClientID %>').click();
                });
                $("#flip3").bind("click", function () {
                    document.getElementById('<%= btnAddAdmin.ClientID %>').click();
                });
                $("#flip4").bind("click", function () {
                    document.getElementById('<%= btnsubcrib.ClientID %>').click();
                });
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


</asp:Content>

