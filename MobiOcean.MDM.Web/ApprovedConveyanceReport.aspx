<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ApprovedConveyanceReport.aspx.cs" Inherits="MobiOcean.MDM.Web.ApprovedConveyanceReport1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active div">
                <div class="profile1" style="margin: 0px;">
                    <span>Conveyance History</span>

                    <div class="clearfix"></div>
                </div>


                <br />


                <div class="row" style="text-align: center">

                    <div class=" form">

                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    By Employee Name :
                                                               <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                                </label>
                            </div>
                        </div>


                        <div class="form-group ">
                            <div class="col-lg-4">
                                <label>
                                    From Date :
                                                                    <asp:TextBox ID="txtFrmDt" runat="server" class="form-control"></asp:TextBox>

                                </label>
                            </div>
                            <div class="col-lg-4">
                                <label>
                                    To Date :
                                                                    <asp:TextBox ID="txtToDt" runat="server" class="form-control"></asp:TextBox>
                                </label>
                            </div>
                            <div class="col-lg-12">
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
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="table-responsive">
                    <asp:GridView ID="grdUser" runat="server" DataKeyNames="ApprovedConveyanceId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                        PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        EmptyDataText="No record found." Width="100%" OnPageIndexChanging="grdUser_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("ApprovedConveyanceId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("FromDate").ToString()==""?"---":Eval("FromDate")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblToTime" runat="server" Text='<%#Eval("ToDate").ToString()==""?"---":Eval("ToDate")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Distance(In KM)">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistance" runat="server" Text='<%#Eval("TotalDistance").ToString()==""?"---":Eval("TotalDistance")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Conveyance Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblConveyanceRate" runat="server" Text='<%#Eval("ConveyanceRate").ToString()==""?"---":Eval("ConveyanceRate")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("TotalAmount").ToString()==""?"---":Eval("TotalAmount","{0:0.00}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved By">
                                <ItemTemplate>
                                    <asp:Label ID="lblApprovedBy" runat="server" Text='<%#Eval("ApprovedByUserName").ToString()==""?"---":Eval("ApprovedByUserName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Details">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnDetails" runat="server" Text="Details" OnClick="lnkbtnDetails_Click"></asp:LinkButton>
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
                            <asp:Label ID="message1" runat="server" Text="Mail To :" Style="margin: 0px auto" ForeColor="Black"></asp:Label>
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
                            <asp:Button ID="Send1" runat="server" Text="Send" CssClass="btn btnd btncompt" OnClick="Send_Click" ValidationGroup="mailsend" />&nbsp;
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
    <asp:Button ID="dummy" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mp" runat="server" PopupControlID="pnlpoupup" PopupDragHandleControlID="dragi" TargetControlID="dummy" BackgroundCssClass="modalbackground" CancelControlID="btnclose"></asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="pnlpoupup" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" class="modal-lg modal-sm modal-md modal-xs" aria-hidden="true">
        <div class="modal-content">

            <div class="modal-header" id="dragi">
                <div class="col-lg-6 col-md-12 col-sm-12">
                    <h4 class="modal-title" id="myModalLabel">Conveyance Details</h4>
                </div>
                <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: right">
                    <asp:Button ID="btnclose" runat="server" Text="x" class="close btn btnd btncompt waves-effect waves-light" Style="text-align: right" />
                </div>

            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-sm-12" style="text-align: center">
                        <div class="dataTables_length">
                            <label>
                                <asp:Label ID="lblPopup" runat="server"></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-12" style="height: 300px; overflow: auto">
                        <div class="table-responsive">
                            <asp:GridView ID="Gdv" runat="server" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." OnRowCommand="Gdv_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl" runat="server" Text='<%#Eval("ConveyanceId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPopUpFlocation" runat="server" Text='<%#string.IsNullOrEmpty(Eval("FromLocation").ToString())?"---":Eval("FromLocation").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPopUpToLocation" runat="server" Text='<%#string.IsNullOrEmpty(Eval("ToLocation").ToString())?"---":Eval("ToLocation").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LogDateTime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLogDateTime" runat="server" Text='<%#string.IsNullOrEmpty(Eval("LogDateTime").ToString())?"---":Eval("LogDateTime").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distance (In KM)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPopUpDistance" runat="server" Text='<%#string.IsNullOrEmpty(Eval("Distance").ToString())?"---":Eval("Distance","{0:0.00}").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPopUpRemark" runat="server" Text='<%#string.IsNullOrEmpty(Eval("Remark").ToString())?"---":Eval("Remark").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image Path">
                                        <ItemTemplate>
                                            <%-- <asp:Label ID="lblPopUpImagePath" runat="server" Text='<%#Eval("ImagePath")%>'></asp:Label>--%>
                                            <asp:LinkButton ID="lnkbtnpopupimgdwn" runat="server" Text="Download" CommandArgument='<%#Eval("ImagePath")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <%--<PagerStyle HorizontalAlign = "Right" CssClass = "dataTables_paginate paging_simple_numbers pagination-ys" />--%>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>

            <div class="modal-footer">
                <%--<button type="button" class="btn btnd btncompt" data-dismiss="modal" aria-hidden="true">Close</button>--%>
            </div>
        </div>
    </asp:Panel>
    <script>
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrmDt],[id$=txtToDt]").datepick({
                    dateFormat: 'dd-M-yyyy'
                });
                $('#style-3').scroll(function () {
                    $("[id$=txtFrmDt],[id$=txtToDt]").datepick("hide");
                });
            });
        }
    </script>
</asp:Content>
