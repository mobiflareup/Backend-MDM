<%@ Page Title="App Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="AppMaster.aspx.cs" Inherits="MobiOcean.MDM.Web.AppMaster" %>

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
                            Application Master
          <a href="#" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Add Application</span></a>



                            <div class="clearfix"></div>
                        </div>

                        <br />
                        <br />


                        <div id="panel" class="flipkey">
                            <div class=" form">

                                <div class="col-lg-7 col-md-12">
                                    <div class="form-group ">
                                        <label for="bname" class="control-label col-lg-4">Group Name* : </label>
                                        <div class="col-lg-8">
                                            <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                            <asp:CompareValidator ID="comp" runat="server" ControlToValidate="ddlGroupName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                        </div>
                                    </div>

                                    <div class="form-group ">
                                        <label for="lastname" class="control-label col-lg-4">Application Code* : </label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtApplicationCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="txtApplicationCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegExp1" runat="server" ErrorMessage="*Characters And Numbers Only Allowed" ControlToValidate="txtApplicationCode"
                                                ValidationExpression="^[a-zA-Z0-9\s]{1,500}$" ForeColor="Red" ValidationGroup="save" />
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <label for="firstname" class="control-label col-lg-4">Application Name* : </label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtApplicationName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                ControlToValidate="txtApplicationName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="* use only allowed characters (a-z A-Z 0-9 @&#_)" ControlToValidate="txtApplicationName"
                                                ValidationExpression="^[a-zA-Z0-9@&_#\s]{1,50}$" ForeColor="Red" ValidationGroup="save" Font-Size="smaller" />
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
                                            <asp:Button ID="btnAssign" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="btnsave_Click" />
                                            <input type="button" class="btn btnd btncompt waves-effect" id="btnCancel" value="Cancel" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-5">
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

                                    <div class="col-lg-4">
                                        <label>
                                            Group Name :
                                                                 <asp:DropDownList ID="ddlSrchGroup" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            Application Name : 
                                                                    <asp:TextBox ID="txtSrchApplication" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <div class="col-lg-4">
                                        <label>
                                            <br />
                                            <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="grdAppMaster" runat="server" DataKeyNames="ApplicationId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No record found." OnPageIndexChanging="grdAppMaster_PageIndexChanging" Width="100%"
                                OnRowCancelingEdit="grdAppMaster_RowCancelingEdit" OnRowDeleting="grdAppMaster_RowDeleting"
                                OnRowEditing="grdAppMaster_RowEditing" OnRowUpdating="grdAppMaster_RowUpdating" OnRowDataBound="grdAppMaster_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("ApplicationId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Application Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppCode" runat="server" Text='<%#Eval("AppCode")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="App Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppName" runat="server" Text='<%#Eval("AppName")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupName" runat="server" Text='<%# string.IsNullOrEmpty(Eval("GroupName").ToString())?"---":Eval("GroupName").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="ElblGroupName" runat="server" Text='<%#Eval("GroupId") %>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="EddlGroupName" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="EddlGroupName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="Update" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                        </EditItemTemplate>

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
                        <!-- train section -->

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
                    <asp:Label ID="lblalertapplnid" runat="server" Visible="false"></asp:Label>
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
            $("#flip").click(function () {
                $("#panel").slideToggle("slow");
            });
            $("#btnCancel").click(function () {
                $("#panel").slideToggle("slow");
            });
        }

    </script>
</asp:Content>





