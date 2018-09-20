<%@ Page Title="Support" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="MobiSupport.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.MobiSupport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/MobiOcean/Content/css/common.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid">
        <div class="contents"><!-- 
      <img class="img-responsive" src="Admin/upload/<?php echo $carr[4]['image'];?>" alt="" style="margin:20px 0px 0px 0px;"><span>
   	   		
   	   		 -->
   	   		  <h1>Support</h1>
   	   		 <div class="divider"></div><div><a href="<%= MobiOcean.MDM.BAL.Model.Constant.Home%>">Home</a>&nbsp;/&nbsp;<a href="support">support</a></div>
            <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
               // string[] s = HttpContext.Current.Request.Url.LocalPath.ToString().Split('/');
                %>

                       </div>
             <div class="mycontents" id="support">          
       <br><br>
       <p style=" text-align:center; font-size:17px;">Feel free to contact us for support please select the option that suits you the best</p>
       <br><br>
       <div class="text-center">
	       <div class="col-md-3">
               <%System.Data.DataTable dt = new System.Data.DataTable();
                   dt = wb.fetchDisplay("support order by id desc");
                   if (dt.Rows.Count > 0 && string.IsNullOrWhiteSpace(dt.Rows[0]["pdf"].ToString()))
                   { %>
	       	
	                <a href="faq-new.php" class="downbrou"><img class="" src="/MobiOcean/Content/Admin/upload/<%: dt.Rows[0]["image"] %>"  alt="FAQ" title="FAQ"/></a>
	                <div><a href="faq-new.php" class="downbrou" style="color:#337ab7;"><b><%: dt.Rows[0]["contant"] %></b></a></div>
	               <%}
                       else
                       { %>
	                <img class="img-responsive" src="/MobiOcean/Content/Admin/upload/<%: dt.Rows[0]["image"] %>"/>
	                 <div><b><%: dt.Rows[0]["contant"] %></b></div>
	                <%} %>
	       </div>
	       <div class="col-md-3 ">
	       	 <div><img class="" src="/MobiOcean/Content/Admin/upload/<%: dt.Rows[2]["image"] %>" alt="Email" title="Email"/></div>
	                <div><b>Email</b><br><a href="mailto:<%: dt.Rows[2]["contant"] %>" target="_top"><b><%: dt.Rows[2]["contant"] %></b></a></div>
	       </div>
	       <div class="col-md-3">
	       	 <div><img class="" src="/MobiOcean/Content/Admin/upload/<%: dt.Rows[1]["image"] %>" alt="Chat Support" title="Chat Support"/></div>
	                <div><b><%: dt.Rows[1]["contant"] %></b></div>
	       </div>
	       <div class="col-md-3">
	       	 <div><img class="" src="/MobiOcean/Content/Admin/upload/<%: dt.Rows[3]["image"] %>" alt="Helpdesk" title="Helpdesk"/></div>
	                <%string[] a = dt.Rows[3]["contant"].ToString().Split(' '); %>
	                <div><b>Helpdesk</b><br><b><%: a[0]%></b><br><b><%: a[1] %></b></div>
	       </div>
	       <div class="clearfix"></div>
       </div>
            
            
            
            <div class="col-d-2">&nbsp;</div>
            
            <div class="module  col-md-12">
             <div class="dashborder_img" >
            <h3 style=" float: left; width: 100%; margin-top:0px">Service Request</h3>
           
            </div>
           
                <form class="form1-module" runat="server" id="uploadForm">
                  
                   	<div class="col-md-6">
                      <div> Name* : </div>
                      <div><input type="text" id="supportname" name="supportname" class="form-control" placeholder="Name" required ></div>
                      <div> Company Name* : </div>
                      <div><input type="text" id="supportcompany" name="supportcompany" class="form-control" placeholder="Company Name" required /></div>
                      <div> Mobile No* : </div>
                      <div><input type="text" id="supportmobile" name="supportmobile" class="form-control" placeholder="Mobile No" required></div>
                      <div> Email* : </div>
                      <div><input type="email" id="supportmail" name="supportmail" class="form-control" placeholder="Email-Id" required></div>
                      <div> Problem : </div>
                      <div><input type="text" id="supportproblem" name="supportproblem" class="form-control" placeholder="Brief about problem" required></div>
                      </div>
                      <div class="col-md-6">
                      <div> Description : </div>
                      <div><textarea rows="4" class="form-control"  placeholder="Brief about company" id="discription" name="discription" cols="4" style="  border: 1px solid #ddd;
    					height: 100px;
    						padding: 10px 16px;
    								width: 80%;"></textarea></div><br>
                      <div> Attachment/ Error log/ Screen Shot : </div>
                      <div><%--<input type="file" name="errorlog" id="errorlog">--%>
                          <asp:FileUpload ID="errorlog" runat="server" /></div> 
                     <div>
                   		<div class="form-group">
    									Captcha:
      									<div id="divGenerateRandomValues"></div>
        								<label for='message'>Enter Captcha Code :</label>
        								<input type="text" class="form-control" id="captcha_code" name="captcha_code" required/>
        								Can't read the image? click <a href="javascript:;" onclick="Capcha();">here</a> to refresh.
  									</div>
 </div>  
                   
        								</div>
                      <div class="col-md-12 text-center">&nbsp; 
                     <!--   <input type="submit" value="Submit" class="btnSubmit" /> --><div class="col-lg-5"></div><div class="col-md-2">
					  <%--<button type="submit">Submit</button> --%><asp:Button ID="btnGetCaptcha" runat="server" BackColor="#2b3887" OnClick="Submit_Click" Text="Submit" class="btnSubmit"  />
                         </div><div class="col-lg-5"></div>
                      </div>        
                       
           		   </form>
      
       </div>
     </div>       
       	  
	</div>		
 <footer class="foot">
     
   </footer>
    <script type="text/javascript">
        var iNumber = 0;
        function Capcha() {

           iNumber = Math.floor(1000 + Math.random() * 9000);


            $("#divGenerateRandomValues").css({ "background-image": 'url(/MobiOcean/Content/img/Capcha.jpg)', 'width': '150px', 'height': '50px' });
            $("#divGenerateRandomValues").html("<input id='captchaimg1'></input><input type='hidden' id='captchaimg'></input>");
            $("#captchaimg1").css({ 'background': 'transparent', 'font-family': 'Arial', 'font-style': 'bold', 'font-size': '40px' });
            $("#captchaimg1").css({ 'width': '150px', 'border': 'none', 'color': 'black' });
            $("#captchaimg1").val(iNumber);
            $("#captchaimg1").val(iNumber);
            $("#captchaimg1").prop('disabled', true);

        }

        $(document).ready(function () {
            
            Capcha();

            $("#uploadForm").on("submit", function (a) {
                var b = $("#supportname").val().trim(),
                    c = $("#supportcompany").val().trim(),
                    d = $("#supportmobile").val().trim(),
                    e = $("#supportmail").val().trim(),
                    f = $("#supportproblem").val().trim(),
                    g = $("#discription").val().trim(),
                    h = $("#errorlog").val(),
                    i = "admin.mobiocean.com/File/",
                    j = i.concat(h),
                    l = $("#captcha_code").val(),
                    m = /^\d{10}$/;
                var formData = new FormData($(this)[0]);
                var formData1 = new FormData($("#errorlog").get(0));
                if ("" == b || "" == c || "" == d) return alert("Fill all required field"), !1;
                if (!d.match(m)) return alert("Enter valid mobile number"), $("input#supportmobile").focus(), !1;
                if (!d.match(m)) return alert("Enter valid mobile number"), $("input#supportmobile").focus(), !1;
                if (l != iNumber || l=="") {
                    return alert("Captcha is wrong"), $("input#captcha_code").focus(), !1;
                }
                return true;
            });

        });
        
</script>  
</asp:Content>
