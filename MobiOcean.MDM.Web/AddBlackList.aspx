<%@ Page Title="Restricted Websites" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AddBlackList.aspx.cs" Inherits="MobiOcean.MDM.Web.AddBlackList" %>
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
                            Restricted Websites
                            <a href="#" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Add Website</span></a>
                            <div class="clearfix"></div>
                        </div>
                        <br />


                        <div id="panel" class="flipkey" style="display: none">
                            <div class=" form">
                                <div class="col-lg-7 col-md-12">
                                    <div class="form-group ">
                                        <label for="bname" class="control-label col-lg-4">Category Name* : </label>
                                        <div class="col-lg-8">
                                            <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                            <asp:CompareValidator ID="comp" runat="server" ControlToValidate="ddlGroupName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
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
                            <div class="col-sm-12" style="text-align: ">
                                <div class="dataTables_length" id="datatable-editable_length">
                                    <label>
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>
                       
                        <div class="row" style="text-align:center ">
                            <div class=" form">
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            Category Name :
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
                                            <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("CategoryName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="ElblGroupName" runat="server" Text='<%#Eval("CategoryId") %>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="EddlGroupName" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="EddlGroupName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="Update" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
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
                                                ToolTip="Update" ValidationGroup="Update"><i  class="fa fa-save "></i></asp:LinkButton>
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
            //});
        }
    </script>
</asp:Content>
