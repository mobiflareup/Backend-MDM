<%@ Page Title="" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="Attendancecontant.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.Attendancecontant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
        System.Data.DataTable dt,dt1,dt2,dt3 = new System.Data.DataTable();
        string originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
        string parentDirectory = originalPath.Substring(originalPath.LastIndexOf("/")+1);
        string[] urlarr = parentDirectory.Split('?');
        parentDirectory = urlarr[1].Substring(4);
        string mid = wb.getValuefromTable("id", "website_menu", "page_url",parentDirectory);//need to change parentDirectory
        dt = wb.fetchDisplay("content where menu_id ='" + mid + "'");//$carr
        dt1 = wb.fetchDisplay("website_menu where id='" + mid + "'");//$wm%>
    <meta charset="UTF-8">
<meta name="Generator" content="EditPlus�">
<meta name="Author" content="">
<meta name="Keywords" content="<%: dt1.Rows[0]["seo_keyword"] %>">
<meta name="Description" content="<%: dt1.Rows[0]["seo_description"] %>">
    <%if (dt.Rows[0]["bottom_right_bottom"].ToString() != "")
        { %>

<link rel="stylesheet" type="text/css"
	href="videolb/overlay-minimal.css" />
<script src="videolb/jquery.js" type="text/javascript"></script>
<script src="videolb/swfobject.js" type="text/javascript"></script>
<script src="videolb/jquery.tools.min.js" type="text/javascript"></script>
<script src="videolb/videolightbox.js" type="text/javascript"></script>
<%} %>
<title><%: dt1.Rows[0]["seo_title"] %></title>
<style>
.custom-att-solu-banner
{border: 1px solid #ccc;
    padding: 5px;
    border-radius: 4px;
	}


.carousel-inner.onebyone-carosel { margin: auto; width: 90%; }
.onebyone-carosel .active.left { left: -33.33%; }
.onebyone-carosel .active.right { left: 33.33%; }
.onebyone-carosel .next { left: 33.33%; }
.onebyone-carosel .prev { left: -33.33%; }
.carousel-inner
{
	background:none;
}
.carousel-control
{
	display:block;
}

.feature-2 {
    padding: 12px;
    -webkit-transition: all 0.3s;
    transition: all 0.3s;
}.media:first-child {
    margin-top: 0;
}
.media, .media-body {
    overflow: hidden;
    zoom: 1;
}
.media-body, .media-left, .media-right {
    display: table-cell;
    vertical-align: top;
}
.media-left, .media>.pull-left {
    padding-right: 10px;
}
.feature-2 .feature-icon {
    font-size: 45px;
    width: 45px;
    height: 45px;
    line-height: 45px;
	background:#ccc;
	border-radius:50%;
    color: #00bcd4;
    -webkit-transition: all 0.3s;
    -o-transition: all 0.3s;
    transition: all 0.3s;
}
.feature-2 .media-body {
    padding-left: 10px;
}
.modal-header {
    padding: 10px !important;
    border-bottom: none !important;
}
.custom-pane-content
{
	font-size:16px;
}
.product_img img
{
	max-height:365px !important;
	margin:auto;
}
.custom-remove-glyphi
{
	background: #337ab7;
    padding: 10px;
    position: absolute;
    right: 0px;
    top: 0px;
    color: #fff;
	top:0px !important;
	border-radius:0px 0px 0px 14px;
}
.custom-product-modal-head
{
    padding: 0px !important;
    border-bottom: none !important;
}
.custom-product-modal-body
{
	padding:0px 15px 15px !important;
}

/*homestyle.css-ln:4100*/
.spec-details
{
	border:1px solid #ccc;
	padding:10px;
	font-weight:bold;
    border-top: 5px solid #337ab7;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
        System.Data.DataTable dt,dt1,dt2,dt3 = new System.Data.DataTable();
        string originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
        string parentDirectory = originalPath.Substring(originalPath.LastIndexOf("/")+1);
        string[] urlarr = parentDirectory.Split('?');
        parentDirectory = urlarr[1].Substring(4);
        string mid = wb.getValuefromTable("id", "website_menu", "page_url",parentDirectory);//need to change parentDirectory
        dt = wb.fetchDisplay("content where menu_id ='" + mid + "'");//$carr
        dt1 = wb.fetchDisplay("website_menu where id='" + mid + "'");//$wm
        dt2 = wb.fetchDisplay("product");//$ap
        dt3 = wb.fetchDisplay("content1");//$af%>
    <!--  Body Of Content -->
		<div class="container-fluid">
		<div class="contents"> 
			<h1>MobiOcean Attendance System</h1>
			<div class="divider"></div>
			<a href="<%= MobiOcean.MDM.BAL.Model.Constant.Home%>">Home</a>&nbsp;/&nbsp;<a href="attendance-management-system">Attendance Management System</a>&nbsp;/&nbsp;
			<a href="advance-attendance-solution ">MobiOcean Attendance System</a> 
		</div>
		<br />
		<div class="custom-att-solu-banner">
			<div class="text-center">
				<img src="/Mobiocean/Content/content-images/<%: dt.Rows[0]["top_image"] %>" class="img-responsive" alt="Attendance System" width="100%">
			</div>
		</div>
		<div class="custom-att-solu">
			<div class="text-center">
				<img src="/Mobiocean/Content/content-images/<%: dt.Rows[0]["bottom_image"] %>" class="img-responsive" alt="Attendance System" width="100%">
			</div>
		</div>
		<div class="panel with-nav-tabs panel-primary">
			<div class="panel-heading">
				<h2>MobiOcean Attendance System</h2><br />
			
			</div>
			<div class="panel-body">
				<div class="responsiveTabs">
					<ul id="myTab" class="tabs nav nav-tabs">
						<li class="active"><a href="#home1" data-toggle="tab"><i class="fa fa-laptop"></i> Features</a></li>
						<li><a href="#profile" data-toggle="tab"><i class="fa fa-shopping-cart"></i> Offered Devices</a></li>
						<li><a href="#dropdown3" data-toggle="tab"><i class="fa fa-user"></i>Our Solutions</a></li>
					</ul>
					<div id="myTabContent" class="tab-content" >
						<div class="tab-pane fade in active" id="home1">
                            <%if (dt3.Rows.Count > 0)
                                {
                                    int n = 2;
                                    for (int i = 0; i < 6; i++)
                                    { %>
							<div class="col-lg-4">
								<div class="feature-2">
									<div class="media">
										<div class="media-left hidden-xs">
											<div class="feature-icon text-center">
											</div>
										</div>
										<div class="media-body">
												<h4 class="media-heading"><%: dt3.Rows[i]["title"] %></h4>
											<p><%= Encoding.UTF8.GetString(Encoding.Default.GetBytes(dt3.Rows[i]["description"].ToString()))  %></p>
										</div>
									</div>
								</div>
							</div><%if (i == n)
                                          { %>
                            <div class="clearfix"></div>
                            <%
                                            n += 3; 
                                        }
                                    }
                                }%>
							<%--<div class="col-lg-4">
							
								<div class="feature-2">
									<div class="media">
										<div class="media-left hidden-xs">
											<div class="feature-icon text-center">
											</div>
										</div>
										<div class="media-body">
												<h4 class="media-heading"><?php echo $af[1]['title']?></h4>
											<p><?php echo $af[1]['description']?></p>
										</div>
									</div>
								</div>
							</div>
							
							<div class="col-lg-4">
								<div class="feature-2">
									<div class="media">
										<div class="media-left hidden-xs">
											<div class="feature-icon text-center">
											</div>
										</div>
										<div class="media-body">
												<h4 class="media-heading"><?php echo $af[2]['title']?></h4>
											<p><?php echo $af[2]['description']?></p>
										</div>
									</div>
								</div>
							</div>
							<div class="clearfix"></div>
							<div class="col-lg-4">
								<div class="feature-2">
									<div class="media">
										<div class="media-left hidden-xs">
											<div class="feature-icon text-center">
											</div>
										</div>
										<div class="media-body">
												<h4 class="media-heading"><?php echo $af[3]['title']?></h4>
											<p><?php echo $af[3]['description']?></p>
										</div>
									</div>
								</div>
							</div>
							<div class="col-lg-4">
								<div class="feature-2">
									<div class="media">
										<div class="media-left hidden-xs">
											<div class="feature-icon text-center">
											</div>
										</div>
										<div class="media-body">
												<h4 class="media-heading"><?php echo $af[4]['title']?></h4>
											<p><?php echo $af[4]['description']?></p>
										</div>
									</div>
								</div>
							</div>
							<div class="col-lg-4">
								<div class="feature-2">
									<div class="media">
										<div class="media-left hidden-xs">
											<div class="feature-icon text-center">
											</div>
										</div>
										<div class="media-body">
												<h4 class="media-heading"><?php echo $af[5]['title']?></h4>
											<p><?php echo $af[5]['description']?></p>
										</div>
									</div>
								</div>
							</div>--%>
							<div class="clearfix"></div>
						</div>
						<div class="tab-pane fade" id="profile">
							<!-- Item slider-->
							<div class="row">
								<div class="col-lg-9">
									<!--<h3>Sample Content title</h3>-->
								</div>
								<div class="col-lg-3">
									<div class="controls pull-right">
										<a class="left fa fa-chevron-left btn btn-success custom-slide-icon" href="#itemslider" data-slide="prev"></a>
										<a class="right fa fa-chevron-right btn btn-success custom-slide-icon" href="#itemslider" data-slide="next"></a>
									</div>
								</div>
								<div class="clearfix"></div>
								<br />
							  <div class="row">
								<div class="col-xs-12 col-sm-12 col-md-12">
								  <div class="carousel carousel-showmanymoveone slide"  id="itemslider"  data-interval="false" data-ride="carousel">
									<div class="carousel-inner">
									<%if (dt2.Rows.Count > 0)
                                        {
                                            int m = 3;
                                            for (int i = 0; i < dt2.Rows.Count; i++)
                                            { %>
									
									 
									 <%if (i == 0)
    { %>
									  <div class="item active">
										<div class="col-xs-12 col-sm-6 col-md-3 ">
											<div class="custom-device">
										  <img src="/Mobiocean/Content/Banner/<%= dt2.Rows[i]["image"] %>" class="img-responsive center-block" width="180" height="200">
										  <h4 class="text-center"><%= dt2.Rows[i]["name"] %></h4>
										  <div class="text-center">
											<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#product_view" id="carousel-selector-<%= i%>"><i class="fa fa-eye"></i> Specification</button>
										  </div>
										  </div>
										</div>
									  </div>
									<%}
    else
    { %>
									
									  <div class="item">
										<div class="col-xs-12 col-sm-6 col-md-3 ">
											<div class="custom-device">
										  <img src="/Mobiocean/Content/Banner/<%= dt2.Rows[i]["image"] %>" class="img-responsive center-block" width="180" height="200">
										  <h4 class="text-center"><%= dt2.Rows[i]["name"] %></h4>
										  <div class="text-center">
											<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#product_view" id="carousel-selector-<%= i%>"><i class="fa fa-eye"></i> Specification</button>
										  </div>
										  </div>
										</div>
									  </div>
									  
									  
									 <%}
                                                       }
    } %>
									  
									  
									</div>

							  </div>
							</div>
						  </div>

							</div>
							<!-- Item slider end-->
						</div>
						<div class="tab-pane fade" id="dropdown3">
							<p class="custom-pane-content"><%= dt.Rows[0]["description2"] %></p>
						</div>
					</div>
				</div>
				<!-- <div class="tab-content">
					<div class="tab-pane fade in active" id="tab1primary">
					
					</div>
					<div class="tab-pane fade" id="tab2primary">
				
					
					</div>
					<div class="tab-pane fade" id="tab3primary">
						
					</div>
				</div> -->
			</div>
		</div>
	</div>
       <div class="modal fade product_view" id="product_view">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header custom-product-modal-head">
					<a href="#" data-dismiss="modal" class="class pull-right"><span class="custom-remove-glyphi glyphicon glyphicon-remove"></span></a>
				</div>
				<div class="modal-body custom-product-modal-body">
				<div id="carousel-example-generic" class="carousel slide" data-interval="false" data-ride="carousel">
				  <!-- Wrapper for slides -->
				  <div class="carousel-inner">
                      <%if (dt2.Rows.Count > 0)
                          {
                              for (int i = 0; i < dt2.Rows.Count; i++)
                              {%>
				   <%if (i == 0)
    {%>
					<div class="item active"  data-slide-number="<%= i%>">
					 	<div class="col-md-4 product_img">
							<h4><b><%= dt2.Rows[i]["name"]%> :</b></h4><br />
							<img src="/Mobiocean/Content/banner/<%= dt2.Rows[i]["image"]%>"  data-toggle="magnify" class="img-responsive">
						</div>
						<div class="col-md-8 product_content">
							<h4><b>Specification :</b></h4><br />
							<div class="spec-details">
								<%= dt2.Rows[i]["specification"]%>
							</div>
						</div>
					</div><%}
    else
    { %>
					
					<div class="item"  data-slide-number="<%= i%>">
					 	<div class="col-md-4 product_img">
							<h4><b>Device Name :</b></h4><br />
							<img src="/Mobiocean/Content/banner/<%= dt2.Rows[i]["image"]%>"  data-toggle="magnify" class="img-responsive">
						</div>
						<div class="col-md-8 product_content">
							<h4><b>Specification :</b></h4><br />
							<div class="spec-details">
								<%= dt2.Rows[i]["specification"]%>
							</div>
						</div>
					</div>
					
					 <%}
                             }
    } %>
				  </div>
					<br />
						<div class="controls pull-right">
							<a class="left  btn btn-success custom-slide-icon" href="#carousel-example-generic" data-slide="prev"><span class="fa fa-chevron-left"></span> Prev</a>
							<a class="right  btn btn-success custom-slide-icon active" href="#carousel-example-generic" data-slide="next">Next <span class="fa fa-chevron-right"></span></a>
						</div>
				</div>
					<div class="row">
					
					</div>
				</div>
			</div>
		</div>
	</div> 
		<!-- End Here -->


	<script>

(function(){
  $('.carousel-showmanymoveone .item').each(function(){
    var itemToClone = $(this);

    for (var i=1;i<4;i++) {
      itemToClone = itemToClone.next();

      // wrap around if at end of item collection
      if (!itemToClone.length) {
        itemToClone = $(this).siblings(':first');
      }

      // grab item, clone, add marker class, add to collection
      itemToClone.children(':first-child').clone()
        .addClass("cloneditem-"+(i))
        .appendTo($(this));
    }
  });
}());
	</script>
	
	<script>
	jQuery(document).ready(function($) {
 
        //Handles the carousel thumbnails
        $('[id^=carousel-selector-]').click(function () {
        var id_selector = $(this).attr("id");
        try {
            var id = /-(\d+)$/.exec(id_selector)[1];
            console.log(id_selector, id);
            jQuery('#carousel-example-generic').carousel(parseInt(id));
        } catch (e) {
            console.log('Regex failed!', e);
        }
    });
        // When the carousel slides, auto update the text
        $('#carousel-example-generic').on('slid.bs.carousel', function (e) {
                 var id = $('.item.active').data('slide-number');
                $('#carousel-text').html($('#slide-content-'+id).html());
        });
});
	</script>

	<!-- Responsive TABCOLLAPSE Script-->
	
<script type="text/javascript">
			!function ($) {

			"use strict";

			// TABCOLLAPSE CLASS DEFINITION
			// ======================

			var TabCollapse = function (el, options) {
				this.options   = options;
				this.$tabs  = $(el);

				this._accordionVisible = false; //content is attached to tabs at first
				this._initAccordion();
				this._checkStateOnResize();


				// checkState() has gone to setTimeout for making it possible to attach listeners to
				// shown-accordion.bs.tabcollapse event on page load.
				// See https://github.com/flatlogic/bootstrap-tabcollapse/issues/23
				var that = this;
				setTimeout(function() {
				  that.checkState();
				}, 0);
			};

			TabCollapse.DEFAULTS = {
				accordionClass: 'visible-xs',
				tabsClass: 'hidden-xs',
				accordionTemplate: function(heading, groupId, parentId, active) {
					return  '<div class="panel panel-default">' +
							'   <div class="panel-heading">' +
							'      <h4 class="panel-title">' +
							'      </h4>' +
							'   </div>' +
							'   <div id="' + groupId + '" class="panel-collapse collapse ' + (active ? 'in' : '') + '">' +
							'       <div class="panel-body js-tabcollapse-panel-body">' +
							'       </div>' +
							'   </div>' +
							'</div>'

				}
			};

			TabCollapse.prototype.checkState = function(){
				if (this.$tabs.is(':visible') && this._accordionVisible){
					this.showTabs();
					this._accordionVisible = false;
				} else if (this.$accordion.is(':visible') && !this._accordionVisible){
					this.showAccordion();
					this._accordionVisible = true;
				}
			};

			TabCollapse.prototype.showTabs = function(){
				var view = this;
				this.$tabs.trigger($.Event('show-tabs.bs.tabcollapse'));

				var $panelHeadings = this.$accordion.find('.js-tabcollapse-panel-heading').detach();

				$panelHeadings.each(function() {
					var $panelHeading = $(this),
					$parentLi = $panelHeading.data('bs.tabcollapse.parentLi');

					var $oldHeading = view._panelHeadingToTabHeading($panelHeading);

					$parentLi.removeClass('active');
					if ($parentLi.parent().hasClass('dropdown-menu') && !$parentLi.siblings('li').hasClass('active')) {
						$parentLi.parent().parent().removeClass('active');
					}

					if (!$oldHeading.hasClass('collapsed')) {
						$parentLi.addClass('active');
						if ($parentLi.parent().hasClass('dropdown-menu')) {
							$parentLi.parent().parent().addClass('active');
						}
					} else {
						$oldHeading.removeClass('collapsed');
					}

					$parentLi.append($panelHeading);
				});

				if (!$('li').hasClass('active')) {
					$('li').first().addClass('active')
				}

				var $panelBodies = this.$accordion.find('.js-tabcollapse-panel-body');
				$panelBodies.each(function(){
					var $panelBody = $(this),
						$tabPane = $panelBody.data('bs.tabcollapse.tabpane');
					$tabPane.append($panelBody.contents().detach());
				});
				this.$accordion.html('');

				if(this.options.updateLinks) {
					var $tabContents = this.getTabContentElement();
					$tabContents.find('[data-toggle-was="tab"], [data-toggle-was="pill"]').each(function() {
						var $el = $(this);
						var href = $el.attr('href').replace(/-collapse$/g, '');
						$el.attr({
							'data-toggle': $el.attr('data-toggle-was'),
							'data-toggle-was': '',
							'data-parent': '',
							href: href
						});
					});
				}

				this.$tabs.trigger($.Event('shown-tabs.bs.tabcollapse'));
			};

			TabCollapse.prototype.getTabContentElement = function(){
				var $tabContents = $(this.options.tabContentSelector);
				if($tabContents.length === 0) {
					$tabContents = this.$tabs.siblings('.tab-content');
				}
				return $tabContents;
			};

			TabCollapse.prototype.showAccordion = function(){
				this.$tabs.trigger($.Event('show-accordion.bs.tabcollapse'));

				var $headings = this.$tabs.find('li:not(.dropdown) [data-toggle="tab"], li:not(.dropdown) [data-toggle="pill"]'),
					view = this;
				$headings.each(function(){
					var $heading = $(this),
						$parentLi = $heading.parent();
					$heading.data('bs.tabcollapse.parentLi', $parentLi);
					view.$accordion.append(view._createAccordionGroup(view.$accordion.attr('id'), $heading.detach()));
				});

				if(this.options.updateLinks) {
					var parentId = this.$accordion.attr('id');
					var $selector = this.$accordion.find('.js-tabcollapse-panel-body');
					$selector.find('[data-toggle="tab"], [data-toggle="pill"]').each(function() {
						var $el = $(this);
						var href = $el.attr('href') + '-collapse';
						$el.attr({
							'data-toggle-was': $el.attr('data-toggle'),
							'data-toggle': 'collapse',
							'data-parent': '#' + parentId,
							href: href
						});
					});
				}

				this.$tabs.trigger($.Event('shown-accordion.bs.tabcollapse'));
			};

			TabCollapse.prototype._panelHeadingToTabHeading = function($heading) {
				var href = $heading.attr('href').replace(/-collapse$/g, '');
				$heading.attr({
					'data-toggle': 'tab',
					'href': href,
					'data-parent': ''
				});
				return $heading;
			};

			TabCollapse.prototype._tabHeadingToPanelHeading = function($heading, groupId, parentId, active) {
				$heading.addClass('js-tabcollapse-panel-heading ' + (active ? '' : 'collapsed'));
				$heading.attr({
					'data-toggle': 'collapse',
					'data-parent': '#' + parentId,
					'href': '#' + groupId
				});
				return $heading;
			};

			TabCollapse.prototype._checkStateOnResize = function(){
				var view = this;
				$(window).resize(function(){
					clearTimeout(view._resizeTimeout);
					view._resizeTimeout = setTimeout(function(){
						view.checkState();
					}, 100);
				});
			};


			TabCollapse.prototype._initAccordion = function(){
				var randomString = function() {
					var result = "",
						possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
					for( var i=0; i < 5; i++ ) {
						result += possible.charAt(Math.floor(Math.random() * possible.length));
					}
					return result;
				};

				var srcId = this.$tabs.attr('id'),
					accordionId = (srcId ? srcId : randomString()) + '-accordion';

				this.$accordion = $('<div class="panel-group ' + this.options.accordionClass + '" id="' + accordionId +'"></div>');
				this.$tabs.after(this.$accordion);
				this.$tabs.addClass(this.options.tabsClass);
				this.getTabContentElement().addClass(this.options.tabsClass);
			};

			TabCollapse.prototype._createAccordionGroup = function(parentId, $heading){
				var tabSelector = $heading.attr('data-target'),
					active = $heading.data('bs.tabcollapse.parentLi').is('.active');

				if (!tabSelector) {
					tabSelector = $heading.attr('href');
					tabSelector = tabSelector && tabSelector.replace(/.*(?=#[^\s]*$)/, ''); //strip for ie7
				}

				var $tabPane = $(tabSelector),
					groupId = $tabPane.attr('id') + '-collapse',
					$panel = $(this.options.accordionTemplate($heading, groupId, parentId, active));
				$panel.find('.panel-heading > .panel-title').append(this._tabHeadingToPanelHeading($heading, groupId, parentId, active));
				$panel.find('.panel-body').append($tabPane.contents().detach())
					.data('bs.tabcollapse.tabpane', $tabPane);

				return $panel;
			};



			// TABCOLLAPSE PLUGIN DEFINITION
			// =======================

			$.fn.tabCollapse = function (option) {
				return this.each(function () {
					var $this   = $(this);
					var data    = $this.data('bs.tabcollapse');
					var options = $.extend({}, TabCollapse.DEFAULTS, $this.data(), typeof option === 'object' && option);

					if (!data) $this.data('bs.tabcollapse', new TabCollapse(this, options));
				});
			};

			$.fn.tabCollapse.Constructor = TabCollapse;


		}(window.jQuery);


		$('#myTab').tabCollapse();
</script>
	<!--End-->

  <!-- MAGNIFY PUBLIC CLASS DEFINITION -->
    
<script type="text/javascript">

!function ($) {

    "use strict"; // jshint ;_;


    /* MAGNIFY PUBLIC CLASS DEFINITION
     * =============================== */

    var Magnify = function (element, options) {
        this.init('magnify', element, options)
    }

    Magnify.prototype = {

        constructor: Magnify

        , init: function (type, element, options) {
            var event = 'mousemove'
                , eventOut = 'mouseleave';

            this.type = type
            this.$element = $(element)
            this.options = this.getOptions(options)
            this.nativeWidth = 0
            this.nativeHeight = 0

            this.$element.wrap('<div class="magnify" \>');
            this.$element.parent('.magnify').append('<div class="magnify-large" \>');
            this.$element.siblings(".magnify-large").css("background","url('" + this.$element.attr("src") + "') no-repeat");

            this.$element.parent('.magnify').on(event + '.' + this.type, $.proxy(this.check, this));
            this.$element.parent('.magnify').on(eventOut + '.' + this.type, $.proxy(this.check, this));
        }

        , getOptions: function (options) {
            options = $.extend({}, $.fn[this.type].defaults, options, this.$element.data())

            if (options.delay && typeof options.delay == 'number') {
                options.delay = {
                    show: options.delay
                    , hide: options.delay
                }
            }

            return options
        }

        , check: function (e) {
            var container = $(e.currentTarget);
            var self = container.children('img');
            var mag = container.children(".magnify-large");

            // Get the native dimensions of the image
            if(!this.nativeWidth && !this.nativeHeight) {
                var image = new Image();
                image.src = self.attr("src");

                this.nativeWidth = image.width;
                this.nativeHeight = image.height;

            } else {

                var magnifyOffset = container.offset();
                var mx = e.pageX - magnifyOffset.left;
                var my = e.pageY - magnifyOffset.top;

                if (mx < container.width() && my < container.height() && mx > 0 && my > 0) {
                    mag.fadeIn(100);
                } else {
                    mag.fadeOut(100);
                }

                if(mag.is(":visible"))
                {
                    var rx = Math.round(mx/container.width()*this.nativeWidth - mag.width()/2)*-1;
                    var ry = Math.round(my/container.height()*this.nativeHeight - mag.height()/2)*-1;
                    var bgp = rx + "px " + ry + "px";

                    var px = mx - mag.width()/2;
                    var py = my - mag.height()/2;

                    mag.css({left: px, top: py, backgroundPosition: bgp});
                }
            }

        }
    }


    /* MAGNIFY PLUGIN DEFINITION
     * ========================= */

    $.fn.magnify = function ( option ) {
        return this.each(function () {
            var $this = $(this)
                , data = $this.data('magnify')
                , options = typeof option == 'object' && option
            if (!data) $this.data('tooltip', (data = new Magnify(this, options)))
            if (typeof option == 'string') data[option]()
        })
    }

    $.fn.magnify.Constructor = Magnify

    $.fn.magnify.defaults = {
        delay: 0
    }


    /* MAGNIFY DATA-API
     * ================ */

    $(window).on('load', function () {
        $('[data-toggle="magnify"]').each(function () {
            var $mag = $(this);
            $mag.magnify()
        })
    })

} ( window.jQuery );
</script>
</asp:Content>
