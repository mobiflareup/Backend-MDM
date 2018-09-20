<%@ Page Title="Login" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="MobiLogin.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.MobiLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <div class="content">
            <div class="dashborder_img" style="background: #eee">
                <div class="wrap">
                    <div class="container">
                        <div class="col-md-6 col-sm-8 col-xs-10 col-md-offset-3 col-sm-offset-2 col-xs-offset-1">
                            <h3>Login</h3>
                        </div>
                        <div class="module form-module col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2 col-xs-10 col-xs-offset-1">

                            <div class="form">
                                <form id="ajaxform" method="post" style="padding: 10px">

                                    <div class="form-group">
                                        <label>Username </label>
                                        <input class="form-control" name="EmailId" id="EmailId" type="email" placeholder="Enter your email" />
                                    </div>
                                    <div class="form-group">
                                        <label>Password </label>
                                        <input class="form-control" id="Password" name="Password" type="password" placeholder="Enter your password" />
                                    </div>
                                    <button class="loginbtnclick" id="loginbtnclick">Login</button>
                                    <div class="cta">
                                        <%--<a class="js-open-modal" href="#" data-modal-id="popup1">Forgot Password</a>--%>
                                        <a class="js-open-modal" data-toggle="modal" data-target="#myModalForget" style="cursor: pointer;">Forgot Password</a>
                                    </div>
                                    <div class="cta">Don't have an account with us? <a href="Registration">Register here</a><!--|<a style="padding:5px" class="padding_5" href="forgot.html"></a>--></div>
                                </form>
                            </div>

                            <div class="clear"></div>

                            <%-- start --%>

                            <div id="myModalForget" class="modal fade" role="dialog">
                                <div class="modal-dialog">

                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <center>
        	<a  class="close" style="color:black;" data-dismiss="modal">&times;</a>
                                </center>
                                            <div class="modal-body">
                                                <div class="form-group">
                                                    <label>Email Id </label>
                                                    <input class="form-control" name="EmailId" id="fEmailId" type="email" placeholder="enter your email">
                                                </div>
                                                <button class="forgotbtnclick">Submit</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- End --%>

                            <%--<div id="popup1" class="modal-box">
  <header> <a href="#" class="js-modal-close close">×</a>
  </header>
  <div class="modal-body">
    <div class="form-group">
                                              <label> Email Id </label>                                          
                                              <input class="form-control" name="EmailId" id="fEmailId" type="email" placeholder="enter your email">
                                          </div>
                                          <button class="forgotbtnclick">Submit</button>
  </div>
    </div>
                            --%>
                        </div>

                    </div>


                </div>
            </div>
        </div>
    </div>



    <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
    <%--    <script type="text/javascript">
        $(document).ready(function () {


            $("#down").on('click', function (e) {
                //alert();
                window.location.href = "http://admin.mobiocean.com/API.aspx";

                $('#myModaldownload').modal('toggle');
            });

        });

    </script>--%>
    <%--<script type="text/javascript">

$(function(){

var appendthis =  ("<div class='modal-overlay js-modal-close'></div>");

	$('a[data-modal-id]').click(function(e) {
		e.preventDefault();
    $("body").append(appendthis);
    $(".modal-overlay").fadeTo(500, 0.7);
    //$(".js-modalbox").fadeIn(500);
		var modalBox = $(this).attr('data-modal-id');
		$('#'+modalBox).fadeIn($(this).data());
	});  
  
  
$(".js-modal-close, .modal-overlay").click(function() {
    $(".modal-box, .modal-overlay").fadeOut(500, function() {
        $(".modal-overlay").remove();
    });
 
});
 
$(window).resize(function() {
    $(".modal-box").css({
        top: ($(window).height() - $(".modal-box").outerHeight()) / 10,
        left: ($(window).width() - $(".modal-box").outerWidth()) / 10
    });
});
 
$(window).resize();
 
});
</script>--%>
    <script type="text/javascript">


        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-36251023-1']);
        _gaq.push(['_setDomainName', 'jqueryscript.net']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
</asp:Content>

