<%@ Page Title="Web Category" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="WebCategories.aspx.cs" Inherits="MobiOcean.MDM.Web.WebCategories" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" Runat="Server">
     
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                    <div class="force-overflow">
                    <!-- flight section -->
                    <div class="bhoechie-tab-content active">
                        <div class="profile1" style="margin: 0px;">
                                Web Category Management<asp:Button ID="addappmaster" runat="server" class="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Add Web Category" OnClick="addappmaster_Click" />
                                <div class="clearfix"></div>
                            </div>
                       


                        <asp:Panel ID="pnladdappMas" class="form-group" runat="server">
                            <div class="panel-body table-rep-plugin">
                                <div class=" form">
                                    <div class="col-lg-7">
                                        <div class="form-group ">
                                            <label for="lblname" class="control-label col-lg-4">Category Code* : </label>
                                            <div class="col-lg-8">
                                                  <asp:TextBox ID="txtKCode" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="txtKCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="lblRole" class="control-label col-lg-4">Category Name* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txtKName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtKName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group ">
                                            <label for="lblEmail" class="control-label col-lg-4">Description* :  </label>
                                            <div class="col-lg-8">
                                               <asp:TextBox ID="txtKDesc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="txtKDesc" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>



                                        <div class="form-group ">
                                            <div class="col-lg-10">
                                                <asp:Label ID="msglbll" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-lg-offset-2 col-lg-12">
                                                <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="btnAssign_Click" />
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" CommandName="Cancel"  OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                       
                        <div class="dataTables_length" id="datatable-editable_length" style="text-align:center">
                            <label>
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </label>
                           </div>
                                <div class="row" style="text-align: center">
                                    <div class=" form">
                                        <div class="col-lg-6">
                                            <label>
                                                Category Code :
                                               <div class="input-group stylish-input-group">
                                                   <asp:TextBox ID="txtSrchKCode" runat="server" class="form-control ps"></asp:TextBox>
                                                   <span class="input-group-addon">
                                                       <asp:ImageButton ID="groupCode" runat="server" OnClick="btnSrch_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                   </span>
                                               </div>
                                            </label>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>
                                               Category Name : 
                                              <div class="input-group stylish-input-group">
                                                  <asp:TextBox ID="txtSrchKName" runat="server" class="form-control ps"></asp:TextBox>
                                                  <span class="input-group-addon">
                                                      <asp:ImageButton ID="groupName" runat="server" OnClick="btnSrch_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                  </span>
                                              </div>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <br />

                                <div class="table-responsive">
                                    <asp:GridView ID="grdKey" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false"
                                        ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" OnRowCancelingEdit="grdKey_RowCancelingEdit"
                                         OnRowDeleting="grdKey_RowDeleting" OnRowEditing="grdKey_RowEditing" 
                                        OnRowUpdating="grdKey_RowUpdating" OnPageIndexChanging="grdKey_PageIndexChanging" AllowPaging="true" DataKeyNames="CategoryId" PageSize="20">
                                        <Columns >
                                           <asp:TemplateField HeaderText="Id" Visible="false">
                                           <ItemTemplate>
                                                 <asp:Label ID="lblUId" runat="server" Text='<%#Eval("CategoryId")%>'></asp:Label>
                                           </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category Code">   
                                                <HeaderStyle HorizontalAlign="center" />                                             
                                            <ItemTemplate>
                                                <asp:Label ID="lblKeywordCode" runat="server" Text='<%#Eval("CtegoryCode")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtKeywordCode" runat="server" Text='<%#Eval("CtegoryCode")%>' cssclass="form-control input-sm"></asp:TextBox>
                                            </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                          </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Category Name">
                                             <HeaderStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblKeywordName" runat="server" Text='<%#Eval("CategoryName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtKeywordName" runat="server" Text='<%#Eval("CategoryName")%>' cssclass="form-control input-sm"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Description">
                                                   <HeaderStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("CategoryDesc")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescription" runat="server" Text='<%#Eval("CategoryDesc")%>' cssclass="form-control input-sm"></asp:TextBox>
                                              
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>
                                               
                                               <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" CssClass="btn-link"
                                                Text="Edit" ToolTip="Edit" />
                                           
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="btn-link"
                                                Text="Update" ToolTip="Update" ValidationGroup="Update" />
                                            &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel" CssClass="btn-link"
                                                Text="Cancel" ToolTip="Canecl" />
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"  Width="80px"  />
                                    </asp:TemplateField >
                                         <asp:TemplateField HeaderText="Delete">
                                         <ItemTemplate>
                                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn-link" Text="Delete" CommandName="Delete"   OnClientClick="return confirm('The Keyword will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                        </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" Width="70px" />
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
</asp:Content>

