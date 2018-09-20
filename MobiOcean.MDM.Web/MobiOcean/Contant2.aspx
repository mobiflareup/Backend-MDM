<%@ Page Title="" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="Contant2.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.Contant2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
        System.Data.DataTable dt,dt1 = new System.Data.DataTable();
        string originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
        string parentDirectory = originalPath.Substring(originalPath.LastIndexOf("/")+1);
        string[] urlarr = parentDirectory.Split('?');
        parentDirectory = urlarr[1].Substring(4);
        string mid = wb.getValuefromTable("id", "website_menu", "page_url",parentDirectory);//need to change parentDirectory
        dt1 = wb.fetchDisplay("content where menu_id ='" + mid + "'");
        dt = wb.fetchDisplay("website_menu where id='" + mid + "'");%>
        <meta name="Keywords" content="<%= dt.Rows[0]["seo_keyword"] %>">
<meta name="Description" content="<%= dt.Rows[0]["seo_description"] %>">
    <%if (dt1.Rows[0]["bottom_right_bottom"].ToString() != "")
        { %>
    <link rel="stylesheet" type="text/css" href="/MobiOcean/Content/videolb/overlay-minimal.css"/>
<script src="/MobiOcean/Content/videolb/jquery.js" type="text/javascript"></script>
<script src="/MobiOcean/Content/videolb/swfobject.js" type="text/javascript"></script>
<script src="/MobiOcean/Content/videolb/jquery.tools.min.js" type="text/javascript"></script>
<script src="/MobiOcean/Content/videolb/videolightbox.js" type="text/javascript"></script>
    <%} %>
    <title><%= dt.Rows[0]["seo_title"] %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
         System.Data.DataTable dt, dt1, dt2, dt3, dt4, dt5,dt6 = new System.Data.DataTable();
         string originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
         string parentDirectory = originalPath.Substring(originalPath.LastIndexOf("/") + 1);
         string[] urlarr = parentDirectory.Split('?');
        parentDirectory = urlarr[1].Substring(4);
         string mid = wb.getValuefromTable("id", "website_menu", "page_url", parentDirectory);//need to change parentDirectory
         dt = wb.fetchDisplay("content where menu_id ='" + mid + "'");
         dt1 = wb.fetchDisplay("website_menu where id='" + mid + "'");%>
    <div class="container-fluid"> 
            
           <div class="contents"> 
   	   		 <h1><%= dt.Rows[0]["name"] %></h1>
   	   		 
   	   		 
   	   		 <div class="divider"></div>
               <%dt2 = wb.fetchDisplay("website_menu where id='" + dt1.Rows[0]["parent_id"].ToString() + "'");//parent
                   dt3 = wb.fetchDisplay("website_menu where id='" + dt2.Rows[0]["parent_id"].ToString() + "'");//grand parent
                %>
   	   		   
               <a href="<%= MobiOcean.MDM.BAL.Model.Constant.Home%>">Home</a>&nbsp;/&nbsp;
               <%if (dt3.Rows[0]["parent_id"].ToString() != "ROOT")
                   { %>
               <%--<a href="<%= dt3.Rows[0]["page_url"] %>"><%= dt3.Rows[0]["name"] %></a>&nbsp;/&nbsp;--%>
               <%} %>
               <%if (Convert.ToInt32(dt2.Rows[0]["parent_id"]) == 10)
                   { %>
               <a href="<%= dt2.Rows[0]["page_url"] %>"><%= dt2.Rows[0]["name"] %></a>&nbsp;/&nbsp;
               
               <% }%><a href="<%= dt1.Rows[0]["page_url"] %>"><%= dt1.Rows[0]["name"] %></a>
                       </div> 
 
<div class="clearfix"></div>
<br><br>
				
				<div class="mycontent">
				<div class="col-md-8">
					<div class="row" style="border:1px solid #ccc;padding:10px;margin-bottom:20px;box-shadow:0px 0px 5px 0px #000;">
						<%if (dt.Rows[0]["top_image"].ToString() != "")
                            { %>
            	<img src="/Mobiocean/content/content-images/<%= dt.Rows[0]["top_image"]%>" alt="<%= dt.Rows[0]["alt"]%>" title="<%= dt.Rows[0]["title"]%>" class="img-responsive" id="soluhei" alt="<%= dt.Rows[0]["name"]%>" style="background-size:100% 100%;border:1px solid #ccc;margin:0px 8px 8px 0px;padding:5px;width:100%">
               <%} %>
				 
					<p class="text-justify">
						<%=  dt.Rows[0]["description"] %>
						
</p>
					
					<div class="row">
					<%dt4 = wb.fetchDisplay("add_feature where ref_id ='" + mid + "'");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (System.Data.DataRow item in dt4.Rows)
                            { %>
                        <div class="col-lg-4">
							<h4 class="text-center" style="background:#337ab7;padding:5px;border-radius:3px;box-shadow:0px 1px 3px 0px #000;color:#fff;"><%= item["title"].ToString().First().ToString().ToUpper() + item["title"].ToString().Substring(1) %></h4>
							<p><%= Encoding.UTF8.GetString(Encoding.Default.GetBytes(item["content"].ToString())) %><a href="<%= item["link"] %>">..<strong>Read More</strong></a></p>
						</div>
                        <%} %>
                        <%} %>
		
					
				
				
						
						
						<div class="clearfix"></div>
					</div>
				</div>
				</div>   
			   <div class="col-md-4">
					<div id="leftside">
						<span>SOLUTIONS >></span>


	<div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
	<%dt5 = wb.fetchDisplay("website_menu where parent_id='10'and status='Publish' order by sort_order desc");
    if (dt5.Rows.Count > 0)
    {
        foreach (System.Data.DataRow item1 in dt5.Rows)
        {
            dt6 = wb.fetchDisplay("website_menu where parent_id='" + item1["id"] + "'and status='Publish' order by sort_order desc");
            if (dt6.Rows.Count > 0)
            { %>
                <div class="panel panel-default custom-panel">
            <div class="panel-heading custom-panel-heading" role="tab" id="headingOne">
                <h4 class="panel-title custom-panel-title">
                 <p>
                    <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapse<%= item1["id"] %>" aria-expanded="true" aria-controls="collapseOne">
                        <i class="more-less glyphicon glyphicon-plus custom-glyphi"></i>
                        
                    </a> 
                    <a href="<%= item1["page_url"] %>"><%= item1["name"] %></a>
                   </p>
                </h4>
            </div>
                    <div id="collapse<%= item1["id"]%>" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                <div class="panel-body custom-panel-body">
                <%foreach (System.Data.DataRow item2 in dt6.Rows)
    { %>
                      <a href="<%= item2["page_url"]%>" class="list-group-item"><%= item2["name"]%></a>
                       <%} %>
                </div>
            </div>
        </div>
        <%}  else {%>
        <div class="panel panel-default custom-panel">
            	<div class="panel-heading custom-panel-heading" role="tab" id="headingOne">
                <h4 class="panel-title custom-panel-title">
                    <p><a  href="<%= item1["page_url"] %>" >
                        <%= item1["name"] %>
                    </a></p>
                </h4>
            </div> 
            </div>
         <%} %>
        <%} %>
<%}%></div>
	<%if (dt.Rows[0]["bottom_right_top"].ToString() != "" && dt.Rows[0]["pdf"].ToString() != "")
        { %>
						<a href="pdf/<%= dt.Rows[0]["pdf"]%>" class="downbrou" target="_blank"><img class="img-responsive" src="/Mobiocean/content/content-images/<%= dt.Rows[0]["bottom_right_top"] %>" alt="" /></a>
						<%} %><br />
                        <%if (dt.Rows[0]["bottom_right_top"].ToString() != "" && dt.Rows[0]["video_link"].ToString() != "")
                            {
                                string[] linkarr = dt.Rows[1]["video_link"].ToString().Split('?');
                        string link = linkarr[1].Substring(2);%>
						<a class="voverlay" href="http://www.youtube.com/v/<%= link%>?autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer" title="MobiOcean Solution Video"><img class="img-responsive" src="/Mobiocean/content/content-images/<%=  dt.Rows[0]["bottom_right_bottom"]%>" alt="" /></a>        
						<%} %>
				
				</div>
			</div>


		</div>		  
				
	 </div>  
       </asp:content>
     <asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
	  <script>
		function toggleIcon(e) {
    $(e.target)
        .prev('.panel-heading')
        .find(".more-less")
        .toggleClass('glyphicon-plus glyphicon-minus');
}
$('.panel-group').on('hidden.bs.collapse', toggleIcon);
$('.panel-group').on('shown.bs.collapse', toggleIcon);
	  </script>
</asp:Content>
