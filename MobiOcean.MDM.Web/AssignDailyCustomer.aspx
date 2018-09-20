<%@ Page Title="Assign Daily Customer" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AssignDailyCustomer.aspx.cs" Inherits="MobiOcean.MDM.Web.AssignDailyCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">

                    <div class="bhoechie-tab-content active div">

                        <div class="profile1">
                            &nbsp;&nbsp;Assign Daily Customer
                              <asp:Button ID="btnBack" runat="server" CssClass="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Back" OnClick="btnBack_Click"></asp:Button>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6">
                                <asp:Label ID="lblusernamee" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                            </div>
                            <div class="col-sm-6" style="text-align: center">
                                <asp:Label ID="lblPopMsg" runat="server"></asp:Label>
                            </div>
                        </div>

                        <br />
                        <br />
                        <div class=" row">
                            <div class="col-md-12">
                                <div class="col-md-3">

                                    <asp:RadioButton ID="RDBAssigned" runat="server" GroupName="b" Checked="true" OnCheckedChanged="RDBAssigned_CheckedChanged" AutoPostBack="true" CssClass="col-md-4" />
                                    <label class="control-label col-md-8">Assigned Cutomers</label>
                                </div>

                                <div class="col-md-3">
                                    <asp:RadioButton ID="RDBAll" runat="server" GroupName="b" OnCheckedChanged="RDBAll_CheckedChanged" AutoPostBack="true" CssClass="col-md-4" />
                                    <label class="control-label col-md-8">All Customers</label>
                                </div>
                                <div class="col-md-2">
                                    <br />
                                </div>
                                <div class="col-md-2">
                                    <label class="control-label col-md-8">Date  : </label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtFrDt" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Select date" ControlToValidate="txtFrDt"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                        </div>

                        <div class="table-responsive" style="max-height: 550px; min-height: 200px; overflow: scroll;">
                            <asp:GridView ID="grdCustomer" runat="server" GridLines="None" class="table mGrid" AutoGenerateColumns="false"
                                HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                                Width="100%">
                                <%--// PageSize="10" AllowPaging="true"--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAId" runat="server" Text='<%#Bind("CustomerId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCheckId" runat="server" Text="0" Visible="false"></asp:Label>
                                            <asp:CheckBox runat="server" ID="AchkRow_Parents" AutoPostBack="true" OnCheckedChanged="AchkRow_Parents_CheckedChanged" />
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
                                    <asp:TemplateField HeaderText="Assign Time">
                                        <ItemTemplate>

                                            <%--<input type="text" id="time" runat="server" onselect="select(this)" onfocus="select(this);" onkeyup="select(this)" oninput="select(this)" onkeypress="select(this)"/>--%>
                                            <%-- <div class="col-sm-6">--%>
                                            <asp:Label ID="lbldrop" runat="server" Text="0" Visible="false"></asp:Label>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlFromHour" ValueToCompare="0" Operator="NotEqual" ValidationGroup="Save" Enabled="false" ErrorMessage="Hours Required!" ForeColor="Red"></asp:CompareValidator>
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
                                            <%--  </label>--%>

                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlFromMin" ValueToCompare="0" Operator="NotEqual" ValidationGroup="Save" Enabled="false" ErrorMessage="Minutes Required!" ForeColor="Red"></asp:CompareValidator>
                                            <%-- </div>--%>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Task Detail">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTaskDetail" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            <%--<textarea id="txtTaskDetail" rows="2" cols="10"></textarea>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                            </asp:GridView>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnAssigned" runat="server" ValidationGroup="Save" CssClass="btn btnd btncompt" Text="Save" OnClick="btnAssigned_Click" />
                            &nbsp; &nbsp;
                     <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt" Text="Cancel" OnClick="btnCancel_Click" />
                            <asp:Label ID="HiddenField" runat="server" Text="Label" Visible="false"></asp:Label>
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

    <script>
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrDt]").datepick({
                    dateFormat: 'dd-M-yyyy'
                });

            });

        }
    </script>
</asp:Content>
