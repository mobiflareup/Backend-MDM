<%@ Page Title="" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="resource_contant.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.resource_contant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
        System.Data.DataTable dt = new System.Data.DataTable();
        string originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
        string parentDirectory = originalPath.Substring(originalPath.LastIndexOf("/")+1);
        string[] urlarr = parentDirectory.Split('?');
        parentDirectory = urlarr[1].Substring(4);
        string mid = wb.getValuefromTable("id", "website_menu", "page_url",parentDirectory);
        //dt = wb.fetchDisplay("content where menu_id ='" + mid + "'");
        //dt1 = wb.fetchDisplay("resourse_content where id='" + mid + "'");
        //dt2 = wb.fetchDisplay("resourse_content_solution where id='" + mid + "'");
        dt = wb.fetchDisplay("website_menu where id='" + mid + "'");
        %>
    <style>
img.imagehiwi {
    margin: 0px !important;
    width: 24px !important;
}
</style>
    <%if (parentDirectory == "videos")
        { %>

<link rel="stylesheet" type="text/css" href="videolb/overlay-minimal.css"/>
<script src="videolb/jquery.js" type="text/javascript"></script>
<script src="videolb/swfobject.js" type="text/javascript"></script>
<script src="videolb/jquery.tools.min.js" type="text/javascript"></script>
<script src="videolb/videolightbox.js" type="text/javascript"></script>
<%} %>
<meta charset="UTF-8">
<meta name="Generator" content="EditPlus®">
<meta name="Author" content="">
<meta name="Keywords" content="<%= dt.Rows[0]["seo_keyword"] %>">
<meta name="Description" content="<%= dt.Rows[0]["seo_description"] %>">
<title><%= dt.Rows[0]["seo_title"] %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
        System.Data.DataTable dt, dt1, dt2, dt3, dt4, dt5,dt6,dt7 = new System.Data.DataTable();
        string originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
        string parentDirectory = originalPath.Substring(originalPath.LastIndexOf("/") + 1);
        string[] urlarr = parentDirectory.Split('?');
        parentDirectory = urlarr[1].Substring(4);
        string mid = wb.getValuefromTable("id", "website_menu", "page_url", parentDirectory);//need to change parentDirectory
        dt = wb.fetchDisplay("content where menu_id ='" + mid + "'");//carr
        dt1 = wb.fetchDisplay("resourse_content where menu_id='" + mid + "'");//ccarr
        dt2 = wb.fetchDisplay("resourse_content_solution where menu_id='" + mid + "'");//csarr
        dt3 = wb.fetchDisplay("website_menu where id='" + mid + "'");//wm
        %>

    <%-- Content Start --%>
     <div class="container-fluid"> 
   	   		
            
     			<div class="contents"> 
   	   		 		<h1><%= dt.Rows[0]["name"] %></h1>
   	   		 		
   	   		 		<div class="divider"></div>
                     <%dt4 = wb.fetchDisplay("website_menu where id =" + Convert.ToInt32(dt3.Rows[0]["parent_id"]));//parent
                         dt5 = wb.fetchDisplay("website_menu where id =" + Convert.ToInt32(dt4.Rows[0]["parent_id"]));//grand parent %>
   	   		 		
    		<a href="<%= MobiOcean.MDM.BAL.Model.Constant.Home%>">Home</a>&nbsp;/&nbsp;
                     <%if (dt5.Rows[0]["name"].ToString() != "ROOT")
                         {
                         }
                        %><a href="<%= dt3.Rows[0]["page_url"]%>"><%= dt3.Rows[0]["name"]%></a>
                       </div> 
                       <br>
                       <br>
            <!-- products video -->
            <div class="mycontents video"> 
             <div class="col-md-8">
            <div class="row">
            <div class="col-md-12">
                 <%if (dt1.Rows.Count > 0)
                     {
                         if (dt1.Rows[0]["name"].ToString() != "")
                         { %>
                      
                   <h3><%= dt1.Rows[0]["name"]%></h3>
                   <%}
    else
    {
                           %>

                   <h3> Products </h3>


                <%
    }
    for (int i = 0; i < dt1.Rows.Count; i++)
    { %>
                  
                   <div class="col-md-3 col-sm-4"><%if (parentDirectory == "videos" && dt1.Rows[i]["Link"].ToString() != "")
                                                      {
                                                          string[] linkarr = dt1.Rows[i]["Link"].ToString().Split('?');
                                                          string link = linkarr[1].Substring(2);%>
                   
                         <a class="voverlay" href="http://www.youtube.com/v/<%= link%>?autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer"
						  title=""><img class="img-responsive" id="resouresvideo" src="/Mobiocean/Content/Admin/upload/<%= dt1.Rows[i]["image"]%>" alt="<%= dt1.Rows[i]["name"] %>" title="<%= dt1.Rows[i]["name"] %>"></a>
                          <%}
    else if (dt1.Rows[i]["pdf"].ToString() != "")
    { %>
                         <a href="pdf/<%= dt1.Rows[i]["pdf"] %>" class="downbrou" target="_blank"><img class="img-responsive" id="resouresvideo" src="/Mobiocean/Content/Admin/upload/<%= dt1.Rows[i]["image"] %>" alt="White Paper" title="White Paper" ></a>
                         <%} else { %>
                         <img class="img-responsive" id="resouresvideo" src="/Mobiocean/Content/Admin/upload/<%= dt1.Rows[i]["image"]%>" > 
                    <%} %>
                   </div>
                   <%
                           }
                       } %>
                    
                   
             </div>  </div>     
        
             <!-- rightside bar -->
               
        
             <!-- solutions video -->
             <div class="row">
                 <%if (parentDirectory != "case-studies")
                     {%>
              
             <div class="col-md-12">       
                    <%if (dt2.Rows.Count > 0)
    {
        if (dt2.Rows[0]["name"].ToString() != "")
        { %>
                      
                   <h3><%= dt2.Rows[0]["name"]%></h3>
                   <%}
    else
    {   %>


                   <h3> Solutions </ h3 >
                 <% }%>
                 <%for (int i = 0; i < dt2.Rows.Count; i++)
    {%>
                    
                   <div class="col-md-3 col-sm-4"><%if (parentDirectory == "videos" && dt2.Rows[i]["Link"].ToString() != "")
    {
        string[] linkarr = dt2.Rows[i]["Link"].ToString().Split('?');
        string link = linkarr[1].Substring(2);%>
                    
                          <a class="voverlay" href="http://www.youtube.com/v/<%= link%>?autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer" 
						  title=""><img class="img-responsive" id="resouresvideo" src="/Mobiocean/Content/Admin/upload/<%= dt2.Rows[i]["image"]%>" alt="<%= dt2.Rows[i]["alt"]%>" title="<%= dt2.Rows[i]["title"]%>"></a> 
                        <%}
    else if (dt2.Rows[i]["pdf"].ToString() != "")
    { %>
                        <a href="pdf/<%= dt2.Rows[i]["pdf"]%>" class="downbrou" target="_blank"> <img class="img-responsive" id="resouresvideo"  src="/Mobiocean/Content/Admin/upload/<%= dt2.Rows[i]["image"]%>" alt="<%= dt2.Rows[i]["alt"]%>" title="<%= dt2.Rows[i]["title"]%>"> </a>
                        <%}
    else
    { %>
                         <img class="img-responsive" id="resouresvideo" src="/Mobiocean/Content/Admin/upload/<%= dt2.Rows[i]["image"]%>" alt="<%= dt2.Rows[i]["alt"]%>" title="<%= dt2.Rows[i]["title"]%>">
                         <%} %>
                   </div>
                   <%}
    } %>
                   
             </div>  
             <%
    }%>
             
             </div>  </div>
             
              <div class="col-md-4">
                        <div id="leftside">
                            <span>RESOURCES >></span>
                            <ul>
                               <%--<?php 
								   include_once('Classes/Website-Menu.php');
								   $w = new WebsiteMenu();
								   $w->menu_options2(26);
								 ?>--%>
                                <%-- data start --%>
                                <%= wb.option2("26",parentDirectory)%>
                                <%-- data End --%>
                            </ul>
                            
                         
                        </div>	
                </div>
       </div>  
    </div>    
       	  
    <%-- Content End --%>
</asp:Content>
