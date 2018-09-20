<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="TAFULL.aspx.cs" Inherits="MobiOcean.MDM.Web.TAFULL" %>

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
                    TA Full Details
                    

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
                <div class="content padding-top-none">
                    <div class="row" style="text-align: center">


                        <div class=" form">
                            <div class="form-group ">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label>
                                            User Name :
                                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>

                                    <div class="col-lg-3">
                                        <label>
                                            From Date :
                                                <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                    <div class="col-lg-3">
                                        <label>
                                            To Date :
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>


                                    <div class="col-lg-3">
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

                                <asp:TemplateField HeaderText="Log Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLogdt" runat="server" Text='<%#Eval("MLogdate")%>'></asp:Label>
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
                                        <asp:Label ID="lblApAmt" runat="server" Text='<%#Eval("IsApproved").ToString()=="0"?Eval("ApprovedAmt"):"0"%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approval">
                                    <ItemTemplate>
                                        <asp:Label ID="lblapprovalr" runat="server" Text='<%#Eval("IsApproved").ToString()=="1"?"Yes":"NO"%>'></asp:Label>
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
                                        <asp:Label ID="lblpaid" runat="server" Text='<%#Eval("IsPaid").ToString()=="1"?"Yes":"No"%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
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

                                <asp:TemplateField HeaderText="To Date Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTD" runat="server" Text='<%#Eval("ToDateTime")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Visited">
                                    <ItemTemplate>
                                        <asp:Label ID="lbVst" runat="server" Text='<%#Eval("IsVisited").ToString()=="1"?"Yes":"No"%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
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
                                        <asp:Label ID="lblDst" runat="server" Text='<%#string.IsNullOrEmpty(Eval("VTotalDistance").ToString())?"--":Eval("TotalDistance")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

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
                                <asp:TemplateField HeaderText="Latitude">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLat" runat="server" Text='<%#Eval("Latitude")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Longitude">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllongitude" runat="server" Text='<%#Eval("Longitude")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Location Create Date">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("locationcreatedatetime")%>'></asp:Label>
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
    </div>
    <script>
        function closepopup() {
            $find('MP1').hide();
        }

    </script>
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


