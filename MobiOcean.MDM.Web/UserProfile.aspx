<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Profile" AutoEventWireup="true"
     CodeBehind="UserProfile.aspx.cs" Inherits="MobiOcean.MDM.Web.UserProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                    <div class="force-overflow">

                        <div class="bhoechie-tab-content active div">

                            <div class="profile1">&nbsp;&nbsp;User Profile</div>

                            <br />

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel-group panel-group-joined" id="accordion-test">



                                        <div class=" form">
                                            <div class="col-lg-7">
                                                <div class="form-group ">
                                                    <label for="firstname" class="control-label col-lg-4">User Name* : </label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="UTextBox2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                            ControlToValidate="UTextBox2" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="bname" class="control-label col-lg-4">Employee Id* : </label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="UTextBox1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                            ControlToValidate="UTextBox1" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="username" class="control-label col-lg-4">Email Id* : </label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="UTextBox4" runat="server" CssClass="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                            ControlToValidate="UTextBox4" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="xyz@xyz.xyz" ForeColor="Red" ValidationGroup="save"
                                                            ControlToValidate="UTextBox4" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="lastname" class="control-label col-lg-4">Mobile No* : </label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="UTextBox3" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="fmob" runat="server" TargetControlID="UTextBox3" FilterType="Numbers" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                            ControlToValidate="UTextBox3" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="save"
                                                            ControlToValidate="UTextBox3" ErrorMessage="Must be 10 digits" ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="Branch" class="control-label col-lg-4">Branch* : </label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group ">

                                                    <div class="col-lg-12">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="email" class="control-label col-lg-4">Department* : </label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtDept" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                                                     <br />
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="confirm_password" class="control-label col-lg-4">Designation* : </label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtGender" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                                                         <br />
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="phone" class="control-label col-lg-4">User Role* : </label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtRole" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                                                     <br />
                                                    </div>
                                                </div>

                                                <div class="form-group ">
                                                    <label for="confirm_password" class="control-label col-lg-4">Reporting Manager* : </label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtManager" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                                                          <br />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-lg-4"></label>
                                                    <div class="col-lg-8">
                                                        <asp:Label ID="lblpopmsg" runat="server"></asp:Label>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-lg-offset-4 col-lg-6">
                                                        <asp:Button ID="btnSave" runat="server" class="btn btnd btncompt waves-effect" Text="Edit"
                                                            OnClick="btnSave_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Visible="false" CssClass="btn btnd btncompt waves-effect" Text="Back" OnClick="btnCancel_Click" />

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-5">
                                                <div class="row">
                                                    <div class="col-md-12 portlets" style="text-align: center">
                                                        <asp:Image ID="profileImage" runat="server" Style="width: 100%;" />
                                                        <asp:Label ID="lblimagepath" runat="server" Visible="false"></asp:Label>
                                                        <div class="m-b-30">
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                             
                                        </div>


                                    </div>
                                </div>
                            </div>

                            <br />








                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- ============================================================== -->
        <!-- End Right content here -->
        <!-- ============================================================== -->
    <%--</form>--%>
</asp:Content>
