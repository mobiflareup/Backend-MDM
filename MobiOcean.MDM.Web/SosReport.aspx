<%@ Page Title="SOS Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="SosReport.aspx.cs" Inherits="MobiOcean.MDM.Web.SosReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <div class="bhoechie-tab-content active">

                <div class="profile1" style="margin: 0px;">
                    SOS Report
                         <div class="clearfix"></div>
                </div>



                <div class="row" style="text-align: center">


                    <div class=" form">
                        <div class="form-group ">
                            <div class="col-lg-8">
                                <div class="col-lg-6">
                                    <label>
                                        User :
                                                                   <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                    </label>
                                </div>
                                <div class="col-lg-6">
                                    <label>
                                        Device :
                                            <asp:TextBox ID="txtDeviceName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-lg-6">
                                    <label>
                                        From Date :
                                            <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-lg-6">
                                    <label>
                                        To Date :
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>

                            </div>

                            <div class="col-lg-4">
                                <br />
                                <br />
                                <label>
                                    <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />
                                </label>
                            </div>
                            <div class="col-lg-12">
                                <br />
                            </div>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col-sm-12" style="text-align: ">
                        <div class="dataTables_length" id="datatable-editable_length">
                            <label>
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>


                <div class="table-responsive">


                    <asp:GridView ID="grdclient" runat="server" DataKeyNames="DeviceLocId" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" OnPageIndexChanging="grdclient_PageIndexChanging" AllowPaging="true" PageSize="20">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("DeviceLocId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Device Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceId" runat="server" Text='<%#Eval("DeviceId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName").ToString()==""?"---":Eval("DeviceName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("MobileNo1")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Latitude" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLat" runat="server" Text='<%#Eval("Latitude")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Longitude" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLongi" runat="server" Text='<%#Eval("Longitude")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation1" runat="server" Text='<%#Eval("Location")%>' Visible='<%#(Eval("Location").ToString()=="Location not found")?((Eval("Latitude").ToString()=="0") || (Eval("Longitude").ToString()=="0"))?true:false:true%>'></asp:Label>
                                    <a id="jiraLink1" href='https://maps.google.com?q=<%#Eval("Latitude")%>,<%#Eval("Longitude")%>' target="_blank" style="display: <%#(Eval("Location").ToString()=="Location not found")?((string.IsNullOrEmpty(Eval("Latitude").ToString())) || (string.IsNullOrEmpty(Eval("Longitude").ToString())))?"none":"display":"none"%>">Check Location (<%#Eval("Latitude")%>,<%#Eval("Longitude")%>)</a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblLogDate" runat="server" Text='<%#Eval("LogDate")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblLogTime" runat="server" Text='<%#Eval("LogTime")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nearest Employee">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPopUp" runat="server" OnClick="lnkPopUp_Click"><i class="fa fa-user"></i></asp:LinkButton>
                                    <%if (ClientId == 264 || ClientId == 208)
                                      { %>
                                    <asp:LinkButton ID="lnkCamera" runat="server" OnClick="lnkCamera_Click"><img src="image/Security_Camera-512.png"></img></asp:LinkButton>
                                    <%} %>
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
    
    <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog popwidth">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title" id="myModalLabel">Nearest Employee</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive" data-pattern="priority-columns">
                                <asp:GridView ID="Gdv" runat="server" class="table table-small-font table-bordered table-striped mGrid"
                                    AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPopId" runat="server" Text='<%#Eval("SosSentId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPopUserId" runat="server" Text='<%#Eval("UserId")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblPopUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Device">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPopDeviceId" runat="server" Text='<%#Eval("DeviceId")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblPopDeviceName" runat="server" Text='<%#Eval("DeviceName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPopProfileName" runat="server" Text='<%#Eval("Location")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Distance(In K.M.)">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Distance")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Alert Sent?">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPopIsEnable" runat="server" Text='<%#Eval("IsAlertSend").ToString()=="1"?"Yes":"No"%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
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
</asp:Content>




