<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="CustomerDetails.aspx.cs" Inherits="MobiOcean.MDM.Web.CustomerDetails" %>

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
    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>--%>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active div">
                <div class="profile1">&nbsp;&nbsp;Customer Details</div>
                <br />
                <div class="row" style="text-align: center">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <div class="row" style="text-align: center">

                    <div class=" form">
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    Customer Name :
                                                               <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    Mobile No :
                                                                    <asp:TextBox ID="txtMblNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    Email Id :
                                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    Contact Person :
                                                                    <asp:TextBox ID="txtcontperson" runat="server" CssClass="form-control"></asp:TextBox>
                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    <br />
                                    <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click" />
                                </label>
                            </div>
                        </div>


                    </div>
                </div>
                <br/>
                <div class="tab-content">
                    <div class="table-responsive">
                        <asp:GridView ID="grdCustomer" runat="server" DataKeyNames="CustomerId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                            PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No record found." OnPageIndexChanging="grdCustomer_PageIndexChanging" Width="100%" OnRowDeleting="grdCustomer_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("CustomerId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCname" runat="server" Text='<%#string.IsNullOrEmpty(Eval("Name").ToString())?"---":Eval("Name").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Mobile No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMNo" runat="server" Text='<%#string.IsNullOrEmpty(Eval("MobileNo").ToString())?"---":Eval("CountryId1")+" "+Eval("MobileNo").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Email Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailid" runat="server" Text='<%#string.IsNullOrEmpty(Eval("EmailId").ToString())?"---":Eval("EmailId").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Address ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%#string.IsNullOrEmpty(Eval("Address").ToString())?"---":Eval("Address").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Contact Person">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContact" runat="server" Text='<%#string.IsNullOrEmpty(Eval("ContactPersion").ToString())?"---":Eval("ContactPersion").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TIN Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltinnumber" runat="server" Text='<%#string.IsNullOrEmpty(Eval("TinNumber").ToString())?"---":Eval("TinNumber").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Details">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPopUp" runat="server" OnClick="lnkPopUp_Click"><i class="fa fa-book custom-table-fa"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" OnClick="EditButton_Click"
                                            ToolTip="Edit"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>

                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="btn-link"
                                            ToolTip="Delete"><i class="fa fa-trash-o custom-table-fa"></i></asp:LinkButton>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                        </asp:GridView>

                    </div>
                </div>

                <br />
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

                <h4 class="modal-title" id="myModalLabel">Customer Detail
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
                                            <label for="Client" class="control-label col-md-5">Alternate Mobile No </label>
                                            <div class="col-md-7">
                                                <asp:Label ID="txtAltmobile" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEmail" class="control-label col-md-5">Alternate Contact Person </label>
                                            <div class="col-md-7">
                                                <asp:Label ID="txtaltcontactpersion" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblRole" class="control-label col-md-5">Alternate Email Id </label>
                                            <div class="col-md-7">
                                                <asp:Label ID="txtAltEmail" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">Lattitude</label>
                                            <div class="col-md-7">
                                                <asp:Label ID="txtLat" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">Longitude</label>
                                            <div class="col-md-7">
                                                <asp:Label ID="txtLong" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">City</label>
                                            <div class="col-md-7">
                                                <asp:Label ID="txtcity" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">District</label>
                                            <div class="col-md-7">
                                                <asp:Label ID="txtDist" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">State</label>
                                            <div class="col-md-7">
                                                <asp:Label ID="txtState" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">Country</label>
                                            <div class="col-md-7">
                                                <asp:Label ID="txtCountry" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="lblEId" class="control-label col-md-5">Pin Code </label>
                                            <div class="col-md-7">
                                                <asp:Label ID="txtPin" runat="server"></asp:Label>
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


    <asp:Button ID="dummypopupbtn" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpdelete" runat="server" PopupControlID="pnlpopup"
        TargetControlID="dummypopupbtn" CancelControlID="InvisibleNo"
        BackgroundCssClass="modalBackgroundTemp">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="150px" Width="400px">
        <table width="100%" style="border: Solid 2px; width: 100%; height: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold; background-color: #2a368b; color: #FFFFFF; height: 10px">
                    <asp:Label ID="lblalert" runat="server" Text="Alert" />
                    <asp:Label ID="lblalertprofileid" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold; background-color: #e5e5e5; color: #000000">
                    <asp:Label ID="lblUser" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td></td>
                <td align="right" style="padding-right: 15px; color: #000000; background-color: #e5e5e5;">
                    <asp:Button ID="Yes" runat="server" CssClass="btn btn-sm btnd btncompt" Text="OK" OnClick="Yes_Click" />
                    <asp:Button ID="No" runat="server" CssClass="btn btn-sm btn-warning" Text="Cancel" OnClick="No_Click" />
                    <asp:Button ID="InvisibleNo" runat="server" CssClass="btn btn-sm btn-warning" Text="Cancel" Style="display: none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <script>
        function closepopup() {
            $find('MP1').hide();
        }

    </script>
</asp:Content>

