<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="TAOthersDtl.aspx.cs" Inherits="MobiOcean.MDM.Web.TAOthersDtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active div">

                <div class="profile1" style="margin: 0px;">
                    TA Extra Charge<a href="TAMaster.aspx" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-backward"></i>&nbsp;&nbsp;<span>Back</span></a>
                    <div class="clearfix"></div>
                </div>

                <br />
                <div class="row" style="text-align: center">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <br>
                <div class="panel-body table-rep-plugin">
                    <div class=" form">
                        <div class="col-md-12">
                            <div class="form-group col-md-6">
                                <label for="Client" class="control-label col-md-5">User Name : </label>
                                <div class="col-md-7 pull-left">
                                    <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="lblname" class="control-label col-md-5">Log Date : </label>
                                <div class="col-md-7 pull-left">
                                    <asp:Label ID="lblLogDt" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />

                <div class="table-responsive">
                    <asp:GridView ID="taExtra" runat="server" DataKeyNames="ExtraDetailId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                        PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        EmptyDataText="No record found." OnPageIndexChanging="taExtra_PageIndexChanging" Width="100%">
                        <Columns>

                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("ExtraDetailId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblRmk" runat="server" Text='<%#string.IsNullOrEmpty(Eval("Remark").ToString())?"--":Eval("Remark")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Claimed Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblCTA" runat="server" Text='<%#string.IsNullOrEmpty(Eval("ClaimedAmt").ToString())?"--":Eval("ClaimedAmt")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Creation Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbllogdt" runat="server" Text='<%#Convert.ToDateTime(Eval("CreationDateTime")).ToString("dd-MMM-yyyy HH:mm")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Proof">
                                <ItemTemplate>
                                    <asp:Label ID="lblProof" runat="server" Text="----" Visible='<%#string.IsNullOrEmpty(Eval("FilePath").ToString())?true:false%>'></asp:Label>
                                    <asp:LinkButton ID="lnkDownload" Visible='<%#string.IsNullOrEmpty(Eval("FilePath").ToString())?false:true%>' Text="Download" CommandArgument='<%#Eval("FilePath")%>' runat="server" OnClick="lnkDownload_Click"></asp:LinkButton>
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
                    <br />
                    <!-- train section -->
                </div>
                <div class="row" style="text-align: right">
                    <asp:Button ID="btnsavetopdf" runat="server" CssClass="btn btnd btncompt" Text="Save To Pdf" align="right" OnClick="btnsavetopdf_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btnd btncompt" Text="Print" OnClick="btnPrint_Click" />
                    <asp:Button ID="btnSendtomail" runat="server" CssClass="btn btnd btncompt" Text="Send to Mail" OnClick="btnSendtomail_Click" />
                </div>
            </div>

            <br />
            <!-- train section -->

        </div>
    </div>
</asp:Content>

