<%@ Page Title="Assigned Customer List" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AssignedCustomerList.aspx.cs" Inherits="MobiOcean.MDM.Web.AssignedCustomerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <asp:Label ID="HiddenField" runat="server" Visible="false"></asp:Label>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active div">
                <div class="profile1">
                    &nbsp;&nbsp;Assigned Customer List
                     <asp:Button ID="btnBack" runat="server" CssClass="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Back" OnClick="btnBack_Click"></asp:Button>
                    <asp:Button ID="btnAssignCustomer" runat="server" CssClass="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Assign Daily Customer" OnClick="btnAssignCustomer_Click"></asp:Button>
                </div>

                <br />
                <div class="row">
                    <div class="col-sm-6">
                        <asp:Label ID="lblusernamee" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                    </div>
                    <div class="col-sm-6" style="text-align: center">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>

                </div>

                <div class="row" style="text-align: center">

                    <div class=" form">

                        <div class="form-group ">

                            <div class="col-lg-3">
                                <label>
                                    From Date :
                                            <asp:TextBox ID="txtFrDt" runat="server" class="form-control"></asp:TextBox>
                                </label>
                            </div>
                            <div class="col-lg-3">
                                <label>
                                    To Date :
                                            <asp:TextBox ID="txtToDt" runat="server" class="form-control"></asp:TextBox>
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

                <div class="table-responsive" style="max-height: 550px; min-height: 200px; overflow: scroll;">
                    <asp:GridView ID="grdCustomer" runat="server" GridLines="None" class="table mGrid" AutoGenerateColumns="false" OnRowDataBound="grdCustomer_RowDataBound"
                        HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                        Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblAId" runat="server" Text='<%#Eval("AssignId") %>'></asp:Label>
                                    <asp:Label ID="lbldrop" runat="server" Text="0" Visible="false"></asp:Label>
                                    <asp:Label ID="lblFromTime" runat="server" Text='<%#Eval("AssignTime")%>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblCheckId" runat="server" Text="0" Visible="false"></asp:Label>
                                    <asp:CheckBox runat="server" ID="AchkRow_Parents" Checked="true" AutoPostBack="true" OnCheckedChanged="AchkRow_Parents_CheckedChanged" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" ID="AchkHeader_Parents" AutoPostBack="true" OnCheckedChanged="AchkHeader_Parents_CheckedChanged" Checked="true" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:Label ID="AlblCustName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assign Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblfrDate" runat="server" Text='<%#Eval("AssignDate")%>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Task Detail">
                                <ItemTemplate>
                                    <asp:Label ID="lbltask" runat="server" Visible="false" Text="0"></asp:Label>
                                    <asp:TextBox ID="txtTaskDetail" runat="server" Text='<%#Eval("TaskDetail")%>' TextMode="MultiLine" OnTextChanged="txtTaskDetail_TextChanged"></asp:TextBox>
                                    <%--<asp:Label ID="lblTaskDetail" runat="server" Text='<%#Eval("TaskDetail")%>'></asp:Label>--%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assign Time">
                                <ItemTemplate>

                                    <%--          <div class="col-sm-6">--%>
                                    
                                    <asp:DropDownList ID="ddlFromHour" runat="server" OnSelectedIndexChanged="ddlFromHour_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">HH</asp:ListItem>
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList><%--</label><label>--%>
                                    <asp:DropDownList ID="ddlFromMin" runat="server" OnSelectedIndexChanged="ddlFromMin_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">MM</asp:ListItem>
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>
                                        <asp:ListItem>59</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlFromHour" ValueToCompare="0" Operator="NotEqual" ValidationGroup="Save" Enabled="false" ErrorMessage="Hours Required!" ForeColor="Red"></asp:CompareValidator>
                                    <%--</label>--%>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlFromMin" ValueToCompare="0" Operator="NotEqual" ValidationGroup="Save" Enabled="false" ErrorMessage="Minutes Required!" ForeColor="Red"></asp:CompareValidator>
                                    <%--   </div>--%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IsAccept">
                                <ItemTemplate>
                                    <asp:Label ID="AlblApproval" runat="server" Text='<%#Eval("Approval")%>' Visible="false"></asp:Label>
                                    <asp:Image ID="AImage1" runat="server" Width="25px" Height="25px" ImageUrl='<%#(string.IsNullOrWhiteSpace(Eval("Approval").ToString())?"~/image/waiting.png":(Eval("Approval").ToString()=="2")?"~/image/waiting.png":(Eval("Approval").ToString()=="0")?"~/image/Check.png":"~/image/Reject.png")%>' ToolTip='<%#(string.IsNullOrWhiteSpace(Eval("Approval").ToString())?"Pending":(Eval("Approval").ToString()=="2")?"Pending":(Eval("Approval").ToString()=="0")?"Accept":"Reject")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="AlblReason" runat="server" Text='<%#(string.IsNullOrWhiteSpace(Eval("Reason").ToString())?"---":Eval("Reason"))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Visited?">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsVisited" runat="server" Text='<%#string.IsNullOrWhiteSpace(Eval("IsVisited").ToString())?"---":Eval("IsVisited").ToString()=="0"?"Yes":"No"%>' Visible="false"></asp:Label>
                                    <asp:Image ID="AImage2" runat="server" Width="25px" Height="25px" ImageUrl='<%#(string.IsNullOrWhiteSpace(Eval("IsVisited").ToString())?"~/image/waiting.png":(Eval("IsVisited").ToString()=="0")?"~/image/Check.png":"~/image/Reject.png")%>' ToolTip='<%#string.IsNullOrWhiteSpace(Eval("IsVisited").ToString())?"Pending":Eval("IsVisited").ToString()=="0"?"Yes":"No"%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Server Log Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblVstTime" runat="server" Text='<%#string.IsNullOrWhiteSpace(Eval("VisitedTime").ToString())?"---":Eval("VisitedTime")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Visit Log Time">
                                <ItemTemplate>
                                    <asp:Label ID="lbllog" runat="server" Text='<%#string.IsNullOrWhiteSpace(Eval("VisitedLogTime").ToString())?"---":Eval("VisitedLogTime")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Alert Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblsts" runat="server" Text='<%#string.IsNullOrWhiteSpace(Eval("AlertStatus").ToString())?"---":!Convert.ToBoolean(Eval("AlertStatus").ToString())?"Sent":"---"%>'></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Alert Sent Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblalertme" runat="server" Text='<%#string.IsNullOrWhiteSpace(Eval("AlertSentTime").ToString())?"---":Eval("AlertSentTime")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                    </asp:GridView>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnAssigned" runat="server" ValidationGroup="Save" CssClass="btn btnd btncompt" Text="Save" OnClick="btnAssigned_Click" />
                    &nbsp; &nbsp;
                 
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                </div>


            </div>
        </div>
    </div>

    <script>
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrDt]").datepick({
                    dateFormat: 'dd-M-yyyy'
                });
                $("[id$=txtToDt]").datepick({
                    dateFormat: 'dd-M-yyyy'
                });
            });
        }
    </script>
</asp:Content>
