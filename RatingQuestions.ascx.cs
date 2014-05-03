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

public partial class RatingQuestions : System.Web.UI.UserControl
{
    string usercode = "";
    int userid = 0;
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
    int quesperPage = 10;
    int testId = 0; int testsectionid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Load();
        }
        catch (Exception ex) 
        {
            if (Session["questionColl"] == null)
            {
                if (Session["evaldirection"].ToString() == "Next")
                {
                    Session["SubCtrl"] = "ObjectiveQuestns.ascx";
                    Response.Redirect("FJAHome.aspx");
                }
            }
           // Load();  
        }//lblmessage.Text = ex.Message; }
    }
    private void Load()
    {
        /// bip 10052010
        if (Session["timeExpired"] != null)
            if (Session["timeExpired"].ToString() == "True")
            {
                CheckTime();
                return;
            }
        ///
        // if (CheckTime() == true) return;//bip 15052010
        //
        int testSecondVariableId = 0, testFirstVariableId = 0, testSectionID = 0;
        if (Session["CurrentTestSectionId"] != null)
            testSectionID = int.Parse(Session["CurrentTestSectionId"].ToString());

        if (Session["CurrentTestSecondVariableId"] != null)
            testSecondVariableId = int.Parse(Session["CurrentTestSecondVariableId"].ToString());
        if (Session["CurrentTestFirstVariableId"] != null)
            testFirstVariableId = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
        //


        if (Session["UserTestId"] != null)
        {
            // FillTestSectionInstructions();
            testId = int.Parse(Session["UserTestId"].ToString());
            if (Session["UserID"] != null)
                userid = int.Parse(Session["UserID"].ToString());

            FillQuestion();
        }
    }
    private void FillTestSectionInstructions()
    {
        int testSectionID = 0;
        if (Session["CurrentTestSectionId"] != null)
            testSectionID = int.Parse(Session["CurrentTestSectionId"].ToString());
        bool valueexists = false;
        var InstructionDetails = from instructiondet in dataclass.TestSectionInstructions
                                 where instructiondet.CategoryId == 3 && instructiondet.TestId == testId && instructiondet.TestSectionId == testSectionID
                                 select instructiondet;
        if (InstructionDetails.Count() > 0)
        {
            if (InstructionDetails.First().InstructionDetails != null)
            {
                divInstructions.InnerHtml = InstructionDetails.First().InstructionDetails.ToString();
                valueexists = true;
            }
        }
        if (valueexists == false)
            FillCommonInstructions();
        else FillTitle();
    }
    private void FillTitle()
    {
        var InstructionDetails = from instructiondet in dataclass.TestInstructions
                                 where instructiondet.CategoryId == 3
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
                                 where instructiondet.CategoryId == 3
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
        //try
        //{
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
            dtQuestionList.Columns.Add("Option6");
            dtQuestionList.Columns.Add("Option7");
            dtQuestionList.Columns.Add("Option8");
            dtQuestionList.Columns.Add("Option9");
            dtQuestionList.Columns.Add("Option10");
            dtQuestionList.Columns.Add("ScoringStyle");
            DataRow drQurstionList;

            // bip 08122009;

            querystring = "select distinct RatingQuestionCount,SectionId from QuestionCount where RatingQuestionCount>0 and TestId=" + testId + " and TestSectionId=" + testsectionid;// + " and SectionNameSub1='" + secondVariableName + "'";

            //lblmessage.Text = querystring;//bipson 11092010

            DataSet dsQuestioncount = new DataSet();
            dsQuestioncount = clsclass.GetValuesFromDB(querystring);
            if (dsQuestioncount != null)
                if (dsQuestioncount.Tables.Count > 0)
                    if (dsQuestioncount.Tables[0].Rows.Count > 0)
                    {

                        for (int c = 0; c < dsQuestioncount.Tables[0].Rows.Count; c++)//                   
                        {
                            int ratingtypeQuescount = int.Parse(dsQuestioncount.Tables[0].Rows[c]["RatingQuestionCount"].ToString());
                            int sectionid = int.Parse(dsQuestioncount.Tables[0].Rows[c]["SectionId"].ToString());

                            if (ratingtypeQuescount > 0)
                            {
                                querystring = "SELECT TOP (" + ratingtypeQuescount + ") QuestionID,Question,Answer,Option1,Option2,Option3,Option4,Option5,Option6,Option7,Option8,Option9,Option10,ScoringStyle  FROM View_TestBaseQuestionList where Category = 'RatingType' AND TestBaseQuestionStatus=1 AND TestSectionId=" + testsectionid + " AND SectionId=" + sectionid + " AND TestId=" + testId + " ORDER BY RAND((100*QuestionID)*DATEPART(millisecond, GETDATE())) ";

                                dsQuesCount = new DataSet();
                                dsQuesCount = clsclass.GetValuesFromDB(querystring);
                                if (dsQuesCount != null)
                                    if (dsQuesCount.Tables[0].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dsQuesCount.Tables[0].Rows.Count; i++)
                                        {
                                            dataclass.Procedure_TestBaseQuestionList_Temp(int.Parse(dsQuesCount.Tables[0].Rows[i]["QuestionID"].ToString()), dsQuesCount.Tables[0].Rows[i]["Question"].ToString(), dsQuesCount.Tables[0].Rows[i]["Answer"].ToString(),
                                            dsQuesCount.Tables[0].Rows[i]["Option1"].ToString(), dsQuesCount.Tables[0].Rows[i]["Option2"].ToString(), dsQuesCount.Tables[0].Rows[i]["Option3"].ToString(), dsQuesCount.Tables[0].Rows[i]["Option4"].ToString(), dsQuesCount.Tables[0].Rows[i]["Option5"].ToString(),
                                            dsQuesCount.Tables[0].Rows[i]["Option6"].ToString(), dsQuesCount.Tables[0].Rows[i]["Option7"].ToString(), dsQuesCount.Tables[0].Rows[i]["Option8"].ToString(), dsQuesCount.Tables[0].Rows[i]["Option9"].ToString(), dsQuesCount.Tables[0].Rows[i]["Option10"].ToString(), userid, dsQuesCount.Tables[0].Rows[i]["ScoringStyle"].ToString());
                                        }
                                    }
                            }
                        }
                        // lblmessage.Text += " totcountref..= " + totcount.ToString(); // bip 17-12-2009
                        // }
                        querystring = "SELECT  QuestionID,Question,Answer,Option1,Option2,Option3,Option4,Option5,Option6,Option7,Option8,Option9,Option10,ScoringStyle  FROM TestBaseQuestionList_Temp where userid=" + userid + " ORDER BY RAND((100*QuestionID)*DATEPART(millisecond, GETDATE())) ";
                        //lblmessage.Text = querystring; //return null;//bipson 01092010
                        dsQuesCount = new DataSet();
                        dsQuesCount = clsclass.GetValuesFromDB(querystring);
                        if (dsQuesCount != null)
                            if (dsQuesCount.Tables[0].Rows.Count > 0)
                            {
                                // lblmessage.Text += " secondcount= " + dsQuesCount.Tables[0].Rows.Count.ToString(); // bip 17-12-2009
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
                                    drQurstionList["Option6"] = dsQuesCount.Tables[0].Rows[i]["Option6"];
                                    drQurstionList["Option7"] = dsQuesCount.Tables[0].Rows[i]["Option7"];
                                    drQurstionList["Option8"] = dsQuesCount.Tables[0].Rows[i]["Option8"];
                                    drQurstionList["Option9"] = dsQuesCount.Tables[0].Rows[i]["Option9"];
                                    drQurstionList["Option10"] = dsQuesCount.Tables[0].Rows[i]["Option10"];
                                    drQurstionList["ScoringStyle"] = dsQuesCount.Tables[0].Rows[i]["ScoringStyle"];

                                    dtQuestionList.Rows.Add(drQurstionList);
                                }
                            }
                    }
            DataSet dsQuestionList = new DataSet();
            if (dtQuestionList.Rows.Count > 0)
            {
                dsQuestionList.Tables.Add(dtQuestionList); Session["questionColl_Rating"] = dsQuestionList;
                Session["totalQuesCount_Rating"] = dsQuestionList.Tables[0].Rows.Count.ToString();
                dataclass.Procedure_DeleteTestBaseQuestionList_Temp(userid);
            }
            else { Session["questionColl_Rating"] = null; dsQuestionList = null; }

            return dsQuestionList;
       // }
        //catch (Exception ex) { return null; }// GetQuestionList(testsectionid);
    }

    private string GetAnswerOptionOrder(int index)
    {
        string option = "";

        if (index == 1)
            option = "A";
        else if (index == 2)
            option = "B";
        else if (index == 3)
            option = "C";
        else if (index == 4)
            option = "D";
        else if (index == 5)
            option = "E";
        else if (index == 6)
            option = "F";
        else if (index == 7)
            option = "G";
        else if (index == 8)
            option = "H";
        else if (index == 9)
            option = "I";
        else if (index == 10)
            option = "J";

        return option;
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
        int questionid = 0; string questiontype = "RatingType";

        string querystringquesTemp = "select distinct QuestionId from UserTestQuestions_Temp where UserID=" + userid + " and TestId=" + testId + " and TestSectionId=" + testsectionid + "  and QuestionType='" + questiontype + "'";//and FirstVariableId=" + testFirstVariableId + "
        
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
                    
                    querystringquesTemp = "SELECT QuestionID,Question,Answer,Option1,Option2,Option3,Option4,Option5,Option6,Option7,Option8,Option9,Option10,ScoringStyle  FROM View_TestBaseQuestionList where " + questionids;
                    dsTempData = clsclass.GetValuesFromDB(querystringquesTemp);
                    Session["questionColl_Rating"] = dsTempData;

                    var GetPageIndex = from pageindex in dataclass.UserTestPageIndex_Temps
                                       where pageindex.UserId == userid && pageindex.TestId == testId && pageindex.TestSectionId == testsectionid &&
                                       pageindex.QuestionType == questiontype
                                       select pageindex;
                    if (GetPageIndex.Count() > 0)
                    {
                        if (GetPageIndex.First().PageIndex != null)
                        {
                            pagecount = int.Parse(GetPageIndex.First().PageIndex.ToString());
                            Session["pagecountRating"] = pagecount;                            
                        }
                    }
                }
                else dsTempData = null;

        return dsTempData;
    }
    private void FillQuestion()
    {
        //try
        //{
            pnlMessage.Visible = false;

            ClearControls();
            string querystring = "";
            //int totalQues = 0;
            int QuestionID = 0;
            
            if (Session["pagecountRating"] != null)
                pagecount = int.Parse(Session["pagecountRating"].ToString());            

            string connString = ConfigurationManager.ConnectionStrings["talentscoutConnectionString"].ConnectionString;
            
            bool secExists = false;
            ds = new DataSet();//int testsectionid = 0;
            if (Session["CurrentTestSectionId"] != null)
                testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());

            if (Session["CurRatingTestSecionId"] != null)
                if (Session["CurRatingTestSecionId"].ToString() == testsectionid.ToString())
                    secExists = true;

            Session["CurRatingTestSecionId"] = testsectionid;
            if (secExists == true)
            {               
                if (Session["curRatingQuesCompleted"] != null)
                    if (Session["curRatingQuesCompleted"].ToString() == "True")
                    {
                        string evaldirection = "Next";
                        if (Session["evaldirection"] != null)
                            evaldirection = Session["evaldirection"].ToString();
                        if (evaldirection == "Next")
                        {
                            GoToNextPage(); return;//14-05-2013                         
                        }
                        else GoToPreviousPage();return;// GoToNextPage(); return;
                    }
            }
            else Session["curRatingQuesCompleted"] = null;
            if (Session["questionColl_Rating"] != null)
            { ds = (DataSet)Session["questionColl_Rating"];} 
            else
            {
                //bip 081009
                // int testsectionid = 0;
                if (Session["CurrentTestSectionId"] != null)
                {
                    testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());


                    //  //// 220110 ... bip
                   
                    int questionid = 0; string questiontype = "PhotoType";

                    ds = GetTempData();
                    bool newentry = false;
                    if (ds == null)
                    {
                        newentry = true;
                        ds = GetQuestionList(testsectionid);
                    }
                    else if (ds.Tables.Count <= 0)
                    { newentry = true; ds = GetQuestionList(testsectionid); }

                    
                    if (ds == null)
                    {

                        //lblmessage.Text += "dsNULL=true";

                        string evaldirection = "Next";
                        if (Session["evaldirection"] != null)
                            evaldirection = Session["evaldirection"].ToString();
                        if (evaldirection == "Next")
                        {                           
                                GoToNextPage();                           
                        }
                        else GoToPreviousPage();
                        return;
                        
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
                                        dataclass.Procedure_UserTestQuestions_Temp(userid, testId, testsectionid, 0, 0, questionid, questiontype);
                                    }
                                }
                    }
                }
            }
            int slno = 0;
            slno = pagecount * quesperPage + 1;
            if (slno < 0)
                slno = 1;

            int pagecnt = 0;
            int curntquescnt = 0;
            int slnos = 0;
            ////lblmessage.Text += "test111= " + testsectionid;

           // return;//bipson 11092010

            if (ds.Tables[0].Rows.Count > 0)
            {

                FillTestSectionInstructions();

                Session["totalQuesAvailable_Rating"] = ds.Tables[0].Rows.Count.ToString();
                int j = 0;
                for (int i = slno - 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (j >= quesperPage) break;
                    Session["CurrentControlCtrl"] = "RatingQuestions.ascx";// bip 08012010
                    Session["ValueExists"] = "True";

                    string Answer = "";
                    int optindex = 0;// bip 05-06-2011 starting value is changed to 0 instead of 1
                    switch (j)
                    {
                        case 0:
                            // slnos = CheckSlNo();
                            lblNo1.Text = slno.ToString() + ".  "; lblNo1.Visible = true; 
                            lblNo1.Focus();// 29122009 bip
                            //lblQues1.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues1.InnerHtml= ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID1.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();
                            lblRatingType1.Text = ds.Tables[0].Rows[i]["ScoringStyle"].ToString();

                            var Ques1 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString()  && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;//Ques.UserCode == usercode &&
                            if (Ques1.Count() > 0)
                                if (Ques1.First().Answer != null)
                                    Answer = Ques1.First().Answer.ToString();
                            if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                            {
                                lblTotalOptions1.Text = optindex.ToString();
                                rbQues1Answer1.Visible = true; //bipson 12082010 lblA1.Visible = true;
                                rbQues1Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                if (optindex.ToString() == Answer)
                                    rbQues1Answer1.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                            {
                                lblTotalOptions1.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 2)  lblB1.Text = GetAnswerOptionOrder(optindex); lblB1.Visible = true;
                                rbQues1Answer2.Visible = true; 
                                rbQues1Answer2.Text= ds.Tables[0].Rows[i]["Option2"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues1Answer2.Checked = true;
                                optindex++; 

                            }
                            if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                            {
                                lblTotalOptions1.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 3) lblC1.Text = GetAnswerOptionOrder(optindex);lblC1.Visible = true;
                                rbQues1Answer3.Visible = true; 
                               rbQues1Answer3.Text= ds.Tables[0].Rows[i]["Option3"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues1Answer3.Checked = true;
                                optindex++; 
                            }

                            if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                            {
                                lblTotalOptions1.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 4) lblD1.Text = GetAnswerOptionOrder(optindex);lblD1.Visible = true;
                                rbQues1Answer4.Visible = true; 
                                rbQues1Answer4.Text= ds.Tables[0].Rows[i]["Option4"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues1Answer4.Checked = true;
                                optindex++; 
                            }

                            if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                            {
                                lblTotalOptions1.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 5)lblE1.Text = GetAnswerOptionOrder(optindex);lblE1.Visible = true;
                                    
                                rbQues1Answer5.Visible = true; 
                                rbQues1Answer5.Text= ds.Tables[0].Rows[i]["Option5"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues1Answer5.Checked = true;
                                optindex++; 
                            }

                            //
                            if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                            {
                                lblTotalOptions1.Text = optindex.ToString();
                               // if (optindex != 6)lblF1.Text = GetAnswerOptionOrder(optindex);lblF1.Visible = true;
                                    
                                rbQues1Answer6.Visible = true; 
                                rbQues1Answer6.Text= ds.Tables[0].Rows[i]["Option6"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues1Answer6.Checked = true;
                                optindex++; 
                            }
                            if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                            {
                                lblTotalOptions1.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 7)lblG1.Text = GetAnswerOptionOrder(optindex);lblG1.Visible = true;
                                    
                                rbQues1Answer7.Visible = true; 
                                rbQues1Answer7.Text= ds.Tables[0].Rows[i]["Option7"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues1Answer7.Checked = true;
                                optindex++; 
                            }
                            if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                            {
                                lblTotalOptions1.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 8)lblH1.Text = GetAnswerOptionOrder(optindex);lblH1.Visible = true;
                                    
                                rbQues1Answer8.Visible = true; 
                                rbQues1Answer8.Text= ds.Tables[0].Rows[i]["Option8"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues1Answer8.Checked = true;
                                optindex++; 
                            }
                            if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                            {
                                lblTotalOptions1.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 9)lblI1.Text = GetAnswerOptionOrder(optindex);lblI1.Visible = true;
                                    
                                rbQues1Answer9.Visible = true; 
                               rbQues1Answer9.Text= ds.Tables[0].Rows[i]["Option9"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues1Answer9.Checked = true;
                                optindex++; 
                            }
                            if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                            {
                                lblTotalOptions1.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 10)lblJ1.Text = GetAnswerOptionOrder(optindex);lblJ1.Visible = true;
                                    
                                rbQues1Answer10.Visible = true; 
                                rbQues1Answer10.Text = ds.Tables[0].Rows[i]["Option10"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues1Answer10.Checked = true;
                            }
                            //

                            curntquescnt++;
                            j++;
                            pnlQuestion1.Visible = true;
                            break;

                        case 1:
                            //optindex = 1;
                            lblNo2.Text = (slno + 1).ToString() + ".  "; lblNo2.Visible = true;
                            //lblQues2.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues2.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID2.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();
                            lblRatingType2.Text = ds.Tables[0].Rows[i]["ScoringStyle"].ToString();

                            var Ques2 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques2.Count() > 0)
                                if (Ques2.First().Answer != null)
                                    Answer = Ques2.First().Answer.ToString();
                            if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                            {
                                lblTotalOptions2.Text = optindex.ToString();
                                rbQues2Answer1.Visible = true; //bipson 12082010 lblA2.Visible = true;
                               rbQues2Answer1.Text= ds.Tables[0].Rows[i]["Option1"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues2Answer1.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                            {
                                lblTotalOptions2.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 2)lblB2.Text = GetAnswerOptionOrder(optindex);lblB2.Visible = true;
                                    
                                rbQues2Answer2.Visible = true; 
                               rbQues2Answer2.Text= ds.Tables[0].Rows[i]["Option2"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues2Answer2.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                            {
                                lblTotalOptions2.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 3)lblC2.Text = GetAnswerOptionOrder(optindex);lblC2.Visible = true;
                                    
                                rbQues2Answer3.Visible = true; 
                                rbQues2Answer3.Text= ds.Tables[0].Rows[i]["Option3"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues2Answer3.Checked = true;
                                optindex++;
                            }

                            //radioQues2.Items.Add(ds.Tables[0].Rows[i]["Option3"].ToString());
                            if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                            {
                                lblTotalOptions2.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 4)lblD2.Text = GetAnswerOptionOrder(optindex);lblD2.Visible = true;
                                    
                                rbQues2Answer4.Visible = true; 
                                rbQues2Answer4.Text= ds.Tables[0].Rows[i]["Option4"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues2Answer4.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                            {
                                lblTotalOptions2.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 5)lblE2.Text = GetAnswerOptionOrder(optindex);lblE2.Visible = true;
                                    
                                rbQues2Answer5.Visible = true; 
                                rbQues2Answer5.Text= ds.Tables[0].Rows[i]["Option5"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues2Answer5.Checked = true;
                                optindex++;
                            }

                            //
                            if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                            {
                                lblTotalOptions2.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 6)lblF2.Text = GetAnswerOptionOrder(optindex);lblF2.Visible = true;
                                    
                                rbQues2Answer6.Visible = true; 
                                rbQues2Answer6.Text= ds.Tables[0].Rows[i]["Option6"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues2Answer6.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                            {
                                lblTotalOptions2.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 7)lblG2.Text = GetAnswerOptionOrder(optindex);lblG2.Visible = true;
                                    
                                rbQues2Answer7.Visible = true; 
                                rbQues2Answer7.Text= ds.Tables[0].Rows[i]["Option7"].ToString();
                              //  if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues2Answer7.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                            {
                                lblTotalOptions2.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 8)lblH2.Text = GetAnswerOptionOrder(optindex);lblH2.Visible = true;
                                    
                                rbQues2Answer8.Visible = true; 
                               rbQues2Answer8.Text= ds.Tables[0].Rows[i]["Option8"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues2Answer8.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                            {
                                lblTotalOptions2.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 9)lblI2.Text = GetAnswerOptionOrder(optindex);lblI2.Visible = true;
                                    
                                rbQues2Answer9.Visible = true; 
                               rbQues2Answer9.Text= ds.Tables[0].Rows[i]["Option9"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues2Answer9.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                            {
                                lblTotalOptions2.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 10)lblJ2.Text = GetAnswerOptionOrder(optindex);lblJ2.Visible = true;
                                    
                                rbQues2Answer10.Visible = true; 
                                rbQues2Answer10.Text= ds.Tables[0].Rows[i]["Option10"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues2Answer10.Checked = true;
                            }
                            //

                            curntquescnt++;
                            j++;
                            pnlQuestion2.Visible = true;
                            break;

                        case 2:
                            //optindex = 1;
                            lblNo3.Text = (slno + 2).ToString() + ".  "; lblNo3.Visible = true;
                            //lblQues3.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues3.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID3.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();
                            lblRatingType3.Text = ds.Tables[0].Rows[i]["ScoringStyle"].ToString();

                            var Ques3 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques3.Count() > 0)
                                if (Ques3.First().Answer != null)
                                    Answer = Ques3.First().Answer.ToString();
                            if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                            {
                                lblTotalOptions3.Text = optindex.ToString();
                                rbQues3Answer1.Visible = true; //bipson 12082010 lblA3.Visible = true;
                                rbQues3Answer1.Text= ds.Tables[0].Rows[i]["Option1"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues3Answer1.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                            {
                                lblTotalOptions3.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 2)lblB3.Text = GetAnswerOptionOrder(optindex);lblB3.Visible = true;
                                    
                                rbQues3Answer2.Visible = true; 
                                rbQues3Answer2.Text= ds.Tables[0].Rows[i]["Option2"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues3Answer2.Checked = true;
                                optindex++;
                            }

                            //radioQues3.Items.Add(ds.Tables[0].Rows[i]["Option2"].ToString());
                            if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                            {
                                lblTotalOptions3.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 3)lblC3.Text = GetAnswerOptionOrder(optindex);lblC3.Visible = true;
                                    
                                rbQues3Answer3.Visible = true; 
                                rbQues3Answer3.Text= ds.Tables[0].Rows[i]["Option3"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues3Answer3.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                            {
                                lblTotalOptions3.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 4)lblD3.Text = GetAnswerOptionOrder(optindex);lblD3.Visible = true;
                                    
                                rbQues3Answer4.Visible = true; 
                                rbQues3Answer4.Text= ds.Tables[0].Rows[i]["Option4"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues3Answer4.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                            {
                                lblTotalOptions3.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 5)lblE3.Text = GetAnswerOptionOrder(optindex);lblE3.Visible = true;
                                    
                                rbQues3Answer5.Visible = true; 
                                rbQues3Answer5.Text= ds.Tables[0].Rows[i]["Option5"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues3Answer5.Checked = true;
                                optindex++;
                            }

                            //
                            if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                            {
                                lblTotalOptions3.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 6)lblF3.Text = GetAnswerOptionOrder(optindex);lblF3.Visible = true;
                                    
                                rbQues3Answer6.Visible = true; 
                                rbQues3Answer6.Text= ds.Tables[0].Rows[i]["Option6"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues3Answer6.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                            {
                                lblTotalOptions3.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 7)lblG3.Text = GetAnswerOptionOrder(optindex);lblG3.Visible = true;
                                    
                                rbQues3Answer7.Visible = true; 
                                rbQues3Answer7.Text= ds.Tables[0].Rows[i]["Option7"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues3Answer7.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                            {
                                lblTotalOptions3.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 8)lblH3.Text = GetAnswerOptionOrder(optindex);lblH3.Visible = true;
                                    
                                rbQues3Answer8.Visible = true; 
                                rbQues3Answer8.Text= ds.Tables[0].Rows[i]["Option8"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues3Answer8.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                            {
                                lblTotalOptions3.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 9)lblI3.Text = GetAnswerOptionOrder(optindex);lblI3.Visible = true;
                                    
                                rbQues3Answer9.Visible = true; 
                                 rbQues3Answer9.Text= ds.Tables[0].Rows[i]["Option9"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                 if (optindex.ToString() == Answer) rbQues3Answer9.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                            {
                                lblTotalOptions3.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 10)lblJ3.Text = GetAnswerOptionOrder(optindex);lblJ3.Visible = true;
                                    
                                rbQues3Answer10.Visible = true; 
                                rbQues3Answer10.Text= ds.Tables[0].Rows[i]["Option10"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues3Answer10.Checked = true;
                            }
                            //


                            curntquescnt++;
                            j++;
                            pnlQuestion3.Visible = true;
                            break;

                        case 3:
                            //optindex = 1;
                            lblNo4.Text = (slno + 3).ToString() + ".  "; lblNo4.Visible = true;
                            //lblQues4.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues4.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID4.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();
                            lblRatingType4.Text = ds.Tables[0].Rows[i]["ScoringStyle"].ToString();

                            var Ques4 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques4.Count() > 0)
                                if (Ques4.First().Answer != null)
                                    Answer = Ques4.First().Answer.ToString();

                            if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                            {
                                lblTotalOptions4.Text = optindex.ToString();
                                rbQues4Answer1.Visible = true; //bipson 12082010 lblA4.Visible = true;
                                rbQues4Answer1.Text= ds.Tables[0].Rows[i]["Option1"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues4Answer1.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                            //radioQues4.Items.Add(ds.Tables[0].Rows[i]["Option1"].ToString());
                            {
                                lblTotalOptions4.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 2)lblB4.Text = GetAnswerOptionOrder(optindex);lblB4.Visible = true;
                                    
                                rbQues4Answer2.Visible = true; 
                                rbQues4Answer2.Text= ds.Tables[0].Rows[i]["Option2"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues4Answer2.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                            {
                                lblTotalOptions4.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 3)lblC4.Text = GetAnswerOptionOrder(optindex);lblC4.Visible = true;
                                    
                                rbQues4Answer3.Visible = true; 
                                rbQues4Answer3.Text= ds.Tables[0].Rows[i]["Option3"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues4Answer3.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                            {
                                lblTotalOptions4.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 4)lblD4.Text = GetAnswerOptionOrder(optindex);lblD4.Visible = true;
                                    
                                rbQues4Answer4.Visible = true; 
                                rbQues4Answer4.Text= ds.Tables[0].Rows[i]["Option4"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues4Answer4.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                            {
                                lblTotalOptions4.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 5)lblE4.Text = GetAnswerOptionOrder(optindex);lblE4.Visible = true;
                                    
                                rbQues4Answer5.Visible = true; 
                                rbQues4Answer5.Text= ds.Tables[0].Rows[i]["Option5"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues4Answer5.Checked = true;
                                optindex++;
                            }

                            //
                            if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                            {
                                lblTotalOptions4.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 6)lblF4.Text = GetAnswerOptionOrder(optindex);lblF4.Visible = true;
                                    
                                rbQues4Answer6.Visible = true; 
                               rbQues4Answer6.Text= ds.Tables[0].Rows[i]["Option6"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues4Answer6.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                            {
                                lblTotalOptions4.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 7)lblG4.Text = GetAnswerOptionOrder(optindex);lblG4.Visible = true;
                                    
                                rbQues4Answer7.Visible = true; 
                               rbQues4Answer7.Text= ds.Tables[0].Rows[i]["Option7"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues4Answer7.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                            {
                                lblTotalOptions4.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 8)lblH4.Text = GetAnswerOptionOrder(optindex);lblH4.Visible = true;
                                    
                                rbQues4Answer8.Visible = true; 
                                rbQues4Answer8.Text= ds.Tables[0].Rows[i]["Option8"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues4Answer8.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                            {
                                lblTotalOptions4.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 9)lblI4.Text = GetAnswerOptionOrder(optindex);lblI4.Visible = true;
                                    
                                rbQues4Answer9.Visible = true; 
                                rbQues4Answer9.Text= ds.Tables[0].Rows[i]["Option9"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues4Answer9.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                            {
                                lblTotalOptions4.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 10)lblJ4.Text = GetAnswerOptionOrder(optindex);lblJ4.Visible = true;
                                    
                                rbQues4Answer10.Visible = true; 
                                rbQues4Answer10.Text= ds.Tables[0].Rows[i]["Option10"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues4Answer10.Checked = true;
                            }
                            //

                            curntquescnt++;
                            j++;
                            pnlQuestion4.Visible = true;
                            break;

                        case 4:
                            //optindex = 1;
                            lblNo5.Text = (slno + 4).ToString() + ".  "; lblNo5.Visible = true;
                            //lblQues5.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues5.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID5.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();
                            lblRatingType5.Text = ds.Tables[0].Rows[i]["ScoringStyle"].ToString();

                            var Ques5 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques5.Count() > 0)
                                if (Ques5.First().Answer != null)
                                    Answer = Ques5.First().Answer.ToString();
                            if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                            {
                                lblTotalOptions5.Text = optindex.ToString();
                                rbQues5Answer1.Visible = true; //bipson 12082010 lblA5.Visible = true;
                                rbQues5Answer1.Text= ds.Tables[0].Rows[i]["Option1"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues5Answer1.Checked = true;
                                optindex++;
                            }

                            //radioQues5.Items.Add(ds.Tables[0].Rows[i]["Option1"].ToString());
                            if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                            {
                                lblTotalOptions5.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 2)lblB5.Text = GetAnswerOptionOrder(optindex);lblB5.Visible = true;
                                    
                                rbQues5Answer2.Visible = true; 
                                rbQues5Answer2.Text= ds.Tables[0].Rows[i]["Option2"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues5Answer2.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                            {
                                lblTotalOptions5.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 3)lblC5.Text = GetAnswerOptionOrder(optindex);lblC5.Visible = true;
                                    
                                rbQues5Answer3.Visible = true; 
                                rbQues5Answer3.Text= ds.Tables[0].Rows[i]["Option3"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues5Answer3.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                            {
                                lblTotalOptions5.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 4)lblD5.Text = GetAnswerOptionOrder(optindex);lblD5.Visible = true;
                                    
                                rbQues5Answer4.Visible = true; 
                                rbQues5Answer4.Text= ds.Tables[0].Rows[i]["Option4"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues5Answer4.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                            {
                                lblTotalOptions5.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 5)lblE5.Text = GetAnswerOptionOrder(optindex);lblE5.Visible = true;
                                    
                                rbQues5Answer5.Visible = true; 
                                rbQues5Answer5.Text= ds.Tables[0].Rows[i]["Option5"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues5Answer5.Checked = true;
                                optindex++;
                            }

                            //
                            if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                            {
                                lblTotalOptions5.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 6)lblF5.Text = GetAnswerOptionOrder(optindex);lblF5.Visible = true;
                                    
                                rbQues5Answer6.Visible = true; 
                                rbQues5Answer6.Text= ds.Tables[0].Rows[i]["Option6"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues5Answer6.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                            {
                                lblTotalOptions5.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 7) lblG5.Text = GetAnswerOptionOrder(optindex);lblG5.Visible = true;
                                   
                                rbQues5Answer7.Visible = true; 
                                rbQues5Answer7.Text= ds.Tables[0].Rows[i]["Option7"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues5Answer7.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                            {
                                lblTotalOptions5.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 8)lblH5.Text = GetAnswerOptionOrder(optindex);lblH5.Visible = true;
                                    
                                rbQues5Answer8.Visible = true; 
                                rbQues5Answer8.Text= ds.Tables[0].Rows[i]["Option8"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues5Answer8.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                            {
                                lblTotalOptions5.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 9)lblI5.Text = GetAnswerOptionOrder(optindex); lblJ5.Visible = true;
                                    
                                rbQues5Answer9.Visible = true;
                                rbQues5Answer9.Text= ds.Tables[0].Rows[i]["Option9"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues5Answer9.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                            {
                                lblTotalOptions5.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 10)lblJ5.Text = GetAnswerOptionOrder(optindex);lblJ5.Visible = true;
                                    
                                rbQues5Answer10.Visible = true; 
                                rbQues5Answer10.Text= ds.Tables[0].Rows[i]["Option10"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues5Answer10.Checked = true;                               
                            }
                            //

                            curntquescnt++;
                            j++;
                            pnlQuestion5.Visible = true;
                            break;
                        ///////////////


                        case 5:
                            //optindex = 1;
                            // slnos = CheckSlNo();
                            lblNo6.Text = (slno + 5).ToString() + ".  "; lblNo6.Visible = true;
                            //lblQues6.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues6.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID6.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();
                            lblRatingType6.Text = ds.Tables[0].Rows[i]["ScoringStyle"].ToString();

                            var Ques6 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques6.Count() > 0)
                                if (Ques6.First().Answer != null)
                                    Answer = Ques6.First().Answer.ToString();
                            if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                            {
                                lblTotalOptions6.Text = optindex.ToString();
                                rbQues6Answer1.Visible = true; //bipson 12082010 lblA6.Visible = true;
                                rbQues6Answer1.Text= ds.Tables[0].Rows[i]["Option1"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues6Answer1.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                            {
                                lblTotalOptions6.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 2)lblB6.Text = GetAnswerOptionOrder(optindex); lblB6.Visible = true;
                                    
                                rbQues6Answer2.Visible = true;
                                rbQues6Answer2.Text= ds.Tables[0].Rows[i]["Option2"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues6Answer2.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                            {
                                lblTotalOptions6.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 3)lblC6.Text = GetAnswerOptionOrder(optindex);lblC6.Visible = true;
                                    
                                rbQues6Answer3.Visible = true; 
                                rbQues6Answer3.Text= ds.Tables[0].Rows[i]["Option3"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues6Answer3.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                            {
                                lblTotalOptions6.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 4) lblD6.Text = GetAnswerOptionOrder(optindex);lblD6.Visible = true;
                                   
                                rbQues6Answer4.Visible = true; 
                                rbQues6Answer4.Text= ds.Tables[0].Rows[i]["Option4"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues6Answer4.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                            {
                                lblTotalOptions6.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 5)lblE6.Text = GetAnswerOptionOrder(optindex);lblE6.Visible = true;
                                    
                                rbQues6Answer5.Visible = true; 
                               rbQues6Answer5.Text= ds.Tables[0].Rows[i]["Option5"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues6Answer5.Checked = true;
                                optindex++;
                            }

                            //
                            if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                            {
                                lblTotalOptions6.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 6)lblF6.Text = GetAnswerOptionOrder(optindex);lblF6.Visible = true;
                                    
                                rbQues6Answer6.Visible = true; 
                               rbQues6Answer6.Text= ds.Tables[0].Rows[i]["Option6"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues6Answer6.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                            {
                                lblTotalOptions6.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 7)lblG6.Text = GetAnswerOptionOrder(optindex);lblG6.Visible = true;
                                    
                                rbQues6Answer7.Visible = true; 
                                rbQues6Answer7.Text= ds.Tables[0].Rows[i]["Option7"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues6Answer7.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                            {
                                lblTotalOptions6.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 8)lblH6.Text = GetAnswerOptionOrder(optindex); lblH6.Visible = true;
                                    
                                rbQues6Answer8.Visible = true;
                                rbQues6Answer8.Text= ds.Tables[0].Rows[i]["Option8"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues6Answer8.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                            {
                                lblTotalOptions6.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 9)lblI6.Text = GetAnswerOptionOrder(optindex);lblI6.Visible = true;
                                    
                                rbQues6Answer9.Visible = true; 
                                rbQues6Answer9.Text= ds.Tables[0].Rows[i]["Option9"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues6Answer9.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                            {
                                lblTotalOptions6.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 10)lblJ6.Text = GetAnswerOptionOrder(optindex);lblJ6.Visible = true;
                                    
                                rbQues6Answer10.Visible = true; 
                                rbQues6Answer10.Text= ds.Tables[0].Rows[i]["Option10"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues6Answer10.Checked = true;
                            }
                            //

                            curntquescnt++;
                            j++;
                            pnlQuestion6.Visible = true;
                            break;

                        case 6:
                            //optindex = 1;
                            lblNo7.Text = (slno + 6).ToString() + ".  "; lblNo7.Visible = true;
                           // lblQues7.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues7.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID7.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();
                            lblRatingType7.Text = ds.Tables[0].Rows[i]["ScoringStyle"].ToString();

                            var Ques7 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques7.Count() > 0)
                                if (Ques7.First().Answer != null)
                                    Answer = Ques7.First().Answer.ToString();
                            if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                            {
                                lblTotalOptions7.Text = optindex.ToString();
                                rbQues7Answer1.Visible = true; //bipson 12082010 lblA7.Visible = true;
                                rbQues7Answer1.Text= ds.Tables[0].Rows[i]["Option1"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues7Answer1.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                            {
                                lblTotalOptions7.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 2)lblB7.Text = GetAnswerOptionOrder(optindex);lblB7.Visible = true;
                                    
                                rbQues7Answer2.Visible = true; 
                                rbQues7Answer2.Text= ds.Tables[0].Rows[i]["Option2"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues7Answer2.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                            {
                                lblTotalOptions7.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 3)lblC7.Text = GetAnswerOptionOrder(optindex); lblC7.Visible = true;
                                    
                                rbQues7Answer3.Visible = true;
                                rbQues7Answer3.Text= ds.Tables[0].Rows[i]["Option3"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues7Answer3.Checked = true;
                                optindex++;
                            }

                            //radioQues7.Items.Add(ds.Tables[0].Rows[i]["Option3"].ToString());
                            if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                            {
                                lblTotalOptions7.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 4)lblD7.Text = GetAnswerOptionOrder(optindex);lblD7.Visible = true;
                                    
                                rbQues7Answer4.Visible = true; 
                                rbQues7Answer4.Text= ds.Tables[0].Rows[i]["Option4"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues7Answer4.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                            {
                                lblTotalOptions7.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 5)lblE7.Text = GetAnswerOptionOrder(optindex);lblE7.Visible = true;
                                    
                                rbQues7Answer5.Visible = true; 
                                rbQues7Answer5.Text= ds.Tables[0].Rows[i]["Option5"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues7Answer5.Checked = true;
                                optindex++;
                            }

                            //
                            if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                            {
                                lblTotalOptions7.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 6)lblF7.Text = GetAnswerOptionOrder(optindex); lblF7.Visible = true;
                                    
                                rbQues7Answer6.Visible = true;
                                rbQues7Answer6.Text= ds.Tables[0].Rows[i]["Option6"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues7Answer6.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                            {
                                lblTotalOptions7.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 7)lblG7.Text = GetAnswerOptionOrder(optindex);lblG7.Visible = true;
                                    
                                rbQues7Answer7.Visible = true; 
                                rbQues7Answer7.Text= ds.Tables[0].Rows[i]["Option7"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues7Answer7.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                            {
                                lblTotalOptions7.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 8)lblH7.Text = GetAnswerOptionOrder(optindex);lblH7.Visible = true;
                                    
                                rbQues7Answer8.Visible = true; 
                                rbQues7Answer8.Text= ds.Tables[0].Rows[i]["Option8"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues7Answer8.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                            {
                                lblTotalOptions7.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 9) lblI7.Text = GetAnswerOptionOrder(optindex);lblI7.Visible = true;
                                   
                                rbQues7Answer9.Visible = true; 
                                rbQues7Answer9.Text= ds.Tables[0].Rows[i]["Option9"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues7Answer9.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                            {
                                lblTotalOptions7.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 10) lblJ7.Text = GetAnswerOptionOrder(optindex); lblJ7.Visible = true;
                                   
                                rbQues7Answer10.Visible = true;
                                rbQues7Answer10.Text= ds.Tables[0].Rows[i]["Option10"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues7Answer10.Checked = true;                               
                            }
                            //

                            curntquescnt++;
                            j++;
                            pnlQuestion7.Visible = true;
                            break;

                        case 7:
                            //optindex = 1;
                            lblNo8.Text = (slno + 7).ToString() + ".  "; lblNo8.Visible = true;
                            //lblQues8.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues8.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID8.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();
                            lblRatingType8.Text = ds.Tables[0].Rows[i]["ScoringStyle"].ToString();

                            var Ques8 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques8.Count() > 0)
                                if (Ques8.First().Answer != null)
                                    Answer = Ques8.First().Answer.ToString();
                            if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                            {
                                lblTotalOptions8.Text = optindex.ToString();
                                rbQues8Answer1.Visible = true; //bipson 12082010 lblA8.Visible = true;
                                rbQues8Answer1.Text= ds.Tables[0].Rows[i]["Option1"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues8Answer1.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                            {
                                lblTotalOptions8.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 2)lblB8.Text = GetAnswerOptionOrder(optindex); lblB8.Visible = true;
                                    
                                rbQues8Answer2.Visible = true;
                                rbQues8Answer2.Text= ds.Tables[0].Rows[i]["Option2"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues8Answer2.Checked = true;
                                optindex++;
                            }

                            //radioQues8.Items.Add(ds.Tables[0].Rows[i]["Option2"].ToString());
                            if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                            {
                                lblTotalOptions8.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 3)lblC8.Text = GetAnswerOptionOrder(optindex);lblC8.Visible = true;
                                    
                                rbQues8Answer3.Visible = true; 
                                rbQues8Answer3.Text= ds.Tables[0].Rows[i]["Option3"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues8Answer3.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                            {
                                lblTotalOptions8.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 4)lblD8.Text = GetAnswerOptionOrder(optindex);lblD8.Visible = true;
                                    
                                rbQues8Answer4.Visible = true; 
                                rbQues8Answer4.Text= ds.Tables[0].Rows[i]["Option4"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues8Answer4.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                            {
                                lblTotalOptions8.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 5)lblE8.Text = GetAnswerOptionOrder(optindex);lblE8.Visible = true;
                                    
                                rbQues8Answer5.Visible = true; 
                               rbQues8Answer5.Text= ds.Tables[0].Rows[i]["Option5"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues8Answer5.Checked = true;
                                optindex++;
                            }

                            //
                            if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                            {
                                lblTotalOptions8.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 6)lblF8.Text = GetAnswerOptionOrder(optindex); lblF8.Visible = true;
                                    
                                rbQues8Answer6.Visible = true;
                                rbQues8Answer6.Text= ds.Tables[0].Rows[i]["Option6"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues8Answer6.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                            {
                                lblTotalOptions8.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 7)lblG8.Text = GetAnswerOptionOrder(optindex); lblG8.Visible = true;
                                    
                                rbQues8Answer7.Visible = true;
                                rbQues8Answer7.Text= ds.Tables[0].Rows[i]["Option7"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues8Answer7.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                            {
                                lblTotalOptions8.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 8)lblH8.Text = GetAnswerOptionOrder(optindex); lblH8.Visible = true;
                                    
                                rbQues8Answer8.Visible = true;
                                rbQues8Answer8.Text= ds.Tables[0].Rows[i]["Option8"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues8Answer8.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                            {
                                lblTotalOptions8.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 9) lblI8.Text = GetAnswerOptionOrder(optindex); lblI8.Visible = true;
                                   
                                rbQues8Answer9.Visible = true;
                                rbQues8Answer9.Text= ds.Tables[0].Rows[i]["Option9"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues8Answer9.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                            {
                                lblTotalOptions8.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 10) lblJ8.Text = GetAnswerOptionOrder(optindex); lblJ8.Visible = true;
                                   
                                rbQues8Answer10.Visible = true;
                                rbQues8Answer10.Text= ds.Tables[0].Rows[i]["Option10"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues8Answer10.Checked = true;
                            }
                            //


                            curntquescnt++;
                            j++;
                            pnlQuestion8.Visible = true;
                            break;

                        case 8:
                            //optindex = 1;
                            lblNo9.Text = (slno + 8).ToString() + ".  "; lblNo9.Visible = true;
                            //lblQues9.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues9.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID9.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();
                            lblRatingType9.Text = ds.Tables[0].Rows[i]["ScoringStyle"].ToString();

                            var Ques9 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques9.Count() > 0)
                                if (Ques9.First().Answer != null)
                                    Answer = Ques9.First().Answer.ToString();

                            if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                            {
                                lblTotalOptions9.Text = optindex.ToString();
                                rbQues9Answer1.Visible = true; //bipson 12082010 lblA9.Visible = true;
                                rbQues9Answer1.Text= ds.Tables[0].Rows[i]["Option1"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues9Answer1.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                            //radioQues9.Items.Add(ds.Tables[0].Rows[i]["Option1"].ToString());
                            {
                                lblTotalOptions9.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 2)lblB9.Text = GetAnswerOptionOrder(optindex); lblB9.Visible = true;
                                    
                                rbQues9Answer2.Visible = true;
                                rbQues9Answer2.Text= ds.Tables[0].Rows[i]["Option2"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues9Answer2.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                            {
                                lblTotalOptions9.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 3)lblC9.Text = GetAnswerOptionOrder(optindex); lblC9.Visible = true;
                                    
                                rbQues9Answer3.Visible = true;
                                rbQues9Answer3.Text= ds.Tables[0].Rows[i]["Option3"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues9Answer3.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                            {
                                lblTotalOptions9.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 4) lblD9.Text = GetAnswerOptionOrder(optindex); lblD9.Visible = true;
                                   
                                rbQues9Answer4.Visible = true;
                                rbQues9Answer4.Text= ds.Tables[0].Rows[i]["Option4"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues9Answer4.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                            {
                                lblTotalOptions9.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 5) lblE9.Text = GetAnswerOptionOrder(optindex); lblE9.Visible = true;
                                   
                                rbQues9Answer5.Visible = true;
                                rbQues9Answer5.Text= ds.Tables[0].Rows[i]["Option5"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues9Answer5.Checked = true;
                                optindex++;
                            }

                            //
                            if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                            {
                                lblTotalOptions9.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 6) lblF9.Text = GetAnswerOptionOrder(optindex);lblF9.Visible = true;
                                   
                                rbQues9Answer6.Visible = true; 
                                rbQues9Answer6.Text= ds.Tables[0].Rows[i]["Option6"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues9Answer6.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                            {
                                lblTotalOptions9.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 7) lblG9.Text = GetAnswerOptionOrder(optindex); lblG9.Visible = true;
                                   
                                rbQues9Answer7.Visible = true;
                                rbQues9Answer7.Text= ds.Tables[0].Rows[i]["Option7"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues9Answer7.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                            {
                                lblTotalOptions9.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 8) lblH9.Text = GetAnswerOptionOrder(optindex); lblH9.Visible = true;
                                   
                                rbQues9Answer8.Visible = true;
                                rbQues9Answer8.Text= ds.Tables[0].Rows[i]["Option8"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues9Answer8.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                            {
                                lblTotalOptions9.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 9) lblI9.Text = GetAnswerOptionOrder(optindex); lblI9.Visible = true;
                                   
                                rbQues9Answer9.Visible = true;
                                rbQues9Answer9.Text= ds.Tables[0].Rows[i]["Option9"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues9Answer9.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                            {
                                lblTotalOptions9.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 10)lblJ9.Text = GetAnswerOptionOrder(optindex); lblJ9.Visible = true;
                                    
                                rbQues9Answer10.Visible = true;
                                rbQues9Answer10.Text= ds.Tables[0].Rows[i]["Option10"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues9Answer10.Checked = true;
                            }
                            //

                            curntquescnt++;
                            j++;
                            pnlQuestion9.Visible = true;
                            break;

                        case 9:
                            //optindex = 1;
                            lblNo10.Text = (slno + 9).ToString() + ".  "; lblNo10.Visible = true;
                            //lblQues10.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues10.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID10.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();
                            lblRatingType10.Text = ds.Tables[0].Rows[i]["ScoringStyle"].ToString();

                            var Ques10 = from Ques in dataclass.EvaluationResults
                                         where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                         Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                         select Ques;
                            if (Ques10.Count() > 0)
                                if (Ques10.First().Answer != null)
                                    Answer = Ques10.First().Answer.ToString();
                            if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                            {
                                lblTotalOptions10.Text = optindex.ToString();
                                rbQues10Answer1.Visible = true;//bipson 12082010  lblA10.Visible = true; 
                                rbQues10Answer1.Text= ds.Tables[0].Rows[i]["Option1"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues10Answer1.Checked = true;
                                optindex++;
                            }

                            //radioQues10.Items.Add(ds.Tables[0].Rows[i]["Option1"].ToString());
                            if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                            {
                                lblTotalOptions10.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 2) lblB10.Text = GetAnswerOptionOrder(optindex); lblB10.Visible = true;
                                   
                                rbQues10Answer2.Visible = true;
                                rbQues10Answer2.Text= ds.Tables[0].Rows[i]["Option2"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues10Answer2.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                            {
                                lblTotalOptions10.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 3) lblC10.Text = GetAnswerOptionOrder(optindex); lblC10.Visible = true;
                                   
                                rbQues10Answer3.Visible = true;
                                rbQues10Answer3.Text= ds.Tables[0].Rows[i]["Option3"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues10Answer3.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                            {
                                lblTotalOptions10.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 4) lblD10.Text = GetAnswerOptionOrder(optindex); lblD10.Visible = true;
                                   
                                rbQues10Answer4.Visible = true;
                                rbQues10Answer4.Text= ds.Tables[0].Rows[i]["Option4"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues10Answer4.Checked = true;
                                optindex++;
                            }

                            if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                            {
                                lblTotalOptions10.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 5)lblE10.Text = GetAnswerOptionOrder(optindex);lblE10.Visible = true;
                                    
                                rbQues10Answer5.Visible = true; 
                                rbQues10Answer5.Text= ds.Tables[0].Rows[i]["Option5"].ToString();
                                //if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues10Answer5.Checked = true;
                                optindex++;
                            }

                            //
                            if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                            {
                                lblTotalOptions10.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 6) lblF10.Text = GetAnswerOptionOrder(optindex); lblF10.Visible = true;
                                   
                                rbQues10Answer6.Visible = true;
                                rbQues10Answer6.Text= ds.Tables[0].Rows[i]["Option6"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues10Answer6.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                            {
                                lblTotalOptions10.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 7) lblG10.Text = GetAnswerOptionOrder(optindex); lblG10.Visible = true;
                                   
                                rbQues10Answer7.Visible = true;
                                rbQues10Answer7.Text= ds.Tables[0].Rows[i]["Option7"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues10Answer7.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                            {
                                lblTotalOptions10.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 8) lblH10.Text = GetAnswerOptionOrder(optindex); lblH10.Visible = true;
                                   
                                rbQues10Answer8.Visible = true;
                                rbQues10Answer8.Text= ds.Tables[0].Rows[i]["Option8"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues10Answer8.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                            {
                                lblTotalOptions10.Text = optindex.ToString();
                                //bipson 12082010  if (optindex != 9) lblI10.Text = GetAnswerOptionOrder(optindex); lblI10.Visible = true;
                                   
                                rbQues10Answer9.Visible = true;
                               rbQues10Answer9.Text= ds.Tables[0].Rows[i]["Option9"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                               if (optindex.ToString() == Answer) rbQues10Answer9.Checked = true;
                                optindex++;
                            }
                            if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                            {
                                lblTotalOptions10.Text = optindex.ToString();
                                //bipson 12082010 if (optindex != 10) lblJ10.Text = GetAnswerOptionOrder(optindex); lblJ10.Visible = true;
                                   
                                rbQues10Answer10.Visible = true;
                                rbQues10Answer10.Text= ds.Tables[0].Rows[i]["Option10"].ToString();
                               // if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                if (optindex.ToString() == Answer) rbQues10Answer10.Checked = true;
                            }
                            //

                            curntquescnt++;
                            j++;
                            pnlQuestion10.Visible = true;
                            break;
                    }
                }
            }
            else
            {
                GoToNextPage();
            }
            Session["curntques"] = curntquescnt;
        //}
        //catch (Exception ex)
        //{
        //    FillQuestion();//bipson 14082010 to avoid arithmetic error...
            
        //    //lblmessage.Text += ex.Message;
        //    //conn.Close();
        //}
    }

    private void GoToNextPage()
    {

        Session["curRatingQuesCompleted"] = "True";

        //
        Session["AudioPrevious"] = null; Session["AudioCurrentpage"] = null;
        Session["VideoPrevious"] = null; Session["VideoCurrentpage"] = null;
        Session["MemWordPrevious"] = null; Session["MemWordCurrentpage"] = null;        //
        
        Session["questionColl"] = null;        
        Session["totalQuesCount"] = null; Session["totalQuesAvailable"] = null;        

        ClearSessionValues();      

        int variableIdPageNo = 0;
        if (Session["VariableIdIndexNo"] != null)
            variableIdPageNo = int.Parse(Session["VariableIdIndexNo"].ToString());
        variableIdPageNo = variableIdPageNo + 1;
        Session["VariableIdIndexNo"] = variableIdPageNo.ToString();
        Session["VariableIdIndexNo_timer"] = variableIdPageNo.ToString();// bip 12052010

        ///////
        Session["pagecount"] = null;//objques
        //if (Session["sectionIdIndexNo"] != null)
        //{
        //    int sectionIdIndexNo = int.Parse(Session["sectionIdIndexNo"].ToString());
        //    Session["sectionIdIndexNo"] = (sectionIdIndexNo + 1).ToString();
        //}
 
        Session["evaldirection"] = "Next";
        
        Session["SubCtrl"] = "ObjectiveQuestns.ascx";
        Response.Redirect("FJAHome.aspx"); 

    }

    private int GetRatingtypeAnswer(int index)
    {

        int answer = -1;
        int optioncount = 0;
        if (index == 1)
        {
            if (rbQues1Answer1.Text.Trim() != "") { if (rbQues1Answer1.Checked == true) answer = optioncount; }

            if (rbQues1Answer2.Text.Trim() != "") { optioncount++; if (rbQues1Answer2.Checked == true) answer = optioncount; }

             if (rbQues1Answer3.Text.Trim() != "") { optioncount++; if (rbQues1Answer3.Checked == true) answer = optioncount; }

             if (rbQues1Answer4.Text.Trim() != "") { optioncount++; if (rbQues1Answer4.Checked == true) answer = optioncount; }

             if (rbQues1Answer5.Text.Trim() != "") { optioncount++; if (rbQues1Answer5.Checked == true) answer = optioncount; }

             if (rbQues1Answer6.Text.Trim() != "") { optioncount++; if (rbQues1Answer6.Checked == true) answer = optioncount; }

             if (rbQues1Answer7.Text.Trim() != "") { optioncount++; if (rbQues1Answer7.Checked == true) answer = optioncount; }

             if (rbQues1Answer8.Text.Trim() != "") { optioncount++; if (rbQues1Answer8.Checked == true) answer = optioncount; }

             if (rbQues1Answer9.Text.Trim() != "") { optioncount++; if (rbQues1Answer9.Checked == true) answer = optioncount; }

             if (rbQues1Answer10.Text.Trim() != "") { optioncount++; if (rbQues1Answer10.Checked == true) answer = optioncount; }
            if (answer >= 0)
            {
                if (lblRatingType1.Text == "Reverse")
                {
                    answer = (optioncount - answer);// +1;
                }
            }
        }
        else if (index == 2)
        {
            if (rbQues2Answer1.Text.Trim() != "") { if (rbQues2Answer1.Checked == true) answer = optioncount;  }

             if (rbQues2Answer2.Text.Trim() != "") { optioncount++; if (rbQues2Answer2.Checked == true) answer = optioncount;  }

             if (rbQues2Answer3.Text.Trim() != "") { optioncount++; if (rbQues2Answer3.Checked == true) answer = optioncount;  }

             if (rbQues2Answer4.Text.Trim() != "") { optioncount++; if (rbQues2Answer4.Checked == true) answer = optioncount;  }

             if (rbQues2Answer5.Text.Trim() != "") { optioncount++; if (rbQues2Answer5.Checked == true) answer = optioncount;  }

             if (rbQues2Answer6.Text.Trim() != "") { optioncount++; if (rbQues2Answer6.Checked == true) answer = optioncount;  }

             if (rbQues2Answer7.Text.Trim() != "") { optioncount++; if (rbQues2Answer7.Checked == true) answer = optioncount;  }

             if (rbQues2Answer8.Text.Trim() != "") { optioncount++; if (rbQues2Answer8.Checked == true) answer = optioncount;  }

             if (rbQues2Answer9.Text.Trim() != "") { optioncount++; if (rbQues2Answer9.Checked == true) answer = optioncount;  }

             if (rbQues2Answer10.Text.Trim() != "") { optioncount++; if (rbQues2Answer10.Checked == true) answer = optioncount; }
            if (answer >= 0)
            {
                if (lblRatingType2.Text == "Reverse")
                {
                    answer = (optioncount - answer) ;// +1;
                }
            }
        }
        else if (index == 3)
        {
            if (rbQues3Answer1.Text.Trim() != "") { if (rbQues3Answer1.Checked == true) answer = optioncount;  }

             if (rbQues3Answer2.Text.Trim() != "") { optioncount++; if (rbQues3Answer2.Checked == true) answer = optioncount;  }

             if (rbQues3Answer3.Text.Trim() != "") { optioncount++; if (rbQues3Answer3.Checked == true) answer = optioncount;  }

             if (rbQues3Answer4.Text.Trim() != "") { optioncount++; if (rbQues3Answer4.Checked == true) answer = optioncount;  }

             if (rbQues3Answer5.Text.Trim() != "") { optioncount++; if (rbQues3Answer5.Checked == true) answer = optioncount;  }

             if (rbQues3Answer6.Text.Trim() != "") { optioncount++; if (rbQues3Answer6.Checked == true) answer = optioncount;  }

             if (rbQues3Answer7.Text.Trim() != "") { optioncount++; if (rbQues3Answer7.Checked == true) answer = optioncount;  }

             if (rbQues3Answer8.Text.Trim() != "") { optioncount++; if (rbQues3Answer8.Checked == true) answer = optioncount;  }

             if (rbQues3Answer9.Text.Trim() != "") { optioncount++; if (rbQues3Answer9.Checked == true) answer = optioncount;  }

             if (rbQues3Answer10.Text.Trim() != "") { optioncount++; if (rbQues3Answer10.Checked == true) answer = optioncount; }
            if (answer >= 0)
            {
                if (lblRatingType3.Text == "Reverse")
                {
                    answer = (optioncount - answer) ;// +1;
                }
            }
        }
        else if (index == 4)
        {
            if (rbQues4Answer1.Text.Trim() != "") { if (rbQues4Answer1.Checked == true) answer = optioncount;  }

             if (rbQues4Answer2.Text.Trim() != "") { optioncount++; if (rbQues4Answer2.Checked == true) answer = optioncount;  }

             if (rbQues4Answer3.Text.Trim() != "") { optioncount++; if (rbQues4Answer3.Checked == true) answer = optioncount;  }

             if (rbQues4Answer4.Text.Trim() != "") { optioncount++; if (rbQues4Answer4.Checked == true) answer = optioncount;  }

             if (rbQues4Answer5.Text.Trim() != "") { optioncount++; if (rbQues4Answer5.Checked == true) answer = optioncount;  }

             if (rbQues4Answer6.Text.Trim() != "") { optioncount++; if (rbQues4Answer6.Checked == true) answer = optioncount;  }

             if (rbQues4Answer7.Text.Trim() != "") { optioncount++; if (rbQues4Answer7.Checked == true) answer = optioncount;  }

             if (rbQues4Answer8.Text.Trim() != "") { optioncount++; if (rbQues4Answer8.Checked == true) answer = optioncount;  }

             if (rbQues4Answer9.Text.Trim() != "") { optioncount++; if (rbQues4Answer9.Checked == true) answer = optioncount;  }

             if (rbQues4Answer10.Text.Trim() != "") { optioncount++; if (rbQues4Answer10.Checked == true) answer = optioncount; }
            if (answer >= 0)
            {
                if (lblRatingType4.Text == "Reverse")
                {
                    answer = (optioncount - answer) ;// +1;
                }
            }
        }
        else if (index == 5)
        {
            if (rbQues5Answer1.Text.Trim() != "") { if (rbQues5Answer1.Checked == true) answer = optioncount;  }

             if (rbQues5Answer2.Text.Trim() != "") { optioncount++; if (rbQues5Answer2.Checked == true) answer = optioncount;  }

             if (rbQues5Answer3.Text.Trim() != "") { optioncount++; if (rbQues5Answer3.Checked == true) answer = optioncount;  }

             if (rbQues5Answer4.Text.Trim() != "") { optioncount++; if (rbQues5Answer4.Checked == true) answer = optioncount;  }

             if (rbQues5Answer5.Text.Trim() != "") { optioncount++; if (rbQues5Answer5.Checked == true) answer = optioncount;  }

             if (rbQues5Answer6.Text.Trim() != "") { optioncount++; if (rbQues5Answer6.Checked == true) answer = optioncount;  }

             if (rbQues5Answer7.Text.Trim() != "") { optioncount++; if (rbQues5Answer7.Checked == true) answer = optioncount;  }

             if (rbQues5Answer8.Text.Trim() != "") { optioncount++; if (rbQues5Answer8.Checked == true) answer = optioncount;  }

             if (rbQues5Answer9.Text.Trim() != "") { optioncount++; if (rbQues5Answer9.Checked == true) answer = optioncount;  }

             if (rbQues5Answer10.Text.Trim() != "") { optioncount++; if (rbQues5Answer10.Checked == true) answer = optioncount; }
            if (answer >= 0)
            {
                if (lblRatingType5.Text == "Reverse")
                {
                    answer = (optioncount - answer) ;// +1;
                }
            }
        }
        else if (index == 6)
        {
            if (rbQues6Answer1.Text.Trim() != "") { if (rbQues6Answer1.Checked == true) answer = optioncount;  }

             if (rbQues6Answer2.Text.Trim() != "") { optioncount++; if (rbQues6Answer2.Checked == true) answer = optioncount;  }

             if (rbQues6Answer3.Text.Trim() != "") { optioncount++; if (rbQues6Answer3.Checked == true) answer = optioncount;  }

             if (rbQues6Answer4.Text.Trim() != "") { optioncount++; if (rbQues6Answer4.Checked == true) answer = optioncount;  }

             if (rbQues6Answer5.Text.Trim() != "") { optioncount++; if (rbQues6Answer5.Checked == true) answer = optioncount;  }

             if (rbQues6Answer6.Text.Trim() != "") { optioncount++; if (rbQues6Answer6.Checked == true) answer = optioncount;  }

             if (rbQues6Answer7.Text.Trim() != "") { optioncount++; if (rbQues6Answer7.Checked == true) answer = optioncount;  }

             if (rbQues6Answer8.Text.Trim() != "") { optioncount++; if (rbQues6Answer8.Checked == true) answer = optioncount;  }

             if (rbQues6Answer9.Text.Trim() != "") { optioncount++; if (rbQues6Answer9.Checked == true) answer = optioncount;  }

             if (rbQues6Answer10.Text.Trim() != "") { optioncount++; if (rbQues6Answer10.Checked == true) answer = optioncount; }
            if (answer >= 0)
            {
                if (lblRatingType6.Text == "Reverse")
                {
                    answer = (optioncount - answer) ;// +1;
                }
            }
        }
        else if (index == 7)
        {
            if (rbQues7Answer1.Text.Trim() != "") { if (rbQues7Answer1.Checked == true) answer = optioncount;  }

             if (rbQues7Answer2.Text.Trim() != "") { optioncount++; if (rbQues7Answer2.Checked == true) answer = optioncount;  }

             if (rbQues7Answer3.Text.Trim() != "") { optioncount++; if (rbQues7Answer3.Checked == true) answer = optioncount;  }

             if (rbQues7Answer4.Text.Trim() != "") { optioncount++; if (rbQues7Answer4.Checked == true) answer = optioncount;  }

             if (rbQues7Answer5.Text.Trim() != "") { optioncount++; if (rbQues7Answer5.Checked == true) answer = optioncount;  }

             if (rbQues7Answer6.Text.Trim() != "") { optioncount++; if (rbQues7Answer6.Checked == true) answer = optioncount;  }

             if (rbQues7Answer7.Text.Trim() != "") { optioncount++; if (rbQues7Answer7.Checked == true) answer = optioncount;  }

             if (rbQues7Answer8.Text.Trim() != "") { optioncount++; if (rbQues7Answer8.Checked == true) answer = optioncount;  }

             if (rbQues7Answer9.Text.Trim() != "") { optioncount++; if (rbQues7Answer9.Checked == true) answer = optioncount;  }

             if (rbQues7Answer10.Text.Trim() != "") { optioncount++; if (rbQues7Answer10.Checked == true) answer = optioncount; }
            if (answer >= 0)
            {
                if (lblRatingType7.Text == "Reverse")
                {
                    answer = (optioncount - answer) ;// +1;
                }
            }
        }
        else if (index == 8)
        {
            if (rbQues8Answer1.Text.Trim() != "") { if (rbQues8Answer1.Checked == true) answer = optioncount;  }

             if (rbQues8Answer2.Text.Trim() != "") { optioncount++; if (rbQues8Answer2.Checked == true) answer = optioncount;  }

             if (rbQues8Answer3.Text.Trim() != "") { optioncount++; if (rbQues8Answer3.Checked == true) answer = optioncount;  }

             if (rbQues8Answer4.Text.Trim() != "") { optioncount++; if (rbQues8Answer4.Checked == true) answer = optioncount;  }

             if (rbQues8Answer5.Text.Trim() != "") { optioncount++; if (rbQues8Answer5.Checked == true) answer = optioncount;  }

             if (rbQues8Answer6.Text.Trim() != "") { optioncount++; if (rbQues8Answer6.Checked == true) answer = optioncount;  }

             if (rbQues8Answer7.Text.Trim() != "") { optioncount++; if (rbQues8Answer7.Checked == true) answer = optioncount;  }

             if (rbQues8Answer8.Text.Trim() != "") { optioncount++; if (rbQues8Answer8.Checked == true) answer = optioncount;  }

             if (rbQues8Answer9.Text.Trim() != "") { optioncount++; if (rbQues8Answer9.Checked == true) answer = optioncount;  }

             if (rbQues8Answer10.Text.Trim() != "") { optioncount++; if (rbQues8Answer10.Checked == true) answer = optioncount; }

            if (answer >= 0)
            {
                if (lblRatingType8.Text == "Reverse")
                {
                    answer = (optioncount - answer) ;// +1;
                }
            }
        }
        else if (index == 9)
        {
            if (rbQues9Answer1.Text.Trim() != "") { if (rbQues9Answer1.Checked == true) answer = optioncount;  }

             if (rbQues9Answer2.Text.Trim() != "") { optioncount++; if (rbQues9Answer2.Checked == true) answer = optioncount;  }

             if (rbQues9Answer3.Text.Trim() != "") { optioncount++; if (rbQues9Answer3.Checked == true) answer = optioncount;  }

             if (rbQues9Answer4.Text.Trim() != "") { optioncount++; if (rbQues9Answer4.Checked == true) answer = optioncount;  }

             if (rbQues9Answer5.Text.Trim() != "") { optioncount++; if (rbQues9Answer5.Checked == true) answer = optioncount;  }

             if (rbQues9Answer6.Text.Trim() != "") { optioncount++; if (rbQues9Answer6.Checked == true) answer = optioncount;  }

             if (rbQues9Answer7.Text.Trim() != "") { optioncount++; if (rbQues9Answer7.Checked == true) answer = optioncount;  }

             if (rbQues9Answer8.Text.Trim() != "") { optioncount++; if (rbQues9Answer8.Checked == true) answer = optioncount;  }

             if (rbQues9Answer9.Text.Trim() != "") { optioncount++; if (rbQues9Answer9.Checked == true) answer = optioncount;  }

             if (rbQues9Answer10.Text.Trim() != "") { optioncount++; if (rbQues9Answer10.Checked == true) answer = optioncount; }
            if (answer >= 0)
            {
                if (lblRatingType9.Text == "Reverse")
                {
                    answer = (optioncount - answer) ;// +1;
                }
            }
        }
        else if (index == 10)
        {
            if (rbQues10Answer1.Text.Trim() != "") { if (rbQues10Answer1.Checked == true) answer = optioncount;  }

             if (rbQues10Answer2.Text.Trim() != "") { optioncount++; if (rbQues10Answer2.Checked == true) answer = optioncount;  }

             if (rbQues10Answer3.Text.Trim() != "") { optioncount++; if (rbQues10Answer3.Checked == true) answer = optioncount;  }

             if (rbQues10Answer4.Text.Trim() != "") { optioncount++; if (rbQues10Answer4.Checked == true) answer = optioncount;  }

             if (rbQues10Answer5.Text.Trim() != "") { optioncount++; if (rbQues10Answer5.Checked == true) answer = optioncount;  }

             if (rbQues10Answer6.Text.Trim() != "") { optioncount++; if (rbQues10Answer6.Checked == true) answer = optioncount;  }

             if (rbQues10Answer7.Text.Trim() != "") { optioncount++; if (rbQues10Answer7.Checked == true) answer = optioncount;  }

             if (rbQues10Answer8.Text.Trim() != "") { optioncount++; if (rbQues10Answer8.Checked == true) answer = optioncount;  }

             if (rbQues10Answer9.Text.Trim() != "") { optioncount++; if (rbQues10Answer9.Checked == true) answer = optioncount;  }

             if (rbQues10Answer10.Text.Trim() != "") { optioncount++; if (rbQues10Answer10.Checked == true) answer = optioncount; }
            if (answer >= 0)
            {
                if (lblRatingType10.Text == "Reverse")
                {
                    answer = (optioncount - answer) ;// +1;
                }
            }
        }
        return answer;
    }

    private void SaveAnswer()
    {
        string quescategory = "RatingType";
        int testsectionid = 0;
        if (Session["CurrentTestSectionId"] != null)
            testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());

        string answerResult = "-";
        int answer = 0;
        if (lblQuesID1.Text.Trim() != "")
        {           
            answer = GetRatingtypeAnswer(1);
            // code to insert "-" sign if user leave any answers // bip 05062011
            if (answer < 0)
                answerResult = "-";
            else answerResult = answer.ToString();

            qusid1 = int.Parse(lblQuesID1.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid1, usercode, tcellQues1.InnerHtml, answerResult, userid, testId, testsectionid, quescategory);
        }
        answer = 0;// "";

        if (lblQuesID2.Text.Trim() != "")
        {            
            answer = GetRatingtypeAnswer(2);
            if (answer < 0)
                answerResult = "-";
            else answerResult = answer.ToString();
            qusid2 = int.Parse(lblQuesID2.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid2, usercode, tcellQues2.InnerHtml, answerResult, userid, testId, testsectionid, quescategory);
        }

        answer = 0;// "";
        if (lblQuesID3.Text.Trim() != "")
        {            
            answer = GetRatingtypeAnswer(3);
            if (answer < 0)
                answerResult = "-";
            else answerResult = answer.ToString();
            qusid3 = int.Parse(lblQuesID3.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid3, usercode, tcellQues3.InnerHtml, answerResult, userid, testId, testsectionid, quescategory);
        }

        answer = 0;// "";

        if (lblQuesID4.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(4);
            if (answer < 0)
                answerResult = "-";
            else answerResult = answer.ToString();
            qusid4 = int.Parse(lblQuesID4.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid4, usercode, tcellQues4.InnerHtml, answerResult, userid, testId, testsectionid, quescategory);
        }

        answer = 0;// "";

        if (lblQuesID5.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(5);
            if (answer < 0)
                answerResult = "-";
            else answerResult = answer.ToString();
            qusid5 = int.Parse(lblQuesID5.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid5, usercode, tcellQues5.InnerHtml, answerResult, userid, testId, testsectionid, quescategory);
        }

        answer = 0;// "";

        if (lblQuesID6.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(6);
            if (answer < 0)
                answerResult = "-";
            else answerResult = answer.ToString();
            qusid6 = int.Parse(lblQuesID6.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid6, usercode, tcellQues6.InnerHtml, answerResult, userid, testId, testsectionid, quescategory);
        }

        answer = 0;// "";

        if (lblQuesID7.Text.Trim() != "")
        {           
            answer = GetRatingtypeAnswer(7);
            if (answer < 0)
                answerResult = "-";
            else answerResult = answer.ToString();
            qusid7 = int.Parse(lblQuesID7.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid7, usercode, tcellQues7.InnerHtml, answerResult, userid, testId, testsectionid, quescategory);
        }
        answer = 0;// "";

        if (lblQuesID8.Text.Trim() != "")
        {           
            answer = GetRatingtypeAnswer(8);
            if (answer < 0)
                answerResult = "-";
            else answerResult = answer.ToString();
            qusid8 = int.Parse(lblQuesID8.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid8, usercode, tcellQues8.InnerHtml, answerResult, userid, testId, testsectionid, quescategory);
        }

        answer = 0;// "";
        if (lblQuesID9.Text.Trim() != "")
        {           
            answer = GetRatingtypeAnswer(9);
            if (answer < 0)
                answerResult = "-";
            else answerResult = answer.ToString();
            qusid9 = int.Parse(lblQuesID9.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid9, usercode, tcellQues9.InnerHtml, answerResult, userid, testId, testsectionid, quescategory);
        }

        answer = 0;// "";
        if (lblQuesID10.Text.Trim() != "")
        {           
            answer = GetRatingtypeAnswer(10);
            if (answer < 0)
                answerResult = "-";
            else answerResult = answer.ToString();
            qusid10 = int.Parse(lblQuesID10.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid10, usercode, tcellQues10.InnerHtml, answerResult, userid, testId, testsectionid, quescategory);
        }
    }

    private void ClearSessionValues()
    {
       // Session["pagecountRating"] = null;
    }

    private void ClearControls()
    {
        rbQues1Answer1.Checked = false; rbQues1Answer1.Visible = false; rbQues1Answer2.Checked = false; rbQues1Answer2.Visible = false;
        rbQues1Answer3.Checked = false; rbQues1Answer3.Visible = false; rbQues1Answer4.Checked = false; rbQues1Answer4.Visible = false;
        rbQues1Answer5.Checked = false; rbQues1Answer5.Visible = false; rbQues1Answer6.Checked = false; rbQues1Answer6.Visible = false;
        rbQues1Answer7.Checked = false; rbQues1Answer7.Visible = false; rbQues1Answer8.Checked = false; rbQues1Answer8.Visible = false;
        rbQues1Answer9.Checked = false; rbQues1Answer9.Visible = false; rbQues1Answer10.Checked = false; rbQues1Answer10.Visible = false;

        rbQues2Answer1.Checked = false; rbQues2Answer1.Visible = false; rbQues2Answer2.Checked = false; rbQues2Answer2.Visible = false;
        rbQues2Answer3.Checked = false; rbQues2Answer3.Visible = false; rbQues2Answer4.Checked = false; rbQues2Answer4.Visible = false;
        rbQues2Answer5.Checked = false; rbQues2Answer5.Visible = false; rbQues2Answer6.Checked = false; rbQues2Answer6.Visible = false;
        rbQues2Answer7.Checked = false; rbQues2Answer7.Visible = false; rbQues2Answer8.Checked = false; rbQues2Answer8.Visible = false;
        rbQues2Answer9.Checked = false; rbQues2Answer9.Visible = false; rbQues2Answer10.Checked = false; rbQues2Answer10.Visible = false;

        rbQues3Answer1.Checked = false; rbQues3Answer1.Visible = false; rbQues3Answer2.Checked = false; rbQues3Answer2.Visible = false;
        rbQues3Answer3.Checked = false; rbQues3Answer3.Visible = false; rbQues3Answer4.Checked = false; rbQues3Answer4.Visible = false;
        rbQues3Answer5.Checked = false; rbQues3Answer5.Visible = false; rbQues3Answer6.Checked = false; rbQues3Answer6.Visible = false;
        rbQues3Answer7.Checked = false; rbQues3Answer7.Visible = false; rbQues3Answer8.Checked = false; rbQues3Answer8.Visible = false;
        rbQues3Answer9.Checked = false; rbQues3Answer9.Visible = false; rbQues3Answer10.Checked = false; rbQues3Answer10.Visible = false;

        rbQues4Answer1.Checked = false; rbQues4Answer1.Visible = false; rbQues4Answer2.Checked = false; rbQues4Answer2.Visible = false;
        rbQues4Answer3.Checked = false; rbQues4Answer3.Visible = false; rbQues4Answer4.Checked = false; rbQues4Answer4.Visible = false;
        rbQues4Answer5.Checked = false; rbQues4Answer5.Visible = false; rbQues4Answer6.Checked = false; rbQues4Answer6.Visible = false;
        rbQues4Answer7.Checked = false; rbQues4Answer7.Visible = false; rbQues4Answer8.Checked = false; rbQues4Answer8.Visible = false;
        rbQues4Answer9.Checked = false; rbQues4Answer9.Visible = false; rbQues4Answer10.Checked = false; rbQues4Answer10.Visible = false;

        rbQues5Answer1.Checked = false; rbQues5Answer1.Visible = false; rbQues5Answer2.Checked = false; rbQues5Answer2.Visible = false;
        rbQues5Answer3.Checked = false; rbQues5Answer3.Visible = false; rbQues5Answer4.Checked = false; rbQues5Answer4.Visible = false;
        rbQues5Answer5.Checked = false; rbQues5Answer5.Visible = false; rbQues5Answer6.Checked = false; rbQues5Answer6.Visible = false;
        rbQues5Answer7.Checked = false; rbQues5Answer7.Visible = false; rbQues5Answer8.Checked = false; rbQues5Answer8.Visible = false;
        rbQues5Answer9.Checked = false; rbQues5Answer9.Visible = false; rbQues5Answer10.Checked = false; rbQues5Answer10.Visible = false;

        rbQues6Answer1.Checked = false; rbQues6Answer1.Visible = false; rbQues6Answer2.Checked = false; rbQues6Answer2.Visible = false;
        rbQues6Answer3.Checked = false; rbQues6Answer3.Visible = false; rbQues6Answer4.Checked = false; rbQues6Answer4.Visible = false;
        rbQues6Answer5.Checked = false; rbQues6Answer5.Visible = false; rbQues6Answer6.Checked = false; rbQues6Answer6.Visible = false;
        rbQues6Answer7.Checked = false; rbQues6Answer7.Visible = false; rbQues6Answer8.Checked = false; rbQues6Answer8.Visible = false;
        rbQues6Answer9.Checked = false; rbQues6Answer9.Visible = false; rbQues6Answer10.Checked = false; rbQues6Answer10.Visible = false;

        rbQues7Answer1.Checked = false; rbQues7Answer1.Visible = false; rbQues7Answer2.Checked = false; rbQues7Answer2.Visible = false;
        rbQues7Answer3.Checked = false; rbQues7Answer3.Visible = false; rbQues7Answer4.Checked = false; rbQues7Answer4.Visible = false;
        rbQues7Answer5.Checked = false; rbQues7Answer5.Visible = false; rbQues7Answer6.Checked = false; rbQues7Answer6.Visible = false;
        rbQues7Answer7.Checked = false; rbQues7Answer7.Visible = false; rbQues7Answer8.Checked = false; rbQues7Answer8.Visible = false;
        rbQues7Answer9.Checked = false; rbQues7Answer9.Visible = false; rbQues7Answer10.Checked = false; rbQues7Answer10.Visible = false;

        rbQues8Answer1.Checked = false; rbQues8Answer1.Visible = false; rbQues8Answer2.Checked = false; rbQues8Answer2.Visible = false;
        rbQues8Answer3.Checked = false; rbQues8Answer3.Visible = false; rbQues8Answer4.Checked = false; rbQues8Answer4.Visible = false;
        rbQues8Answer5.Checked = false; rbQues8Answer5.Visible = false; rbQues8Answer6.Checked = false; rbQues8Answer6.Visible = false;
        rbQues8Answer7.Checked = false; rbQues8Answer7.Visible = false; rbQues8Answer8.Checked = false; rbQues8Answer8.Visible = false;
        rbQues8Answer9.Checked = false; rbQues8Answer9.Visible = false; rbQues8Answer10.Checked = false; rbQues8Answer10.Visible = false;

        rbQues9Answer1.Checked = false; rbQues9Answer1.Visible = false; rbQues9Answer2.Checked = false; rbQues9Answer2.Visible = false;
        rbQues9Answer3.Checked = false; rbQues9Answer3.Visible = false; rbQues9Answer4.Checked = false; rbQues9Answer4.Visible = false;
        rbQues9Answer5.Checked = false; rbQues9Answer5.Visible = false; rbQues9Answer6.Checked = false; rbQues9Answer6.Visible = false;
        rbQues9Answer7.Checked = false; rbQues9Answer7.Visible = false; rbQues9Answer8.Checked = false; rbQues9Answer8.Visible = false;
        rbQues9Answer9.Checked = false; rbQues9Answer9.Visible = false; rbQues9Answer10.Checked = false; rbQues9Answer10.Visible = false;

        rbQues10Answer1.Checked = false; rbQues10Answer1.Visible = false; rbQues10Answer2.Checked = false; rbQues10Answer2.Visible = false;
        rbQues10Answer3.Checked = false; rbQues10Answer3.Visible = false; rbQues10Answer4.Checked = false; rbQues10Answer4.Visible = false;
        rbQues10Answer5.Checked = false; rbQues10Answer5.Visible = false; rbQues10Answer6.Checked = false; rbQues10Answer6.Visible = false;
        rbQues10Answer7.Checked = false; rbQues10Answer7.Visible = false; rbQues10Answer8.Checked = false; rbQues10Answer8.Visible = false;
        rbQues10Answer9.Checked = false; rbQues10Answer9.Visible = false; rbQues10Answer10.Checked = false; rbQues10Answer10.Visible = false;

        lblA1.Visible = false; lblB1.Visible = false; lblC1.Visible = false; lblD1.Visible = false; lblE1.Visible = false; lblF1.Visible = false; lblG1.Visible = false; lblH1.Visible = false; lblI1.Visible = false; lblJ1.Visible = false;
        lblA2.Visible = false; lblB2.Visible = false; lblC2.Visible = false; lblD2.Visible = false; lblE2.Visible = false; lblF2.Visible = false; lblG2.Visible = false; lblH2.Visible = false; lblI2.Visible = false; lblJ2.Visible = false;
        lblA3.Visible = false; lblB3.Visible = false; lblC3.Visible = false; lblD3.Visible = false; lblE3.Visible = false; lblF3.Visible = false; lblG3.Visible = false; lblH3.Visible = false; lblI3.Visible = false; lblJ3.Visible = false;
        lblA4.Visible = false; lblB4.Visible = false; lblC4.Visible = false; lblD4.Visible = false; lblE4.Visible = false; lblF4.Visible = false; lblG4.Visible = false; lblH4.Visible = false; lblI4.Visible = false; lblJ4.Visible = false;
        lblA5.Visible = false; lblB5.Visible = false; lblC5.Visible = false; lblD5.Visible = false; lblE5.Visible = false; lblF5.Visible = false; lblG5.Visible = false; lblH5.Visible = false; lblI5.Visible = false; lblJ5.Visible = false;
        lblA6.Visible = false; lblB6.Visible = false; lblC6.Visible = false; lblD6.Visible = false; lblE6.Visible = false; lblF6.Visible = false; lblG6.Visible = false; lblH6.Visible = false; lblI6.Visible = false; lblJ6.Visible = false;
        lblA7.Visible = false; lblB7.Visible = false; lblC7.Visible = false; lblD7.Visible = false; lblE7.Visible = false; lblF7.Visible = false; lblG7.Visible = false; lblH7.Visible = false; lblI7.Visible = false; lblJ7.Visible = false;
        lblA8.Visible = false; lblB8.Visible = false; lblC8.Visible = false; lblD8.Visible = false; lblE8.Visible = false; lblF8.Visible = false; lblG8.Visible = false; lblH8.Visible = false; lblI8.Visible = false; lblJ8.Visible = false;
        lblA9.Visible = false; lblB9.Visible = false; lblC9.Visible = false; lblD9.Visible = false; lblE9.Visible = false; lblF9.Visible = false; lblG9.Visible = false; lblH9.Visible = false; lblI9.Visible = false; lblJ9.Visible = false;
        lblA10.Visible = false; lblB10.Visible = false; lblC10.Visible = false; lblD10.Visible = false; lblE10.Visible = false; lblF10.Visible = false; lblG10.Visible = false; lblH10.Visible = false; lblI10.Visible = false; lblJ10.Visible = false;

        lblQuesID1.Text = ""; lblQuesID2.Text = ""; lblQuesID3.Text = ""; lblQuesID4.Text = ""; lblQuesID5.Text = "";
        lblQuesID6.Text = ""; lblQuesID7.Text = ""; lblQuesID8.Text = ""; lblQuesID9.Text = ""; lblQuesID10.Text = "";

        // bip 17122009
        lblQuesID1.Visible=false; lblQuesID2.Visible=false; lblQuesID3.Visible=false; lblQuesID4.Visible=false; lblQuesID5.Visible=false;
        lblQuesID6.Visible=false; lblQuesID7.Visible=false; lblQuesID8.Visible=false; lblQuesID9.Visible=false; lblQuesID10.Visible=false;

        lblNo1.Text = ""; lblNo2.Text = ""; lblNo3.Text = ""; lblNo4.Text = ""; lblNo5.Text = "";
        lblNo6.Text = ""; lblNo7.Text = ""; lblNo8.Text = ""; lblNo9.Text = ""; lblNo10.Text = "";

        // bip 17122009
        lblNo1.Visible=false; lblNo2.Visible=false; lblNo3.Visible=false; lblNo4.Visible=false; lblNo5.Visible=false;
        lblNo6.Visible=false; lblNo7.Visible=false; lblNo8.Visible=false; lblNo9.Visible=false; lblNo10.Visible=false;

        lblQues1.Text = ""; lblQues2.Text = ""; lblQues3.Text = ""; lblQues4.Text = ""; lblQues5.Text = "";
        lblQues6.Text = ""; lblQues7.Text = ""; lblQues8.Text = ""; lblQues9.Text = ""; lblQues10.Text = "";

        // bip 17122009
        lblQues1.Visible=false; lblQues2.Visible=false; lblQues3.Visible=false; lblQues4.Visible=false; lblQues5.Visible=false;
        lblQues6.Visible=false; lblQues7.Visible=false; lblQues8.Visible=false; lblQues9.Visible=false; lblQues10.Visible=false;

        tcellQues1.InnerHtml = ""; tcellQues2.InnerHtml = ""; tcellQues3.InnerHtml = ""; tcellQues4.InnerHtml = ""; tcellQues5.InnerHtml = "";
        tcellQues6.InnerHtml = ""; tcellQues7.InnerHtml = ""; tcellQues8.InnerHtml = ""; tcellQues9.InnerHtml = ""; tcellQues10.InnerHtml = "";

    }


    private Boolean CheckTime()//bip 10052010 
    {
        Boolean timeExpired = false;//bip 10052010 
        if (CheckTestTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for your test is completed, <br> If you are interested to take this test again please contact our site admin";
            pnlpopup_timer.Visible = true; btnYes_timer_Test.Visible = true; Timer1.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";//bip 10052010
            //return;
        }
        else if (CheckTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for this Test Section is completed, <br> Do you want to continue with the remaining sections under your Test?";
            pnlpopup_timer.Visible = true; btnYes_timer.Visible = true; btnNo_timer.Visible = true; Timer1.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";//bip 10052010
            //return;
        }
        else if (CheckTestSecVarTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for this Variable under selected Section is completed, <br> Do you want to continue with the remaining Variables under your Test Section?";
            pnlpopup_timer.Visible = true; btnYes_timer_TestVariable.Visible = true; btnNo_timer.Visible = true; Timer1.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";//bip 10052010
            //return;
        }
        if (timeExpired == true)
        {
            if (Session["saved"] != null)
                if (Session["saved"].ToString() == "true")
                    return timeExpired;

            SaveAnswer(); Session["saved"] = "true";
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
        //else SaveAnswer();//bip 10052010
    }
    private void SaveValues()
    {
        SaveAnswer();

        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());


        int curcount = 0;
        int totalcount = 0, totalquesavail = 0;
        if (Session["totalQuesCount_Rating"] != null)
            totalcount = int.Parse(Session["totalQuesCount_Rating"].ToString());
        if (Session["totalQuesAvailable_Rating"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable_Rating"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecountRating"] != null)
            pagecount = int.Parse(Session["pagecountRating"].ToString());

        int curindex = 0;
        curindex = (pagecount + 1) * quesperPage;
        if (curindex < curcount)
        {
            pagecount++;
            Session["pagecountRating"] = pagecount;SetCurrentPageCount();// 230110 bip
            FillQuestion(); 
        }
        else
        {
            GoToNextPage();
        }
    }
    private void SetCurrentPageCount()
    {
        testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());
        //  //// 220110 ... bip        
        string questiontype = "RatingType";
        dataclass.Procedure_UserTestPageIndex_Temp(userid, testId, testsectionid, 0, 0, pagecount, questiontype);
    }
    private Boolean CheckAnswer()
    {
        Boolean answercompleted = true;
        string message = "You have missed question No:";
        //string answer = "0";
        int answer = -1;
        if (lblQuesID1.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(1);//.ToString();
            if (answer < 0)
            {
                message += lblNo1.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

      //  return answercompleted;
        if (lblQuesID2.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(2);//.ToString();
            if (answer < 0)
            {
                message += lblNo2.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

       // return answercompleted;
        if (lblQuesID3.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(3);//.ToString();
            if (answer < 0)
            {
                message += lblNo3.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

       // return answercompleted;
        if (lblQuesID4.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(4);//.ToString();
            if (answer < 0)
            {
                message += lblNo4.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

       // return answercompleted;
        if (lblQuesID5.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(5);//.ToString();
            if (answer < 0)
            {
                message += lblNo5.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

       // return answercompleted;

        if (lblQuesID6.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(6);//.ToString();
            if (answer < 0)
            {
                message += lblNo6.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

       // return answercompleted;
        if (lblQuesID7.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(7);//.ToString();
            if (answer < 0)
            {
                message += lblNo7.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

       // return answercompleted;
        if (lblQuesID8.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(8);//.ToString();
            if (answer < 0)
            {
                message += lblNo8.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

        //return answercompleted;
        if (lblQuesID9.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(9);//.ToString();
            if (answer < 0)
            {
                message += lblNo9.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

       // return answercompleted;
        if (lblQuesID10.Text.Trim() != "")
        {
            answer = GetRatingtypeAnswer(10);//.ToString();
            if (answer < 0)
            {
                message += lblNo10.Text.Trim() + ", ";
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
    protected void ptnPrevious_Click(object sender, EventArgs e)
    {
        int curcount = 0;
        int totalcount = 0, totalquesavail = 0;
        if (Session["totalQuesCount_Rating"] != null)
            totalcount = int.Parse(Session["totalQuesCount_Rating"].ToString());
        if (Session["totalQuesAvailable_Rating"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable_Rating"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecountRating"] != null)
            pagecount = int.Parse(Session["pagecountRating"].ToString());

        int curindex = 0;
        curindex = (pagecount - 1) * quesperPage;
        if (curindex >= 0)
        {            
            pagecount--;
            Session["pagecountRating"] = pagecount; SetCurrentPageCount();// 230110 bip
        }
        else
        {
            GoToPreviousPage();            
        }
        FillQuestion();
    }

    private void GoToPreviousPage()
    {
        Session["AudioPrevious"] = "True";
        //Session["AudioCurrentpage"] = "0";
        Session["VideoPrevious"] = "True";
        //Session["VideoCurrentpage"] = "0";

        int testsectionid = 0;
        if (Session["CurrentTestSectionId"] != null)
        {
            testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());
        }        

        ClearSessionValues();
        Session["evaldirection"] = "Previous";
        //Session["pagecountRating"] = null;
        Session["SubCtrl"] = "VideoQuestions.ascx";
        Response.Redirect("FJAHome.aspx");
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        //;9037390734
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        pnlMessage.Visible = false; pnlMessage_confirm.Visible = true;
        btnSubmit.Visible = false; btnPrevious.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlMessage.Visible = false;
        btnSubmit.Visible = true; btnPrevious.Visible = true;
    }
    protected void btnYes_confirm_Click(object sender, EventArgs e)
    {
        pnlMessage.Visible = false; pnlMessage_confirm.Visible = false;        
        GoToNextPage();
    }
    protected void btnCancel_confirm_Click(object sender, EventArgs e)
    {
        pnlMessage_confirm.Visible = false; pnlMessage.Visible = false;
        btnSubmit.Visible = true; btnPrevious.Visible = true;
    }
    protected void btnYespopup_Click(object sender, EventArgs e)
    {        
        SaveValues(); pnlpopup.Visible = false;
        Timer1.Enabled = true;
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
                    //drTimedet["CompletionStatus"] = 0;
                    drTimedet["TimeUsed"] = hrs + ":" + min + ":" + sec;
                    drTimedet["AssignTime"] = setHrs + ":" + setMin + ":" + 0;
                    drTimedet["AssignStatus"] = 1;
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
            else return CheckTestSecVarGenValidity();

        }
        //else Session["AudioStatrtTime"] = DateTime.Now;

        return false;
    }
    private Boolean CheckTestSecVarGenValidity()
    {
        //if (Session["TestSectionVariableTimeDuration"] == null)
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
                            Session["FirstvarTimeDet"] = dtTimeDetails; break;
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
                        //dtTimeDetails.Rows.Add(drTimedet);

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

        // FillQuestion();

    }

    protected void btnYes_timer_Click(object sender, EventArgs e)
    {
        Session["timeExpired"] = null; Session["saved"] = null;
        //SaveAnswer();// bip 07052010
        SetNextSectionDetails_Timer();
    }
    protected void btnNo_timer_Click(object sender, EventArgs e)
    {
        Session["timeExpired"] = null; Session["saved"] = null;
        //SaveAnswer();// bip 07052010
        //// 230110 bip        
        dataclass.Procedure_DeleteUserTest_TempValues(userid, 0, 0);
        ////
        Session.Clear();
        Response.Redirect("FJAHome.aspx"); //bipson 18082010// Response.Redirect("CareerJudge.htm");
    }
    protected void btnYes_timer_Test_Click(object sender, EventArgs e)
    {
        Session["timeExpired"] = null; Session["saved"] = null;
        //SaveAnswer();// bip 07052010
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
        //SaveAnswer();// bip 07052010
        SetNextSectionVariableDetails_Timer();
    }
   
    protected void Timer1_Tick(object sender, EventArgs e)
    {

        /// bip 10052010
        if (Session["timeExpired"] != null)
            if (Session["timeExpired"].ToString() == "True")
                return;

        CheckTime();
        //SaveAnswer();
        ///

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Timer1.Enabled = true;
    }
}
