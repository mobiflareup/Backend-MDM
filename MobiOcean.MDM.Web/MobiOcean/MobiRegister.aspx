<%@ Page Title="Registration" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="MobiRegister.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
.my-error-class {
    color:#FF0000;
    font-style: normal;
      /* red */
}
.custom-mandatory-input
{
	margin-left:-40px;
}

.required-field-block {
    position: relative;   
}

.required-field-block .required-icon {
    display: inline-block;
    vertical-align: middle;
    margin: -0.25em 0.25em 0em;
    background-color: #E8E8E8;
    border-color: #E8E8E8;
    padding: 0.5em 0.8em;
    color: rgba(0, 0, 0, 0.65);
    text-transform: uppercase;
    font-weight: normal;
    border-radius: 0.325em;
    -webkit-box-sizing: border-box;
    -moz-box-sizing: border-box;
    -ms-box-sizing: border-box;
    box-sizing: border-box;
    -webkit-transition: background 0.1s linear;
    -moz-transition: background 0.1s linear;
    transition: background 0.1s linear;
    font-size: 75%;
}
	
.required-field-block .required-icon {
    background-color: transparent;
    position: absolute;
    top: 0em;
    right: 67px !important;
    z-index: 10;
    margin: 0em;
    width: 30px;
    height: 30px;
    padding: 0em;
    text-align: center;
    -webkit-transition: color 0.2s ease;
    -moz-transition: color 0.2s ease;
    transition: color 0.2s ease;
}

.required-field-block .required-icon:after {
    position: absolute;
    content: "";
    right: 1px;
    top: 1px;
    z-index: -1;
    width: 0em;
    height: 0em;
    border-top: 0em solid transparent;
    border-right: 30px solid transparent;
    border-bottom: 30px solid transparent;
    border-left: 0em solid transparent;
    border-right-color: inherit;
    -webkit-transition: border-color 0.2s ease;
    -moz-transition: border-color 0.2s ease;
    transition: border-color 0.2s ease;
}

.required-field-block .required-icon .text {
	color: #B80000;
	font-size: 26px;
	margin: -3px 0 0 12px;
}
.red-tooltip + .tooltip > .tooltip-inner {padding:10px;z-index:1000;text-align:left;}
.custom-titl
{
	text-decoration:underline;
}
/* Required field END */

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
	 	<div class="content">	
	 		 
          
           			 
           	  <div class="dashborder_img" style="background:#eee;" >
                   <div class="wrap"> 
						<div class="container">
                          <div class="col-md-8 col-md-offset-2"><h3 style="width:100%">Registration Form</h3></div>
							<div class="col-md-8 col-md-offset-2">
							<div class="module form-module">
							  
							  <div clss="formm">
								<form action="" method="post" id="passwordResetForm">	

									<div class="col-md-6">
										<div class="form-group">
											<label>First Name</label>
											<input class="form-control" name="fname" id="fname"  type="text" placeholder="Mandatory" maxlength="25" required/>
										</div>
									</div>
									<div class="col-md-6">
                                     <div class="form-group">
                                        <label>Last Name</label>
                                        <input class="form-control" name="lname" id="Lname" type="text" placeholder="Mandatory" required/>
                                    </div>
									</div>
									<div class="clearfix"></div>	

									<div class="col-md-6">
								    <div class="form-group">
                                        <label>Designation</label>
                                        <input class="form-control" name="designation" id="Desgnitation"  type="text" placeholder="Recommended " />
                                    </div>
									</div>
									<div class="col-md-6"> 
                                      <div class="form-group">
                                        <label>Company ID</label><br>
                                        <input class="form-control" name="userid" id="UserId" type="text" placeholder="Mandatory " required/>
                                    </div>
									</div>
									<div class="clearfix"></div>	

									<div class="col-md-6">
                                     <div class="form-group">
                                        <label>Country</label>
                                        
                                       <select class="form-control" style="width:80%;" name="Country" id="Country"  required >
                                       <option>India</option>
                                        </select>
                                    </div>
									</div>
									<div class="col-md-6" style=""> 
                                   <div class="form-group "> 
                                        <label>Mobile Number</label><br>
                                         <div class = "row">
                                         <div class="col-md-4 col-xs-4">
											<input class="form-control" name="code" id="code" type="text" disabled/>
                                        </div>
                                        <div class="col-md-8 col-xs-8 custom-mandatory-input" style=""> 
											<input class="form-control" name="mobile" id="Mno" type="text" placeholder="Mandatory" required/> 
                                        </div>
										<div class="clearfix"></div>
                                     	
                                       </div>
                                  </div> 
									</div>
									<div class="clearfix"></div>	

									<div class="col-md-6">
										<div class="form-group">
											<label>Number of employees</label>
											<input class="form-control" name="no_of_employee" id="NoofEmployee" type="text" placeholder="Recommended " />
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Company Name</label>
											<input class="form-control" name="company_name" id="CompName" type="text" placeholder="Mandatory " />
										</div>
									</div>
									<div class="clearfix"></div>	

									<div class="col-md-6">
									<div class="form-group">
                                        <label>E-Mail Id</label>
                                        <input class="form-control" id="email" name="email"  type="email" placeholder="Mandatory" required/>
                                    </div>
									</div>
									<div class="col-md-6"> 
									<div class="form-group">
                                        <label>Type of Industry</label>
                                        <input class="form-control" name="type_of_industry" id="TYOINDU"  type="text" placeholder="Recommended" />
                                    </div>
									</div>
									<div class="clearfix"></div>	

									<div class="col-md-6">
										<div class="form-group">
											<label>Password</label><br>
											<div class="required-field-block passwordpolicy" style="text-align:left;">
												<input data-html="true" data-toggle="tooltip" class="red-tooltip" align='left' type="password" id="RPassword" required pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}" name="pwd1" onchange="
											  this.setCustomValidity(this.validity.patternMismatch ? this.title : '');
											  if(this.checkValidity()) form.pwd2.pattern = this.value;
											" placeholder="Mandatory"  required/>
											</div>
										</div>
									</div>
									<div class="col-md-6"> 
										<div class="form-group">
											<label>Confirm password</label><br>
											<div class="required-field-block">
												<input class="form-control" name="cpassword" id="RCPassword" type="password" placeholder="Mandatory " required/>
											</div>
										</div>
									</div>
									<div class="clearfix"></div>



								
								<div class="col-md-6">
                                    <div class="form-group">
    									Captcha:
      									<div id="divGenerateRandomValues"></div>
        								<label for='message'>Enter Captcha Code :</label>
        								<input type="text" class="form-control" id="captcha_code" name="captcha_code" required/>
        								Can't read the image? click <a href="javascript:;" onclick="Capcha();">here</a> to refresh.
  											  <!--   &nbsp;<input name="Submit" type="submit" onclick="return validate();" value="Submit" class="button1"> --> 
                                    </div>
                                   
                                </div>
                                
                                <div class="col-md-6"> 
                                   
                                   
                                   
                                    <div class="form-group">
                                        <label>Address</label><br>
                                        <textarea name="address" class="form-control" placeholder="Recommended" id="Address" style="width:80%; height:100px;"></textarea>
                                    </div>
                                    
										<div class="col-sm-1 padd0"><input type="checkbox" id="terms" name="terms" value="1"  /></div>
                                        <div class="col-sm-7" style="margin-bottom:22px;">I Agree to the MobiOcean <a href="termofuse.php">Terms and Policy</a> <br>
                                        </div>

										  <!--  <input name="Submit" type="submit" value="Submit" class="button1" id="registersubmit" > -->
										  </div> <div class="clearfix"></div>
										    <div class = "form-group col-lg-12 text-center" style="margin-bottom:-25px">
								     <button  id="registersubmit" >Register</button>   
                                 </div>
                                    <div class="clearfix"></div><br><br>
									
                                   
                                    
								    </form>
                                    
                                    
								</div>
								</div>
							  
							  <div class="clear"></div>
							
							 
							</div>
							   </div>
					</div>
					</div>
			</div>
		</div>

       	  
			  
        


     <script src="Content/js/jquery.validate.min.js" ></script>
   
    <script>  
        var iNumber = 0;
        function Capcha() {
            iNumber = Math.floor(1000 + Math.random() * 9000);

            $("#divGenerateRandomValues").css({ "background-image": 'url(/MobiOcean/Content/img/Capcha.jpg)', 'width': '150px', 'height': '50px' });
            $("#divGenerateRandomValues").html("<input id='captchaimg'></input>");
            $("#captchaimg").css({ 'background': 'transparent', 'font-family': 'Arial', 'font-style': 'bold', 'font-size': '40px' });
            $("#captchaimg").css({ 'width': '150px', 'border': 'none', 'color': 'black' });
            $("#captchaimg").val(iNumber);
            $("#captchaimg").prop('disabled', true);
        }
        $(document).ready(function () {  
            
            Capcha();
            $("#registersubmit").click(function () {
                var a = "http://admin.mobiocean.com/api/Deviceinfo/InsertClientManager",
                    b = $("input#email").val().trim(),
                    c = $("input#fname").val().trim(),
                    d = $("input#Lname").val().trim(),
                    e = $("input#CompName").val().trim(),
                    f = c + " " + d,
                    g = $("textarea#Address").val().trim(),
                    h = $("input#Mno").val().trim(),
                    i = $("input#RPassword").val().trim(),
                    j = $("input#RCPassword").val().trim(),
                    k = /\S+@\S+\.\S+/,
                    l = $("input#TYOINDU").val().trim(),
                    n = $("input#UserId").val().trim(),
                    o = $("input#NoofEmployee").val().trim(),
                    p = $("input#Desgnitation").val().trim(),
                    q = $("input#captcha_code").val().trim(),
                    r = $("#Country").val().trim();
                if ("" == c) return alert("Please enter First Name"), $("input#fname").focus(), !1;
                if ("" == d) return alert("Please enter Last Name"), $("input#Lname").focus(), !1;
                if ("" == n) return alert("Please enter Employee Id"), $("input#UserId").focus(), !1;
                if ("" == h) return alert("Please enter Mobile No."), $("input#Mno").focus(), !1;
                if (!/^\d{10}$/.test(h)) return alert("Invalid number; must be ten digits"), $("input#Mno").focus(), !1;
                if ("" == e) return alert("Please enter Company Name"), $("input#CompName").focus(), !1;
                if ("" == b) return alert("Please enter Email"), $("input#email").focus(), !1;
                if (!k.test(b)) return alert("Please enter a valid email address."), !1;
                if ("" == i) return alert("Please enter Password"), $("input#RPassword").focus(), !1;
                if (i.length < 8) return alert("Password should be minimum 8 length of chracter"), $("input#RPassword").focus(), !1;
                var s = i.match(/[*^!@$%&]/) && i.match(/[A-Z]/g) && i.match(/[\d]/g) && i.match(/[a-z]/);
                if (!s) return alert('Password must be Complex Type.eg("Asd123qwe!@#")'), $("input#RPassword").focus(), !1;
                if ("" == j) return alert("Please enter Confirm Password"), $("input#RCPassword").focus(), !1;
                if (i != j) return alert("Your password and confirmation password do not match."), $("input#RPassword").focus(), !1;
                if (!$("#terms").is(":checked")) return alert("Please mark as agree on our terms and condition"), !1;
                var t = 1;
                if ("" == q) return alert("captcha value is empty"), $("input#captcha_code").focus(), !1;
                if ("" != q && q == iNumber) {
                    $.ajax({
                        url: a,
                        type: "POST",
                        data: {
                            EmpCompanyId: n,
                            UserName: f,
                            MobileNo: h,
                            Password: i,
                            ConfirmPassword: j,
                            ClientName: e,
                            EmailId: b,
                            Address: g,
                            FirstN: c,
                            LastN: d,
                            TypeOFIndustry: l,
                            UserId: n,
                            NoOfEmployees: o,
                            Designation: p,
                            IsAgreeTerms: t,
                            CountryId: r
                        },
                        success: function (a, b, c) {
                            m(), alert(a), "You have registered succesfully please go to your mail and activate your account" == a && location.reload();
                            location.reload();
                        },
                        error: function (a, b, c) {
                            m(), alert("error")
                        }

                    })


                }
                else {
                    alert("!Invalid Capcha");
                }
            });
        });  
</script>  
   
	<script type="text/javascript">
	$(document).ready(function () {

		$.validator.addMethod("pwcheckallowedchars", function (value) {
	        return /^[a-zA-Z0-9!@#$%^&*()_=\[\]{};':"\\|,.<>\/?+-]+$/.test(value) // has only allowed chars letter
	    }, "The password contains non-admitted characters");
	    
	    $.validator.addMethod("pwcheckspechars", function (value) {
	        return /[!@#$%^&*()_=\[\]{};':"\\|,.<>\/?+-]/.test(value)
	    }, "The password must contain at least one special character");
	    
		$.validator.addMethod("pwcheckconsecchars", function (value) {
	        return ! (/(.)\1\1/.test(value)) // does not contain 3 consecutive identical chars
	    }, "The password must not contain 3 consecutive identical characters");

	    $.validator.addMethod("pwchecklowercase", function (value) {
	        return /[a-z]/.test(value) // has a lowercase letter
	    }, "The password must contain at least one lowercase letter");
	    
	    $.validator.addMethod("pwcheckrepeatnum", function (value) {
	        return /\d{2}/.test(value) // has a lowercase letter
	    }, "The password must contain at least one lowercase letter");
	    
	    $.validator.addMethod("pwcheckuppercase", function (value) {
	        return /[A-Z]/.test(value) // has an uppercase letter
	    }, "The password must contain at least one uppercase letter");
	    
	    $.validator.addMethod("pwchecknumber", function (value) {
	        return /\d/.test(value) // has a digit
	    }, "The password must contain at least one number");
	    
	    
	    
	    
	    
	    $('#passwordResetForm').validate({

	    	errorClass: "my-error-class",
	    	   validClass: "my-valid-class",
	        // other options,
	        rules: {
	            "pwd1": {
	                required: true,
	                pwchecklowercase: true,
	                pwcheckuppercase: true,
	                pwchecknumber: true,
	                pwcheckconsecchars: true,
	                pwcheckspechars: true,
	                pwcheckallowedchars: true,
	                minlength: 8,
	                maxlength: 20
	            },
	            cpassword: {
	                required: true,
	                equalTo: "#RPassword"
	            },
	        }
	    });


		
		var formURL='http://admin.mobiocean.com/api/Permises/Country';
		 $.ajax({
		        url: formURL,
		        type: "GET",
		        success: function (res) {
		        	var obj = JSON.parse(res);
		           $("#Country").html(""); 
                   $("#Country").append(
                            $('<option></option>').val(0).html("Select")
                            );
                   $.each(obj,function(i,data){
                	   $("#Country").append(
                               $('<option></option>').val(data.CountryId).html(data.CountryName)
                               );
                   })
		        }
		    });

		 $('#Country').on('change',function(){
		        var countryID = $(this).val();

		      
		      if(countryID){
		            $.ajax({
		                type:'Get',
		                url:'http://admin.mobiocean.com/api/Permises/Country',
		               // data:'country_id='+countryID,
		                success:function(html){
		                	var obj = JSON.parse(html);

		                		 $("#code").val(obj[countryID-1].PhoneCode);
		                    
		                }
		            }); 
		        }else{
		            $('#state').html('<option value="">Select country first</option>');
		            $('#city').html('<option value="">Select state first</option>'); 
		        }
		    }); 


});
			
	$(function() {
    $('.passwordpolicy input').tooltip({
        tooltipClass: "custom-tooltip",
        placement: 'top',
			align:'left',
		title: '<b class="custom-titl">PASSWORD POLICY</b><br/>1.Minimum 8 Characters.<br/>2.At least one Numeric.<br>3.One Upper Case Character.<br>4.One Lower Case Character.<br>5.One Special Charcter.',
       // title: 'Password <br> must contain at least 6 characters, including UPPER/lowercase and numbers',
		trigger: "focus"
        });

});
	</script>
</asp:Content>
