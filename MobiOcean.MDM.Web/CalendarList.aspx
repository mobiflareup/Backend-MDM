<%@ Page Title="Calendar List" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="CalendarList.aspx.cs" Inherits="MobiOcean.MDM.Web.CalendarList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">


    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <!-- flight section -->
        <div class="force-overflow">
            <div class="bhoechie-tab-content active">

                <div class="profile1">&nbsp;&nbsp;Calendar List</div>

                <div class="dataTables_length" id="datatable-editable_length">
                    <label>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>

                    </label>
                    </div>
                    <div class="content padding-top-none">


                        <div class="row" style="text-align: center">
                            <div class=" form">
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            Device/User Name :
                                                                   <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            Sync Date Time :
                                                                    <asp:DropDownList ID="ddlSyncDateTime" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            <br />
                                            <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                        </label>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <br />



                        <div class="table-responsive">

                            <asp:GridView ID="grdCalender" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" OnPageIndexChanging="grdCalender_PageIndexChanging" AllowPaging="true" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("CalenderSyncId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Device Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sync Date Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSyncName" runat="server" Text='<%#Eval("SyncDateTime")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Event Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEventName" runat="server" Text='<%#Eval("EventName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Start Date Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDateTime" runat="server" Text='<%#Eval("StartDateTime")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDateTime" runat="server" Text='<%#Eval("EndDateTime")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%#string.IsNullOrEmpty(Eval("Location").ToString()) ? "---" : Eval("Location")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                            </asp:GridView>
                        </div>
                    </div>
                
            </div>
        </div>
    </div>
</asp:Content>
