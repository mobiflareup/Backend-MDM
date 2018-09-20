	// <![CDATA[
	$(window).load(function()
	{
		var device=$.browser.device=(/android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase()));
				if(!device)
		{
			if($(window).width()>1200)
			{
				$("#header").css({"position":"fixed","top":"0","z-index":"2"});
				$("#main").css({"margin-top":$("#header").outerHeight(true)+"px"});
				$(window).resize(function()
				{
					$("#main").css({"margin-top":$("#header").outerHeight(true)+"px"});				
				});
			}
		}
		$("#foot_image").each(function()
		{
			var tot=$(this).width();
			var num=$(this).find("li").size()-1;
			var wid=$(this).find("ul").width();
			$(this).find("li").css({"margin-right":(Math.floor((tot-wid)/num))+"px"});
			$(this).find("li").last().css({"margin":"0"});
			$(this).find("li").each(function()
			{
				var pad=($(this).parent().height()-$(this).height())/2;
				$(this).css({"padding-top":pad+"px","padding-bottom":pad+"px"});
			});
		});
		$("#main_title").each(function()
		{
			if($(this).find("img").size()>0)
			{
				$(this).css({"padding-left":($(this).find("img").width()+20)+"px","background-image":"url('"+$(this).find("img").attr("src")+"')","background-size":$(this).find("img").width()+"px"});
				$(this).find("img").remove();
			}
		});
			});
	// ]]>
