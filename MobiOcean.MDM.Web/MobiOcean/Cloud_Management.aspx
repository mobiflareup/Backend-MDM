<%@ Page Title="Cloud Managed Pricing-MobiOcean" Language="C#" MasterPageFile="~/MobiOcean/webmaster.Master" AutoEventWireup="true" CodeBehind="Cloud_Management.aspx.cs" Inherits="MobiOcean.MDM.Web.MobiOcean.Cloud_Management" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
#accordion .cloud-title>a.accordion-toggle::before,#accordion a[data-toggle="collapse"]::before
	{
	content: "\2212";
	float: left;
	font-family: 'Glyphicons Halflings';
	margin-right: 1em;
}

#accordion .cloud-title>a.accordion-toggle.collapsed::before,#accordion a.collapsed[data-toggle="collapse"]::before
	{
	content: "\2b";
}

.cloud-title>a {
	display: block;
}

.cloud-title>a:hover {
	text-decoration: none;
}

.panel-group {
	margin-bottom: 0px;
}

input:focus {
	color: #676967;
}
</style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="container-fluid">
		<div class="contents">
			<h1>Cloud Managed Pricing</h1>
			<div class="divider"></div><%string s = Request.Form["Payment"];%>
			<a href="<%= MobiOcean.MDM.BAL.Model.Constant.Home%>">Home</a>&nbsp;/&nbsp;<a
				href="Cloud-Managed">Cloud Managed</a>
		</div>
		<br />
		
		  <div class="container">
			<div class="row">
				<div class="custom-cloud-manage">
					<div class='panel-group' id='accordion' role='tablist'
						aria-multiselectable='true'></div>

					<div class="total-amt">
						<div style="width: 100%; float: left; background: #fff;">
							<span
								style="font-weight: bold; float: right; padding: 10px 0px; color: #666; text-align: center;">
								<b>Total Amount :</b> <input type="text"
								placeholder="Total Amount" id="Subtotal" size="20" height="48"
								style="height: 50px;" disabled /> </span>
						</div>
						<div class="clearfix"></div>
					</div>
					<br /> <br />
					<div class="col-lg-12 text-center">
						<button type="button" class="btn btn-info btn-lg custom-pay-btn"
							data-toggle="modal" data-target="#myPaytmModal" id="paytm"
							style="margin: 8px; padding: 10px 25px;">Pay by Paytm</button>
						<button type="button"
							class="btn btn-lg btn-primary custom-pay-btn"
							style="margin: 8px; padding: 10px 25px;" id="paynow">Pay Now</button>
					</div>
					<div class="clearfix"></div>
				</div>
			</div>
		</div>

		<div id="myPaytmModal" class="modal fade" role="dialog">
			<div class="modal-dialog">

				<!-- Modal content-->
				<div class="modal-content">
					<div class="modal-header">
						<h4 class="modal-title text-center">Scan QR Code or Enter the
							Mobile No</h4>
					</div>
					<div class="modal-body">
						<img src="/MobiOcean/Content/images/Paytm-Scan-Code.png" class="img-responsive"
							alt="Paytm-Acceptrd-Here" title="Paytm-Acceptrd-Here"
							style="margin: auto; border: 3px solid #223265;">
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
					</div>
				</div>

			</div>
		</div>
		</div>

<script>
    var SolutionArray = [];
    function SelectALL1(chk, id, tabId) {
        var chkdata = chk.split(',');
        var selecttotal = 0;
        var flag = $("#" + id).prop('checked') == true ? true : false;
        //alert(flag);
        if (flag) {

            if (chkdata[1] != null && chkdata[1] != "") {
                $("." + chkdata[1]).prop('disabled', false);
                $(".Solution" + tabId).closest('table').find('.checkBoxClassSolution').each(function () {
                    var flagdata = $(this).prop('checked') == true ? true : false;
                    if (!flagdata) {
                        selecttotal = parseInt(selecttotal) + parseInt($(this).parents("tr").find("td .custom_total").val() == "" ? 0 : $(this).parents("tr").find("td .custom_total").val());
                    }
                   
                })
                $("#Subtotal").val(parseInt($("#Subtotal").val() == "" ? 0 : $("#Subtotal").val()) + parseInt(selecttotal));
            }
            $("." + chkdata[0]).prop('checked', flag);
            
          
        } else {
           
            if (chkdata[1] != null && chkdata[1] != "") {
                $("." + chkdata[1]).prop('disabled', true);
                $(".Solution" + tabId).closest('table').find('.checkBoxClassSolution').each(function () {
                    var flagdata = $(this).prop('checked') == true ? true : false;
                    if (flagdata) {
                        selecttotal = parseInt(selecttotal) + parseInt($(this).parents("tr").find("td .custom_total").val() == "" ? 0 : $(this).parents("tr").find("td .custom_total").val());
                        $(this).parents("tr").find("td .custom_total").val("");
                        $(this).parents("tr").find("td ." + chkdata[1]).val("");
                    }
                    //}
                })
                $("#Subtotal").val(parseInt($("#Subtotal").val() == "" ? 0 : $("#Subtotal").val()) - parseInt(selecttotal));
            }
            $("." + chkdata[0]).prop('checked', flag);
        }
    }
function SelectALL(chk, id, tabId){
    
    var selecttotal = 0;
		var flag = $("#" + id).prop('checked') == true ? true : false;
		//alert(flag);
		if(flag){
		    
		    
		    $("." + chk).prop('checked', flag);
		//var str = 
			Solution(tabId);	
			OnchangeTotal("checkBoxClass"+tabId);		
		}else{
		    $("." + chk).prop('checked', flag);
		   
			Solution(tabId);
			OnchangeTotal("checkBoxClass"+tabId);	
		}
		}
$(window).bind("load", function()    {
	var res = GetData();
	});
function GetData(){		
         var htmltxt = "";
		$.ajax({
            url: 'http://admin.mobiocean.com/api/CategoryList/GetPaymentWebPageData1',
            type: "GET",
            async:false,
            success: function (obj) {
                
                $.each(obj, function (i, bnddata) {
                   
                    if (bnddata.Id == 1) {
                        //var htmltxtKeys1 = '<div class=""  style="margin-top:30px"><div class="col-lg-4" ><p style="background:#2b3887;padding:10px 15px;color:#fff;">License(S)</p><br /><div id="text" class="text-xs text-center"><input type="number" id="users'+bnddata.Id+'" min="0" onchange="Solution('+bnddata.Id+')"  placeholder="Enter No Of Users"></div><br /></div><div class="col-lg-4"><p style="background:#2b3887;padding:10px 15px;color:#fff;">Duration(S)</p><br /><div id="text" class="text-xs text-center"><input type="number" min="0" id="duration'+bnddata.Id+'" onchange=Solution('+bnddata.Id+'); placeholder="Enter Duration(In Months)"></div><br></div><div class="col-lg-4"><p style="background:#2b3887;padding:10px 15px;color:#fff;">Total Amount</p><br /><div id="text" class="text-xs text-center"><input type="text"  placeholder="Total Amount" id="total" class="TotalQuantity" disabled/></div><br></div><div class="clearfix"></div>';
						
                        htmltxt = htmltxt + "<div class='panel panel-default'><div class='panel-heading' role='tab' id='headingOne' style='background:#2b3887;color:#fff;'><h4 class='panel-title cloud-title'>" +
                           "<a role='button' data-toggle='collapse' data-parent='#accordion' href='#cloud-collapseOne' aria-expanded='true' aria-controls='collapseOne'><strong>" + bnddata.Name + "</strong></a>" +
                           "</h4></div><div id='cloud-collapseOne' class='panel-collapse collapse in' role='tabpanel' aria-labelledby='headingOne'><div class='panel-body' style='padding:0px;'>" +
                           "<div class='table-responsive'><table id='tableID"+bnddata.Id+"' class='table table-bordered custom-sub-table'><thead><tr class=''  style='background:#676967;color:#fff;font-weight:bold;'>" +
                           "<th class='col-lg-1 text-center'><input name='foo' id='ReccheckAll' type='checkbox' class='checkbox1 checkboxall Solution" + bnddata.Id + "' onchange=SelectALL1('RecCheck,custom_status',this.id," + bnddata.Id + ");></th><th class='col-lg-4 text-center'>Category</th><th class='col-lg-3 text-center'>" +
                           "Price / Day / User</th><th class='col-lg-2 text-center'>Android</th><th class='col-lg-2 text-center'>iOS</th><th class='col-lg-2 text-center'>License(S)</th><th class='col-lg-2 text-center'>Duration(S)</th><th class='col-lg-2 text-center'>Total Amount</th></tr></thead><tbody>";
                        
                        //var innercount = 0;
                        $.each(bnddata.data, function (is, catdata) {
                        	
                            htmltxt = htmltxt + "<tr class='' style='background:#f16a27;color:#fff;'>" +
                                                "<td class='' colspan='8'><strong>" + catdata.CategoryName + "</strong></td></tr>";
                            //var count = 0;

                            $.each(catdata.Features, function (ia, Feadata) {
                                var temp = Feadata.Price != null ? Feadata.Price : '';
                                var And = Feadata.IsAndroid == true ? '/MobiOcean/Content/images/approve1.png' : '/MobiOcean/Content/images/disapprove1.png';
                                var IOS = Feadata.IsIOS == true ? '/MobiOcean/Content/images/approve1.png' : '/MobiOcean/Content/images/disapprove1.png';
                                
                                if(Feadata.IsBuyNow == 0){                                   
									
                                    htmltxt = htmltxt + "<tr class='text-center' id='Sol" + Feadata.FeatureId + "'><td><input type='checkbox'  name='FeatureRec_" + is + "_" + ia + "' id='FeatureRec_" + is + "_" + ia + "' value=" + Feadata.FeatureId + " type='checkbox' class='checkBoxClassSolution checkBoxClass RecCheck' onchange=solution1('" + is + "_" + ia + "');></td>" +
                                    "<td>" + Feadata.FeatureName + "</td>"+
                 					"<td>"+Feadata.Price+"</td>" +
                                    "<td><img name='FeatureRecAnd_" + is + "_" + ia + "' id='FeatureRecAnd_" + is + "_" + ia + "' class='websitemenu_pub_unpub_img' src=" + And + "></td>" +
                                	"<td><img name='FeatureRecIOS_" + is + "_" + ia+ "' id='FeatureRecIOS_" + is + "_" + ia + "' class='websitemenu_pub_unpub_img' src=" + IOS + "></td>"+
                                    "<td><input type='number' id='users" + is + "_" + ia + "' min='0' class='custom_status custom_user' required onclick=store('" + is + "_" + ia + "','user'); onchange=solution2('" + is + "_" + ia + "','user'); disabled  placeholder='Enter No Of Users'></td>" +
                                    "<td><input type='number' min='0' id='duration" + is + "_" + ia + "' class='custom_status custom_duration' required onclick=store('" + is + "_" + ia + "','duration'); onchange=solution2('" + is + "_" + ia + "','duration'); disabled placeholder='Enter Duration(In Months)'></td>" +
                                    "<td><input type='text'  placeholder='Total Amount' id='total" + is + "_" + ia + "' class='TotalQuantity custom_total' disabled/></td></tr>";
                                    //count = count + 1;
                                }else{
                                    htmltxt = htmltxt + "<tr class='text-center'><td></td>" +
                                    "<td>" + Feadata.FeatureName + "</td>"+
                                    "<td><a href ='"+Feadata.BuyNowLink+"' class='btn-lg btn-primary' target='_blank'>BuyNow</a> </td>" +
                                    "<td><img name='FeatureRecAnd_" + is + "_" + ia + "' id='FeatureRecAnd_" + is + "_" + ia + "' class='websitemenu_pub_unpub_img' src=" + And + "></td>" +
                                    "<td><img name='FeatureRecIOS_" + is + "_" + ia + "' id='FeatureRecIOS_" + is + "_" + ia + "' class='websitemenu_pub_unpub_img' src=" + IOS + "></td>"+
                                    "<td>---</td>" +
                                   "<td>---</td>" +
                                   "<td>---</td></tr>";
                                    //count = count + 1;
                                }
                                
                                
                            });                          
                            //innercount = innercount + 1;
                            //htmltxt = htmltxt + "<input type=hidden name='RecCount_1_"+innercount+"' id='RecCount_1_"+innercount+"' value=" + count + "/>";<input type=hidden name='RecCount_1' id='RecCount_1' value='" + innercount+ "'/>
                        });
						
						
                        htmltxt = htmltxt + "</tbody></table></div></div></div></div>";

                        
                    }                   
                    else { 
                        htmltxt = htmltxt + "<div class='panel panel-default'><div class='panel-heading' role='tab' id='heading3' style='background:#2b3887;color:#fff;'><h4 class='panel-title cloud-title'><a class='collapsed' role='button' data-toggle='collapse' data-parent='#accordion' href='#cloud-collapse"+bnddata.Id+"' aria-expanded='false' aria-controls='collapse"+bnddata.Id+"'>" +
                                "<strong>" + bnddata.Name + "</strong></a></h4></div><div id='cloud-collapse"+bnddata.Id+"' class='panel-collapse collapse' role='tabpanel' aria-labelledby='heading"+bnddata.Id+"'><div class='panel-body'>" +
                                "<div class='table-responsive'><table class='table custom-sub-table table-bordered' id='tableID"+bnddata.Id+"'><thead><tr  style='background:#676967;color:#fff;font-weight:bold;'><th class='text-center'><input name='foo' id='ReccheckAll"+bnddata.Id+"' onchange=SelectALL('ReccheckAllClass"+bnddata.Id+"',this.id,"+bnddata.Id+"); type='checkbox' class='checkbox1 checkboxall Device'></th>" +
                                "<th class='text-center'>Device Model</th>";
                        if(bnddata.Id == 2){
                            htmltxt += "<th class='text-center'>Price/Device/Month</th>";
                        }
                        else{
                            htmltxt += "<th class='text-center'>Unit Price</th>";
                        }
							
                        htmltxt += "<th class='text-center'>Quantity</th>";
                        if(bnddata.Id == 2){
                            htmltxt += "<th class='text-center'>Duration(In Months)</th>";
                        }
                        htmltxt += "<th class='text-center'>Total Price</th></tr></thead><tbody>";
					
                        //var count = 0;
                        $.each(bnddata.data, function (is, otobj) {
							
												
                            if(bnddata.data.length != 1 && otobj.CategoryName != bnddata.Name){
                                htmltxt = htmltxt + "<tr class='' style='background:#f16a27;color:#fff;'>" +
                                                "<th class='' colspan='5'><strong>" + otobj.CategoryName + "</strong></th></tr>";
                            }
							
                            $.each(otobj.Features, function (ia, Feadata) {						 
			
                                htmltxt = htmltxt + "<tr class='text-center' id='Feature"+bnddata.Id+ Feadata.FeatureId +"'><td><input type='checkbox'  name='FeatureRec_" + is + "_" + ia + "' id='FeatureRec_" + is+ "_" + ia + "' value=" + Feadata.FeatureId + " type='checkbox' class='checkBoxClassSolution checkBoxClass checkBoxClass"+bnddata.Id+" ReccheckAllClass"+bnddata.Id+"' onchange=OnchangeTotal('checkBoxClass"+bnddata.Id+"');></td>" +
                                        "<td>" + Feadata.FeatureName + "</td>"+
                                        "<td>"+Feadata.Price+"</td>" +
                                        "<td><input type='number' min='1' class='Quantity' onchange=OnchangeTotal('checkBoxClass"+bnddata.Id+"');></td>";
                                if(bnddata.Id == 2){
                                    htmltxt += "<td><input type='number' min='1' class='duration' onchange=OnchangeTotal('checkBoxClass"+bnddata.Id+"');></td>";
                                }
                                htmltxt +=	"<td><input type='text' size='20' class='TotalQuantity' disabled></td> </tr>";
                                //count = count + 1;

                            });
                            //innercount = innercount + 1;
                            //htmltxt = htmltxt + "<input type=hidden name='RecCount_1_"+innercount+"' id='RecCount_1_"+innercount+"' value=" + count + "/>";<input type=hidden name='DevCount_3' id='DevCount_3' value=" + count + "/>

                        });
						 
                        htmltxt = htmltxt + "</tbody></table></div></div></div></div>";
						
                    }
                    
                });
                $("#accordion").append(htmltxt);
                <%if (!string.IsNullOrWhiteSpace(s)) { %>
                var data = <%= Newtonsoft.Json.JsonConvert.DeserializeObject(s)%>;//<?php echo json_encode($getdata) ?>;
                
                    CheckBoxbinds1(data);
               
          <%  }%>
               
            }
       });
	
		}		
var numberOfChildCheckBoxes = $('#checkAll').length;
$('#checkAll').change(function() {
	
  var checkedChildCheckBox = $('#checkAll:checked').length;
  if (checkedChildCheckBox == numberOfChildCheckBoxes)
    $(".checkBoxClass").prop('checked', true);
	
  else
    $(".checkBoxClass").prop('checked', false);
});
function CheckBoxbinds1(data){
	$.each(data, function(i, json){
		if(json.Id == 1){
		$.each(json.Features, function () {
			//$("#users"+json.Id).val(json.Features.License); 
			//$("#duration"+json.Id).val(json.Features.Duration);
		    var arrayFromIds = json.Features.FeatureIds.split(",");
		    var arrayQuan = json.Features.Quantity.split(",");
		    var arrayDuar = json.Features.Duration.split(",");
			 $.each(arrayFromIds, function (i, elem) {
			 var id = parseInt(elem);
			 $("#Sol" + id).closest('tr').find('.checkBoxClass').prop('checked', true);
			 $("#Sol" + id).closest('tr').find('.custom_user').val(arrayQuan[i]);
			 var userid=$("#Sol" + id).closest('tr').find('.custom_user').attr('id');
			 solution2(userid.slice(5),'user');
            $("#Sol" + id).closest('tr').find('.custom_duration').val(arrayDuar[i]);
			 solution2(userid.slice(5),'duration');
             $("#users"+userid.slice(5)).prop('disabled',false);
             $("#duration"+userid.slice(5)).prop('disabled',false);
			 });
  		});
		Solution(json.Id);
		}else{
			
			$.each(json.Features, function () {
				
				var arrayFromIds = json.Features.FeatureIds.split(",");
				var noofusers = json.Features.License.split(",");
				var duration = [];
				if(json.Id == 2){ duration = json.Features.Duration.split(","); }
				
			i=0;
			$.each(arrayFromIds, function (i, elem) {
			 var id = parseInt(elem);
  			 $("#Feature"+json.Id+id).closest('tr').find('.checkBoxClass').prop('checked', true);
			 $("#Feature"+json.Id+id).closest('tr').find('.Quantity').val(noofusers[i]);
			 if(json.Id == 2){
			 $("#Feature"+json.Id+id).closest('tr').find('.duration').val(duration[i]);
			 }
			 i++;
			 });
			
			});
			OnchangeTotal("checkBoxClass"+json.Id);
		}
	});
	
	}
$("#paynow").click(function(){
				
				var inputStr = '';
				var finalStr = '[';
				for(i=1;i<=$('.table').length;i++){
				
				var Solution = [];
				var Qty = [];
				var Dtn = [];
				var Qty1 = [];
				var Dtn1 = [];
				inputStr += '{"Id":'+i+',"Features":{';
			   $("#tableID"+i+" tbody tr").each(function(){
				   
				  if(i == 1){
					  if($(this).find("td:eq(0) input[type=checkbox]").prop('checked')){
						 Solution.push($(this).find("td:eq(0) input[type=checkbox]").val());
						  if($("#users"+i).val()!=""&& $("#duration"+i).val()!=""){
						      Qty1.push($(this).find("td .custom_user").val());
						      Dtn1.push($(this).find("td .custom_duration").val());
							  //Qty1 = $("#users"+i).val();
							  //Dtn1 = $("#duration"+i).val();
						  }else{
							   //if($("#tabId"+i).closest('table').find('.checkBoxClassSolution').prop('checked').length > 0){
							   if($("#users"+i).val()==""){
								   alert("Choose Users");
								   inputStr = '';
								   return false;
								
							   }
							   if($("#duration"+i).val()==""){
								   alert("Choose Duration");
								   inputStr = '';
								   return false;
								  
							   }
							  
						  }
					  }
					}
					else
					{
						
					if($(this).find("td:eq(0) input[type=checkbox]").prop('checked')){
						
					Solution.push($(this).find("td:eq(0) input[type=checkbox]").val());
					
					if($(this).find("td:eq(0) input[type=checkbox]").prop('checked') && $(this).find("td:eq(3) input[type=number]").val()!=""&&$(this).find("td:eq(4) input[type=number]").val()!=""){
					
					Qty.push($(this).find("td:eq(3) input[type=number]").val());
					if(i == 2){
					Dtn.push($(this).find("td:eq(4) input[type=number]").val());
					}
					}else{
						
						if($(this).find("td:eq(0) input[type=checkbox]").prop('checked'))
						{
						if($(this).find("td:eq(3) input[type=number]").val()=="")
						{
							alert("choose"+"\xa0\xa0"+$(this).find("td:eq(1)").text()+"\xa0\xa0"+"Quantity");
							inputStr = '';
							return false;
						
						}
						if(i == 2){
						 if($(this).find("td:eq(4) input[type=number]").val()=="")
						{
							alert("Choose"+"\xa0\xa0"+$(this).find("td:eq(1)").text()+"\xa0\xa0"+"Duration");
							inputStr = '';
							return false;
						}
						}
						Solution = [];
						Qty = [];
						Dtn = []; 
						}
				  }
				}
					} 
					
				}); 
				if(Solution.length>0){	
				if(i == 1){
					if(Solution.length>0&&Qty1!=""&&Dtn1!=""){
					inputStr += '"FeatureIds":"'+Solution+'", "Quantity": "'+Qty1+'", "Duration":"'+Dtn1+'"}}';
					}
					else{
						return false;
					}
				}else{
					
					if(Solution.length>0&&Qty.length>0 && Solution.length == Qty.length){
					if(i==2){
					if(Solution.length>0&&Dtn.length>0 && Solution.length == Dtn.length){
					inputStr += '"FeatureIds":"'+Solution+'", "Quantity": "'+Qty+'", "Duration":"'+Dtn+'"}}';
					}
					}else if(i>2){
					inputStr += '"FeatureIds":"'+Solution+'", "Quantity": "'+Qty+'", "Duration":"'+Dtn+'"}}';
					}
					
					}
					else{
						return false;
					}
				}
				if(finalStr === '['){
					finalStr += inputStr != '' ? inputStr : '';
					//alert(finalStr);
				}
				else{
					finalStr +=  inputStr != '' ? ',' +inputStr : ',';
					//alert(finalStr);
				}
				
				}
				
				inputStr = '';
				
			
			}
			
			finalStr += ']';
			if('[]' != finalStr){
				
			console.log(finalStr);
			var form = $(document.createElement('form'));
			$(form).attr("action", "http://admin.MobiOcean.com/Web/mobiPayment");
			$(form).attr("method", "POST");
				var input1 = $("<input>")
				.attr("type", "hidden")
				.attr("name", "Payment")
				.val(finalStr);
			$(form).append($(input1));
			form.appendTo(document.body)
			$(form).submit();
			
			}
			else{
				
				alert("Please Choose atleast one solution");	
			}

		});
function OnchangeTotal(className){
		
		Attendance1(className);
		FullTotal();
		}
function Solution(id){
		
		SolutionArray = [];
    	$(".Solution"+id).closest('table').find('.checkBoxClassSolution').each(function(){ // input[type=checkbox]
			
    	    if ($(this).prop('checked'))	
        	{ 
				SolutionArray.push($(this).val());
    	    }
    	});
		
    	if($("#users"+id).val() !="" && $("#duration"+id).val() !="" && SolutionArray.length > 0){
			var total = 0;
			SolutionArray = $.unique(SolutionArray);
			$.each(SolutionArray, function(key, val){
				total += parseFloat($("#Sol"+val).closest('tr').find("td:eq(2)").text());
			});
			$("#total"+id).val(total +  parseInt($("#users"+id).val()) * (30 * parseInt($("#duration"+id).val())));
			var tots = total *  parseInt($("#users"+id).val()) * (30 * parseInt($("#duration"+id).val()))
			FullTotal();
    		return true;
        	}
    	else{
    		if($("#users"+id).val() == false && $("#duration"+id).val() == false && SolutionArray.length == 0){return true;}else{
    		var msg = $("#users"+id).val() == false ? "Choose no of users": $("#duration"+id).val() == false ? "Choose duration" : SolutionArray.length == 0 ? "Choose any solution" : "----" ;
			//$("#total").val("");
    		//alert(msg);
			$("#total"+id).val("0");
			FullTotal();
    		return false;
        	} 	
    }
}
var globaluser = 0;
var globalduration = 0;

function store(id, name) {
    if (name == "user") {
        globaluser = $("#users" + id).val() == "" ? 0 : $("#users" + id).val();
    }
    if (name == "duration") {
        globalduration = $("#duration" + id).val() == "" ? 0 : $("#duration" + id).val();
    }
}
function solution2(id,name) {
    var cal = 0;
    var temp = 0;
    if (name == "user") {
        if (parseInt(globaluser) > parseInt($("#users" + id).val() == "" ? 0 : $("#users" + id).val())) {
            cal = parseInt(globaluser) - parseInt($("#users" + id).val() == "" ? 0 : $("#users" + id).val());
            temp = parseInt(cal) * (30 * parseInt($("#duration" + id).val() == "" ? 0 : $("#duration" + id).val()));
            $("#Subtotal").val(parseInt($("#Subtotal").val() == "" ? 0 : $("#Subtotal").val()) - parseInt(temp));
            temp = parseInt($("#users" + id).val() == "" ? 0 : $("#users" + id).val()) * (30 * parseInt($("#duration" + id).val() == "" ? 0 : $("#duration" + id).val()));
        }
        else
        {
            cal = parseInt($("#users" + id).val() == "" ? 0 : $("#users" + id).val()) - parseInt(globaluser);
            temp = parseInt(cal) * (30 * parseInt($("#duration" + id).val() == "" ? 0 : $("#duration" + id).val()));
            $("#Subtotal").val(parseInt($("#Subtotal").val() == "" ? 0 : $("#Subtotal").val()) + parseInt(temp));
            temp = parseInt($("#users" + id).val() == "" ? 0 : $("#users" + id).val()) * (30 * parseInt($("#duration" + id).val() == "" ? 0 : $("#duration" + id).val()));
        }
        $("#total" + id).val(temp);
        globaluser = 0;
    }
    if (name == "duration") {
        if (parseInt(globalduration) > parseInt($("#duration" + id).val() == "" ? 0 : $("#duration" + id).val())) {
            cal = parseInt(globalduration) - parseInt($("#duration" + id).val() == "" ? 0 : $("#duration" + id).val());
            temp = parseInt($("#users" + id).val() == "" ? 0 : $("#users" + id).val()) * (30 * parseInt(cal));
            $("#Subtotal").val(parseInt($("#Subtotal").val() == "" ? 0 : $("#Subtotal").val()) - parseInt(temp));
            temp = parseInt($("#users" + id).val() == "" ? 0 : $("#users" + id).val()) * (30 * parseInt($("#duration" + id).val() == "" ? 0 : $("#duration" + id).val()));
        }
        else {
            cal = parseInt($("#duration" + id).val() == "" ? 0 : $("#duration" + id).val()) - parseInt(globalduration);
            temp = parseInt($("#users" + id).val() == "" ? 0 : $("#users" + id).val()) * (30 * parseInt(cal));
            $("#Subtotal").val(parseInt($("#Subtotal").val() == "" ? 0 : $("#Subtotal").val()) + parseInt(temp));
            temp = parseInt($("#users" + id).val() == "" ? 0 : $("#users" + id).val()) * (30 * parseInt($("#duration" + id).val() == "" ? 0 : $("#duration" + id).val()));
        }
        $("#total" + id).val(temp);
        globalduration = 0;
    }
    
}
function solution1(id) {
    if ($("#FeatureRec_"+id).is(":checked")) {
        $("#users"+id).prop("disabled", false);
        $("#duration" + id).prop("disabled", false);
        $("#Subtotal").val(parseInt($("#Subtotal").val() == "" ? 0 : $("#Subtotal").val()) + parseInt($("#total" + id).val() == "" ? 0 : $("#total" + id).val()));
    }
    else
    {
        $("#Subtotal").val(parseInt($("#Subtotal").val() == "" ? 0 : $("#Subtotal").val()) - parseInt( $("#total" + id).val()==""?0: $("#total" + id).val()));
        //var temp = parseInt($("#users" + id).val()) * (30 * parseInt($("#duration" + id).val()));
        $("#users" + id).val("");
        $("#duration" + id).val("");
        $("#total" + id).val("");
        $("#users" + id).prop("disabled", true);
        $("#duration" + id).prop("disabled", true);
    }
}
function Attendance1(className){
    	$('.'+className).closest('table').find('.'+className).each(function(){// 'input[type=checkbox]'
	
    	    if ($(this).prop('checked')==true)
        	{ 
					var res = '';
					if($(this).parents("tr").find("td:eq(4) input[type=number]").is( ".duration" )){
					
					res = parseFloat($(this).parents("tr").find("td:eq(2)").html()) * parseInt($(this).parents("tr").find(".Quantity").val()) * parseInt($(this).parents("tr").find(".duration").val())
					res = isNaN(res) == true ? 0 : res;
					 $(this).parents("tr").find(".TotalQuantity").val(res); 
					
					}else{
						res = parseFloat($(this).parents("tr").find("td:eq(2)").html()) * parseInt($(this).parents("tr").find(".Quantity").val())
							res = isNaN(res) == true ? 0 : res;
							$(this).parents("tr").find(".TotalQuantity").val(res); 
							}
					 if(res == 0){
						
						 return false;
					 }
    	    }
			if($(this).prop('checked')==false){ $(this).parents("tr").find(".TotalQuantity").val("0"); FullTotal();}
    	    });
		return true;
    }
function FullTotal(){
		var total = 0;
		$('.TotalQuantity').each(function(){		
			if($(this).val()!=0 || $(this).val()!=""){ 	
			total += parseFloat($(this).val());
			
			}
		}); 
		$("#Subtotal").val("");
		$("#Subtotal").val(total);
	}
</script>



</asp:Content>
