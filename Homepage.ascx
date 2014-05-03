<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Homepage.ascx.cs" Inherits="Homepage" %>

<link rel="stylesheet" type="text/css" href="FJAStyles.css" />
<link href="style.css" rel="stylesheet" type="text/css" />


<!-- Start WOWSlider.com HEAD section -->
	<link rel="stylesheet" type="text/css" href="engine1/style.css" media="screen" />
	<style type="text/css">a#vlb{display:none}</style>
	<script type="text/javascript" src="engine1/jquery.js"></script>
	<!-- End WOWSlider.com HEAD section -->



<%--<script type="text/jscript" src="FJAJScript.js"></script>--%>
<script language="JavaScript" type="text/JavaScript">
var count=1;
var s;
function ImageShow1()
{
var ss=document.getElementById('AnimationPix');
if(ss !=null){
document.getElementById('AnimationPix').src = "images/Homepage_animation_" + count + ".jpg";
if(count==10){count=0;}
count=count + 1
s=setTimeout("ImageShow1()",5000)}
}
</script>
<%--<br /><br />onload="ImageShow1();"--%>
<body >
<div id="main_box_middle">
    
   	  <div class="main_box_left">
        	<div class="left_animation">
            
            
            
            
            	<!-- Start WOWSlider.com BODY section -->
	<div id="wowslider-container1">
	<div class="ws_images">
<a href="#"><img src="data1/images/1.jpg" alt="1" title="1" id="wows0"/></a>
<a href="#"><img src="data1/images/2.jpg" alt="2" title="2" id="wows1"/></a>
<a href="#"><img src="data1/images/3.jpg" alt="3" title="3" id="wows2"/></a>
<a href="#"><img src="data1/images/4.jpg" alt="4" title="4" id="wows3"/></a>
</div>
<a style="display:none" href="http://wowslider.com">Html5 Image Carousel by WOWSlider.com v1.7</a>
	
	</div>
	<script type="text/javascript" src="engine1/script.js"></script>
	<!-- End WOWSlider.com BODY section -->
            
            
            
            </div>
            

   <div class="left_text">
   		<div class="left_text_head">What</div>
        <div class="left_text_head_2">we do</div>
        <div class="left_text_inner">Provide a technology and a professional platform for identifying, measuring and developing talent</div>
   </div>
    <div class="left_text_2">
    
    
    
    
    		<div class="left_text_head">How</div>
        <div class="left_text_head_2">we do it</div>
        <div class="left_text_inner">Extract, analyze and process objective and valid information about job-worker dynamics through CODE-Informatics ™, our web-based information processing technology. <br/>Develop and deliver customized assessments for measuring psychometric, aptitude and skill tests through TalentSCOUT ™, our Online Assessment Engine.<br/>
Guide young minds in choosing right careers and pursuing excellence through career profiling, counseling and grooming.</div>
    
    
    
    
    
    
    
    
    
    
    </div>
            
            
            
            
            
      </div>
        <div class="main_box_right">
        	<div class="right_banner"></div>
            <div class="right_menu">
            	<div class="right_menu_bg">
                    <asp:LinkButton ID="lnkbtn_codeinformatics" runat="server" OnClick="lnkbtn_codeinformatics_Click">CODE-Informatics™</asp:LinkButton>
                    <%--<a href="#">CODE-Informatics™</a>--%></div>
                <div class="right_menu_bg">
                    <asp:LinkButton ID="lnkbtn_talentscout" runat="server" OnClick="lnkbtn_talentscout_Click" >TalentSCOUT™</asp:LinkButton>
                    <%--<a href="#">TalentSCOUT™</a>--%></div>
                <div class="right_menu_bg">
                    <asp:LinkButton ID="lnkbtn_hrservice" runat="server" OnClick="lnkbtn_hrservice_Click"  >Corporate HR Services</asp:LinkButton>
                 <%--   <a href="#">Corporate HR Services</a>--%></div>
                <div class="right_menu_bg">
                    <asp:LinkButton ID="lnkbtn_assessmentService" runat="server" OnClick="lnkbtn_assessmentService_Click"  >Assessment Services</asp:LinkButton>
                    <%--<a href="#">Assessment Services</a>--%></div>
                <div class="right_menu_bg">
                    <asp:LinkButton ID="lnkbtn_careermanagement" runat="server" OnClick="lnkbtn_careermanagement_Click"  >Career ManagementServices</asp:LinkButton>
                 <%--   <a href="#">Career ManagementServices</a>--%></div>
                <div class="right_menu_bg">
                    <asp:LinkButton ID="lnkbtn_taketest" runat="server" OnClick="lnkbtn_taketest_Click"  >Take a Test</asp:LinkButton>
                    <%--<a href="#">Take a Test</a>--%></div>
            </div>
            <div class="right_text">
            
            
            
            
            
            
           <div class="hd"> Tests for IT & ITES</div>
Common IT Aptitude Test (CITAT)<br/>
Test for Project Management Skills (TPMS)
<div class="hd"> BPO-Call Center Readiness Tests</div>
BPO Readiness Test – Voice Process (BPO-RTV)
BPO Readiness Test – Non-Voice Process (BPO-RTN)
Psychometric Assessment for Tele-Selling (PATS)
<div class="hd"> Assessments for Leader Potential</div>
People Management Skills Test (PMST)
Test for Effective Communication Skills (TECS)
Test for Assertiveness Skills (TAS)
Test for Effective Presentation Skills (TEPS)
 <div class="hd">Tests for Art Aptitudes</div>
Visual Art Aptitude Test (VAAT)<br/>
Aptitude Test for Performing Arts (ATPA)
            
            
            
            
            
            
            </div>
        </div>
    </div>
</body>