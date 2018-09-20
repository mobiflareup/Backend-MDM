<%@ Page Title="Role Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="RoleMaster.aspx.cs" Inherits="MobiOcean.MDM.Web.RoleMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" Runat="Server">
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
							<h1 class="pull-left page-title">Role Master</h1>
						</div>
						<div class="col-sm-2 pull-right">
							<h3  style="display:inline-block;">
							<a title="" data-placement="left" data-toggle="tooltip" class="btn btn-default circleicon colorgrey" href="AddRole.aspx" data-original-title="Add Role"><i class="fa fa-user-plus"></i></a>
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
								<asp:Button id="addToTable" runat="server" class="btn btn-primary waves-effect waves-light" Text="Add Role" OnClick="addToTable_Click"  />
								</div>
							</div>
							
							<div class="row">
								<div class="col-lg-12">
									<div class="panel-group panel-group-joined" id="accordion-test"> 
										<div class="panel panel-default margin-top-20"> 
											<div class="panel-heading"> 
												<h4 class="panel-title"> 
													<a data-toggle="collapse" data-parent="#accordion-test" href="#collapseTwo">
														Role Master
													</a> 
												</h4> 
											</div> 
											<div id="collapseTwo" class="panel-collapse collapse in"> 
												<div class="panel-body table-rep-plugin"> 
												<div class="row">
													<div class="col-sm-12" style="text-align:center">
															<div class="dataTables_length" id="datatable-editable_length">
																<label><asp:Label ID="lblMsg" runat="server"></asp:Label>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     																
																</label>
                                                                </div>
                                                        </div>
                                                        </div>
                                                     <div class="row" style="text-align:center">
                                                             
                                                         <div class=" form">
                                                        <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                                                            <div class="form-group ">
                                                               
                                                                <div class="col-lg-4">
                                                                     <label > Role Code:
                                                               <asp:TextBox ID="txtRcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                         </label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                
                                                                <div class="col-lg-4">
                                                                    <label >Role Name:
                                                                    <asp:TextBox ID="txtRname" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                        </label>
                                                                </div>
                                                            </div>
                                                            
                                                            <div class="form-group ">
                                                                
                                                                <div class="col-lg-4">
                                                                    <label><br />
                                                                  <asp:Button ID="btnSrch" runat="server" CssClass="btn btn-primary waves-effect waves-light" Text="Search" OnClick ="btnSrch_Click" />
                                                                        </label>
                                                                </div>
                                                            </div>
                                                         
                                                           
                                                    </div>
                                                     </div>           
                                                             
                                                    <br />
													<div class="table-responsive" data-pattern="priority-columns">
														
															<asp:GridView ID="grdRole" runat="server" class="table table-small-font table-bordered table-striped mGrid" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." OnPageIndexChanging="grdRole_PageIndexChanging" Width="100%" OnRowCancelingEdit="grdRole_RowCancelingEdit" OnRowDeleting="grdRole_RowDeleting" OnRowEditing="grdRole_RowEditing" OnRowUpdating="grdRole_RowUpdating" PageSize="20" AllowPaging="true">
                                           <Columns >
                                           <asp:TemplateField HeaderText="Id" Visible="false">
                                           <ItemTemplate>
                                                 <asp:Label ID="lblId" runat="server" Text='<%#Eval("RoleId")%>'></asp:Label>
                                           </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RoleCode" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblRoleCode" runat="server" Text='<%#Eval("RoleCode")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox id="txtRoleCode" runat="server" Text='<%#Eval("RoleCode")%>' cssclass="TxtBox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtrolecode" runat="server" ControlToValidate="txtRoleCode" ErrorMessage="*" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="RoleName" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblRoleName" runat="server" Text='<%#Eval("RoleName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox id="txtRoleName" runat="server" Text='<%#Eval("RoleName")%>' cssclass="TxtBox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtRoleName" runat="server" ControlToValidate="txtRoleName" ErrorMessage="*" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                              
                                              
                                                <%-- <asp:TemplateField HeaderText="Edit Profile">
                                        <ItemTemplate>
                                         <asp:LinkButton ID="lbEdit" runat="server" href="javascript:;" CssClass="btn-link" Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="UpdateButton" runat="server" href="#" class="hidden on-editing save-row" CommandName="Update" CssClass="LinkBtn"
                                                Text="Update" ToolTip="Update" ValidationGroup="Update" /><i class="fa fa-save fa-lg"></i>
                                            &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" href="#" class="on-editing cancel-row" CommandName="Cancel" CssClass="LinkBtn"
                                                Text="Cancel" ToolTip="Canecl" /><i class="fa fa-times fa-lg"></i>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" CssClass="btn-link"
                                                Text="Edit" ToolTip="Edit" />
                                           
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="btn-link"
                                                Text="Update" ToolTip="Update" ValidationGroup="Update" />
                                            &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel" CssClass="btn-link"
                                                Text="Cancel" ToolTip="Canecl" />
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Delete Profile">
                                        <ItemTemplate>
                                           <%-- <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn-link" Text="Delete"></asp:LinkButton>--%>
                                            <%--<asp:ImageButton ID="lbldelete" runat="server" CssClass="bg-img"/>--%>
                                             <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="btn-link"
                                                Text="Delete" ToolTip="Delete" OnClientClick="return confirm('The Role will be deleted. Are you sure want to continue?');" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateField>
                                     </Columns>
                                         <PagerStyle HorizontalAlign = "Right" CssClass = "dataTables_paginate paging_simple_numbers pagination-ys" />
                                     </asp:GridView>
														
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
           
	<!-- ============================================================== -->
	<!-- End Right content here -->
	<!-- ============================================================== -->	
 
</asp:Content>

