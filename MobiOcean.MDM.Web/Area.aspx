<%@ Page Title="Area Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="Area.aspx.cs" Inherits="MobiOcean.MDM.Web.Area" %>

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
                           Area Management
                        <a href="addarea.aspx" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Add Area</span></a>
                            




                            <div class="clearfix"></div>
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
                                        Area Name : 
                                                                    <asp:TextBox ID="txtSrchArea" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                            </div>

                            <div class="form-group ">
                                <div class="col-lg-6">
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
                        <asp:GridView ID="grdArea" runat="server" DataKeyNames="AreaId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                            PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No record found." Width="100%" OnPageIndexChanging="grdArea_PageIndexChanging" OnRowCancelingEdit="grdArea_RowCancelingEdit" OnRowDeleting="grdArea_RowDeleting" OnRowEditing="grdArea_RowEditing" OnRowUpdating="grdArea_RowUpdating">

                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("AreaId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Area Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAreaName" runat="server" Text='<%#Eval("AreaName")%>'></asp:Label>
                                    </ItemTemplate>
                                   
                                           
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location")%>'></asp:Label>
                                    </ItemTemplate>
                                    
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Radius (In Meter)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRadius" runat="server" Text='<%#Eval("Radius")%>'></asp:Label>
                                    </ItemTemplate>
                                   

                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Latitude" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLat" runat="server" Text='<%#Eval("Latitude")%>'></asp:Label>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Longitude" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLongi" runat="server" Text='<%#Eval("Longitude")%>'></asp:Label>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit"
                                            ToolTip="Edit" OnClick="EditButton_click"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>

                                    </ItemTemplate>
                                   
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
                                    <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold;background-color:#2a368b;color:#FFFFFF;height:10px">
                                        <asp:Label ID="lblalert" runat="server" Text="Alert" />
                                        <asp:Label ID="lblkeyid" runat="server" Visible="false"></asp:Label>                                                                        
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



