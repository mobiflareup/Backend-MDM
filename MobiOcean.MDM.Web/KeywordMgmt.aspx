<%@ Page Title="Keyword Management" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="KeywordMgmt.aspx.cs" Inherits="MobiOcean.MDM.Web.KeywordMgmt" %>

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
                           Keyword Management
                            <a href="#" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Add Keyword</span></a>
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
                          <label for="lastname" class="control-label col-lg-4">Keyword Code* : </label>
                          <div class="col-lg-8">
                              <asp:TextBox ID="txtAddKeywordCode" runat="server" CssClass="form-control"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                  ControlToValidate="txtAddKeywordCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>

                          </div>
                      </div>
                      <div class="form-group ">
                          <label for="firstname" class="control-label col-lg-4">Keyword Name* : </label>
                          <div class="col-lg-8">
                              <asp:TextBox ID="txtAddKeywordName" runat="server" CssClass="form-control"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                  ControlToValidate="txtAddKeywordName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                          </div>
                      </div>
                      <div class="form-group ">
                          <label for="firstname" class="control-label col-lg-4">Description* : </label>
                          <div class="col-lg-8">
                              <asp:TextBox ID="txtAddDescription" runat="server" CssClass="form-control"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                  ControlToValidate="txtAddDescription" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
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
                              <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="btnAssign_Click" />
                              <input type="button" id="btnCancel" class="btn btnd btncompt waves-effect" value="Cancel" />
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
                                                Keyword Code :
                                                                <asp:TextBox ID="txtSrchKCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                Keyword Name : 
                                                                   <asp:TextBox ID="txtSrchKName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </label>
                                        </div>
                                    </div>

                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                <br />
                                                <asp:Button ID="btnSrch" runat="server" Text="Search" class="btn btnd btncompt" OnClick="btnSrch_Click" />
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <br />
                            <div class="table-responsive">
                                <asp:GridView ID="grdKey" runat="server" DataKeyNames="KeywordId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                    PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                    EmptyDataText="No record found." OnPageIndexChanging="grdKey_PageIndexChanging" Width="100%"
                                    OnRowCancelingEdit="grdKey_RowCancelingEdit" OnRowDeleting="grdKey_RowDeleting"
                                    OnRowEditing="grdKey_RowEditing" OnRowUpdating="grdKey_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUId" runat="server" Text='<%#Eval("KeywordId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Keyword Code">
                                            <HeaderStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblKeywordCode" runat="server" Text='<%#Eval("KeywordCode")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtKeywordCode" runat="server" Text='<%#Eval("KeywordCode")%>' CssClass="form-control input-sm"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfkc" runat="server"
                                                    ControlToValidate="txtKeywordCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Keyword Name">
                                            <HeaderStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblKeywordName" runat="server" Text='<%#Eval("KeywordName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtKeywordName" runat="server" Text='<%#Eval("KeywordName")%>' CssClass="form-control input-sm"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfkn" runat="server"
                                                    ControlToValidate="txtKeywordName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <HeaderStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescription" runat="server" Text='<%#Eval("Description")%>' CssClass="form-control input-sm"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfkd" runat="server"
                                                    ControlToValidate="txtDescription" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>

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
                                                <%--<asp:LinkButton ID="lbDelete" runat="server" CssClass="btn-link" Text="Delete"></asp:LinkButton>--%>
                                                <%--<asp:ImageButton ID="lbldelete" runat="server" CssClass="bg-img"/>--%>
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
   <script>
       function pageLoad(sender, args) {
           //$(document).ready(function () {
               $("#flip").click(function () {
                   $('#<%=lblMsg.ClientID%>').html("");
                    $("#panel").slideToggle("slow");
                });
                $("#btnCancel").click(function () {
                    $("#panel").slideToggle("slow");
                });
           // });
        }
    </script>
     
</asp:Content>



