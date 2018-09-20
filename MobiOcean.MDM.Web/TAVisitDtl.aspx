<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="TAVisitDtl.aspx.cs" Inherits="MobiOcean.MDM.Web.TAVisitDtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active div">
                <div class="profile1" style="margin: 0px;">
                    Visit Details<a href="TAMaster.aspx" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-backward"></i>&nbsp;&nbsp;<span>Back</span></a>
                         <div class="clearfix"></div>
                </div>
                
                
                <br />
                <div class="row" style="text-align: center">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
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
                        <asp:GridView ID="tavisit" runat="server" DataKeyNames="VisitDetailId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                            PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No record found." OnPageIndexChanging="tavisit_PageIndexChanging" Width="100%">
                            <Columns>



                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("VisitDetailId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCName" runat="server" Text='<%#!string.IsNullOrEmpty(Eval("name").ToString())? Eval("name") : !string.IsNullOrEmpty(Eval("TempCustomer").ToString())?Eval("TempCustomer").ToString()+" (Temp)":"----"%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="From Date Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblfrdt" runat="server" Text='<%#Eval("FromDateTime")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Visit Date Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVD" runat="server" Text='<%#Eval("VisitDateTime")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <%--   <asp:TemplateField HeaderText=" Mobile No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMNo" runat="server" Text='<%#Eval("MobileNo")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="To Date Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTD" runat="server" Text='<%#Eval("ToDateTime")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Visited">
                                    <ItemTemplate>
                                        <asp:Label ID="lbVst" runat="server" Text='<%#Eval("IsVisited").ToString()=="1"?"Yes":"No"%>'></asp:Label>
                                        <%--<asp:TextBox ID="txtApAmt" runat="server" Text='<%#Eval("IsApproved").ToString()=="0"?Eval("ApprovedAmt"):"0"%>' Enabled='<%#Eval("IsApproved").ToString()=="0"?false:true%>'></asp:TextBox>--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <%--                                <asp:TemplateField HeaderText="Return">
                                    <ItemTemplate>
                                        <asp:Label ID="lblapprovalrRtn" runat="server" Text='<%#Eval("IsReturn").ToString()=="0"?"Yes":"NO"%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Proof">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl" runat="server" Text="----" Visible='<%#string.IsNullOrEmpty(Eval("FilePath").ToString())?true:false%>'></asp:Label>
                                        <asp:LinkButton ID="lnkDownload" Visible='<%#string.IsNullOrEmpty(Eval("FilePath").ToString())?false:true%>' Text="Download" CommandArgument='<%#Eval("FilePath")%>' runat="server" OnClick="lnkDownload_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRmk" runat="server" Text='<%#string.IsNullOrEmpty(Eval("Remark").ToString())?"--":Eval("Remark")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Distance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDst" runat="server" Text='<%#string.IsNullOrEmpty(Eval("TotalDistance").ToString())?"--":Eval("TotalDistance")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <%--                                <asp:TemplateField HeaderText="Rate Per KM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRPKM" runat="server" Text='<%#string.IsNullOrEmpty(Eval("RatePerKM").ToString())?"--":Eval("RatePerKM")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Claimed Travel Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCTA" runat="server" Text='<%#string.IsNullOrEmpty(Eval("ClaimedTravelAmt").ToString())?"--":Eval("ClaimedTravelAmt")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Mode of Travel">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCTAM" runat="server" Text='<%#string.IsNullOrEmpty(Eval("ModeOfTravel").ToString())?"--":Eval("ModeOfTravel")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="View Locations" Visible="false">
                                    <ItemTemplate>
                                        <asp:Button ID="btnViewLocation" runat="server" CssClass="btn btnd btncompt" Text="Locations"  OnClick="btnViewLocation_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <%--                                <asp:TemplateField HeaderText="Approved Travel Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblATA" runat="server" Text='<%#string.IsNullOrEmpty(Eval("ApprovedTravelAmt").ToString())?"--":Eval("ApprovedTravelAmt")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approver Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAremark" runat="server" Text='<%#string.IsNullOrEmpty(Eval("ApproverRemark").ToString())?"--":Eval("ApproverRemark")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
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
                

              
                <!-- train section -->
            </div>
        </div>
    </div>
</asp:Content>

