$(document).ready(function()
	{
		if($("#foot_left").find("img"))
		{
			$("#foot_left").css({"background-image":"url('"+$("#foot_left").find("img").attr("src")+"')"});
			$("#foot_left").find("img").remove();
		}
		$("#foot_right").append("<span class='white_button' onclick='registerPop();'>Start now</span>");
	});
	// ]]>