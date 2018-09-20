
	// <![CDATA[
	$(document).ready(function()
	{
		setTimeout(animateFeatures,4000);
				if($("#head_image").find("img"))
		{
			$("#home_content").css({"background-image":"url('"+$("#head_image").find("img").attr("src")+"')"});
			$("#head_image").remove();
		}
		$("#home_text").append("<br/><br/><span class='green_button' onclick='registerPop();'>FREE Trial</span>");
		if($(window).width()>1024)
			$("#home_small_text").css({"margin":"0","position":"absolute","bottom":"-5px","right":"0"});
				var xx=1;
		$("#home_widget_area .mint_widget").each(function()
		{
			if(xx%3==0)
				$(this).after("<div class='clear'></div>").css({"margin-right":"0"});
			xx++;
		});
		$("#home_features_area").each(function()
		{
			var num=$(this).find(".home_features").size();
			var wid=$(this).find(".home_features").outerWidth(true);
			$(this).find(".home_features_inner").css({"width":(num*wid)+"px"});
		});
		var yy=1;
		$("#home_multi_area .home_multi").each(function()
		{
			$(this).find(".multiField_1").before("<div class='home_multi_left'>"+yy+"</div><div class='home_multi_right'></div>");
			$(this).find(".multiField_1").appendTo($(this).find(".home_multi_right"));
			$(this).find(".multiField_2").appendTo($(this).find(".home_multi_right"));
			$(this).find(".multiField_3").appendTo($(this).find(".home_multi_right"));
						$(this).css({"min-height":"120px","max-height":"120px"});
			$(this).wrap("<div class='home_multi_area'></div>").find(".home_multi_right").css({"position":"relative"});
			$(this).parent().css({"position":"relative"});
			$(this).css({"width":($(this).parent().width()-30)+"px"}).find(".multiField_3").css({"width":"auto","position":"absolute","top":"-20px","right":"20px"});
			$(this).parent().append("<div class='home_multi_arrow'></div>");
			if(yy%3==0)
			{
				$(this).css({"width":($(this).parent().width())+"px"});
				$(this).parent().find(".home_multi_arrow").remove();
				$(this).parent().after("<div class='clear'></div>");
			}
			yy++;
			$(this).parent().find(".home_multi_arrow").css({"right":"-"+($(this).height()/2)+"px"});
					});
		$(window).resize(function()
		{
						if($(window).width()>1024)
				$("#home_small_text").css({"margin":"0","position":"absolute","bottom":"-5px","right":"0"});
			$("#home_multi_area .home_multi").each(function()
			{
				$(this).css({"width":($(this).parent().width()-30)+"px"});
				$(this).parent().find(".home_multi_arrow").css({"right":"-"+($(this).height()/2)+"px"});
			});
					});
	});
	// ]]>
