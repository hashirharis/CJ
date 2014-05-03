

//function clearData(){
//window.clipboardData.setData('text','') 
//}
//function ccb(){
//if(clipboardData){
//clipboardData.clearData();
//}
//setInterval("ccb();", 100000);
//}




function myJSFunction(ids)
{
var currentId=ids;
var contents=document.getElementById(ids.id).value;
    if ((((contents / contents) != 1) && (contents != 0)) || contents == "" )
	{
		alert('Please insert a Numeric value');
	    document.getElementById(ids.id).value="";
	
		//document.getElementById(ids.id).value='0.0';
		setTimeout(function () {currentId.focus() }, 50);
        document.getElementById(ids.id).value='0';
        currentId.select();

	//	currentId.focus();
	}
//document.getElementById(ids.id).focus();
}

 function ChangeDate(sDATE,sControl)
 {
	var ctr2= document.getElementById("txt"); 
	var elem = document.getElementsByTagName("input");
	
	if(elem==null)
	{
	alert("elememnt not found");
	}
	var ctrl;
	for(var i=0;i<elem.length;++i)
	{
	    ctrl = elem[i];
	    var ctrlName=ctrl.getAttribute("ID");		        
	     //if(ctrlName.indexOf(sControl)!=-1)
	     //{	  
	       //document.forms[0][ctrlName].value=sDATE;
	     //}
	}
}
function dispCal(dtCtrl)
{
window.open("calendar.aspx?Ctrl=" + dtCtrl,"calendar","width=240,height=220,resizable=0,scrollbars=0,left=400,top=300");
}

function ImageToCell(sDATE,sControl)
{
//alert(sDATE);
document.getElementById(sControl).innerHTML="<div><img src='" + sDATE + "' Class='mainimageEdit' /></div>";//width=200 height=200 

}
function DispImageInCell(ctrl,index)
{
window.open("BrowseImageToCell.aspx?Ctrl=" + ctrl +"&ImageIndex=" + index ,"ImageToCell","width=400,height=250,resizable=1,scrollbars=0,left=400,top=300");
}

function ChangeFile(sDATE,sControl)
{
	var ctr2= document.getElementById("txt"); 
	var elem = document.getElementsByTagName("input");
	
	if(elem==null)
	{
	alert("elememnt not found");
	}
	var ctrl;
	for(var i=0;i<elem.length;++i)
	{
	 ctrl=elem[i];
	 var ctrlName=ctrl.getAttribute("ID");
	 if(ctrlName.indexOf(sControl)!=-1)
	 {
	   document.forms[0][ctrlName].value=sDATE;
	 }
	}
}
function dispQuestionFile(dtCtrl)
{
window.open("FilUploadAnimation.aspx?Ctrl=" + dtCtrl,"UploadQuestionFiles","width=300,height=220,resizable=0,scrollbars=0,left=400,top=300");
}

//function ShowPopupMessage(i)
//{
//window.open("PopupMessage.aspx?id=" + i,"FJAMessage","width=240,height=220,resizable=0,scrollbars=0,left=400,top=300");
//} 

function noCopyMouse(e) {
        var isRight = (e.button) ? (e.button == 2) : (e.which == 3);
        
        if(isRight) {
            alert('You are prompted to type this twice for a reason!');
            return false;
        }
        return true;
    }

    function noCopyKey(e) {
        var forbiddenKeys = new Array('c','x','v');
        var keyCode = (e.keyCode) ? e.keyCode : e.which;
        var isCtrl;

        if(window.event)
            isCtrl = e.ctrlKey
        else
            isCtrl = (window.Event) ? ((e.modifiers & Event.CTRL_MASK) == Event.CTRL_MASK) : false;
    
        if(isCtrl) {
            for(i = 0; i < forbiddenKeys.length; i++) {
                if(forbiddenKeys[i] == String.fromCharCode(keyCode).toLowerCase()) {
                    alert('You are prompted to type this twice for a reason!');
                    return false;
                }
            }
        }
        return true;
    }
    
    function validateEmail(fld) {

    var error="";
    var tfld = trim(fld.value); 
                      // value of field with whitespace trimmed off
    var emailFilter = /^[^@]+@[^@.]+\.[^@]*\w\w$/ ;
    var illegalChars= /[\(\)\<\>\,\;\:\\\"\[\]]/ ;

    if (fld.value == "") {
      
        error = "You didn't enter an email address.\n";
    } else if (!emailFilter.test(tfld)) {              //test email for illegal characters
      
        error = "Please enter a valid email address.\n";
    } else if (fld.value.match(illegalChars)) {
     
        error = "The email address contains illegal characters.\n";
    } else {
       
    }
    if(error!="")
    {
    alert(error);
    setTimeout(function () {fld.focus() }, 50);
    fld.select();
}
    
}

//floating popup code statrt
var y1 = 20;   // change the # on the left to adjuct the Y co-ordinate
(document.getElementById) ? dom = true : dom = false;

function hidemessagebox() {
  if (dom) {document.getElementById("divmessage").style.visibility='hidden';}
}

function hideIt() {
  if (dom) {document.getElementById("layer2").style.visibility='hidden';}
}
//divRestrictedmsg
function hideRestrictmsg() {
  if (dom) {document.getElementById("divRestrictedmsg").style.visibility='hidden';}
}
//divActionmsg
function hideActionmsg() {
  if (dom) {document.getElementById("divActionmsg").style.visibility='hidden';}
}
//divaddnewverbmsg
function hideAddnewverbmsg() {
  if (dom) {document.getElementById("divaddnewverbmsg").style.visibility='hidden';}
}
//divsubmitmsg
function hidesubmitmsg() {
  if (dom) {document.getElementById("divsubmit").style.visibility='hidden';}
}
//div
function hideProcessmsg(){
  if (dom) {document.getElementById("divProcessmsg").style.visibility='hidden';}
}
//div
function hideNonverbmsg(){
  if (dom) {document.getElementById("divNonverbmsg").style.visibility='hidden';}
}
function hideInputmessage()
{if (dom) {document.getElementById("tcellmsgEntries1").style.visibility='hidden';}}

function hideall()
{
hidemessagebox();
hideIt();
hideAddnewverbmsg();
hidesubmitmsg();
hideProcessmsg();
hideNonverbmsg();

hideRestrictmsg();
hideActionmsg();

hideInputmessage();
}

function showIt(msgbody) {
document.getElementById("layer2").innerHTML="<font face='verdana, arial, helvetica, sans-serif' size='2'><div style='float:left; background-color:yellow; padding:3px; border:1px solid black'>" +
    "<span style='float:right; background-color:gray; color:white; font-weight:bold; width='20px'; text-align:center; cursor:pointer' onclick='javascript:hideIt()'>&nbsp;X&nbsp;</span>" + msgbody + " </div></font>";
  if (dom) {document.getElementById("layer2").style.visibility='visible';}
}

function placeIt() {
  if (dom && !document.all) {document.getElementById("layer2").style.top = window.pageYOffset + (window.innerHeight - (window.innerHeight-y1)) + "px";}
  if (document.all) {document.all["layer2"].style.top = document.documentElement.scrollTop + (document.documentElement.clientHeight - (document.documentElement.clientHeight-y1)) + "px";}
  window.setTimeout("placeIt()", 10); }
  
  //floating popup code end
  
  function ShowPopupMessage(i)
{
//window.open("PopupMessage.aspx?id=" + i,"FJAMessage","width=1,height=1,resizable=0,scrollbars=0,left=0,top=0");
}

function GetVerbFile(sDATE,sControl)
{
	var ctr2= document.getElementById("txt");

 
	var elem = document.getElementsByTagName("input");
	
	if(elem==null)
	{
	alert("elememnt not found");
	}
	var ctrl;
	for(var i=0;i<elem.length;++i)
	{
	 ctrl=elem[i];
	 var ctrlName=ctrl.getAttribute("ID");	 
	 if(ctrlName.indexOf(sControl)!=-1)
	 {	  
	   document.forms[0][ctrlName].value=sDATE;
	 }	
	}
}
function dispExcelFile(dtCtrl)
{
window.open("ExportExcelFile.aspx?Ctrl=" + dtCtrl,"UploadActionVerbList","width=300,height=220,resizable=0,scrollbars=0,left=400,top=300");
} 

function dispBulkUserExcelFile(dtCtrl)
{
window.open("ExportExcelFile.aspx?Ctrl=" + dtCtrl,"BulkUserCreation","titlebar=no,toolbars=no,menubar=no,status=no,width=300,height=220,resizable=0,scrollbars=0,left=400,top=300");
} 

function dispHelpFile()
{
window.open("Help_CTask.aspx","HelpForControlledTaskEntry","resizable=0,scrollbars=1,left=300,top=100");
}

var s;
function ShowMessageInTaskEntry(i)
{
if(i==1){
document.getElementById("tceltaskmessage1").innerHTML="<div ><table width=500; height=90; class='taskmessage'><tr><td></td></tr></table></div>";
//s=setTimeout("ShowMessageInTaskEntry(1)",5000)<p style='padding-left: 20px'>Enter the verb that describes the task you have in mind.<u>in the Action Field</u></p>
}
if(i==2)
{document.getElementById("tceltaskmessage1").innerHTML="<div><table width=500; height=90; class='taskmessage1'><tr><td style='padding-left: 15px'></td></tr></table></div>";
//s=setTimeout("ShowMessageInTaskEntry(2)",5000)Enter the names of things, people or data that get affected by the task performance as well as the context within which it happens.
}
if(i==3)
{document.getElementById("tceltaskmessage1").innerHTML="<div><table width=500; height=90; class='taskmessage2'><tr><td style='padding-left: 15px'> </td></tr></table></div>";
//s=setTimeout("ShowMessageInTaskEntry(3)",5000)Enter the objective/purpose of the task performance in the format 'in order to ….'
}

if(i==0){
document.getElementById("tceltaskmessage1").innerHTML="<div><table width=450; height=70; class='tasksavemessage'><tr><td >You have completed the task entry .. you can add more tasks after submitting these entries</td></tr></table></div>";
}
}

function ShowMessageSubTask(i)
{
if(i==1){
document.getElementById("tcellmsgEntries1").innerHTML="<div class='subtaskmessage';><table ><tr><td></td></tr></table></div>";
//document.getElementById("tcellmsgEntries1").innerHTML="<div class='subtaskmessage'; style='background-image: url(images/msgaction.jpg);background-repeat: no-repeat;'><table style='height:70px;'><tr><td><b>Enter here a verb representing one or more specific activities under the above task.</b></td></tr>" +
//                                            "<tr><td>NOTE: You may use the Action Verb Library to identify suitable action verbs in the area of your work.</td></tr></table></div>";<b>Enter here a verb representing one or more specific activities under the above task.</b></td></tr>" +
                                            //"<tr><td>NOTE: You may use the Action Verb Library to identify suitable action verbs in the area of your work.

////clik();
//setFocus("txtObj");
}
if(i==2){

// Objectmessage  
document.getElementById("tcellmsgEntries1").innerHTML="<div class='subtaskmessage1'><table ><tr><td></td></tr></table></div>";
//width='600px;' <b>Enter here appropriate objects of action (descriptions of things/ data/ people) and as well as the context within which it happens.</b></td></tr>" +
//                                   "<tr><td> NOTE: Object of action may be a word or one or more phrases.
//setFocus("txtReqA");
}
if(i==3){
// Requirement_A_message
document.getElementById("tcellmsgEntries1").innerHTML= "<div class='subtaskmessage2'><table><tr><td></td></tr></table></div>";
hideIt(); 
//<b>Enter here descriptions of the <i>tools, instruments and/or machines</i> you use to accomplish this action.</b></td></tr>" +
//                                         "<tr><td>Example: 	Cut vegetable with a <span style='color:Red;' >knife</span> </td></tr>" +
//                                         "<tr><td> <span style='color:Red;' >Knife</span> is the tool used for the action.</td></tr>" +
//                                         "<tr><td>NOTE:   Using/with is already prefixed in the entry format, so don’t type it again.
//setFocus("txtReqB");
}
if(i==4){
// Requirement_B_message
document.getElementById("tcellmsgEntries1").innerHTML= "<div class='subtaskmessage3'><table><tr><td></td></tr></table></div>";
//<b>Enter here descriptions of <i>job-specific skills and knowledge</i> required from you to accomplish this action.</b></td></tr>" +
//                                         "<tr><td> Example: Drive a truck using <span style='color:Red;' >driving skills and knowledge of city map</span>. </td></tr>" +
//                                         "<tr><td> <span style='color:Red;' >Driving skills and knowledge of city map</span> are <span style='color:Red;' >job-</span>specific skills and knowledge.</td></tr>" +
//                                         "<tr><td> NOTE:   Using/with is already prefixed in the entry format, so don’t type it again.
document.getElementById("divReqC").visible=true;
hideIt(); 
}
if(i==5){
//Requirement_C_message
document.getElementById("tcellmsgEntries1").innerHTML= "<div class='subtaskmessage4'><table width='650px;'><tr><td></td></tr></table></div>";
//<b>Identify from the drop-down list the <i>individual specific abilities and skills</i> required from you to accomplish this action. And/or, enter here descriptions of appropriate abilities and skills, if you don’t find them in the drop-down list </b></td></tr>" +
//                                            "<tr><td> Example: 	Understand the problem using <span style='color:Red;' >analytical ability and attention to detail</span></td></tr>" +
//                                            "<tr><td> <span style='color:Red;' >Analytical ability and attention to detail</span> are <span style='color:Red;' >individual</span> ability and skill respectively.</td></tr>" +
//                                            "<tr><td> NOTE:   Using/with is already prefixed in the entry format, so don’t type it again.

hideIt(); 
}
if(i==6){
//purpose_message
document.getElementById("tcellmsgEntries1").innerHTML= "<div class='subtaskmessage5'><table><tr><td></td></tr></table></div>";
//<b>Enter here the objective/purpose of this action.  OR, click on OBJECTIVE UPLOAD button, if the objective of action is to serve the completion of its parent task/subtask.</b></td></tr>" +
//                                    "<tr><td> Example: 	Cut vegetable in order to cook food for guests </td></tr>" +
//                                    "<tr><td> In order to cook food for guests is the objective of action.</td></tr>" +
//                                    "<tr><td> NOTE:   In order to is already prefixed in the entry format, so don’t type it again.
hideIt(); 
}
}



function ShowProcess()
{
document.getElementById("divProcess").innerHTML="<img ID='imgProcessing' alt='' src='images/processing2.gif' />";

}
function ShowSelProcess()
{
document.getElementById("divProcessSel").innerHTML="<img ID='imgProcessing' alt='' src='images/processing2.gif' />";

}

function ShowSelProcess1()
{
document.getElementById("divProcessSel").innerHTML="<span style='color:blue;'>Processing .... </span>";

}


function clik()
{
var element=document.getElementById('txtAction');  
if (document.all)
{
var range= element.createTextRange();
range.collapse(false);
range.select();
} 
else {
element.focus();
var v= element.value;
element.value= '';
element.value= v;
}
}

var count=1;
var s;
function ImageShow()
{ 	
document.getElementById('AnimationPix').src = "images/Homepage_animation_" + count + ".jpg";
if(count==10){count=0;}
count=count + 1
s=setTimeout("ImageShow()",5000)
}


function GetScoreFile(sDATE,sControl)
{
	var ctr2= document.getElementById("txt");

 
	var elem = document.getElementsByTagName("input");
	
	if(elem==null)
	{
	alert("elememnt not found");
	}
	var ctrl;
	for(var i=0;i<elem.length;++i)
	{
	 ctrl=elem[i];
	 var ctrlName=ctrl.getAttribute("ID");	 
	 if(ctrlName.indexOf(sControl)!=-1)
	 {	  
	   document.forms[0][ctrlName].value=sDATE;
	 }	
	}
}
function dispExcelFile(dtCtrl)
{
window.open("ExportExcelFile.aspx?Ctrl=" + dtCtrl,"UploadActionVerbList","width=300,height=220,resizable=0,scrollbars=0,left=400,top=300");
}

function ResetScroll()
{
ShowSelProcess();
window.scrollTo(0,0);
}
var seconds=0 ;
var minutes=00;
var hours=00;
var value;
function countdown() 
    {
        //seconds = document.forms[0].Text1.value;
        if (seconds > 0 && seconds <60) 
        {
         seconds = parseFloat(seconds) + 1;
         if(seconds <10)
         seconds ="0" + seconds;
        }
        else if(seconds==60)
        {
            if(minutes >=0 && minutes<60)
                {
                minutes=parseFloat(minutes)+1;
                if(minutes<10)
                minutes= "0" + minutes
                }
            else 
                {
                if(minutes==60)
                {
                hours=parseFloat(hours)+1;   
                if(hours<10)
                hours="0" + hours;     
                }
                }
        }        
        else {
        seconds =1;
            }
            
//            if(parseInt(hours)<10)
//            hours="0" + hours;
//            if(parseInt(minutes)<10)
//            minutes= "0" + minutes
//            if(parseInt(seconds)<10)
//            seconds="0" + seconds;
            
            document.forms[0].Text1.value = hours+":"+minutes+":"+seconds;
            setTimeout("countdown()", 1000);
    }
//var starttime= new Date();
//    function GetCurrentTime(){
//    var currentTime = new Date()
////var hours = currentTime.getHours()
////var minutes = currentTime.getMinutes()
////if (minutes < 10){
////minutes = "0" + minutes
////}
////var time=hours + ":" + minutes + " "
//////document.getElementById(Test1).value=
////if(hours > 11){
////time= time + "PM";
////} else {
////time= time + "AM";
////}
//var diff;  
//    diff = (currentTime.getTime() - starttime.getTime());
//  
////var hours = diff.getHours();
////var minutes = diff.getMinutes();

//document.forms[0].Text1.value=diff.getHours().ToString() + ":" + diff.getMinutes().ToString();
//}

//var laterdate = Date();     // 1st January 2000
// 
////timeDifference(laterdate,earlierdate);

//function timeDifference()
//{
//var earlierdate = new Date();  // 13th March 1998
//var difference = laterdate.getTime() - earlierdate.getTime();
//var daysDifference = Math.floor(difference/1000/60/60/24);
//    difference -= daysDifference*1000*60*60*24
// 
//    var hoursDifference = Math.floor(difference/1000/60/60);
//    difference -= hoursDifference*1000*60*60
// 
//    var minutesDifference = Math.floor(difference/1000/60);
//    difference -= minutesDifference*1000*60
// 
//    var secondsDifference = Math.floor(difference/1000);
//     document.write('difference = ' + daysDifference + ' day/s ' + hoursDifference + ' hour/s ' + minutesDifference + ' minute/s ' + secondsDifference + ' second/s ');
//setTimeout("timeDifference()",1000)
//}

function GetTimeDiff(id) {
//var date1='<%=Session["startdate"];%>';
//alert(document.forms[0].starttime.value)//document.getElementById('starttime').value);
//var sessionvalue= document.counter.tblABudget_ItemRecordControl_starttime.value;
var date1=document.forms[0][id].value;// document.forms[0].starttime.value;//document.getElementById('starttime').value;//
//  var sessionValue = '<%= Session["SesValue"] %>';
//alert(sessionValue);

  //alert(date1);
  dt2 = new Date();
  dt1 = new Date();
  if(date1 != 0)
  dt1 = new Date(date1);
  else timeval=dt;// document.forms[0].starttime.value=dt1;
  var diff;

  if(dt1 > dt2) {
    diff = new Date(dt1 - dt2);
  } else {
    diff = new Date(dt2 - dt1);
  }
  setTimeout("GetTimeDiff()",5000)
  //diff.getHours()
  //var time=diff.getHours() + ":"+diff.getMinutes()+":"+diff.getSeconds();
  var hrs=parseInt(dt2.getHours())-parseInt(dt1.getHours());
  var min=parseInt( dt2.getMinutes())-parseInt( dt1.getMinutes());
  var sec=parseInt( dt2.getSeconds())-parseInt( dt1.getSeconds());
  var time= hrs+ ":"+min+":"+sec;
alert(time);
  //return diff;
}


////define universal reference to "staticcontent"
//var crossobj=document.all? 
//document.all.staticcontent : 
//document.getElementById("staticcontent")
////define reference to the body object in IE
var iebody=(document.compatMode && 
document.compatMode != "BackCompat")? 
document.documentElement : document.body
function positionit(){
//define universal dsoc left point
var dsocleft=document.all? iebody.scrollLeft : 
pageXOffset
//define universal dsoc top point
var dsoctop=document.all? iebody.scrollTop : 
pageYOffset

//if the user is using IE 4+ or Firefox/ NS6+document.all||
if (document.getElementById){
document.getElementById("staticcontent").style.right=25+"px"//parseInt(dsocleft)+5+"px"
document.getElementById("staticcontent").style.top=dsoctop+100+"px"
}setInterval("positionit()",1000)
}





