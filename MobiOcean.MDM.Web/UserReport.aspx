<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserReport.aspx.cs" Inherits="MobiOcean.MDM.Web.UserReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active div">

                <div class="profile1" style="margin: 0px;">
                    User Report                     

                    <div class="clearfix"></div>
                </div>

                <br />
                <br />
                <div class="row" style="text-align: center">

                    <div class=" form">

                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    By User Name :
                                                               <asp:TextBox ID="txtUser" runat="server" CssClass="form-control"></asp:TextBox>
                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    By Mobile No :
                                                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                                </label>
                            </div>
                        </div>

                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    <br />
                                    <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click" />
                                </label>
                            </div>
                        </div>


                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-12" style="text-align: center">
                        <div class="dataTables_length" id="datatable-editable_length">
                            <label>
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>


                <br />
                <div class="table-responsive">
                    <asp:GridView ID="grdUser" runat="server" DataKeyNames="UserId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                        PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        EmptyDataText="No record found." OnPageIndexChanging="grdUser_PageIndexChanging" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserCode" runat="server" Text='<%#Eval("UserCode").ToString()==""?"---":Eval("UserCode")%>'></asp:Label>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Email Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId").ToString()==""?"---":Eval("EmailId")%>'></asp:Label>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo").ToString()==""?"---":Eval("MobileNo")%>'></asp:Label>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created By">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreated" runat="server" Text='<%#Eval("CreatedByUserName").ToString()==""?"---":Eval("CreatedByUserName")%>'></asp:Label>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Creation Date Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreationDate" runat="server" Text='<%#MyFormat(Eval("CreationDate").ToString())%>'></asp:Label>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Updated By">
                                <ItemTemplate>
                                    <asp:Label ID="lblUpdated" runat="server" Text='<%#Eval("UpdatedByUserName").ToString()==""?"---":Eval("UpdatedByUserName")%>'></asp:Label>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Updation Date Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblUpdationDate" runat="server" Text='<%#Eval("UpdationDate").ToString()==""?"---":MyFormat(Eval("UpdationDate").ToString())%>'></asp:Label>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="Center" />
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

                <!-- train section -->




            </div>
        </div>
    </div>
</asp:Content>
