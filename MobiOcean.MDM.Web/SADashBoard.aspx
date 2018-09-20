<%@ Page Title="SA DashBoard" Language="C#" MasterPageFile="~/MasterPage.master" 
    CodeBehind="SADashBoard.aspx.cs" Inherits="MobiOcean.MDM.Web.SADashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" runat="Server">

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">

                    <!-- Start content -->
                    <div class="bhoechie-tab-content active">
                        <div class="profile1">
                   SuperAdmin Dashboard
                     <a href="addclient.aspx" class="btn btn-sky text-uppercase custom-add-profile pull-right">
                         <i class="fa fa-user-plus"></i> &nbsp;&nbsp;Add Client
                     </a>
                </div>
<br /><br />

                    <!-- Start content -->
                    <div class="content">
                        <div class="container">
                            <div class="panel">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-12">

                                            <div class="panel-body table-rep-plugin">
                                                <div class="table-responsive" data-pattern="priority-columns">

                                                    <asp:GridView ID="grdClient" class="table table-small-font table-bordered table-striped mGrid" runat="server" AllowPaging="true" OnPageIndexChanging="grdClient_PageIndexChanging" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" PageSize="20" EmptyDataText="No Data Found">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Clients">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClients" runat="server" Text='<%#Eval("ClientName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No. Of Users">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDevices" runat="server" Text='<%#Eval("NoOfUsers")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No. Of Devices">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUsers" runat="server" Text='<%#Eval("NoOfDevices")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No. Of Profiles">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprofiles" runat="server" Text='<%#Eval("NoOfProfiles")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total No. Of SMS">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("SMSCount")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>



                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <!-- end: page -->
                                </div>
                                <!-- end Panel -->
                            </div>
                            <!-- container -->
                        </div>
                        <!-- content -->
                        <!-- container -->
                    </div>
                    <!-- content -->
                </div>
            </div>
        </div>
        <asp:Button ID="dummy_BtnAsgnGp" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="mp" runat="server" PopupControlID="myModal"
            PopupDragHandleControlID="dragi" TargetControlID="dummy_BtnAsgnGp" CancelControlID="btnclose"
            BackgroundCssClass="modalbackground">
        </asp:ModalPopupExtender>

        <asp:Panel runat="server" ID="myModal" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <%--<div ID="myModal"  class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">--%>
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header" id="dragi" style="height: 50px">
                        <%--<button type="button" class="close"  data-dismiss="modal" aria-hidden="true">×</button> --%>
                        <asp:Button ID="btnclose" class="close" runat="server" Text="x" />

                    </div>

                    <div class="modal-body">
                        <div class="table-responsive" data-pattern="priority-columns">
                            <table id="profileadditional" class="table table-small-font table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th style="text-align: center">


                                            <asp:Label ID="lblpwdexpry" runat="server" CssClass="form-control"></asp:Label>


                                        </th>
                                    </tr>
                                </thead>

                            </table>
                        </div>
                    </div>

                </div>
            </div>
            <%-- </div> --%>
        </asp:Panel>

    <%--</form>--%>

</asp:Content>
