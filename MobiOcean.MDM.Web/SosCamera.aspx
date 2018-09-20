<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SosCamera.aspx.cs" Inherits="MobiOcean.MDM.Web.SosCamera" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active">
                <div class="profile1">&nbsp;&nbsp;SOS Camera</div>

                <div class="content padding-top-none">
                    <div class="row" style="text-align: center">


                        <div class=" form">
                            <div class="form-group ">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label>
                                            Person Name :
                                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>
                                            Contact No :
                                                <asp:TextBox ID="txtcontact" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>
                                            From Date :
                                                <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>
                                            To Date :
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>


                                    <div class="col-md-12">
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
                        <asp:GridView ID="sosCam" runat="server" DataKeyNames="CameraId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                            PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No record found." OnPageIndexChanging="sosCam_PageIndexChanging" Width="100%">
                            <Columns>



                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("CameraId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="UserId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Person Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUname" runat="server" Text='<%#Eval("PersonName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Contact No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMNo" runat="server" Text='<%#Eval("ContactNo")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Latitude">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLat" runat="server" Text='<%#Eval("Latitude")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Longitude">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLong" runat="server" Text='<%#Eval("Longitude")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Log Date Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLogdt" runat="server" Text='<%#Convert.ToDateTime(Eval("LogDateTime")).ToString("dd-MMM-yyyy HH:mm")%>'></asp:Label>
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
                    </div>
                    <div class="row" style="text-align: right">
                        <asp:Button ID="btnsavetopdf" runat="server" CssClass="btn btnd btncompt" Text="Save To Pdf" align="right" OnClick="btnsavetopdf_Click" />
                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btnd btncompt" Text="Print" OnClick="btnPrint_Click" />
                        <asp:Button ID="btnSendtomail" runat="server" CssClass="btn btnd btncompt" Text="Send to Mail" OnClick="btnSendtomail_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="dummypopupbtn" runat="server" Style="display: none;" />
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
