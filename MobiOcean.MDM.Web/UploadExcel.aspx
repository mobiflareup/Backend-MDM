<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="UploadExcel.aspx.cs" Inherits="MobiOcean.MDM.Web.UploadExcel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
     </asp:ScriptManager>--%>
<!-- ============================================================== -->
	<!-- Start right Content here -->
	<!-- ============================================================== -->                      
	<div class="content-page">
		<!-- Start content -->
		<div class="content padding-top-none">
			<!-- Page-Title -->
			<div class="container whitebg padding-top-20">
				<div class="row margin-none">
					<div class="col-sm-12">
						<div class="col-sm-10">
							<h1 class="pull-left page-title">Upload Excel</h1>
						</div>
						<div class="col-sm-2 pull-right">
							<h3  style="display:inline-block;">
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
							<div class="row">
								<div class="col-sm-6">
									<div class="m-b-30">
										<%--<a id="addToTable" href="AssignGroupToUser.aspx" class="btn btn-primary waves-effect waves-light">Assign Group to User <i class="fa fa-plus"></i></a>--%>
									</div>
								</div>
							</div>
							
							<div class="row">
								<div class="col-lg-12">
									<div class="panel-group panel-group-joined" id="accordion-test"> 
										<div class="panel panel-default margin-top-20"> 
											<div class="panel-heading"> 
												<h4 class="panel-title"> 
													<a data-toggle="collapse" data-parent="#accordion-test" href="#collapseTwo">
														Upload Excel
													</a> 
												</h4> 
											</div> 
											<div id="collapseTwo" class="panel-collapse collapse in"> 
												<div class="panel-body table-rep-plugin"> 
													<div class="row">
														<div class="col-sm-12" style="text-align:center">
															<div class="dataTables_length" id="datatable-editable_length">
																<label><asp:Label ID="lblmsg" runat="server"></asp:Label>
																	
																</label>
															</div>
														</div>
														
													</div>
                                                    
                                                    <div class="row" style="text-align:center">										                                                    
                                                    <div class=" form">
                                                        <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                                                            <div class="form-group ">
                                                               
                                                                <div class="col-lg-4">
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btn-success waves-effect waves-light"/>
                                    <asp:Button runat="server" ID="btnupload" Text="Upload" CssClass="btn btn-primary waves-effect waves-light" Width="110px" OnClick="btnUpload_Click"/>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                
                                                                <div class="col-lg-4">
                                                                     Select Sheet : <asp:DropDownList ID="ddlSheet" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                
                                                                <div class="col-lg-4">
                                                                     <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary waves-effect waves-light" Text="Save Excel" OnClick="btnSave_Click" Enabled="false" />
                                                                </div>
                                                            </div>        
                                                                                                       
                                                         
                                                            <div class="col-lg-10">
                                                                    <br />
                                                                </div>
                                                    </div>
                                                        </div>
													<div class="table-responsive" data-pattern="priority-columns">
														
															 <asp:GridView ID="GridView1" AllowPaging="true" AutoGenerateColumns="true" runat="server" CssClass="mGrid" Visible="false"></asp:GridView>
														
													</div>  
													
												</div> 
											</div> 
										</div> 
									</div> 
								</div>
							</div>
						<!-- end: page -->
					</div> <!-- end Panel -->
				</div> <!-- container -->
			</div> <!-- content -->
			<!-- container -->
		</div> <!-- content -->
		
	</div>
                    </div>
                </div>
            </div>
        </div>
	<!-- ============================================================== -->
	<!-- End Right content here -->
	<!-- ============================================================== -->	
<%--</form>--%>
</asp:Content>
