<%@ Page  Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ApplicationManageOTG.aspx.cs" Inherits="MobiOcean.MDM.Web.ApplicationManageOTG" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    <style>
        .img-50 {
            width: 50px;
        }

        .modalBackgroundTemp {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">

                    <div class="bhoechie-tab-content active div">

                        <div class="profile1">&nbsp;&nbsp;OS List<asp:Button ID="addos" runat="server" class="btn btn-sky text-uppercase custom-add-profile pull-right" Text=" Add Os" OnClick="addos_Click"/></div>
                        <br />
                        <div class="row" style="text-align: center">
                            <div class=" form">

                                <div class="col-md-3">
                                    <label>
                                        By Package Name :
                              <asp:TextBox ID="txtSrchAppPackage" runat="server" class="form-control"></asp:TextBox>
                                    </label>
                                </div>

                                <div class="col-md-3">
                                    <label>
                                        By App Version :
                              <asp:TextBox ID="txtSrchAppVersion" runat="server" class="form-control"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <br />
                                    <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />

                                </div>

                            </div>


                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <div class="dataTables_length" id="datatable-editable_length">
                                    <label>
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="grdapplst" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None"
                                AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                                OnPageIndexChanging="grdapplst_PageIndexChanging"
                                AllowPaging="true" DataKeyNames="OsId" PageSize="10" Width="100%" OnRowDeleting="grdapplst_RowDeleting">
                                <%--// OnRowDataBound="grdapplst_RowDataBound" OnRowCommand="grdapplst_RowCommand" OnRowEditing="grdapplst_RowEditing" OnRowCancelingEdit="grdapplst_RowCancelingEdit" OnRowUpdating="grdapplst_RowUpdating" --%>

                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOsId" runat="server" Text='<%#Eval("OsId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="App Package">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppName" runat="server" Text='<%#Eval("AppPackage")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="App Version">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppType" runat="server" Text='<%#Eval("AppVersion")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="App Version No">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeviceType" runat="server" Text='<%#Eval("AppVersionNo")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="Mandatory Update">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppVersion" runat="server" Text='<%#Eval("MandatoryUpdate").ToString()=="0"?"Yes":"No"%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Allow Download">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateTime" runat="server" Text='<%#Eval("AllowDownload").ToString()=="0"?"Yes":"No"%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Remark">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remark")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnUpgrade" runat="server" CommandName="Upgrade" CssClass="btn btn-xs btn-default single-delete" OnClick="lnkbtnUpgrade_Click"><i class="fa fa-arrow-up"></i> Upgrade</asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="btn btn-xs btn-default single-delete" OnClick="lnkbtnEdit_Click"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" ToolTip="Delete" CssClass="btn btn-xs btn-default single-delete" OnClick="lnkbtnDelete_Click"><i class="fa fa-trash-o"></i> Delete</asp:LinkButton>
                                        </ItemTemplate>
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
    <center>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divProcessing">
                            <asp:Image runat="server" ID="progressImg2" ImageUrl="~/images/Processing.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </center>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="dummydelete" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="mpdelete" runat="server" PopupControlID="DeleteMessagePnl"
                TargetControlID="dummydelete" CancelControlID="btnccl"
                BackgroundCssClass="modalbackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="DeleteMessagePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">

                <div class="modal-body" style="text-align: center;">
                    <asp:Label ID="lblfinalAppMarketId" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="Button2" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
                    <asp:Label ID="Label2" runat="server" Text="Are you sure you want to Delete?" ForeColor="White"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btncancelok" runat="server" Text="OK" OnClick="Yes_Click" />
                    <asp:Button ID="btnccl" runat="server" Text="Cancel" OnClick="No_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


