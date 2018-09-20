<%@ Page Title="Contact" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="MobiContact_Us.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.MobiContact_Us" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid"> 
       
        <div class="contents"><!-- 
      <img class="img-responsive" src="Admin/upload/<?php echo $carr[4]['image'];?>" alt="" style="margin:20px 0px 0px 0px;"><span>
   	   		
   	   		 -->
   	   		  <h1>CONTACT US</h1>
   	   		 <div class="divider"></div>
   	   		<%-- <?php 
   	   		 
   	   		 echo '<a href="http://'.$_SERVER['SERVER_NAME'].'">Home</a>';
			
			echo '&nbsp;/&nbsp;';
			
				echo '<a href="contact.php">Contact</a>';
				
				?>--%><a href="<%= MobiOcean.MDM.BAL.Model.Constant.Home%>">Home</a>&nbsp;/&nbsp;<a href="contact.php">Contact</a>

             <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
                 System.Data.DataTable dt = new System.Data.DataTable();
                 %>
                       </div>
   	   		<!--<div class="divider col-md-4"></div> 
   	   		<h2 class="col-md-4" style=" margin-bottom:15px !important;">CONTACT US</h2>
                 <div class="divider col-md-4" style="border-right:254px solid #fff; border-left:0px solid"></div>
                      <div class="clearfix"></div> -->
                       <div id="contact_page"> <%dt = wb.fetchDisplay("contact_us order by id asc"); %>
                      <p style=" text-align:center; font-size:17px;"><%: dt.Rows[0]["top_title"]%></p> 
	 		
            <div class="col-md-8">
            
                <% for (int i = 0; i < dt.Rows.Count; i++)
                 {%>
            
               <div class="col-md-6">
                   <b><%= dt.Rows[i]["title"]%></b><%= dt.Rows[i]["discription"].ToString()%>
                  
                 </div>
                <%} %>
                
             
            </div> 
     
          <div class="col-md-4 col-xs-12">
                <div id="leftside">
                    <span>Connect With >></span>
                    <ul>
                        <li><a class="arialbold" href="https://www.youtube.com/watch?v=oZIQ-oxP790" target="_blank"><img src="/MobiOcean/Content/img/contact_yt.png" alt="Youtube" Title="Youtube" style="width:30px; margin:0px 10px;border-radius:50%;"> Youtube</a></li>
                        <li><a class="arialbold" href="https://www.facebook.com/mobioceansolutions/" target="_blank"><img src="/MobiOcean/Content/img/contact_fb.png" alt="Facebook" Title="Facebook" style="width:30px; margin:0px 10px;border-radius:50%;"> Facebook</a></li>										
                        <li><a class="arialbold" href="https://twitter.com/Mobi_Ocean" target="_blank"><img src="/MobiOcean/Content/img/contact_twi.png" alt="Twitter" Title="Twitter" style="width:30px; margin:0px 10px;border-radius:50%;"> Twitter</a></li>
                        <li><a class="arialbold" href="https://www.linkedin.com/company/13206407" target="_blank"><img src="/MobiOcean/Content/img/contact_li.png" alt="LinkedIn" Title="LinkedIn" style="width:30px; margin:0px 10px;border-radius:50%;"> Linkedin</a></li>	
                        <li><a class="arialbold" href="https://plus.google.com/100872825159864456831" target="_blank"><img src="/MobiOcean/Content/img/google-plus.png" alt="Google+" Title="Google+" style="width:30px; margin:0px 10px;border-radius:50%;"> Google+</a></li>
                        <!--<li><img src="img/wtapp.png" alt="WhatsApp" Title="WhatsApp" style="width:30px; margin:0px 10px;"><a class="arialbold" href="#"> Whatsapp</a></li>
						<br><br>
                      
                    --></ul>
                </div>
          </div>
          <div class=" clearfix"></div>
          
          
          <!-- form -->
          </div>
            <div class="col-md-12"> <div class="dashborder_img" >
            <h3 >Write to Us</h3>
           
            </div>
           <div class="module form1-module" id="form">
              
                       <form action="" method="post">
                       <div class ="col-md-6">
                          <div class=""><label> Name :</label></div>
                          <div class=""><input type="text" id="contactname" class="form-control" placeholder="Name" required ></div>
                          <div class=""> <label>Mobile No : </label></div>
                          <div class=""><input type="text" id="contactmobile" class="form-control" placeholder="Mobile No" required></div>
                          <div class=""> <label>Email ID : </label> </div>
                       
                          <div class=""><input type="email" id="contactmail" class="form-control" placeholder="Email-Id" required></div>
                             </div>
                            <div class ="col-md-6">
                          <div class=""> <label>Company Name : </label></div>
                          <div class=""><input type="text" id="contactcompany" class="form-control" placeholder="Company Name" required></div>
                        
                         <div class=""> <label>Type Of Industry : </label> </div>
                       	<div class=""><input type="text" id="contactindustry" class="form-control" placeholder="Industry Name"></div><div class="clearfix"></div> 
                         <div class=""> <label>Country : </label> </div>
                         <div class=""><input type="text" id="contactcountry" class="form-control" placeholder="Country Name" required></div>
                         </div>
                         <div class="clearfix"></div>
                         <div class ="col-md-12">
                         <div class=""> <label>How may we assist you:</label> </div>
                          <div class=""><textarea rows="4" class="form-control"  placeholder="Type your  message/query" id="cdescription"  style=" 
						    height: 100px;
						    padding: 10px 18px;
						    width: 90%;"></textarea></div><br>   
						    </div>  
						                        
                       <div class ="col-md-12">
                          <div class=""  style="text-align:center">&nbsp; 
						  <br/>
					
						  <button id="contactsubmit" style="text-align:center" >Submit</button>
						  </div>
						  </div>
                       </form>
          
          </div>
        </div><!--
           <div class="col-md-4">
            <img class="img-responsive" src="img/contact.jpg" alt="">
           </div>
           
       --></div>  
    </div>  
</asp:Content>
