// Copyright Mintedbox 2011
function capitalise(str)
{var letter=str.substr(0,1);return letter.toUpperCase()+str.substr(1);}
function refreshNow()
{document.cookie="mint_sh="+window.pageYOffset+"; path=/";window.location.href=window.location.href;}
function el(id)
{try{return document.getElementById(id);}catch(e){console.log("element error - "+id);}}
function vl(id)
{if(el(id))return el(id).tagName.toLowerCase()=="div" || el(id).tagName.toLowerCase()=="span" ? el(id).innerHTML : el(id).value;else return false;}
function svl(id,vv)
{if(el(id).nodeName.toLowerCase()=="select"){$("#"+id).children("option").each(function(ind,vl){if($(this).attr("value").toLowerCase() == vv.toLowerCase() || vv.toLowerCase() == $(this).attr("value").toLowerCase()){el(id).selectedIndex=ind;return ind;}});return false;}else return el(id).value=vv;}
function addEl(type,id,target,clname)
{if(el(target)&&!el(id))
{var newdiv=document.createElement(type);newdiv.setAttribute('id',id);el(target).appendChild(newdiv);if(clname){el(id).className=clname;}
return el(id);}
else if(el(id))
return el(id);}
function rmEl(element_id,parent_id)
{if(el(element_id)&&el(parent_id))
{el(parent_id).removeChild(el(element_id));}}
function getWidth()
{var widChk=document.createElement('div');widChk.setAttribute('style','height:100%;width:100%;position:fixed;');widChk.setAttribute('id','wid');document.body.appendChild(widChk);var tmpw=el("wid").offsetWidth;tmpw=tmpw-5;document.body.removeChild(document.getElementById("wid"));return tmpw;}
function getHeight()
{var widChk=document.createElement('div');widChk.setAttribute('style','height:100%;width:100%;position:fixed;');widChk.setAttribute('id','wid');document.body.appendChild(widChk);var tmpw=el("wid").offsetHeight;tmpw=tmpw-5;document.body.removeChild(document.getElementById("wid"));if(tmpw<10)
{tmpw=500;}
return tmpw;}
function ucfirst(string)
{return string.charAt(0).toUpperCase()+string.slice(1);}
function findPos(aa)
{try{var obj=el(aa);var curleft=curtop=0;if(obj.offsetParent)
{do
{curleft+=obj.offsetLeft;curtop+=obj.offsetTop;}
while(obj=obj.offsetParent);return[curleft,curtop];}}
catch(ee){}}
function frp(aa,bb)
{var a1,b1;a1=findPos(aa);b1=findPos(bb);return [b1[0]-a1[0],b1[1]-a1[1]];}
function pos2Mouse(event,divid,offX,offY)
{var xpos=event.clientX;var ypos=event.clientY+getScrollY();if(offX){xpos+=offX;}
if(offY){ypos+=offY;}
el(divid).style.top=ypos+"px";el(divid).style.left=xpos+"px";if((getWidth()-10)<xpos+el(divid).offsetWidth)
{xpos-=(el(divid).offsetWidth+10);ypos-=10;}}
function pos2(elthis,elthat,size,offX,offY)
{if(el(elthis).style.position!="absolute"){el(elthis).style.position="absolute";}
if(!offX){offX=0;}
if(!offY){offY=0;}
var pp=findPos(elthat);el(elthis).style.left=(pp[0]+parseInt(offX))+"px";el(elthis).style.top=(pp[1]+parseInt(offY))+"px";if(size)
{el(elthis).style.width=el(elthat).style.width;el(elthis).style.height=el(elthat).style.height;}}
function isDOM(o)
{return(typeof HTMLElement==="object"?o instanceof HTMLElement:typeof o==="object"&&o.nodeType===1&&typeof o.nodeName==="string");}
function onEnter(eve,func)
{if (window.event) keycode = window.event.keyCode;else if (eve) keycode = eve.which;if (keycode == 13 && func) eval(func+"();");}
function getScrollY()
{var scrOfY = 0;try{if( typeof( window.pageYOffset ) == 'number' )	scrOfY = window.pageYOffset;else if( document.body && ( document.body.scrollLeft || document.body.scrollTop ) )	scrOfY = document.body.scrollTop;else if( document.documentElement && ( document.documentElement.scrollLeft || document.documentElement.scrollTop ) )	scrOfY = document.documentElement.scrollTop;return scrOfY;}catch(ee){}}
