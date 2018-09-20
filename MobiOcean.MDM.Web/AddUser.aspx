<%@ Page Title="Add User" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeBehind="AddUser.aspx.cs" Inherits="MobiOcean.MDM.Web.AddUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">

                <div class="profile1">
                    &nbsp;&nbsp;Add User
                </div>
                <br />
                <br />

                <div class="row">
                    <div class="col-xs-12 col-sm-4">
                        <asp:DropDownList ID="dtBranch" runat="server" class="btn btn-danger btn-select" AppendDataBoundItems="true" style="text-align:left">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="dtBranch" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                    </div>
                    <div class="col-xs-12 col-sm-4">

                        <asp:DropDownList ID="dtDepartment" runat="server" class="btn btn-warning btn-select" AppendDataBoundItems="true" style="text-align:left">
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

                <asp:Button ID="btnForm" runat="server" OnClick="btnForm_Click" Style="display: none" />
                <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Style="display: none" />
                <br />
                <div class="row" style="text-align: center">
                    <asp:Label ID="lblpopmsg" runat="server"></asp:Label>
                </div>
                <br />

                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="-1">

                    <asp:View ID="Tab1" runat="server">
                        <div id="panel21" class="flipkey">

                            <a id="summa"></a>
                            <asp:Panel ID="Panel1" class="form-group" runat="server">
                                <div class="panel-body table-rep-plugin">
                                    <div class="form">
                                        <div class="col-lg-7">
                                            <div class="form-group">
                                                <label for="Client" class="control-label col-lg-4">
                                                    <asp:Label ID="lblClient" runat="server" Text="Client Name* : "></asp:Label></label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged"></asp:DropDownList><br />
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="lblname" class="control-label col-lg-4">Name* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="lblEId" class="control-label col-lg-4">Employee ID* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtEId" runat="server" CssClass="form-control" placeholder="Employee Id"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEId" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="lblEmail" class="control-label col-lg-4">Email ID* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server"
                                                        ControlToValidate="txtemail" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ControlToValidate="txtemail" ForeColor="Red" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter valid Email-Id" ValidationGroup="save" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>

                                            <div class="form-group ">
                                                <label for="lbldst" class="control-label col-lg-4">Designation : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtdst" runat="server" CssClass="form-control" placeholder="Designation"></asp:TextBox>
                                                    <br />
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdst" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="lblRole" class="control-label col-lg-4">Role* : </label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="droprole" runat="server" class="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="droprole_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="comp" runat="server" ControlToValidate="droprole" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="rptmngr" class="control-label col-lg-4">Reporting Manager* : </label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlRportngMngr" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <br />

                                                </div>
                                            </div>
                                            <br />
                                            <div class="form-group ">
                                                <label for="lblEId" class="control-label col-lg-4">Device Ownership* : </label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="drpOwner" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpOwner" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <%--<label class="control-label col-lg-4">Password* : </label>--%>
                                                <asp:Label ID="lblpwd" runat="server" class="control-label col-lg-4" Text="Password* :" Font-Bold="true"></asp:Label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtpwd" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                    <asp:BalloonPopupExtender ID="BalloonPopupExtender1" TargetControlID="txtpwd" UseShadow="true"
                                                        DisplayOnFocus="true" Position="BottomRight" BalloonPopupControlID="pswd_info" BalloonStyle="Rectangle"
                                                        runat="server" />

                                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorpwd" runat="server" ValidationGroup="save"
                                                        ErrorMessage="Password must be Minimum 8 characters long with at least one numeric, one upper case character, One lower case character and one special character."
                                                        ForeColor="Red" ControlToValidate="txtpwd" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}">

                                                    </asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="req" runat="server" ControlToValidate="txtpwd" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <%--<label class="control-label col-lg-4">Confirm Password* : </label>--%>
                                                <asp:Label ID="lblCnfmPwd" runat="server" class="control-label col-lg-4" Text="Confirm Password* : " Font-Bold="true"></asp:Label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtCnfrmPwd" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorcnfrm" runat="server" ControlToValidate="txtCnfrmPwd" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="comppwd" runat="server" ControlToValidate="txtCnfrmPwd" ControlToCompare="txtpwd" ErrorMessage="Password Mismatch!" ForeColor="Red" ValidationGroup="save"></asp:CompareValidator>
                                                </div>
                                            </div>
                                            <div class="form-group ">

                                                <div class="col-lg-12">
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <div class="col-lg-10">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-lg-offset-2 col-lg-12">
                                                    <asp:Button ID="BtnSave" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="btnsave_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Reset" OnClick="btnCancel_Click" />

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-5">
                                            <div class="form-group ">
                                                <br />
                                                <br />
                                                <label for="lblCountry" class="control-label col-lg-4">Country : </label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="ddlCountry" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                        ControlToValidate="txtCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="lblmob" class="control-label col-lg-4">Mobile No* : </label>
                                                <div class="col-lg-8">
                                                    <asp:Button ID="btnAdd12" runat="server" CssClass="form-control" Text="Add" OnClick="btnAdd_Click" Style="display: none" ValidationGroup="Add" />
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblCountryId" runat="server" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtmob" runat="server" CssClass="form-control" placeholder="Mobile No" MaxLength="16"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtmob" runat="server" ControlToValidate="txtmob" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtmob" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                                        <br />
                                                    </div>
                                                    <a class="branch brancht branchm" style="width: 66%; font-size: 15px; margin-left: 20%; cursor: pointer;" id="btnAdd"><i class="fa fa-plus-circle"></i>Add More Device</a><br />
                                                    <br />
                                                    <asp:GridView ID="gdvMobile" runat="server" AutoGenerateColumns="false" ShowHeader="false" GridLines="None" Width="100%" OnRowDeleting="gdvMobile_RowDeleting">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtgdvcntry" runat="server" Enabled="false" Text='<%#Eval("CountryCode")%>' CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtgdv" runat="server" Enabled="false" Text='<%#Eval("MobileNo")%>' CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Delete" runat="server" CommandName="Delete"><i class="fa fa-remove"></i></asp:LinkButton>
                                                                    <br />
                                                                    <br />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <%-- <div id="TextBoxContainer"></div>--%>
                                                    <br />
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>




                        </div>

                    </asp:View>
                    <asp:View ID="Tab2" runat="server">
                        <div id="Div1" class="flipkey">

                            <a id="summa1"></a>
                            <asp:Panel ID="Panel2" CssClass="form-group" runat="server">
                                <div class="panel-body table-rep-plugin">
                                    <div class="row" style="text-align: center">

                                        <div class="row">
                                            <div class=" form">
                                                <div class="col-lg-7">
                                                    <div class="form-group ">
                                                        <label for="rptmngr" class="control-label col-lg-4">Reporting Manager* : </label>
                                                        <div class="col-lg-8">
                                                            <asp:DropDownList ID="dddlExcelRptMngr" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                                            </asp:DropDownList>
                                                            <br />

                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="form-group ">
                                                        <label for="lblEId" class="control-label col-lg-4">Device Ownership* : </label>
                                                        <div class="col-lg-8">
                                                            <asp:DropDownList ID="dddlExcelOwner" runat="server" class="form-control" AppendDataBoundItems="true">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="dddlExcelOwner" ValueToCompare="0" Operator="NotEqual" ValidationGroup="upload" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                                        </div>
                                                    </div>


                                                    <div class="form-group ">
                                                        <label for="lblEId" class="control-label col-lg-4">Choose File* : </label>
                                                        <div class="col-lg-6">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" />
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:LinkButton ID="btndwnld" runat="server" OnClick="btndwnld_Click1"> <img src="images/SampleFormat.png" class="appman" />&nbsp;<u>Sample.xlsx</u></asp:LinkButton>
                                                            <%--<asp:ImageButton ID="btndwnld" runat="server" ImageUrl="~/images/SampleFormat.png" Width="40px" Height="50px" AlternateText="Sample.xlsx"  OnClick="btndwnld_Click1"  />--%>
                                                        </div>

                                                    </div>
                                                    <div class="form-group ">

                                                        <div class="col-lg-10">
                                                            <br />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">

                                                        <div class="col-lg-12">
                                                            <asp:Button runat="server" ID="btnupload" Text="Upload" CssClass="btn btnd btncompt waves-effect waves-light" Width="110px" OnClick="btnUpload_Click" ValidationGroup="upload" />

                                                        </div>
                                                        <%-- <div class="col-lg-3" style="text-align: center">
                                                    Select Sheet :
                                                    <asp:DropDownList ID="ddlSheet" runat="server" AutoPostBack="true" CssClass="form-control" Width="274px"></asp:DropDownList>
                                                </div>
                                                 <div class="col-lg-9">
                                                     <br />
                                                      <asp:Button ID="btnSaveExcel" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save Excel" OnClick="btnSaveExcel_Click" Enabled="false" />
                                          
                                                     </div>--%>
                                                    </div>
                                                </div>
                                                <div class="col-lg-5">
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

                            </asp:Panel>
                        </div>
                    </asp:View>
                </asp:MultiView>

            </div>


        </div>
        <!-- train section -->
    </div>


    <%--</form>--%>


    <asp:Panel ID="pswd_info" runat="server">
        <h6><u><b>PASSWORD POLICY</b></u><br />
            1. Minimum 8 Characters.<br />
            2. At Least One Numeric.<br />
            3. One Upper Case Character.<br />
            4. One Lower Case Character.<br />
            5. One Special Character.<br />
        </h6>
    </asp:Panel>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $("#flip1").bind("click", function () {
                document.getElementById('<%= btnForm.ClientID %>').click();
            });
            $("#flip2").bind("click", function () {
                document.getElementById('<%= btnExcel.ClientID %>').click();
            });
            $("#btnAdd").bind("click", function () {
                document.getElementById('<%= btnAdd12.ClientID %>').click();
            });
        }
        function HideLabel() {
            var seconds = 9;
            setTimeout(function () {
                document.getElementById("<%=lblpopmsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>

