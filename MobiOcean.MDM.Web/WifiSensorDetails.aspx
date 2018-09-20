<%@ Page Title="Sensor Details" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="WifiSensorDetails.aspx.cs" Inherits="MobiOcean.MDM.Web.WifiSensorDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    <style type="text/css">
        .modalBackgroundTemp {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active">
                <div class="profile1">&nbsp;&nbsp;Sensor Details</div>
                <br />
                <div class="row">
                    <div class="col-sm-12" style="text-align: center">
                        <div class="dataTables_length" id="datatable-editable_length">
                            <label>
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="form">
                    <div class="form-group">

                        <div class="col-lg-6" style="text-align: center">
                            <label>
                                Sensor Name :
                                                                    <asp:TextBox ID="txtSName" runat="server" CssClass="form-control"></asp:TextBox>
                            </label>
                        </div>
                    </div>

                    <div class="form-group ">

                        <div class="col-lg-6" style="text-align: center">
                            <label>
                                <br />
                                <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click" />
                            </label>
                        </div>
                    </div>


                </div>

                <br />
                <br />
                <br />
                <br />
                <div class="row">
                    <div class="col-lg-12" style="text-align: center">
                        <div class="table-responsive">
                            <asp:GridView ID="grdwifisensor" runat="server" DataKeyNames="SensorId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No record found." OnPageIndexChanging="grdwifisensor_PageIndexChanging" Width="100%"
                                OnRowCancelingEdit="grdwifisensor_RowCancelingEdit" OnRowDeleting="grdwifisensor_RowDeleting"
                                OnRowEditing="grdwifisensor_RowEditing" OnRowUpdating="grdwifisensor_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("SensorId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Profile Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbId" runat="server" Text='<%#Eval("BranchId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Profile Name" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldId" runat="server" Text='<%#Eval("DepartmentId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sensor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSensorName" runat="server" Text='<%#Eval("SensorName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Descripition")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" Text='<%#Eval("Descripition")%>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BSSID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBSSID" runat="server" Text='<%#Eval("BSSID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBSSID" runat="server" Text='<%#Eval("BSSID")%>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SSID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSSID" runat="server" Text='<%#Eval("SSID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSSID" runat="server" Text='<%#Eval("SSID")%>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Password">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPassword" runat="server" Text='<%#Eval("Password")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPassword" runat="server" Text='<%#Eval("Password")%>' CssClass="form-control"></asp:TextBox>
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
                        <!-- train section -->
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
                <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold; background-color: #2a368b; color: #FFFFFF; height: 10px">
                    <asp:Label ID="lblalert" runat="server" Text="Alert" />
                    <asp:Label ID="lblalertid" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold; background-color: #e5e5e5; color: #000000">
                    <asp:Label ID="lblUser" runat="server" />
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
</asp:Content>

