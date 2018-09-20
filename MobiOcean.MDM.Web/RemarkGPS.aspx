<%@ Page Title="GPS Status" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="RemarkGPS.aspx.cs" Inherits="MobiOcean.MDM.Web.RemarkGPS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active div">

                <li class="profile1">&nbsp;&nbsp;GPS Status

                </li>
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

                        <div class="col-md-3">
                            <label>
                                By Name :
                                 <asp:TextBox ID="txtSrchUserName" runat="server" class="form-control"></asp:TextBox>
                            </label>
                        </div>

                        <div class="col-md-3">
                            <label>
                                By Email :
                              <asp:TextBox ID="txtSrchEmailId" runat="server" class="form-control"></asp:TextBox>
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
                            <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btnd btncompt" OnClick="btnSrch_Click" />

                        </div>

                    </div>


                </div>
                <br />
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
                    <asp:GridView ID="grdUsr" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None"
                        AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                        OnPageIndexChanging="grdUsr_PageIndexChanging" AllowPaging="true" DataKeyNames="UserId" PageSize="20" Width="100%">

                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Id">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUserCode" runat="server" Text='<%#Eval("UserCode")%>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Email Id">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId")%>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Wrap="true" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo")%>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                           

                            <asp:TemplateField HeaderText="Logged Date">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDateTime" runat="server" Text='<%#Convert.ToDateTime(Eval("LogDatetime")).ToString("dd MMM yy HH:mm")%>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GPS Status">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
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
                 </ContentTemplate>
        </asp:UpdatePanel>
    <center>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divProcessing">
                            <asp:Image runat="server" ID="progressImg2" ImageUrl="~/images/Processing.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </center>
        <script>
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrmDate]").datepick({
                    dateFormat: 'dd M yyyy'
                });
                $("[id$=txtToDate]").datepick({
                    dateFormat: 'dd M yyyy'
                });
                $('#style-3').scroll(function () {
                    $("[id$=txtFrmDate],[id$=txtToDate]").datepick("hide");
                });

            });

        }
    </script>
</asp:Content>

