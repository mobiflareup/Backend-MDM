<%@ Page Title="Branch" MasterPageFile="~/MasterPage.master" Language="C#" AutoEventWireup="true" 
    CodeBehind="Branch.aspx.cs" Inherits="MobiOcean.MDM.Web.Branch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content2" runat="server" ContentPlaceHolderID="ContentHead">
    <script>
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblpopmsg.ClientID %>").style.display = "none";
             }, seconds * 1000);
         };
    </script>
     <style type="text/css">
        .modalBackgroundTemp {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
  
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <!-- flight section branch brancht branchm-->
                <div class="bhoechie-tab-content active">

                    <div class="profile1">&nbsp;&nbsp;Branch</div>

                    <br />

                    <div class="col-lg-12" style="text-align: center">
                        <asp:Label ID="lblpopmsg" runat="server"></asp:Label>
                    </div>
                    <br />
                    <br />
                  
                    <div class="row">
                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                          
                            <asp:GridView ID="grdBranch" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                OnRowDeleting="grdBranch_RowDeleting"
                                OnRowCancelingEdit="grdBranch_RowCancelingEdit" OnRowEditing="grdBranch_RowEditing"
                                OnRowUpdating="grdBranch_RowUpdating" Width="70%" GridLines="None" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("BranchId")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="departmentname">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgdva" Enabled="false" runat="server" Text='<%#Eval("BranchName")%>' CssClass="form-control"></asp:TextBox>
                                            <br />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgdv" runat="server" Text='<%#Eval("BranchName")%>' CssClass="form-control"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredtxtgdvTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom,Numbers" ValidChars="()_&@#"
                                                TargetControlID="txtgdv" />
                                            <asp:RequiredFieldValidator ID="rfvtxtrngolecode" runat="server" ControlToValidate="txtgdv" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                            <br />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtgdvftr" runat="server" CssClass="form-control" PlaceHolder="Branch Name"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredtxtgdvftrTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom,Numbers" ValidChars="()_&@#"
                                                TargetControlID="txtgdvftr" />
                                            <asp:RequiredFieldValidator ID="rfhgjv" runat="server" ControlToValidate="txtgdvftr" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            <br />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            &nbsp;&nbsp;
                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" ToolTip="Edit"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>&nbsp;
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
                                            <br />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;&nbsp;
                               <asp:LinkButton ID="AddButton" runat="server" OnClick="AddButton_Click" ToolTip="Add" ValidationGroup="Add">
                                               <i class="fa fa-plus-square-o custom-table-fa" style="width: 19px; margin-bottom: 17px;"></i></asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemStyle VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    <td>
                                        <asp:TextBox ID="txtgdvftre" runat="server" CssClass="form-control" PlaceHolder="Branch Name"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredtxtgdvftreTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom,Numbers" ValidChars="()_&@#"
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

     <asp:Button ID="dummypopupbtn" runat="server" Style="display: none;" />
                        <asp:ModalPopupExtender ID="mpdelete" runat="server" PopupControlID="pnlpopup"
                            TargetControlID="dummypopupbtn" CancelControlID="InvisibleNo"
                            BackgroundCssClass="modalBackgroundTemp">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="150px" Width="400px">
                            <table width="100%" style="border: Solid 2px; width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>                                   
                                    <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold;background-color:#2a368b;color:#FFFFFF;height:10px">
                                        <asp:Label ID="lblalert" runat="server" Text="Alert" />
                                        <asp:Label ID="lblkeyid" runat="server" Visible="false"></asp:Label> 
                                        <asp:Label ID="lblbname" runat="server" Visible="false"></asp:Label>                                                                       
                                    </td>                                   
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold;background-color:#e5e5e5;color:#000000">
                                        <asp:Label ID="lblUser" runat="server" Text="Are you sure to delete?" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td align="right" style="padding-right: 15px;color:#000000;background-color:#e5e5e5;">
                                        <asp:Button ID="Yes" runat="server" CssClass="btn btn-sm btnd btncompt" Text="OK" OnClick="Yes_Click" />
                                        <asp:Button ID="No" runat="server" CssClass="btn btn-sm btn-warning" Text="Cancel" OnClick="No_Click"/>
                                        <asp:Button ID="InvisibleNo" runat="server" CssClass="btn btn-sm btn-warning" Text="Cancel" Style="display: none;"/>                                       
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

</asp:Content>

