<%@ Page Title="Login" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="Mobi_Free_Trial.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.Mobi_Free_Trial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="main">
	 	<div class="content">	
	 		<div class="dashborder_img" style="background:#eee">
                   <div class="wrap"> 
				   <div class="container"><!--<h3>Login</h3>-->
                                <!--<div class="col-md-3"></div>
								<div class="module form-module col-md-6" style="margin-top:30px"  >-->
                                  <div class="col-md-6 col-sm-8 col-xs-10 col-md-offset-3 col-sm-offset-2 col-xs-offset-1" ><h3>Free Trial</h3></div>
								<div class="module form-module col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2 col-xs-10 col-xs-offset-1" >
								  <div clss="formm">
                                    <form id="ajaxform" method="post" action="" style="padding:10px" >
                                    
                                          <b style=" font-size:16px; text-transform:uppercase">If you want to use free trial login here :- </b>
                                          
                                          <div class="form-group"  style="margin-top:10px" >
                                              <label> Email Id </label>                                          
                                              <input class="form-control" name="EmailId" id="EmailId"  type="email" placeholder="Email-Id"/>
                                          </div>
                                          <div class="form-group">
                                              <label> Password </label>
                                              <input class="form-control" id="Password" name="Password" type="password" placeholder="Password"/>
                                          </div>
										  <button class="loginbtnclick" id="loginbtnclick">Login</button>
										</form>
									  </div>
								 
								  <div class="clear"></div>
                                        <div class="cta" style="padding:10px" ><b style=" font-size:14px;">For new user click here to registration :-</b>
                                            <a href="Registration"> Register here</a>
                                        </div>
                                        
                                        <div class="cta" style="padding:10px" ><b style=" font-size:14px;">For Subcription based pricing  here :-</b>
                                            <a href="cloud-managed"> Subcription</a>
                                        </div> 
								</div>
					
					</div>
        
							  
						</div>
           			</div>
       	</div>
	 </div>
   
</asp:Content>
