<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AssignDailyCustomerByExcel.aspx.cs" Inherits="MobiOcean.MDM.Web.AssignDailyCustomerByExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active div">
                <div class="profile1">
                    &nbsp;&nbsp;Assign Daily Customer by Excel
                     <asp:Button ID="btnBack" runat="server" CssClass="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Back" OnClick="btnBack_Click"></asp:Button>
                </div>
                <br />
                <div class=" row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            Assign Date
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFrDt" runat="server" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ValidationGroup="upload" ErrorMessage="Select the date" ControlToValidate="txtFrDt"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6 text-center">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label></div>
                    </div>
                </div>
                <div class="panel-body table-rep-plugin">
                    <div class="row" style="text-align: center">

                        <div class="row">
                            <div class=" form">
                                <div class="col-md-7">


                                    <div class="form-group ">
                                        <label for="lblEId" class="control-label col-md-5">Choose File* : </label>
                                        <div class="col-md-6">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>
                                        <div class="col-md-1">
                                            <asp:LinkButton ID="btndwnld" runat="server" OnClick="btndwnld_Click"> <img src="images/SampleFormat.png" class="appman" />&nbsp;<u>Sample.xlsx</u></asp:LinkButton>
                                            <%--<asp:ImageButton ID="btndwnld" runat="server" ImageUrl="~/images/SampleFormat.png" Width="40px" Height="50px" AlternateText="Sample.xlsx"  OnClick="btndwnld_Click1"  />--%>
                                        </div>

                                    </div>
                                    <div class="form-group ">

                                        <div class="col-md-10">
                                            <br />
                                        </div>
                                    </div>
                                    <div class="form-group">

                                        <div class="col-md-12">
                                            <asp:Button runat="server" ID="btnupload" Text="Upload" CssClass="btn btnd btncompt waves-effect waves-light" Width="110px" OnClick="btnupload_Click" ValidationGroup="upload" />

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <br />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <br />
                            <br />
                            <div class="table-responsive">

                                <asp:GridView ID="GridView1" AllowPaging="false" GridLines="None" AutoGenerateColumns="true" runat="server" CssClass="table" HeaderStyle-CssClass="protable"></asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrDt]").datepick({
                    dateFormat: 'dd-M-yyyy'
                });

            });
        }
        function HideLabel() {
            var seconds = 9;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>
