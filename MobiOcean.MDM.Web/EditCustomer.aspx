<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeBehind="EditCustomer.aspx.cs" Inherits="MobiOcean.MDM.Web.EditCustomer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active div">
                <div class="profile1">&nbsp;&nbsp;Edit Customer Details</div>
                <br />
                <div class="row" style="text-align: center">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <br />

                <div class="panel-body table-rep-plugin">
                    <div class=" form">
                        <div class="col-md-12">
                            <div class="form-group col-md-6">
                                <label for="Client" class="control-label col-md-5">Company Name *</label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="lblEId" class="control-label col-md-5">TIN Number </label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txttin" runat="server" CssClass="form-control" placeholder="TIN Number" MaxLength="11"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txttin" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                    <br />
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="lblEmail" class="control-label col-md-5">Contact Person *</label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtcontact" runat="server" CssClass="form-control" placeholder="Contact Person"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server"
                                        ControlToValidate="txtcontact" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="lblname" class="control-label col-md-5">Contact No *</label>
                                <div class="col-lg-7">
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control" placeholder="Mobile Number" MaxLength="16"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtmobile" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtmobile" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-md-6">
                                <label for="lblEmail" class="control-label col-md-5">Alternate Contact Person </label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtaltcontactpersion" runat="server" CssClass="form-control" placeholder="Contact Person"></asp:TextBox>
                                    <br />
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="lblEId" class="control-label col-md-5">Alternate Contact No </label>
                                <div class="col-md-7">
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlaltcontact" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtAltmobile" runat="server" CssClass="form-control" placeholder="Mobile No" MaxLength="15"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAltmobile" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                        <br />
                                    </div>
                                </div>
                            </div>


                            <div class="form-group col-md-6">
                                <label for="lbldst" class="control-label col-md-5">Email Id *</label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" placeholder="Email Id"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtemail" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ControlToValidate="txtemail" ForeColor="Red" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter valid Email-Id" ValidationGroup="save" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="lblRole" class="control-label col-md-5">Alternate Email Id </label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtAltEmail" runat="server" CssClass="form-control" placeholder="Email Id"></asp:TextBox>
                                    <asp:RegularExpressionValidator ControlToValidate="txtAltEmail" ForeColor="Red" ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter valid Email-Id" ValidationGroup="save" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="rptmngr" class="control-label col-md-5">Address line 1</label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address"></asp:TextBox>
                                    <br />
                                </div>
                            </div>
                            <br />
                            <div class="form-group col-md-6">
                                <label for="lblEId" class="control-label col-md-5">Address line 2 </label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtDist" runat="server" CssClass="form-control" placeholder="District"></asp:TextBox>
                                    <br />
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="lblEId" class="control-label col-md-5">City *</label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtcity" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtcity" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group col-md-6">
                                <label for="lblEId" class="control-label col-md-5">State *</label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control" placeholder="State"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtState" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="lblEId" class="control-label col-md-5">Country *</label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" placeholder="Country"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCountry" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group col-md-6">
                                <label for="lblEId" class="control-label col-md-5">Pin Code *</label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtPin" runat="server" CssClass="form-control" placeholder="Pin Code"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPin" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtPin" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="lblEId" class="control-label col-md-5">Lattitude *</label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtLat" runat="server" CssClass="form-control" placeholder="Lattitude"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLat" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="lblEId" class="control-label col-md-5">Longitude *</label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtLong" runat="server" CssClass="form-control" placeholder="Longitude"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtLong" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-md-12">
                                    <center>
                                                    <asp:Button ID="BtnSave" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="BtnSave_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnCancel_Click" />
                                                        </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

