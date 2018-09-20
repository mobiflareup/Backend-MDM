<%@ Page Title="Add Client" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeBehind="AddClient.aspx.cs" Inherits="MobiOcean.MDM.Web.AddClient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
               
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tabscrollbar"  id="style-3">
                    <!-- flight section -->
                    <div class="bhoechie-tab-content active">

                        <div class="profile1">&nbsp;&nbsp;Add New Client</div>
                        <br />


                        <div class="form-group ">
                            <div class="col-lg-10">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                        </div>


                        <div class=" form">
                            <div class="col-lg-8">
                            <div class="form-group ">
                                <label for="company" class="control-label col-lg-4">Company Code *</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtClientcode" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtClientcode"
                                        WatermarkText=" Mandatory" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtClientcode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    <br />
                                </div>
                            </div>
                            <div class="form-group ">
                                <label for="firstname" class="control-label col-lg-4">Comapany Name *</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtClientName" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtClientName"
                                        WatermarkText=" Mandatory" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtClientName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group ">
                                <label for="company" class="control-label col-lg-4">User Name *</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtManagerName" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtManagerName"
                                        WatermarkText=" Mandatory" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtManagerName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group ">
                                <label for="company" class="control-label col-lg-4">Designation</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtdsgn" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtdsgn"
                                        WatermarkText=" Recommented" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                    <br />
                                </div>
                            </div>

                            <div class="form-group ">
                                <label for="username" class="control-label col-lg-4">E-mail Id *</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtEmailid" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtEmailid"
                                        WatermarkText=" Mandatory" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server"
                                        ControlToValidate="txtEmailid" ErrorMessage="Required!" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                        ControlToValidate="txtEmailid" Display="Dynamic" ErrorMessage="abc@abc.abc"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="save" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="form-group ">
                                <label for="phone" class="control-label col-lg-4">Mobile No *</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtManagerContactNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="fte1" runat="server" TargetControlID="txtManagerContactNo" FilterType="Numbers" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" runat="server"
                                        ControlToValidate="txtManagerContactNo" ErrorMessage="Required!" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender14" runat="server" TargetControlID="txtManagerContactNo"
                                        WatermarkText=" Recommented" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                </div>
                            </div>

                            <div class="form-group ">
                                <label for="lastname" class="control-label col-lg-4">Type Of Company</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtcmpytype" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtcmpytype"
                                        WatermarkText=" Recommented" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                    <br />
                                </div>
                            </div>
                            <div class="form-group ">
                                <label for="lastname" class="control-label col-lg-4">Number Of Employees</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtempNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="txtempNo"
                                        WatermarkText=" Recommented" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                    <br />
                                </div>
                            </div>
                            <div class="form-group ">
                                <label for="lastname" class="control-label col-lg-4">Address</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtAddress"
                                        WatermarkText=" Recommented" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                    <br />
                                </div>
                            </div>
                            <div class="form-group ">
                                <label for="lastname" class="control-label col-lg-4">Employee Id *</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" TargetControlID="txtUserId"
                                        WatermarkText=" Mandatory" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                        ControlToValidate="txtUserId" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group ">
                                <label for="lastname" class="control-label col-lg-4">Password *</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtpass" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender12" runat="server" TargetControlID="txtpass"
                                        WatermarkText=" Mandatory" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                     <asp:BalloonPopupExtender ID="BalloonPopupExtender3" TargetControlID="txtpass" UseShadow="true"
                                                         DisplayOnFocus="true" Position="TopRight" BalloonPopupControlID="Panel3" BalloonStyle="Rectangle"
                                                            runat="server" />                                                        <asp:regularexpressionvalidator display="Dynamic" id="RegularExpressionValidator1" runat="server" ValidationGroup="save"
                                                            errormessage="Password must be Minimum 8 characters long with at least one numeric, one upper case character, One lower case character and one special character." 
                                                            forecolor="Red" controltovalidate="txtpass" validationexpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}">

                                                        </asp:regularexpressionvalidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="txtpass" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>   
                                </div>
                            </div>
                            <div class="form-group ">
                                <label for="lastname" class="control-label col-lg-4">Conform Password *</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtConformpass" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="txtConformpass"
                                        WatermarkText=" Mandatory" WatermarkCssClass="form-control"></asp:TextBoxWatermarkExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                        ControlToValidate="txtConformpass" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ID="cmpNumbers" ControlToValidate="txtpass" ForeColor="Red" ControlToCompare="txtConformpass" Operator="Equal" Type="String" ErrorMessage="Password Not Match!" /><br />
                                </div>
                            </div>

                            <div class="form-group">
                            <label for="lastname" class="control-label col-lg-4">Logo</label>
                            <div class="col-lg-8">
                                <%-- <asp:Image ID="profileImage" runat="server" Style="width: 100%;" />--%>
                                <asp:Label ID="lblimagepath" runat="server" Visible="false"></asp:Label>
                                <div class="m-b-30">
                                    <form action="#" class="dropzone" id="dropzone">
                                        <div class="fallback">
                                            &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" class="btn btnd btncompt waves-effect" ClientIDMode="Static" /></br>
                                        </div>
                                </div>
                                </div>
                                </div>
                                <div class="form-group ">
                                    <div class="col-lg-10">
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </div>
                                </div>

                            <div class="form-group ">
                                      
                                <div class="col-lg-3" style="text-align:right">
                                      <asp:Button ID="btnAssign" runat="server" CssClass="btn btnd success waves-effect waves-light" Text="Save" OnClick="btnSave_Click" ValidationGroup="save" />
                                </div>
                                <div class="col-lg-3" style="text-align:center">
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="Panel3" runat="server">
           <h6><u><b>PASSWORD POLICY</b></u><br />
           1. Minimum 8 Characters.<br />
           2. At Least One Numeric.<br />
           3. One Upper Case Character.<br />
           4. One Lower Case Character.<br />
           5. One Special Character.<br /></h6>
         </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
