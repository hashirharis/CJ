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


public partial class MasterPage4 : System.Web.UI.MasterPage
{
    DBManagementClass clsClass = new DBManagementClass();
   AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["UserID"] != null)
        {lbtnLogout.Visible = true; lbtnChangePassword.Visible = true;}

        if (Session["ControlToReDirect"] != null)
        {           
            ShowPopup();
        }
        else if (LastLoadedControl != null)
        {
            AddControl(LastLoadedControl);
        }
        //else AddControl("HomeControl.ascx");
        divmenu.Visible = false;
        if (Session["usertype"] != null)
            if (Session["usertype"].ToString() == "SuperAdmin")
                divmenu.Visible = true;
    }
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
        ////AddControl("Homepage.ascx");
        //Session["SubCtrl"] = "Homepage.ascx"; Response.Redirect("FJAAdmin.aspx");
       // Response.Redirect("CareerJudge.htm");

        Session["ControlToReDirect"] = "Homepage.ascx";
        ShowPopup();
    }
    protected void lbtnInformationKit_Click(object sender, EventArgs e)
    {
        //AddControl("DownLoadFiles.ascx");
       // ReDirectToCareerJudge("InfoKit.ascx");

        Session["ControlToReDirect"] = "TakeTest.ascx";
        ShowPopup();
    }
    protected void lbtnTakeaTest_Click(object sender, EventArgs e)
    {
        //AddControl("TakeaTestOnline.ascx");
       // ReDirectToCareerJudge("TakeTest.ascx");

        Session["ControlToReDirect"] = "TakeTest.ascx";
        ShowPopup();
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
        //AddControl("AboutUS.ascx");
       // ReDirectToCareerJudge("AboutUS.ascx");

        Session["ControlToReDirect"] = "AboutUS.ascx";
        ShowPopup();
    }
    protected void lbtnContactUs_Click(object sender, EventArgs e)
    {
        // AddControl("ContactUsControl.ascx");
        // ReDirectToCareerJudge("ContactUsControl.ascx");

        Session["ControlToReDirect"] = "ContactUsControl.ascx";
        ShowPopup();
    }

    protected void mnuSideMenu_MenuItemClick(object sender, MenuEventArgs e)
    {
        MenuItem mnuitem = new MenuItem();
        mnuitem = e.Item;
        if (mnuitem.Target == ".") return;
        AddControl(mnuitem.Value);
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        ////////// only for local test

        ////Session.Clear();
        ////Session["SubCtrl"] = null;
        ////Session["MasterCtrl"] = "~/MasterPage5.master";
        ////Response.Redirect("FJAHome.aspx");
        
        //////ReDirectToCareerJudge("HomePage.ascx");

        //////////     

        
        Session["ControlToReDirect"] = "HomePage.ascx";
        ShowPopup();
    }
    protected void mnuAboutUs_MenuItemClick(object sender, MenuEventArgs e)
    {
        MenuItem mnuitem = new MenuItem();
        mnuitem = e.Item; if (mnuitem.Value == "AboutUs") return;
       // ReDirectToCareerJudge(mnuitem.Value);
        Session["ControlToReDirect"] = mnuitem.Value;
        ShowPopup();
    }
    protected void mnuProducts_MenuItemClick(object sender, MenuEventArgs e)
    {
        MenuItem mnuitem = new MenuItem();
        mnuitem = e.Item; if (mnuitem.Value == "Products") return;
        //ReDirectToCareerJudge(mnuitem.Value);
        Session["ControlToReDirect"] = mnuitem.Value;
        ShowPopup();
    }
    protected void mnuServices_MenuItemClick(object sender, MenuEventArgs e)
    {
        MenuItem mnuitem = new MenuItem();
        mnuitem = e.Item; if (mnuitem.Value == "Services") return;
        //ReDirectToCareerJudge(mnuitem.Value);
        Session["ControlToReDirect"] = mnuitem.Value;
        ShowPopup();
    }
    private void ReDirectToCareerJudge(string controlname)
    {
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
        Response.Redirect("http://careerjudge.com/FJAHome.aspx?ctrlid=" + controlname);

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
        ShowPopup();
    }
    protected void lbtnTermsAndConditions_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "TermsAndConditions.ascx";
        ShowPopup();
    }
    protected void lbtnIPR_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "IPR.ascx";
        ShowPopup();
    }
    protected void lbtnChangePassword_Click(object sender, EventArgs e)
    {
        Session["pwdChanged"] = null;
        Session["SubCtrl"] = "ChangePasswordControl.ascx";
        Response.Redirect("FJAHome.aspx");
    }
    private void CheckUserDetails(int userid, string dirlog)
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
                { //lblMessage_Login.Text = "You cant Login now. Contact  site admin"; 
                    return; }
                if (dtfrom <= today && dtto >= today)
                { }
                else { //lblMessage_Login.Text = "You cant Login now. Contact  site admin"; return;
                }

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
                       // lblMessage_Login.Text = "No Test assigned for you. Please Contact to your admin ";
                        return;
                    }

                    //if (passmark == 0) { lblMessage.Text = "Passmark for your Test is not defined;"; return; }
                    int orgid = 0;
                    if (LoginDetails1.First().OrganizationID != null && LoginDetails1.First().OrganizationID != 0)
                        orgid = int.Parse(LoginDetails1.First().OrganizationID.ToString());

                    if (orgid > 0)
                    {
                        //////var OrgLogoName = from logofile in dataclass.Organizations
                        //////                  where logofile.OrganizationID == orgid
                        //////                  select logofile;
                        //////if (OrgLogoName.Count() > 0)
                        //////{
                        //////    string sourcepath, fileName;
                        //////    sourcepath = "images\\bannerfiles\\";// + fileName;
                        //////    if (OrgLogoName.First().LogoFileNameLeft != null && OrgLogoName.First().LogoFileNameLeft != "")
                        //////    {
                        //////        fileName = OrgLogoName.First().LogoFileNameLeft.ToString();
                        //////        if (File.Exists(Server.MapPath(sourcepath + fileName)))
                        //////            Session["LogoFileNameLeft"] = OrgLogoName.First().LogoFileNameLeft.ToString();
                        //////    }
                        //////    if (OrgLogoName.First().LogoFileNameMiddle != null && OrgLogoName.First().LogoFileNameMiddle != "")
                        //////    {
                        //////        fileName = OrgLogoName.First().LogoFileNameMiddle.ToString();
                        //////        if (File.Exists(Server.MapPath(sourcepath + fileName)))
                        //////            Session["LogoFileNameMiddle"] = OrgLogoName.First().LogoFileNameMiddle.ToString();

                        //////    }
                        //////    if (OrgLogoName.First().LogoFileName != null && OrgLogoName.First().LogoFileName != "")
                        //////    {
                        //////        fileName = OrgLogoName.First().LogoFileName.ToString();
                        //////        if (File.Exists(Server.MapPath(sourcepath + fileName)))
                        //////            Session["LogoFileNameRight"] = OrgLogoName.First().LogoFileName.ToString();
                        //////    }

                        //////}
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

                                //lblMessage_Login.Text = "You have already attended the test. For more details please contact  site admin"; return;
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
           // lblMessage_Login.Text = "Incorrect UserName/Password! ";
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
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
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
            //lblmessage.Text = LoginDetails1.Count().ToString();
            if (LoginDetails1.Count() > 0)
            {
                userid_new = int.Parse(LoginDetails1.First().UserId.ToString());
                CheckUserDetails(userid_new, "Yes");
            }
        //    else //lblMessage_Login.Text = "Invalid username/password";
       }
        //else //lblMessage_Login.Text = "Enter username/password";

    }
}
