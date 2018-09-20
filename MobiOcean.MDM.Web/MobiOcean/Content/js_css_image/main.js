function showNav(){}
$(document).ready(function()
{
	if($(window).width()>1200)
	{
		hideSidebar();
	}
	$("#tabs > ul > li").each(function()
	{
		if($(this).find("ul").size()>0)
		{
			$(this).css({"margin":"0 20px","background-image":"url('"+WEB_ROOT+"/mint-project/style/images/white-arrow-down.png')"});
			$(this).find("div").css({"padding":"8px 30px 8px 12px"});
		}
	});
	$("#sidebar > ul > li").each(function()
	{
		if($(this).find("ul").size()>0)
		{
			$(this).css({"background-image":"url('"+WEB_ROOT+"/mint-project/style/images/white-arrow-down.png')"});
		}
	});
	$("#tabs_parent-login, #sidebar_parent-login").html("<div onclick='parentsLoginPop();'>Parent Login</div>");
	$("#tabs_school-login, #sidebar_school-login").html("<div onclick='window.location.href=\"https://school.mobileguardian.com/#/login\"'>School Login</div>");
	$("#tabs_free-trial, #sidebar_free-trial").html("<div onclick='registerPop();'>FREE Trial</div>");
	$("#foot_sign-up").html("<span onclick='registerPop();'>Sign Up</span>");
	$(window).resize(function()
	{
		if($(window).width()>1200)
		{
			hideSidebar();
		}
		$("#tabs > ul > li").each(function()
		{
			if($(this).find("ul").size()>0)
			{
				$(this).css({"margin":"0 20px","background-image":"url('"+WEB_ROOT+"/mint-project/style/images/white-arrow-down.png')"});
				$(this).find("div").css({"padding":"8px 30px 8px 12px"});
			}
		});
	});
});
function showSidebar()
{
	$("#sidebar_area").show();
	$("#overdiv").animate({"right":"200px"},1000,function()
	{	
		$("#sidebar_area").css({"height":$(document).outerHeight(true)});
		$("#sidebar_image").removeAttr("onclick");
		$("#overdiv").bind("click",function()
		{
			hideSidebar();
		});
	});
}
function hideSidebar()
{
	$("#overdiv").unbind("click").animate({"right":"0"},function()
	{
		$("#sidebar_area").hide();
		$("#sidebar_image").attr("onclick","showSidebar();");
	});
}
function loginPop()
{
	closePopUp();
	popUp2(600,250);
	$.post(WEB_ROOT+"/mint-project/ajax/pop.ajax.php",{rs:"login"},function(data)
	{
		$('#popup2').html(data);
	});
}
function parentsLoginPop()
{
	closePopUp();
	popUp2(600,450);
	$.post(WEB_ROOT+"/mint-project/ajax/pop.ajax.php",{rs:"parent_login"},function(data)
	{
		$('#popup2').html(data);
	});
}
function forgotPop()
{
	closePopUp();
	popUp2(600,220);
	$.post(WEB_ROOT+"/mint-project/ajax/pop.ajax.php",{rs:"forgot"},function(data)
	{
		$('#popup2').html(data);
	});
}
function registerPop()
{
	closePopUp();
	popUp2(600,250);
	$.post(WEB_ROOT+"/mint-project/ajax/pop.ajax.php",{rs:"register"},function(data)
	{
		$('#popup2').html(data);
	});
}
function parentRegisterPop()
{
	closePopUp();
	popUp2(600,650);
	$.post(WEB_ROOT+"/mint-project/ajax/pop.ajax.php",{rs:"parent_register"},function(data)
	{
		$('#popup2').html(data);
	});
}
function popUp2(wid,hei)
{
	addEl("div","blocker","appends","blocker");
	addEl("div","popup2","appends");
	$("#appends").find(".blocker").attr("onclick","closePopUp();");
	if($(window).width()>600)
	{
		$("#popup2").css({"width":wid,"height":hei,"margin-left":(($("#blocker").width()-parseInt(wid))/2)+"px","top":(($(window).height()-parseInt(hei))/2)+"px"});
	}
	else
	{
		$("#popup2").css({"width":"auto","height":"auto"});
		$("html,body").animate({scrollTop:$("#overdiv").position().top},"slow");
	}
	$(window).resize(function()
	{
		if($(window).width()>600)
		{
			$("#popup2").css({"width":wid,"height":hei,"margin-left":(($("#blocker").width()-parseInt(wid))/2)+"px","top":(($(window).height()-parseInt(hei))/2)+"px"});
		}
		else
		{
			$("#popup2").css({"width":"auto","height":"auto"});
			$("html,body").animate({scrollTop:$("#overdiv").position().top},"slow");
		}
	});
}
function closePopUp()
{
	rmEl("blocker","appends");
	rmEl("popup2","appends");
}
var timer;
function animateFeatures()
{
	moving=true;
	$("#home_features_area .home_features_inner").stop().animate({"left":"-"+$("#home_features_area .home_features").outerWidth(true)+"px"},1000,function()
	{
		$("#home_features_area .home_features").first().appendTo("#home_features_area .home_features_inner");
		$("#home_features_area .home_features_inner").css("left",0);
		timer=setTimeout(animateFeatures,1000);
		moving=false;
	});
}
var moving=false;
function scrollMultiFields(dir)
{
	if(!moving)
	{
		clearTimeout(timer);
		var num=$("#home_features_area .home_features").size();
		if(dir)
		{
			moving=true;
			$("#home_features_area .home_features_inner").animate({"left":"-="+$("#home_features_area .home_features").outerWidth(true)},"slow",function()
			{
				$("#home_features_area .home_features").first().appendTo("#home_features_area .home_features_inner");
				$("#home_features_area .home_features_inner").css("left","+="+$("#home_features_area .home_features").outerWidth(true));
				timer=setTimeout(animateFeatures,1000);
				moving=false;
			});
			$("#home_features_area .selected").removeClass("selected").next().addClass("selected");
		}
		else
		{
			moving=true;
			$("#home_features_area .home_features").last().prependTo("#home_features_area .home_features_inner");
			$("#home_features_area .home_features_inner").css("left","-="+$("#home_features_area .home_features").outerWidth(true));
			$("#home_features_area .home_features_inner").animate({"left":"+="+$("#home_features_area .home_features").outerWidth(true)},"slow",function()
			{
				timer=setTimeout(animateFeatures,1000);
				moving=false;
			});
			$("#home_features_area .selected").removeClass("selected").prev().addClass("selected");
		}
	}
}
function showFeatures(ele,tb)
{
	$(ele).removeAttr("onclick").attr("onclick","hideFeatures(this,"+tb+");").find(".multiField_2").css({"background-image":"url('"+WEB_ROOT+"/mint-project/style/images/pink-arrow-up.png')"});
	$(ele).next().animate({"height":tb+"px"},"slow");
}
function hideFeatures(ele,tb)
{
	$(ele).removeAttr("onclick").attr("onclick","showFeatures(this,"+tb+");").find(".multiField_2").css({"background-image":"url('"+WEB_ROOT+"/mint-project/style/images/green-arrow-down.png')"});
	$(ele).next().animate({"height":"0"},"slow");
}
var validate=new Array();
validate[0]="&nbsp;";
validate[1]="Mobile Guardian account email";
validate[2]="This will be your account username";
function validatePopForm(fname)
{
	var flag=true;
	var form_name="";
	if(fname)
		form_name="#"+fname+" ";
	$(form_name+".validate").each(function()
	{
		$(this).removeClass("invalid");
		if(validate.indexOf(this.value)>0 || !this.value)
		{
			$(this).addClass("invalid");
			flag=false;
		}
		else
			$(this).removeClass("invalid");
	});
	if(fname=="register_form")
	{
		if(document.getElementById("terms_and_conditions").checked)
			$("#terms_and_conditions").next().removeClass("invalid_check");
		else
		{
			$("#terms_and_conditions").next().addClass("invalid_check");
			flag=false;
		}
	}
	if(!flag)
		return false;
	if(fname=="login_form")
	{
		$.post(WEB_ROOT+"/mint-project/mobiflock/mobiflock_interface.php?action=login&ajax=1",{username:$("#username").val(),password:$("#password").val()},function(data,textStatus)
		{
			data=JSON.parse(data);
			if(textStatus=="success")
			{
        		if(data["status"]=="0")
				{
        			performLogin2(data["message"]);
        			return;
        		}
				else
        			$("#form_error").html(data["message"]);
        	}
			else
        		$("#form_error").html("There was a problem processing your request, please try again");
		});
	}
	else if(fname=="forgot_form")
	{
		$.post(WEB_ROOT+"/mint-project/mobiflock/mobiflock_interface.php?action=forgot&ajax=1",{username:$("#username").val()},function(data,textStatus)
		{
			data=JSON.parse(data);
			if(textStatus=="success")
			{
        		if(data["status"]=="0")
				{
        			$("#forgot_form").hide();
					$("#form_sent").fadeIn("slow");
        			return;
        		}
				else
        			$("#form_error").html(data["message"]);
        	}
			else
        		$("#form_error").html("There was a problem processing your request, please try again");
		});
	}
	else if(fname=="register_form")
	{
		$.post(WEB_ROOT+"/mint-project/mobiflock/mobiflock_interface.php?action=register&ajax=1",{first_name:$("#first_name").val(),last_name:$("#last_name").val(),email_address:$("#email_address").val(),confirm_email_address:$("#confirm_email_address").val(),password:$("#password").val(),terms_and_conditions:$("#terms_and_conditions").val()},function(data,textStatus)
		{
			data=JSON.parse(data);
			if(textStatus=="success")
			{
				console.log(data["status"]);
				if(data["status"]=="0")
				{
					performLogin(data["message"]);
					return;
				}
				else
				{
					$("#form_error").html(data["message"]);
					if(data["fields"])
					{
						$(".form_error").html("");
						$.each(data["fields"],function(key,val)
						{
							fieldKey="#"+key;
							if(key!="terms_and_conditions")
								$(fieldKey).next(".form_error").html(val);
							else
								$(fieldKey).next().next(".form_error").html(val);
						});
					}
				}
			}
			else
				$("#form_error").html("There was a problem processing your request, please try again");
		});
	}
}
function performLogin(url){
	$("#loginForm").attr("action",url);
    $("#loginUsername").attr("value",$.trim($("#email_address").attr("value")));
    $("#loginPassword").attr("value",$("#password").attr("value"));
    $("#loginForm").submit();
}
function performLogin2(url)
{
	$("#loginForm").attr("action",url);
    $("#loginUsername").attr("value",$.trim($("#username").attr("value")));
    $("#loginPassword").attr("value",$("#password").attr("value"));
    $("#loginForm").submit();
}
function validateForm(fname)
{
	var flag=true;
	var form_name="";
	if(fname)
		form_name="#"+fname+" ";
	$(form_name+".validate").each(function()
	{
		if(!this.value)
		{
			$(this).addClass("invalid");
			flag=false;
		}
		else
			$(this).removeClass("invalid");
	});
	if(fname=="school_contact_form")
	{
		if(document.getElementById("marketing").checked)
			$("#marketing").next().removeClass("invalid_check");
		else
		{
			$("#marketing").next().addClass("invalid_check");
			flag=false;
		}
	}
	return flag;
}
