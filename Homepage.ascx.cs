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

public partial class Homepage : System.Web.UI.UserControl
{
    DBManagementClass clsClass = new DBManagementClass();
 AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
            //txtUsername.Focus();
    }
    protected void imgBtnLogin_Click(object sender, ImageClickEventArgs e)
    {
        ////try
        ////{
        ////Session["SubCtrl"] = "ReportPreviewCtrl.ascx";
        ////Response.Redirect("FJAHome.aspx");
        ////return;
        //    if (txtUsername.Value != "" && txtPassword.Value != "")
        //    {
        //        Session.Clear();
                


        //        //lblMessage.Text = "login success" + txtUsername.Value + " , " + txtPassword.Value; return;
        //        var LoginDetails1 = from LoginDetails in dataclass.UserProfiles
        //                            where LoginDetails.UserName == txtUsername.Value && LoginDetails.Password == txtPassword.Value
        //                            select LoginDetails;
        //        if (LoginDetails1.Count() > 0)
        //        {
        //            ////////////////

        //            if (LoginDetails1.First().UserType.ToString() != "SuperAdmin")
        //            {


        //                Boolean dtAssigned = false;
        //                //int userid = 0;
        //                DateTime dtfrom = DateTime.Today;
        //                if (LoginDetails1.First().LoginFromDate != null)
        //                {
        //                    dtfrom = DateTime.Parse(LoginDetails1.First().LoginFromDate.ToString());
        //                    dtAssigned = true;
        //                }
        //                DateTime dtto = DateTime.Today;
        //                if (LoginDetails1.First().LoginToDate != null)
        //                {
        //                    dtto = DateTime.Parse(LoginDetails1.First().LoginToDate.ToString());
        //                    dtAssigned = true;
        //                }
        //                DateTime today = DateTime.Today;
        //                if (dtAssigned == false)
        //                { lblMessage.Text = "You cant Login now. Contact  site admin"; return; }
        //                if (dtfrom <= today && dtto >= today)
        //                { }
        //                else { lblMessage.Text = "You cant Login now. Contact  site admin"; return; }

        //            }

        //            /////////////

        //            int userid = 0;
        //            //if (Session["UserID"] != null)
        //            //    userid = int.Parse(Session["UserID"].ToString());

        //            if (LoginDetails1.First().UserId != null)
        //            {
        //                Session["UserID"] = LoginDetails1.First().UserId.ToString();
        //                userid = int.Parse(Session["UserID"].ToString());
        //            }
        //            if (LoginDetails1.First().ReportAccess != null)                    
        //                Session["CurUserReportAccess"] = LoginDetails1.First().ReportAccess.ToString();
                        
                    

        //            ////
        //            //Session["UserCode"] = "UID_" + Session["UserID"].ToString() + "_" + DateTime.Now;
        //            //Session["SubCtrl"] = "TaskEntry.ascx";//"ControlledTaskEntryPhaseCtrl.ascx";
        //            //Session["MasterCtrl"] = "~/MasterPage2.master";
        //            //Response.Redirect("FJAHome.aspx");
        //            ////

        //            if (LoginDetails1.First().UserType != null)
        //            {
        //                Session["usertype"] = LoginDetails1.First().UserType.ToString();
        //                if (LoginDetails1.First().UserType.ToString() == "SuperAdmin")
        //                {
        //                    //Session["usertype"] = "SuperAdmin";
        //                    Session["SubCtrl"] = "UserCreation.ascx";// null;
        //                    Session["MasterCtrl"] = "~/MasterPage4.master"; //"FJAAdminMasterPage.master";
        //                    //Response.Redirect("FJAAdmin.aspx"); 
        //                    Response.Redirect("FJAHome.aspx");
        //                }
        //                else if (LoginDetails1.First().UserType.ToString() == "OrgAdmin")
        //                {
        //                    Session["AdminOrganizationID"] = LoginDetails1.First().OrganizationID.ToString();
        //                    Session["SubCtrl"] = "ReportSel_OrgAdmin.ascx"; Response.Redirect("FJAHome.aspx");
        //                    return;
        //                }
        //                else if (LoginDetails1.First().UserType.ToString() == "GrpAdmin")
        //                {
        //                    Session["AdminOrganizationID"] = LoginDetails1.First().OrganizationID.ToString();
        //                    Session["AdminGroupID"] = LoginDetails1.First().GrpUserID.ToString();
        //                    Session["SubCtrl"] = "ReportSel_GroupAdmin.ascx"; Response.Redirect("FJAHome.aspx");

        //                    return;
        //                }
        //                else
        //                {
        //                    int testid = 0;
        //                    if (LoginDetails1.First().TestId != null && LoginDetails1.First().TestId != 0)
        //                    {
        //                        Session["UserTestId"] = int.Parse(LoginDetails1.First().TestId.ToString());
        //                        testid = int.Parse(LoginDetails1.First().TestId.ToString());
        //                    }
        //                    else
        //                    {
        //                        lblMessage.Text = "No Test assigned for you. Please Contact to your admin ";
        //                        return;
        //                    }
        //                    //int passmark = 0;
        //                    //if (testid > 0)
        //                    //{
        //                    //    var getpassmark = from passmarkdet in dataclass.TestLists
        //                    //                      where passmarkdet.TestId == testid
        //                    //                      select passmarkdet;
        //                    //    if (getpassmark.Count() > 0)
        //                    //        if (getpassmark.First().PassMark != null)
        //                    //            passmark = int.Parse(getpassmark.First().PassMark.ToString());                               
        //                    //}

        //                    //if (passmark == 0) { lblMessage.Text = "Passmark for your Test is not defined;"; return; }
        //                    int orgid = 0;
        //                    if (LoginDetails1.First().OrganizationID != null && LoginDetails1.First().OrganizationID != 0)
        //                        orgid = int.Parse(LoginDetails1.First().OrganizationID.ToString());

        //                    if (orgid > 0)
        //                    {
        //                        var OrgLogoName = from logofile in dataclass.Organizations
        //                                          where logofile.OrganizationID == orgid
        //                                          select logofile;
        //                        if (OrgLogoName.Count() > 0)
        //                        {
        //                            if (OrgLogoName.First().LogoFileName != null && OrgLogoName.First().LogoFileName != "")
        //                                Session["LogoFileName"] = OrgLogoName.First().LogoFileName.ToString();
        //                        }
        //                    }


        //                    string usercode = "";
        //                    string curcontrol = "";
        //                    int Evalstatid = 0;
        //                    //var EvaluationDetails = from EvalDet in dataclass.EvaluationStatus
        //                    //                        where EvalDet.EvalCompletionStatus == 0 && EvalDet.UserId == userid
        //                    //                        select EvalDet;
        //                    var EvaluationDetails = from EvalDet in dataclass.EvaluationStatus
        //                                            where EvalDet.UserId == userid
        //                                            select EvalDet;
        //                    if (EvaluationDetails.Count() > 0)
        //                    {
        //                        if (EvaluationDetails.First().EvalCompletionStatus != null)
        //                            if (EvaluationDetails.First().EvalCompletionStatus.ToString() == "1")
        //                            {
        //                                if (Session["CurUserReportAccess"] != null)
        //                                    if (Session["CurUserReportAccess"].ToString() == "1")
        //                                    { Session["SubCtrl"] = "ThankYou.ascx"; Response.Redirect("FJAHome.aspx"); return; }
                                                
        //                                        lblMessage.Text = "You have already completed the test. For more details please contact  site admin"; return;
        //                            }
        //                            else
        //                            {
        //                                DeleteIncompleteTestDetails(userid, testid);
        //                                //dataclass.DeleteEvaluationResult(userid); 
        //                            }

        //                        //{
        //                        //    if (LoginDetails1.First().ReportAccess.ToString() == "1") curcontrol = "UserReportViewControl.ascx";
        //                        //    else { lblMessage.Text = "You cant Login now. Please contact  site admin"; return; }
        //                        //}//lblMessage.Text = "You cant Login now. Contact  site admin"; return;
        //                        //else
        //                        //{

        //                        if (EvaluationDetails.First().EvalStatusId != null)
        //                            Session["EvalStatId"] = EvaluationDetails.First().EvalStatusId.ToString();
        //                        Evalstatid = int.Parse(Session["EvalStatId"].ToString());
        //                        Session["UserCode"] = EvaluationDetails.First().UserCode.ToString();
        //                        //Session["UserID"] = EvaluationDetails.First().UserId.ToString();
        //                        curcontrol = EvaluationDetails.First().EvalControl.ToString();
        //                        //}
        //                    }

        //                    /*
        //                    string passmark = "0";
        //                    var getpassmark = from passmarkdet in dataclass.TestPassmarks
        //                                      select passmarkdet;
        //                    if (getpassmark.Count() > 0)
        //                        if (getpassmark.First().Passmark != null)
        //                            passmark = getpassmark.First().Passmark.ToString();
        //                    if (passmark == "0" || passmark == "0.00")
        //                    { lblMessage.Text = "You cant access evalution part now. For more details please contact  site admin"; return; }
                            
        //                    */

        //                    if (Session["UserCode"] == null)
        //                    {
        //                        Session["UserCode"] = "UID_" + Session["UserID"].ToString() + "_" + DateTime.Now;
        //                        usercode = Session["UserCode"].ToString();
        //                    }
        //                    if (curcontrol == "")
        //                    {
        //                        curcontrol = "WelcomePage.ascx";// "UserProfileControl.ascx";
        //                    }
        //                    //dataclass.ProcedureEvaluationStatus(Evalstatid, curcontrol, 0, 0, usercode, userid);
        //                    Session["SubCtrl"] = curcontrol;//"TaskEntry.ascx";//   "UserProfileControl.ascx";
        //                    //Session["MasterCtrl"] = "~/MasterPage2.master";
        //                    Response.Redirect("FJAHome.aspx");
        //                    //Session["MasterCtrl"] = "~/MasterPage2.master";
        //                    //Response.Redirect("Test.aspx");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            lblMessage.Text = "Incorrect UserName/Password! ";
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Eneter UserName/Password";
        //        return;
        //    }
        ////}
        ////catch (Exception ex) { }
    }

    private void DeleteIncompleteTestDetails(int userid, int testid)
    {
       
        //string condition = "";
        //var getTestSectiondetails = from testsecdet in dataclass.UserTestSectionDetails
        //                            where testsecdet.UserId == userid && testsecdet.TestId == testid
        //                            select testsecdet;
        //if (getTestSectiondetails.Count() > 0)
        //{
        //    foreach (var testsecids in getTestSectiondetails)
        //    {
        //        if (condition != "")
        //            condition += " and ";

        //        condition += " TestSectionId <> " + testsecids.TestSectionId;
        //    }


        //}
        //if (condition != "")
        //{
        //    string querystring = "delete from EvaluationResult where UserId=" + userid + " and TestId=" + testid + " and " + condition;
            
        //    clsClass.ExcecuteDB(querystring);
        //}
        //else
        //{
        //    string querystring = "delete from EvaluationResult where UserId=" + userid;
        //    clsClass.ExcecuteDB(querystring);
        //}
    }
    //protected void txtUsername_TextChanged(object sender, EventArgs e)
    //{
    //   // txtPassword.Focus();
    //}
    //protected void txtPassword_TextChanged(object sender, EventArgs e)
    //{
    //  //  imgBtnLogin.Focus();
    //}
    protected void lnkbtn_codeinformatics_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "CodeInformatics.ascx";
        Response.Redirect("FJAHome.aspx");
        // AddControl("CodeInformatics.ascx");
    }
    //private void AddControl(string ControlPath)
    //{
    //    try
    //    {
    //        System.Web.UI.Control Control_ToAdd;
    //        Control_ToAdd = LoadControl(ControlPath);
    //        Control_ToAdd.ID = "fja";
    //        //cplhLoader.Controls.Clear();
    //        //cplhLoader.Controls.Add(Control_ToAdd);
    //        //LastLoadedControl = ControlPath;
    //    }
    //    catch (Exception ex) 
    //    { //lblmessage.Text = ex.Message; 
    //    }//
    //}
    protected void lnkbtn_talentscout_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "TalentScout.ascx";
        Response.Redirect("FJAHome.aspx");

    }
    protected void lnkbtn_hrservice_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "Services_HR.ascx";
        Response.Redirect("FJAHome.aspx");
    }
    protected void lnkbtn_assessmentService_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "Services_Assesment.ascx";
        Response.Redirect("FJAHome.aspx");
    }
    protected void lnkbtn_careermanagement_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "Services_Career.ascx";
        Response.Redirect("FJAHome.aspx");
    }
    protected void lnkbtn_taketest_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "TakeTest.ascx";
        Response.Redirect("FJAHome.aspx");
    }
}
