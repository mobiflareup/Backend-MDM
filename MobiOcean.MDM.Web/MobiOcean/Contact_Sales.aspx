<%@ Page Title="MobiOcean | Partner" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="Contact_Sales.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.Contact_Sales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <!-- content start here -->    
<div class="container-fluid">    
    <div class="contents"> <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
                               System.Data.DataTable dt = new System.Data.DataTable();
                               dt = wb.fetchDisplay("contact_sales where id = 1"); %>       
        <h2><%= dt.Rows[0]["title"]%></h2>
        <div class="divider">
         </div> 
       <div>
            <a href="<%= MobiOcean.MDM.BAL.Model.Constant.Home%>">Home</a>&nbsp;/&nbsp;<a href="Contact_Sales.aspx">Contact Sales</a></div> 
        <div class="col-md-6">   
            <%= dt.Rows[0]["description"]%>
        </div>
        <div class="col-md-1">&nbsp;</div>
        <div class="col-md-6" style="padding-right:0px;">
             
	 		<div class="dashborder_img" >
                 
				   <h3 style=" float: left;  margin-top:0px; width: 100%;">Contact Sales</h3>
								<div class="module form-module">
                                 
								  <div clss="formm">
                                    <form id="ajaxform" method="post" action="" style="padding:0px 10px">
                                         
                                          <div class="form-group">
                                              <label> Name* </label>                                          
                                              <input class="form-control" name="cname" id="csname"  type="text" placeholder="enter your Name" required/>
                                          </div>
                                          <div class="form-group">
                                              <label> Company Name* </label>
                                              <input class="form-control" id="cscompanyname" name="comname" type="text" placeholder="Company Name" required/>
                                          </div>
                                            <div class="form-group">
                                              <label> Mobile No* </label>
                                              <input class="form-control" id="csphone" name="csPhonrno" type="text" placeholder="enter your Mobile No" required/>
                                          </div>
                                            <div class="form-group">
                                              <label> Email-Id* </label>
                                              <input class="form-control" id="csemail" name="csemailid" type="email" placeholder="enter your Email-Id" required/>
                                          </div>
                                          <div class="form-group">
                                            <label>Industry</label><br>
                                              <textarea  placeholder="Brief about your company..." id="csindustry" style="width:100%; height:100px;" ></textarea>  
                                            
                                          </div>
										  <button id="contactsalessubmit" >Submit</button>
										</form>
									  </div>
								 
								  <div class="clear"></div>
								  
								</div>
					
					
        
							  
						</div>
           			</div>
       
    </div>
</div>
<!-- content end here -->
</asp:Content>
