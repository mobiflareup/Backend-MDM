<%@ Page Title="Alert Management" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="AlertManagement.aspx.cs" Inherits="MobiOcean.MDM.Web.AlertManagement" %>

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
                    <div class="bhoechie-tab-content active div">


                        <div class="profile1" style="margin: 0px;">
                            Alert Management
                            <a href="#" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Add Alert</span></a>




                            <div class="clearfix"></div>
                        </div>

                        <div id="panel" class="flipkey">
                            <div class=" form">
                                <div class="col-lg-7 col-md-12">
                                    <div class="form-group ">

                                        <div class="col-lg-12">
                                            <br />
                                        </div>
                                    </div>

                                    <div class="form-group ">
                                        <label for="company" class="control-label col-lg-4">Mobile Number* : </label>
                                        <div class="col-lg-8">
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtAddMobileNo" runat="server" CssClass="form-control" MaxLength="15" placeholder="Mobile Number"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ControlToValidate="txtAddMobileNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                <asp:FilteredTextBoxExtender runat="server" ID="ftal" TargetControlID="txtAddMobileNo" FilterType="Numbers" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label col-lg-4"></label>
                                            <div class="col-lg-8">
                                                <asp:Label ID="lbladdmsg" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-lg-12 text-center">
                                                </br/>
                                                <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-5">
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <div class="dataTables_length" id="datatable-editable_length">
                                    <label>
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="text-align: center">
                            <div class=" form">
                                <div class="form-group ">

                                    <div class="col-lg-6">
                                        <label>
                                            Mobile Number :
                                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="ftserch" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers" />
                                        </label>
                                    </div>
                                </div>


                                <div class="form-group ">

                                    <div class="col-lg-6">
                                        <label>
                                            <br />
                                            <asp:Button ID="btnSrch" runat="server" Text="Search" class="btn btnd btncompt" OnClick="btnSrch_Click" />
                                        </label>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <br />
                                </div>
                            </div>
                        </div>

                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="grdAlert" runat="server" class="table table-small-font table-bordered table-striped mGrid" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdAlert_PageIndexChanging" OnRowCancelingEdit="grdAlert_RowCancelingEdit" OnRowDeleting="grdAlert_RowDeleting" OnRowEditing="grdAlert_RowEditing" OnRowUpdating="grdAlert_RowUpdating" OnRowDataBound="grdAlert_RowDataBound" CssClass="table mGrid" HeaderStyle-CssClass="protable">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("AlertId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("Country") +" "+Eval("MobileNo")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlCountryEdit" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="ddlCountryEdit" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6">
                                                 <asp:TextBox ID="txtEMobileNo" runat="server" Text='<%#Eval("MobileNo")%>' CssClass="form-control"  MaxLength="15"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                ControlToValidate="txtEMobileNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                                <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtEMobileNo" FilterType="Numbers" />
                                            </div>
                                           
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alert For" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("AlertFor").ToString()=="1"?"Incoming Calls":Eval("AlertFor").ToString()=="2"?"OutGoing Calls":Eval("AlertFor").ToString()=="3"?"Incoming Sms":""%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblEAlertFor" runat="server" Text='<%#Eval("AlertFor")%>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlEAlertFor" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="InComing Call" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="OutGoing Call" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Incoming SMS" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit"
                                                ToolTip="Edit"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>

                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="update"
                                                ToolTip="Update" ValidationGroup="Update"><i  class="fa fa-save "></i></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel"
                                                Text="Cancel" ToolTip="Canecl"><i  class="fa fa-close "></i></asp:LinkButton>
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
                    <asp:Label ID="lblalertid" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold; background-color: #e5e5e5; color: #000000">
                    <asp:Label ID="lblUser" runat="server" Text="Are you sure to delete?" />
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
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
            //$(document).ready(function () {
            $("#flip").click(function () {
                $("#panel").slideToggle("slow");
            });
            $("#btnCancel").click(function () {
                $("#panel").slideToggle("slow");
            });
            //});
        }
    </script>
</asp:Content>
