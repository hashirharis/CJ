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
using System.Data.SqlClient;
using System.Linq;

public partial class FillBalnksQues : System.Web.UI.UserControl
{
    int userid = 0;
    string usercode = "";
    DBManagementClass clsclass = new DBManagementClass();
  AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    
    DataSet ds;
    int qusid1 = 0;
    int qusid2 = 0;
    int qusid3 = 0;
    int qusid4 = 0;
    int qusid5 = 0;
    int qusid6 = 0;
    int qusid7 = 0;
    int qusid8 = 0;
    int qusid9 = 0;
    int qusid10 = 0;
    int pagecount = 0;
    int quesperPage = 5;
    int testId = 0; int testsectionid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserTestId"] != null)
        {
            if (Session["timeExpired"] != null)
                if (Session["timeExpired"].ToString() == "True")
                {
                    CheckTime();
                    return;
                }

           // if (CheckTime() == true) return;//bip 15052010
           
            testId = int.Parse(Session["UserTestId"].ToString());
            if (Session["UserID"] != null)
            {
                userid = int.Parse(Session["UserID"].ToString());                
                FillQuestions();
            }
        }
    }
    private void FillFillBlanksQuestionInstructions()
    {
        
        int testSecondVariableId = 0, testFirstVariableId = 0, testSectionID = 0;
        if (Session["CurrentTestSectionId"] != null)
            testSectionID = int.Parse(Session["CurrentTestSectionId"].ToString());

        if (Session["CurrentTestSecondVariableId"] != null)
            testSecondVariableId = int.Parse(Session["CurrentTestSecondVariableId"].ToString());
        if (Session["CurrentTestFirstVariableId"] != null)
            testFirstVariableId = int.Parse(Session["CurrentTestFirstVariableId"].ToString());

        bool valueexists = false;
        DataSet dsInstructiondet = new DataSet();
        string quesrystring = "";
        if (testSecondVariableId > 0)
            quesrystring = "select InstructionDetails,InstructionImage1,InstructionDetails2,InstructionImage2,InstructionDetails3 from TestSectionVariablewiseInstructions where CategoryId =2 and TestId=" + testId + " and TestSectionId =" + testSectionID + " and FirstVariableId=" + testFirstVariableId + " and SecondVariableId =" + testSecondVariableId;
        else if (testFirstVariableId > 0)
            quesrystring = "select InstructionDetails,InstructionImage1,InstructionDetails2,InstructionImage2,InstructionDetails3 from TestSectionVariablewiseInstructions where CategoryId =2 and TestId=" + testId + " and TestSectionId =" + testSectionID + " and FirstVariableId=" + testFirstVariableId;

        dsInstructiondet = clsclass.GetValuesFromDB(quesrystring);

        if (dsInstructiondet != null) if (dsInstructiondet.Tables.Count > 0) if (dsInstructiondet.Tables[0].Rows.Count > 0)
                { //divInstructions.InnerHtml = dsInstructiondet.Tables[0].Rows[0]["InstructionDetails"].ToString(); valueexists = true; 
                    string styleproperties = "padding-left: 40px";
                    HtmlTable tblInstructions = new HtmlTable();
                    HtmlTableRow trInstructions = new HtmlTableRow();
                    HtmlTableCell tcellInstructions = new HtmlTableCell();
                    if (dsInstructiondet.Tables[0].Rows[0]["InstructionDetails"].ToString() != null && dsInstructiondet.Tables[0].Rows[0]["InstructionDetails"].ToString() != "")
                    {
                        tcellInstructions.InnerHtml = dsInstructiondet.Tables[0].Rows[0]["InstructionDetails"].ToString();
                        trInstructions.Cells.Add(tcellInstructions);
                        tblInstructions.Rows.Add(trInstructions);
                        valueexists = true;
                    }
                    if (dsInstructiondet.Tables[0].Rows[0]["InstructionImage1"].ToString() != null && dsInstructiondet.Tables[0].Rows[0]["InstructionImage1"].ToString() != "")
                    {
                        trInstructions = new HtmlTableRow();
                        tcellInstructions = new HtmlTableCell();
                        tcellInstructions.InnerHtml = "<div style='" + styleproperties + "'><img alt='' src='QuestionAnswerFiles/InstructionImages/" + dsInstructiondet.Tables[0].Rows[0]["InstructionImage1"].ToString() + "' /></div>";
                        trInstructions.Cells.Add(tcellInstructions);
                        tblInstructions.Rows.Add(trInstructions);
                        valueexists = true;
                    }
                    if (dsInstructiondet.Tables[0].Rows[0]["InstructionDetails2"].ToString() != null && dsInstructiondet.Tables[0].Rows[0]["InstructionDetails2"].ToString() != "")
                    {
                        trInstructions = new HtmlTableRow();
                        tcellInstructions = new HtmlTableCell();
                        tcellInstructions.InnerHtml = dsInstructiondet.Tables[0].Rows[0]["InstructionDetails2"].ToString();
                        trInstructions.Cells.Add(tcellInstructions);
                        tblInstructions.Rows.Add(trInstructions);
                        valueexists = true;
                    }
                    if (dsInstructiondet.Tables[0].Rows[0]["InstructionImage2"].ToString() != null && dsInstructiondet.Tables[0].Rows[0]["InstructionImage2"].ToString() != "")
                    {
                        trInstructions = new HtmlTableRow();
                        tcellInstructions = new HtmlTableCell();
                        tcellInstructions.InnerHtml = "<div style='" + styleproperties + "'><img alt='' src='QuestionAnswerFiles/InstructionImages/" + dsInstructiondet.Tables[0].Rows[0]["InstructionImage2"].ToString() + "' /></div>";
                        trInstructions.Cells.Add(tcellInstructions);
                        tblInstructions.Rows.Add(trInstructions);
                        valueexists = true;
                    }
                    if (dsInstructiondet.Tables[0].Rows[0]["InstructionDetails3"].ToString() != null && dsInstructiondet.Tables[0].Rows[0]["InstructionDetails3"].ToString() != "")
                    {
                        trInstructions = new HtmlTableRow();
                        tcellInstructions = new HtmlTableCell();
                        tcellInstructions.InnerHtml = dsInstructiondet.Tables[0].Rows[0]["InstructionDetails3"].ToString();
                        trInstructions.Cells.Add(tcellInstructions);
                        tblInstructions.Rows.Add(trInstructions);
                        valueexists = true;
                    }
                    divInstructions.Controls.Clear();
                    divInstructions.Controls.Add(tblInstructions);

                }


        if (valueexists == false)
            FillCommonInstructions();
        else FillTitle();
    }
    private void FillTitle()
    {
        var InstructionDetails = from instructiondet in dataclass.TestInstructions
                                 where instructiondet.CategoryId == 2
                                 select instructiondet;
        if (InstructionDetails.Count() > 0)
        {
            if (InstructionDetails.First().Title != null)
                divtitle.InnerHtml = InstructionDetails.First().Title.ToString();
        }
    }
    private void FillCommonInstructions()
    {
        var InstructionDetails = from instructiondet in dataclass.TestInstructions
                                 where instructiondet.CategoryId == 2
                                 select instructiondet;
        if (InstructionDetails.Count() > 0)
        {
            if (InstructionDetails.First().Title != null)
                divtitle.InnerHtml = InstructionDetails.First().Title.ToString();

            if (InstructionDetails.First().InstructionDetails != null)
                divInstructions.InnerHtml = InstructionDetails.First().InstructionDetails.ToString();
        }
    }

    private DataSet GetQuestionList(int testsectionid)
    { 
        //// bip 07122009        

        string firstVariableName = ""; string secondVariableName = "";
        if (Session["TestFirstVariableName"] != null)
            firstVariableName = Session["TestFirstVariableName"].ToString();
        if (Session["TestSecondVariableName"] != null)
            secondVariableName = Session["TestSecondVariableName"].ToString();

        //
        DataSet dsQuesCount;
        string querystring = "";
        
        DataTable dtQuestionList = new DataTable();
        //QuestionID,Question,Answer,Option1,Option2,Option3,Option4,Option5
        dtQuestionList.Columns.Add("QuestionID");
        dtQuestionList.Columns.Add("Question");
        dtQuestionList.Columns.Add("Answer");
        dtQuestionList.Columns.Add("Option1");
        dtQuestionList.Columns.Add("Option2");
        dtQuestionList.Columns.Add("Option3");
        dtQuestionList.Columns.Add("Option4");
        dtQuestionList.Columns.Add("Option5");
        dtQuestionList.Columns.Add("QuestionFileName");
        dtQuestionList.Columns.Add("QuestionFileNameSub1");
        DataRow drQurstionList;
        // bip 08122009;
        if (secondVariableName != "")
            querystring = "select distinct FillBlanksQuestionCount,SectionId from QuestionCount where FillBlanksQuestionCount>0 and TestId=" + testId + " and TestSectionId=" + testsectionid + " and SectionName='" + firstVariableName + "' and SectionNameSub1='" + secondVariableName + "' order by sectionid";
        else if (firstVariableName != "")
            querystring = "select distinct FillBlanksQuestionCount,SectionId from QuestionCount where FillBlanksQuestionCount>0 and TestId=" + testId + " and TestSectionId=" + testsectionid + " and SectionName='" + firstVariableName + "' order by sectionid";

        DataSet dsQuestioncount = new DataSet();
        dsQuestioncount = clsclass.GetValuesFromDB(querystring);
        if (dsQuestioncount != null)
            if (dsQuestioncount.Tables.Count > 0)
                if (dsQuestioncount.Tables[0].Rows.Count > 0)
                    for (int c = 0; c < dsQuestioncount.Tables[0].Rows.Count; c++)//        
        {            
            int fillQuescount = int.Parse(dsQuestioncount.Tables[0].Rows[c]["FillBlanksQuestionCount"].ToString());
            int sectionid = int.Parse(dsQuestioncount.Tables[0].Rows[c]["SectionId"].ToString());
           
            if (fillQuescount > 0)
            {

                querystring = "SELECT TOP (" + fillQuescount + ") QuestionID,Question,Answer,Option1,Option2,Option3,Option4,Option5,QuestionFileName,QuestionFileNameSub1  FROM View_TestBaseQuestionList where Category = 'FillBlanks' AND TestBaseQuestionStatus=1 AND TestSectionId=" + testsectionid + " AND SectionId=" + sectionid + " AND TestId=" + testId + " ORDER BY RAND((100*QuestionID)*DATEPART(millisecond, GETDATE())) ";
                
                dsQuesCount = new DataSet();
                dsQuesCount = clsclass.GetValuesFromDB(querystring);
                if (dsQuesCount != null)
                    if (dsQuesCount.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsQuesCount.Tables[0].Rows.Count; i++)
                        {
                            drQurstionList = dtQuestionList.NewRow();
                            drQurstionList["QuestionID"] = dsQuesCount.Tables[0].Rows[i]["QuestionID"];
                            drQurstionList["Question"] = dsQuesCount.Tables[0].Rows[i]["Question"];
                            drQurstionList["Answer"] = dsQuesCount.Tables[0].Rows[i]["Answer"];
                            drQurstionList["Option1"] = dsQuesCount.Tables[0].Rows[i]["Option1"];
                            drQurstionList["Option2"] = dsQuesCount.Tables[0].Rows[i]["Option2"];
                            drQurstionList["Option3"] = dsQuesCount.Tables[0].Rows[i]["Option3"];
                            drQurstionList["Option4"] = dsQuesCount.Tables[0].Rows[i]["Option4"];
                            drQurstionList["Option5"] = dsQuesCount.Tables[0].Rows[i]["Option5"];
                            drQurstionList["QuestionFileName"] = dsQuesCount.Tables[0].Rows[i]["QuestionFileName"];
                            drQurstionList["QuestionFileNameSub1"] = dsQuesCount.Tables[0].Rows[i]["QuestionFileNameSub1"];

                            dtQuestionList.Rows.Add(drQurstionList);
                        }
                    }
            }
        }
        DataSet dsQuestionList = new DataSet();
        if (dtQuestionList.Rows.Count > 0)
        {
            dsQuestionList.Tables.Add(dtQuestionList); Session["questionColl_fill"] = dsQuestionList;
            Session["totalQuesCount_fill"] = dsQuestionList.Tables[0].Rows.Count.ToString();
        }
        else { Session["questionColl_fill"] = null; dsQuestionList = null; }

        return dsQuestionList;
    }
    private DataSet GetTempData()
    {
        DataSet dsTempData = new DataSet();
        //// 220110 ... bip
        int testSecondVariableId = 0, testFirstVariableId = 0;
        if (Session["CurrentTestSecondVariableId"] != null)
            testSecondVariableId = int.Parse(Session["CurrentTestSecondVariableId"].ToString());
        if (Session["CurrentTestFirstVariableId"] != null)
            testFirstVariableId = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
        int questionid = 0; string questiontype = "FillBlanks";

        string querystringquesTemp = "select distinct QuestionId from UserTestQuestions_Temp where UserID=" + userid + " and TestId=" + testId + " and TestSectionId=" + testsectionid + " and FirstVariableId=" + testFirstVariableId + " and QuestionType='" + questiontype + "'";
        if (testSecondVariableId > 0)
            querystringquesTemp += " and SecondVariableId = " + testSecondVariableId;
        DataSet dsquestionIdColl = clsclass.GetValuesFromDB(querystringquesTemp);
        if (dsquestionIdColl != null)
            if (dsquestionIdColl.Tables.Count > 0)
                if (dsquestionIdColl.Tables[0].Rows.Count > 0)
                {
                    string questionids = "";
                    for (int index = 0; index < dsquestionIdColl.Tables[0].Rows.Count; index++)
                    {
                        if (questionids != "")
                            questionids += " or ";
                        questionids += " QuestionId=" + dsquestionIdColl.Tables[0].Rows[index]["QuestionId"].ToString();
                    }

                    querystringquesTemp = "SELECT QuestionID,Question,Answer,Option1,Option2,Option3,Option4,Option5  FROM View_TestBaseQuestionList where " + questionids;
                    dsTempData = clsclass.GetValuesFromDB(querystringquesTemp);
                    Session["questionColl_fill"] = dsTempData;

                    var GetPageIndex = from pageindex in dataclass.UserTestPageIndex_Temps
                                       where pageindex.UserId == userid && pageindex.TestId == testId && pageindex.TestSectionId == testsectionid &&
                                       pageindex.FirstVariableId == testFirstVariableId && pageindex.SecondVariableId == testSecondVariableId &&
                                       pageindex.QuestionType == questiontype
                                       select pageindex;
                    if (GetPageIndex.Count() > 0)
                    {
                        if (GetPageIndex.First().PageIndex != null)
                        {
                            pagecount = int.Parse(GetPageIndex.First().PageIndex.ToString());
                            Session["pagecount_fill"] = pagecount;                            
                        }
                    }
                }
                else dsTempData = null;

        return dsTempData;
    }
    private void FillQuestions()
    {

        //try
        //{
            ClearControls();
            int QuestionID = 0;
            string querystring = "";           

            if (Session["pagecount_fill"] != null)
                pagecount = int.Parse(Session["pagecount_fill"].ToString());
                       
            string connString = ConfigurationManager.ConnectionStrings["talentscoutConnectionString"].ConnectionString;
            //int slno = pagecount * 1 + 1;
            //if (slno < 0)
            //    slno = 1;
            int testsectionid = 0; 
            if (Session["CurrentTestSectionId"] != null)
                testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());  
            if (Session["questionColl_fill"] != null)
                ds = (DataSet)Session["questionColl_fill"];
            else
            {
                //bip 081009
                //int testsectionid = 0;
                if (Session["CurrentTestSectionId"] != null)
                {
                    testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());
                    //
                    //  //// 220110 ... bip
                    int testSecondVariableId = 0, testFirstVariableId = 0;
                    if (Session["CurrentTestSecondVariableId"] != null)
                        testSecondVariableId = int.Parse(Session["CurrentTestSecondVariableId"].ToString());
                    if (Session["CurrentTestFirstVariableId"] != null)
                        testFirstVariableId = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
                    int questionid = 0; string questiontype = "FillBlanks";

                    ds = GetTempData();
                    bool newentry = false;
                    if (ds == null)
                    {
                        newentry = true;
                        ds = GetQuestionList(testsectionid);
                    }
                    else if (ds.Tables.Count <= 0)
                    { newentry = true; ds = GetQuestionList(testsectionid); }

                    //ds = GetQuestionList(testsectionid);
                    if (ds == null)
                    {

                        string evaldirection = "Next";
                        if (Session["evaldirection"] != null)
                            evaldirection = Session["evaldirection"].ToString();
                        if (evaldirection == "Next")
                            GoToNextPage();
                        else GoToPreviousPage();
                        return;

                        //GoToNextPage(); return; 
                    }
                    else // store the questiondetails in a temp table.
                    {
                        if (newentry == true)
                            if (ds.Tables.Count > 0)
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int q = 0; q < ds.Tables[0].Rows.Count; q++)
                                    {
                                        questionid = int.Parse(ds.Tables[0].Rows[q]["QuestionID"].ToString());
                                        dataclass.Procedure_UserTestQuestions_Temp(userid, testId, testsectionid, testFirstVariableId, testSecondVariableId, questionid, questiontype);
                                    }
                                }
                    }
                }
            }
            int slno = pagecount * quesperPage + 1;
            if (slno < 0)
                slno = 1;

            //not completed below
            int pagecnt = 0;
            int curntquescnt = 0;
            int slnos = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                //Session["ValueExists"] = "True";

                FillFillBlanksQuestionInstructions();

                Session["totalQuesAvailable_fill"] = ds.Tables[0].Rows.Count.ToString();
                int j = 0;
                for (int i = slno - 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (j >= quesperPage) break;
                    string Answer = "";
                    Session["CurrentControlCtrl"] = "FillBalnksQues.ascx";// bip 08012010
                    Session["ValueExists"] = "True";

                    switch (j)
                    {
                        case 0:
                            lblNo1.Text = slno.ToString() + ".  ";
                            //lblQues1.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues1.InnerHtml=ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID1.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();
                            /// bip 13052010 inorder to add images in the question part of fillblanks questions
                            if (ds.Tables[0].Rows[i]["QuestionFileName"].ToString() != "")
                            {
                                imgQuestion1.Visible = true;
                                imgQuestion1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileName"].ToString();                                
                            }
                            if (ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString() != "")
                            {
                                imgQuestion1Sub1.Visible = true;
                                imgQuestion1Sub1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString();                                
                            }
                            ///
                            txtAns1.Visible = true;
                            var Ques1 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;//Ques.UserCode == usercode &&
                            if (Ques1.Count() > 0)
                                if (Ques1.First().Answer != null)
                                {
                                    txtAns1.Text = Ques1.First().Answer.ToString();
                                }
                            
                            curntquescnt++;
                            j++;
                            pnlQuestion1.Visible = true;
                            break;
                        case 1:
                            lblNo2.Text = (slno + 1).ToString() + ".  ";
                            //lblQues2.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues2.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID2.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                            /// bip 13052010 inorder to add images in the question part of fillblanks questions
                            if (ds.Tables[0].Rows[i]["QuestionFileName"].ToString() != "")
                            {
                                imgQuestion2.Visible = true;
                                imgQuestion2.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileName"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString() != "")
                            {
                                imgQuestion1Sub2.Visible = true;
                                imgQuestion1Sub2.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString();
                            }
                            ///

                            txtAns2.Visible = true;

                            var Ques2 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;//Ques.UserCode == usercode &&
                            if (Ques2.Count() > 0)

                                if (Ques2.First().Answer != null)
                                {
                                    txtAns2.Text = Ques2.First().Answer.ToString();
                                }
                           
                            curntquescnt++;
                            j++;
                            pnlQuestion2.Visible = true;
                            break;
                        case 2:
                            lblNo3.Text = (slno + 2).ToString() + ".  ";
                            //lblQues3.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues3.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID3.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                            /// bip 13052010 inorder to add images in the question part of fillblanks questions
                            if (ds.Tables[0].Rows[i]["QuestionFileName"].ToString() != "")
                            {
                                imgQuestion3.Visible = true;
                                imgQuestion3.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileName"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString() != "")
                            {
                                imgQuestion1Sub3.Visible = true;
                                imgQuestion1Sub3.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString();
                            }
                            ///

                            txtAns3.Visible = true;

                            var Ques3 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques3.Count() > 0)
                                if (Ques3.First().Answer != null)
                                {
                                    txtAns3.Text = Ques3.First().Answer.ToString();
                                }
                            
                            curntquescnt++;
                            j++;
                            pnlQuestion3.Visible = true;
                            break;
                        case 3:
                            lblNo4.Text = (slno + 3).ToString() + ".  ";
                            //lblQues4.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues4.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID4.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                            /// bip 13052010 inorder to add images in the question part of fillblanks questions
                            if (ds.Tables[0].Rows[i]["QuestionFileName"].ToString() != "")
                            {
                                imgQuestion4.Visible = true;
                                imgQuestion4.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileName"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString() != "")
                            {
                                imgQuestion1Sub4.Visible = true;
                                imgQuestion1Sub4.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString();
                            }
                            ///

                            txtAns4.Visible = true;
                            var Ques4 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques4.Count() > 0)
                                if (Ques4.First().Answer != null)
                                {
                                    txtAns4.Text = Ques4.First().Answer.ToString();
                                }
                            
                            curntquescnt++;
                            j++;
                            pnlQuestion4.Visible = true;
                            break;
                        case 4:
                            lblNo5.Text = (slno + 4).ToString() + ".  ";
                            //lblQues5.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues5.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID5.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                            /// bip 13052010 inorder to add images in the question part of fillblanks questions
                            if (ds.Tables[0].Rows[i]["QuestionFileName"].ToString() != "")
                            {
                                imgQuestion5.Visible = true;
                                imgQuestion5.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileName"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString() != "")
                            {
                                imgQuestion1Sub5.Visible = true;
                                imgQuestion1Sub5.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString();
                            }
                            ///

                            txtAns5.Visible = true;

                            var Ques5 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques5.Count() > 0)
                                if (Ques5.First().Answer != null)
                                {
                                    txtAns5.Text = Ques5.First().Answer.ToString();
                                }
                            
                            curntquescnt++;
                            j++;
                            pnlQuestion5.Visible = true;
                            break;                       
                    }
                }
            }
            else { GoToNextPage(); }
            Session["curntques1"] = curntquescnt;
        //}
        //catch (Exception ex)
        //{
        //    FillQuestions();//bipson 14082010 to avoid arithmetic error...//conn.Close();
        //}
    }

    private void GoToNextPage()
    {
        //ClearSessionValues();
        ClearAllPageCountValues(1);
        Session["evaldirection"] = "Next";
        Session["SubCtrl"] = "ImageQuestions.ascx";
        //Session["SubCtrl"] = "RatingQuestions.ascx";
        Response.Redirect("FJAHome.aspx");
    }
    private Boolean CheckTime()//bip 10052010 
    {
        Boolean timeExpired = false;
        if (CheckTestTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for your test is completed, <br> If you are interested to take this test again please contact our site admin";
            pnlpopup_timer.Visible = true; btnYes_timer_Test.Visible = true; Timer1.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";
            //return;            
        }
        else if (CheckTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for this Test Section is completed, <br> Do you want to continue with the remaining sections under your Test?";
            pnlpopup_timer.Visible = true; btnYes_timer.Visible = true; btnNo_timer.Visible = true; Timer1.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";
            //return;            
        }
        else if (CheckTestSecVarTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for this Variable under selected Section is completed, <br> Do you want to continue with the remaining Variables under your Test Section?";
            pnlpopup_timer.Visible = true; btnYes_timer_TestVariable.Visible = true; btnNo_timer.Visible = true; Timer1.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";
            //return;            
        }
        if (timeExpired == true)
        {
            if (Session["saved"] != null)
                if (Session["saved"].ToString() == "true")
                    return timeExpired;

            SaveAnswers(); Session["saved"] = "true";
        }
        return timeExpired;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        Boolean timeExpired = CheckTime();
        if (timeExpired == false)//bip 10052010
        {
            if (CheckAnswer() == false)
            {
                Timer1.Enabled = false;
                pnlpopup.Visible = true;
                return;
            }
            SaveValues();
        }
        //else SaveAnswers();//bip 10052010
        
    }
    private void SaveValues()
    {
        SaveAnswers();
        usercode = Session["UserCode"].ToString();
        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());


        int curcount = 0;
        int totalcount = 0, totalquesavail = 0;
        if (Session["totalQuesCount_fill"] != null)
            totalcount = int.Parse(Session["totalQuesCount_fill"].ToString());
        if (Session["totalQuesAvailable_fill"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable_fill"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecount_fill"] != null)
            pagecount = int.Parse(Session["pagecount_fill"].ToString());

        int curindex = 0;
        curindex = (pagecount + 1) * quesperPage;
        if (curindex < curcount)
        {
            pagecount++;
            Session["pagecount_fill"] = pagecount;SetCurrentPageCount();
            FillQuestions(); 
        }
        else
        {
            ClearSessionValues();
            GoToNextPage();
        }
    }
    private void SetCurrentPageCount()
    {
        testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());
        //  //// 220110 ... bip
        int testSecondVariableId = 0, testFirstVariableId = 0;
        if (Session["CurrentTestSecondVariableId"] != null)
            testSecondVariableId = int.Parse(Session["CurrentTestSecondVariableId"].ToString());
        if (Session["CurrentTestFirstVariableId"] != null)
            testFirstVariableId = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
        int questionid = 0; string questiontype = "FillBlanks";
        dataclass.Procedure_UserTestPageIndex_Temp(userid, testId, testsectionid, testFirstVariableId, testSecondVariableId, pagecount, questiontype);
    }
    private Boolean CheckAnswer()
    {
        Boolean answercompleted = true;

        string answer = "";
        string message = "You have missed question No:";

        if (lblQuesID1.Text.Trim() != "")
        {
            qusid1 = int.Parse(lblQuesID1.Text.Trim());
            answer = txtAns1.Text.Trim();


            if (answer == "")
            {
                //lblpopup.Text += "QuestionNo:" + lblQuesID1.Text.Trim();
                message += lblNo1.Text.Trim() + ", ";
                answercompleted = false;
            }
        }
        answer = "";
        if (lblQuesID2.Text.Trim() != "")
        {
            qusid2 = int.Parse(lblQuesID2.Text.Trim());
            answer = txtAns2.Text;

            if (answer == "")
            {
                // lblpopup.Text += "QuestionNo:" + lblQuesID2.Text.Trim();
                message += lblNo2.Text.Trim() + ", ";
                answercompleted = false;
            }
        }
        answer = "";

        if (lblQuesID3.Text.Trim() != "")
        {
            qusid3 = int.Parse(lblQuesID3.Text.Trim());
            answer = txtAns3.Text;

            if (answer == "")
            {
                // lblpopup.Text += "QuestionNo:" + lblQuesID3.Text.Trim();
                message += lblNo3.Text.Trim() + ", ";
                answercompleted = false;
            }
        }
        answer = "";
        if (lblQuesID4.Text.Trim() != "")
        {
            qusid4 = int.Parse(lblQuesID4.Text.Trim());
            answer = txtAns4.Text;

            if (answer == "")
            {
                //lblpopup.Text += "QuestionNo:" + lblQuesID1.Text.Trim();
                message += lblNo4.Text.Trim() + ", ";
                answercompleted = false;
            }
        }
        answer = "";
        if (lblQuesID5.Text.Trim() != "")
        {
            qusid5 = int.Parse(lblQuesID5.Text.Trim());
            answer = txtAns5.Text;

            if (answer == "")
            {
                //lblpopup.Text += "QuestionNo:" + lblQuesID1.Text.Trim();
                message += lblNo5.Text.Trim() + ", ";
                answercompleted = false;
            }
        }
        if (answercompleted == false)
        {
            message += " Please, complete all questions before moving to the next page. <br> Do you want to go to next page? ";
            lblpopup.Text = message;
        }
        return answercompleted;
    }

    private void ClearSessionValues()
    {
        //Session["pagecount_fill"] = null;         
    }
    private void ClearAllPageCountValues(int index)
    {
        if (index == 0)
            Session["pagecount_fill"] = null;
        Session["pagecount_img"] = null;
        Session["pagecount_memWords"] = null;
        Session["pagecount_audio"] = null;
        Session["pagecount_memImages"] = null;
        Session["pagecount_imgPhoto"] = null;
        Session["pagecountRating"] = null;
        Session["pagecount_video"] = null;
    }
    private void SaveAnswers()
    {
        string quescategory = "FillBlanks";
        int testsectionid = 0;
        if (Session["CurrentTestSectionId"] != null)
            testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());

        string answer = "";
        int questionid = 0;
        // usercode = Session["UserCode"].ToString();
        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());

        if (lblQuesID1.Text.Trim() != "")
        {
            qusid1 = int.Parse(lblQuesID1.Text.Trim());

            answer = txtAns1.Text;
            dataclass.Procedure_QuesAnswers(qusid1, usercode, tcellQues1.InnerHtml, answer, userid, testId, testsectionid,quescategory);
        }

        if (lblQuesID2.Text.Trim() != "")
        {
            qusid2 = int.Parse(lblQuesID2.Text.Trim());

            answer = txtAns2.Text;
            dataclass.Procedure_QuesAnswers(qusid2, usercode, tcellQues2.InnerHtml, answer, userid, testId, testsectionid,quescategory);
        }

        if (lblQuesID3.Text.Trim() != "")
        {
            qusid3 = int.Parse(lblQuesID3.Text.Trim());

            answer = txtAns3.Text;
            dataclass.Procedure_QuesAnswers(qusid3, usercode, tcellQues3.InnerHtml, answer, userid, testId, testsectionid,quescategory);
        }

        if (lblQuesID4.Text.Trim() != "")
        {
            qusid4 = int.Parse(lblQuesID4.Text.Trim());

            answer = txtAns4.Text;
            dataclass.Procedure_QuesAnswers(qusid4, usercode, tcellQues4.InnerHtml, answer, userid, testId, testsectionid,quescategory);
        }

        if (lblQuesID5.Text.Trim() != "")
        {
            qusid5 = int.Parse(lblQuesID5.Text.Trim());

            answer = txtAns5.Text;
            dataclass.Procedure_QuesAnswers(qusid5, usercode, tcellQues5.InnerHtml, answer, userid, testId, testsectionid,quescategory);
        }
    }   

    private void ClearControls()
    {
        lblNo1.Text = "";
        lblNo2.Text = "";
        lblNo3.Text = "";
        lblNo4.Text = "";
        lblNo5.Text = "";
        
        txtAns1.Text = "";
        txtAns2.Text = "";
        txtAns3.Text = "";
        txtAns4.Text = "";
        txtAns5.Text = "";
        
        //lblQues1.Text = "";
        //lblQues2.Text = "";
        //lblQues3.Text = "";
        //lblQues4.Text = "";
        //lblQues5.Text = "";
        
        txtAns1.Visible = false;
        txtAns2.Visible = false;
        txtAns3.Visible = false;
        txtAns4.Visible = false;
        txtAns5.Visible = false;        
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session["SubCtrl"] = null;
        //Session["MasterCtrl"] = "~/MasterPage.master";
        Response.Redirect("FJAHome.aspx");
    }

    protected void ptnPrevious_Click(object sender, EventArgs e)
    {        
        int curcount = 0;
        int totalcount = 0, totalquesavail = 0;
        if (Session["totalQuesCount_fill"] != null)
            totalcount = int.Parse(Session["totalQuesCount_fill"].ToString());
        if (Session["totalQuesAvailable_fill"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable_fill"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecount_fill"] != null)
            pagecount = int.Parse(Session["pagecount_fill"].ToString());

        int curindex = 0;
        curindex = (pagecount - 1) * quesperPage;
        if (curindex >=0)
        {            
            pagecount--;
            Session["pagecount_fill"] = pagecount;
            SetCurrentPageCount();// 230110 bip
        }
        else
        {
            GoToPreviousPage();           
        }
        FillQuestions();
    }

    private void GoToPreviousPage()
    {
        ClearSessionValues();
        Session["pagecount"] = null;
        Session["pagecount_fill"] = null;
        Session["evaldirection"] = "Previous";
        Session["SubCtrl"] = "ObjectiveQuestns.ascx";
        Response.Redirect("FJAHome.aspx");
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {        
        SaveValues(); pnlpopup.Visible = false;
        Timer1.Enabled = true;// 26-10-2010 bip
    }

    // for first level variablewise time setting 24-02-2010 bip  

    private Boolean CheckTestSecVarTimeValidity()
    {
        if (Session["TestSectionVariableStartTime"] != null)
        {
            if (Session["TestSectionVariableTimeDuration"] != null)
            {
                DateTime dtStart = (DateTime)Session["TestSectionVariableStartTime"];
                DateTime dtNow = DateTime.Now;
                TimeSpan tsNow = dtNow.Subtract(dtStart);
                int hrs = tsNow.Hours;
                int min = tsNow.Minutes;
                int sec = tsNow.Seconds;

                string strDuration = Session["TestSectionVariableTimeDuration"].ToString();
                string[] timeValues = strDuration.Split(new char[] { ':' });
                int setHrs = int.Parse(timeValues[0].ToString());
                int setMin = int.Parse(timeValues[1].ToString());
                int setSec = int.Parse(timeValues[2].ToString());

                TimeSpan tsDuration = new TimeSpan(setHrs, setMin, setSec);//25-02-2010 bip
                bool timeExpired = false;
                if (tsNow > tsDuration)
                    timeExpired = true;
                // return true;
                TimeSpan tsTimeRemains = tsDuration.Subtract(tsNow);

                // 03-02-2010 bip
                int firstvariableid = 0;
                if (Session["CurrentTestFirstVariableId"] != null)
                    firstvariableid = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
                DataTable dtTimeDetails = new DataTable();
                if (Session["FirstvarTimeDet"] == null)
                {
                    dtTimeDetails.Columns.Add("FirstVarID");
                    dtTimeDetails.Columns.Add("TimeUsed");
                    dtTimeDetails.Columns.Add("CompletionStatus");
                    dtTimeDetails.Columns.Add("AssignTime");
                    dtTimeDetails.Columns.Add("AssignStatus");
                }
                else
                {
                    dtTimeDetails = (DataTable)Session["FirstvarTimeDet"];
                }
                bool valueexists = false;
                for (int i = 0; i < dtTimeDetails.Rows.Count; i++)
                {
                    int firstvarid = 0;
                    string competionstatus = "", assigntime = "", timeused = "", assignStatus = "";
                    if (dtTimeDetails.Rows[i]["FirstVarID"] != null)
                        firstvarid = int.Parse(dtTimeDetails.Rows[i]["FirstVarID"].ToString());
                    if (dtTimeDetails.Rows[i]["TimeUsed"] != null)
                        timeused = dtTimeDetails.Rows[i]["TimeUsed"].ToString();
                    if (dtTimeDetails.Rows[i]["CompletionStatus"] != null)
                        competionstatus = dtTimeDetails.Rows[i]["CompletionStatus"].ToString();
                    if (dtTimeDetails.Rows[i]["AssignTime"] != null)
                        assigntime = dtTimeDetails.Rows[i]["AssignTime"].ToString();
                    if (dtTimeDetails.Rows[i]["AssignStatus"] != null)
                        assignStatus = dtTimeDetails.Rows[i]["AssignStatus"].ToString();

                    if (firstvariableid == firstvarid)
                    {
                        valueexists = true;

                        if (dtTimeDetails.Rows[i]["CompletionStatus"].ToString() == "1")
                            return true;

                        dtTimeDetails.Rows[i]["TimeUsed"] = hrs + ":" + min + ":" + sec;
                        if (timeExpired == true)
                        {
                            dtTimeDetails.Rows[i]["CompletionStatus"] = 1;
                            Session["FirstvarTimeDet"] = dtTimeDetails;
                            return true;
                        }
                        Session["FirstvarTimeDet"] = dtTimeDetails;
                        break;
                    }
                }
                if (valueexists == false)
                {
                    // code to assign values from here
                    DataRow drTimedet = dtTimeDetails.NewRow();
                    drTimedet["FirstVarID"] = firstvariableid;
                    drTimedet["CompletionStatus"] = 0;
                    drTimedet["TimeUsed"] = hrs + ":" + min + ":" + sec;
                    drTimedet["AssignTime"] = setHrs + ":" + setMin + ":" + 0;                    
                    drTimedet["AssignStatus"] = 1;

                    if (timeExpired == true)
                    {
                        drTimedet["CompletionStatus"] = 1;
                        dtTimeDetails.Rows.Add(drTimedet);
                        Session["FirstvarTimeDet"] = dtTimeDetails;
                        return true;
                    }
                    drTimedet["CompletionStatus"] = 0;
                    dtTimeDetails.Rows.Add(drTimedet);
                    Session["FirstvarTimeDet"] = dtTimeDetails;

                    //dtTimeDetails.Rows.Add(drTimedet);
                }
                ////

            }
            else return CheckTestSecVarGenValidity();

        }
        //else Session["AudioStatrtTime"] = DateTime.Now;

        return false;
    }
    private Boolean CheckTestSecVarGenValidity()
    {
       // if (Session["TestSectionVariableTimeDuration"] == null)
            if (Session["TotalTimeForUnAssgnSecVariables"] != null)
            {
                string[] totaltimeforUnAssgn = Session["TotalTimeForUnAssgnSecVariables"].ToString().Split(new char[] { ':' });
                int totalHrsforUnAssgn = int.Parse(totaltimeforUnAssgn[0]);
                int totalMinforUnAssgn = int.Parse(totaltimeforUnAssgn[1]);
                int totalSecforUnAssgn = int.Parse(totaltimeforUnAssgn[2]);

                TimeSpan tsUnAssgn = new TimeSpan(totalHrsforUnAssgn, totalMinforUnAssgn, totalSecforUnAssgn);
                int genHrs = 0, genMin = 0, genSec = 0;
                if (Session["GeneralVariableTimeUsed"] != null)
                {
                    string[] genTimeUsed = Session["GeneralVariableTimeUsed"].ToString().Split(new char[] { ':' });
                    genHrs = int.Parse(genTimeUsed[0]);
                    genMin = int.Parse(genTimeUsed[1]);
                    genSec = int.Parse(genTimeUsed[2]);
                    TimeSpan tsGeneral = new TimeSpan(genHrs, genMin, genSec);

                    if (tsGeneral > tsUnAssgn)
                        return true;
                }

                //

                if (Session["TestSectionVariableStartTime"] != null)
                {
                    DateTime dtStartTime = (DateTime)Session["TestSectionVariableStartTime"];
                    DateTime dtendTime = DateTime.Now;
                    TimeSpan tsUsed = dtendTime.Subtract(dtStartTime);

                    genHrs += tsUsed.Hours;
                    genMin += tsUsed.Minutes;
                    genSec += tsUsed.Seconds;

                    if (genSec >= 60)
                    {
                        double getMin = float.Parse(genSec.ToString()) / 60;
                        string[] newMin = getMin.ToString().Split(new char[] { '.' });
                        if (newMin.Length > 1)
                        {
                            int newminutes = int.Parse(newMin[0]);
                            genMin += newminutes;

                            float newSec = float.Parse(getMin.ToString().Substring(1)) * 60;
                            string[] newSeconds = newSec.ToString().Split(new char[] { '.' });
                            int seconds = int.Parse(newSeconds[0]);
                            genSec = seconds;
                        }
                    }

                    if (genMin >= 60)
                    {
                        double getHrs = float.Parse(genMin.ToString()) / 60;
                        string[] newHrs = getHrs.ToString().Split(new char[] { '.' });
                        if (newHrs.Length > 1)
                        {
                            int newhours = int.Parse(newHrs[0]);
                            genHrs += newhours;

                            float newMin = float.Parse(getHrs.ToString().Substring(1)) * 60;
                            string[] newMinutes = newMin.ToString().Split(new char[] { '.' });
                            int minutes = int.Parse(newMinutes[0]);
                            genMin = minutes;
                        }
                    }
                    Session["GeneralVariableTimeUsed"] = genHrs + ":" + genMin + ":" + genSec;
                    bool timeExpired = false;
                    if (tsUsed > tsUnAssgn)
                        timeExpired = true;
                       // return true;

                   // return false;// 03-02-2010 bip

                    TimeSpan tsTimeRemains = tsUnAssgn.Subtract(tsUsed);

                    // 03-02-2010 bip
                    int firstvariableid = 0;
                    if (Session["CurrentTestFirstVariableId"] != null)
                        firstvariableid = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
                    DataTable dtTimeDetails = new DataTable();
                    if (Session["FirstvarTimeDet"] == null)
                    {
                        dtTimeDetails.Columns.Add("FirstVarID");
                        dtTimeDetails.Columns.Add("TimeUsed");
                        dtTimeDetails.Columns.Add("CompletionStatus");
                        dtTimeDetails.Columns.Add("AssignTime");
                        dtTimeDetails.Columns.Add("AssignStatus");
                    }
                    else
                    {
                        dtTimeDetails = (DataTable)Session["FirstvarTimeDet"];
                    }
                    bool valueexists = false;
                    for (int i = 0; i < dtTimeDetails.Rows.Count; i++)
                    {
                        int firstvarid = 0;
                        string competionstatus = "", remainingtime = "", timeused = "";
                        if (dtTimeDetails.Rows[i]["FirstVarID"] != null)
                            firstvarid = int.Parse(dtTimeDetails.Rows[i]["FirstVarID"].ToString());
                        if (dtTimeDetails.Rows[i]["TimeUsed"] != null)
                            timeused = dtTimeDetails.Rows[i]["TimeUsed"].ToString();
                        if (dtTimeDetails.Rows[i]["CompletionStatus"] != null)
                            competionstatus = dtTimeDetails.Rows[i]["CompletionStatus"].ToString();
                        if (dtTimeDetails.Rows[i]["AssignTime"] != null)
                            remainingtime = dtTimeDetails.Rows[i]["AssignTime"].ToString();
                        if (dtTimeDetails.Rows[i]["AssignStatus"] != null)
                            remainingtime = dtTimeDetails.Rows[i]["AssignStatus"].ToString();

                        if (firstvariableid == firstvarid)
                        {
                            valueexists = true;

                            dtTimeDetails.Rows[i]["TimeUsed"] = genHrs + ":" + genMin + ":" + genSec;

                            if (timeExpired == true)
                            {
                                dtTimeDetails.Rows[i]["CompletionStatus"] = 1;
                                Session["FirstvarTimeDet"] = dtTimeDetails;
                                return true;
                            }
                            Session["FirstvarTimeDet"] = dtTimeDetails;
                            break;
                        }
                    }
                    if (valueexists == false)
                    {
                        // code to assign values from here
                        DataRow drTimedet = dtTimeDetails.NewRow();
                        drTimedet["FirstVarID"] = firstvariableid;
                        //drTimedet["CompletionStatus"] = 0;
                        drTimedet["TimeUsed"] = genHrs + ":" + genMin + ":" + genSec;
                        drTimedet["AssignTime"] = totalHrsforUnAssgn + ":" + totalMinforUnAssgn + ":" + totalSecforUnAssgn;
                        drTimedet["AssignStatus"] = 0;
                       // dtTimeDetails.Rows.Add(drTimedet);

                        if (timeExpired == true)
                        {
                            drTimedet["CompletionStatus"] = 1;
                            dtTimeDetails.Rows.Add(drTimedet);
                            Session["FirstvarTimeDet"] = dtTimeDetails;
                            return true;
                        }
                        drTimedet["CompletionStatus"] = 0;
                        dtTimeDetails.Rows.Add(drTimedet);
                        Session["FirstvarTimeDet"] = dtTimeDetails;
                    }
                    ////
                }
            }
        return false;
    }

    private Boolean CheckTimeValidity()
    {
        if (Session["TestSectionStartTime"] != null)
        {
            if (Session["TestSectionTimeDuration"] != null)
            {
                DateTime dtStart = (DateTime)Session["TestSectionStartTime"];
                DateTime dtNow = DateTime.Now;
                TimeSpan tsNow = dtNow.Subtract(dtStart);
                int hrs = tsNow.Hours;
                int min = tsNow.Minutes;

                string strDuration = Session["TestSectionTimeDuration"].ToString();
                string[] timeValues = strDuration.Split(new char[] { ':' });
                int setHrs = int.Parse(timeValues[0].ToString());
                int setMin = int.Parse(timeValues[1].ToString());

                TimeSpan tsDuration = new TimeSpan(setHrs, setMin, 0);//25-02-2010 bip
                if ( tsNow>tsDuration )
                    return true;
            }
            else return CheckTestSecGenValidity();

        }
        //else Session["AudioStatrtTime"] = DateTime.Now;

        return false;
    }
    private Boolean CheckTestSecGenValidity()
    {
        if (Session["TestSectionTimeDuration"] == null)
            if (Session["TotalTimeForUnAssgnSections"] != null)
            {
                string[] totaltimeforUnAssgn = Session["TotalTimeForUnAssgnSections"].ToString().Split(new char[] { ':' });
                int totalHrsforUnAssgn = int.Parse(totaltimeforUnAssgn[0]);
                int totalMinforUnAssgn = int.Parse(totaltimeforUnAssgn[1]);

                TimeSpan tsUnAssgn = new TimeSpan(totalHrsforUnAssgn, totalMinforUnAssgn, 0);
                int genHrs = 0, genMin = 0;
                if (Session["GeneralTimeUsed"] != null)
                {
                    string[] genTimeUsed = Session["GeneralTimeUsed"].ToString().Split(new char[] { ':' });
                    genHrs = int.Parse(genTimeUsed[0]);
                    genMin = int.Parse(genTimeUsed[1]);
                    TimeSpan tsGeneral = new TimeSpan(genHrs, genMin, 0);
                    if (tsGeneral > tsUnAssgn)
                        return true;
                }

                //

                if (Session["TestSectionStartTime"] != null)
                {
                    DateTime dtStartTime = (DateTime)Session["TestSectionStartTime"];
                    DateTime dtendTime = DateTime.Now;
                    TimeSpan tsUsed = dtendTime.Subtract(dtStartTime);

                    genHrs += tsUsed.Hours;
                    genMin += tsUsed.Minutes;

                    if (genMin >= 60)
                    {
                        double getHrs = float.Parse(genMin.ToString()) / 60;
                        string[] newHrs = getHrs.ToString().Split(new char[] { '.' });
                        if (newHrs.Length > 1)
                        {
                            int newhours = int.Parse(newHrs[0]);
                            genHrs += newhours;

                            float newMin = float.Parse(getHrs.ToString().Substring(1)) * 60;
                            string[] newMinutes = newMin.ToString().Split(new char[] { '.' });
                            int minutes = int.Parse(newMinutes[0]);
                            genMin = minutes;
                        }
                    }
                    Session["GeneralTimeUsed"] = genHrs + ":" + genMin;
                    if (tsUsed > tsUnAssgn)
                        return true;
                }
            }
        return false;
    }
    private Boolean CheckTestTimeValidity()
    {
        if (Session["TestStartTime"] != null)
        {
            if (Session["TestTimeDuration"] != null)
            {
                DateTime dtTestStart = (DateTime)Session["TestStartTime"];
                string strTestDuration = Session["TestTimeDuration"].ToString();
                DateTime dtNow = DateTime.Now;
                TimeSpan tsNow = dtNow.Subtract(dtTestStart);
                int hrs = tsNow.Hours;
                int min = tsNow.Minutes;

                string[] timeValues = strTestDuration.Split(new char[] { ':' });
                int setHrs = int.Parse(timeValues[0].ToString());
                int setMin = int.Parse(timeValues[1].ToString());

                TimeSpan tsDuration = new TimeSpan(setHrs, setMin, 0);//25-02-2010 bip
                if ( tsNow>tsDuration )
                    return true;
            }
        }

        return false;
    }
    private void SetNextSectionDetails_Timer()
    {
        int testsectionid = 0;
        if (Session["CurrentTestSectionId"] != null)
        {
            testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());
            dataclass.Procedure_UserTestSectionDetails(0, userid, 0, testId, testsectionid);
        }

        int sectionIdIndexNo = 0;
        if (Session["sectionIdIndexNo"] != null)
            sectionIdIndexNo = int.Parse(Session["sectionIdIndexNo"].ToString());
        sectionIdIndexNo = sectionIdIndexNo + 1;
        Session["sectionIdIndexNo"] = sectionIdIndexNo.ToString();

        ShowNextControl_Timer(0);

    }
    private void SetNextSectionVariableDetails_Timer()// 24-02-2010 bip
    {
        
        // 27-02-2010 bip
        int firstvaridfortimer = 0, varidindex_timer = 0;
        if (Session["FirstVariableIdForTimer"] != null)
        {
            firstvaridfortimer = int.Parse(Session["FirstVariableIdForTimer"].ToString());
            firstvaridfortimer += 1;
            Session["FirstVariableIdForTimer"] = firstvaridfortimer;

            /// bip 12052010
            varidindex_timer = int.Parse(Session["VariableIdIndexNo_timer"].ToString());
            Session["VariableIdIndexNo_timer"] = varidindex_timer + 1;
            ///
        }

        ShowNextControl_Timer(1);

    }
    private void ShowNextControl_Timer(int index)// 24-02-2010 bip
    {
        Session["TestSectionVariableTimeDuration"] = null;// 24-02-2010 bip

        Session["AudioPrevious"] = null; Session["AudioCurrentpage"] = null;
        Session["VideoPrevious"] = null; Session["VideoCurrentpage"] = null;
        Session["MemWordPrevious"] = null; Session["MemWordCurrentpage"] = null;
        if (index == 0)// 24-02-2010 bip
        {
            Session["TestSectionTimeDuration"] = null;// to be added to each ctrl 22-02-2010 bip
            Session["VariableIdIndexNo"] = null;
            Session["dsTestVariableIds"] = null;
            Session["VariableIdIndexNo_timer"] = null;// bip 12052010

        }
        Session["questionColl_audio"] = null;
        Session["questionColl_fill"] = null;
        Session["questionColl_img"] = null;
        Session["questionColl_memImages"] = null;
        Session["questionColl"] = null;
        Session["questionColl_Rating"] = null;
        Session["questionColl_video"] = null;
        Session["questionColl_memWords"] = null;
        Session["questionColl_imgPhoto"] = null;

        Session["totalQuesCount_audio"] = null; Session["totalQuesAvailable_audio"] = null;
        Session["totalQuesCount_fill"] = null; Session["totalQuesAvailable_fill"] = null;
        Session["totalQuesCount_img"] = null; Session["totalQuesAvailable_img"] = null;
        Session["totalQuesCount_memImages"] = null; Session["totalQuesAvailable_memImages"] = null;
        Session["totalQuesCount"] = null; Session["totalQuesAvailable"] = null;
        Session["totalQuesCount_Rating"] = null; Session["totalQuesAvailable_Rating"] = null;
        Session["totalQuesCount_video"] = null; Session["totalQuesAvailable_video"] = null;
        Session["totalQuesCount_memWords"] = null; Session["totalQuesAvailable_memWords"] = null;

        Session["totalQuesCount_imgPhoto"] = null; Session["totalQuesAvailable_imgPhoto"] = null;
        
        Session["SubCtrl"] = "ObjectiveQuestns.ascx";
        Response.Redirect("FJAHome.aspx");

        ////FillQuestion();

    }

    protected void btnYes_timer_Click(object sender, EventArgs e)
    {
        Session["timeExpired"] = null; Session["saved"] = null;
        //SaveAnswers();// bip 07052010
        SetNextSectionDetails_Timer();
    }
    protected void btnNo_timer_Click(object sender, EventArgs e)
    {
        Session["timeExpired"] = null; Session["saved"] = null;
        //SaveAnswers();// bip 07052010
        //// 230110 bip        
        dataclass.Procedure_DeleteUserTest_TempValues(userid, 0, 0);
        ////
        Session.Clear();
        Response.Redirect("FJAHome.aspx"); //bipson 18082010// Response.Redirect("CareerJudge.htm");;
    }
    protected void btnYes_timer_Test_Click(object sender, EventArgs e)
    {
        Session["timeExpired"] = null; Session["saved"] = null;
       // SaveAnswers();// bip 07052010
        //// 230110 bip        
        dataclass.Procedure_DeleteUserTest_TempValues(userid, 0, 0);
        ////
        Session.Clear();
        Response.Redirect("FJAHome.aspx"); //bipson 18082010// Response.Redirect("CareerJudge.htm");;
    }
    protected void btnNo_timer_Test_Click(object sender, EventArgs e)
    {

    }
    protected void btnYes_timer_TestVariable_Click(object sender, EventArgs e)
    {
        Session["timeExpired"] = null; Session["saved"] = null;
       // SaveAnswers();// bip 07052010
        SetNextSectionVariableDetails_Timer();
    }
   
    protected void Timer1_Tick(object sender, EventArgs e)
    {        
        ///bip 10052010
        if (Session["timeExpired"] != null)
            if (Session["timeExpired"].ToString() == "True")
                return;

        CheckTime();// 
       // SaveAnswers();
        ///

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Timer1.Enabled = true;// 26-10-2010 bip
    }
}
