<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AddAppMaster.aspx.cs" Inherits="MobiOcean.MDM.Web.AddAppMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" Runat="Server">
    
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar"  id="style-3">                   
	<div class="content-page">
		<!-- Start content -->
		<div class="content padding-top-none">
			<!-- Page-Title -->
			<div class="container whitebg padding-top-20">
				<div class="row margin-none">
					<div class="col-sm-12">
						<div class="col-sm-10">
							<h1 class="pull-left page-title">Add New App</h1>
						</div>
						<div class="col-sm-2 pull-right">
							<h3  style="display:inline-block;">
							
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
                                           
											<div class="col-lg-7">
												<div class="form-group ">
                                                    <label for="bname" class="control-label col-lg-4">Group Name* : </label>
                                                    <div class="col-lg-8">
                                                       <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="form-control" AppendDataBoundItems="true" ></asp:DropDownList>
                                                        <asp:CompareValidator ID="comp" runat="server" ControlToValidate="ddlGroupName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                   
                                                    <div class="col-lg-12">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="lastname" class="control-label col-lg-4">Application Code* : </label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtApplicationCode" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="txtApplicationCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                             
                                                    </div>
                                                </div>
												<div class="form-group ">
                                                    <label for="firstname" class="control-label col-lg-4">Application Name* : </label>
                                                    <div class="col-lg-8">
                                                       <asp:TextBox ID="txtApplicationName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                    ControlToValidate="txtApplicationName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
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
                                                        <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt" Text="Save" ValidationGroup="save"  OnClick="btnsave_Click" />
                                                        
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt" Text="Cancel" CommandName="Cancel" OnClick="btnCancel_Click"/>
                                                         
                                                    </div>
                                                </div>
                                            </div>
											<div class="col-lg-5">
											
												</div>
											</div>
											</div>
											
											</div>
											
											 
                                        </div> 
											<%--</div>--%> 
										
								</div>
							</div>
						</div>
						<!-- end: page -->
					</div> <!-- end Panel -->
				</div> <!-- container -->
			</div> <!-- content -->
			<!-- container -->
		</div> <!-- content -->
		
	
             
</asp:Content>



