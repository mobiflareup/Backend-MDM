<%@ Page Title="Client Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ClientMaster.aspx.cs" Inherits="MobiOcean.MDM.Web.ClientMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <!-- flight section -->
                <div class="force-overflow">
                       <div class="profile1">
                   Client Master
                     <a href="addclient.aspx" class="btn btn-sky text-uppercase custom-add-profile pull-right" >
                         <i class="fa fa-user-plus"></i> &nbsp;&nbsp;Add Client
                     </a>
                </div>

                    <div class="dataTables_length" id="datatable-editable_length">
                        <label>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </label>
                    </div>
                    <div class="row" style="text-align: center">
                        <div class=" form">
                            <div class="col-lg-6">
                                <label>
                                    Client Code :
                                               <div class="input-group stylish-input-group">
                                                   <asp:TextBox ID="txtSrchCode" runat="server" class="form-control ps"></asp:TextBox>
                                                   <span class="input-group-addon">
                                                       <asp:ImageButton ID="groupCode" runat="server" OnClick="btnSrch_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                   </span>
                                               </div>
                                </label>
                            </div>
                            <div class="col-lg-6">
                                <label>
                                    Client Name :
                                              <div class="input-group stylish-input-group">
                                                  <asp:TextBox ID="txtSrchName" runat="server" class="form-control ps"></asp:TextBox>
                                                  <span class="input-group-addon">
                                                      <asp:ImageButton ID="groupName" runat="server" OnClick="btnSrch_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                  </span>
                                              </div>
                                </label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div style="text-align: center">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="grdclient" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" OnPageIndexChanging="grdclient_PageIndexChanging" Width="100%"
                            OnRowCancelingEdit="grdclient_RowCancelingEdit" OnRowDeleting="grdclient_RowDeleting" OnRowEditing="grdclient_RowEditing"
                            OnRowUpdating="grdclient_RowUpdating" OnRowCommand="grdclient_RowCommand" AllowPaging="true" PageSize="20">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("ClientId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Client Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClientCode" runat="server" Text='<%#Eval("ClientCode")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtClientCode" runat="server" Text='<%#Eval("ClientCode")%>' CssClass="TxtBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtCode" runat="server" ControlToValidate="txtClientCode" ErrorMessage="*" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Client Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClientName" runat="server" Text='<%#Eval("ClientName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtClientName" runat="server" Text='<%#Eval("ClientName")%>' CssClass="TxtBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtClientName" runat="server" ControlToValidate="txtClientName" ErrorMessage="*" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAddress" runat="server" Text='<%#Eval("Address")%>' Width="115px" CssClass="TxtBox"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEmailId" runat="server" Text='<%#Eval("EmailId")%>' Width="115px" CssClass="TxtBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEmailId" runat="server" ControlToValidate="txtEmailId" ErrorMessage="*" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                            ControlToValidate="txtEmailId" Display="Dynamic" ErrorMessage="abc@abc.abc"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="Update" ForeColor="Red"></asp:RegularExpressionValidator>

                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Website" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblWebsite" runat="server" Text='<%#Eval("Website")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox id="txtWebsite" runat="server" Text='<%#Eval("Website")%>' cssclass="TxtBox"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"/>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Manager">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManagerName" runat="server" Text='<%#Eval("ManagerName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtManagerName" runat="server" Text='<%#Eval("ManagerName")%>' Width="115px" CssClass="TxtBox"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manager Contact No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManagerContactNo" runat="server" Text='<%#Eval("ManagerContactNo")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtManagerContactNo" runat="server" Text='<%#Eval("ManagerContactNo")%>' MaxLength="10"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="fltrcontact" runat="server" TargetControlID="txtManagerContactNo" FilterType="Numbers" />
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
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assign App">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="AssignApp" runat="server" ToolTip="Assign Custom App" CommandName="Assign App" CommandArgument='<%#Bind("ClientId") %>'><i class="fa fa-tasks"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="btn-link"
                                            ToolTip="Delete" OnClientClick="return confirm('The Keyword will be deleted. Are you sure want to continue?');"><i class="fa fa-trash-o custom-table-fa"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


