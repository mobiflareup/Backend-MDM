<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="scall.aspx.cs" Inherits="MobiOcean.MDM.Web.scall" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ScriptManager>--%>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->
        <style type="text/css">
            td.actions {
                text-align: center;
            }
        </style>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <div class="content-page">
            <!-- Start content -->
            <div class="content padding-top-none">
                <!-- Page-Title -->

                <div class="container margin-top-20 margin-bottom-20">
                    <div class="row margin-none">
                        <div class="col-sm-12 col-md-12 col-lg-12">
                        </div>
                    </div>
                </div>
                <!-- Start content -->
                <div class="content">
                    <div class="container">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="m-b-30">
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">

                                        <h2>Allowed Phone No</h2>
                                        <div class="panel panel-border panel-primary">
                                            <div class="panel-body table-rep-plugin">
                                                <div class="row">
														<div class="col-sm-12" style="text-align:right">
															<div class="dataTables_length" id="datatable-editable_length">
																<label>
                                                                   <asp:Button id="btnViewHistory" runat="server" CssClass="btn btn-primary waves-effect waves-light" Text="View History" OnClick="btnViewHistory_Click" />
																</label>
															</div>
														</div>
                                                    </div>
                                                <div class="table-responsive" data-pattern="priority-columns">

                                                    <table id="sec_mess" class="table table-small-font table-bordered table-striped mGrid ">                                                       
                                                        <tbody>                                                            
                                                         
                                                            <tr>
                                                                <td colspan="2">Allow calls to and from specific numbers by entering them here. Click 'Add a new number' every time you want to add a new one,<br>
                                                                     To make things easier, we will also add these numbers automatically to the call WhiteList as well.

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtNo" runat="server" CssClass="form-control" MaxLength="10" Width="40%"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="save"
                                                                    ControlToValidate="txtNo" ErrorMessage="Must be 10 digits" ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                                <asp:FilteredTextBoxExtender ID="fe" runat="server" TargetControlID="txtNo" FilterType="Numbers" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                    ControlToValidate="txtNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                    &nbsp;
                                                                
                                                                </td>
                                                                 
                                                               
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                   <asp:Button ID="btnAddNo" runat="server" CssClass="btn btn-primary waves-effect waves-light" Text="Add a New Number" OnClick="btnAddNo_Click" ValidationGroup="save" /> 

                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="2" style="text-align:center"><label><asp:Label ID="lblMsg" runat="server"></asp:Label>
																	
																</label></td>
                                                            </tr>
                                                            <tr class="number">
                                                               
                                                                <td colspan="2">
                                                                    <asp:GridView ID="grdNo" runat="server" CssClass="table table-small-font table-bordered table-striped mGrid" AutoGenerateColumns="false" OnRowDeleting="grdNo_RowDeleting">
                                                                        <Columns>
                                                                           <%-- <asp:TemplateField HeaderText="Id" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("AllowedPhNoId") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="Number">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNo" runat="server" Text='<%#Eval("AllowPhnNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" CssClass="btn-link" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Button ID="btnApplyChanges" runat="server" Text="Apply Changes" CssClass="btn btn-primary waves-effect waves-light" OnClick="btnApplyChanges_Click" />
                                                                    &nbsp;
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary waves-effect waves-light" OnClick="btnCancel_Click" />
                                                                </td>
                                                            </tr>


                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
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
      
	
	<!-- End Right content here -->
        <!-- ============================================================== -->
     
</asp:Content>

