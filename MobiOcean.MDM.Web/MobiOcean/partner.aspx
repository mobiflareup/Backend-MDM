<%@ Page Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="partner.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.partner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
        System.Data.DataTable dt = new System.Data.DataTable();
        string originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
        string parentDirectory = originalPath.Substring(originalPath.LastIndexOf("/")+1);
        string[] urlarr = parentDirectory.Split('?');
        parentDirectory = urlarr[1].Substring(4);
        string mid = wb.getValuefromTable("id", "website_menu", "page_url",parentDirectory);
        dt = wb.fetchDisplay("website_menu where id= '" + mid + "'");
         %>
    <title><%: dt.Rows[0]["seo_title"]%></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <!-- content start here --> 
    <div class="container-fluid">    
    <div class="contents"> <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
                               System.Data.DataTable dt = new System.Data.DataTable();
                               string originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                               string parentDirectory = originalPath.Substring(originalPath.LastIndexOf("/")+1);
                               string[] urlarr = parentDirectory.Split('?');
        parentDirectory = urlarr[1].Substring(4);
                               string mid = wb.getValuefromTable("id", "website_menu", "page_url",parentDirectory);
                               dt = wb.fetchDisplay("content where menu_id ='"+mid+"'");
                                    %>       
        <h1><%: dt.Rows[0]["name"] %></h1>
        <div class="divider">
        
        </div> 
        <div><a href="<%= MobiOcean.MDM.BAL.Model.Constant.MobiURL%>">Home</a>&nbsp;/&nbsp;<a href="Partner">Partner</a></div>
        <div class="col-md-6" id="home">  <%= HttpUtility.HtmlDecode(dt.Rows[0]["description"].ToString()) %> 
           
        </div>
        <div class="cl-md-1">&nbsp;</div>
        <div class="col-md-6" style="padding-right:0px;">
             
	 		<div class="dashborder_img" >
                 
				   <h3 style=" float: left; width: 100%; margin-top:0px">Become A Partner</h3>
								<div class="module form-module">
                                 
								  <div class="formm">
                                    <form id="ajaxform" method="post" action="" style="padding:0px 10px">
                                         
                                          <div class="form-group">
                                              <label> Name* </label>                                          
                                              <input class="form-control" name="patnam" id="partnername"  type="text" placeholder="Name" reqired/>
                                          </div>
                                          <div class="form-group">
                                              <label> Company Name* </label>
                                              <input class="form-control" id="partnercompanyname" name="patCN" type="text" placeholder="Company Name" reqired/>
                                          </div>
                                            <div class="form-group">
                                              <label> Mobile No* </label>
                                              <input class="form-control" id="partnerphone" name="patmn" type="text" placeholder="Mobile No" reqired/>
                                          </div>
                                            <div class="form-group">
                                              <label> Email-Id* </label>
                                              <input class="form-control" id="partneremail" name="patemil" type="email" placeholder="Email-Id" reqired/>
                                          </div>
                                          <div class="form-group">
                                            <label>Industry</label><br>
                                            <textarea  placeholder="brief about company" id="partnerindustry" style="  background: #efefef;border: 1px solid #ddd;  height: 100px;
    padding: 10px 16px;  width: 80%; border-radius:5px;"></textarea>
                                          </div>
										  <button id="partnersubmit" >Submit</button>
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
