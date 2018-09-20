<%@ Page Title="Add Profile" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="AddNewProfile.aspx.cs" Inherits="MobiOcean.MDM.Web.AddNewProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ScriptManager>--%>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <!-- flight section -->
                <div class="bhoechie-tab-content active">
               <li class="profile1"><i><img src="image/plus-4.png" class="iconview"></i>&nbsp;&nbsp;Add Profile</li>
<%--<li class="profile1xs hidden-lg hidden-md"><i><img src="image/plus-4.png" class="iconview"></i></li>--%>
<br />

<%--<div class="creatp">--%><%--<a href="#" class="btn btnd btncompt"><i><img src="image/plus-4.png" class="iconview1"></i>&nbsp;&nbsp;<span class="creatsp">Add Profile</span></a>--%><%--</div>--%>
<br />
<%--<br />
<h1 class="pmanage">Add Profile</h1>--%>
            <!-- Start content -->
            <div class="content padding-top-none">
                <!-- Page-Title -->
                
                
                <!-- Start content -->
               
                              
                                
                                                <div class="panel-body table-rep-plugin">
                                                    <div class=" form">
                                                        <div class="col-lg-7">                                                        
                                                            <div class="form-group ">
                                                                <label for="company" class="control-label col-lg-4">Profile Code* : </label>
                                                                <div class="col-lg-8">
                                                                    <asp:TextBox ID="txtProfilecode" runat="server" CssClass="form-control" placeholder="Profile Code"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                        ControlToValidate="txtProfilecode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                <label for="firstname" class="control-label col-lg-4">Profile Name* : </label>
                                                                <div class="col-lg-8">
                                                                    <asp:TextBox ID="txtProfileName" runat="server" CssClass="form-control" placeholder="Profile Name"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                        ControlToValidate="txtProfileName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            
                                                            <div class="form-group ">
                                                                <label for="username" class="control-label col-lg-4">Profile Purpose* : </label>
                                                                <div class="col-lg-8">
                                                                    <asp:TextBox ID="txtProfilePurpose" runat="server" CssClass="form-control" placeholder="Profile Purpose"></asp:TextBox>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server"
                                                                        ControlToValidate="txtProfilePurpose" ErrorMessage="Required!" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                   
                                                                </div>
                                                            </div>
                                                        
                                                        <div class="form-group ">
                                                                
                                                                <div class="col-lg-12">
                                                                    <br />
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                <%--<label for="phone" class="control-label col-lg-2">Manager Contact No. :</label>--%>
                                                                <div class="col-lg-10">
                                                                    <asp:Label ID="lblpopmsg" runat="server"></asp:Label>
                                                                </div>
                                                            </div>


                                                            <div class="form-group">
                                                                <div class="col-lg-offset-2 col-lg-12">
                                                                    <%--<asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-default waves-effect" Text="Edit" OnClick="btnUpdate_Click" ValidationGroup="save" /> --%> 
                                                                     <asp:Button ID="BtnSave" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save" OnClick="btnSave_Click" ValidationGroup="save"/> 
                                                                    
                                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnCancel_Click" />                                                               
	<%--<button type="submit" class="btn btnd btncompt">Save</button>
    <button type="submit" class="btn btnd btncompt">Cancel</button>--%> 
                                                                </div>
                                                            </div>
                                                          </div> 

                                                    </div>
                                                   
                                                    </div>
                                                  
                                               
                               
                    <!-- content -->
                    <!-- container -->
                </div>
                <!-- content -->
</div>
    
 
  
  

                    
                    
                      
                    
                </div>
                <!-- train section -->
            </div>



        
      
       
	<!-- ============================================================== -->
        <!-- End Right content here -->
        <!-- ============================================================== -->
    <%--</form>--%>
</asp:Content>



