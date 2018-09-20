<%@ Page Title="Web Category" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="WebCategory.aspx.cs" Inherits="MobiOcean.MDM.Web.WebCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">
                    <div class="bhoechie-tab-content active div">

                        <div class="profile1" style="margin: 0px;">
                            Web Category<asp:Button ID="addappmaster" runat="server" class="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Add Website Category" OnClick="addappmaster_Click" />



                            <div class="clearfix"></div>
                        </div>



                        <asp:Panel ID="pnladdappMas" class="form-group" runat="server">
                            <div class="panel-body table-rep-plugin">
                                <div class=" form">
                                    <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                                    <div class="col-lg-7">
                                        <div class="form-group ">
                                            <label for="bname" class="control-label col-lg-4">Category Code* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txtKCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                    ControlToValidate="txtKCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">

                                            <%-- <div class="col-lg-12">
                                                        <br />
                                                    </div>--%>
                                        </div>
                                        <div class="form-group ">
                                            <label for="firstname" class="control-label col-lg-4">Category Name* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txtKName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtKName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="lastname" class="control-label col-lg-4">Description* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txtKDesc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtKDesc" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
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
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" CommandName="Cancel" OnClick="btnCancel_Click" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-5">
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>












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
                                <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>

                                <div class="form-group ">

                                    <div class="col-lg-6">
                                        <label>
                                            Category Name :
                                                                    <asp:TextBox ID="txtGrpName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group ">

                                    <div class="col-lg-6">
                                        <label>
                                            <br />
                                            <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click" />
                                        </label>
                                    </div>
                                </div>


                            </div>
                        </div>

                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="GridFeature" runat="server" DataKeyNames="CategoryId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No record found." OnRowEditing="GridFeature_RowEditing" OnRowCancelingEdit="GridFeature_RowCancelingEdit" OnRowDeleting="GridFeature_RowDeleting" OnRowUpdating="GridFeature_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("CategoryId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryCode" runat="server" Text='<%#Eval("CtegoryCode")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEGrpCode" runat="server" Text='<%#Eval("CtegoryCode") %>' CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtEGrpCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%#Eval("CategoryName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEGrpName" runat="server" Text='<%#Eval("CategoryName") %>' CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtEGrpName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryDesc" runat="server" Text='<%#Eval("CategoryDesc")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEDesc" runat="server" Text='<%#Eval("CategoryDesc") %>' CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvnametxtEDesc" runat="server" ControlToValidate="txtEDesc" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manage">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPopUp" runat="server" OnClick="lnkPopUp_Click"><i class="fa fa-cogs custom-table-fa" aria-hidden="true"></i>
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandName="Edit" ToolTip="Edit" CssClass="btn-link"></asp:LinkButton>
                                            &nbsp;
                                                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CommandName="Delete" ToolTip="Delete" CssClass="btn-link"></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="btn-link"
                                                Text="Update" ToolTip="Update" ValidationGroup="Update" />
                                            &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel" CssClass="btn-link"
                                                Text="Cancel" ToolTip="Canecl" />
                                        </EditItemTemplate>
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="dummy_BtnAsgnGp" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mp" runat="server" PopupControlID="myModal"
                PopupDragHandleControlID="dragi" TargetControlID="dummy_BtnAsgnGp" CancelControlID="btnclose"
                BackgroundCssClass="modalbackground">
            </asp:ModalPopupExtender>
            <%--<div id="myModal"  class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">--%>

            <asp:Panel runat="server" ID="myModal" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" class="modal-lg modal-md modal-sm modal-xs">
                <%--<div class="modal-dialog popwidth">--%>
                <div class="modal-content">
                    <div class="modal-header" id="dragi">
                        <div class="col-sm-8" style="text-align: left">
                            <asp:Label ID="lblGrpId" runat="server" Visible="false"></asp:Label>
                            <h4><b></b>Category Name :
                                <asp:Label ID="lblGroupName" runat="server"></asp:Label></b></h4>
                        </div>
                        <div class="col-sm-4" style="text-align: right">
                            <asp:Button ID="btnclose" runat="server" Text="x" class="close btn btnd btncompt" />
                        </div>
                    </div>
                    <div class="modal-header">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblPopMsg" runat="server"></asp:Label>
                            </div>

                        </div>

                    </div>

                    <div class="modal-body">
                        <div class="row" style="height: 250px; overflow: auto">
                            <div class="col-sm-6">
                                <asp:Button ID="btnaddselected" runat="server" class="btn btnd btncompt" Text="Add Selected ==>>" Width="200px" OnClick="btnaddselected_Click" />
                                &nbsp; 
                                <br />
                                <br />
                                <%-- Search --%>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnSearch" runat="server" class="btn btnd btncompt" Text="Search" Width="100px" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="table-responsive" data-pattern="priority-columns">
                                    <asp:GridView ID="grdaddselected" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record found" ShowHeader="true" ShowHeaderWhenEmpty="true" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAId" runat="server" Text='<%#Bind("UrlId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="AchkRow_Parents" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="AchkHeader_Parents" runat="server" AutoPostBack="true" OnCheckedChanged="AchkHeader_Parents_CheckedChanged" />

                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Website">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppName" runat="server" Text='<%#Eval("Url")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppGroupName" runat="server" Text='<%#string.IsNullOrEmpty(Eval("CategoryName").ToString())?"---":Eval("CategoryName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                            <%--<div class="col-sm-4">
                                    <br />
                                        </div>--%>
                            <div class="col-sm-6">
                                <asp:Button ID="btnremoveselected" runat="server" class="btn btnd btncompt" Text="<<==Remove Selected" Width="200px" OnClick="btnremoveselected_Click" />
                                &nbsp;
                                <br />
                                <br />

                                <%-- Search --%>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtRemoveSearch" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnRemoveSearch" runat="server" class="btn btnd btncompt" Text="Search" Width="100px" OnClick="btnRemoveSearch_Click" />
                                    </div>
                                </div>
                                <br />


                                <div class="table-responsive" data-pattern="priority-columns">
                                    <asp:GridView ID="grdremoveselected" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record found" ShowHeader="true" ShowHeaderWhenEmpty="true" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="RlblAId" runat="server" Text='<%#Bind("UrlId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="RachkRow_Parents" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="RachkHeader_Parents" AutoPostBack="true" OnCheckedChanged="RachkHeader_Parents_CheckedChanged" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Website">
                                                <ItemTemplate>
                                                    <asp:Label ID="RlblAppName" runat="server" Text='<%#Eval("Url")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#string.IsNullOrEmpty(Eval("CategoryName").ToString())?"---":Eval("CategoryName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="modal-footer">

                        <%--
                                                       
                                <button type="button" class="btn btn-primary waves-effect" data-dismiss="modal">Save</button>
                                <button type="button" class="btn btn-primary waves-effect" data-dismiss="modal">Cancel</button>--%>
                    </div>
                </div>
                <!-- /.modal-content -->
                <%-- </div>--%>
                <!-- /.modal-dialog -->
                <%-- </div>--%>
                <!-- /.modal -->
            </asp:Panel>
            <%--   </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            if (!args.get_isPartialLoad()) {
                //  adding handler to the document's keydown event
                $addHandler(document, "keydown", onKeyDown);
            }
        }
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                // if the key pressed is the escape key, then close the dialog
                $find("<% =mp.ClientID%>").hide();
               }
           }
    </script>
</asp:Content>

