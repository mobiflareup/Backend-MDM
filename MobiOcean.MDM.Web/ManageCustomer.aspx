<%@ Page Title="Manage Customer" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ManageCustomer.aspx.cs" Inherits="MobiOcean.MDM.Web.ManageCustomer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">
                    <div class="bhoechie-tab-content active div">
                        <div class="profile1">&nbsp;&nbsp;Manage Customer
                            <asp:Button ID="btnViewDailyTask" runat="server" CssClass="btn btn-sky text-uppercase custom-add-profile pull-right" Text="View Daily Task" OnClick="btnViewDailyTask_Click"></asp:Button>
                                 <asp:Button ID="btnAssignCustomer" runat="server" CssClass="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Assign Daily Customer By Excel" OnClick="btnAssignCustomer_Click"></asp:Button>

                        </div>

                        <br />

                        <div class="dataTables_length" id="datatable-editable_length">
                            <label>
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </label>
                        </div>

                        <div class="row" style="text-align: center">

                            <div class=" form">

                                <div class="form-group ">

                                    <div class="col-lg-6">
                                        <label>
                                            User Name :
                                                               <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
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
                            <asp:GridView ID="grdUser" runat="server" DataKeyNames="UserId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No record found." OnPageIndexChanging="grdUser_PageIndexChanging" Width="100%" OnRowCommand="grdUser_RowCommand">

                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Bind("UserId") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%#Bind("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemailId" runat="server" Text='<%#Bind("EmailId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMblno" runat="server" Text='<%#Bind("MobileNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manage Customer">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnManageCust" runat="server" Text="Manage Customer" CommandName="Manage App" ToolTip="Manage App" CssClass="btn-link" OnClick="lnkbtnManageCust_Click"></asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manage Daily Customer">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnDailyCust" runat="server" Text="Assigned Daily Customer" CommandName="Daily Customer" CommandArgument='<%#Bind("UserId")%>' CssClass="btn-link"></asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notification">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Notchk" runat="server" AutoPostBack="true" Checked='<%#string.IsNullOrEmpty(Eval("DateOfJoining").ToString() ) ? false: Eval("DateOfJoining").ToString() == "0" ? false : true   %>' OnCheckedChanged="Notchk_change_checkedchange"/>

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
                            <h4><b>
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
                                <asp:Button ID="btnaddselected" runat="server" class="btn btnd btncompt" Width="250px" Text="Add Selected Customer ==>> "  OnClick="btnaddselected_Click" />
                                &nbsp; 
                                        <br />
                                <br />


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
                                            <asp:TemplateField HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAId" runat="server" Text='<%#Bind("CustomerId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="AchkRow_Parents" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="AchkHeader_Parents" AutoPostBack="true" OnCheckedChanged="AchkHeader_Parents_CheckedChanged" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblCustName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>

                            <div class="col-sm-6">
                                <asp:Button ID="btnremoveselected" runat="server" class="btn btnd btncompt" Text=" <<== Remove Selected Customer" Width="250px" OnClick="btnremoveselected_Click" />
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
                                            <asp:TemplateField HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="RlblAId" runat="server" Text='<%#Bind("CustomerId") %>'></asp:Label>
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
                                            <asp:TemplateField HeaderText="Customer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="RlblCustName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
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

</asp:Content>
