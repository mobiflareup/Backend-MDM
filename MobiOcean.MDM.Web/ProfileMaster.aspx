<%@ Page Title="Profile" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ProfileMaster.aspx.cs" Inherits="MobiOcean.MDM.Web.ProfileMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="coh" runat="server" ContentPlaceHolderID="ContentHead">

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">

                    <div class="bhoechie-tab-content active">

                        <div class="profile1">
                            &nbsp;&nbsp;Profile / Policy
                                 <a href="#" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Add New Profile</span></a>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <div class="dataTables_length" id="datatable-editable_length">
                                    <label>
                                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div id="panel" class="flipkey">
                                <div class=" form">
                                    <div class="col-lg-7 col-md-12">
                                        <div class="form-group ">

                                            <div class="col-lg-12">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="lastname" class="control-label col-lg-4">Profile Code* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txtAddProfileCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="txtAddProfileCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="firstname" class="control-label col-lg-4">Profile Name* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txtAddProfileName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                    ControlToValidate="txtAddProfileName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="firstname" class="control-label col-lg-4">Profile Purpose* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txtAddProfilePurpose" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtAddProfilePurpose" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-lg-4"></label>
                                            <div class="col-lg-8">
                                                <asp:Label ID="Label1" runat="server"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-lg-offset-4 col-lg-6">
                                                <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="btnSave_Click" />
                                                <input id="btnCancel" type="button" value="Cancel" class="btn btnd btncompt waves-effect" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-5">
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="row" style="text-align: center">

                            <div class=" form">
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            Profile Code :
                                                               <asp:TextBox ID="txtPcode" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            Profile Name :
                                                                    <asp:TextBox ID="txtPname" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            <br />
                                            <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click" />
                                        </label>
                                    </div>
                                </div>


                            </div>
                        </div>

                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="grdclient" runat="server" DataKeyNames="ProfileId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No record found." OnPageIndexChanging="grdclient_PageIndexChanging" Width="100%"
                                OnRowCancelingEdit="grdclient_RowCancelingEdit" OnRowDeleting="grdclient_RowDeleting"
                                OnRowEditing="grdclient_RowEditing" OnRowUpdating="grdclient_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("ProfileId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Profile Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProfileCode" runat="server" Text='<%#Eval("ProfileCode")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtProfileCode" runat="server" Text='<%#Eval("ProfileCode")%>' CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtProfileCode" ErrorMessage="*" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Profile Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProfileName" runat="server" Text='<%#Eval("ProfileName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtProfileName" runat="server" Text='<%#Eval("ProfileName")%>' CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtProfileName" runat="server" ControlToValidate="txtProfileName" ErrorMessage="*" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Profile Purpose">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProfilePurpose" runat="server" Text='<%#Eval("ProfilePurpose")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtProfilePurpose" runat="server" Text='<%#Eval("ProfilePurpose")%>' CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtProfileNamedummy" runat="server" ControlToValidate="txtProfilePurpose" ErrorMessage="*" ForeColor="Red" ValidationGroup="Update1"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manage Feature">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" runat="server" OnClick="lbEdit_Click"><i class="fa fa-clock-o custom-table-fa"></i></asp:LinkButton>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Push Profile">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbPushprofile" runat="server" OnClick="lbPushprofile_Click"><i class="fa fa-upload custom-table-fa"></i></asp:LinkButton>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employees">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPopUp" runat="server" OnClick="lnkPopUp_Click"><i class="fa fa-users custom-table-fa"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnReport" runat="server" OnClick="lnkbtnReport_Click" CommandName="Report"><i class="fa fa-eye custom-table-fa"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit"
                                                ToolTip="Edit"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>

                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update"
                                                ToolTip="Update" ValidationGroup="Update"><i  class="fa fa-save"></i></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel"
                                                Text="Cancel" ToolTip="Canecl"><i  class="fa fa-close"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="btn-link"
                                                ToolTip="Delete"><i class="fa fa-trash-o custom-table-fa"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg model-md model-xs">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close btn btnd btncompt" data-dismiss="modal" aria-hidden="true" style="background-color: #2A368B; width: 35px; vertical-align: middle; padding-top: 7px;">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Assigned Employee</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="Gdv" runat="server" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                            AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPopId" runat="server" Text='<%#Eval("ProfileUserId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPopUserId" runat="server" Text='<%#Eval("UserId")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblPopUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Profile Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPopProfileId" runat="server" Text='<%#Eval("ProfileId")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblPopProfileName" runat="server" Text='<%#Eval("ProfileName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Current Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPopIsEnable" runat="server" Text='<%#Eval("IsEnable").ToString()=="1"?"Enabled":"Disabled"%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <button type="button" class="btn btnd btncompt" data-dismiss="modal" aria-hidden="true">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <center>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divProcessing">
                            <asp:Image runat="server" ID="progressImg2" ImageUrl="~/images/Processing.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:Button ID="dummypopupbtn" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpdelete" runat="server" PopupControlID="pnlpopup"
        TargetControlID="dummypopupbtn" CancelControlID="InvisibleNo"
        BackgroundCssClass="modalBackgroundTemp">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="150px" Width="400px">
        <table width="100%" style="border: Solid 2px; width: 100%; height: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold; background-color: #2a368b; color: #FFFFFF; height: 10px">
                    <asp:Label ID="lblalert" runat="server" Text="Alert" />
                    <asp:Label ID="lblalertprofileid" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold; background-color: #e5e5e5; color: #000000">
                    <asp:Label ID="lblUser" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center">
                    Are you sure that you want to permanently delete the profile
                </td>
            </tr>
            <tr>
                <td></td>
                <td align="right" style="padding-right: 15px; color: #000000; background-color: #e5e5e5;">
                    <asp:Button ID="Yes" runat="server" CssClass="btn btn-sm btnd btncompt" Text="OK" OnClick="Yes_Click" />
                    <asp:Button ID="No" runat="server" CssClass="btn btn-sm btn-warning" Text="Cancel" OnClick="No_Click" />
                    <asp:Button ID="InvisibleNo" runat="server" CssClass="btn btn-sm btn-warning" Text="Cancel" Style="display: none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <script>
        function pageLoad(sender, args) {
            $("#flip").click(function () {
                $("#panel").slideToggle("slow");
            });
            $("#btnCancel").click(function () {
                $("#panel").slideToggle("slow");
            });
        }
        function closepopup() {
            $find('MP1').hide();
        }
    </script>
</asp:Content>




