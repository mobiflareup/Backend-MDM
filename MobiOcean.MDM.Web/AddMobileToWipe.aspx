<%@ Page Title="Add Mobile To Wipe Data" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="AddMobileToWipe.aspx.cs" Inherits="MobiOcean.MDM.Web.AddMobileToWipe" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ScriptManager>--%>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="content-page">
                <!-- Start content -->
                <div class="content padding-top-none">
                    <!-- Page-Title -->
                    <div class="container whitebg padding-top-20">
                        <div class="row margin-none">
                            <div class="col-sm-12">
                                <div class="col-sm-10">
                                    <h1 class="pull-left page-title">Add Mobile To Wipe Data</h1>
                                </div>
                                <div class="col-sm-2 pull-right">
                                    <h3 style="display: inline-block;">
                                        <button title="" data-placement="left" data-toggle="tooltip" class="btn btn-default circleicon colorgrey" type="button" data-original-title="Add SubAdmin"><i class="fa fa-user-plus"></i></button>
                                    </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    


                    <!-- Start content -->
                    <div class="content m-t-20">
                        <div class="container">
                            <div class="panel">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel-group panel-group-joined" id="accordion-test">
                                           
                                                        <div class=" form">
                                                            <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>

                                                            <div class="form-group ">
                                                                <%--<label for="bname" class="control-label col-lg-4">User Name* : </label>--%>
                                                                <div class="col-lg-6">
                                                                    <label>User Name* :
                                                                        <asp:DropDownList ID="ddlUserName" runat="server" AutoPostBack="true" CssClass="form-control" Width="150px" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged"></asp:DropDownList></label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" InitialValue="0" ControlToValidate="ddlUserName" ErrorMessage="*Please Choose User" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                <div class="col-lg-6">
                                                                    <label>Requester Name* :
                                                                        <asp:TextBox ID="txtRequestorName" runat="server" CssClass="form-control"></asp:TextBox></label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                        ControlToValidate="txtRequestorName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                <div class="col-lg-6">
                                                                    <label>
                                                                        Mobile No* :
                                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="fmob" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                            ControlToValidate="txtMobileNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="save"
                                                                            ControlToValidate="txtMobileNo" ErrorMessage="Must be 10 digits" ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                                    </label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                <div class="col-lg-6">
                                                                    <label>
                                                                        <br/>
                                                                   <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="save" Width="80px" CssClass="btn btnd btncompt" OnClick="btnAdd_Click" />
                                                                    </label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                <div class="col-lg-12" style="text-align: center">
                                                                    <label>
                                                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Maroon"></asp:Label>
                                                                    </label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="table-responsive" data-pattern="priority-columns">

                                                            <asp:GridView ID="grdAddMobile" runat="server" class="table-responsive table table-small-font table-bordered table-striped mGrid" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." OnPageIndexChanging="grdAddMobile_PageIndexChanging" Width="100%" OnRowDeleting="grdAddMobile_RowDeleting"
                                                                PageSize="20" AllowPaging="true">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("WipeDataId")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S.No.">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblContactPersonName" runat="server" Text='<%#Eval("PersonName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mobile No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("PersonNo")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="btn-link"
                                                                                Text="Delete" ToolTip="Delete" OnClientClick="return confirm('The Mobile No. will be deleted. Are you sure want to continue?');" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12">


                                                                <asp:Button ID="btnApply" runat="server" class="btn btnd btncompt" OnClick="btnApply_Click" Text="Apply Changes" />
                                                                <asp:Button ID="btncancel" runat="server" class="btn btnd btncompt" Text="Cancel" OnClick="btncancel_Click" />



                                                          
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <%--</form>--%>
</asp:Content>




