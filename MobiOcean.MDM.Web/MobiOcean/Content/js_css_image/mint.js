var sliders = new Array();
var sliders_timeouts = new Array();
var allowAdvance = new Array();
$(document).ready(function()
{
	if($(".jp-jplayer").size()>0)
	{
		$(".jp-jplayer").each(function(){
			var ext = $(this).attr("class").split(" ")[1].split("_")[1];
			var ID = $(this).attr("class").split(" ")[2].split("_")[1];
			eval("$('#jquery_jplayer_"+ID+"').jPlayer({ready: function () {$(this).jPlayer('setMedia', {"+((ext=="ogg") ? "oga" : ext)+": '"+WEB_ROOT+"/mint-project/uploads/"+ID+"."+ext+"'});},cssSelectorAncestor: '#jp_container_"+ID+"',swfPath: '"+WEB_ROOT+"/mint-global/swf', solution: 'flash,html',supplied: '"+((ext=="ogg") ? "oga" : ext)+"'});");
		});
	}
	
	$("ul li a").click(function(e) {
	   e.stopPropagation();
	});
})
function initPlayer(ID, ext)
{
	eval("$('#jquery_jplayer_"+ID+"').jPlayer({ready: function () {$(this).jPlayer('setMedia', {"+((ext=="ogg") ? "oga" : ext)+": '"+WEB_ROOT+"/mint-project/uploads/"+ID+"."+ext+"'});},cssSelectorAncestor: '#jp_container_"+ID+"',swfPath: '"+WEB_ROOT+"/mint-global/swf', solution: 'flash,html',supplied: '"+((ext=="ogg") ? "oga" : ext)+"'});");
}
function initSlide()
{
	$(".slide").each(function()
	{
		$(this).parent().addClass("sld_box");
		if(!$(this).hasClass("slide_show"))
			$(this).css("display","none");
	});
	$(".sld_box").each(function()
	{
		if(!sliders[this.id])
		{
			$(this).find(".slide:first").css("display","block");
			$(this).find(".slide_img:first").css("display","inline");
			var tid = this.id;
			var tim = parseInt($("#"+this.id+" > .slide_timing").attr("value"));
			if(tim>0)
				sliders_timeouts[this.id]=setTimeout(function(){loopSlide(tid);},tim);
			sliders[this.id]=2;
			allowAdvance[this.id]=true;
		}
	});
}
function loopSlide(eleid,noLoop)
{
	if(allowAdvance[eleid])
	{
		allowAdvance[eleid] = false;
		var lp=true;
		var nxt=false;
		var tim=parseInt($("#"+eleid+" > .slide_timing").attr("value"));
		if(tim==0)
		{
			tim=1000;
			noLoop=true;
		}
		setTimeout(function(){allowAdvance[eleid]=true;},tim/2);
		var typ=$("#"+eleid+" > .slide_type").attr("value");
		var slide_no=sliders[eleid];
		if(!sliders[eleid])
			slide_no=2;
		if(slide_no>$("#"+eleid+" > .slide").size())
			slide_no=1;
		var slide_before = slide_no-1
		if(slide_before<=0)
			slide_before=$("#"+eleid+" > .slide").size();
		sliders[eleid]=slide_no+1;
		switch(typ)
		{
			case "fade":
				$("#"+eleid).find(".slide_"+slide_before+", .slide_img_"+slide_before).fadeOut( (tim/4) ,function(){this.style.display="none"} );
				
				$("#"+eleid).find(".slide_"+slide_no+", .slide_img_"+slide_no).fadeIn( (tim/4) );
				break;
			case "pulse":
				$("#"+eleid).find(".slide_"+slide_before+", .slide_img_"+slide_before).css( "display" , "none" );
				
				$("#"+eleid).find(".slide_"+slide_no+", .slide_img_"+slide_no).fadeIn( (tim/4) );
				break;
			case "slidethrough":
				$("#"+eleid).find(".slide_"+slide_before+", .slide_img_"+slide_before).animate({"left":"-"+$("#"+eleid).width()+"px"},(tim/2));
				
				$("#"+eleid).find(".slide_"+slide_no).css({"left" : $("#"+eleid).width()+"px", "display" : "block"} );
				$("#"+eleid).find(".slide_img_"+slide_no).css({"left" : $("#"+eleid).width()+"px", "display" : "inline"} );
				
				$("#"+eleid).find(".slide_"+slide_no+", .slide_img_"+slide_no).animate({"left":"0"},(tim/2));
				break;
			case "slidein":
				$("#"+eleid).find(".slide, .slide_img").css({"z-index":"298","display":"none"});
				$("#"+eleid).find(".slide_"+slide_before+", .slide_img_"+slide_before).css({"z-index":"299","display":"block"});
				
				$("#"+eleid).find(".slide_"+slide_no).css({"left" : $("#"+eleid).width()+"px", "display" : "block", "z-index" : "300"} );
				$("#"+eleid).find(".slide_img_"+slide_no).css({"left" : $("#"+eleid).width()+"px", "display" : "inline", "z-index" : "300"} );
				
				$("#"+eleid).find(".slide_"+slide_no+", .slide_img_"+slide_no).animate({"left":"0"},(tim/2));
				break;
			case "slidefade":
				$("#"+eleid).find(".slide_"+slide_before+", .slide_img_"+slide_before).fadeOut((tim/4));
				
				$("#"+eleid).find(".slide_"+slide_no).css({"left" : $("#"+eleid).width()+"px", "display" : "block"} );
				$("#"+eleid).find(".slide_img_"+slide_no).css({"left" : $("#"+eleid).width()+"px", "display" : "inline"} );
				
				$("#"+eleid).find(".slide_"+slide_no+", .slide_img_"+slide_no).animate({"left":"0"},(tim/2));
				break;
		}
		$(".slide_control_"+eleid+" > .slide_tracker_on").removeClass("slide_tracker_on");
		$(".slide_control_"+eleid+" > #slide_tracker_"+slide_no).addClass("slide_tracker_on");
	}
	if(!noLoop)
		sliders_timeouts[eleid]=setTimeout(function(){loopSlide(eleid);},tim);
}
function loopBack(eleid,noLoop)
{
	if(allowAdvance[eleid])
	{
		allowAdvance[eleid] = false;
		var lp=true;
		var nxt=false;
		var tim=parseInt($("#"+eleid+" > .slide_timing").attr("value"));
		if(tim==0)
		{
			tim=1000;
			noLoop=true;
		}
		setTimeout(function(){allowAdvance[eleid]=true;},tim/2);
		var typ=$("#"+eleid+" > .slide_type").attr("value");
		var slide_no=sliders[eleid];
		var current_slide = slide_no-1
		if(current_slide<=0)
			current_slide=$("#"+eleid+" > .slide").size();
		target_slide=current_slide-1;
		if(target_slide<=0)
			target_slide=$("#"+eleid+" > .slide").size();
		sliders[eleid]=target_slide+1;
		switch(typ)
		{
			case "fade":
				$("#"+eleid).find(".slide_"+current_slide+", .slide_img_"+current_slide).fadeOut( (tim/4) ,function(){this.style.display="none"} );
				
				$("#"+eleid).find(".slide_"+target_slide+", .slide_img_"+target_slide).fadeIn( (tim/4) );
				break;
			case "pulse":
				$("#"+eleid).find(".slide_"+current_slide+", .slide_img_"+current_slide).css( "display" , "none" );
				
				$("#"+eleid).find(".slide_"+target_slide+", .slide_img_"+target_slide).fadeIn( (tim/4) );
				break;
			case "slidethrough":
				$("#"+eleid).find(".slide_"+current_slide+", .slide_img_"+current_slide).animate({"left":$("#"+eleid).width()+"px"},(tim/2));
				
				$("#"+eleid).find(".slide_"+target_slide).css({"left" : "-"+$("#"+eleid).width()+"px", "display" : "block"} );
				$("#"+eleid).find(".slide_img_"+target_slide).css({"left" : "-"+$("#"+eleid).width()+"px", "display" : "inline"} );
				
				$("#"+eleid).find(".slide_"+target_slide+", .slide_img_"+target_slide).animate({"left":"0"},(tim/2));
				break;
			case "slidein":
				$("#"+eleid).find(".slide, .slide_img").css({"z-index":"298","display":"none"});
				$("#"+eleid).find(".slide_"+current_slide+", .slide_img_"+current_slide).css({"z-index":"299","display":"block"});
				
				$("#"+eleid).find(".slide_"+target_slide).css({"left" : "-"+$("#"+eleid).width()+"px", "display" : "block", "z-index" : "300"} );
				$("#"+eleid).find(".slide_img_"+target_slide).css({"left" : "-"+$("#"+eleid).width()+"px", "display" : "inline", "z-index" : "300"} );
				
				$("#"+eleid).find(".slide_"+target_slide+", .slide_img_"+target_slide).animate({"left":"0"},(tim/2));
				break;
			case "slidefade":
				$("#"+eleid).find(".slide_"+current_slide+", .slide_img_"+current_slide).fadeOut((tim/4));
				
				$("#"+eleid).find(".slide_"+target_slide).css({"left" : "-"+$("#"+eleid).width()+"px", "display" : "block"} );
				$("#"+eleid).find(".slide_img_"+target_slide).css({"left" : "-"+$("#"+eleid).width()+"px", "display" : "inline"} );
				
				$("#"+eleid).find(".slide_"+target_slide+", .slide_img_"+target_slide).animate({"left":"0"},(tim/2));
				break;
		}
		$(".slide_control_"+eleid+" > .slide_tracker_on").removeClass("slide_tracker_on");
		$(".slide_control_"+eleid+" > #slide_tracker_"+slide_no).addClass("slide_tracker_on");
	}
	if(!noLoop)
		sliders_timeouts[eleid]=setTimeout(function(){loopSlide(eleid);},tim);
}
function toSlide(eleid,no)
{
	$("#"+eleid+" > .slide, #"+eleid+" > .slide_img").fadeOut( $("#"+eleid+" > .slide_timing").attr("value") ).removeClass("disp");
	$("#"+eleid+" > .slide_"+no).fadeIn( $("#"+eleid+" > .slide_timing").attr("value") ).addClass("disp");
	$("#"+eleid+" > .slide_img_"+no).fadeIn( $("#"+eleid+" > .slide_timing").attr("value") );
	$(".slide_tracker_on").removeClass("slide_tracker_on");
	$("#slide_tracker_"+no).addClass("slide_tracker_on");
	clearTimeout(time);
	time=setTimeout(function(){loopSlide(eleid)}, (parseInt($("#"+eleid+" > .slide_timing").attr("value"))+2000));
	sliders[eleid]=no+1;
	slide_no=no;
}
function gotoNextSlide(eleid,noloop)
{
	clearTimeout(sliders_timeouts[eleid]);
	loopSlide(eleid,noloop);
}
function gotoPrevSlide(eleid,noloop)
{
	clearTimeout(sliders_timeouts[eleid]);
	loopBack(eleid,noloop);
}