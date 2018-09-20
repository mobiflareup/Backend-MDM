<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ModeOfTravel.aspx.cs" Inherits="MobiOcean.MDM.Web.ModeOfTravel" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <!-- flight section -->
                <div class="bhoechie-tab-content active">

                    <div class="profile1">&nbsp;&nbsp;Mode Of Travel</div>

                    <div class="row">
														<div class="col-sm-12" style="text-align:center">
															<div class="dataTables_length" id="datatable-editable_length">
																<label> <asp:Label ID="lblMsg" runat="server"></asp:Label></label>
															</div>
														</div>														
													</div>

                  
                        
                        <div class="content padding-top-none">

                            <div class="row" style="text-align: center">
                                <div class=" form">
                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                Mode Of Travel :
                                                                  <asp:TextBox ID="txtMode" runat="server" CssClass="form-control"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="txtMode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>

                                            </label>
                                        </div>
                                    </div>
                                   
                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                Conveyance Rate Per km : 
                                                                    <asp:TextBox ID="txtConveyanceAmt" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtConveyanceAmt" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtConveyanceAmt"
                                FilterType="Numbers">
                                               </asp:FilteredTextBoxExtender>
                                            </label>
                                        </div>
                                    </div>

                                    <div class="form-group ">
                                        <div class="col-lg-4">
                                            <%--<label>--%>
                                                <br />
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btnd btncompt" OnClick="btnAdd_Click" ValidationGroup="save"/>
                                                &nbsp;&nbsp;<asp:Button ID="rest" runat="server" Text="Reset" CssClass="btn btnd btncompt" OnClick="rest_Click" />
                                            <%--</label>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            

                        </div>
                            <br />

                            <div class="table-responsive">


                                <asp:GridView ID="grdMode" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Record found." DataKeyNames="ModeId"
                                    OnPageIndexChanging="grdMode_PageIndexChanging" Width="100%" PageSize="20" AllowPaging="true" OnRowCancelingEdit="grdMode_RowCancelingEdit" OnRowDeleting="grdMode_RowDeleting" OnRowEditing="grdMode_RowEditing" OnRowUpdating="grdMode_RowUpdating">
                                  
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%#Eval("ModeId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mode Of Travel">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModeOfTravel" runat="server" Text='<%#Eval("ModeOfTravel")%>'></asp:Label>
                                            </ItemTemplate>
                                              <EditItemTemplate>
                                                <asp:TextBox ID="txtModeOFTravel" runat="server" Text='<%#Eval("ModeOfTravel")%>' CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtModeOFTravel" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Conveyance Rate Per km">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConveyanceAmt" runat="server" Text='<%#Eval("ConveyanceAmount")%>'></asp:Label>
                                            </ItemTemplate>
                                             <EditItemTemplate>
                                                <asp:TextBox ID="txtConveyanceAmount" runat="server" Text='<%#Eval("ConveyanceAmount")%>' CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvcode1" runat="server" ControlToValidate="txtConveyanceAmount" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="Edit" Visible="true">
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
                                        <asp:Label ID="lblalerturlid" runat="server" Visible="false"></asp:Label>                                                                        
                                    </td>                                   
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold;background-color:#e5e5e5;color:#000000">
                                        <asp:Label ID="lblUser" runat="server" Text="Are you sure to delete?" />
                                    </td>
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

