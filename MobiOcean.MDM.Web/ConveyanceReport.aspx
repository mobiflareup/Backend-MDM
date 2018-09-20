<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConveyanceReport.aspx.cs" Inherits="MobiOcean.MDM.Web.ConveyanceReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active div">

                <div class="profile1" style="margin: 0px;">
                    <span>&nbsp;&nbsp;Conveyance Report</span>
                    <asp:Button ID="Button1" runat="server" Text="Conveyance History" CssClass="btn btn-sky text-uppercase custom-add-profile pull-right" OnClick="btnhstry_Click" />
                    <div class="clearfix"></div>
                </div>



                <div class="row" style="text-align: center">

                    <div class=" form">

                        <div class="form-group ">

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
                                    By Mobile No :
                                                               <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
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
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>
                <br />
               <%-- <div class="row">

                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                            </div>
                            <div class="panel-body">
                                <div id="map_canvas"></div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="table-responsive">
                    <asp:GridView ID="grdUser" runat="server" DataKeyNames="ConveyanceId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                        AllowPaging="false" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="grdUser_RowDataBound" ShowHeaderWhenEmpty="true"
                        EmptyDataText="No record found." Width="100%" OnRowCommand="grdUser_RowCommand"
                        OnRowCreated="grdUser_RowCreated" OnRowCancelingEdit="grdUser_RowCancelingEdit" OnRowEditing="grdUser_RowEditing" OnRowUpdating="grdUser_RowUpdating">
                        <Columns>

                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("ConveyanceId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpId" runat="server" Text='<%#Eval("EmpCompanyId").ToString()==""?"---":Eval("EmpCompanyId")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>

                                    <div style="text-align: center">
                                        <asp:Label ID="lbltotaldistance1" Text="Grand Total Distance(In KM) : " runat="server"></asp:Label>
                                        <asp:Label ID="lblTotalDistanceG" runat="server"></asp:Label>
                                    </div>
                                    <div style="text-align: center">
                                        <asp:Label ID="Label2" Text="Total Conveyance : " runat="server"></asp:Label>
                                        <asp:Label ID="lblConRt" runat="server"></asp:Label>
                                    </div>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("UserId").ToString()==""?"---":Eval("UserId")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From Date">
                                <ItemTemplate>
                                     <div style="width:75px;">
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("LogDateTime").ToString()==""?"---":Eval("LogDateTime")%>'></asp:Label>
                                         </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Date">
                                <ItemTemplate>
                                      <div style="width:75px;">
                                    <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("ToLogDateTime").ToString()==""?"---":Eval("ToLogDateTime","{0:dd-MMM-yyyy HH:MM}")%>'></asp:Label>
                                          </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From Location">
                                <ItemTemplate>
                                     <div style="width:150px;">
                                   <%-- <asp:Label ID="lblLocationIn" runat="server" Text='<%#Eval("FromLocation").ToString()==""?"---":Eval("FromLocation")%>'></asp:Label>--%>
                                          <asp:Label ID="lblLocationIn" runat="server" Text='<%#Eval("FromLocation")%>' Visible='<%#(Eval("FromLocation").ToString()=="Location not found")?((Eval("FromLatitude").ToString()=="0") || (Eval("FromLongitude").ToString()=="0"))?true:false:true%>'></asp:Label>
                                    <a id="jiraLink" href='https://maps.google.com?q=<%#Eval("FromLatitude")%>,<%#Eval("FromLongitude")%>' target="_blank" style="display: <%#(Eval("FromLocation").ToString()=="Location not found")?((string.IsNullOrEmpty(Eval("FromLatitude").ToString())) || (string.IsNullOrEmpty(Eval("FromLongitude").ToString())))?"none":"display":"none"%>">Check Location (<%#Eval("FromLatitude")%> , <%#Eval("FromLongitude")%>)</a>
                                         </div>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Location">
                                <ItemTemplate>
                                     <div style="width:150px;">
                                   <%-- <asp:Label ID="lblLocationOut" runat="server" Text='<%#Eval("ToLocation").ToString()==""?"---":Eval("ToLocation")%>'></asp:Label>--%>
                                          <asp:Label ID="lblLocationOut" runat="server" Text='<%#Eval("ToLocation")%>' Visible='<%#(Eval("ToLocation").ToString()=="Location not found")?((Eval("ToLatitude").ToString()=="0") || (Eval("ToLongitude").ToString()=="0"))?true:false:true%>'></asp:Label>
                                    <a id="jiraLink1" href='https://maps.google.com?q=<%#Eval("ToLatitude")%>,<%#Eval("FromLongitude")%>' target="_blank" style="display: <%#(Eval("ToLocation").ToString()=="Location not found")?((string.IsNullOrEmpty(Eval("ToLatitude").ToString())) || (string.IsNullOrEmpty(Eval("ToLongitude").ToString())))?"none":"display":"none"%>">Check Location (<%#Eval("ToLatitude")%> , <%#Eval("ToLongitude")%>)</a>
                                         </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Distance(In km)">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistance" runat="server" Text='<%#Eval("Distance").ToString()==""?"---":Eval("Distance","{0:0.00}")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDistance" runat="server" Text='<%#Eval("Distance").ToString()==""?"---":Eval("Distance","{0:0.00}") %>' ValidationGroup="Update"></asp:TextBox>
                                    <asp:RegularExpressionValidator ControlToValidate="txtDistance" ForeColor="Red" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Distance with 2 decimal precision" ValidationGroup="Update" ValidationExpression="\d+(\.\d{2})?|\.\d{2}"></asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remark").ToString()==""?"---":Eval("Remark")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Vehicle Start Reading">
                                <ItemTemplate>
                                     <div style="width:75px;">
                                    <asp:Label ID="lblvehiclestartreading" runat="server" Text='<%#Eval("VehicleStartReading").ToString()==""?"---":Eval("VehicleStartReading")%>'></asp:Label>
                                         </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Start Time Remark">
                                <ItemTemplate>
                                    <div style="width:75px;">
                                    <asp:Label ID="lblstarttimeremark" runat="server" Text='<%#Eval("StartTimeRemark").ToString()==""?"---":Eval("StartTimeRemark")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="Start Time Image">
                                <ItemTemplate>
                                    <div style="width:75px;">
                                      <asp:LinkButton ID="lblstarttimeimage" runat="server" CommandArgument='<%#Eval("StartTimeImage")%>' Text='<%#string.IsNullOrEmpty(Eval("StartTimeImage").ToString())?"---":"Download"%>'></asp:LinkButton>
                                        </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Vehicle Stop Reading">
                                <ItemTemplate>
                                    <div style="width:75px;">
                                    <asp:Label ID="lblvehiclestopreading" runat="server" Text='<%#Eval("VehicleStopReading").ToString()==""?"---":Eval("VehicleStopReading")%>'></asp:Label>
                                        </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Stop Time Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblstoptimeremark" runat="server" Text='<%#Eval("StopTimeRemark").ToString()==""?"---":Eval("StopTimeRemark")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Stop Time Image">
                                <ItemTemplate>
                                    <div style="width:75px;">
                                    <asp:LinkButton ID="lblstoptimeimage" runat="server" CommandArgument='<%#Eval("StopTimeImage")%>' Text='<%#string.IsNullOrEmpty(Eval("StopTimeImage").ToString())?"---":"Download"%>'></asp:LinkButton>
                                        </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Distance According To Vehicle Reading(In km)">
                                <ItemTemplate>
                                     <div style="width:150px;">
                                    <asp:Label ID="lblVehicleDistance" runat="server" Text='<%#Eval("VehicleReadingDistance").ToString()==""?"---":Eval("VehicleReadingDistance","{0:0.00}")%>'></asp:Label>
                                         </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Purpose And Proof Docs">
                                <ItemTemplate>
                                    <div style="width:150px;">
                                    <asp:LinkButton ID="lbPushprofile" runat="server" CommandArgument='<%#Eval("ImagePath")%>' Text='<%#string.IsNullOrEmpty(Eval("ImagePath").ToString())?"---":"Download"%>'></asp:LinkButton>
                                        </div>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Approved">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkboxRows_Parents" runat="server" Enabled='<%#Eval("ToLogDateTime").ToString()==""?false:true%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <div style="width:50px;">
                                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit"
                                        ToolTip="Edit"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnShowOnMap" runat="server" OnClick="lnkbtnShowOnMap_Click"><i class="fa fa-map-marker custom-table-fa" title="Show On Map"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnView" runat="server" OnClick="lnkbtnView_Click"><i class="fa fa-eye custom-table-fa" title="View"></i></asp:LinkButton>
                                         </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update"
                                        ToolTip="Update" ValidationGroup="Update"><i  class="fa fa-save"></i></asp:LinkButton>
                                    &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel"
                                                Text="Cancel" ToolTip="Canecl"><i  class="fa fa-close"></i></asp:LinkButton>
                                </EditItemTemplate>
                               
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>

                        <FooterStyle BackColor="#cccccc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Width="100%" />

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
                    <asp:Button ID="btnApprove" runat="server" CssClass="btn btnd btncompt" Text="Approve" align="right" OnClick="btnApprove_Click" Visible="false" />
                    <asp:Button ID="btnsavetopdf" runat="server" CssClass="btn btnd btncompt" Text="Save To PDF" align="right" OnClick="btnsavetopdf_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btnd btncompt" Text="Print" OnClick="btnPrint_Click" />
                    <asp:Button ID="btnSendtomail" runat="server" CssClass="btn btnd btncompt" Text="Send To Mail" OnClick="btnSendtomail_Click" />
                </div>


                <!-- train section -->


            </div>

        </div>
    </div>
      <asp:Button ID="dummybtndtl" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpdtl" runat="server" TargetControlID="dummybtndtl" PopupControlID="Panel1" PopupDragHandleControlID="dragi1"
        CancelControlID="Close" BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>


    <asp:Panel runat="server" ID="Panel1" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true" class="modal-lg modal-sm modal-md modal-xs">
        <div class="modal-content">

            <div class="modal-header" id="dragi1">

                <h4 class="modal-title" id="myModalLabel">Conveyance Details
                                <asp:Button ID="Close" runat="server" CssClass="close btn btnd btncompt waves-effect waves-light" Text="x" />

                </h4>

            </div>
            <div class="modal-body">
                <div class="row" style="height: 200px; overflow: auto">

                    <div class="col-sm-12">
                        <div class="table-responsive">
                            <div class="panel-body table-rep-plugin">
                                <div class="form">

                                    <div class="col-md-12">
                                        <div class="form-group col-md-6">
                                            <label for="Client" class="control-label col-md-5">Employee Id </label>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblEmpId" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEmail" class="control-label col-md-5">Employee Name</label>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblEmpName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblRole" class="control-label col-md-5">From Date </label>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblFromDate" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">To Date</label>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblToDate" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">From Location</label>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblFromLoc" runat="server"></asp:Label>
                                            </div>
                                             <br /> <br /> <br /> <br />
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">To Location</label>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblToLoc" runat="server"></asp:Label>
                                            </div>
                                               <br /> <br /> <br /> <br />
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">Vehicle Start Reading</label>
                                            <div class="col-md-7" style="padding-top: 15px;">
                                                <asp:Label ID="lblStartReading" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">Vehicle Stop Reading</label>
                                            <div class="col-md-7" style="padding-top: 15px;">
                                                <asp:Label ID="lblStopReading" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">Start Time Remark</label>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblStartRemark" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">Stop Time Remark </label>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblStopRemark" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                         <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">Distance According To Vehicle Reading</label>
                                            <div class="col-md-7" style="padding-top: 15px;">
                                                <asp:Label ID="lblReadingDistance" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">Distance </label>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblPopUpDistance" runat="server"></asp:Label>
                                            </div>
                                             <br /> <br /> <br />
                                        </div>
                                       
                                          <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">Remark </label>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblPopUpRemark" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>

                </div>
            </div>


            <div class="modal-footer">
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
