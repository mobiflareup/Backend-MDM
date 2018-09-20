<%@ Page Title="All Orders" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeBehind="OrderDetail.aspx.cs" Inherits="MobiOcean.MDM.Web.OrderDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHead" runat="Server">

    <style type="text/css">
        .modalBackgroundTemp {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">

            <div class="bhoechie-tab-content active div">
                <div class="profile1">&nbsp;&nbsp;All Orders</div>
                <br />
                <div class="row" style="text-align: center">
                    <div class=" form">
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    Employee :
                                                               <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    Payment Status :
                                                               <asp:DropDownList ID="ddlpayment" runat="server" AppendDataBoundItems="true"
                                                                   CssClass="form-control">
                                                                   <asp:ListItem Text="--- All ---" Value="100"></asp:ListItem>
                                                                   <asp:ListItem Text="Waiting" Value="1"></asp:ListItem>
                                                                   <asp:ListItem Text="Received" Value="2"></asp:ListItem>
                                                               </asp:DropDownList>
                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    Order Status :
                                                              <asp:DropDownList ID="ddlapprove" runat="server" AppendDataBoundItems="true"
                                                                  CssClass="form-control">
                                                                  <asp:ListItem Text="--- All ---" Value="100"></asp:ListItem>
                                                                  <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                                                  <asp:ListItem Text="Approved" Value="2"></asp:ListItem>
                                                                  <asp:ListItem Text="Cancelled" Value="3"></asp:ListItem>
                                                              </asp:DropDownList>
                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    Customer Name :
                                                              <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control"></asp:TextBox>
                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    From Date :
                                                              <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>

                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-4">
                                <label>
                                    To Date :
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>

                                </label>
                            </div>
                        </div>
                        <div class="form-group ">

                            <div class="col-lg-12">
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
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            </label>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-12" style="text-align: right">
                        <asp:Button ID="btnExcel" runat="server" CssClass="btn btnd btncompt" Text="Export to Excel" />
                    </div>
                </div>
                <br />
                <div class="table-responsive">
                    <asp:GridView ID="gdvStudent" runat="server" AutoGenerateColumns="False" EmptyDataText="Sorry! No Record found."
                        CssClass="table mGrid" DataKeyNames="OrderMasterId" PageSize="20" AllowPaging="True" ShowHeader="true" ShowHeaderWhenEmpty="true" HeaderStyle-CssClass="protable" GridLines="None" OnRowCancelingEdit="gdvStudent_RowCancelingEdit" OnRowDeleting="gdvStudent_RowDeleting" OnRowEditing="gdvStudent_RowEditing" OnRowUpdating="gdvStudent_RowUpdating" OnRowDataBound="gdvStudent_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("OrderMasterId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order No">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnordernumber" runat="server" CssClass="LinkBtn" Text='<%#Eval("OrderNo")%>' OnClick="lbtnordernumber_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbldealer" runat="server" Text='<%#Eval("CustomerName").ToString()==""?"---":Eval("CustomerName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Taken By">
                                <ItemTemplate>
                                    <asp:Label ID="lblusername" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Date" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblorderdate" runat="server" Text='<%#Eval("OrderDate").ToString()==""?"---":Eval("OrderDate","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expected Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblexpecteddate" runat="server" Text='<%#Eval("ExpectedDate").ToString()==""?"---":Eval("ExpectedDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Log Date Time">
                                <ItemTemplate>
                                    <asp:Label ID="lbllogdatetime" runat="server" Text='<%#Eval("LogDateTime").ToString()==""?"---":Eval("LogDateTime","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:Label ID="lbllocation" runat="server" Text='<%#Eval("Location").ToString()==""?"---":Eval("Location")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Total Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblttlamount" runat="server" Text='<%#Eval("TotalAmount").ToString()==""?"---":Eval("TotalAmount")%>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Status" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("OrderStatus")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblstatusstring1" runat="server" Text='<%#Eval("OrderStatus").ToString() == "1" ? "Pending" :Eval("OrderStatus").ToString() == "2" ? "Approved":" Cancelled"%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblEstatus" runat="server" Text='<%#Eval("OrderStatus")%>' Visible="false"></asp:Label>
                                    <asp:DropDownList ID="DDIsApproved" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">Pending</asp:ListItem>
                                        <asp:ListItem Value="2">Approved</asp:ListItem>
                                        <asp:ListItem Value="3">Cancelled</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Status" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblpaymentstatus" runat="server" Text='<%#Eval("IsPaymentReceived")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblpaymentstatusstring" runat="server" Text='<%#Eval("IsPaymentReceived").ToString() == "1" ? "Waiting" : "Received"%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblEpaymentstatus" runat="server" Text='<%#Eval("IsPaymentReceived")%>' Visible="false"></asp:Label>
                                    <asp:DropDownList ID="DDIspaymentreceived" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">Waiting</asp:ListItem>
                                        <asp:ListItem Value="2">Received</asp:ListItem>

                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action" Visible="true">
                                <ItemTemplate>

                                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" CssClass="LinkBtn"
                                        Text="Edit" ToolTip="Edit"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>
                                    &nbsp;
                                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="LinkBtn"
                                                Text="Delete" ToolTip="Delete"><i class="fa fa-trash-o custom-table-fa"></i></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="LinkBtn"
                                        Text="Update" ToolTip="Update" ValidationGroup="Update"><i  class="fa fa-save"></i></asp:LinkButton>
                                    &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel" CssClass="LinkBtn"
                                                Text="Cancel" ToolTip="Canecl"><i  class="fa fa-close"></i></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>

    <asp:Button ID="dummyBtn" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpeManFields" runat="server" TargetControlID="dummyBtn" PopupControlID="pnlPopUp" PopupDragHandleControlID="dragi2"
        CancelControlID="Closegeo" BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>

    <asp:Panel runat="server" ID="pnlPopUp" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true" class="modal-lg modal-md modal-sm modal-xs">

        <div class="modal-content">
            <div class="modal-header" id="dragi2">

                <h4 class="modal-title" id="myModalLabel2">Order Detail
                        
                    <asp:Button ID="btnCloseGeo" runat="server" CssClass="close btn btnd btncompt waves-effect waves-light" Text="x" />
                    <asp:Button ID="Closegeo" runat="server" CssClass="close btn btnd btncompt waves-effect waves-light" Text="x" Style="display: none" />
                </h4>

            </div>


            <div class="modal-body">
                <div class="row" style="height: 300px; overflow-y: auto">
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <b>Order No : </b>
                            <asp:Label runat="server" ID="lblPopStuInfo" Font-Bold="true" Font-Size="Large"></asp:Label>
                            <asp:Label runat="server" ID="lblPopStuId" Visible="false"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <b>Customer : </b>
                            <asp:Label ID="mplbldlr" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <b>Order Date : </b>
                            <asp:Label ID="mplblordrdate" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <b>Sales Person  : </b>
                            <asp:Label ID="mplblordrby" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <b>Approved / Rejected By : </b>
                            <asp:Label runat="server" ID="mplblAprovedby" Visible="True"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <b>Status  : </b>
                            <asp:Label runat="server" ID="mplblarstatus" Visible="True"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-12">


                        <div class="col-md-4">
                            <b>Total Amount :</b>
                            <asp:Label runat="server" ID="mplblttlamount" Visible="True"></asp:Label>
                        </div>
                        <br />
                    </div>


                    <div class="col-md-12" style="text-align: center">
                        <asp:Label ID="lblMsgPop" runat="server"></asp:Label>
                        <asp:Label ID="lblname" runat="server" Visible="false"></asp:Label>
                    </div>
                    <br />

                    <div class="col-md-12">

                        <div class="table-responsive">
                            <asp:GridView ID="gdvSelectBin" runat="server" AutoGenerateColumns="False"
                                EmptyDataText="No data found" Width="100%" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" OnRowCancelingEdit="gdvSelectBin_RowCancelingEdit" OnRowDeleting="gdvSelectBin_RowDeleting" OnRowEditing="gdvSelectBin_RowEditing" OnRowUpdating="gdvSelectBin_RowUpdating">
                                <AlternatingRowStyle CssClass="alt" />
                                <FooterStyle CssClass="footer" />
                                <Columns>

                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Detail Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderDetailId" runat="server" Text='<%#Eval("OrderDetailId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="mplblItemName" runat="server" Text='<%#Eval("ItemName").ToString()==""?"---":Eval("ItemName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Content">
                                        <ItemTemplate>
                                            <asp:Label ID="mplblitem" runat="server" Text='<%#Eval("ItemContent").ToString()==""?"---":Eval("ItemContent")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manufacturer Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmanufacturer" runat="server" Text='<%#Eval("ManufacturerName").ToString()==""?"---":Eval("ManufacturerName")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="mplblQuantity" runat="server" Text='<%#Eval("OrderQuantity").ToString()==""?"---":Eval("OrderQuantity")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="mplblQuantity1" runat="server" Text='<%#Eval("OrderQuantity")%>' Visible="false"></asp:Label>
                                            <asp:TextBox ID="mptxtQuantity" runat="server" CssClass="form-control" Text='<%#Eval("OrderQuantity")%>'></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="fte" runat="server" TargetControlID="mptxtQuantity" FilterType="Numbers" />
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock">
                                        <ItemTemplate>
                                            <asp:Label ID="mplblstock" runat="server" Text='<%#Eval("Stock").ToString()==""?"---":Eval("Stock")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Packing">
                                        <ItemTemplate>
                                            <asp:Label ID="mplblpacking" runat="server" Text='<%#Eval("Packing").ToString()==""?"---":Eval("Packing")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Schema">
                                        <ItemTemplate>
                                            <asp:Label ID="mplblscheme" runat="server" Text='<%#Eval("Scheme").ToString()==""?"---":Eval("Scheme")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sales Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="mplblsales" runat="server" Text='<%#Eval("SalesRate").ToString()==""?"---":Eval("SalesRate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("MRP").ToString()==""?"---":Eval("MRP")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" Visible="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="mpEditButton" runat="server" CommandName="Edit" CssClass="LinkBtn"
                                                Text="Edit" ToolTip="Edit"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="mpDeleteButton" runat="server" CommandName="Delete" CssClass="LinkBtn"
                                                Text="Delete" ToolTip="Delete"><i class="fa fa-trash-o custom-table-fa"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="mpUpdateButton" runat="server" CommandName="Update" CssClass="LinkBtn"
                                                Text="Update" ToolTip="Update" ValidationGroup="Update"><i  class="fa fa-save"></i></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="mpCancelUpdateButton" runat="server" CommandName="Cancel" CssClass="LinkBtn"
                                                Text="Cancel" ToolTip="Canecl"><i  class="fa fa-close"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>

            <div class="modal-footer">
                <div class="col-md-12 text-center">
                    <asp:Button runat="server" ID="btnAprove" CssClass="btn btnd btncompt"
                        Text="Approve" Visible="false" />
                    &nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnSynckCancel" CssClass="btn btnd btncompt" Text="Back" />
                    &nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnReject" CssClass="btn btnd btncompt" Text="Reject" Visible="false" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="dummypopupbtn" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpdelete" runat="server" PopupControlID="pnldelete"
        TargetControlID="dummypopupbtn" CancelControlID="InvisibleNo"
        BackgroundCssClass="modalBackgroundTemp">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnldelete" runat="server" BackColor="White" Height="150px" Width="400px">
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
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFrmDate]").datepick({
                    dateFormat: 'dd-M-yyyy'
                });
                $("[id$=txtToDate]").datepick({
                    dateFormat: 'dd-M-yyyy'
                });
                $('#style-3').scroll(function () {
                    $("[id$=txtFrmDate],[id$=txtToDate]").datepick("hide");
                });

            });

        }
    </script>
</asp:Content>
