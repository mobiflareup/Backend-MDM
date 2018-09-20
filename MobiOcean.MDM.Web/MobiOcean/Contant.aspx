<%@ Page Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="Contant.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.Contant" %>
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
        System.Data.DataTable dt, dt1, dt2, dt3, dt4, dt5 = new System.Data.DataTable();
        string originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
        string parentDirectory = originalPath.Substring(originalPath.LastIndexOf("/") + 1);
        string[] urlarr = parentDirectory.Split('?');
        parentDirectory = urlarr[1].Substring(4);
        string mid = wb.getValuefromTable("id", "website_menu", "page_url", parentDirectory);//need to change parentDirectory
        dt = wb.fetchDisplay("content where menu_id ='" + mid + "'");
        dt1 = wb.fetchDisplay("website_menu where id='" + mid + "'");%>

    <!-- Modal -->
  <div class="modal fade" id="AppModal" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title text-center">Download Android App</h4>
        </div>
        <div class="modal-body">
       <form action="save.php" method="post" name="myForm"  onsubmit="return validateForm()">
			  <div class="form-group">
				<label for="email">Email address:</label>
				<input type="email" class="form-control"  name="email" placeholder="Enter email">
			  </div>
		
			<p class="text-center"><button type="submit"" class="btn btn-success cta hvr-float-shadow text-center">Submit</button></p>
			  
			</form>
        </div>
      </div>
    </div>
  </div>
  
  <!-- Modal -->
  <div class="modal fade" id="IModal" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title text-center">Download App</h4>
        </div>
        <div class="modal-body">
       <form  action="save1.php" method="POST" role="form" name="myForm1"  onsubmit="return validateForm1()">
			  <div class="form-group">
				<label for="email">Email address:</label>
				<input type="email" name="email" class="form-control" id="iemail" placeholder="Enter email">
			  </div>
			  <p class="text-center"><button type="submit"" class="btn btn-success cta hvr-float-shadow text-center">Submit</button></p>
			  
			</form>
        </div>
      </div>
    </div>
  </div>
  
  <!-- Modal -->
  <div class="modal fade" id="AppeModal" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title text-center">Download Android App</h4>
        </div>
        <div class="modal-body">
       <form action="esave.php" method="post" name="myForm2"  onsubmit="return validateForm2()">
			  <div class="form-group">
				<label for="email">Email address:</label>
				<input type="email" class="form-control"  name="email" placeholder="Enter email">
			  </div>
		
			<p class="text-center"><button type="submit"" class="btn btn-success cta hvr-float-shadow text-center">Submit</button></p>
			  
			</form>
        </div>
      </div>
    </div>
  </div>
  
  <!-- Modal -->
  <div class="modal fade" id="IeModal" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title text-center">Download App</h4>
        </div>
        <div class="modal-body">
       <form  action="esave1.php" method="POST" role="form" name="myForm3"  onsubmit="return validateForm3()">
			  <div class="form-group">
				<label for="email">Email address:</label>
				<input type="email" name="email" class="form-control" id="iemail" placeholder="Enter email">
			  </div>
			  <p class="text-center"><button type="submit"" class="btn btn-success cta hvr-float-shadow text-center">Submit</button></p>
			  
			</form>
        </div>
      </div>
    </div>
  </div>
  
   <!-- Modal -->
  <div class="modal fade" id="AppcModal" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title text-center">Download Android App</h4>
        </div>
        <div class="modal-body">
       <form action="csave.php" method="post" name="myForm4"  onsubmit="return validateForm4()">
			  <div class="form-group">
				<label for="email">Email address:</label>
				<input type="email" class="form-control"  name="email" placeholder="Enter email">
			  </div>
		
			<p class="text-center"><button type="submit"" class="btn btn-success cta hvr-float-shadow text-center">Submit</button></p>
			  
			</form>
        </div>
      </div>
    </div>
  </div>
  
     <!-- Modal -->
  <div class="modal fade" id="pcModal" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title text-center">Download For PC</h4>
        </div>
        <div class="modal-body">
       <form action="downloads.php" method="post" name="myForm5"  onsubmit="return validateForm5()">
			  <div class="form-group">
				<label for="email">Email address:</label>
				<input type="email" class="form-control"  name="email" placeholder="Enter email">
			  </div>
		
			<p class="text-center"><a href="javascript:;" ><button type="submit"  class="btn btn-success cta hvr-float-shadow text-center">Submit</button>&nbsp;<button type="submit" data-dismiss="modal"  class="btn btn-warning cta hvr-float-shadow text-center">Close</button></a></p>
			
			  
			</form>
        </div>
      </div>
    </div>
  </div>
  

   
    
   
       <div class="container-fluid"> 
            
           <div class="contents"> 
   	   		 <h1><%= dt1.Rows[0]["name"] %></h1>
   	   		 
   	   		 <div class="divider"></div>
               <%dt2 = wb.fetchDisplay("website_menu where id='" + dt1.Rows[0]["parent_id"].ToString() + "'");//parent
                   dt3 = wb.fetchDisplay("website_menu where id='" + dt2.Rows[0]["parent_id"].ToString() + "'");//grand parent
                %>
   	   		
    	<a href="<%= MobiOcean.MDM.BAL.Model.Constant.Home%>">Home</a>&nbsp;/&nbsp;
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
				  
            	<img src="/MobiOcean/Content/content-images/<%= dt.Rows[0]["top_image"] %>" alt="<%= dt.Rows[0]["alt"] %>" title="<%= dt.Rows[0]["title"] %>" class="img-responsive" id="soluhei" alt="<%= dt.Rows[0]["name"] %>" style="background-size:100% 100%;border:1px solid #ccc;margin:0px 8px 8px 0px;padding:5px;width:100%">
                 
                      <%}
                          if (dt.Rows[0]["appstore"].ToString() != "")
                          {%>
                  
                  
				 	<a href="javascript:;" style="display:inline-block;" ><%= dt.Rows[0]["appstore"]%></a>
                      <%}
                          if (dt.Rows[0]["istore"].ToString() != "")
                          {
                               %>
				 	
				 	<a href="javascript:;" style="display:inline-block;"><%= dt.Rows[0]["istore"]%></a>
				 	
				 	<%}
                        if (Convert.ToInt32(mid) == 45)
                        {%>
				 	
				 			
				 			<a href="javascript:;" id="pcStore" style="display:inline-block;" ><img src="/MobiOcean/Content/images/pcicon.png" alt="Download for PC" Title="Download for PC"></a>
				 	
				 	<%} %>
					<p class="text-justify">
						<%= dt.Rows[0]["description"] %>
						

						
					</p>
					<p class="text-justify">
                        <%= dt.Rows[0]["description1"] %>
						
                        <%= dt.Rows[0]["description2"] %>
						
					</p>
					
				</div> 
				
			</div>
            <div class="col-md-4">
			<div id="leftside">
                <span>SOLUTIONS >></span>


	<div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
	
	
        <%dt4 = wb.fetchDisplay("website_menu where parent_id='10'and status='Publish' order by sort_order desc");
            if (dt4.Rows.Count > 0)
            {
                foreach (System.Data.DataRow item in dt4.Rows)
                {
                    dt5 = wb.fetchDisplay("website_menu where parent_id='" + item["id"].ToString() + "'and status='Publish' order by sort_order desc");
                    if (dt5.Rows.Count > 0)
                    { %>

        <div class="panel panel-default custom-panel">
            <div class="panel-heading custom-panel-heading" role="tab" id="headingOne">
                <h4 class="panel-title custom-panel-title">
                 <p>
                    <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapse<%= item["id"] %>" aria-expanded="true" aria-controls="collapseOne">
                        <i class="more-less glyphicon glyphicon-plus custom-glyphi"></i>
                        
                    </a> 
                    <a href="<%= item["page_url"] %>"><%= item["name"] %></a>
                   </p>
                </h4>
            </div>
            <div id="collapse<%= dt4.Rows[0]["id"] %>" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                <div class="panel-body custom-panel-body">
                    <%foreach (System.Data.DataRow item1 in dt5.Rows)
                        { %>
                <a href="<%= item1["page_url"] %>" class="list-group-item"><%= item1["name"] %></a>
                    <%} %>
                     
                </div>
            </div>
        </div>
         <%}
             else
             {%>
        
                <div class="panel panel-default custom-panel">
            	<div class="panel-heading custom-panel-heading" role="tab" id="headingOne">
                <h4 class="panel-title custom-panel-title">
                    <p><a  href="<%= item["page_url"] %>" >
                        
                        <%= item["name"] %>
                    </a></p>
                </h4>
            </div> 
            </div>
           <%}
                   }
               } %>
    

           	

    </div>
                
                <%if (dt.Rows[0]["bottom_right_top"].ToString() != "" && dt.Rows[0]["pdf"].ToString() != "")
                    { %>
                
                <a href="pdf/<%= dt.Rows[0]["pdf"] %>" class="downbrou" target="_blank"><img class="img-responsive" src="/MobiOcean/Content/content-images/<%= dt.Rows[0]["bottom_right_top"] %>" alt="" /></a>
                <%} %>
               <!-- else { <img class="img-responsive" src="content-images/< ?php echo $carr[0]['bottom_right_top'] ?>" alt="" />
                // < ?php } ?>-->
                <br />
                <%if (dt.Rows[0]["bottom_right_bottom"].ToString() != "" && dt.Rows[0]["video_link"].ToString() != "")
                    {
                        string[] linkarr = dt.Rows[0]["video_link"].ToString().Split('?');
                        string link = linkarr[1].Substring(2);%>
                <a class="voverlay" href="http://www.youtube.com/v/<%= link%>?autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer" title="MobiOcean Solution Video"><img class="img-responsive" src="/MobiOcean/Content/content-images/<%= dt.Rows[0]["bottom_right_bottom"] %>" alt="" /></a>        
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
	  
	 
<script type="text/javascript">

function validateForm() {
    var x = document.forms["myForm"]["email"].value;
    if (x == "") {
        alert("Email must be filled out");
        return false;
    }
}

function validateForm1() {
    var x = document.forms["myForm1"]["email"].value;
    if (x == "") {
        alert("Email must be filled out");
        return false;
    }
}
function validateForm2() {
    var x = document.forms["myForm2"]["email"].value;
    if (x == "") {
        alert("Email must be filled out");
        return false;
    }
}
function validateForm3() {
    var x = document.forms["myForm3"]["email"].value;
    if (x == "") {
        alert("Email must be filled out");
        return false;
    }
}
function validateForm4() {
    var x = document.forms["myForm4"]["email"].value;
    if (x == "") {
        alert("Email must be filled out");
        return false;
    }
}
function validateForm5() {
    var x = document.forms["myForm5"]["email"].value;
    if (x == "") {
        alert("Email must be filled out");
        return false;
    }
}
$(document).ready(function(){

	$("#AppStore").click(function(){

		 $('#AppModal').modal('show');
		
		});

	$("#IStore").click(function(){

		 $('#IModal').modal('show');
		
		
		});

	$("#AppeStore").click(function(){

		 $('#AppeModal').modal('show');
		
		});

	$("#IeStore").click(function(){

		 $('#IeModal').modal('show');
		
		
		});
	$("#AppcStore").click(function(){

		 $('#AppcModal').modal('show');
		
		});
	$("#pcStore").click(function(){

		 $('#pcModal').modal('show');
		
		});
	$("#Astore").click(function(){

		var email = $("#aemail").val();
		if(email==""){
			alert("Please Enter Email");
			return false;
		}

		//alert(email);
		$.ajax({
            type: "POST",
            url: "http://localhost/dev.mobiocean.com/save.php",
            datatype:json,
            //data: 'id='+email,
            data: {id:email},  						
            success:function(data)
			{
            	//getPreventDefault();
            	alert(data);
				
            	
				
				//window.location.href = ('https://play.google.com/store/apps/details?id=com.loment.peanut.mobile&hl=en');
				//window.open('https://play.google.com/store/apps/details?id=com.loment.peanut.mobile&hl=en');
				
			},
			failure:function(htm){
				alert(htm);
				}
          });
		
		
		});

	$("#Istore").click(function(event){

		var email = $("#iemail").val();
		if(email==""){
				alert("Please Enter Email");
				return false;
			}
		$.ajax({
			  url: 'http://localhost/dev.mobiocean.com/save.php',
			  type: 'POST',
			  data: {id:email},
			  success: function(data) {
				 // event.PreventDefault();
				  alert(data);
				  
				
				//window.location.href = ('https://itunes.apple.com/in/app/walnut-secure-email/id574848194?mt=8');
			  },
			  error: function(e) {
				//called when there is an error
				console.log(e.message);
			  }
			});
		
		});
	$("#pc").click(function(event){

		var email = $("#iemail").val();
		//if(email==""){
				//alert("Please Enter Email");
				//return false;
			//}
		$.ajax({
			  url: 'http://localhost/dev.mobiocean.com/downloads.php',
			  type: 'POST',
			  data: {id:email},
			  success: function(data) {
				 // event.PreventDefault();
				  //alert(data);
				  
				 $('#AppcModal').modal('close');
				//window.location.href = ('https://itunes.apple.com/in/app/walnut-secure-email/id574848194?mt=8');
			  },
			  error: function(e) {
				//called when there is an error
				console.log(e.message);
			  }
			});
		
		});
	
});
</script>
</asp:Content>
