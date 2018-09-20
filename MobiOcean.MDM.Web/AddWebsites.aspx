<%@ Page Title="Web Management for Organization" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="AddWebsites.aspx.cs" Inherits="MobiOcean.MDM.Web.AddWebsites" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">

    <style type="text/css">
        .modalBackgroundTemp {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .addweb-alert {
            color: white;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
        }

        .mailto {
            color: white;
        }

            .mailto:hover {
                color: darkgrey;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">

                <div class="force-overflow">
                    <div class="bhoechie-tab-content active div">

                        <div class="profile1" style="margin: 0px;">
                            Web Management for Organization
                             <a href="#" id="flip1" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Settings</span></a>
                            <a href="#" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Add Website</span></a>



                            <div class="clearfix"></div>
                        </div>

                        <div class="profile1" style="margin: 0px; text-align: center">
                            <sapn class="addweb-alert"> This feature will work on your company server. Please contact us on <a class="mailto" href="mailto:<%=MobiOcean.MDM.BAL.Model.Constant.supportEmail %>" target="_blank"> <%=MobiOcean.MDM.BAL.Model.Constant.supportEmail %> </a>or on +91-9958421037.</sapn>
                            <br />
                            <br />
                        </div>

                        <br />
                        <br />
                        <div id="panel1" class="flipkey" style="display: none">
                            <div class=" form">
                                <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                    <div class="form-group ">
                                        <label class="control-label col-md-4">Choose Is Enabled  :</label>

                                        <asp:RadioButton ID="RDBEnabled" runat="server" GroupName="b" Checked="true" Text="Yes" CssClass="col-md-2" />

                                        <asp:RadioButton ID="RDBNotEnabled" runat="server" GroupName="b" Text="No" CssClass="col-md-2" />

                                    </div>
                                </div>
                                <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">

                                    <div class="form-group ">
                                        <label class="control-label col-md-4">IP Address  :</label>

                                        <asp:TextBox ID="txtIpAddress" runat="server" class="col-md-4"></asp:TextBox>

                                        <asp:RegularExpressionValidator ID="regexpName" runat="server"
                                            ErrorMessage="Not Valid Ip Address ."
                                            ControlToValidate="txtIpAddress" ForeColor="Red" ValidationGroup="save1"
                                            ValidationExpression="^(?=\d+\.\d+\.\d+\.\d+$)(?:(?:25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]|[0-9])\.?){4}$" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtIpAddress" ErrorMessage=" Required! " ForeColor="Red" ValidationGroup="save1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">


                                    <div class="form-group ">
                                        <label class="control-label col-md-4">Choose Is Whitelist  :</label>

                                        <asp:RadioButton ID="RDBWhiteList" runat="server" GroupName="a" Checked="true" Text="Whitelist" CssClass="col-md-3" />

                                        <asp:RadioButton ID="RDBBlackList" runat="server" GroupName="a" Text="Blacklist" CssClass="col-md-3" />

                                    </div>
                                </div>
                                <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">

                                    <div class="form-group ">
                                        <div class="col-lg-offset-4 col-lg-6">
                                            <asp:Button ID="Setting" runat="server" CssClass="btn btnd btncompt" Text="Save" OnClick="Setting_Click" ValidationGroup="save1" />
                                            &nbsp;&nbsp;<asp:Button ID="btnsetting" runat="server" CssClass="btn btnd btncompt" Text="Cancel" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="clearfix"></div>

                        <div id="panel" class="flipkey" style="display: none">
                            <div class=" form">
                                <div class="col-lg-7 col-md-12">
                                    <div class="form-group ">
                                        <label for="bname" class="control-label col-lg-4">Is Whitelist </label>
                                        <div class="col-lg-8">
                                            <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                            <asp:CompareValidator ID="comp" runat="server" ControlToValidate="ddlGroupName" ValueToCompare="-1" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                        </div>
                                    </div>

                                    <div class="form-group ">
                                        <label for="firstname" class="control-label col-lg-4">Website* : </label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtApplicationName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                ControlToValidate="txtApplicationName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
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
                                            <asp:Button ID="btnAssign" runat="server" CssClass="btn btnd btncompt" Text="Save" ValidationGroup="save" OnClick="btnAssign_Click" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt" Text="Cancel" />

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
                                            Is Whitelist :
                                            <asp:DropDownList ID="ddlSrchGroup" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            Website : 
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
                            <asp:GridView ID="grdBlackList" runat="server" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." OnPageIndexChanging="grdBlackList_PageIndexChanging" Width="100%" OnRowDeleting="grdBlackList_RowDeleting"
                                PageSize="20" AllowPaging="true" OnRowEditing="grdBlackList_RowEditing" OnRowUpdating="grdBlackList_RowUpdating" OnRowCancelingEdit="grdBlackList_RowCancelingEdit" OnRowDataBound="grdBlackList_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="URL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUrlId" runat="server" Text='<%#Eval("UrlId")%>' Visible="false"></asp:Label>
                                            <a id="anchor" runat="server" target="_blank" href='<%#Eval("Url").ToString().IndexOf("http://")>0?Eval("Url").ToString():"http://"+Eval("Url").ToString()%>'><%#Eval("Url")%></a>
                                            <asp:Label ID="lblUrl" runat="server" Text='<%#Eval("Url")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsWhiteList" runat="server" Text='<%#((Eval("IsWhiteList").ToString())=="1")?"White List":"Black List"%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="ElblGroupName" runat="server" Text='<%#((Eval("IsWhiteList").ToString())=="1")?"White List":"Black List"%>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="EddlGroupName" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                            <asp:CompareValidator ID="comp" runat="server" ControlToValidate="EddlGroupName" ValueToCompare="-1" Operator="NotEqual" ValidationGroup="Update" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                        </EditItemTemplate>
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
                    <asp:Label ID="lblalerturlid" runat="server" Visible="false"></asp:Label>
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
            $("#flip1").click(function () {
                $("#panel1").slideToggle("slow");
            });
            $("#btnsetting").click(function () {
                $("#panel1").slideToggle("slow");
            });
            //});
        }
    </script>
</asp:Content>

