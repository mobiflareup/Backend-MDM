<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LTCollegeVisit.aspx.cs" Inherits="MobiOcean.MDM.Web.LTCollegeVisit" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active div">

                <div class="profile1">&nbsp;&nbsp;<%=Convert.ToInt32(Session["ClientId"].ToString()) == 399?"College":"Customer"%> Visit Report
                    
                    <asp:Button ID="Button1" runat="server" CssClass="btn btnd btncompt text-right pull-right" Text="Today Visit Report Excel" OnClick="btnExportToExcel_ClickTodayVisit"/>&nbsp;&nbsp;
                    &nbsp;&nbsp;<asp:Button ID="CreatePdf" runat="server" CssClass="btn btnd btncompt text-right pull-right" Text="Today Visit Report Pdf" OnClick="CreatePdf_Click"/>
                </div>
               
                <br />
                <div class="row" style="text-align: center">

                    <div class=" form">
                        <div class="form-group col-lg-12">

                            <div class="col-lg-4">
                                <label>
                                    By Employee Name :
                                                               <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                </label>
                            </div>
                            <div class="col-lg-4">
                                <label>
                                    By Employee Id :
                                                               <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control"></asp:TextBox>
                                </label>
                            </div>
                            <div class="col-lg-4">
                                <label>
                                    By <%=Convert.ToInt32(Session["ClientId"].ToString()) == 399?"College":"Customer"%> Name:
                                                               <%--<asp:TextBox ID="txtCollegeName" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlCollegeName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                </label>
                            </div>
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
                            <div class="col-lg-4">
                                <label>
                                    <br />
                                    <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click" />
                                </label>
                            </div>
                        </div>
                        <%-- <div class="form-group col-lg-4" style="vertical-align: middle">
                            <label>
                                <br />
                                <br />
                                <asp:Button ID="btnSrch1" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click" />
                            </label>
                        </div>--%>
                    </div>
                </div>
                <hr />
                <div class="row" style="text-align: center">

                    <div class=" form">
                <div class="form-group col-lg-12">
                    <div class="col-lg-4">
                    <label>Unique User Count</label>
                    <asp:Label ID="UserCount" runat="server" Text="" class="badge badge-primary"></asp:Label>
                    </div>
                    <div class="col-lg-4">
                    <label>Total Vist Count</label>
                    <asp:Label ID="VisitCount" runat="server" Text="" CssClass="badge badge-warning"></asp:Label>
                    </div>
                    <div class="col-lg-4">
                     <label>Unique College Count</label>
                    <asp:Label ID="CollegeCount" runat="server" Text="" CssClass="badge badge-success"></asp:Label>
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
                    <asp:GridView ID="grdUser" runat="server" DataKeyNames="LTCollegeVisitId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                        PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        EmptyDataText="No record found." Width="100%" OnPageIndexChanging="grdUser_PageIndexChanging" OnRowDataBound="grdUser_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("LTCollegeVisitId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IsInLocationManual" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsInLocationManual" runat="server" Text='<%#Eval("IsInLocationManuallyEntered")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IsOutLocationManual" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsOutLocationManual" runat="server" Text='<%#Eval("IsOutLocationManuallyEntered")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="College Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblcname" runat="server" Text='<%#Eval("Name").ToString()==""?"---":Eval("Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpId" runat="server" Text='<%#Eval("EmpCompanyId").ToString()==""?"---":Eval("EmpCompanyId")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="In Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblIn" runat="server" Text='<%#Eval("InTime").ToString()==""?"---":Eval("InTime")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Out Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblOut" runat="server" Text='<%#Eval("OutTime").ToString()==""?"---":Eval("OutTime")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="In LogDateTime">
                                <ItemTemplate>
                                    <asp:Label ID="lblInTime" runat="server" Text='<%#string.IsNullOrEmpty(Eval("InLogDateTime").ToString())?"---": Convert.ToDateTime(Eval("InLogDateTime").ToString()).ToString("dd-MMM-yyyy HH:mm") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Out LogDateTime">
                                <ItemTemplate>
                                    <asp:Label ID="lblOutTime" runat="server" Text='<%#string.IsNullOrEmpty(Eval("OutLogDateTime").ToString())?"---": Convert.ToDateTime(Eval("OutLogDateTime").ToString()).ToString("dd-MMM-yyyy HH:mm") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Location In">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocationIn" runat="server"></asp:Label>
                                    <div id="inloc" runat="server" visible="false">
                                        <asp:Label ID="lblLocationIn1" runat="server" Text='<%#Eval("InLocation")%>'
                                            Visible='<%#(Eval("InLocation").ToString()=="Location not found")?((Eval("InLatitude").ToString()=="0") || (Eval("InLongitude").ToString()=="0"))?true:false:true%>'></asp:Label>

                                        <asp:Label ID="isinmanual" runat="server" Text='<%#Convert.ToBoolean(Eval("IsInLocationManuallyEntered"))?" (Manually Entered)":""%>' ForeColor="Red"></asp:Label>
                                        <a id="jiraLink1" href='https://maps.google.com?q=<%#Eval("InLatitude")%>,<%#Eval("InLongitude")%>' target="_blank" style="display: <%#(Eval("InLocation").ToString()=="Location not found")?((string.IsNullOrEmpty(Eval("InLatitude").ToString())) || (string.IsNullOrEmpty(Eval("InLongitude").ToString())))?"none":"display":"none"%>">Check Location (<%#Eval("InLatitude")%>,<%#Eval("InLongitude")%>)</a>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Location Out">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocationOut" runat="server"></asp:Label>
                                    <div id="outloc" runat="server" visible="false">
                                        <asp:Label ID="lblLocationOut1" runat="server" Text='<%#string.IsNullOrEmpty(Eval("OutLocation").ToString())?"----":Eval("OutLocation").ToString()%>' Visible='<%#(Eval("OutLocation").ToString()=="Location not found")?((Eval("OutLatitude").ToString()=="0") || (Eval("OutLongitude").ToString()=="0"))?true:false:true%>'></asp:Label>
                                        <asp:Label ID="isoutmanual" runat="server" Text='<%#Convert.ToBoolean(Eval("IsOutLocationManuallyEntered"))?" (Manually Entered)":""%>' ForeColor="Red"></asp:Label>
                                        <a id="jiraLink" href='https://maps.google.com?q=<%#Eval("OutLatitude")%>,<%#Eval("OutLongitude")%>' target="_blank" style="display: <%#(Eval("OutLocation").ToString()=="Location not found")?((string.IsNullOrEmpty(Eval("OutLatitude").ToString())) || (string.IsNullOrEmpty(Eval("OutLongitude").ToString())))?"none":"display":"none"%>">Check Location (<%#Eval("OutLatitude")%>,<%#Eval("OutLongitude")%>)</a>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="In Verification">
                                <ItemTemplate>
                                    <asp:Label ID="lblInVerification" runat="server" Text='<%#Eval("InVerification").ToString() == "1" ? "Yes" : (CheckLatLong(Eval("cusomerlatitude").ToString(), Eval("customerlongitude").ToString()))? "Customer Location Not Available":"No"  %> ' ForeColor='<%#Eval("InVerification").ToString() == "1" ? System.Drawing.Color.Green : System.Drawing.Color.Red  %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Out Verification">
                                <ItemTemplate>
                                    <asp:Label ID="lblOutVerification" runat="server" Text='<%#!string.IsNullOrEmpty(Eval("OutVerification").ToString())?Eval("OutVerification").ToString() == "1" ? "Yes":(CheckLatLong(Eval("cusomerlatitude").ToString(), Eval("customerlongitude").ToString()))? "Customer Location Not Available":"No":"----"%>' ForeColor='<%#Eval("OutVerification").ToString() == "1" ? System.Drawing.Color.Green : System.Drawing.Color.Red  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnView" runat="server" OnClick="lnkbtnView_Click"><i class="fa fa-eye custom-table-fa"></i></asp:LinkButton>
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
                            <asp:Label ID="messageMail" runat="server" Text="Mail To :" Style="margin: 0px auto" ForeColor="Black"></asp:Label>
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
                    <asp:Button ID="btnExportToExcel" runat="server" CssClass="btn btnd btncompt" Text="Export To Excel" align="right" OnClick="btnExportToExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btnd btncompt" Text="Print" OnClick="btnPrint_Click" />
                    <asp:Button ID="btnSendtomail" runat="server" CssClass="btn btnd btncompt" Text="Send To Mail" OnClick="btnSendtomail_Click" />
                </div>


                <!-- train section -->


            </div>

        </div>
    </div>
    <asp:Button ID="dummybtnfreq" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpattach" runat="server" TargetControlID="dummybtnfreq" PopupControlID="Panel1" PopupDragHandleControlID="dragi1"
        CancelControlID="Close" BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="Panel1" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true" class="modal-lg modal-md modal-xs">
        <div class="modal-content">
            <div class="modal-header" id="dragi1">
                <asp:Button ID="Close" runat="server" CssClass="btn btnd btncompt" Text="Close" Style="display: none;" />
                <div class="col-lg-6 col-md-6 col-sm-6" style="text-align: left">
                    <h4 class="modal-title" id="myModalLabel1">View Attachment</h4>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6" style="text-align: right">
                    <asp:Button ID="btnclose" runat="server" CssClass="btn btnd btncompt" Text="X" Style="text-align: right" />
                </div>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lcname" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lename" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12" style="height: 500px; overflow: auto">
                        <div class="table-responsive">
                            <asp:GridView ID="Gdv" runat="server" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." OnRowCommand="Gdv_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPopId" runat="server" Text='<%#Eval("LTCollegeVisitRemarkId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LogDateTime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInTime1" runat="server" Text='<%#string.IsNullOrEmpty(Eval("LogDateTime").ToString())?"---": Convert.ToDateTime(Eval("LogDateTime").ToString()).ToString("dd-MMM-yyyy HH:mm") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrm" runat="server" Text='<%#Eval("remark").ToString()==""?"---":Eval("remark")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblimg" runat="server" CommandArgument='<%#Eval("imagepath")%>' Text='<%#string.IsNullOrEmpty(Eval("imagepath").ToString())?"---":"Download"%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>

            <div class="modal-footer">
                <%--<button type="button" class="btn btnd btncompt" data-dismiss="mpattach" aria-hidden="true">Close</button>--%>
            </div>
        </div>
    </asp:Panel>
    <script>
        function pageLoad(sender, args) {
            $("[id$=txtFrmDt],[id$=txtToDt]").datepick({
                dateFormat: 'dd-M-yyyy',
            });
            $('#style-3').scroll(function () {
                $("[id$=txtFrmDt],[id$=txtToDt]").datepick("hide");
            });
        }
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

    </script>
</asp:Content>

