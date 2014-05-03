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

public partial class ThankYou : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
       // return;//bip for testing with same user multiple times only for testing purpose....
        int userid = 0;       
        string usercode = "";       
        if (Session["UserCode"] != null)
        {
            usercode = Session["UserCode"].ToString();
        }
        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());
        string curcontrol = "ThankYou.ascx";
        int Evalstatid = 0;
       
        if (Session["EvalStatId"] != null)
        {
            Evalstatid = int.Parse(Session["EvalStatId"].ToString());
        }
        dataclass.ProcedureEvaluationStatus(Evalstatid, curcontrol, 1, 0, usercode, userid);
        //// 230110 bip        
        dataclass.Procedure_DeleteUserTest_TempValues(userid, 0, 0);
        ////
        if (Session["CurUserReportAccess"] != null)
            if (Session["CurUserReportAccess"].ToString() == "1")
                btnShowReport.Visible = true;
    }
    
    protected void btnExit_Click(object sender, EventArgs e)
    {
        /*
       // SaveControlName();
        Session["SubCtrl"] = null;
        Response.Redirect("FJAHome.aspx");
        */

        ReDirectToCareerJudge();////bip 15062010

    }
    private void ReDirectToCareerJudge()
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
        Response.Redirect("http://careerjudge.com/FJAHome.aspx?ctrlid=HomePage.ascx");

    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        //SaveControlName();
        Session["SubCtrl"] = "UserReportViewControl.ascx";
        Response.Redirect("FJAHome.aspx");
    }
    private void SaveControlName()
    {
        string usercode = "";
        string curcontrol = "";
        int Evalstatid = 0;
        int userid = 0;
        if (Session["EvalStatId"] != null)
            Evalstatid = int.Parse(Session["EvalStatId"].ToString());

        userid = int.Parse(Session["UserID"].ToString());
        usercode = Session["UserCode"].ToString();
        curcontrol = "UserReportViewControl.ascx";
        //FJAdataclassses.ProcedureEvaluationStatus(Evalstatid, curcontrol, 1, 1, usercode, userid);

    }
    protected void btnShowReport_Click1(object sender, EventArgs e)
    {
        Session["UserId_Report"] = Session["UserID"];
        Session["UserTestID_Report"] = Session["UserTestId"];      
        GetReportType(int.Parse(Session["UserTestId"].ToString()));

        Session["SubCtrl"] = "ReportPreviewCtrl.ascx";
        Response.Redirect("FJAHome.aspx");
    }
    private void GetReportType(int testidreport)
    {
        string reporttype = "", scoringType = "";
        var ReportTextDetails = from reportdet in dataclass.ReportDescriptions
                                where reportdet.TestId == testidreport
                                select reportdet;
        if (ReportTextDetails.Count() > 0)
        {
            if (ReportTextDetails.First().ReportType != null)
                reporttype = ReportTextDetails.First().ReportType.ToString();
            if (ReportTextDetails.First().ScoringType != null)
                scoringType = ReportTextDetails.First().ScoringType.ToString();
        }

        Session["ReportType"] = reporttype;
        Session["ScoringType"] = scoringType;
    }

}
