<%@ Page Title="Profile Update Detail" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="Updatedtl.aspx.cs" Inherits="MobiOcean.MDM.Web.Updatedtl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">

                <div class="profile1">&nbsp;&nbsp;Profile Update Detail</div>

                <!-- Start content -->
                <div class="row">
                    <div class="col-sm-12" style="text-align: center">
                        <div class="dataTables_length" id="datatable-editable_length">
                            <label>
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="content padding-top-none">


                    <div class="row" style="text-align: center">
                        <div class=" form">
                            <div class="form-group ">

                                <div class="col-lg-6">
                                    <label>
                                        By Device / User Name :
                                                                   <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <br />
                    <div class="table-responsive">

                        <asp:GridView ID="grdDept" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" OnPageIndexChanging="grdDept_PageIndexChanging" AllowPaging="true" PageSize="20">
                            <Columns>

                                <asp:TemplateField HeaderText="S.No" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbluserId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeptCode" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName").ToString()%>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Device Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeptName" runat="server" Text='<%#Eval("DeviceName").ToString()==""?"---":Eval("DeviceName").ToString()%>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactPerson" runat="server" Text='<%#Eval("AckDateTime")%>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtn" runat="server" Text="View More" OnClick="lnkbtn_click"></asp:LinkButton>
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
                </div>
            </div>
        </div>
    </div>

    <asp:Button ID="dummybtngeo" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="MP1" runat="server" TargetControlID="dummybtngeo" PopupControlID="Panel2" PopupDragHandleControlID="dragi2"
        CancelControlID="Closegeo" BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="Panel2" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true" class="modal-lg modal-md modal-sm modal-xs">
        <div class="modal-content">
            <div class="modal-header" id="dragi2">
                <h4 class="modal-title" id="myModalLabel2">
                    <label>Updation Detail</label>
                    <asp:Button ID="Closegeo" runat="server" CssClass="close btn btnd btncompt waves-effect waves-light" Text="x" Style="display: none;" />
                    <asp:Button ID="btnCloseGeo" runat="server" CssClass="close btn btnd btncompt waves-effect waves-light" Text="x" OnClick="btnClose_Click" />
                </h4>
            </div>

            <div class="modal-body">
                <div class="row" style="height: 300px; overflow: auto">
                    <div class="table-responsive">
                        <div class="row" style="text-align:center">
                            <asp:GridView ID="Gdv" runat="server" AutoGenerateColumns="false" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" AllowPaging="false" CssClass="table mGrid" HeaderStyle-CssClass="protable">
                                <Columns>
                                     <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRouteName" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Device Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("DeviceName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Time">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("AckDateTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>

    </asp:Panel>


</asp:Content>


