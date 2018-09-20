<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="SADashBoard1.aspx.cs" Inherits="MobiOcean.MDM.Web.SADashBoard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active div">

                <div class="profile1">&nbsp;&nbsp;Super Admin Dashboard

                </div>
                <br />

                <div class="row" style="text-align: center">
                    <div class=" form">
                     


                        <div class="col-md-3">
                            <label>
                                By Name :
                                                                         <div class="input-group stylish-input-group">
                                                                             <asp:TextBox ID="txtSrchUserName" runat="server" class="form-control ps"></asp:TextBox>
                                                                             <span class="input-group-addon">
                                                                                 <asp:ImageButton ID="ImageButton2" runat="server" OnClick="btnSrch_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                                             </span>
                                                                         </div>
                            </label>
                        </div>
                        
                        
                        <div class="col-md-3">
                            <label>
                                By Email :
                                                                      <div class="input-group stylish-input-group">
                                                                          <asp:TextBox ID="txtSrchEmailId" runat="server" class="form-control ps"></asp:TextBox>
                                                                          <span class="input-group-addon">
                                                                              <asp:ImageButton ID="ImageButton8" runat="server" OnClick="btnSrch3_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                                          </span>
                                                                      </div>

                            </label>

                        </div>
                        <div class="col-md-3">
                            <label>
                                By Role :
                                                         
                                                                         <asp:DropDownList ID="dtRoleId" runat="server" CssClass="form-control" AppendDataBoundItems="true" Style="color: black;" OnSelectedIndexChanged="dtRoleId_SelectedIndexChanged" AutoPostBack="true">
                                                                          </asp:DropDownList>
                                                        

                            </label>
                        </div>
                        <div class="col-md-3">
                            <label>
                                By Client :
                                                                      
                                                                          <asp:DropDownList ID="dtClientId" runat="server" CssClass="form-control" AppendDataBoundItems="true" Style="color: black;" OnSelectedIndexChanged="dtClientId_SelectedIndexChanged" AutoPostBack="true">
                                                                          </asp:DropDownList>
                                                              

                            </label>

                        </div>

                            </div>


                    </div>
  <br />
                <div class="row">
                    <div class="col-sm-12" style="text-align: center">
                        <div class="dataTables_length" id="datatable-editable_length">
                            <label>
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="table-responsive">
                    <asp:GridView ID="grdUsr" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                        OnRowCancelingEdit="grdUsr_RowCancelingEdit" OnRowDeleting="grdUsr_RowDeleting" OnRowEditing="grdUsr_RowEditing" OnRowDataBound="grdUsr_RowDataBound"
                        OnRowUpdating="grdUsr_RowUpdating" OnPageIndexChanging="grdUsr_PageIndexChanging" AllowPaging="true" DataKeyNames="UserId" PageSize="20" Width="100%">

                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Client Name">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblClientName" runat="server" Text='<%#Eval("ClientName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Client Code">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblClientCode" runat="server" Text='<%#Eval("ClientCode")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Employee Id">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUserCode" runat="server" Text='<%#Eval("UserCode")%>'></asp:Label>
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:TextBox ID="txtUserCode" runat="server" Text='<%#Eval("UserCode")%>' CssClass="form-control input-sm"></asp:TextBox>
                                </EditItemTemplate>--%>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:TextBox ID="txtUserName" runat="server" Text='<%#Eval("UserName")%>' CssClass="form-control input-sm"></asp:TextBox>
                                </EditItemTemplate>--%>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mobile No">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo")%>'></asp:Label>
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:TextBox ID="txtMobileNo" runat="server" Text='<%#Eval("MobileNo")%>' CssClass="form-control input-sm" MaxLength="10"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="Efmob" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers" />
                                </EditItemTemplate>--%>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email Id">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId")%>'></asp:Label>
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:TextBox ID="txtEmailId" runat="server" Text='<%#Eval("EmailId")%>' CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                        ControlToValidate="txtEmailId" Display="Dynamic" ErrorMessage="abc@abc.abc"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="Update" ForeColor="Red"></asp:RegularExpressionValidator>
                                </EditItemTemplate>--%>
                                <ItemStyle HorizontalAlign="Center" Wrap="true" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Password">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPassword" runat="server" Text='<%#Eval("Password")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRole" runat="server" Text='<%#Eval("RoleName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <%--   <asp:TemplateField HeaderText="Branch Name">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("BranchName").ToString()==""?"---":Eval("BranchName").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBranchName" runat="server" Text='<%#Eval("BranchName")%>' CssClass="form-control input-sm"></asp:TextBox>

                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Name">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepartName" runat="server" Text='<%#Eval("DeptName").ToString()==""?"---":Eval("DeptName").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDepartName" runat="server" Text='<%#Eval("DeptName")%>' CssClass="form-control input-sm"></asp:TextBox>

                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Profile Name">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProfileName" runat="server" Text='<%#Eval("ProfileName").ToString()==""?"---":Eval("ProfileName")%>'></asp:Label>
                                        <asp:Label ID="lblProfileId" runat="server" Text='<%#Eval("ProfileId")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Profile Status">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsEnable" runat="server" Text='<%#Eval("IsEnable")%>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lbtnactivate" runat="server" CssClass="LinkBtn" OnClick="lbtnactivate_Click" Text='<%#Eval("IsEnable").ToString()=="1"?"Active":Eval("IsEnable").ToString()=="0"?"Inactive":"---"%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="DashBoard">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Dashboard" runat="server" CssClass="btn-link"
                                            Text="DashBoard" ToolTip="User DashBoard" OnClick="Dashboard_Click"  />

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit"
                                            ToolTip="Edit" OnClick="EditButton_Click"><i><img src="image/edit.png"></i></asp:LinkButton>

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
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="btn-link"
                                            ToolTip="Delete"><i><img src="image/Delete.png" class="iconview"></i></asp:LinkButton>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

