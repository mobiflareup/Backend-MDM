<%@ Page Title="Location Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="UserLocationList.aspx.cs" Inherits="MobiOcean.MDM.Web.UserLocationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">

                <div class="profile1" style="margin: 0px;">
                    Location Report
                        <a href="DeviceLocation.aspx" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-eye custom-table-fa"></i>&nbsp;&nbsp;<span>Show On Map</span></a>
                    <a href="UserCurrentLocation.aspx" id="flip" class="btn btn-sky text-uppercase custom-add-profile pull-right"><i class="fa fa-map-marker custom-table-fa"></i>&nbsp;&nbsp;<span>Last Locations</span></a>

                    



                    <div class="clearfix"></div>
                </div>

                <br />

                <br />
                <!-- Start content -->





                <div class="row" style="text-align: center">
                    <div class=" form">

                        <div class="form-group ">
                            <div class="col-lg-8">
                                <div class="col-lg-6">
                                    <label>
                                        By User :
                                                                   <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                    </label>
                                </div>
                                <div class="col-lg-6">
                                    <label>
                                        By Device :
                                                                    
                                                <asp:TextBox ID="txtDeviceName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-lg-6">
                                    <label>
                                        From Date :
                                                                    <%--<asp:DropDownList ID="ddlProfileName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>--%>
                                        <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFrmDt" Format="dd MMM yyyy" PopupButtonID="txtFrmDt" />--%>
                                    </label>
                                </div>
                                <div class="col-lg-6">
                                    <label>
                                        To Date :
                                                                    <%--<asp:DropDownList ID="ddlProfileName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>--%>
                                        <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:CalendarExtender ID="ce2" runat="server" TargetControlID="txtToDt" Format="dd MMM yyyy" PopupButtonID="txtToDt" />--%>
                                    </label>
                                </div>
                            </div>

                            <%--<div class="col-lg-4">
                                                                    <br />
                                                                </div>--%>
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
                    <asp:GridView ID="grdUser" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false"
                        ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" OnPageIndexChanging="grdUser_PageIndexChanging" AllowPaging="true" PageSize="20">
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
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Device Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName").ToString()==""?"---":Eval("DeviceName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("MobileNo")%>'></asp:Label>
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
                                    <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location")%>' Visible='<%#(Eval("Location").ToString()=="Location not found")?((Eval("Latitude").ToString()=="0") || (Eval("Longitude").ToString()=="0"))?true:false:true%>'></asp:Label>
                                    <a id="jiraLink" href='https://maps.google.com?q=<%#Eval("Latitude")%>,<%#Eval("Longitude")%>' target="_blank" style="display: <%#(Eval("Location").ToString()=="Location not found")?((string.IsNullOrEmpty(Eval("Latitude").ToString())) || (string.IsNullOrEmpty(Eval("Longitude").ToString())))?"none":"display":"none"%>">Check Location (<%#Eval("Latitude")%>,<%#Eval("Longitude")%>)</a>
                                    <%--  <a runat="server" href="http://maps.google.com?q=" + <%#Eval("Latitude")%> + "," + <%#Eval("Longitude")%> +"  ></a>--%>
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
                            <asp:TemplateField HeaderText="Location Source">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocationSource" runat="server" Text='<%#Eval("LocationSource")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Called By">
                                <ItemTemplate>
                                    <asp:Label ID="lblSrvcCalledBy" runat="server" Text='<%#Eval("SrvcCalledBy")%>'></asp:Label>
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
                    <asp:Button ID="btnsavetopdf" runat="server" CssClass="btn btnd btncompt" Text="Save To PDF" OnClick="btnsavetopdf_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btnd btncompt" Text="Print" OnClick="btnPrint_Click" />
                    <asp:Button ID="btnSendtomail" runat="server" CssClass="btn btnd btncompt" Text="Send To Mail" OnClick="btnSendtomail_Click" />
                </div>

            </div>
        </div>
    </div>
    <script>
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrmDt],[id$=txtToDt]").datepick({
                    dateFormat: 'dd M yyyy'
                });
                $('#style-3').scroll(function () {
                    $("[id$=txtFrmDt],[id$=txtToDt]").datepick("hide");
                });
            });
        }
    </script>
</asp:Content>

