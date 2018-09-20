<%@ Page Title="" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="onpremises.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.onpremises" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
        System.Data.DataTable dt = new System.Data.DataTable();
        string originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
        string parentDirectory = originalPath.Substring(originalPath.LastIndexOf("/")+1);
        string[] urlarr = parentDirectory.Split('?');
        parentDirectory = urlarr[1].Substring(4);
        string mid = wb.getValuefromTable("id", "website_menu", "page_url",parentDirectory);//need to change parentDirectory%>
    <title><%= wb.fetchDisplay("website_menu where id= '" + mid + "'").Rows[0]["seo_title"]%></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">    
    <div class="contents"><%MobiOcean.MDM.BAL.Query.websearch wb = new MobiOcean.MDM.BAL.Query.websearch();
                              System.Data.DataTable dt,dt1,dt2,dt3 = new System.Data.DataTable();
                              string originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                              string parentDirectory = originalPath.Substring(originalPath.LastIndexOf("/")+1);
                              string[] urlarr = parentDirectory.Split('?');
        parentDirectory = urlarr[1].Substring(4);
                              string mid = wb.getValuefromTable("id", "website_menu", "page_url",parentDirectory);//need to change parentDirectory
                              dt = wb.fetchDisplay("content where menu_id ='" + mid + "'");%>        
        <h1><%: dt.Rows[0]["name"] %></h1>
        
        <div class="divider"></div> 
       <div>
           <%dt1 = wb.fetchDisplay("website_menu where id ='"+mid+"'");//child
               dt2 = wb.fetchDisplay("website_menu where id='"+dt1.Rows[0]["parent_id"].ToString()+"'");//parent
               dt3= wb.fetchDisplay("website_menu where id='"+dt2.Rows[0]["parent_id"].ToString()+"'");//grand parent
                %>
       <a href="<%= MobiOcean.MDM.BAL.Model.Constant.Home%>">Home</a>&nbsp;/&nbsp;<a href="<%= dt1.Rows[0]["page_url"].ToString()+".aspx"%>"><%= dt1.Rows[0]["name"]%></a>

       </div>
        <div class="col-md-6">   
            <%= dt.Rows[0]["description"] %>
            
        </div>
        <div class="cl-md-1">&nbsp;</div>
        <div class="col-md-6"  style="padding-right:0px;">
        <div class="dashborder_img" >
        <h3 class="" style=" float: left; width: 100%; margin-top:0px">On-Premises Form</h3>
           <form>  
             <div class="col-lg-12 " style="border:3px solid #ccc;padding-bottom:25px;margin-bottom:30px;">
		
		<div class="module form-module" style="border:none;background:none;box-shadow:none;margin-bottom:0px;padding:25px 0px 0px 0px;">
			<div class="form-group">
                                        <label>Name*</label>
                                        <input class="form-control" name="OPname" id="OPname" type="text"  required/>
                                    </div>
			<div class="form-group">
                                        <label>E-mailId*</label>
                                        <input class="form-control" name="mail" id ="mail"  type="text" required/>
                                    </div>
			<div class="form-group">
                                        <label>Mobile No*</label>
                                        <input class="form-control" name="phone"  id="phone" type="text"  required/>
                                    </div>
                                    <div class="form-group">
                                        <label>Company Name</label>
                                        <input class="form-control" name="company"  id="company" type="text" />
                                    </div>
                                    <div class="form-group">
                                        <label> Industry</label>
                                        <input class="form-control" name="industry" id="industry"  type="text"  />
                                    </div>
                                    <div class="form-group">
                                        <label>Number of employees</label>
                                        <input class="form-control" name="noofemp"  id="noofemp" type="text"  />
                                    </div></div>
                                    <script language="JavaScript">
										function toggle(source) {
										  checkboxes = document.getElementsByName('foo');
										  for(var i=0, n=checkboxes.length;i<n;i++) {
											checkboxes[i].checked = source.checked;
										  } 
										}
									</script>
			
			<div class="checkbox  custom-checkbox">
				<label class=" "><strong><input type="checkbox" id="ckbCheckAl" value="">Select the category of features:</strong></label>
			</div>
			<div class="" style="box-shadow:0px 0px 0px 2px #ccc;">
				
				<div class = "category" id ="category"></div>
				<div class="clearfix"></div>
				
			</div><br>
			<div class="module form-module" style="border:none;background:none;box-shadow:none;margin-bottom:0px;padding:25px 0px 0px 0px;">
			<div class="form-group">
                                        <label> Duration (Months)</label>
                                        <input class="form-control" name="period" id="period"  type="text"  />
                                    </div>
                                    
                                    <div class="col-md-3  " style="margin-left:-17px">
                                     <button type="button" id="submitpermises" value="Submit" >Submit</button>
								    	
                                    </div>
                                    </div></div>
		</form>
	</div>
	 		
					
					
        
							  
						</div>
           			</div>
       
    </div>

 

	<script>
	$(document).ready(function(){

		 $.ajax({
             url: "http://admin.mobiocean.com/api/CategoryList/GetSolutions",
             type: "GET",
             success: function (response) {

		var i = $("");
		
		var si = $("<div class='col-lg-12' style='border-bottom:1px solid #aaa;padding-bottom:10px;'>");
	    $.each(response, function(index, item){
	    	if(item.categoryList.length!=0){
		    i = $("<div class='col-lg-12' style='border-bottom:1px solid #aaa;padding-bottom:10px;'><div class='col-lg-12' style='padding-left:0px;padding-right:0px;'><div class='checkbox  custom-checkbox '><label class=' '><strong><input type='checkbox' value="+item.solutionId+" id=Project"+item.solutionId+" onchange=Solution("+item.solutionId+")>"+item.solutionName+"</strong></label></div></div><br>");
		    
	    	}else{
	    		i = $("<div class='col-lg-12' style='border-bottom:1px solid #aaa;padding-bottom:10px;'><div class='col-lg-12' style='padding-left:0px;padding-right:0px;'><div class='checkbox  custom-checkbox '><label class=' '><strong><input type='checkbox'  value='"+item.solutionName+"' class = 'features' id=Project"+item.solutionId+" >"+item.solutionName+"</strong></label></div></div><br>");
		    	}
			var li = $("<div class='col-lg-12' style='padding-left:25px ;' id='SubDivProject"+item.solutionId+"'>");
		    $.each(item.categoryList, function(index1, category){
		    	
		    	 list = $("<div class='checkbox  custom-checkbox'><label class=' col-sm-4'><input type='checkbox' name='foo' class='features' id=SubProject"+category.CategoryId+" value='"+category.CategoryName+"'>"+category.CategoryName+"</label></div>");

		    	 li.append(list);
				
		    });
		    
		    i.append(li);
			   $('#category').append(i);
	    });

		 }
         
         });

		 $("#ckbCheckAl").click(function(){
			 var all = $(this);
			  $('input:checkbox').each(function() {
			       $(this).prop("checked", all.prop("checked"));
			  });
		 });

		 
	    
		});

	function Solution(id){

		 if ($('#Project' + id).is(':checked')) {
	            $('#SubDivProject' + id + ' :input[type=checkbox]').each(function () {
	                this.checked = true;
	            });
	        }
	        else {
	            $("#SubDivProject" + id + " :input[type=checkbox]").each(function () {
	                this.checked = false;
	            });
				
		}
	}

		//$("#submitpermisess").click(function(){

			//  var arr = [];
			
	        //$('input:checkbox[class=features]:checked').each(function(){
	         //   arr.push($(this).val());
	       //  });
			
	       //  alert(arr);
			
			//});
	</script>
</asp:Content>
