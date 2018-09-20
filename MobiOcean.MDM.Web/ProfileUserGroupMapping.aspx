<%@ Page Title="Profile V/s User Group" Language="C#" MasterPageFile="~/MasterPage.master"
     AutoEventWireup="true" CodeBehind="ProfileUserGroupMapping.aspx.cs" Inherits="MobiOcean.MDM.Web.ProfileUserGroupMapping" %>
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
							<h1 class="pull-left page-title">Profile User Group Mapping</h1>
						</div>
						<div class="col-sm-2 pull-right">
							<h3  style="display:inline-block;">
							<a title="" data-placement="left" data-toggle="tooltip" class="btn btn-default circleicon colorgrey" href="AssignGroupToUser.aspx" data-original-title="Assign Profile"><i class="fa fa-user-plus"></i></a>
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
										<a id="addToTable" href="AssignGroupToUser.aspx" class="btn btn-primary waves-effect waves-light">Assign Profile <i class="fa fa-plus"></i></a>
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
														Profile User Group Mapping
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
														<%--<div class="col-sm-4 ">
															<div id="datatable-editable_filter" class="dataTables_filter">
																<label>Search:
																	<input type="search" class="form-control input-sm" placeholder="" aria-controls="datatable-editable">
																</label>
															</div>
														</div>--%>
													</div>
                                                    
                                                    <div class="row" style="text-align:center">										                                                    
                                                    <div class=" form">
                                                        <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                                                            <div class="form-group ">
                                                               
                                                                <div class="col-lg-4">
                                                                     <label >Group Name :
                                                                   <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                                         </label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                
                                                                <div class="col-lg-4">
                                                                    <label >Profile Name :
                                                                    <asp:DropDownList ID="ddlProfileName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                                        </label>
                                                                </div>
                                                            </div>
                                                            
                                                           
                                                         <div class="form-group ">
                                                                
                                                                <div class="col-lg-4">
                                                                    <label>
                                                                        <br />
                                                                        <asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btn-primary waves-effect waves-light" OnClick="btnSrch_Click" />
                                                                        </label>
                                                                </div>
                                                            </div>
                                                           
                                                    </div>
                                                        </div>
                                                    <br />
													<div class="table-responsive" data-pattern="priority-columns">
														
															<asp:GridView ID="grdProfile" runat="server" class="table table-small-font table-bordered table-striped mGrid" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." OnPageIndexChanging="grdProfile_PageIndexChanging" OnRowCancelingEdit="grdProfile_RowCancelingEdit" OnRowDataBound="grdProfile_RowDataBound" OnRowDeleting="grdProfile_RowDeleting" OnRowEditing="grdProfile_RowEditing" OnRowUpdating="grdProfile_RowUpdating" AllowPaging="true" PageSize="20">
                                           <Columns >
                                           <asp:TemplateField HeaderText="Id" Visible="false">
                                           <ItemTemplate>
                                                 <asp:Label ID="lblId" runat="server" Text='<%#Eval("ProfileGroupId")%>'></asp:Label>
                                           </ItemTemplate>
                                           </asp:TemplateField>                                            
                                         <asp:TemplateField HeaderText="Group Name" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("GrouppName")%>'></asp:Label>
                                                <asp:Label ID="lblGroupId" runat="server" Text='<%#Eval("GroupId")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox id="txtUserName" runat="server" Text='<%#Eval("UserName")%>' cssclass="TxtBox"></asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Profile Name" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblProfileName" runat="server" Text='<%#Eval("ProfileName")%>'></asp:Label>
                                                <asp:Label ID="lblProfileId" runat="server" Text='<%#Eval("ProfileId")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEProfileId" runat="server" Text='<%#Eval("ProfileId")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="DropDownList1" runat="server" cssclass="form-control" AppendDataBoundItems="true"></asp:DropDownList>                                              
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Current Status" >
                                            <ItemTemplate>
                                               <asp:Label ID="lblIsEnable" runat="server" Text='<%#Eval("IsEnable").ToString()=="1"?"Enabled":"Disabled"%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEIsEnable" runat="server" Visible="false" Text='<%#Eval("IsEnable")%>'></asp:Label>
                                                <asp:DropDownList ID="ddlIsEnable" runat="server" cssclass="form-control">
                                                    <asp:ListItem Value="0" >Disabled</asp:ListItem>
                                                    <asp:ListItem Value="1" >Enabled</asp:ListItem>
                                                </asp:DropDownList>
                                             
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>                                             
                                                 <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                         <asp:LinkButton ID="lbEdit" runat="server"  CssClass="btn-link" Text="Edit" CommandName="Edit" ToolTip="Edit"></asp:LinkButton>
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
                                                 <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn-link" Text="Delete" CommandName="Delete" OnClientClick="return confirm('The Group will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                            <%--<asp:ImageButton ID="lbldelete" runat="server" CssClass="bg-img"/>--%>
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
                
	<!-- ============================================================== -->
	<!-- End Right content here -->
	<!-- ============================================================== -->	
 
</asp:Content>


