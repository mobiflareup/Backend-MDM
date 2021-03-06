﻿<%@ Page Title="Remote Trigger Report" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="Trigger.aspx.cs" Inherits="MobiOcean.MDM.Web.Trigger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
    <style type="text/css">
        td.actions {
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">


    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">
                <div class="profile1" style="margin: 0px;">
                    Remote Trigger Report
                         <div class="clearfix"></div>
                </div>


                <div class="row">
                    <div class="col-sm-12" style="text-align: ">
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
                            <div class="col-lg-8">
                                <div class="col-lg-6">
                                    <label>
                                        By Device / User Name :
                                                                   <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                    </label>
                                </div>
                                <div class="col-lg-6">
                                    <label>
                                        By Trigger Type :
                                                                    <asp:TextBox ID="txtTriggerType" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-lg-6">
                                    <label>
                                        From Date :
                                                                    <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%-- <asp:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFrmDate" Format="dd MMM yyyy" PopupButtonID="txtFrmDate" />--%>
                                    </label>
                                </div>
                                <div class="col-lg-6">
                                    <label>
                                        To Date :
                                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:CalendarExtender ID="ce2" runat="server" TargetControlID="txtToDate" Format="dd MMM yyyy" PopupButtonID="txtToDate" />--%>
                                    </label>
                                </div>

                            </div>

                            <%--<div class="col-lg-4">
                                                                    <br />
                                                                </div>--%>
                            <div class="col-lg-4">
                                <br />
                                <br />
                                <br />
                                <label>
                                    <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                </label>
                            </div>
                            <div class="col-lg-12">
                                <br />
                            </div>
                        </div>
                    </div>
                </div>

                <br />
                <hr />
                <br />


                <div class="table-responsive">


                    <asp:GridView ID="grdTrigger" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" OnPageIndexChanging="grdTrigger_PageIndexChanging" AllowPaging="true" PageSize="20">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("TriggerId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trigger Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblTriggerType" runat="server" Text='<%#Eval("TriggerType")%>'></asp:Label>

                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trigged By (User Name)">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>

                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trigger To (User Name)">
                                <ItemTemplate>
                                    <asp:Label ID="lblTriggerToUserName" runat="server" Text='<%#Eval("UName").ToString()==""?"---":Eval("UName")%>'></asp:Label>

                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trigger To (Device Name)">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName").ToString()==""?"---":Eval("DeviceName")%>'></asp:Label>

                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trigger Date Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblTriggerTime" runat="server" Text='<%#Eval("TriggerTime")%>'></asp:Label>

                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                    </asp:GridView>
                </div>
                <div class="row" style="text-align: right">
                    <asp:Panel runat="server" ID="MessagePnl" Height="160px" CssClass="msgpopup" Visible="false">

                        <div class="modal-body" style="text-align: center; color: green;">
                            <asp:Button ID="btnccl" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
                            <asp:RadioButton ID="RbtnYou" GroupName="Group1" Text="Send To Yourself" Value="Yes" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;
                                    <asp:RadioButton ID="RbtnOther" GroupName="Group1" Text="Send To Other" Value="No" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                            <br />
                            <asp:Label ID="lblmessage" runat="server" Text="Mail To :" Style="margin: 0px auto" ForeColor="Black"></asp:Label>
                            <asp:TextBox ID="txtMailTo" runat="server" ForeColor="Black"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server"
                                ControlToValidate="txtMailTo" ErrorMessage="Required!" ValidationGroup="mailsend"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                ControlToValidate="txtMailTo" Display="Dynamic" ErrorMessage="Enter Valid Email-Id"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="mailsend" ForeColor="Red"></asp:RegularExpressionValidator>
                            <br />
                            <asp:Label ID="lblerrorMailTo" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer" style="text-align: center;">
                            <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btnd btncompt" OnClick="Send_Click" ValidationGroup="mailsend" />&nbsp;
                            <asp:Button ID="CancelMail" runat="server" CssClass="btn btnd btncompt" Text="Cancel" OnClick="CancelMail_Click" />
                        </div>


                    </asp:Panel>
                </div>
                <div class="row" style="text-align: right">
                    <asp:Button ID="btnsavetopdf" runat="server" CssClass="btn btnd btncompt" Text="Save To PDF" align="right" OnClick="btnsavetopdf_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btnd btncompt" Text="Print" OnClick="btnPrint_Click" />
                    <asp:Button ID="btnSendtomail" runat="server" CssClass="btn btnd btncompt" Text="Send To Mail" OnClick="btnSendtomail_Click" />
                </div>
            </div>
        </div>
    </div>
    <script>
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrmDate],[id$=txtToDate]").datepick({
                    dateFormat: 'dd M yyyy'
                });
                $('#style-3').scroll(function () {
                    $("[id$=txtFrmDate],[id$=txtToDate]").datepick("hide");
                });
            });
        }
    </script>
</asp:Content>



