<%@ Page Title="App Group" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="AppGroup.aspx.cs" Inherits="MobiOcean.MDM.Web.AppGroup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHead" runat="Server"> </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">
                    <div class="bhoechie-tab-content active div">

                        
                            <div class="profile1" style="margin: 0px;">
                                App Group Management<asp:Button ID="addappmaster" runat="server" class="btn btn-sky text-uppercase custom-add-profile pull-right" Text=" Add App Group " OnClick="addappmaster_Click" />
                                <div class="clearfix"></div>
                            </div>

                            <asp:Panel ID="pnladdappMas" class="form-group" runat="server">
                                <div class="panel-body table-rep-plugin">
                                    <div class=" form">
                                        <div class="form-group ">
                                            <label for="company" class="control-label col-lg-4">Group Code *:</label>
                                            <div class="col-lg-6">
                                                <asp:TextBox ID="txtGrpCode1" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtGrpCode1" ErrorMessage="*" ValidationGroup="save" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="firstname" class="control-label col-lg-4">Group Name *:</label>
                                            <div class="col-lg-6">
                                                <asp:TextBox ID="txtGrpName1" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtGrpName1" ErrorMessage="*" ValidationGroup="save" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group ">

                                            <div class="col-lg-10">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <div class="col-lg-10">
                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <div class="col-lg-offset-2 col-lg-10">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="btn btn-primary waves-effect waves-light" Text="Save" OnClick="btnSave_Click" ValidationGroup="save" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success waves-effect waves-light" Text="Cancel" OnClick="btnCancel_Click" />
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

                            <div class="row" style="text-align:center">

                                <div class="form">
                                   
                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                Group Code :
                                                               <asp:TextBox ID="txtGrpCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group ">

                                        <div class="col-lg-4">
                                            <label>
                                                Group Name :
                                                                    <asp:TextBox ID="txtGrpName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </label>
                                        </div>
                                    </div>

                                    <div class="form-group ">

                                        <div class="col-lg-4">
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
                                <asp:GridView ID="grdAppGrp" runat="server" DataKeyNames="AppGroupId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                    PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                    EmptyDataText="No record found." OnPageIndexChanging="grdAppGrp_PageIndexChanging" Width="100%"
                                    OnRowCancelingEdit="grdAppGrp_RowCancelingEdit" OnRowDeleting="grdAppGrp_RowDeleting"
                                    OnRowEditing="grdAppGrp_RowEditing" OnRowUpdating="grdAppGrp_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%#Bind("AppGroupId") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrpCode" runat="server" Text='<%#Bind("AppGroupCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%#Bind("AppGroupCode") %>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrpName" runat="server" Text='<%#Bind("AppGroupName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%#Bind("AppGroupName") %>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manage App">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnManageApp" runat="server" CommandName="Manage App" ToolTip="Manage App" CssClass="btn-link" OnClick="lnkbtnManageApp_Click">
                                                    <i class="fa fa-cogs custom-table-fa" aria-hidden="true"></i>
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


            <asp:Panel runat="server" ID="myModal" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" class="modal-lg modal-md modal-xs">

                <div class="modal-content">
                    <div class="modal-header" id="dragi">
                        <div class="col-sm-6" style="text-align: left">
                            <asp:Label ID="lblGrpId" runat="server" Visible="false"></asp:Label>
                            <h4><b>Group Name :
                                <asp:Label ID="lblGroupName" runat="server"></asp:Label></b></h4>
                        </div>
                        <div class="col-sm-6" style="text-align: right">
                            <asp:Button ID="btnclose" runat="server" Text="x" class="close btn btnd btncompt" Style="margin-top: 3px; margin-right: -15px;" />
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
                                <asp:Button ID="btnaddselected" runat="server" class="btn btnd btncompt" Text="Add Selected App==>>" Width="200px" OnClick="btnaddselected_Click" />
                                &nbsp; 
                                        <br />
                                <br />
                                <%--Search option --%>

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
                                <br />

                                <div class="table-responsive" data-pattern="priority-columns">
                                    <asp:GridView ID="grdaddselected" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record found" ShowHeader="true" ShowHeaderWhenEmpty="true" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable" RowStyle-HorizontalAlign="Center">
                                        <Columns>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="AchkRow_Parents" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="AchkHeader_Parents" AutoPostBack="true" OnCheckedChanged="AchkHeader_Parents_CheckedChanged" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppName" runat="server" Text='<%#Eval("AppName")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppGroupName" runat="server" Text='<%#string.IsNullOrEmpty(Eval("AppGroupName").ToString())?"---":Eval("AppGroupName")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>

                            <div class="col-sm-6">
                                <asp:Button ID="btnremoveselected" runat="server" class="btn btnd btncompt" Text="<<==Remove Selected App" Width="200px" OnClick="btnremoveselected_Click" />
                                &nbsp;
                                <br />
                                <br />


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
                                 <br />
                                <div class="table-responsive" data-pattern="priority-columns">
                                    <asp:GridView ID="grdremoveselected" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record found" ShowHeader="true" ShowHeaderWhenEmpty="true" GridLines="None" CssClass="table mGrid" RowStyle-HorizontalAlign="Center" HeaderStyle-CssClass="protable">
                                        <Columns>

                                            <asp:TemplateField>

                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="RachkRow_Parents" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="RachkHeader_Parents" AutoPostBack="true" OnCheckedChanged="RachkHeader_Parents_CheckedChanged" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="RlblAppName" runat="server" Text='<%#Eval("AppName")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#string.IsNullOrEmpty(Eval("AppGroupName").ToString())?"---":Eval("AppGroupName")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="modal-footer">
                    </div>
                </div>

            </asp:Panel>

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



