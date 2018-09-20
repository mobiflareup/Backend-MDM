<%@ Page Title="Approve Customer" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeBehind="ApproveCustomer.aspx.cs" Inherits="MobiOcean.MDM.Web.ApproveCustomer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">

                    <div class="bhoechie-tab-content active">

                        <div class="profile1">&nbsp;&nbsp;Approve Customer</div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <div class="dataTables_length" id="datatable-editable_length">
                                    <label>
                                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="text-align: center">

                            <div class=" form">
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            Name :
                                                               <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            Email Id :
                                                                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            Mobile No :
                                                                    <asp:TextBox ID="txtmblno" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="fte" runat="server" TargetControlID="txtmblno" FilterType="Numbers" />
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group ">

                                    <div class="col-lg-4">
                                        <label>
                                            Contact Person :
                                                                    <asp:TextBox ID="txtcontactperson" runat="server" CssClass="form-control"></asp:TextBox>
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

                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="grdapprove" runat="server" DataKeyNames="CustomerTempId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No record found." OnPageIndexChanging="grdapprove_PageIndexChanging" Width="100%" OnRowDataBound="grdapprove_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("CustomerTempId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkbox" runat="server" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkheader" runat="server" OnCheckedChanged="chkheader_OnCheckedCHanged" AutoPostBack="true" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustname" runat="server" Text='<%#Eval("Name").ToString()==""?"---":Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemailid" runat="server" Text='<%#Eval("EmailId").ToString()==""?"---":Eval("EmailId")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmblno" runat="server" Text='<%#Eval("MobileNo").ToString()==""?"---":Eval("MobileNo")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcont" runat="server" Text='<%#Eval("ContactPersion").ToString()==""?"---":Eval("ContactPersion")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lbladdr" runat="server" Text='<%#Eval("Address").ToString()==""?"---":Eval("Address")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcreatedby" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Customer" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbliscustomer" runat="server" Text='<%#Eval("IsCustomer")%>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View" Visible="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnView" runat="server" CommandName="View" OnClick="lnkbtnView_Click"><i class="fa fa-eye custom-table-fa"></i></asp:LinkButton>&nbsp;                                           
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" Visible="true">
                                        <ItemTemplate>                                
                                                  <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit"
                                                      ToolTip="Edit" OnClick="EditButton_Click"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>
                                            <asp:Label ID="error" runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>
                                     
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                            </asp:GridView>

                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Button ID="btnapprove" runat="server" CssClass="btn btnd btncompt" Text="Approve" OnClick="btnapprove_click" />&nbsp;
                                 <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    <asp:Button ID="dummyBtn" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpeManFields" runat="server" TargetControlID="dummyBtn" PopupControlID="pnlPopUp" PopupDragHandleControlID="dragi2"
        CancelControlID="Closegeo" BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>

    <asp:Panel runat="server" ID="pnlPopUp" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true" class="modal-lg modal-md modal-sm modal-xs" Height="60%" Width="50%" ScrollBars="Auto">

        <div class="modal-content">
            <div class="modal-header" id="dragi2">

                <h4 class="modal-title" id="myModalLabel2">Customer Details
                        
                    <asp:Button ID="btnCloseGeo" runat="server" CssClass="close btn btnd btncompt waves-effect waves-light" Text="x" />
                    <asp:Button ID="Closegeo" runat="server" CssClass="close btn btnd btncompt waves-effect waves-light" Text="x" Style="display: none" />
                </h4>

            </div>
         
            <div class="col-md-12">
                
             <br /> <br />

                <div class="col-md-6">
                    Name:
                    <br /> <br />
                </div>
                <div class="col-md-6">
                     <asp:Label runat="server" ID="lblName" Font-Bold="true"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    AltEmail Id:
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lblaltemailid" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    AltMobile No:
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lblaltmblno" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    AltContact Person:
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lblaltcontperson" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                   Latitude:
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lbllat" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    Longitude:
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lbllong" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    AltAddress:
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lblaltaddr" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            
            <div class="col-md-12">
                <div class="col-md-6">
                    City :
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lblcity" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    District :
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lbldistrict" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    State :
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lblstate" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    Country :
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lblcountry" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    Pin Code :
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lblpin" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    Tin Number :
                    <br /> <br />
                </div>
                <div class="col-md-6">
                   <asp:Label ID="lbltin" runat="server"></asp:Label>
                    <br /> <br />
                </div>
            </div>
          

            <div class="modal-footer">
            </div>
        </div>

        <%--</div>--%>
    </asp:Panel>
    <!-- ============================================================== -->
    <!-- End Right content here -->
    <!-- ============================================================== -->
    <%--</form>--%>
</asp:Content>
