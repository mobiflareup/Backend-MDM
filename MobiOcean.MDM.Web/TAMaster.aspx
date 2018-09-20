<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="TAMaster.aspx.cs" Inherits="MobiOcean.MDM.Web.TAMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">


    <style type="text/css">
        .modalBackgroundTemp {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active">

                <div class="profile1" style="margin: 0px;">
                    TA Master
                         <div class="clearfix"></div>
                </div>

                <br />
                <div class="row" style="text-align: right" runat="server" id="divclientddl">
                    <div class="col-md-12">
                        <label style="text-align: center">
                            By Client :
                                                 <asp:DropDownList ID="dtClientId" runat="server" CssClass="form-control" AppendDataBoundItems="true" Style="color: black;" OnSelectedIndexChanged="dtClientId_SelectedIndexChanged" AutoPostBack="true">
                                                 </asp:DropDownList>
                        </label>
                    </div>
                </div>
                <br />

                <div class="row" style="text-align: center">


                    <div class=" form">
                        <div class="form-group ">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <label>
                                        User Name :
                                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>

                                <div class="col-lg-4">
                                    <label>
                                        From Date :
                                                <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-lg-4">
                                    <label>
                                        To Date :
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-lg-4">
                                    <label>
                                        Approved :
                                                <asp:DropDownList ID="ddlApproved" runat="server" AppendDataBoundItems="true" Width="125px" CssClass="form-control">
                                                    <asp:ListItem Text="--- Select ---" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                    </label>
                                </div>
                                <div class="col-lg-4">
                                    <label>
                                        Paid :
                                                <asp:DropDownList ID="ddlPaid" runat="server" AppendDataBoundItems="true" Width="125px" CssClass="form-control">
                                                    <asp:ListItem Text="--- Select ---" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                    </label>
                                </div>

                                <div class="col-lg-4">
                                    <br />
                                    <center>
                                                <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                          </center>
                                </div>
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


                <div class="table-responsive">
                    <asp:GridView ID="tamaster" runat="server" DataKeyNames="MasterId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                        PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        EmptyDataText="No record found." OnPageIndexChanging="tamaster_PageIndexChanging" Width="100%">
                        <Columns>



                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("MasterId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkboxRows_Parents" runat="server" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxHeader_Parents" runat="server" AutoPostBack="true" OnCheckedChanged="chkbox_CheckedChanged" />
                                </HeaderTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Id ">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpId" runat="server" Text='<%#Eval("EmpCompanyId")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUname" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <%--   <asp:TemplateField HeaderText=" Mobile No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMNo" runat="server" Text='<%#Eval("MobileNo")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Log Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblLogdt" runat="server" Text='<%#Eval("LogDate")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Distance">
                                <ItemTemplate>
                                    <asp:Label ID="lbltdistance" runat="server" Text='<%#Eval("TotalDistance")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Claimed Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblClaimamt" runat="server" Text='<%#Eval("ClaimedAmt")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Approved Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblApAmt" runat="server" Text='<%#Eval("IsApproved").ToString()=="1"?Eval("ApprovedAmt"):"0"%>'></asp:Label>
                                    <%--<asp:TextBox ID="txtApAmt" runat="server" Text='<%#Eval("IsApproved").ToString()=="0"?Eval("ApprovedAmt"):"0"%>' Enabled='<%#Eval("IsApproved").ToString()=="0"?false:true%>'></asp:TextBox>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Approval">
                                <ItemTemplate>
                                    <asp:Button ID="btnapproval" runat="server" CssClass="btn btnd btncompt" Text="Approve" Visible='<%#Eval("IsApproved").ToString()=="1"?false:true%>' OnClick="btnapproval_Click" />
                                    <asp:Label ID="lblapprovalr" runat="server" Text='<%#Eval("IsApproved").ToString()=="1"?"Yes":"NO"%>' Visible='<%#Eval("IsApproved").ToString()=="1"?true:false%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Approved By">
                                <ItemTemplate>
                                    <asp:Label ID="lblappBy" runat="server" Text='<%#string.IsNullOrEmpty(Eval("approvedby").ToString())?"--":Eval("approvedby")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Approver Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblappR" runat="server" Text='<%#string.IsNullOrEmpty(Eval("ApproverRemark").ToString())?"--":Eval("ApproverRemark")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Paid">
                                <ItemTemplate>
                                    <asp:Button ID="btnpaid" runat="server" CssClass="btn btnd btncompt" Text="Paid" Visible='<%#Eval("IsApproved").ToString()=="1"?Eval("IsPaid").ToString()=="1"?false:true:true%>' Enabled='<%#Eval("IsApproved").ToString()=="0"?false:Eval("IsPaid").ToString()=="0"?true:false%>' OnClick="btnpaid_Click" />
                                    <asp:Label ID="lblpaid" runat="server" Visible='<%#Eval("IsApproved").ToString()=="1"?Eval("IsPaid").ToString()=="1"?true:false:false%>' Text='<%#Eval("IsPaid").ToString()=="1"?"Yes":"No"%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Details">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDetails" runat="server" OnClick="lnkDetails_Click" Style="display: inline-block;" ToolTip="visit-details"><i><img src="image/visit-details.png" class="iconview"></i></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="LinkOthers" runat="server" OnClick="LinkOthers_Click" ToolTip="extra-charge"><i><img src="image/extra-charge.png" class="iconview"></i></asp:LinkButton>
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

        </div>
    </div>
    <asp:Button ID="dummypopupbtn" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlpopup"
        TargetControlID="dummypopupbtn" CancelControlID="btncancel"
        BackgroundCssClass="modalBackgroundTemp">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" ScrollBars="Auto" Style="background-color: white; height: 500px; position: fixed; z-index: 10003; left: 10%; right: 10%; top: 10%;">
        <asp:Label ID="lbltamasteid" runat="server" Visible="false"></asp:Label>

        <div class="modal-header">
            <asp:Button ID="btnclose" runat="server" Text="X" CssClass="close" OnClick="btnclose_Click" />
            <h4 class="modal-title" id="myModalLabel">Approval</h4>
        </div>
        <div class="modal-body">
            <div class="row">
                <div class="col-sm-12">
                    <div class="table-responsive">
                        <div class="panel-body table-rep-plugin">
                            <div class=" form">

                                <div class="col-md-12">
                                    <div class="form-group col-md-6">
                                        <label for="Client" class="control-label col-md-5">Employee Id :</label>
                                        <div class="col-md-7">
                                            <asp:Label ID="empid" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="Client" class="control-label col-md-5">User Name :</label>
                                        <div class="col-md-7">
                                            <asp:Label ID="usna" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="Client" class="control-label col-md-5">Log Date :</label>
                                        <div class="col-md-7">
                                            <asp:Label ID="logd" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="Client" class="control-label col-md-5">Claimed Amount :</label>
                                        <div class="col-md-7">
                                            <asp:Label ID="claimamout" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group col-md-12">
                                        <label for="Client" class="control-label col-md-5">Approved Amount :</label>
                                        <div class="col-md-7">
<%--                                            <asp:TextBox ID="txtapprovalamt" runat="server"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtapprovalamt" onkeypress="return isNumberKey(event,this)" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="lblEmail" class="control-label col-md-5">Approver Remark :</label>
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtappremark" runat="server" TextMode="MultiLine" Height="100px" Width="325px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <center>
                                                            <asp:Button runat="server" ID="Approved" CssClass="btn btnd btncompt" Text="Approved" OnClick="Approved_Click"></asp:Button>
                                                            <asp:Button runat="server" ID="approvedandpay" CssClass="btn btnd btncompt" Text="Approved and Pay" OnClick="Approvedandpay_Click"></asp:Button>
                                                        </center>
                                    </div>




                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btncancel" runat="server" Text="Close" CssClass="btn btnd btncompt" />
        </div>


    </asp:Panel>

    <script>
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrmDate],[id$=txtToDate]").datepick({
                    dateFormat: 'dd-M-yyyy'
                });
                $('#style-3').scroll(function () {
                    $("[id$=txtFrmDate],[id$=txtToDate]").datepick("hide");
                });
            });
        }
    </script>
    <script>
        function closepopup() {
            $find('MP1').hide();
        }

    </script>
        <script type="text/javascript">
 
            function isNumberKey(evt, obj)
            {
 
            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

    </script>
    

</asp:Content>

