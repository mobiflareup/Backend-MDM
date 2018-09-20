<%@ Page Title="Web keyword" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="WebKeyword.aspx.cs" Inherits="MobiOcean.MDM.Web.WebKeyword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="coh" runat="server" ContentPlaceHolderID="ContentHead">
    <script>
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $("#flip").click(function () {
                    $('#<%=lblMsg.ClientID%>').html("");
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
                                <img src="image/pro-ed.png" class="iconview"></i>&nbsp;&nbsp;Keyword Management</li>
                            <%--<li class="profile1xs hidden-lg hidden-md"><i><img src="image/pro-ed.png" class="iconview"></i></li>--%>
                            <br />

                            <%--<div class="creatp">--%><a href="#" id="flip" class="btn btnd btncompt"><i><img src="image/plus-4.png" class="iconview1"></i>&nbsp;&nbsp;<span class="creatsp">Add Keyword</span></a><%--</div>--%><br />
                            <%--<div class="row">
<h2 class="pmanage">Keyword Management</h2>         
          </div>--%>
                            <div id="panel" class="flipkey">
                                <div class=" form">
                                    <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
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
                                            <label for="firstname" class="control-label col-lg-4">Keyword Purpose* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txtAddKeywordPurpose" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtAddKeywordPurpose" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
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
                                                <%--<button class="btn btn-success waves-effect waves-light" type="submit">Save</button>
                                                        <a href="admindashboard.aspx" class="btn btn-default waves-effect" type="button">Cancel</a>--%>
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" />

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
                                                <asp:RequiredFieldValidator ID="rfdes" runat="server"
                                                    ControlToValidate="txtDescription" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>

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
                                                    ToolTip="Delete" OnClientClick="return confirm('The Profile will be deleted. Are you sure want to continue?');"><i><img src="image/Delete.png" class="iconview"></i></asp:LinkButton>
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
