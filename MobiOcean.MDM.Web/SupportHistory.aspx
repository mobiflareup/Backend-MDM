<%@ Page Title="Ticket History" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="SupportHistory.aspx.cs" Inherits="MobiOcean.MDM.Web.SupportHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- Start content -->
            <div class="bhoechie-tab-content active">
                <div class="profile1" style="margin: 0px;">
                    Support Ticket History
                      <a href="SupportMaster.aspx" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-backward"></i>&nbsp;&nbsp;<span>Back</span></a>

                    <div class="clearfix"></div>
                </div>


                <br />
                <br />

                <div class="row">
                    <div class="col-lg-12">



                        <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <div class="dataTables_length" id="datatable-editable_length">
                                    <label>
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="grdHstry" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false" DataKeyNames="SupportHistoryId"
                                EmptyDataText="No Record Found" ShowHeader="true" ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("SupportHistoryId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Defect Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStrtTime" runat="server" Text='<%#Eval("DefectName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Error URL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndTime" runat="server" Text='<%#Eval("ErrorURL") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Defect Desc">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDefectDesc" runat="server" Text='<%#Eval("DefectDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Response">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResponse" runat="server" Text='<%#(!string.IsNullOrEmpty(Eval("Response").ToString())) ? Eval("Response"):"N/A"%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reponse Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResponseDate" runat="server" Text='<%#(!string.IsNullOrEmpty(Eval("ResponseDate").ToString())) ? Eval("ResponseDate"):"N/A"%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="file" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfile" runat="server" Text='<%#(!string.IsNullOrEmpty(Eval("DocPath").ToString())) ? Eval("DocPath"):"N/A"%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Download">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" Text='<%#(!string.IsNullOrEmpty(Eval("DocPath").ToString())) ? "Download":"N/A"%>' runat="server" OnClick="lnkButton_Click" CssClass="btn-link" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>
                <!-- end: page -->
            </div>

        </div>
    </div>



</asp:Content>


