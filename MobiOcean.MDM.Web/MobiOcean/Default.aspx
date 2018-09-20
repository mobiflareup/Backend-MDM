<%@ Page Title="Enterprise Mobile management software, Mobility Management solution - MobiOcean" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
		
		<div class="containerfull mobibluebg">
			<div class="cotainer-fluid">
				<div class="main ">
		
			<div class="content">
				<div id="myCarousel1" class="carousel slide">
				</div>
                
				<div id="myCarousel2" class="carousel slide  wow bounceInLeft">
                                
                   <ol class="carousel-indicators">
                      <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
                          System.Data.DataTable dt = new System.Data.DataTable();
                          dt = wb.fetchDisplay("slider order by id asc");
                          for (int i = 0; i < dt.Rows.Count; i++)
                          {
                              if (i == 0)
                              { %>
                       <li class="active" data-slide-to="<%=i%>" data-target="#myCarousel2"></li>
                       <%
                           }
                           else
                           { %> <li data-slide-to="<%=i%>" data-target="#myCarousel2"></li>
                       <%}
                           }%>
                                 
                    </ol>
                              
					<div class="carousel-inner" data-interval="false">
                    <!-- first slide start-->
                    
                    <%for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == 0)
                            { %>
                    	
						<div class="active item custom-banner">               
							<div class="custom-main-banner" alt="Mobi Ocean" title="Mobi Ocean" style="background: url(/MobiOcean/Content/Banner/<%: dt.Rows[i]["image"] %>) no-repeat  center bottom; background-size:100% 100%;min-height:455px;"> 
                             
								<div class="wrapp " > 
									<div class="container" >
										<div class="roww resbanner2">
											<div class="col-sm-12">
												<div class="col-sm-8">
													<div class="centerplayer">
                                                   	
													</div>
												</div>
											</div>
										</div>
										<div class="roww ">
											<div class="col-sm-12 custom-banner-text" style="padding-left:0px;position:absolute;bottom:10px;left:8%;">
												
												<div class="clear"></div>
												
												<p class="banner_pd arialregular" >
                                                    <a href="/MobiOcean/Content/pdf/<%: string.IsNullOrWhiteSpace(dt.Rows[i]["download_brochure"].ToString()) ? "broucher.pdf" : dt.Rows[i]["download_brochure"].ToString()%>" class="downbrou mobigreen" target="_blank"><span class="glyphicon glyphicon-download"></span>&nbsp;&nbsp;Download Brochure</p></a></p>
                                               <div class="clear"></div>
												<p class="banner_pd banner_pd2 arialregular resbanner2 mobigreen">MobiOcean is available for Android.</p>
												<div class="clear"></div>
												<p class="banner_pd banner_pd2 abc resbanner2">
													<!--<a class="firstbtn" style="cursor:default" href="javascript:;">
														<img src="img/google-play.png" />
													</a>-->
													<span class="mobigreen arialregular" style="display:block;  float:left;
    margin-top: 10px;font-size:13px;clear:both;" >Coming Soon For IOS and Windows</span>
													<div class="clear"></div>
													
													
												</p>
												
											</div>
										</div>
										<div class="clear"></div>
									</div>
								</div>
                                
							</div>
						</div>
						<%}
                            else
                            { %>
						<!-- first slide end -->	

                   
                    
                     <!-- third slide start-->
						<div class="item">                        
							<div class="custom-main-banner" alt="Mobi Ocean" title="Mobi Ocean" style="background: url(/MobiOcean/Content/Banner/<%: dt.Rows[i]["image"] %>) no-repeat  center bottom; background-size:100% 100%;min-height:455px;"> 
                             <!-- caption start --> 
                                      <div class="item ">
							
                                   
                                </div>
										 <!-- caption end -->
								<!--<img class="resbanner" src="images/ban1.jpg" />-->
								<div class="wrapp ">   
									<div class="container" >
										<div class="roww resbanner2">
											
											<div class="col-sm-12 ">
												
												<div class="col-sm-8">
													<div class="centerplayer">
                                                  
													</div>
												</div>
											</div>
										</div>
										<div class="roww ">
											<div class="col-sm-12 custom-banner-text" style="padding-left:0px;position:absolute;bottom:10px;left:8%;">
												
												<div class="clear"></div>
												
												<div class="clear"></div>
												<p class="banner_pd banner_pd2 arialregular resbanner2 mobigreen">MobiOcean is available for Android.</p>
												<div class="clear"></div>
												<p class="banner_pd banner_pd2 abc resbanner2">
													<!--<a class="firstbtn" style="cursor:default" href="javascript:;">
														<img src="img/google-play.png" />
													</a>-->
													<span class="mobigreen arialregular" style="display:block;  float:left;
														margin-top: 10px;font-size:13px;clear:both;">Coming Soon For IOS and Windows</span>
													<div class="clear"></div>
													
													
												</p>
												
											</div>
										</div>
										<div class="clear"></div>
									</div>
								</div>
                                
                               
							</div>
							
							
						</div>
					<%}
                            }
                        %>
					<!-- third slide end -->
                   
							
							
							
							<a class="carousel-control left" href="#myCarousel2" data-slide="prev">&lsaquo;</a>
					<a class="carousel-control right" href="#myCarousel2" data-slide="next">&rsaquo;</a>
					</div>
				</div>
				
			</div>
					
			</div>	
			</div>	
		</div>	
	
				
			
            
			<!-- ================= Mobiocean Features start here ================= -->
       <!-- ================= Mobiocean Features start here ================= -->
        <div  style=" background: #fafafa;" >    
            <div class="container-fluid">
				<div class="wrapp" id="mywrap">
               
                           <%string[] a = wb.fetchDisplay("features order by id asc").Rows[0]["title"].ToString().Split(' '); %>                     	
					<h2 class="mobblue arialbold"><%: a[0] %> <span class="mobiorange arialregular"><%: a[1] %></span></h2>
					<h4 class="arilregular"><%System.Data.DataTable dt1 = new System.Data.DataTable();
                                                dt1 = wb.fetchDisplay("features order by id asc"); %><%= dt1.Rows[0]["description"].ToString() %></h4>
				
                         
                   <!--  first set start here -->     
                      <div class="col-md-4">  
                          <%for (int j = 1; j < 5; j++)
                             {%> 
                            <div class=" col-md-12 wow bounceInLeft" > 
								<div class="text-center">
									<img src="/MobiOcean/Content/Admin/upload/<%: dt1.Rows[j]["image"] %>" alt="<%: dt1.Rows[j]["title"] %>" title="<%: dt1.Rows[j]["title"] %>">
								</div>
                                <h5>
                                	<a href="<%: dt1.Rows[j]["link"] %>" >
                                     	<i class="fa fa-hand-o-right">&nbsp;</i><%: dt1.Rows[j]["title"] %>
                                    </a>
                                </h5>
                                <%string id="id_"+j; %>
                                <p><%= dt1.Rows[j]["description"].ToString()%>...<span><a href="<%: dt1.Rows[j]["link"] %>" class="">Read more</a></span>
                                </p>
                            </div>
                            <%} %>
                            
                      </div> 
                        
                 <!--  first set end here -->     
                 
                   
                 <!--  second set start here  style=" margin: 2em 0em 2em 0em;"-->     
                      <div class="col-md-4">    
                          
                           <img src="/MobiOcean/Content/Admin/upload/<%: dt1.Rows[0]["image"] %>" alt="Mobility Management Solutions" title="Mobility Management Solutions" style="max-width:100%;">
                      </div>
                 <!--  second set end here -->      
                  
                 <!--  third set end here -->     
                      <div class="col-md-4" >    
                         <%for (int j = 5; j < 9; j++)
                             {%> 
                            <div class="col-md-12 wow bounceInRight" >

                                <div class="text-center">
									<img src="/MobiOcean/Content/Admin/upload/<%: dt1.Rows[j]["image"] %>" alt="<%: dt1.Rows[j]["title"] %>" title="<%: dt1.Rows[j]["title"] %>">
								</div>
                                <h5>
                                	<a href="<%: dt1.Rows[j]["link"] %>" >
                                     	<i class="fa fa-hand-o-right">&nbsp;</i><%: dt1.Rows[j]["title"] %>
                                    </a>
                                </h5>
                                <%string ids="id_"+j; %>
                                <p>
                                    <%= dt1.Rows[j]["description"].ToString()%>...<span>
                                	<a href="<%: dt1.Rows[j]["link"] %>" class="">Read more</a></span>
                                	
                                </p>

								
                            </div>
                            <%} %>
                            
					</div>
			  <!--  third set end here -->
				
			    </div>
			  <div class="clearfix"></div>
          </div> 
          
       </div>     
          <!-- ================= Mobiocean Features end here ================= --> 
          <!-- ================= Mobiocean Features end here ================= --> 
            
            
			<div class="containerfull mobibluebg wow zoomI" >
	<div class="container-fluid">
	<div class="customers_gridssss mobibluebg" >
	<div class="section group">
	<div id="home_steps_area" >
	<div class="roww">
	<div class="col-sm-12">
	<div class="mint_field whitecolor arialbold" id="home_steps_title"><%System.Data.DataTable dt3 = new System.Data.DataTable();
                                                                           dt3 = wb.fetchDisplay("product_demo order by id asc");    %><%: dt3.Rows[0]["title"] %></div>
	</div>
	</div>
	<div class="container-fluid " >
	<div class="wrapp">
	<div class="roww">
	<div class="col-md-6 col-sm-12 whitecolor paradivhome wow bounceInRight" >
	<div class="myriadregular" style="width:94%;text-align:left:word-wrap:break-word;" >
         <%= dt3.Rows[0]["description"].ToString()%>
    </div>
	</div>
	<div class="col-md-6 col-sm-12wow bounceInLeft"><p class="padding_10" >
	<a  data-toggle="modal" data-target="#myModal3" href="javascript:;"><img alt="" src="/MobiOcean/Content/upload/<%: dt3.Rows[0]["image"] %>" alt="<%: dt3.Rows[0]["title"] %>" title="<%: dt3.Rows[0]["title"] %>"></a>
	 
<%string[] p = dt3.Rows[0]["link"].ToString().Split('?');
    string p1 = p[1].Substring(2); %>
	<a href="http://www.youtube.com/v/<%:p1%>?version=3&amp;autoplay=1" class="play_icon fancybox-media pdemo" rel="media-gallery"></a> 
    
   <!---- ================== product demo start here ====================== -->
<div id="myModal2" class="modal fade" role="dialog">
  <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content" style="z-index:-99; margin-top:230px !important">          
            <div class="md-modal md-effect-13 md-show" id="modal-13">
                  <div class="md-content pductdemo " id="emo_box">
                      <h3>Subscribe!</h3>
                      <div>
                          <p>Enter Email to see the product demo!</p>
                          <p><input id="pdemovid1" type="email" value="aaa@gmail.com" placeHolder="Enter Email" class="form-control" /></p>
                          <a class="demoreq btn-primary btn waves-effect waves-light" href="javascript:;" >Submit</a> 
                          <button type="button" class="close btn-primary" data-dismiss="modal">&times;</button>
                      </div>
                </div>           
           </div>   
       </div>
  </div>
</div>


<!---- ================== product demo modal end here ====================== -->
          
    
    
	
	</p>
	</div>
	</div>
	
	</div>
	</div>
	</div>
	</div>
	</div>
	</div>
	</div>
			
		
            
            <div class="containerfull lightgreybgg">	
				<div class="container-fluid">
					<div class="customers_gridssss " >
					<div class="section group ">
						<div id="home_steps_area" >
							<div class="roww">
								<div class="col-sm-12"><%System.Data.DataTable dt4 = new System.Data.DataTable();
                                                           dt4 = wb.fetchDisplay("get_started order by id asc"); %><%System.Text.RegularExpressions.Regex regHtml = new System.Text.RegularExpressions.Regex("<[^>]*>"); %>
									<div class="mint_field arialbold"  id="home_steps_title2"><%: dt4.Rows[0]["title"] %> <div class="arialregular mobiorange" style="display:inline;"><%=regHtml.Replace(dt4.Rows[0]["description"].ToString(),"") %></div></div>
								</div>
							</div>
							<div class="row">
								<div class="col-sm-12" >
									<div style="width:100%;margin:0px 0px" >
										<%for (int i = 1; i < 4; i++)
                                                 { %>
                                        <div class="home_multi_area wow bounceInLeft col-md-4 col-sm-12 col-xs-12 " style="position: relative;">
											<div class="home_multi multi_field_area mobibluebg col-md-12 col-sm-12 " id="witupdown">
												<div class="home_multi_left myriadbold col-md-2 col-sm-2 col-xs-2"  id="witbox">1</div>
												<div class="home_multi_right col-md-5 col-sm-5 col-xs-8"  id="witpos">
													<div class="multiField_1 mint_multiField mint_field mf_123456789010" id="multi_field_123456789010"><a class="myriadbold" href="free-trial.php"  style="font-size:33px;"><%: dt4.Rows[i]["title"] %></a>
													<br>
                                                    <span id="multi_field_12345678908" class="myriadbold" style="font-size:14px;"><%= dt4.Rows[i]["description"].ToString()%></span>
													</div>
                                                    
													</div>
													<div class="multiField_3 mint_multiField mint_field mf_123456789012  col-md-3 col-sm-3" id="multi_field_123456789012" style="display:block;width: auto; position: absolute; top: -20px; right: 10px;">
														<img class="topimg" src="/MobiOcean/Content/upload/<%: dt4.Rows[i]["image"] %>" alt="<%: dt4.Rows[i]["title"] %>" title="<%: dt4.Rows[i]["title"] %>">
													</div>
												<%if (i == 1)
                                                    { %>
												<div class="clear"></div>
                                                <%} %>
										</div>
                                        </div>
                                        <%} %>
                                       <div class="clear"></div>
									</div>
									
								</div>
							</div>
						</div>
					</div>
				</div>  
				</div>
			</div>
				<div class="clear"></div>
			
<!-- product slider end here -->
<div class="containerfull lightgreybgg">	
				<div class="container">           
    <div id="home_steps_title" class="mint_field arialbold">Solution
    	<div style="display:inline;" class="arialregular mobiorange">Portfolio</div>
    </div> 
    <div id="home_features_area" >
			<div class="home_features_outer_area" id="our_techinacle">
				<div class="home_features_outer" >
					<div class="home_features_left" onclick="scrollMultiFields(0);">
						<img src="/MobiOcean/Content/js_css_image/left-arrrow.png" style="margin-top:140px;" width="60px"/>
					</div>
					
					
					
					<div class="home_features_inner" style="width: 3600px; left: -140.595px;">
                        <%System.Data.DataTable dt5 = new System.Data.DataTable();
                            dt5 = wb.fetchDisplay("footer_logo order by id desc"); %>
                        <%for (int i = 0; i < dt5.Rows.Count; i++)
                            { %>
						
						<div class="home_features">
							<div class="cuadro_intro_hover text-center">
								<div class="multiField_1 ">
								<p class="text-center" style=" margin-top:20px;">
									<img src="/MobiOcean/Content/Admin/upload/<%: dt5.Rows[i]["image"] %>"  alt="<%: dt5.Rows[i]["name"] %>" title="<%: dt5.Rows[i]["name"] %>">
									</p>
								</div>
								
								<div class="caption">
									<div class="blur"></div>
									<div class="caption-text">
										<div class="multiField_2">
											<h5  style="border-top:2px solid white; color:#fff; border-bottom:2px solid white; padding:16px;"><%: dt5.Rows[i]["name"] %></h5>
											<p style="color:#fff;padding:15px 0px;"><a href="<%: dt5.Rows[i]["link"] %>" style="text-decoration:none; color:#fff; ">Read More</a></p>
										</div>
									</div>
								</div>
							</div>
						</div><%} %>
							
							
					</div>
					<div class="home_features_right" onclick="scrollMultiFields(1);"><img src="/MobiOcean/Content/js_css_image/right-arrow.png" alt="Right Arrow" style="margin-top:140px;" width="60px"/></div>
				</div>
			</div>
		</div>
	</div>  <br>
		
		<!-- Solution Portfolio 
	 
	
	<!-- End Here -->
</div>  
 
<!-- marquee end -->

<!-- marquee Js start here --> 
        
   
   
                
<div class="containerfull mobibluebg ">
    <div class="container-fluid">
        <div class="content_bottom mobibluebg"  id="customers">
            <div class="wrap">  
                <div class="ad728x90" style="text-align:center">
                    
                    
                </div>          <%System.Data.DataTable dt6 = new System.Data.DataTable();
                                    dt6 = wb.fetchDisplay("customer order by id asc");%>
                <h2 class="mtop whitecolor arialbold" ><%:dt6.Rows[0]["title1"] %>&nbsp;<span class="arialregular mobiorange" style="color:#f16a27;font-weight:bold;"><%:dt6.Rows[0]["title2"] %></span></h2>
            <br><br>
                
                
                <div class="">
                    <%for (int i = 1; i < 4; i++)
                        { %>
					<div class="col-lg-4">
						<div class="testmi-1">
							<p><img class="img-circle "  src="/MobiOcean/Content/upload/<%:dt6.Rows[i]["image"] %>" alt="<%:dt6.Rows[i]["title1"] %>" title="<%:dt6.Rows[i]["title1"] %>" style="width: 130px;height:130px;padding:5px;border:3px solid #fff"/></p>
							<h3 class="" ><a href="#"  class="mobiorange"><%:dt6.Rows[i]["title1"] %></a></h3>
							<small class="whitecolor " ><%:dt6.Rows[i]["title2"] %></small>
							<div class="customer_desc wow bounceInLeft">
                                <p class="whitecolor arialregular">&quot;<%= dt6.Rows[i]["description"].ToString()%>.&quot;</p>
                                <span class="testimonial-arrow"> </span>
                            </div>
						</div>
					</div>
                    <%} %>
					<div class="clearfix"></div>
				</div>
                
            </div>             
        </div>
    </div>
</div>
   
	
	
       </asp:content>
     <asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
	<script type="text/javascript">
	$(document).ready(function(){
				 $(".home_features").mouseover(function(){

					 $(".home_features_inner").stop();
			});

			
				 $(".home_features").mouseleave(function(){
					// alert('stop');
					 setTimeout(animateFeatures,1000);
				 });
});


        marqueeInit({
            uniqueid: 'mycrawler2',
            style: {
                //'padding': '20px',
                //'width': '980px',
                //'height': '180px'
            },
            inc: 10, //speed - pixel increment for each iteration of this marquee's movement
            mouse: 'cursor driven', //mouseover behavior ('pause' 'cursor driven' or false)
            moveatleast: 2,
            neutral: 150,
            savedirection: true,
            random: true
        });
        </script>

</asp:Content>
