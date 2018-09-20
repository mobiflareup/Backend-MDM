<%@ Page Title="Employee Group" Language="C#" MasterPageFile="~/MasterPage.master"
     AutoEventWireup="true" CodeBehind="EmpGroup.aspx.cs" Inherits="MobiOcean.MDM.Web.EmpGroup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" Runat="Server">
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
     </asp:ScriptManager>--%>
<!-- ============================================================== -->
	<!-- Start right Content here -->
	<!-- ============================================================== -->     
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 bhoechie-tab scrollbar" id="style-3">                 
	<div class="content-page">
		<!-- Start content -->
		<div class="content padding-top-none">
			<!-- Page-Title -->
			<div class="container whitebg padding-top-20">
				<div class="row margin-none">
					<div class="col-sm-12">
						<div class="col-sm-10">
							<h1 class="pull-left page-title">Employee Group</h1>
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
							<%--<div class="row">
								<div class="col-sm-6">
									<div class="m-b-30">
										<a id="addToTable" href="AssignProfileToUser.aspx" class="btn btn-primary waves-effect waves-light">Send Email/Sms to Employee Groups <i class="fa fa-plus"></i></a>
									</div>
								</div>
							</div>--%>
							
							<div class="row">
								<div class="col-lg-12">
									<div class="panel-group panel-group-joined" id="accordion-test"> 
										
											
											<div id="collapseTwo" class="panel-collapse collapse in"> 
												
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
                                                            <div class="form-group ">
                                                               
                                                                <div class="col-lg-4">
                                                                     <label >Employee Group Name :
                                                                   <asp:TextBox ID="txtEmpGrpName" runat="server"  CssClass="form-control"></asp:TextBox>
																	 <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtEmpGrpName"
                                                                        ForeColor="Red" ErrorMessage="Required!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                         </label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                
                                                                <div class="col-lg-4">
                                                                    <label >Description :
                                                                     <asp:TextBox ID="txtDescription" TextMode="MultiLine" Height="40px" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                        </label>
                                                                </div>
                                                            </div>
                                                        </br>
                                              <div class="form-group ">
                                                                
                                                                <div class="col-lg-4">
                                                                    <label>
                                                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btnd btncompt" OnClick="btnSave_Click" ValidationGroup="Save" />
                                                                        </label>
                                                                </div>
                                                            </div>
                                                           
                                                    </div>
                                                        </div>
                                                   
													<div class="table-responsive" data-pattern="priority-columns">
												<asp:GridView ID="grdEmployeeGrp" DataKeyNames="GroupId" runat="server" class="table table-small-font table-bordered table-striped mGrid" OnRowDataBound="grdEmployeeGrp_RowDataBound" OnRowEditing="grdEmployeeGrp_RowEditing" OnRowDeleting="grdEmployeeGrp_RowDeleting1" OnRowUpdating="grdEmployeeGrp_RowUpdating1" OnRowCancelingEdit="grdEmployeeGrp_RowCancelingEdit1" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." AllowPaging="true" PageSize="20">
                                           <Columns >
                                            <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("GroupId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblEId" runat="server" Text='<%#Bind("GroupId")%>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Group Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGpName" runat="server" Text='<%#Eval("GrouppName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEGpName" runat="server" Text='<%#Bind("GrouppName")%>' CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtEGpName"
                                                ForeColor="Red" ErrorMessage="Required!" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEDescription" runat="server" Text='<%#Bind("Description")%>'
                                                CssClass="form-control" Style="resize: none" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton CssClass="btn-link" ID="EditButton" Text="Edit" CommandName="Edit"
                                                runat="server" />
                                            &nbsp;
                                            <asp:LinkButton CssClass="btn-link" ID="DeleteButton" Text="Delete" CommandName="Delete"
                                                OnClientClick="return confirm('The User Group will be deleted. Are you sure want to continue?');" runat="server" />
                                            
                                               
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton CssClass="btn-link" ID="UpdateButton" Text="Update" CommandName="Update"
                                                runat="server" ValidationGroup="Update" />
                                            &nbsp;
                                            <asp:LinkButton CssClass="btn-link" ID="CancelUpdateButton" Text="Cancel" CommandName="Cancel"
                                                runat="server" />
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Manage Employees">
                                             <ItemTemplate>
                                                  <asp:LinkButton CssClass="btn-link" ID="lbAddPrsns" Text="Manage Employees in group"
                                                runat="server" onclick="lbAddPrsns_Click" />
                                             </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Center" />
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
								</div>
							</div>
						<!-- end: page -->
					</div> <!-- end Panel -->
				</div> <!-- container -->
			</div> <!-- content -->
			<!-- container -->
		</div> <!-- content -->
		
	</div>
	<!-- ============================================================== -->
	<!-- End Right content here -->
	<!-- ============================================================== -->	
 
</asp:Content>
