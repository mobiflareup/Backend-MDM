<%@ Page Title="Transgression Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="Alert.aspx.cs" Inherits=" MobiOcean.MDM.Web.Alert" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
           
                <div class="bhoechie-tab-content active">
                    <div class="profile1" style="margin: 0px;">
                        Transgression Report
                         <div class="clearfix"></div>
                    </div>


                    <div class="row">
                        <div class="col-sm-12" style="text-align: ">
                            <div class="dataTables_length" id="datatable-editable_length">
                                <label>
                                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                </label>
                            </div>
                        </div>
                    </div>


                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="grdAlert" runat="server" DataKeyNames="UserId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                            PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No record found." OnPageIndexChanging="grdAlert_PageIndexChanging" Width="100%" OnRowDataBound="grdAlert_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("AlertId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Alert Text">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlertText" runat="server" Text='<%#Eval("AlertText")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Read" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsRead" runat="server" Text='<%#Eval("IsRead")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateTime" runat="server" Text='<%#Eval("LogDateTime")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mark as Read">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkbox" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                        </asp:GridView>

                    </div>
                    <div class="row" style="text-align: right">
                        <asp:Button ID="btnApplyChanges" runat="server" CssClass="btn btnd btncompt" Text="Apply Changes" align="right" OnClick="btnApplyChanges_Click" />
                    </div>


                    <!-- train section -->


                </div>

            </div>
      

       </div>
</asp:Content>
