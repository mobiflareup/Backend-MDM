<%@ Page Title="Wi-Fi Sensor" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="Sensor.aspx.cs" Inherits="MobiOcean.MDM.Web.Sensor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">

                <div class="profile1">
                   &nbsp;&nbsp;Wi-Fi Sensor
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-xs-12 col-sm-4">
                        <asp:DropDownList ID="dtBranch" runat="server" class="btn btn-danger btn-select" AppendDataBoundItems="true">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="dtBranch" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                    </div>
                    <div class="col-xs-12 col-sm-4">
                        <asp:DropDownList ID="dtDepartment" runat="server" class="btn btn-warning btn-select" AppendDataBoundItems="true">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="dtDepartment" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                    </div>
                    <div class="col-xs-12 col-sm-4">

                        <div class="btn btn-primary btn-select">
                            <input type="hidden" class="btn-select-input" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false" id="" name="" value="" />
                            <span class="btn-select-value">User Details</span>
                            <span class='btn-select-arrow glyphicon glyphicon-chevron-down'></span>
                            <ul>
                                <a href="#" id="flip1">
                                    <li>Fill Form</li>
                                </a>
                                <a href="#" id="flip2">
                                    <li>Import Excel</li>
                                </a>
                            </ul>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <br />
                <br />
                <asp:Button ID="btnForm" runat="server" OnClick="btnForm_Click" Style="display: none" />
                <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Style="display: none" />

                <div class="row" style="text-align: ">
                    <asp:Label ID="lblresult" runat="server"></asp:Label>
                </div>
                <br />

                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="-1">
                    <asp:View ID="Tab1" runat="server">
                        <div id="panel21" class="flipkey">

                            <a id="summa"></a>
                            <asp:Panel ID="Panel1" class="form-group" runat="server">
                                <div class="panel-body table-rep-plugin">
                                    <div class=" form">
                                        <div class="col-lg-7">
                                            <div class="form-group ">
                                                <label for="lblname" class="control-label col-lg-4">Sensor Name* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtsen" runat="server" placeholder="Sensor Name" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsen" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group ">
                                                <label for="lblmob" class="control-label col-lg-4">Description : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtdesc" runat="server" CssClass="form-control" placeholder="Description"></asp:TextBox>
                                                    <br />
                                                </div>
                                            </div>


                                            <div class="form-group ">
                                                <label for="lblEId" class="control-label col-lg-4">BSSID* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtBssid" runat="server" CssClass="form-control" placeholder="BSSID"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBssid" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEId" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="lblEId" class="control-label col-lg-4">SSID* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtSsid" runat="server" CssClass="form-control" placeholder="SSID"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSsid" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="lblEId" class="control-label col-lg-4">Password* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtPwd" runat="server" CssClass="form-control" placeholder="Password"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldVatxtPwdlidator3" runat="server" ControlToValidate="txtPwd" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group ">

                                                <div class="col-lg-12">
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <%--<label for="phone" class="control-label col-lg-2">Manager Contact No. :</label>--%>
                                                <div class="col-lg-10">
                                                    <asp:Label ID="lblpopmsg" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-lg-offset-2 col-lg-12">
                                                    <asp:Button ID="BtnSave" runat="server" OnClick="btnsave_Click" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Reset" OnClick="btnCancel_Click" />
                                                    <%--                                     <asp:Button ID="btncancelsensor" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btncancelsensor_Click" />--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>

                    </asp:View>
                    <asp:View ID="Tab2" runat="server">
                        <div id="panel2" class="flipkey">

                            <a id="summa1"></a>
                            <asp:Panel ID="Panel2" class="form-group" runat="server">

                                <div class="row">
                                    <div class=" form">
                                        <div class="col-lg-7">
                                            <div class="form-group ">
                                                <label for="lblEId" class="control-label col-lg-4">Choose File* : </label>
                                                <div class="col-lg-8">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" />
                                                </div>
                                            </div>
                                            <div class="form-group ">

                                                <div class="col-lg-10">
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="form-group">

                                                <div class="col-lg-10" style="text-align: center">
                                                    <asp:Button runat="server" ID="btnupload" Text="Upload" CssClass="btn btnd btncompt waves-effect waves-light" ValidationGroup="save" Width="110px" OnClick="btnUpload_Click" />
                                                    <asp:Button ID="Button1" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btncancelsensor_Click" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="table-responsive" data-pattern="priority-columns">
                                        <asp:GridView ID="GridView1" AllowPaging="false" GridLines="None" AutoGenerateColumns="true" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable"></asp:GridView>

                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>



    <script type="text/javascript">
        function pageLoad(sender, args) {
            $("#flip1").bind("click", function () {
                document.getElementById('<%= btnForm.ClientID %>').click();
                });
                $("#flip2").bind("click", function () {
                    document.getElementById('<%= btnExcel.ClientID %>').click();
                  });
              }
              function HideLabel() {
                  var seconds = 5;
                  setTimeout(function () {
                      document.getElementById("<%=lblresult.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>

</asp:Content>

