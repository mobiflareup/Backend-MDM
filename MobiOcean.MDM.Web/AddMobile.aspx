<%@ Page Title="Add Mobile Sos" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" 
    CodeBehind="AddMobile.aspx.cs" Inherits="MobiOcean.MDM.Web.AddMobile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="coh" runat="server" ContentPlaceHolderID="ContentHead">
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">
                    <div class="bhoechie-tab-content active div">

                        <div class="profile1" style="margin: 0px;">
                            SOS Setup
                            <div class="clearfix"></div>
                        </div>
                        <br />
                        <br />
                        <div class=" row">

                            <div class="col-lg-7 col-md-12">

                                <div class="form-group ">
                                    <label for="bname" class="control-label col-lg-4">Name : </label>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv" Display="Dynamic" runat="server" ControlToValidate="txtName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator><br />
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <label for="firstname" class="control-label col-lg-4">Designation : </label>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" placeholder="Designation"></asp:TextBox><br />
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <label for="firstname" class="control-label col-lg-4">Contact No : </label>
                                    <div class="col-lg-8">
                                        <div class="col-md-5">
                                             <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems ="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Mobile No" MaxLength="16"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="MOoldsf" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save" ></asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="fmob" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers" />
                                        </div>
                                    </div>
                                </div>
                                 <div class="form-group "><div class="col-lg-12"> <br /></div></div>
                                <div class="form-group ">
                                   
                                    <label for="firstname" class="control-label col-lg-4">Email Id : </label>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" placeholder="Email Id"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="xyz@xyz.xyz" ForeColor="Red" ValidationGroup="save"
                                            ControlToValidate="txtEmailId" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-4"></label>
                                    <div class="col-lg-8">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-offset-4 col-lg-6">
                                        <asp:Button ID="btnSaveForm" runat="server" Text="Save" CssClass="btn btnd btncompt" OnClick="btnAdd_Click" ValidationGroup="save" />&nbsp;
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-5">
                            </div>

                        </div>
                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="grdAddMobile" runat="server" GridLines="None" class="table mGrid" AutoGenerateColumns="false" HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." OnPageIndexChanging="grdAddMobile_PageIndexChanging" Width="100%" OnRowDeleting="grdAddMobile_RowDeleting"
                                PageSize="20" AllowPaging="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("ContactId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPersonName" runat="server" Text='<%#Eval("ContactPersonName").ToString()==""?"---":Eval("ContactPersonName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation").ToString()==""?"---":Eval("Designation")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId").ToString()==""?"---":Eval("EmailId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("ContactNo").ToString()==""?"---":Eval("Country")+" "+Eval("ContactNo")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="btn-link"
                                                ToolTip="Delete"><i><img src="image/Delete.png" class="iconview"></i></asp:LinkButton>
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
                    <asp:Label ID="lblkeyid" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold; background-color: #e5e5e5; color: #000000">
                    <asp:Label ID="lblUser" runat="server" Text="Are you sure to delete?" />
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
</asp:Content>



