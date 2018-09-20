<%@ Page Title="Support Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="SupportMaster.aspx.cs" Inherits="MobiOcean.MDM.Web.SupportMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
  
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
               
                <div class="bhoechie-tab-content active">
                   <div class="profile1" style="margin: 0px;">
                                Support Ticket
                       <a href="SupportForm.aspx"  class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Add Ticket</span></a>
                                <div class="clearfix"></div>
                            </div>

                     <br />
                    <br />
                   
                    <label>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </label>
                    <br />
                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="grdSupport" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." DataKeyNames="SupportId" OnPageIndexChanging="grdSupport_PageIndexChanging" Width="100%" PageSize="20" AllowPaging="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("SupportId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClientCode" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Defect Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWebsite" runat="server" Text='<%#Eval("DefectName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Error URL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManagerName" runat="server" Text='<%#Eval("ErrorURL")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Request Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestStatus" runat="server" Text='<%#Eval("RequestStatus").ToString()=="0"?"Open":"Closed"%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Request Date Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreationDate" runat="server" Text='<%#Eval("CreationDate")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reponse Date Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUpdationDate" runat="server" Text='<%#(!string.IsNullOrEmpty(Eval("ResponseDate").ToString())) ? Eval("ResponseDate"):"N/A"%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ViewButton" runat="server" CssClass="btn-link"
                                            Text="View" ToolTip="View" OnClick="ViewButton_Click" ></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="History">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ViewHistory" runat="server" CssClass="btn-link"
                                            Text="History" ToolTip="History" OnClick="btnHistory_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                               
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
       
</asp:Content>





