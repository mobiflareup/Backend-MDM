<%@ Page Title="Assigned Profile" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeBehind="ProfileUserMapping.aspx.cs" Inherits="MobiOcean.MDM.Web.ProfileUserMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" Runat="Server">
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
     </asp:ScriptManager>--%>
<!-- ============================================================== -->
	<!-- Start right Content here -->
	<!-- ============================================================== -->      
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
	<div class="content-page">
		<!-- Start content -->
		<div class="content padding-top-none">
			<!-- Page-Title -->
			<div class="container whitebg padding-top-20">
				<div class="row margin-none">
					<div class="col-sm-12">
						<div class="col-sm-10">
							<h1 class="pull-left page-title">Profile User Mapping</h1>
						</div>
						<div class="col-sm-2 pull-right">
							<h3  style="display:inline-block;">
							<a title="" data-placement="left" data-toggle="tooltip" class="btn btn-default circleicon colorgrey" href="AssignProfileToUser.aspx" data-original-title="Assign Profile"><i class="fa fa-user-plus"></i></a>
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
										<a id="addToTable" href="AssignProfileToUser.aspx" class="btn btn-primary waves-effect waves-light">Assign Profile <i class="fa fa-plus"></i></a>
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
														Profile User Mapping
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
                                                                     <label >User Name : 
                                                                    <asp:DropDownList ID="ddlUser" runat="server" AppendDataBoundItems="true" CssClass="form-control" ></asp:DropDownList>
                                                                         </label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                
                                                                <div class="col-lg-4">
                                                                    <label >Profile Name : 
                                                                    <asp:DropDownList ID="ddlProfile" runat="server" AppendDataBoundItems="true" CssClass="form-control" ></asp:DropDownList>
                                                                        </label>
                                                                </div>
                                                            </div>
                                                            
                                                            <div class="form-group ">
                                                                
                                                                <div class="col-lg-4">
                                                                    <label><br /><asp:Button ID="btnSrch" runat="server" Text="Search" CssClass="btn btn-primary waves-effect waves-light" OnClick="btnSrch_Click" />
                                                                        </label>
                                                                </div>
                                                            </div>
                                                         
                                                           
                                                    </div>
                                                        </div>	
                                                    <div class="row">
                                                        <br />
                                                    </div>
													<div class="table-responsive" data-pattern="priority-columns">
														
															<asp:GridView ID="grdProfile" runat="server" class="table table-small-font table-bordered table-striped mGrid"  DataKeyNames="Profileuserid"
                                                                OnRowDataBound="grdProfile_RowDataBound" OnRowEditing="grdProfile_RowEditing" 
                                                                OnRowDeleting="grdProfile_RowDeleting1" OnRowUpdating="grdProfile_RowUpdating1" 
                                                                OnRowCancelingEdit="grdProfile_RowCancelingEdit1" AutoGenerateColumns="false" 
                                                                ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." AllowPaging="true" PageSize="20" OnPageIndexChanging="grdProfile_PageIndexChanging">
                                           <Columns >
                                           <asp:TemplateField HeaderText="Id" Visible="false">
                                           <ItemTemplate>
                                                 <asp:Label ID="lblId" runat="server" Text='<%#Eval("Profileuserid")%>'></asp:Label>
                                           </ItemTemplate>
                                           </asp:TemplateField>                                            
                                         <asp:TemplateField HeaderText="User Name" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox id="txtUserName" runat="server" Text='<%#Eval("UserName")%>' cssclass="TxtBox"></asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Device" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeviceId" runat="server" Text='<%#Eval("DeviceId")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox id="txtUserName" runat="server" Text='<%#Eval("UserName")%>' cssclass="TxtBox"></asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Profile Name" >
                                            <ItemTemplate>
                                                 <asp:Label ID="lblProfileId" runat="server" Text='<%#Eval("ProfileId")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblProfileName" runat="server" Text='<%#Eval("ProfileName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEProfileId" runat="server" Text='<%#Eval("ProfileId")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlProfileName" runat="server" cssclass="form-control input-sm customselect"></asp:DropDownList>                                              
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Current Status" >
                                            <ItemTemplate>
                                               <asp:Label ID="lblIsEnable" runat="server" Text='<%#Eval("IsEnable").ToString()=="1"?"Enabled":"Disabled"%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEIsEnable" runat="server" Visible="false" Text='<%#Eval("IsEnable")%>'></asp:Label>
                                                <asp:DropDownList ID="ddlIsEnable" runat="server" cssclass="form-control input-sm customselect">
                                                    <asp:ListItem Value="0" >Disabled</asp:ListItem>
                                                    <asp:ListItem Value="1" >Enabled</asp:ListItem>
                                                </asp:DropDownList>
                                             
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>                                             
<asp:TemplateField HeaderText="History">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkbtnHistory" runat="server" CssClass="btn-link" Text="History" CommandName="History" OnClick="lnkbtnHistory_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>                                                 <asp:TemplateField HeaderText="Edit">
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
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn-link" Text="Delete" CommandName="delete"  OnClientClick="return confirm('The Assigned profile will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                            <%--<asp:ImageButton ID="lbldelete" runat="server" CssClass="bg-img"/>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"  />
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

         </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
<div id="myModal"  class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                            <div class="modal-dialog popwidth">

           
                <div class="modal-content">

                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                                        <h4 class="modal-title" id="myModalLabel">Profile Mapping History</h4>
                                                    </div>
                                                                          
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive" data-pattern="priority-columns">
                                    <asp:GridView ID="Gdv" runat="server" class="table table-small-font table-bordered table-striped mGrid"
                                        AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPopId" runat="server" Text='<%#Eval("ProfileUserHstryId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPopUserId" runat="server" Text='<%#Eval("UserId")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblPopUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Device">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPopDeviceId" runat="server" Text='<%#Eval("DeviceId")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblPopDeviceName" runat="server" Text='<%#Eval("DeviceName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profile Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPopProfileId" runat="server" Text='<%#Eval("ProfileId")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblPopProfileName" runat="server" Text='<%#Eval("ProfileName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPopIsEnable" runat="server" Text='<%#Eval("IsEnable").ToString()=="1"?"Enabled":"Disabled"%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreationDate" runat="server" Text='<%#MyFormat(Eval("CreationDate").ToString())%>'></asp:Label>
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

                    <div class="modal-footer">
                       <button type="button" class="btn btn-primary waves-effect waves-light" data-dismiss="modal" aria-hidden="true">Close</button>
                    </div>
                </div>
            </div>
    </div>
   </ContentTemplate>
        </asp:UpdatePanel>
                
	<!-- ============================================================== -->
	<!-- End Right content here -->
	<!-- ============================================================== -->	
 
</asp:Content>


