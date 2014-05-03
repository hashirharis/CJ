using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class MasterPage5 : System.Web.UI.MasterPage
{
    DBManagementClass clsClass = new DBManagementClass();
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();

    protected void Page_Load(object sender, EventArgs e)
       
{
        try
        {
            //////if (!Page.IsPostBack)
            //////    AddControl("Homepage.ascx");
            //if (Session["startdate"] == null)
            //    Session["startdate"] = DateTime.Now;
            //////lblstarttime.Text = Session["startdate"].ToString();
            if (Request.QueryString["uid" + DateTime.Now.ToString("dd-MM-yyyy")] != null)
                Session.Clear();

           ////// string sourcepath = "images\\bannerfiles\\";

            //////if (Session["LogoFileNameLeft"] != null)
            //////    imgLogoLeft.ImageUrl = sourcepath + Session["LogoFileNameLeft"].ToString();
            //////else imgLogoLeft.Visible = false;
            //////if (Session["LogoFileNameMiddle"] != null)
            //////    imgLogoMiddle.ImageUrl = sourcepath + Session["LogoFileNameMiddle"].ToString();
            //////else imgLogoMiddle.Visible = false;
            //////if (Session["LogoFileNameRight"] != null)
            //////    imgLogo.ImageUrl = sourcepath + Session["LogoFileNameRight"].ToString();
            //////else imgLogo.Visible = false;



            if (Session["UserID"] != null)
            { lbtnLogout.Visible = true; }
            //else { pnlLogin.Visible = true; lblMessage_Login.Text = "redirect from careerjudge,, userid=" + Session["UserID"]; return; }

            int userid = 0;
            if (Session["ControlToReDirect"] != null)
            {
                string str = Session["ControlToReDirect"].ToString();
                //ReDirectToCareerJudge(Session["ControlToReDirect"].ToString());
                AddControl(str);
                //////////System.Web.UI.Control Control_ToAdd;
                //////////Control_ToAdd = LoadControl(Session["ControlToReDirect"].ToString());
                //////////Control_ToAdd.ID = "fja";
                //////////cplhLoader.Controls.Clear();
                //////////cplhLoader.Controls.Add(pnlRedirect);
                ////ReDirectToCareerJudge(Session["ControlToReDirect"].ToString());
                ////Session["ControlToReDirect"] = null;
                //ShowPopup();
            }
            else if (LastLoadedControl != null)
            {
                AddControl(LastLoadedControl);
            }
            /*    else {  AddControl("Homepage.ascx"); }  //Timer1.Enabled = true;else AddControl("ReportPreviewCtrl.ascx"); // 

            //// else AddControl("UserCreation.ascx");
          */
            else
            {
                ////////bipson 12082010
                ////if (Session["dirLogin"] != null)
                ////    if (Session["dirLogin"].ToString() == "Yes")
                ////    { pnlLogin.Visible = true; return; }
                ////////

                if (Session["UserID"] != null)
                { lbtnLogout.Visible = true; }
                //else { pnlLogin.Visible = true; lblMessage_Login.Text = "redirect from careerjudge,, userid=" + Session["UserID"]; }


                if (Request.QueryString["uid" + DateTime.Now.ToString("dd-MM-yyyy")] != null)
                {
                    //Session.Clear();
                    userid = int.Parse(Request.QueryString["uid" + DateTime.Now.ToString("dd-MM-yyyy")].ToString());
                    ////lblmessage.Text = userid.ToString();
                    CheckUserDetails(userid, "No");
                }
                //if (Request.QueryString[DateTime.Now.ToString("dd-MM-yyyy")] != null)
                //    userid = int.Parse(Request.QueryString[DateTime.Now.ToString("dd-MM-yyyy")].ToString());

                else if (Session["UserID"] == null)
                    AddControl("Homepage.ascx");
                //////{ pnlLogin.Visible = true; return; }
                //////else Response.Redirect("CareerJudge.htm");
            }
            //else lblmessage.Text = "nocontrol found";
            // else AddControl("Homepage.ascx"); 

            GetTotalTime();
        }
        catch (Exception ex) { lblmessage.Text = ex.Message; }
    }

    private void GetTotalTime()
    { // Session["starttime"] = DateTime.Now;
        if (Session["starttime"] != null)
        {
            pnltimer.Visible = true;
            DateTime startTime, endTime;
            string output = "";
            TimeSpan span;
            //if (Session["starttime"] == null)
            //    Session["starttime"] = DateTime.Now;

            startTime = DateTime.Parse(Session["starttime"].ToString());
            startTime = (DateTime)Session["starttime"];
            endTime = DateTime.Now;

            span = new TimeSpan(endTime.Ticks - startTime.Ticks);
            string[] newtime = span.ToString().Split(new char[] { '.' });
            lblTime.Text = newtime[0];// span.ToString().Split(new char[]{('.')};
        }
        //span = new TimeSpan(endTime.Ticks - startTime.Ticks);
        //lblTime.Text = span.TotalSeconds.ToString();
        //Response.Write("<hr>Time Using StringBuilder: " + span.TotalSeconds.ToString() + "<hr>");
    }

    private void CheckUserDetails(int userid,string dirlog)
    {
        if (dirlog == "No")
            Session.Clear();

        var LoginDetails1 = from LoginDetails in dataclass.UserProfiles
                            where LoginDetails.UserId == userid && LoginDetails.Status == 1
                            select LoginDetails;
        if (LoginDetails1.Count() > 0)
        {
            ////////////////

            if (LoginDetails1.First().UserType.ToString() != "SuperAdmin")
            {
                Boolean dtAssigned = false;
                //int userid = 0;
                DateTime dtfrom = DateTime.Today;
                if (LoginDetails1.First().LoginFromDate != null)
                {
                    dtfrom = DateTime.Parse(LoginDetails1.First().LoginFromDate.ToString());
                    dtAssigned = true;
                }
                DateTime dtto = DateTime.Today;
                if (LoginDetails1.First().LoginToDate != null)
                {
                    dtto = DateTime.Parse(LoginDetails1.First().LoginToDate.ToString());
                    dtAssigned = true;
                }
                DateTime today = DateTime.Today;
                if (dtAssigned == false)
                { lblMessage_Login.Text = "You cant Login now. Contact  site admin"; return; }
                if (dtfrom <= today && dtto >= today)
                { }
                else { lblMessage_Login.Text = "You cant Login now. Contact  site admin"; return; }

            }

            /////////////

          //  int userid = 0;
            //if (Session["UserID"] != null)
            //    userid = int.Parse(Session["UserID"].ToString());

            if (LoginDetails1.First().UserId != null)
            {
                Session["UserID"] = LoginDetails1.First().UserId.ToString();
                userid = int.Parse(Session["UserID"].ToString());
            }
            if (LoginDetails1.First().ReportAccess != null)
                Session["CurUserReportAccess"] = LoginDetails1.First().ReportAccess.ToString();


            if (LoginDetails1.First().UserType != null)
            {
                Session["usertype"] = LoginDetails1.First().UserType.ToString();
                if (LoginDetails1.First().UserType.ToString() == "SuperAdmin")
                {                    
                    Session["SubCtrl"] = "UserCreation.ascx";
                    Session["MasterCtrl"] = "~/MasterPage4.master";                     
                    Response.Redirect("FJAHome.aspx");
                }
                else if (LoginDetails1.First().UserType.ToString() == "SpecialAdmin")
                {
                    Session["AdminOrganizationID"] = LoginDetails1.First().OrganizationID.ToString();
                    Session["SubCtrl"] = null;// "SpecialUserCreation.ascx";
                    Session["MasterCtrl"] = "~/MasterPage6.master";                    
                    Response.Redirect("FJAHome.aspx");
                }
                else if (LoginDetails1.First().UserType.ToString() == "OrgAdmin")
                {
                    Session["AdminOrganizationID"] = LoginDetails1.First().OrganizationID.ToString();
                    Session["SubCtrl"] = "ReportSel_OrgAdmin.ascx"; Response.Redirect("FJAHome.aspx");
                    return;
                }
                else if (LoginDetails1.First().UserType.ToString() == "GrpAdmin")
                {
                    Session["AdminOrganizationID"] = LoginDetails1.First().OrganizationID.ToString();
                    Session["AdminGroupID"] = LoginDetails1.First().GrpUserID.ToString();
                    Session["SubCtrl"] = "ReportSel_GroupAdmin.ascx"; Response.Redirect("FJAHome.aspx");

                    return;
                }
                else
                {
                    int testid = 0;
                    if (LoginDetails1.First().TestId != null && LoginDetails1.First().TestId != 0)
                    {
                        Session["UserTestId"] = int.Parse(LoginDetails1.First().TestId.ToString());
                        testid = int.Parse(LoginDetails1.First().TestId.ToString());
                    }
                    else
                    {
                        lblMessage_Login.Text = "No Test assigned for you. Please Contact to your admin ";
                        return;
                    }

                    //if (passmark == 0) { lblMessage.Text = "Passmark for your Test is not defined;"; return; }
                    int orgid = 0;
                    if (LoginDetails1.First().OrganizationID != null && LoginDetails1.First().OrganizationID != 0)
                        orgid = int.Parse(LoginDetails1.First().OrganizationID.ToString());

                    if (orgid > 0)
                    {
                        var OrgLogoName = from logofile in dataclass.Organizations
                                          where logofile.OrganizationID == orgid
                                          select logofile;
                        if (OrgLogoName.Count() > 0)
                        {
                            string sourcepath, fileName;
                            sourcepath = "images\\bannerfiles\\";// + fileName;
                            if (OrgLogoName.First().LogoFileNameLeft != null && OrgLogoName.First().LogoFileNameLeft != "")
                            {
                               fileName=OrgLogoName.First().LogoFileNameLeft.ToString();
                               if (File.Exists(Server.MapPath(sourcepath + fileName)))                    
                                Session["LogoFileNameLeft"] = OrgLogoName.First().LogoFileNameLeft.ToString();
                            }
                            if (OrgLogoName.First().LogoFileNameMiddle != null && OrgLogoName.First().LogoFileNameMiddle != "")
                            {
                                fileName = OrgLogoName.First().LogoFileNameMiddle.ToString();
                                if (File.Exists(Server.MapPath(sourcepath + fileName)))  
                                Session["LogoFileNameMiddle"] = OrgLogoName.First().LogoFileNameMiddle.ToString();

                            }
                            if (OrgLogoName.First().LogoFileName != null && OrgLogoName.First().LogoFileName != "")
                            {
                                fileName = OrgLogoName.First().LogoFileName.ToString();
                                if (File.Exists(Server.MapPath(sourcepath + fileName)))  
                                Session["LogoFileNameRight"] = OrgLogoName.First().LogoFileName.ToString();
                            }
                            
                        }
                    }

                    string usercode = "";
                    string curcontrol = "";
                    int Evalstatid = 0;
                    //var EvaluationDetails = from EvalDet in dataclass.EvaluationStatus
                    //                        where EvalDet.EvalCompletionStatus == 0 && EvalDet.UserId == userid
                    //                        select EvalDet;
                    var EvaluationDetails = from EvalDet in dataclass.EvaluationStatus
                                            where EvalDet.UserId == userid
                                            select EvalDet;
                    if (EvaluationDetails.Count() > 0)
                    {
                        if (EvaluationDetails.First().EvalCompletionStatus != null)
                            if (EvaluationDetails.First().EvalCompletionStatus.ToString() == "1")
                            {
                                if (Session["CurUserReportAccess"] != null)
                                    if (Session["CurUserReportAccess"].ToString() == "1")
                                    { Session["SubCtrl"] = "ThankYou.ascx"; Response.Redirect("FJAHome.aspx"); return; }

                                lblMessage_Login.Text = "You have already attended the test. For more details please contact  site admin"; return;
                            }
                            else
                            {
                                DeleteIncompleteTestDetails(userid, testid);
                                //dataclass.DeleteEvaluationResult(userid); 
                                dataclass.Procedure_DeleteUserTest_TempValues(userid, 0, 0);
                            }

                        if (EvaluationDetails.First().EvalStatusId != null)
                            Session["EvalStatId"] = EvaluationDetails.First().EvalStatusId.ToString();
                        Evalstatid = int.Parse(Session["EvalStatId"].ToString());
                        Session["UserCode"] = EvaluationDetails.First().UserCode.ToString();
                        //Session["UserID"] = EvaluationDetails.First().UserId.ToString();
                        curcontrol = EvaluationDetails.First().EvalControl.ToString();
                        //}
                    }

                    if (Session["UserCode"] == null)
                    {
                        Session["UserCode"] = "UID_" + Session["UserID"].ToString() + "_" + DateTime.Now;
                        usercode = Session["UserCode"].ToString();
                    }
                    if (curcontrol == "")
                    {
                        curcontrol = "WelcomePage.ascx";// "UserProfileControl.ascx";
                    }
                    //// bipson 12082010
                    if (Session["dirLogin"] != null)
                        if (Session["dirLogin"].ToString() == "Yes")
                            curcontrol = "UserProfileControl.ascx";

                    //dataclass.ProcedureEvaluationStatus(Evalstatid, curcontrol, 0, 0, usercode, userid);
                    Session["SubCtrl"] = curcontrol;//"TaskEntry.ascx";//   "UserProfileControl.ascx";
                    //Session["MasterCtrl"] = "~/MasterPage2.master";
                    Response.Redirect("FJAHome.aspx");
                    //Session["MasterCtrl"] = "~/MasterPage2.master";
                    //Response.Redirect("Test.aspx");
                }
            }
        }
        else
        {
            lblMessage_Login.Text = "Incorrect UserName/Password! ";
            return;
        }
    }
    private void DeleteIncompleteTestDetails(int userid, int testid)
    {

        string condition = "";
        var getTestSectiondetails = from testsecdet in dataclass.UserTestSectionDetails
                                    where testsecdet.UserId == userid && testsecdet.TestId == testid
                                    select testsecdet;
        if (getTestSectiondetails.Count() > 0)
        {
            foreach (var testsecids in getTestSectiondetails)
            {
                if (condition != "")
                    condition += " and ";

                condition += " TestSectionId <> " + testsecids.TestSectionId;
            }


        }
        if (condition != "")
        {
            string querystring = "delete from EvaluationResult where UserId=" + userid + " and TestId=" + testid + " and " + condition;

            clsClass.ExcecuteDB(querystring);
        }
        else
        {
            string querystring = "delete from EvaluationResult where UserId=" + userid;
            clsClass.ExcecuteDB(querystring);
        }
    }

    //}
    private void AddControl(string ControlPath)
    {
        try
        {
            System.Web.UI.Control Control_ToAdd;
            Control_ToAdd = LoadControl(ControlPath);
            Control_ToAdd.ID = "fja";
            cplhLoader.Controls.Clear();
            cplhLoader.Controls.Add(Control_ToAdd);
            LastLoadedControl = ControlPath;
        }
        catch (Exception ex) { lblmessage.Text = ex.Message;}//
    }
    private string LastLoadedControl
    {
        get
        {
            return Session["SubCtrl"] as string;
        }
        set
        {
            Session["SubCtrl"] = value;
        }
    }
    
    protected void lbtnHome_Click(object sender, EventArgs e)
    {
        AddControl("Homepage.ascx");//AddControl("HomeControl.ascx");
        //Session["SubCtrl"] = "Homepage.ascx"; Response.Redirect("FJAHome.aspx");

      //  Response.Redirect("CareerJudge.htm");
        //Session["ControlToReDirect"] = "Homepage.ascx";
        //ShowPopup();

    }
    protected void lbtnInformationKit_Click(object sender, EventArgs e)
    {
        //AddControl("DownLoadFiles.ascx");
       // ReDirectToCareerJudge("InfoKit.ascx");
        AddControl("InfoKit.ascx");
        Session["ControlToReDirect"] = "InfoKit.ascx";
        //ShowPopup();
    }
    protected void lbtnTakeaTest_Click(object sender, EventArgs e)
    {
       // AddControl("TakeaTestOnline.ascx");
       // ReDirectToCareerJudge("TakeTest.ascx");
        AddControl("TakeTest.ascx");
        Session["ControlToReDirect"] = "TakeTest.ascx";
        //ShowPopup();

    }
    protected void lbtnProducts_Click(object sender, EventArgs e)
    {
        AddControl("Products.ascx");
    }
    protected void lbtnServices_Click(object sender, EventArgs e)
    {
        AddControl("Services.ascx");
    }
    protected void lbtnAboutUs_Click(object sender, EventArgs e)
    {
        AddControl("AboutUS.ascx");
       //// ReDirectToCareerJudge("AboutUS.ascx");

       // Session["ControlToReDirect"] = "AboutUS.ascx";
       // ShowPopup();
    }
    protected void lbtnContactUs_Click(object sender, EventArgs e)
    {
        AddControl("ContactUsControl.ascx");
        Session["ControlToReDirect"] = "ContactUsControl.ascx";
       ////// ShowPopup();
       // ReDirectToCareerJudge("ContactUsControl.ascx");

    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        /*
        ////// only for local test
        Session.Clear();

        Session["SubCtrl"] = null;
        ////Session["MasterCtrl"] = "~/MasterPage.master";
        Response.Redirect("FJAHome.aspx");
       // ReDirectToCareerJudge("HomePage.ascx");

        //////
        return;
        */

        Session["ControlToReDirect"] = "HomePage.ascx";
        //ShowPopup();

    }
    private void ReDirectToCareerJudge(string controlname)
    {        
        //// 230110 bip
        int userid = 0;
        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());
        dataclass.Procedure_DeleteUserTest_TempValues(userid, 0, 0);
        ////       
        
        Session["starttime"] = null;//bip 15062010

        //// bipson 12082010
        if (Session["dirLogin"] != null)
            if (Session["dirLogin"].ToString() == "Yes")
            {
                Session.Clear();
                Session["SubCtrl"] = null;
                Session["MasterCtrl"] = "~/MasterPage5.master";
                Response.Redirect("FJAHome.aspx");
                return;
            }

        Session.Clear();
        Response.Redirect("FJAHome.aspx?ctrlid=" + controlname);
       // Response.Redirect("http://careerjudge.com/FJAHome.aspx?ctrlid=" + controlname);

    }
    protected void mnuAboutUs_MenuItemClick(object sender, MenuEventArgs e)
    {
        MenuItem mnuitem = new MenuItem();
        mnuitem = e.Item; if (mnuitem.Value == "AboutUs") return;
        Session["ControlToReDirect"] = mnuitem.Value;

        AddControl(mnuitem.Value);
        
        //ShowPopup();

        //ReDirectToCareerJudge(mnuitem.Value);
        //Session["SubCtrl"] = mnuitem.Value;
        //Response.Redirect("FJAHome.aspx");
    }
    protected void mnuProducts_MenuItemClick(object sender, MenuEventArgs e)
    {
        MenuItem mnuitem = new MenuItem();
        mnuitem = e.Item; if (mnuitem.Value == "Products") return;
        Session["ControlToReDirect"] = mnuitem.Value;
        AddControl(mnuitem.Value);
        //ShowPopup();

        // ReDirectToCareerJudge(mnuitem.Value);
        //Session["SubCtrl"] = mnuitem.Value;
        //Response.Redirect("FJAHome.aspx");
    }
    protected void mnuServices_MenuItemClick(object sender, MenuEventArgs e)
    {
        MenuItem mnuitem = new MenuItem();
        mnuitem = e.Item; if (mnuitem.Value == "Services") return;
        Session["ControlToReDirect"] = mnuitem.Value;
        AddControl(mnuitem.Value);
        //ShowPopup();
       // ReDirectToCareerJudge(mnuitem.Value);
        //Session["SubCtrl"] = mnuitem.Value;
        //Response.Redirect("FJAHome.aspx");
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (Session["ControlToReDirect"] != null)
        {
            ReDirectToCareerJudge(Session["ControlToReDirect"].ToString());
            Session["ControlToReDirect"] = null;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = null;
        if (LastLoadedControl != null)
        {
            AddControl(LastLoadedControl);
        }
    }
    private void ShowPopup()
    {
        pnlRedirect.Visible = true;
        cplhLoader.Controls.Clear();
        cplhLoader.Controls.Add(pnlRedirect);

    }
    protected void lbtnPrivacyPolicy_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "PrivacyPolicy.ascx";
        //ShowPopup();
    }
    protected void lbtnTermsAndConditions_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "TermsAndConditions.ascx";
        //ShowPopup();
    }
    protected void lbtnIPR_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "IPR.ascx";
        //ShowPopup();
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        
        if (txtUsername.Text != "" && txtPassword.Text != "")
        {
            Session.Clear();
            Session["dirLogin"] = "Yes";
            
            int userid_new = 0;

            var LoginDetails1 = from LoginDetails in dataclass.UserProfiles
                                where LoginDetails.UserName == txtUsername.Text && LoginDetails.Password == txtPassword.Text && LoginDetails.Status == 1
                                select LoginDetails;
            //var LoginDetails1 = from LoginDetails in dataclass.UserProfiles
            //                    where LoginDetails.UserName == "admin" && LoginDetails.Password == "123" && LoginDetails.Status == 1
            //                    select LoginDetails;
            lblmessage.Text = LoginDetails1.Count().ToString();
            if (LoginDetails1.Count() > 0)
            {
                userid_new = int.Parse(LoginDetails1.First().UserId.ToString());
                CheckUserDetails(userid_new,"Yes");
            }
            else lblMessage_Login.Text = "Invalid username/password";
        }
        else lblMessage_Login.Text = "Enter username/password";

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Session.Clear();
        txtPassword.Text = ""; txtUsername.Text = "";
    }
    protected void LinkBulbtnContactus1_Click(object sender, EventArgs e)
    {
        AddControl("ContactUsControl.ascx");
        Session["ControlToReDirect"] = "ContactUsControl.ascx";
    }
    protected void LinkBulbtnAboutus1_Click(object sender, EventArgs e)
    {
        AddControl("AboutUs.ascx");
        Session["ControlToReDirect"] = "AboutUs.ascx";
    }
}
