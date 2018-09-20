<%@ Page Title="SMS Backup" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="UserSmsLog.aspx.cs" Inherits="MobiOcean.MDM.Web.UserSmsLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">


    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">

                <div class="profile1">&nbsp;&nbsp;SMS Backup</div>

                <div class="content padding-top-none">
                    <div class="row" style="text-align: center">
                        <div class=" form">
                            <div class="form-group ">
                                <div class="col-lg-8">
                                    <div class="col-lg-6">
                                        <label>
                                            By Direction : 
                                                 <asp:DropDownList ID="ddlDirection" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                     <asp:ListItem Text="--- All ---" Value="100"></asp:ListItem>
                                                     <asp:ListItem Text="Incoming" Value="1"></asp:ListItem>
                                                     <asp:ListItem Text="Outgoing" Value="0"></asp:ListItem>
                                                 </asp:DropDownList>

                                        </label>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>
                                            By Mobile No : 
                                                                   <asp:TextBox ID="txtSrchNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>
                                            From Date :
                                                <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>
                                            To Date :
                                                 <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                </div>

                                <div class="col-lg-4">
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
                        <asp:GridView ID="grdmsgh" runat="server" class="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" OnPageIndexChanging="grdmsgh_PageIndexChanging" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" PageSize="20" AllowPaging="true" OnRowDataBound="grdmsgh_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("LogId")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Device Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName").ToString()==""?"---":Eval("DeviceName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mobile No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumber" runat="server" Text='<%#Eval("MobileNo").ToString()==""?"---":Eval("MobileNo")%>'></asp:Label>

                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Log Date Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLogDateTime" runat="server" Text='<%#Eval("SMSDateTime")%>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Message Text">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMessageText" runat="server" Text='<%#Eval("SMS")%>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Direction">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDirection" runat="server" Text='<%#Eval("IsIncoming").ToString() == "1" ? "Incoming":"Outgoing" %>'>                                                                                                    
                                        </asp:Label>
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
    </div>
      <script>
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrmDate]").datepick({
                    dateFormat: 'dd M yyyy'
                });
                $("[id$=txtToDate]").datepick({
                    dateFormat: 'dd M yyyy'
                });
                $('#style-3').scroll(function () {
                    $("[id$=txtFrmDate],[id$=txtToDate]").datepick("hide");
                });

            });

        }
    </script>
</asp:Content>

