<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ProfileUserMappingHstry.aspx.cs" Inherits="MobiOcean.MDM.Web.ProfileUserMappingHstry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" Runat="Server">
     <%--<form id="form1" runat="server">--%>
	<!-- Start right Content here -->
	<!-- ============================================================== -->                      
	<div class="content-page">
		<!-- Start content -->
        <%-- --------------------------- Admin Dashboard --------------------------------- ----------------------------------------------- ------------------------%>
      <div class="content padding-top-none">
			<!-- Page-Title -->
			<div class="container whitebg padding-top-20">
				<div class="row margin-none">
					<div class="col-sm-12">
						<div class="col-sm-10">                 
                        <table class="col-sm-12">
						<tr>
							<td>
                             <asp:Label ID="Label1" runat="server" class="pull-left page-title"><h2>Admin Dashboard</h2></asp:Label>
                           </td>
                           <div class="col-sm-2 pull-right"> </div>                                                                               				
						   <td class="col-sm-2 pull-right"> 
                               <br />            
                              <asp:ImageButton ID="ImageButton1" runat="server" style="display:inline-block;" title="" data-placement="left" data-toggle="tooltip" class="btn btn-default circleicon colorgrey" data-original-title="Add SubAdmin" /><asp:Label ID="Label3" runat="server" class="fa fa-user-plus"></asp:Label>
                         </td>
                         
                        </tr>
                     </table>
                    </div>   
                 </div>	             
			  </div>
		   </div> 
          
          
			<!-- End row-->
			<!-- Start content -->
           <%-- ---------------------------- Add Profile Button------------------------ --------------------------------- -----------------------------------------------%>
	
        		<div class="content m-t-20">
				<div class="container">
					<div class="panel">
						<div class="panel-body">
							<%--<div class="row">
								<div class="col-sm-6">
									<div class="m-b-30">
                                        <table>
                                            <tr>
                                                <td>
                                                  <asp:Button id="addToTable" runat="server" class="btn btn-primary waves-effect waves-light" Text="Add Profile" OnClick="addToTable_Click1" />
                                                  <asp:Label ID="lblimg" runat="server" class="fa fa-plus"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
								   
									</div>
								</div>
							</div>--%>
                      <%----------------- Sub admin  ----------------------- ---------------------- -------------------------------------- --------------------%>
							<%--<div class="row">
								<div class="col-lg-12">
									<div class="panel-group panel-group-joined" id="accordion-test"> 
										<div class="panel panel-default margin-top-20"> 
											<div  class="panel-heading">                                             
                                               <table>
                                                   <tr>
                                                        <td>
                                                         <asp:Label ID="Label2" runat="server" data-toggle="collapse" data-parent="#accordion-test" href="#collapseTwo"><h4 class="panel-title" > Client Master </h4> </asp:Label>         
                                                        </td>
                                                    </tr>
                                               </table>                                              
											</div> --%>
                           <%-- -----------------  Dropdowns ---------------------------------    -------------------------------------------------%>  
                              <%--<table class="dataTables_length"  id="datatable-editable_length">
                                  <tr class="panel-collapse collapse in">
                                      <td >
                                          <asp:Label ID="lblShow" runat="server" Text="Show" name="datatable-editable_length" aria-controls="datatable-editable"></asp:Label>
                                      </td>
                                      <td>
                                          <asp:DropDownList ID="ddlShow" runat="server" class="form-control input-sm customselect">
                                              <asp:ListItem Value="10">10</asp:ListItem>
                                              <asp:ListItem Value="25">25</asp:ListItem>
                                              <asp:ListItem Value="50">50</asp:ListItem>
                                              <asp:ListItem Value="100">100</asp:ListItem>
                                              <asp:ListItem Value="All">All</asp:ListItem>
                                          </asp:DropDownList>           
                                         </td>
                                    <td>
                                         entries&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPeriod" runat="server" Text="Period" name="datatable-editable_length" aria-controls="datatable-editable"></asp:Label>
                                      </td>
                                      &nbsp;&nbsp;<td>
                                          <asp:DropDownList ID="ddlPeriod" runat="server" class="form-control input-sm customselect">
                                              <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                              <asp:ListItem Value="Quaterly">Quaterly</asp:ListItem>
                                              <asp:ListItem Value="Halfyearly">Halfyearly</asp:ListItem>
                                              <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                          </asp:DropDownList>
                                      </td>&nbsp;&nbsp;
                                        &nbsp;&nbsp;<td>
                                          &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDevice" runat="server" Text="Device" name="datatable-editable_length" aria-controls="datatable-editable"></asp:Label>
                                      </td>
                                      &nbsp;&nbsp;<td>
                                             <asp:DropDownList ID="ddlDevice" runat="server"  class="form-control input-sm customselect">
                                              <asp:ListItem Value="All">All</asp:ListItem>
                                              <asp:ListItem Value="Android">Android</asp:ListItem>
                                              <asp:ListItem Value="IOS">IOS</asp:ListItem>
                                              <asp:ListItem Value="Windows">Windows</asp:ListItem>
                                          </asp:DropDownList>
                                    
                                      </td>&nbsp;&nbsp;
                                      <td>                                        
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblsearch" runat="server" Text="Search:"  ></asp:Label>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txtsearch" runat="server" class="form-control input-sm" placeholder="" aria-controls="datatable-editable"></asp:TextBox>
                                      </td>
                                </tr>
                             </table> --%>
                                  <%----------- Gridview ---------------------------------------------------------------------------------------------------%>
                            <asp:Label ID="Label2" runat="server" class="pull-left page-title"><h2>Profile User Mapping History</h2></asp:Label>
							     <%-- <table class="table-responsive" data-pattern="priority-columns">
                                       <tr>
                                          <td >--%>
                                           <asp:GridView ID="grdProfileusrmpng" runat="server" class="table-responsive table table-small-font table-bordered table-striped" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." OnPageIndexChanging="grdProfileusrmpng_PageIndexChanging" Width="100%" OnRowCancelingEdit="grdProfileusrmpng_RowCancelingEdit" OnRowDeleting="grdProfileusrmpng_RowDeleting" OnRowEditing="grdProfileusrmpng_RowEditing" OnRowUpdating="grdProfileusrmpng_RowUpdating">
                                           <Columns >
                                           <asp:TemplateField HeaderText="Id" Visible="false">
                                           <ItemTemplate>
                                                 <asp:Label ID="lblId" runat="server" Text='<%#Eval("ProfileUserId")%>'></asp:Label>
                                           </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox id="txtUserName" runat="server" Text='<%#Eval("UserName")%>' cssclass="TxtBox"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Profile Name" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblProfileName" runat="server" Text='<%#Eval("ProfileName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox id="txtProfileName" runat="server" Text='<%#Eval("ProfileName")%>' cssclass="TxtBox"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Is Enable" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsEnable" runat="server" Text='<%#Eval("IsEnable").ToString()=="1"?"Enabled":"Disabled"%>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox id="txtIsEnable" runat="server" Text='<%#Eval("IsEnable")%>' cssclass="TxtBox"></asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Applied DateTime" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppliedDateTime" runat="server" Text='<%#Eval("AppliedDateTime")%>'></asp:Label>
                                            </ItemTemplate>
                                           <%-- <EditItemTemplate>
                                                <asp:TextBox id="txtAppliedDateTime" runat="server" Text='<%#Eval("AppliedDateTime")%>' cssclass="TxtBox"></asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
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
                                                Text="Delete" ToolTip="Delete" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateField>
                                     </Columns>
                                     </asp:GridView>
                                    <%-- </td> 
                                  </tr>
							</table>--%>
          <%--  <%------------------------ Editable info status   ---------------------------   --------------------------------------------%>
													<%--<div class="row">
														<div class="col-sm-6">
															 <asp:Label id="datatable-editable_info" class="dataTables_info"  role="status" aria-live="polite">Showing 1 to 10 of 35 entries</asp:Label>
														</div>
														<div class="col-sm-6 ">
															<div class="dataTables_paginate paging_simple_numbers" id="datatable-editable_paginate">
																<ul class="pagination">
																	<li class="paginate_button previous disabled" aria-controls="datatable-editable" tabindex="0" id="datatable-editable_previous">
																	<a href="#">Previous</a></li>
																	<li class="paginate_button active" aria-controls="datatable-editable" tabindex="0"><a href="#">1</a></li>
																	<li class="paginate_button " aria-controls="datatable-editable" tabindex="0"><a href="#">2</a></li>
																	<li class="paginate_button " aria-controls="datatable-editable" tabindex="0"><a href="#">3</a></li>
																	<li class="paginate_button " aria-controls="datatable-editable" tabindex="0"><a href="#">4</a></li>
																	<li class="paginate_button " aria-controls="datatable-editable" tabindex="0"><a href="#">5</a></li>
																	<li class="paginate_button " aria-controls="datatable-editable" tabindex="0"><a href="#">6</a></li>
																	<li class="paginate_button next" aria-controls="datatable-editable" tabindex="0" id="datatable-editable_next"><a href="#">Next</a></li>
																</ul>                                  
															</div>
														</div>
													</div>--%>
												</div> 
											</div> 
										</div> 
									</div> 
								</div>
							</div>
						<%--</div>--%>
						<!-- end: page -->
					<%--</div> <!-- end Panel -->--%>
				<%--</div> <!-- container -->--%>
			<!-- container -->
        <%--</div>
		<footer class="footer text-right">2015 © MobilieyOcean.</footer> --%>    
           
</asp:Content>
