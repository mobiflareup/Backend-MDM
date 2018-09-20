<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AttendanceReport.aspx.cs" Inherits="MobiOcean.MDM.Web.AttendanceReport1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <style>
    .zoom {
        -webkit-transition: all 0.35s ease-in-out;
        -moz-transition: all 0.35s ease-in-out;
        transition: all 0.35s ease-in-out;
        cursor: -webkit-zoom-in;
        cursor: -moz-zoom-in;
        cursor: zoom-in;
    }

        .zoom:hover,
        .zoom:active,
        .zoom:focus {
            /**adjust scale to desired size, add browser prefixes**/
            -ms-transform: scale(1.7);
            -moz-transform: scale(1.7);
            -webkit-transform: scale(1.7);
            -o-transform: scale(1.7);
            transform: scale(1.7);
            position: relative;
            z-index: 100;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <div class="bhoechie-tab-content active div">

                    <div class="profile1">&nbsp;&nbsp;Attendance Report
                        <div class="col-lg-3 pull-right"><a href="AttendanceType.aspx" class="btn btn-sky text-uppercase custom-add-profile pull-right">Attendance Management</a></div>
                    </div>

                    <br />

                    <div class="row" style="text-align:center">

                        <div class=" form">
                            <div class="form-group col-lg-8">

                                <div class="col-lg-6">
                                    <label>
                                        By Employee Name :
                                                               <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                    </label>
                                </div>
                                 <div class="col-lg-6">
                                    <label>
                                        By Employee Id :
                                                               <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                            
                                  <div class="col-lg-6">
                                        <label>
                                            From Date :
                                                                    <asp:TextBox ID="txtFrmDt" runat="server" class="form-control" ></asp:TextBox>                                       

                                        </label>
                                    </div>
                                  <div class="col-lg-6">
                                        <label>
                                            To Date :
                                                                    <asp:TextBox ID="txtToDt" runat="server" class="form-control" ></asp:TextBox>                                          
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group col-lg-4" style="vertical-align:middle">
                                    <label>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click"/>
                                    </label>
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
                        <asp:GridView ID="grdUser" runat="server" DataKeyNames="AttendanceId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                            PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No record found." Width="100%" OnRowDataBound="grdUser_RowDataBound" OnPageIndexChanging="grdUser_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("AttendanceId")%>'></asp:Label>                                    
                                    </ItemTemplate>
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
                                  <asp:TemplateField HeaderText="Attendance Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#Eval("AttendanceDate").ToString()==""?"---":Eval("AttendanceDate")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attendance In">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInTime" runat="server" Text='<%#string.IsNullOrEmpty(Eval("InDateTime").ToString())? Eval("InTime").ToString()==""?"---":Eval("InTime") : Convert.ToDateTime(Eval("InDateTime").ToString()).ToString("dd-MMM-yyyy HH:mm")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attendance Out">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOutTime" runat="server" Text='<%#string.IsNullOrEmpty(Eval("OutDateTime").ToString())?Eval("OutTime").ToString()==""?"---":Eval("OutTime") : Convert.ToDateTime(Eval("OutDateTime").ToString()).ToString("dd-MMM-yyyy HH:mm") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Location In">
                                    <ItemTemplate>
                                       <%-- <asp:Label ID="lblLocationIn" runat="server" Text='<%#Eval("InLocation").ToString()==""?"---":Eval("InLocation")%>'></asp:Label>--%>
                                        <asp:Label ID="lblLocationIn" runat="server" Text='<%#Eval("InLocation")%>' Visible='<%#(Eval("InLocation").ToString()=="Location not found")?((Eval("InLatitude").ToString()=="0") || (Eval("InLongitude").ToString()=="0"))?true:false:true%>'></asp:Label>
                                        <asp:Label ID="isinmanual" runat="server" Text='<%#Convert.ToBoolean(Eval("IsInLocationManuallyEntered"))?" (Manually Entered)":""%>' ForeColor="Red"></asp:Label>
                                    <a id="jiraLink1" href='https://maps.google.com?q=<%#Eval("InLatitude")%>,<%#Eval("InLongitude")%>' target="_blank" style="display: <%#(Eval("InLocation").ToString()=="Location not found")?((string.IsNullOrEmpty(Eval("InLatitude").ToString())) || (string.IsNullOrEmpty(Eval("InLongitude").ToString())))?"none":"display":"none"%>">Check Location (<%#Eval("InLatitude")%>,<%#Eval("InLongitude")%>)</a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Location Out">
                                    <ItemTemplate>
                                      <%--  <asp:Label ID="lblLocationOut" runat="server" Text='<%#Eval("OutLocation").ToString()==""?"---":Eval("OutLocation")%>'></asp:Label>--%>
                                         <asp:Label ID="lblLocationOut" runat="server" Text='<%#Eval("OutLocation")%>' Visible='<%#(Eval("OutLocation").ToString()=="Location not found")?((Eval("OutLatitude").ToString()=="0") || (Eval("OutLongitude").ToString()=="0"))?true:false:true%>'></asp:Label>
                                        <asp:Label ID="isoutmanual" runat="server" Text='<%#Convert.ToBoolean(Eval("IsOutLocationManuallyEntered"))?" (Manually Entered)":""%>' ForeColor="Red"></asp:Label>
                                    <a id="jiraLink" href='https://maps.google.com?q=<%#Eval("OutLatitude")%>,<%#Eval("OutLongitude")%>' target="_blank" style="display: <%#(Eval("OutLocation").ToString()=="Location not found")?((string.IsNullOrEmpty(Eval("OutLatitude").ToString())) || (string.IsNullOrEmpty(Eval("OutLongitude").ToString())))?"none":"display":"none"%>">Check Location (<%#Eval("OutLatitude")%>,<%#Eval("OutLongitude")%>)</a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Duration(HH:MM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDuration" runat="server" Text='<%#MyFormat(Eval("InTime"),Eval("OutTime"),Eval("InDateTime"),Eval("OutDateTime"))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attendance Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAttendanceStatus" runat="server" Visible="false" Text='<%#Eval("AttendanceStatus") %>'></asp:Label>
                                      <asp:DropDownList ID="ddlAttendanceStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAttendanceStatus_SelectedIndexChanged" AutoPostBack="true">
                                          <asp:ListItem Text="Full Day" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="Half Day" Value="2"></asp:ListItem>
                                          <asp:ListItem Text="Absent" Value="0"></asp:ListItem>
                                      </asp:DropDownList>
                                    </ItemTemplate>                                  
                                    <ItemStyle HorizontalAlign="Center" Width="115px" />
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="In Image">
                                <ItemTemplate>
                                    <asp:Image ID="InImage" runat="server"  class="zoom" ImageUrl='<%#((Eval("InImagePath")==null)||(Eval("InImagePath").ToString()==""))?"/Content/images/user.png":"/Files/Android_Files/"+Eval("InImagePath") %>' Style="width: 50px; height: 50px; border-radius: 50px;" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="115px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Out Image">
                                <ItemTemplate>
                                    <asp:Image ID="OutImage" runat="server"  class="zoom" ImageUrl='<%#((Eval("OutImagePath")==null)||(Eval("OutImagePath").ToString()==""))?"/Content/images/user.png":"/Files/Android_Files/"+Eval("OutImagePath") %>' Style="width: 50px; height: 50px; border-radius: 50px;" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="115px" />
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
