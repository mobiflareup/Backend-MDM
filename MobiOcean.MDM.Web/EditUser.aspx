<%@ Page Title="Edit User" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="EditUser.aspx.cs" Inherits="MobiOcean.MDM.Web.EditUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">
                    <div class="bhoechie-tab-content active div">

                        <div class="profile1">&nbsp;&nbsp;Edit User</div>

                        <br />

                        <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <div class="dataTables_length" id="datatable-editable_length">
                                    <label>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="text-align: left">

                            <div class="panel-body table-rep-plugin">
                                <div class=" form">
                                    <div class="col-lg-6">
                                        <div class="form-group ">
                                            <label for="firstname" class="control-label col-lg-4">Name* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="UTextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                    ControlToValidate="UTextBox2" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="bname" class="control-label col-lg-4">Employee ID* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="UTextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                    ControlToValidate="UTextBox1" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="username" class="control-label col-lg-4">Email ID* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="UTextBox4" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                    ControlToValidate="UTextBox4" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="xyz@xyz.xyz" ForeColor="Red" ValidationGroup="save"
                                                    ControlToValidate="UTextBox4" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <label for="confirm_password" class="control-label col-lg-4">Branch : </label>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlBranch" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">

                                            <div class="col-lg-12">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="email" class="control-label col-lg-4">Department : </label>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlDept" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">

                                            <div class="col-lg-12">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="lbldst" class="control-label col-lg-4">Designation : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txtdst" runat="server" CssClass="form-control" placeholder="Designation"></asp:TextBox>
                                                <br />
                                            </div>
                                        </div>

                                        <div class="form-group ">
                                            <label for="phone" class="control-label col-lg-4">Role* : </label>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="DDLrole" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="DDLrole" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">

                                            <div class="col-lg-12">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="confirm_password" class="control-label col-lg-4">Reporting Manager* : </label>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="ddlRportngMngr" runat="server"
                                                    AppendDataBoundItems="True" CssClass="form-control">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="form-group ">

                                            <div class="col-lg-12">
                                                <br />
                                            </div>
                                        </div>

                                        <div class="form-group ">
                                            <label for="lblEId" class="control-label col-lg-4">Device Ownership* : </label>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="drpOwner" runat="server" class="form-control" AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpOwner" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">

                                            <div class="col-lg-12">
                                                <br />
                                            </div>
                                        </div>

                                        <div class="form-group ">

                                            <div class="col-lg-12">
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
                                                <asp:Button ID="btnSave" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Update" ValidationGroup="save"
                                                    OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" CommandName="Cancel" OnClick="btnCancel_Click" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6" style="text-align: center">
                                        <asp:Label ID="lblMobin" runat="server" Text=""></asp:Label>
                                        <div class="form-group ">
                                            <label for="lblCountry" class="control-label col-lg-4">Country : </label>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="ddlCountry" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="lastname" class="control-label col-lg-4">Mobile No* : </label>
                                            <div class="col-lg-8">
                                                <asp:Button ID="btnAdd12" runat="server" CssClass="form-control" Text="Add" OnClick="btnAdd12_Click" Style="display: none" ValidationGroup="Add" />
                                                <div class="col-md-3">
                                                    <asp:Label ID="lblCountryId" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-9">

                                                    <asp:TextBox ID="txtmob" runat="server" CssClass="form-control" placeholder="Mobile No" MaxLength="16"></asp:TextBox>
                                                    <asp:Label ID="lbladdMob" runat="server"></asp:Label>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtmob" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                                    </br>
                                                </div>
                                                <a class="branch brancht branchm" id="btnAdd" style="width: 68%; font-size: 15px; margin-left: 16%; cursor: pointer;"><i class="fa fa-plus-circle"></i>Add More Device</a><br />
                                                <br />
                                                <asp:GridView ID="grdNo" runat="server" AutoGenerateColumns="false" ShowHeader="false" GridLines="None" Width="100%" OnRowDataBound="grdNo_DataBound" OnRowDeleting="grdNo_RowDeleting" OnRowEditing="grdNo_RowEditing" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" OnRowCancelingEdit="grdNo_RowCancelingEdit" OnRowUpdating="grdNo_RowUpdating">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%#Eval("DeviceId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <br />
                                                                <div class="col-md-4">

                                                                    <asp:Label ID="lblCountryIdn" runat="server" Visible="false" Text='<%#Eval("CountryId")%>'></asp:Label>
                                                                    <asp:TextBox ID="txtgdvcntryn" runat="server" Enabled="false" Text='<%#Eval("PhoneCode")%>' CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtgdvn" runat="server" Text='<%#Eval("MobileNo1")%>' Enabled="false" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                                </div>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <div class="col-lg-12" style="border: 1px solid green">
                                                                    <br />
                                                                    <label for="lblCountryas" class="control-label col-lg-4">Country: </label>
                                                                    <div class="col-lg-8">
                                                                        <asp:DropDownList ID="ddlCountryedit" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCountryedit_SelectedIndexChanged"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                                            ControlToValidate="ddlCountryedit" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                            ControlToValidate="txtCodeedit" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="lblCountryIdedit" runat="server" Visible="false" Text='<%#Eval("CountryId")%>'></asp:Label>
                                                                        <asp:TextBox ID="txtCodeedit" runat="server" Enabled="false" Text='<%#Eval("PhoneCode")%>' CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtgdvedit" runat="server" Text='<%#Eval("MobileNo1")%>' CssClass="form-control" MaxLength="16"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtgdvedit" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                            ControlToValidate="txtgdvedit" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </EditItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" ToolTip="Edit"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" ToolTip="Delete"><i class="fa fa-trash-o custom-table-fa"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                &nbsp;
                                                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" ToolTip="Update" ValidationGroup="Update">
                                                     <i  class="fa fa-save"></i></asp:LinkButton>
                                                                &nbsp;
                                                 <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel" Text="Cancel" ToolTip="Cancel">
                                              <i  class="fa fa-close"></i></asp:LinkButton>
                                                            </EditItemTemplate>
                                                            <ItemStyle Width="16%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <!-- train section -->
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button ID="dummy" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="MP1" runat="server" PopupControlID="MessagePnl"
                TargetControlID="dummy" CancelControlID="btnccl"
                BackgroundCssClass="modalbackground">
            </asp:ModalPopupExtender>

            <asp:Panel runat="server" ID="MessagePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">

                <div class="modal-body" style="text-align: center; color: green;">
                    <asp:Button ID="btnccl" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
                    <asp:Label ID="message" runat="server" Text="Updated Successfully"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="ok" runat="server" Text="OK" OnClick="ok_Click" />
                </div>


            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(function () {
                $("#btnAdd").bind("click", function () {
                    document.getElementById('<%= btnAdd12.ClientID %>').click();
                });

            });
        }
        function HideLabel() {
            var seconds = 7;
            setTimeout(function () {
                document.getElementById("<%=lblMobin.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

    </script>

</asp:Content>
