$(document).ready(function()
{
	$(".label-1").hide();
	$(".label-2").hide();
	$(".label-3").hide();
	$(".custom-setting-panel").hide();

	$(".custom-label-1").click(function()
	{
		$(".label-1").fadeToggle(800);
		$(".label-2").hide();
		$(".label-3").hide();
		$(".custom-setting-panel").hide();
	});
	$(".custom-label-2").click(function()
	{
		$(".label-2").fadeToggle(800);
		$(".label-1").hide();
		$(".label-3").hide();
		$(".custom-setting-panel").hide();
	});
	$(".custom-label-3").click(function()
	{
		$(".label-3").fadeToggle(800);
		$(".label-2").hide();
		$(".label-1").hide();
		$(".custom-setting-panel").hide();
	});
	$(".custom-setting1").click(function()
	{
		$(".custom-setting-panel").slideToggle(800);
		$(".label-2").hide();
		$(".label-1").hide();
		$(".label-3").hide();
	});
	$(".custom-bages1 a").click(function()
	{
		$(this).addClass("active").siblings().removeClass("active");
	});
	$(".custom-submenu a").on("click", function(e){
		$(this).next('ul').toggle();
		e.stopPropagation();
		e.preventDefalut();
	});
	$(".custom-enrollment .custom-submenu").click(function()
	{
		$(this).addClass("active").siblings().removeClass("active");
	});
});