<%@ Page Title="Add Web Category" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="AddWebCategory.aspx.cs" Inherits="MobiOcean.MDM.Web.AddWebCategory" %>

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
                                    <h1 class="pull-left page-title">Add Web Category</h1>
                                </div>
                                <div class="col-sm-2 pull-right">
                                    <h3 style="display: inline-block;">
                                        <%--<button title="" data-placement="left" data-toggle="tooltip" class="btn btn-default circleicon colorgrey" type="button" data-original-title="Add SubAdmin"><i class="fa fa-user-plus"></i></button>--%>
                                    </h3>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--<div class="container margin-top-20 margin-bottom-20">
				<div class="row margin-none">
					<div class="col-sm-12 col-md-12 col-lg-12">
					 <div id='chart_div'></div>  
					</div>
				</div>
			</div>--%>
                    <!-- Start content -->
                    <div class="content m-t-20">
                        <div class="container">
                            <div class="panel">
                                <div class="panel-body">
                                    <%--<div class="row">
								<div class="col-sm-6">
									<div class="m-b-30">
										<a id="addToTable" href="profilefeature.aspx" class="btn btn-primary waves-effect waves-light">Add User <i class="fa fa-plus"></i></a>
									</div>
								</div>
							</div>--%>

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel-group panel-group-joined" id="accordion-test">
                                                
                                                        <div class=" form">
                                                            <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                                                            <div class="col-lg-7">
                                                                <div class="form-group ">
                                                                    <label for="bname" class="control-label col-lg-4">Category Code* : </label>
                                                                    <div class="col-lg-8">
                                                                        <asp:TextBox ID="txtKCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                            ControlToValidate="txtKCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group ">

                                                                    <%-- <div class="col-lg-12">
                                                        <br />
                                                    </div>--%>
                                                                </div>
                                                                <div class="form-group ">
                                                                    <label for="firstname" class="control-label col-lg-4">Category Name* : </label>
                                                                    <div class="col-lg-8">
                                                                        <asp:TextBox ID="txtKName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                            ControlToValidate="txtKName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>

                                                                    </div>
                                                                </div>
                                                                <div class="form-group ">
                                                                    <label for="lastname" class="control-label col-lg-4">Description* : </label>
                                                                    <div class="col-lg-8">
                                                                        <asp:TextBox ID="txtKDesc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                            ControlToValidate="txtKDesc" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
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
                                                                        <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt" Text="Save" ValidationGroup="save" OnClick="btnAssign_Click" />
                                                                        <%--<button class="btn btn-success waves-effect waves-light" type="submit">Save</button>
                                                        <a href="admindashboard.aspx" class="btn btn-default waves-effect" type="button">Cancel</a>--%>
                                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt" Text="Cancel" CommandName="Cancel" OnClick="btnCancel_Click" />

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-5">
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
            </div>
        </div>
        <%-- </div>
            </div>
        </div>--%>
        <!-- ============================================================== -->
        <!-- End Right content here -->
        <!-- ============================================================== -->
    <%--</form>--%>
</asp:Content>





