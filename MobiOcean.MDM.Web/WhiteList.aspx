<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="WhiteList.aspx.cs" Inherits="MobiOcean.MDM.Web.WhiteList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="coh" runat="server" ContentPlaceHolderID="ContentHead">
    <script>
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $("#flip").click(function () {
                    $("#panel").slideToggle("slow");
                });
                $("#btnCancel").click(function () {
                    $("#panel").slideToggle("slow");
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ScriptManager>--%>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 bhoechie-tab scrollbar" id="style-3">
                    <div class="force-overflow">

                        <div class="bhoechie-tab-content active div">

                            <!---<li style="width:20%; font-size:18px; list-style:none; float:right">Back</li>---->
                            <li class="profile1"><i>
                                <img src="image/pro-ed.png" class="iconview"></i>&nbsp;&nbsp;White List</li>
                            <%--<li class="profile1xs hidden-lg hidden-md"><i>
                            <img src="image/pro-ed.png" class="iconview"></i></li>--%>
                            <br />

                            <%--<div class="creatp">--%><a href="#" id="flip" class="btn btnd btncompt"><i><img src="image/plus-4.png" class="iconview1"></i>&nbsp;&nbsp;<span class="creatsp">Add Mobile Number</span></a><%--</div>--%><br />
                            <%-- <div class="row">
<h2 class="pmanage">Profile Management</h2>         
          </div>--%><div id="panel" class="flipkey">
              <div class=" form">
                  <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                  <div class="col-lg-7 col-md-12">
                      <div class="form-group ">

                          <div class="col-lg-12">
                              <br />
                          </div>
                      </div>
                      <div class="form-group ">
                          <label for="lastname" class="control-label col-lg-4">Name* : </label>
                          <div class="col-lg-8">
                              <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                  ControlToValidate="txtName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>

                          </div>
                      </div>
                      <div class="form-group ">
                          <label for="firstname" class="control-label col-lg-4">Mobile No* : </label>
                          <div class="col-lg-8">
                              <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                  ControlToValidate="txtMobileNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                              <asp:FilteredTextBoxExtender ID="fe" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers" />
                          </div>
                      </div>
                      <div class="form-group ">
                          <label for="lastname" class="control-label col-lg-4">White list for* : </label>
                          <div class="col-lg-8">
                              <asp:CheckBox ID="chkIncoming" runat="server" Text="Incoming Call" />
                              &nbsp; &nbsp;
                                                     <asp:CheckBox ID="chkOutgoing" runat="server" Text="Outgoing Call" />
                              &nbsp; &nbsp; &nbsp; &nbsp;
                                                     <asp:CheckBox ID="chkSms" runat="server" Text="Sms" />
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
                              <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="btnSave_Click" />
                              <%--<button class="btn btn-success waves-effect waves-light" type="submit">Save</button>
                                                        <a href="admindashboard.aspx" class="btn btn-default waves-effect" type="button">Cancel</a>--%>
                              <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnCancel_Click" />

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
                                            <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="text-align: center">
                            </div>

                            <br />
                            <div class="table-responsive">
                                <asp:GridView ID="grdNo" runat="server" DataKeyNames="AllowedPhNoId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                    PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                    EmptyDataText="No record found." Width="100%" OnRowCancelingEdit="grdNo_RowCancelingEdit" OnRowDeleting="grdNo_RowDeleting" OnRowEditing="grdNo_RowEditing" OnRowUpdating="grdNo_RowUpdating" OnPageIndexChanging="grdNo_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%#Eval("AllowedPhNoId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEName" runat="server" Text='<%#Eval("Name")%>' Font-Bold="False" Font-Size="Large"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEName" runat="server" Text='<%#Eval("Name")%>' CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtEName" ErrorMessage="*" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEMobileNo" runat="server" Text='<%#Eval("MobileNo")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="White List" Visible="false">
                                            <ItemTemplate>
                                                <asp:Image ID="Img" CssClass="iconview" runat="server" ImageUrl='<%#Eval("IsWhiteList").ToString()=="1"?"~/Image/Check.png":"~/Image/Reject.png"%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Incoming Call">
                                            <ItemTemplate>
                                                <asp:Image ID="ImgIncoming" Height="25px" Width="25px" runat="server" ImageUrl='<%#Eval("IsForIncoming").ToString()=="1"?"~/Image/Check.png":"~/Image/Reject.png"%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="ChkEIncoming" runat="server" Checked='<%#GetStatus(Eval("IsForIncoming").ToString())%>' />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Outgoing Call">
                                            <ItemTemplate>
                                                <asp:Image ID="ImgOutgoing" Height="25px" Width="25px" runat="server" ImageUrl='<%#Eval("IsForOutgoing").ToString()=="1"?"~/Image/Check.png":"~/Image/Reject.png"%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="ChkEOutgoing" runat="server" Checked='<%#GetStatus(Eval("IsForOutgoing").ToString())%>' />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sms">
                                            <ItemTemplate>
                                                <asp:Image ID="ImgSms" Height="25px" Width="25px" runat="server" ImageUrl='<%#Eval("IsForSms").ToString()=="1"?"~/Image/Check.png":"~/Image/Reject.png"%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="ChkESms" runat="server" Checked='<%#GetStatus(Eval("IsForSms").ToString())%>' />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit"
                                                    ToolTip="Edit"><i><img src="image/edit.png"></i></asp:LinkButton>

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
                                                    ToolTip="Delete" OnClientClick="return confirm('The Mobile will be deleted. Are you sure want to continue?');"><i><img src="image/Delete.png" class="iconview"></i></asp:LinkButton>
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

        <!-- ============================================================== -->
        <!-- End Right content here -->
        <!-- ============================================================== -->
    <%--</form>--%>
</asp:Content>
