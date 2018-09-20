<%@ Page Title="SOS Contact" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="SosContact.aspx.cs" Inherits="MobiOcean.MDM.Web.SosContact" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3" style="display: inline">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">


                <div class="profile1">&nbsp;SOS Contact</div>
                <br />


                <div class="col-lg-12" style="text-align: center">
                    <asp:Label ID="lblpopmsg" runat="server"></asp:Label>
                </div>
                <br />
                <br />



                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                        <asp:GridView ID="grdsoscontact" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                            OnRowDeleting="grdsoscontact_RowDeleting"
                            OnRowCancelingEdit="grdsoscontact_RowCancelingEdit" OnRowEditing="grdsoscontact_RowEditing"
                            OnRowUpdating="grdsoscontact_RowUpdating" Width="70%" GridLines="None" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactId" runat="server" Text='<%#Eval("ContactId")%>'></asp:Label>
                                        <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SosContactNo">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgdv" Enabled="false" runat="server" Text='<%#Eval("ContactNo")%>' CssClass="form-control"></asp:TextBox>
                                        <br />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgdv" runat="server" Text='<%#Eval("ContactNo")%>' CssClass="form-control" MaxLength="15"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2txtgdv" runat="server" FilterType="Numbers"
                                            TargetControlID="txtgdv" />
                                        <asp:RequiredFieldValidator ID="rfvtxtrngolecode" runat="server" ControlToValidate="txtgdv" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtgdvftr" runat="server" CssClass="form-control" PlaceHolder="Contact No" MaxLength="15"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2txtgdvftr" runat="server" FilterType="Numbers" TargetControlID="txtgdvftr" />
                                        <asp:RequiredFieldValidator ID="rfhgjv" runat="server" ControlToValidate="txtgdvftr" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <br />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        &nbsp;&nbsp;
                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" ToolTip="Edit"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>
                                        &nbsp;
                                <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" ToolTip="Delete"><i class="fa fa-trash-o custom-table-fa"></i></asp:LinkButton>
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        &nbsp;&nbsp;
                                <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" ToolTip="Update" ValidationGroup="Update">
                                                <i  class="fa fa-save"></i></asp:LinkButton>
                                        &nbsp;
                                <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel" Text="Cancel" ToolTip="Canecl">
                                <i  class="fa fa-close"></i></asp:LinkButton>
                                        <br />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        &nbsp;&nbsp;
                               <asp:LinkButton ID="AddButton" runat="server" OnClick="AddButton_Click" ToolTip="Add" ValidationGroup="Add">
                                                <i><img src="image/plus.png" style="width: 19px; margin-bottom: 17px;"></i></asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <td>
                                    <asp:TextBox ID="txtgdvftre" runat="server" CssClass="form-control" PlaceHolder="Contact No"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2txtgdvftre" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom,Numbers" ValidChars="()_&@#"
                                        TargetControlID="txtgdvftre" />
                                    <asp:RequiredFieldValidator ID="rfhgtxtgdvftrejv" runat="server" ControlToValidate="txtgdvftre" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Add1"></asp:RequiredFieldValidator>
                                    <br />
                                </td>
                                <td>&nbsp;&nbsp;
                                <asp:LinkButton ID="AddButton1" runat="server" OnClick="AddButton1_Click" ToolTip="Add" ValidationGroup="Add1">
                                                <i  class="fa fa-save"></i></asp:LinkButton>
                                </td>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>

            </div>


        </div>
    </div>
    <script>
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblpopmsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>

