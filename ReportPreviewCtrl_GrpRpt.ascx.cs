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

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
//using System.Data.SqlClient;
using System.IO;
using System.ComponentModel;
public partial class ReportPreviewCtrl_GrpRpt : System.Web.UI.UserControl
{
    DBManagementClass clsClasses = new DBManagementClass();
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    DataTable dt = new DataTable();
    int userid = 0; int testid = 0; int groupid = 0;
    DataTable dtEmptySessionList = new DataTable();
    string scoringType; string scoretype;
    int totalQuestionMark = 0;

    DataTable dtTestSection;
    Rectangle objRectangle_3 = new Rectangle(250, 200, 50, 25);
    int mark1 = 40, mark2 = 60, mark3 = 80;
    protected void Page_Load(object sender, EventArgs e)
    {
        //userid = int.Parse(Session["UserId_Report"].ToString());
        if (Session["UserGroupID_Report"] != null)// bipson 07-03-2011
            groupid = int.Parse(Session["UserGroupID_Report"].ToString());

        testid = int.Parse(Session["UserTestID_Report"].ToString());
        scoretype = Session["ScoringType"].ToString();
        bool valexists = false;
        if (Session["TestName"] != null)
        {
            lblTestName.Text = Session["TestName"].ToString();
            if (Session["ReportTypeName"] != null)
            { lblReportType.Text = Session["ReportTypeName"].ToString(); valexists = true; }
        }
        if (valexists == false)
        {
            var GetTestName = from testnamedet in dataclass.TestLists
                              where testnamedet.TestId == testid
                              select testnamedet;
            if (GetTestName.Count() > 0)
            {
                if (GetTestName.First().TestName != null)
                {
                    lblTestName.Text = GetTestName.First().TestName.ToString();
                    Session["TestName"] = GetTestName.First().TestName.ToString();
                }
                if (GetTestName.First().ReportType != null)
                {
                    lblReportType.Text = GetTestName.First().ReportType.ToString();
                    Session["ReportTypeName"] = GetTestName.First().ReportType.ToString();
                }
            }
        }

        GetReportGraphDetailsFromDB();

        //FillUserReportDetails(); 
        FillReportDescriptionDetails();

        if (Session["usertype"].ToString() == "OrgAdmin" || Session["usertype"].ToString() == "GrpAdmin" || Session["usertype"].ToString() == "SuperAdmin")
        { lbtnBack.Visible = true; lbtnBack0.Visible = true; }

        goToPrintPage();//bip 17062010
    }

    private void goToPrintPage()
    {
        
        Session["testsectionreportvalues"] = dtTestSection;
        DataTable dtvariablevalues = new DataTable();
        DataRow drvariablelist;
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows[0].Cells.Count; i++)
            {
                dtvariablevalues.Columns.Add("column" + (i + 1).ToString());
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                drvariablelist = dtvariablevalues.NewRow();
                for (int j = 0; j < GridView1.Rows[i].Cells.Count; j++)
                {
                    if (GridView1.Rows[i].Cells[j].Text != "&nbsp;")
                        drvariablelist["column" + (j + 1).ToString()] = GridView1.Rows[i].Cells[j].Text;
                }
                dtvariablevalues.Rows.Add(drvariablelist);
            }

            Session["variablereportvalues"] = dtvariablevalues;


            /// this part is ment for browser back button
            if (Session["usertype"].ToString() == "OrgAdmin")
                Session["SubCtrl"] = "ReportSel_OrgAdmin.ascx";
            else if (Session["usertype"].ToString() == "GrpAdmin")
                Session["SubCtrl"] = "ReportSel_GroupAdmin.ascx";
            else if (Session["usertype"].ToString() == "SuperAdmin")
                Session["SubCtrl"] = "ReportSel_SuperAdmin.ascx";
            ///

            Response.Redirect("RptPrntGrp.aspx"); return;
        }
        else
        {
            //lblMessage.Text = "No Values for Print";

            if (Session["usertype"].ToString() == "OrgAdmin")
            { Session["SubCtrl"] = "ReportSel_OrgAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }

            else if (Session["usertype"].ToString() == "GrpAdmin")
            { Session["SubCtrl"] = "ReportSel_GroupAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }
            else if (Session["usertype"].ToString() == "SuperAdmin")
            { Session["SubCtrl"] = "ReportSel_SuperAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }
        }
    }

    private void FillReportDescriptionDetails()
    {
        lblReportDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");//.ToShortDateString();

        var SummaryDetails1 = from SummaryDetails in dataclass.ReportDescriptions
                              where SummaryDetails.TestId == testid
                              select SummaryDetails;
        if (SummaryDetails1.Count() > 0)
        {
            if (SummaryDetails1.First().Summary1 != null)
                lblSummary1.Text = SummaryDetails1.First().Summary1.ToString();
            if (SummaryDetails1.First().Summary2 != null)
                lblSummary2.Text = SummaryDetails1.First().Summary2.ToString();
            if (SummaryDetails1.First().DescriptiveReport != null)
                tcellDescriptionReport.InnerHtml = SummaryDetails1.First().DescriptiveReport.ToString();
            if (SummaryDetails1.First().Conclusion != null)
                lblConclusion.Text = SummaryDetails1.First().Conclusion.ToString();
            if (SummaryDetails1.First().ScoringType != null)
                scoringType = SummaryDetails1.First().ScoringType.ToString();
        }

    }

   

    private void GetReportGraphDetailsFromDB()
    {
        try
        {
            dtTestSection = new DataTable();
            dtTestSection.Columns.Add("TestSectionID");
            dtTestSection.Columns.Add("TotalMark_sec");
            dtTestSection.Columns.Add("USERID");// BIP 19-01-2011
            DataRow drTestSection;

            DataRow dr;
            dt.Columns.Add("SectionID");
            dt.Columns.Add("SectionName");
            dt.Columns.Add("TestSectionId");
            dt.Columns.Add("TestID");
            dt.Columns.Add("TotalMarks");
            dt.Columns.Add("BandDescription");
            dt.Columns.Add("USERID");// BIP 13-01-2011

            DataRow drEmptySession;
            dtEmptySessionList.Columns.Add("SectionName");
            dtEmptySessionList.Columns.Add("BandDescription");
            dtEmptySessionList.Columns.Add("TestSectionId");
            dtEmptySessionList.Columns.Add("USERID");// BIP 19-01-2011
           
            int MemmoryTestImage = 0;
            int MemmoryTestText = 0;
            int QuesCollection = 0;
            string sectionname = ""; int TestSecionID = 0;
            
            DataSet ds;
            Table tblDisplay = new Table();
            TableCell tblCell = new TableCell();
            TableRow tblRow;
            Label label;
            //int i = 0;
            int rowid = 0;
            int totalmarks = 0;
            int sectionid = 0;
            ////int testid = int.Parse(Session["UserTestID_Report"].ToString());


            //bip 13-01-2011 
            // code to get all user under selected Test

            ////string querystringuserlist = "SELECT USERID FROM USERPROFILE WHERE TESTID=" + testid;
            //var userlist = from userListOfTest in dataclass.UserProfiles
            //               where userListOfTest.TestId == testid
            //               select new { userListOfTest.UserId };
            //if(userlist.Count()>0)
            //    foreach (var userids_test in userlist)
            //    {
            //        userid = userids_test.UserId;

                    string querystringuserlist = "SELECT USERID FROM USERPROFILE WHERE TESTID=" + testid;
                    if (groupid > 0)
                        querystringuserlist += " and GrpUserID=" + groupid;
                    //if (Session["AdminGroupID"] != null)
                    //{
                    //    int grpid = int.Parse(Session["AdminGroupID"].ToString());
                    //    querystringuserlist += " and GrpUserID=" + grpid;
                    //}

                    DataSet dsUserdetails = new DataSet();
                    dsUserdetails = clsClasses.GetValuesFromDB(querystringuserlist);
                    if (dsUserdetails != null)
                        if (dsUserdetails.Tables.Count > 0)
                            if (dsUserdetails.Tables[0].Rows.Count > 0)
                                for (int u = 0; u < dsUserdetails.Tables[0].Rows.Count; u++)
                                {
                                    userid = int.Parse(dsUserdetails.Tables[0].Rows[u]["USERID"].ToString());

                                    //}


                                    //


                                    string quesrystring = "SELECT DISTINCT EvaluationResult.QuestionID, TestBaseQuestionList.TestId, TestBaseQuestionList.TestSectionId,TestBaseQuestionList.Status, EvaluationResult.Question, " +
                                              " EvaluationResult.Answer, EvaluationResult.UserId, EvaluationResult.Category, TestBaseQuestionList.SectionId, " +
                                              " TestBaseQuestionList.FirstVariableId, TestBaseQuestionList.SecondVariableId, TestBaseQuestionList.ThirdVariableId FROM  EvaluationResult INNER JOIN TestBaseQuestionList ON EvaluationResult.QuestionID = TestBaseQuestionList.QuestionId " +
                                              " WHERE     (TestBaseQuestionList.Status = 1) and EvaluationResult.UserId=" + userid + " and TestBaseQuestionList.TestId=" + testid + " order by EvaluationResult.UserId,TestBaseQuestionList.TestSectionId ";// BIP 19-01-2011

                                    DataSet dsEvaluationdetails = new DataSet();
                                    dsEvaluationdetails = clsClasses.GetValuesFromDB(quesrystring);
                                    if (dsEvaluationdetails != null)
                                        if (dsEvaluationdetails.Tables.Count > 0)
                                            if (dsEvaluationdetails.Tables[0].Rows.Count > 0)
                                            {


                                                for (int i = 0; i < dsEvaluationdetails.Tables[0].Rows.Count; i++)
                                                {
                                                    int marks = 0;
                                                    int currentmarks = 0;
                                                    sectionid = 0;
                                                    //if (UserAnswers.Category == "MemTestWords")
                                                    if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "MemTestWords")
                                                    {
                                                        var MemWords1 = from MemWords in dataclass.MemmoryTestTextQuesCollections
                                                                        where MemWords.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && MemWords.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString()) //UserAnswers.Question && MemWords.QuestionID == UserAnswers.QuestionID
                                                                        select MemWords;
                                                        if (MemWords1.Count() > 0)
                                                        {
                                                            if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                                                TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                                            sectionname = MemWords1.First().SectionName.ToString();
                                                            sectionid = int.Parse(MemWords1.First().SectionId.ToString());
                                                            if (sectionid > 0)// bip 05122009
                                                                if (MemWords1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                                                    marks = 1;
                                                        }
                                                    }
                                                    else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "MemTestImages")
                                                    {
                                                        var MemImages1 = from MemImages in dataclass.MemmoryTestImageQuesCollections
                                                                         where MemImages.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && MemImages.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && MemImages.QuestionID == UserAnswers.QuestionID
                                                                         select MemImages;
                                                        if (MemImages1.Count() > 0)
                                                        {
                                                            if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                                                TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                                            sectionname = MemImages1.First().SectionName.ToString();
                                                            sectionid = int.Parse(MemImages1.First().SectionId.ToString());
                                                            if (sectionid > 0)// bip 05122009
                                                                if (MemImages1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                                                    marks = 1;
                                                        }
                                                    }
                                                    else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "FillBlanks")
                                                    {
                                                        var FillQues1 = from FillQues in dataclass.QuestionCollections
                                                                        where FillQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && FillQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                                                        select FillQues;
                                                        if (FillQues1.Count() > 0)
                                                        {
                                                            if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                                                TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                                            sectionname = FillQues1.First().SectionName.ToString();
                                                            sectionid = int.Parse(FillQues1.First().SectionId.ToString());


                                                            if (sectionid > 0)// bip 05122009
                                                                if (FillQues1.First().Option1 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() || FillQues1.First().Option2 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() ||
                                                                     FillQues1.First().Option3 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() || FillQues1.First().Option4 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() ||
                                                                     FillQues1.First().Option5 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())
                                                                    marks = 1;
                                                        }
                                                    }
                                                    else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "RatingType")
                                                    {
                                                        var FillQues1 = from FillQues in dataclass.QuestionCollections
                                                                        where FillQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && FillQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                                                        select FillQues;
                                                        if (FillQues1.Count() > 0)
                                                        {
                                                            if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                                                TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                                            sectionname = FillQues1.First().SectionName.ToString();
                                                            sectionid = int.Parse(FillQues1.First().SectionId.ToString());

                                                            //if (sectionid > 0)// bip 05122009
                                                               // marks = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString());// int.Parse(UserAnswers.Answer.ToString());
                                                            if (sectionid > 0)// bip 05062011 "-" is added if the user leave the anwser while taking the test
                                                                if (dsEvaluationdetails.Tables[0].Rows[i]["Answer"] != null)
                                                                    if (dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() != "-")
                                                                        marks = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString());

                                                        }
                                                    }
                                                    else
                                                    {
                                                        var OtherQues1 = from OtherQues in dataclass.QuestionCollections
                                                                         where OtherQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && OtherQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && OtherQues.QuestionID == UserAnswers.QuestionID
                                                                         select OtherQues;
                                                        if (OtherQues1.Count() > 0)
                                                        {
                                                            if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                                                TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString()); //int.Parse(UserAnswers.TestSectionId.ToString());
                                                            sectionname = OtherQues1.First().SectionName.ToString();
                                                            sectionid = int.Parse(OtherQues1.First().SectionId.ToString());
                                                            if (sectionid > 0)// bip 05122009
                                                                if (OtherQues1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                                                    marks = 1;
                                                        }
                                                    }
                                                    //if (sectionname == "Clarity of Communication")

                                                    //BIP 23-01-2011
                                                    //if (marks >= 0)
                                                    //{
                                                    //BIP 23-01-2011

                                                    bool testsecExists = false; int j = 0;
                                                    if (dtTestSection.Rows.Count > 0)
                                                    {
                                                        for (int n = 0; n < dtTestSection.Rows.Count; n++)
                                                        {
                                                            if (dtTestSection.Rows[n]["TestSectionID"].ToString() == TestSecionID.ToString() && dtTestSection.Rows[n]["USERID"].ToString() == userid.ToString())// BIP 19-01-2011
                                                            { testsecExists = true; j = n; break; }
                                                        }
                                                    }
                                                    if (testsecExists == true)
                                                    {
                                                        int currentmarks_testsecid = 0;
                                                        drTestSection = dtTestSection.Rows[j];
                                                        currentmarks_testsecid = int.Parse(drTestSection["TotalMark_sec"].ToString());
                                                        currentmarks_testsecid = currentmarks_testsecid + marks;
                                                        drTestSection["TotalMark_sec"] = currentmarks_testsecid.ToString();
                                                    }
                                                    else
                                                    {
                                                        drTestSection = dtTestSection.NewRow();
                                                        drTestSection["TestSectionID"] = TestSecionID.ToString();
                                                        drTestSection["TotalMark_sec"] = marks.ToString();
                                                        drTestSection["USERID"] = userid;// BIP 19-01-2011
                                                        dtTestSection.Rows.Add(drTestSection);
                                                    }

                                                    Boolean exists = CheckExistence(sectionname, TestSecionID, userid);
                                                    if (exists == true)
                                                    {
                                                        rowid = int.Parse(Session["RowID"].ToString());
                                                        dr = dt.Rows[rowid];
                                                        currentmarks = int.Parse(dr["TotalMarks"].ToString());
                                                        currentmarks = currentmarks + marks;
                                                        dr["TotalMarks"] = currentmarks;
                                                        marks = 0; Session["RowID"] = null;
                                                        // lblMessage.Text += " || " + sectionname + " totmarknew= " + currentmarks + " || ";
                                                    }
                                                    else
                                                    {
                                                        //if (currentmarks > 0)
                                                        //    marks = currentmarks;

                                                        dr = dt.NewRow();
                                                        dr["SectionID"] = sectionid;//BIP 13-02-2011
                                                        dr["SectionName"] = sectionname;
                                                        dr["TestID"] = testid;
                                                        dr["TestSectionId"] = TestSecionID;
                                                        if (exists == false)
                                                            dr["TotalMarks"] = marks;
                                                        else
                                                            dr["TotalMarks"] = "0";
                                                        dr["BandDescription"] = "";

                                                        dr["USERID"] = userid;// BIP 13-01-2011

                                                        dt.Rows.Add(dr);
                                                        marks = 0;

                                                        // lblMessage.Text += " sec..." + sectionname + " ";
                                                    }

                                                    //BIP 23-01-2011
                                                    //}

                                                    //else
                                                    //{

                                                    //    bool sesExists = false;
                                                    //    if (dtEmptySessionList.Rows.Count > 0)
                                                    //    {
                                                    //        for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                                                    //        {
                                                    //            if (dtEmptySessionList.Rows[e][0].ToString() == sectionname && dtEmptySessionList.Rows[e][2].ToString() == TestSecionID.ToString() && dtEmptySessionList.Rows[e]["USERID"].ToString() == userid.ToString())// BIP 19-01-2011
                                                    //                sesExists = true;
                                                    //        }
                                                    //    }
                                                    //    if (sesExists == false)
                                                    //    {
                                                    //        drEmptySession = dtEmptySessionList.NewRow();
                                                    //        drEmptySession["SectionName"] = sectionname;

                                                    //        string remarks = GetBandDescription(0, sectionname);
                                                    //        drEmptySession["BandDescription"] = remarks;
                                                    //        drEmptySession["TestSectionId"] = TestSecionID;
                                                    //        drEmptySession["USERID"] = userid;// BIP 19-01-2011
                                                    //        dtEmptySessionList.Rows.Add(drEmptySession);
                                                    //    }
                                                    //    if (dtEmptySessionList.Rows.Count > 0)
                                                    //    {
                                                    //        for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                                                    //        {
                                                    //            Boolean exists = CheckExistence(dtEmptySessionList.Rows[e][0].ToString(), TestSecionID);
                                                    //            if (exists == true)
                                                    //            {
                                                    //                dtEmptySessionList.Rows[e].Delete();
                                                    //            }
                                                    //        }
                                                    //    }
                                                    //}//
                                                    // BIP 23-01-2011
                                                }
                                            }

                                }

            GridView1.DataSource = dt;
            GridView1.DataBind(); //return;// bip 19122009
            // lblMessage.Text = scoretype; return;
            //if (Session["DiagramType"].ToString() == "PieGraph")
            //{

            ////
            //GridView1.Sort("USERID", SortDirection.Ascending);// BIP 19-01-2011
            //GridView1.DataBind();

            ////

            if (GridView1.Rows.Count > 0)
            {

                if (scoretype == "Percentage")
                {

                    for (int k = 0; k < GridView1.Rows.Count; k++)
                    {
                        int curmark = int.Parse(GridView1.Rows[k].Cells[4].Text);
                        string remarks = GetBandDescription(curmark, GridView1.Rows[k].Cells[1].Text);
                        GridView1.Rows[k].Cells[5].Text = remarks;

                        if (GridView1.Rows[k].Cells[4].Text != "0" && GridView1.Rows[k].Cells[4].Text != "&nbsp;")
                        {
                            float mark1 = float.Parse(GridView1.Rows[k].Cells[4].Text);
                            //mark1 = ((mark1 * 100) / 360);
                            float totalMarksectionwise = 0, currentmark = 0;
                            totalMarksectionwise = GetSectionwiseTotalQuestionMarks(GridView1.Rows[k].Cells[1].Text, GridView1.Rows[k].Cells[2].Text, 0);
                            if (totalMarksectionwise > 0)
                            {
                                
                                currentmark = (mark1 / totalMarksectionwise) * 100;
                                // lblMessage.Text += " mark=" + mark1.ToString() + "curMark &,& " + currentmark.ToString();
                                currentmark = float.Parse(currentmark.ToString("0.00"));
                                GridView1.Rows[k].Cells[4].Text = currentmark.ToString();

                            }
                        }
                    }
                    //if (Session["ReportType"].ToString() == "TestSectionwise")
                    //{
                    //    //testsection mark details

                    //    for (int k = 0; k < dtTestSection.Rows.Count; k++)
                    //    {
                    //        int curmark = int.Parse(dtTestSection.Rows[k]["TotalMark_sec"].ToString());
                    //        int testsecid = int.Parse(dtTestSection.Rows[k]["TestSectionID"].ToString());

                    //        float mark1 = float.Parse(curmark.ToString());
                    //        //mark1 = ((mark1 * 100) / 360);
                    //        float totMarktestsecwise = 0, currentmark = 0;
                    //        totMarktestsecwise = GetSectionwiseTotalQuestionMarks("", testsecid.ToString(), 1);

                    //        if (totMarktestsecwise > 0)
                    //        {
                                
                    //            currentmark = (mark1 / totMarktestsecwise) * 100;
                                
                    //            currentmark = float.Parse(currentmark.ToString("0.00"));
                                
                    //        }
                    //        else
                    //        {
                    //            currentmark = 0;
                    //        }
                    //        drTestSection = dtTestSection.Rows[k];
                    //        drTestSection["TotalMark_sec"] = currentmark.ToString();
                    //    }

                    //    //for (int k = 0; k < dtTestSection.Rows.Count; k++)
                    //    //{
                    //    //    if (dtTestSection.Rows[k][1].ToString() != "" && dtTestSection.Rows[k][1].ToString() != "Infinity")
                    //    //    {
                    //    //        float curmark = float.Parse(dtTestSection.Rows[k][1].ToString());
                    //    //        if (txtValues.Text.Trim() != "")
                    //    //            txtValues.Text = txtValues.Text.Trim() + ",";
                    //    //        txtValues.Text = txtValues.Text.Trim() + curmark.ToString();//currentmark;// mark1;//
                    //    //        txtParts.Text = (k + 1).ToString();
                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        if (txtValues.Text.Trim() != "")
                    //    //            txtValues.Text = txtValues.Text.Trim() + ",";
                    //    //        txtValues.Text = txtValues.Text.Trim() + "0";//currentmark;// mark1;//
                    //    //        txtParts.Text = (k + 1).ToString();
                    //    //    }
                    //    //}

                    //    //DrawPieGraph_TestSecwise("imgReportGraph1_" + userid + "_" + DateTime.Now.Millisecond.ToString() + ".jpg");//BIP 19-01-2011
                    //    DisplayGroupReportColorGraph();
                    //    return;
                    //}
                }
                else
                {
                    //lblMessage.Text += "hi..."; return;
                    int count = 0;
                    // lblMessage.Text += " rptgridParts= " + GridView1.Rows.Count.ToString() + " marks= ";
                    for (int k = 0; k < GridView1.Rows.Count; k++)
                    {
                        int curmark = int.Parse(GridView1.Rows[k].Cells[4].Text);
                        string remarks = GetBandDescription(curmark, GridView1.Rows[k].Cells[1].Text);
                        GridView1.Rows[k].Cells[5].Text = remarks;

                        // lblMessage.Text += " , " + curmark.ToString();
                        // lblMessage.Text +=" sectionname= " + GridView1.Rows[k].Cells[1].Text + " , curmark= " + curmark.ToString();
                        if (GridView1.Rows[k].Cells[4].Text != "0" && GridView1.Rows[k].Cells[4].Text != "&nbsp;")
                        {
                            float mark1 = float.Parse(GridView1.Rows[k].Cells[4].Text);
                            ////mark1 = ((mark1 * 100) / 360);
                            float currentmark = 0;//,totalMarksectionwise = 0 ;
                            
                            currentmark = GetPercentileScoreUserwise(mark1, GridView1.Rows[k].Cells[1].Text, GridView1.Rows[k].Cells[2].Text);
                            currentmark = float.Parse(currentmark.ToString("0.00"));
                            GridView1.Rows[k].Cells[4].Text = currentmark.ToString();
                            count++;
                            
                        }

                    }

                    ////lblMessage.Text += "hi.. hi..."; return;
                    //if (Session["ReportType"].ToString() == "TestSectionwise")
                    //{
                    //    //testsection mark details

                    //    for (int k = 0; k < dtTestSection.Rows.Count; k++)
                    //    {
                    //        int curmark = 0;
                    //        if (dtTestSection.Rows[k][1].ToString() != "" && dtTestSection.Rows[k][1].ToString() != "Infinity")
                    //            curmark = int.Parse(dtTestSection.Rows[k]["TotalMark_sec"].ToString());

                    //        int testsecid = int.Parse(dtTestSection.Rows[k]["TestSectionID"].ToString());
                    //        if (curmark > 0)
                    //        {
                    //            float mark1 = float.Parse(curmark.ToString());
                    //            //mark1 = ((mark1 * 100) / 360);
                    //            float currentmark = 0;//totMarktestsecwise = 0, 

                    //            currentmark = GetPercentileScoreUserwise_testsec(mark1, testsecid);
                    //            currentmark = float.Parse(currentmark.ToString("0.00"));
                    //            drTestSection = dtTestSection.Rows[k];
                    //            drTestSection["TotalMark_sec"] = currentmark.ToString();
                    //        }
                    //    }

                    //    //for (int k = 0; k < dtTestSection.Rows.Count; k++)
                    //    //{
                    //    //    if (dtTestSection.Rows[k][1].ToString() != "" && dtTestSection.Rows[k][1].ToString() != "Infinity")
                    //    //    {
                    //    //        float curmark = float.Parse(dtTestSection.Rows[k][1].ToString());
                    //    //        if (txtValues.Text.Trim() != "")
                    //    //            txtValues.Text = txtValues.Text.Trim() + ",";
                    //    //        txtValues.Text = txtValues.Text.Trim() + curmark.ToString();//currentmark;// mark1;//
                    //    //        txtParts.Text = (k + 1).ToString();
                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        if (txtValues.Text.Trim() != "")
                    //    //            txtValues.Text = txtValues.Text.Trim() + ",";
                    //    //        txtValues.Text = txtValues.Text.Trim() + "0";
                    //    //        txtParts.Text = (k + 1).ToString();
                    //    //    }
                    //    //}

                    //    //DrawPieGraph_TestSecwise("imgReportGraph1_" + userid + "_" + DateTime.Now.Millisecond.ToString() + ".jpg");// BIP 19-01-2011
                    //    DisplayGroupReportColorGraph();
                    //    return;
                    //}
                }
                
                //int totvalues = 0; //lblMessage.Text += " marks in grid = ";
                //for (int j = 0; j < GridView1.Rows.Count; j++)
                //{
                //    //lblMessage.Text += " , "+ GridView1.Rows[j].Cells[4].Text;
                //    if (GridView1.Rows[j].Cells[1].Text != "&nbsp;")
                //        //if (GridView1.Rows[j].Cells[4].Text != "0" && GridView1.Rows[j].Cells[4].Text != "&nbsp;")
                //        if (GridView1.Rows[j].Cells[4].Text != "&nbsp;")
                //        {
                //            totvalues++;
                //            if (txtValues.Text.Trim() != "")
                //                txtValues.Text = txtValues.Text.Trim() + ",";
                //            txtValues.Text = txtValues.Text.Trim() + GridView1.Rows[j].Cells[4].Text;//currentmark;// mark1;//
                //            txtParts.Text = totvalues.ToString();
                //        }
                //} //lblMessage.Text += " totvaluesingrid= " + totvalues + " *** ";
            }
            // lblMessage.Text += " !!!!! "+ txtParts.Text + " && " + txtValues.Text; return;

            //DrawPieGraph("imgReportGraph1_" + userid + "_" + DateTime.Now.Millisecond.ToString() + ".jpg", 1);// BIP 19-01-2011
            DisplayGroupReportColorGraph();
        }
        catch (Exception ex) { lblMessage.Text += "hi..." + ex.Message; }

    }

   

    private void DisplayGroupReportColorGraph()
    {
        goToPrintPage();//bipson 17-02-2011
        return;

        Table tblDisplay = new Table();
        TableCell tblCell = new TableCell();
        TableRow tblRow;
        Label label;
        int i = 0;
        int rowid = 0;
        int totalmarks = 0;
        int sectionid = 0;
        // code to draw section(variable)wise bargraph 

        //tblDisplay.Width = 650;
        tblDisplay.CellPadding = 0;
        tblDisplay.CellSpacing = 0;
        tblDisplay.BorderWidth = 0;
        string mark = "0"; string benchmark = "";

        // lblMessage.Text += " bargrphParts= " + GridView1.Rows.Count.ToString() + " bargrphValues= ";

        //
        float totalmark_variablewise = 0;
        int GRADE = 0;
        int gradecount = 0;

        //GridView1.Sort("USERID", SortDirection.Ascending);// BIP 19-01-2011
        //GridView1.DataBind();


        //code to get all section(variable) list
        //sectionNAME=GridView1.Rows[j].Cells[1].Text
        DataTable dtSectionList = new DataTable();
        dtSectionList.Columns.Add("sectionid");
        dtSectionList.Columns.Add("sectionname");
        DataRow drSectionList;
        string secname = "", secid = "";
        for (int secIndex = 0; secIndex < GridView1.Rows.Count; secIndex++)
        {
            secid = GridView1.Rows[secIndex].Cells[0].Text;
            secname = GridView1.Rows[secIndex].Cells[1].Text;

            bool secexists = false;
            for (int secIndex1 = 0; secIndex1 < dtSectionList.Rows.Count; secIndex1++)
            {
                if (dtSectionList.Rows[secIndex1]["sectionname"].ToString() == secname)
                { secexists = true; break; }
            }
            if (secexists == false)
            {
                drSectionList = dtSectionList.NewRow();
                drSectionList["sectionid"] = secid;
                drSectionList["sectionname"] = secname;
                dtSectionList.Rows.Add(drSectionList);
            }
        }

        //code add first three headers(USERID,NAME.TESTDATE)

        tblRow = new TableRow();
        tblCell = new TableCell();
        label = new Label();
        label.Font.Size = 12;
        label.Font.Bold = true;
        label.Text = "USER ID";
        //label.Width = 30;
        tblCell.Controls.Add(label);
        tblCell.Style.Value = "border: 1px ridge #000000;text-align: center; vertical-align: middle";
        //tblCell.Text = "USER ID";
        tblRow.Cells.Add(tblCell);
        tblCell = new TableCell();
        label = new Label();
        label.Font.Size = 12;
        label.Font.Bold = true;
        label.Text = "NAME";
        //label.Width = 30;
        tblCell.Controls.Add(label);
        tblCell.Style.Value = "border: 1px ridge #000000;text-align: center; vertical-align: middle";
        tblRow.Cells.Add(tblCell);
        tblCell = new TableCell();
        label = new Label();
        label.Font.Size = 12;
        label.Font.Bold = true;
        label.Text = "TEST DATE";
        //label.Width = 30;
        tblCell.Controls.Add(label);
        tblCell.Style.Value = "border: 1px ridge #000000;text-align: center; vertical-align: middle;";//padding:10
        tblRow.Cells.Add(tblCell);

        //code to design table header(title and variable names on the top of the table cell)
        for (int titleIndex = 0; titleIndex < dtSectionList.Rows.Count; titleIndex++)
        {
            tblCell = new TableCell();
            
            label = new Label();
            label.Font.Size = 12;
            label.Font.Bold = true;
            label.Text = "V" + (titleIndex + 1).ToString();
            //label.Text = dtSectionList.Rows[titleIndex]["sectionname"].ToString();
            //label.Width = 50;
            tblCell.Controls.Add(label);
            tblCell.Style.Value = "border: 1px ridge #000000;text-align: center; vertical-align: middle;width:40px;";//padding:10px
            //tblCell.Text = dtSectionList.Rows[titleIndex]["sectionname"].ToString();
            tblRow.Cells.Add(tblCell);
        }

        tblDisplay.Rows.Add(tblRow);// bip 13-02-2011
        //

        string curUSERNAME = "";
        string curNAME = "";
        string curUSERID = "0";
        string testDATE = "";
        string querystring = "SELECT DISTINCT EvaluationResult.UserId, UserProfile.UserName, UserProfile.FirstName, UserProfile.MiddleName, UserProfile.LastName,UserProfile.FirstLoginDate " +
                                    " FROM EvaluationResult INNER JOIN UserProfile ON EvaluationResult.UserId = UserProfile.UserId ORDER BY UserProfile.UserName";

        DataSet dsEvaluationdetails = new DataSet();
        dsEvaluationdetails = clsClasses.GetValuesFromDB(querystring);
        if (dsEvaluationdetails != null)
            if (dsEvaluationdetails.Tables.Count > 0)
                if (dsEvaluationdetails.Tables[0].Rows.Count > 0)
                    for (int uindex = 0; uindex < dsEvaluationdetails.Tables[0].Rows.Count; uindex++)
                    {
                        curUSERID = dsEvaluationdetails.Tables[0].Rows[uindex]["UserId"].ToString();
                        curUSERNAME = dsEvaluationdetails.Tables[0].Rows[uindex]["UserName"].ToString();
                        curNAME = dsEvaluationdetails.Tables[0].Rows[uindex]["FirstName"].ToString();
                        if (!dsEvaluationdetails.Tables[0].Rows[uindex]["MiddleName"].Equals(""))
                            curNAME += " " + dsEvaluationdetails.Tables[0].Rows[uindex]["MiddleName"].ToString();
                        if (!dsEvaluationdetails.Tables[0].Rows[uindex]["LastName"].Equals(""))
                            curNAME += " " + dsEvaluationdetails.Tables[0].Rows[uindex]["LastName"].ToString();

                        testDATE = dsEvaluationdetails.Tables[0].Rows[uindex]["FirstLoginDate"].ToString();
                        DateTime dttest = DateTime.Parse(testDATE);//, "dd/MM/yyyy");
                        testDATE = dttest.ToString("dd/MM/yyyy");//.ToShortDateString();
                        // }

                        string cellstyle = "border: 1px ridge #000000;text-align: left; vertical-align: middle;padding-left:5px;padding-right:5px";
                        tblRow = new TableRow();
                        // add username,name(firstname+middlename+lastname),testdate;
                        tblCell = new TableCell();
                        label = new Label();
                        label.Font.Size = 12;
                        //label.Font.Bold = true;
                        label.Text = curUSERNAME;
                        tblCell.Controls.Add(label);
                        tblCell.Style.Value = cellstyle;
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        label = new Label();
                        label.Font.Size = 12;
                        //label.Font.Bold = true;
                        label.Text = curNAME;
                        tblCell.Controls.Add(label);
                        tblCell.Style.Value = cellstyle;
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        label = new Label();
                        label.Font.Size = 12;
                        //label.Font.Bold = true;
                        label.Text = testDATE;
                        tblCell.Controls.Add(label);
                        tblCell.Style.Value = cellstyle;
                        tblRow.Cells.Add(tblCell);
                        //


                        //
                        int currentINDEX = 0;

                        for (int j = 0; j < GridView1.Rows.Count; j++)
                        {
                            totalmark_variablewise = 0;
                            if (GridView1.Rows[j].Cells[6].Text != "&nbsp;")
                                if (GridView1.Rows[j].Cells[6].Text != curUSERID)
                                    continue;
                            currentINDEX++;
                            if (GridView1.Rows[j].Cells[1].Text != "&nbsp;")
                            {
                                string TESTSECTIONID = GridView1.Rows[j].Cells[2].Text;
                                mark = "0";
                                if (GridView1.Rows[j].Cells[4].Text != "0" && GridView1.Rows[j].Cells[4].Text != "&nbsp;")
                                    totalmark_variablewise = float.Parse(GridView1.Rows[j].Cells[4].Text);
                                
                                string remarks = ""; string querystring1 = "";
                                benchmark = "";
                                int section_id = GetSectionId(GridView1.Rows[j].Cells[1].Text);
                                
                                querystring1 = "SELECT TestID, BenchMark,DisplayName,MarkFrom,MarkTo FROM TestVariableResultBands WHERE TestID = " + testid + " AND TestSectionId = " + TESTSECTIONID;
                                querystring1 += " AND VariableId = " + section_id + "";

                                DataSet ds1 = new DataSet(); bool benchmarkexists = false;
                                ds1 = clsClasses.GetValuesFromDB(querystring1);
                                if (ds1 != null)
                                    if (ds1.Tables.Count > 0)
                                        if (ds1.Tables[0].Rows.Count > 0)
                                        {                                            
                                            benchmarkexists = true;
                                            gradecount = ds1.Tables[0].Rows.Count;
                                            for (int g = 0; g < ds1.Tables[0].Rows.Count; g++)
                                            {
                                                if (totalmark_variablewise >= float.Parse(ds1.Tables[0].Rows[g]["MarkFrom"].ToString()) && totalmark_variablewise <= float.Parse(ds1.Tables[0].Rows[g]["MarkTo"].ToString()))
                                                { GRADE = g + 1; break; }

                                            }
                                        }
                                

                                if (benchmarkexists == false)
                                {

                                    querystring1 = "SELECT TestID, BenchMark,DisplayName,MarkFrom,MarkTo FROM TestSectionResultBands WHERE TestID = " + testid;
                                    querystring1 += " AND SectionId = " + TESTSECTIONID;

                                    ds1 = new DataSet();
                                    ds1 = clsClasses.GetValuesFromDB(querystring1);
                                    if (ds1 != null)
                                        if (ds1.Tables.Count > 0)
                                            if (ds1.Tables[0].Rows.Count > 0)
                                            {
                                                gradecount = ds1.Tables[0].Rows.Count;
                                                for (int g = 0; g < ds1.Tables[0].Rows.Count; g++)
                                                {
                                                    if (totalmark_variablewise >= float.Parse(ds1.Tables[0].Rows[g]["MarkFrom"].ToString()) && totalmark_variablewise <= float.Parse(ds1.Tables[0].Rows[g]["MarkTo"].ToString()))
                                                    { GRADE = g + 1; break; }
                                                }
                                            }
                                }

                                //tblCell = new TableCell();
                                //label = new Label();
                                //label.Text = totalmark_variablewise.ToString();
                                //tblCell.Controls.Add(label);
                                //tblCell.Style.Value = "text-align: left; vertical-align: middle";
                                //tblCell.BackColor = Color.Red;
                                HtmlImage himage = new HtmlImage();
                               //tblRow = new TableRow();
                                string imgvalue = "width:40px;height:22px";
                                for (int n = 0; n < gradecount; n++)
                                {
                                    tblCell = new TableCell();
                                    himage = new HtmlImage(); himage.Style.Value = imgvalue;// "width: 100%; height: 100%";
                                    if (gradecount == 2)
                                    {
                                        //tblCell.Text=i+1;
                                        if (GRADE == n + 1)
                                        {
                                            if (GRADE == 1)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imgred.JPG";                                                
                                                tblCell.Controls.Add(himage); break;
                                            }
                                            else if (GRADE == 2)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imggreen.JPG";                                                
                                                tblCell.Controls.Add(himage); break;
                                            }
                                        }
                                    }
                                    else if (gradecount == 3)
                                    {
                                        if (GRADE == n + 1)
                                        {
                                            if (GRADE == 1)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imgred.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }
                                            else if (GRADE == 2)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imgash.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }
                                            else if (GRADE == 3)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imggreen.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }
                                        }
                                    }
                                    else if (gradecount == 4)
                                    {
                                        if (GRADE == n + 1)
                                        {
                                            if (GRADE == 1)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imgred.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }
                                            else if (GRADE == 2)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imgash.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }
                                            else if (GRADE == 3)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imgash.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }
                                            else if (GRADE == 4)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imggreen.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }
                                        }
                                    }
                                    else if (gradecount == 5)
                                    {
                                        if (GRADE == n + 1)
                                        {
                                            if (GRADE == 1)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imgred.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }
                                            else if (GRADE == 2)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imgash.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }
                                            else if (GRADE == 3)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imgash.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }
                                            else if (GRADE == 4)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imgash.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }
                                            else if (GRADE == 5)
                                            {
                                                himage.Src = "~/QuestionAnswerFiles/ReportImages/imggreen.JPG";
                                                tblCell.Controls.Add(himage); break;
                                            }

                                        }
                                    }
                                    
                                }
                                 tblCell.Style.Value = "border: 1px ridge #000000";
                                    tblRow.Cells.Add(tblCell);                              
                            }
                            tblDisplay.Rows.Add(tblRow);
                            //break;
                        }
                        //tblDisplay.Rows.Add(tblRow);

                    }// end of for loop userdetails...
        
        //BIP 23-01-2011
        
        // tcellBarGraph.Width = "200";
        tcellBarGraph.Controls.Add(tblDisplay);
        // imgGraph.Visible = false;


    }



    private float GetPercentileScoreUserwise_testsec(float totalmark, int testsecionid)
    {
        double score = 0;
        int XF = 1, CF = 0, totnum = 0;
        string quesrystring = "select sum(TotalScore) as SecTotalScore,userid from ScoreTable where TestId=" + testid + " and TestSectionId=" + testsecionid + " group by Userid";
        DataSet dssecdet = new DataSet();
        dssecdet = clsClasses.GetValuesFromDB(quesrystring);
        if (dssecdet != null)
            if (dssecdet.Tables.Count > 0)
                if (dssecdet.Tables[0].Rows.Count > 0)
                {
                    totnum = dssecdet.Tables[0].Rows.Count;
                    for (int i = 0; i < dssecdet.Tables[0].Rows.Count; i++)
                    //foreach (var totalmarkdet in GetPercentileScore)
                    {
                        float mark = float.Parse(dssecdet.Tables[0].Rows[i]["SecTotalScore"].ToString());
                        if (mark >= (totalmark - .5) && mark <= (totalmark + .5))
                            XF = XF + 1;
                        else if (mark < (totalmark - .5))
                            CF = CF + 1;
                    }
                    double totsamescore = .5 * XF; totnum = totnum + 1;
                    //score = (CF + (.5 * XF) * 100) / totnum;
                    score = ((CF + totsamescore) * 100) / totnum;

                }
        float scoretotal = 0;
        scoretotal = float.Parse(score.ToString());
        return scoretotal;
    }

    private float GetPercentileScoreUserwise(float totalmark, string sectionname, string testsecId)
    {

        double score = 0;
        int XF = 1, CF = 0, totnum = 1;
        DataSet dsRefScoreDetails = new DataSet();
        string quesrystring = "select * from ScoreTable where TestId = " + testid + " and TestSectionId =" + testsecId + " and SectionName ='" + sectionname + "'";//like '%" + sectionname + "%'"; //
        //lblMessage.Text += " query --- = " + quesrystring;
        dsRefScoreDetails = clsClasses.GetValuesFromDB(quesrystring);
        if (dsRefScoreDetails != null)
            if (dsRefScoreDetails.Tables.Count > 0)
                if (dsRefScoreDetails.Tables[0].Rows.Count > 0)
                {
                    totnum = int.Parse(dsRefScoreDetails.Tables[0].Rows.Count.ToString());
                    //lblMessage.Text += " rowcount= " + totnum;
                    for (int i = 0; i < dsRefScoreDetails.Tables[0].Rows.Count; i++)
                    {
                        float mark = float.Parse(dsRefScoreDetails.Tables[0].Rows[i]["TotalScore"].ToString());
                        if (mark >= (totalmark - .5) && mark <= (totalmark + .5))
                            XF = XF + 1;
                        else if (mark < (totalmark - .5))
                            CF = CF + 1;
                    }
                    double totsamescore = .5 * XF; totnum = totnum + 1;

                    score = ((CF + totsamescore) / totnum) * 100;

                }
        float scoretotal = 0;
        scoretotal = float.Parse(score.ToString());
        return scoretotal;

    }

    private String GetBandDescription(int mark, string secName)
    {
        string remarks = "";
        int secid = GetSectionId(secName);
        string querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid;
        querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
        querystring1 += " AND VariableId = " + secid;
        DataSet ds1 = new DataSet();
        ds1 = clsClasses.GetValuesFromDB(querystring1);
        if (ds1 != null)
            if (ds1.Tables.Count > 0)
                if (ds1.Tables[0].Rows.Count > 0)
                    if (ds1.Tables[0].Rows[0]["DisplayName"] != "")
                        remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();

        return remarks;
    }

    private int GetSectionwiseTotalQuestionMarks(string sectioname, string testsectionid, int index)
    {
        int totalMemWordQuesCount = 0, totalMemImagesQuesCount = 0, totalRatingQuesCount = 0, totalOtherQuesCount = 0;
        int totalObjQuesCount = 0, totalFillQuesCount = 0, totalImageQuesCount = 0, totalVideoQuesCount = 0, totalAudioQuesCount = 0,
            totalPhotoQuesCount = 0;
        string QuerystringQuestionCount = "select sum(ObjQuestionCount) as ObjQuestions,sum(FillBlanksQuestionCount) as FillQuestions," +
                                            "sum(RatingQuestionCount) as RatingQuestions, sum(ImageQuestionCount) as ImageQuestions," +
                                            "sum(VideoQuestionCount) as VideoQuestions,sum(AudioQuestionCount) as AudioQuestions," +
                                            "sum(PhotoTypeQuestionCount) as PhotoQuestions,sum(WordTypeMemQuestionCount) as WordMemQuestions," +
                                            "sum(ImageTypeMemQuestionCount) as ImageMemQuestions from questioncount where TestId=" + testid + " and TestSectionId=" + testsectionid;
        if (index == 0)
            QuerystringQuestionCount += "and SectionName='" + sectioname + "'";

        DataSet dsQuestionCount = clsClasses.GetValuesFromDB(QuerystringQuestionCount);
        if (dsQuestionCount != null)
            if (dsQuestionCount.Tables.Count > 0)
                if (dsQuestionCount.Tables[0].Rows.Count > 0)
                {
                    for (int c = 0; c < dsQuestionCount.Tables[0].Rows.Count; c++)
                    {
                        if (dsQuestionCount.Tables[0].Rows[c]["ObjQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["ObjQuestions"].ToString() != "")
                        {
                            totalObjQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["ObjQuestions"].ToString());
                            totalOtherQuesCount += totalObjQuesCount;
                        }
                        if (dsQuestionCount.Tables[0].Rows[c]["FillQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["FillQuestions"].ToString() != "")
                        {
                            totalFillQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["FillQuestions"].ToString());
                            totalOtherQuesCount += totalFillQuesCount;
                        }
                        if (dsQuestionCount.Tables[0].Rows[c]["ImageQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["ImageQuestions"].ToString() != "")
                        {
                            totalImageQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["ImageQuestions"].ToString());
                            totalOtherQuesCount += totalImageQuesCount;
                        }
                        if (dsQuestionCount.Tables[0].Rows[c]["VideoQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["VideoQuestions"].ToString() != "")
                        {
                            totalVideoQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["VideoQuestions"].ToString());
                            totalOtherQuesCount += totalVideoQuesCount;
                        }
                        if (dsQuestionCount.Tables[0].Rows[c]["AudioQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["AudioQuestions"].ToString() != "")
                        {
                            totalAudioQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["AudioQuestions"].ToString());
                            totalOtherQuesCount += totalAudioQuesCount;
                        }
                        if (dsQuestionCount.Tables[0].Rows[c]["PhotoQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["PhotoQuestions"].ToString() != "")
                        {
                            totalPhotoQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["PhotoQuestions"].ToString());
                            totalOtherQuesCount += totalPhotoQuesCount;
                        }

                        if (dsQuestionCount.Tables[0].Rows[c]["RatingQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["RatingQuestions"].ToString() != "")
                            totalRatingQuesCount += int.Parse(dsQuestionCount.Tables[0].Rows[c]["RatingQuestions"].ToString());
                        if (dsQuestionCount.Tables[0].Rows[c]["WordMemQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["WordMemQuestions"].ToString() != "")
                            totalMemWordQuesCount += int.Parse(dsQuestionCount.Tables[0].Rows[c]["WordMemQuestions"].ToString());
                        if (dsQuestionCount.Tables[0].Rows[c]["ImageMemQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["ImageMemQuestions"].ToString() != "")
                            totalMemImagesQuesCount += int.Parse(dsQuestionCount.Tables[0].Rows[c]["ImageMemQuestions"].ToString());
                    }
                }

        int totalmark = 0;
        DataSet dsCount = new DataSet();
        int memimageQuesValue = 0, memWordsQuesValue = 0, quesValue = 0, ratingQuesValue = 0;
        string quesryString = "";
        // memImages questions
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memImages where status=1 and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memImages where status=1 and TestId=" + testid + " and TestSectionId=" + testsectionid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        memimageQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (memimageQuesValue > totalMemImagesQuesCount)
                            memimageQuesValue = totalMemImagesQuesCount;
                    }

        //memWords questions
        dsCount = new DataSet();
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memWords where status=1 and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memWords where status=1 and TestId=" + testid + " and TestSectionId=" + testsectionid + "  and SectionName='" + sectioname + "'";
        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        memWordsQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (memWordsQuesValue > totalMemWordQuesCount)
                            memWordsQuesValue = totalMemWordQuesCount;
                    }

        //Rating questions
        dsCount = new DataSet();
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'RatingType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'RatingType' and TestId=" + testid + " and TestSectionId=" + testsectionid + "  and SectionName='" + sectioname + "'";

        //lblMessage.Text += quesryString;

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        int totalrate = 0; int questionid = 0;
                        ratingQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (ratingQuesValue > totalRatingQuesCount)
                            ratingQuesValue = totalRatingQuesCount;
                        // questionid = int.Parse(dsCount.Tables[0].Rows[0][1].ToString());
                        // lblMessage.Text += sectioname + " ratingquesCount= " + ratingQuesValue.ToString();// 021209 bip
                        if (index > 0)
                        {
                            var optionCount = from optcount in dataclass.View_TestBaseQuestionLists
                                              where optcount.TestSectionId == int.Parse(testsectionid) && optcount.TestId == testid && optcount.Category == "RatingType" && optcount.TestBaseQuestionStatus == 1
                                              select optcount;
                            if (optionCount.Count() > 0)
                            {
                                if (optionCount.First().Option1 != null && optionCount.First().Option1.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option2 != null && optionCount.First().Option2.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option3 != null && optionCount.First().Option3.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option4 != null && optionCount.First().Option4.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option5 != null && optionCount.First().Option5.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option6 != null && optionCount.First().Option6.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option7 != null && optionCount.First().Option7.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option8 != null && optionCount.First().Option8.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option9 != null && optionCount.First().Option9.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option10 != null && optionCount.First().Option10.ToString() != "")
                                    totalrate++;
                                //if (totalrate > 0)
                                ratingQuesValue = ratingQuesValue * (totalrate - 1);// bip 08-06-2011

                                // lblMessage.Text += " totaloptions= " + totalrate + " TotRate= " + ratingQuesValue.ToString() + " ... ";
                            }
                        }
                        else
                        {
                            var optionCount = from optcount in dataclass.View_TestBaseQuestionLists
                                              where optcount.TestSectionId == int.Parse(testsectionid) && optcount.SectionName == sectioname && optcount.TestId == testid && optcount.Category == "RatingType" && optcount.TestBaseQuestionStatus == 1
                                              select optcount;
                            if (optionCount.Count() > 0)
                            {
                                if (optionCount.First().Option1 != null && optionCount.First().Option1.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option2 != null && optionCount.First().Option2.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option3 != null && optionCount.First().Option3.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option4 != null && optionCount.First().Option4.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option5 != null && optionCount.First().Option5.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option6 != null && optionCount.First().Option6.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option7 != null && optionCount.First().Option7.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option8 != null && optionCount.First().Option8.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option9 != null && optionCount.First().Option9.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option10 != null && optionCount.First().Option10.ToString() != "")
                                    totalrate++;
                                //if (totalrate > 0)
                                ratingQuesValue = ratingQuesValue * (totalrate - 1);// bip 08-06-2011
                            }
                        }
                    }

        dsCount = new DataSet();
        int curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'Objective' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'Objective' and TestId=" + testid + " and SectionName='" + sectioname + "'";
        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalObjQuesCount)
                            curvalue = totalObjQuesCount;

                        quesValue += curvalue;
                    }

        // filblanks
        dsCount = new DataSet();
        curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'FillBlanks' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'FillBlanks' and TestId=" + testid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalFillQuesCount)
                            curvalue = totalFillQuesCount;

                        quesValue += curvalue;
                    }

        // imagetype
        dsCount = new DataSet();
        curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'ImageType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'ImageType' and TestId=" + testid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalImageQuesCount)
                            curvalue = totalImageQuesCount;

                        quesValue += curvalue;
                    }
        // videotype
        dsCount = new DataSet();
        curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'VideoType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'VideoType' and TestId=" + testid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalVideoQuesCount)
                            curvalue = totalVideoQuesCount;

                        quesValue += curvalue;
                    }
        // audiotype
        dsCount = new DataSet();
        curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'AudioType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'AudioType' and TestId=" + testid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalAudioQuesCount)
                            curvalue = totalAudioQuesCount;

                        quesValue += curvalue;
                    }
        // phototype
        dsCount = new DataSet();
        curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'PhotoType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'PhotoType' and TestId=" + testid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalPhotoQuesCount)
                            curvalue = totalPhotoQuesCount;

                        quesValue += curvalue;
                    }

        //////

        totalmark = memimageQuesValue + memWordsQuesValue + quesValue + ratingQuesValue;

        return totalmark;
    }

    private Boolean CheckExistence(string section, int testsectinid,int user_id)
    {
        Boolean result = false;
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["SectionName"].ToString() == section && dt.Rows[i]["TestSectionId"].ToString() == testsectinid.ToString()&& dt.Rows[i]["USERID"].ToString() == user_id.ToString())
                {
                    result = true;
                    Session["RowID"] = i;
                    break;
                }
            }
        }
        return result;
    }

    private int GetSectionId(string sectionname)
    {
        int secID = 0;
        string querystring1 = "select SectionId from SectionDetail where ParentId=0 and SectionName='" + sectionname + "'";

        DataSet ds1 = new DataSet();
        ds1 = clsClasses.GetValuesFromDB(querystring1);
        if (ds1 != null)
            if (ds1.Tables.Count > 0)
                if (ds1.Tables[0].Rows.Count > 0)
                    if (ds1.Tables[0].Rows[0]["SectionId"] != "")
                        secID = int.Parse(ds1.Tables[0].Rows[0]["SectionId"].ToString());

        return secID;
    }


    private int GetBenchMarkVariablewise(string variableName, string curmark, string TESTSECTIONID)
    {
        int benchmark = 0;
        string remarks = "";
        try
        {
            string[] strValue = curmark.Split(new char[] { '.' });
            int mark = int.Parse(strValue[0]);

            int secid = GetSectionId(variableName);
            string querystring1 = "";
            if (curmark == "0")
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid + " AND TestSectionId = " + TESTSECTIONID;
                querystring1 += " AND (0 >= MarkFrom AND 0 <= MarkTo)";
                querystring1 += " AND VariableId = " + secid;
            }
            else
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid + " AND TestSectionId = " + TESTSECTIONID;
                querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//   MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
                querystring1 += " AND VariableId = " + secid + "";
            }
            bool benchmarkexists = false;
            DataSet ds1 = new DataSet();
            ds1 = clsClasses.GetValuesFromDB(querystring1);
            if (ds1 != null)
                if (ds1.Tables.Count > 0)
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["BenchMark"] != "")
                            benchmark = int.Parse(ds1.Tables[0].Rows[0]["BenchMark"].ToString());
                        remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();
                        benchmarkexists = true;
                    }

            if (benchmarkexists == false)
            {
                benchmark = GetBenchMarkTestSectionwise(curmark, TESTSECTIONID);
            }

            // lblMessage.Text += querystring1;

        }
        catch (Exception ex) { benchmark = 0; }

        return benchmark;
    }

     

    private string GetTestSectionName(int testsecionID)
    {
        string secName = "";
        var testsectionname = from testsecname in dataclass.TestSectionsLists
                              where testsecname.TestSectionId == testsecionID
                              select testsecname;
        if (testsectionname.Count() > 0)
        {
            if (testsectionname.First().SectionName != null && testsectionname.First().SectionName != "")
                secName = testsectionname.First().SectionName.ToString();
        }
        return secName;
    }

    private int GetBenchMarkTestSectionwise(string curmark, string TESTSECTIONID)
    {
        int benchmark = 0;
        string remarks = "";
        try
        {
            string[] strValue = curmark.Split(new char[] { '.' });
            int mark = int.Parse(strValue[0]);

            // int secid = GetSectionId(variableName);
            string querystring1 = "";

            if (curmark == "0")
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestSectionResultBands WHERE TestID = " + testid;
                querystring1 += " AND (0 >= MarkFrom AND 0 <= MarkTo)";
                querystring1 += " AND SectionId = " + TESTSECTIONID;
            }
            else
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestSectionResultBands WHERE TestID = " + testid;
                querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//   MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
                querystring1 += " AND SectionId = " + TESTSECTIONID;
            }
            DataSet ds1 = new DataSet();
            ds1 = clsClasses.GetValuesFromDB(querystring1);
            if (ds1 != null)
                if (ds1.Tables.Count > 0)
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["BenchMark"] != "")
                            benchmark = int.Parse(ds1.Tables[0].Rows[0]["BenchMark"].ToString());
                        remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();

                    }
        }
        catch (Exception ex) { benchmark = 0; }

        return benchmark;
    }

    

    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "OrgAdmin")
        { Session["SubCtrl"] = "ReportSel_OrgAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }

        else if (Session["usertype"].ToString() == "GrpAdmin")
        { Session["SubCtrl"] = "ReportSel_GroupAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }
        else if (Session["usertype"].ToString() == "SuperAdmin")
        { Session["SubCtrl"] = "ReportSel_SuperAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        
        Session["testsectionreportvalues"] = dtTestSection;
        DataTable dtvariablevalues = new DataTable();
        DataRow drvariablelist;
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows[0].Cells.Count; i++)
            {
                dtvariablevalues.Columns.Add("column" + (i + 1).ToString());
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                drvariablelist = dtvariablevalues.NewRow();
                for (int j = 0; j < GridView1.Rows[i].Cells.Count; j++)
                {
                    if (GridView1.Rows[i].Cells[j].Text != "&nbsp;")
                        drvariablelist["column" + (j + 1).ToString()] = GridView1.Rows[i].Cells[j].Text;
                }
                dtvariablevalues.Rows.Add(drvariablelist);
            }

            Session["variablereportvalues"] = dtvariablevalues;

            Response.Redirect("ReportPrint.aspx"); return;
        }
        else { lblMessage.Text = "No Values for Print"; }
    }
}
