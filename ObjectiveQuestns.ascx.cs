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
using System.Numeric;

public partial class ObjectiveQuestns : System.Web.UI.UserControl
{   
    string usercode = "";
    int userid = 0;
    DBManagementClass clsclass = new DBManagementClass();
    AssesmentDataClassesDataContext dataclass = new  AssesmentDataClassesDataContext();
    
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
    int quesperPage =5;

    int testId = 0; int testsectionid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        /// bip 10052010
        if (Session["timeExpired"] != null)
            if (Session["timeExpired"].ToString() == "True")
            {
                CheckTime();
                return;
            }
        ///

        //if (CheckTime() == true) return;//bip 15052010

        if (Session["starttime"] == null)
            Session["starttime"] = DateTime.Now;

        if (Session["UserTestId"] != null)
        {
            //FillObjectiveQuestionInstructions();
            testId = int.Parse(Session["UserTestId"].ToString());
            if (Session["UserID"] != null)
                userid = int.Parse(Session["UserID"].ToString());

            SetTestTimeDetails();// 22-02-2010 bip

            FillQuestion();

            SetTestSectionTimeDetails();// 22-02-2010 bip
            SetTestSectionVariableTimeDetails();// 24-02-2010 bip
        }
    }    

    private void SetTestTimeDetails()
    {
        if (Session["TestStartTime"] == null)
            Session["TestStartTime"] = DateTime.Now;

        string testDuration = "";
        int testHrs = 0, testMin = 0;
        if (Session["TestTimeDuration"] == null)
        {
            var GetTestTimeDuration = from testtimedet in dataclass.TimerDetails
                                      where testtimedet.TestId == testId && testtimedet.TestSectionId == 0 && testtimedet.TestVariableId == 0
                                      select testtimedet;
            if (GetTestTimeDuration.Count() > 0)
            {
                if (GetTestTimeDuration.First().TimeHours != null)
                    testHrs = int.Parse(GetTestTimeDuration.First().TimeHours.ToString());
                if (GetTestTimeDuration.First().TimeMinutes != null)
                    testMin = int.Parse(GetTestTimeDuration.First().TimeMinutes.ToString());
                testDuration = testHrs + ":" + testMin;
                Session["TestTimeDuration"] = testDuration;
            }

            var GetTotalTestTimeRemains = from testtimedet in dataclass.TimerDetails
                                          where testtimedet.TestId == testId && testtimedet.TestSectionId > 0 && testtimedet.TestVariableId == 0
                                          select testtimedet;
            if (GetTotalTestTimeRemains.Count() > 0)
            {
                int totalAssnTestSecTimehrs = 0, totalAssnTestSecTimemin = 0;
                string totalAssnTestSecTime = "";
                foreach (var timedet in GetTotalTestTimeRemains)
                {
                    if (timedet.TimeHours != null)
                        totalAssnTestSecTimehrs += int.Parse(timedet.TimeHours.ToString());
                    if (timedet.TimeMinutes != null)
                        totalAssnTestSecTimemin += int.Parse(timedet.TimeMinutes.ToString());
                }
                if (totalAssnTestSecTimemin >= 60)
                {
                    double getHrs = float.Parse(totalAssnTestSecTimemin.ToString()) / 60;
                    string[] newHrs = getHrs.ToString().Split(new char[] { '.' });
                    if (newHrs.Length > 1)
                    {
                        int newhours = int.Parse(newHrs[0]);
                        totalAssnTestSecTimehrs += newhours;

                        float newMin = float.Parse(getHrs.ToString().Substring(1)) * 60;
                        string[] newMinutes = newMin.ToString().Split(new char[] { '.' });
                        int minutes = int.Parse(newMinutes[0]);
                        totalAssnTestSecTimemin = minutes;
                    }
                }
                totalAssnTestSecTime = totalAssnTestSecTimehrs + ":" + totalAssnTestSecTimemin;
               // Session["TotalTimeForAssgnSections"] = totalAssnTestSecTime;
                if (testDuration != "")
                {
                    TimeSpan tsTestTime = new TimeSpan(testHrs, testMin, 0);
                    TimeSpan tsTestSecTime = new TimeSpan(totalAssnTestSecTimehrs, totalAssnTestSecTimemin, 0);

                    TimeSpan tsTotal = tsTestTime.Subtract(tsTestSecTime);

                    int genTimeHrs = tsTotal.Hours;
                    int genTimeMin = tsTotal.Minutes;

                    string strGenTime = genTimeHrs + ":" + genTimeMin;
                    Session["TotalTimeForUnAssgnSections"] = strGenTime;
                }
            }
        }
    }
    private void SetTestSectionTimeDetails()// 24-02-2010 bip
    {
        int testsectionId = 0,cutestsecID=0;
        if (Session["CurrentTestSectionId"] != null)
            testsectionId = int.Parse(Session["CurrentTestSectionId"].ToString());
        if (testsectionId > 0)
        {
            if (Session["curTestSectionId_timer"] != null)
            {
                cutestsecID = int.Parse(Session["curTestSectionId_timer"].ToString());
                if (testsectionId == cutestsecID) return;
            }
            Session["curTestSectionId_timer"] = testsectionId.ToString();
        }

        //if (Session["TestSectionStartTime"] == null)
            Session["TestSectionStartTime"] = DateTime.Now;

        string testDuration = "";
        int testHrs = 0, testMin = 0;
        if (Session["TestSectionTimeDuration"] == null)
        {
            var GetTestTimeDuration = from testtimedet in dataclass.TimerDetails
                                      where testtimedet.TestId == testId && testtimedet.TestSectionId == testsectionid && testtimedet.TestVariableId == 0
                                      select testtimedet;
            if (GetTestTimeDuration.Count() > 0)
            {
                if (GetTestTimeDuration.First().TimeHours != null)
                    testHrs = int.Parse(GetTestTimeDuration.First().TimeHours.ToString());
                if (GetTestTimeDuration.First().TimeMinutes != null)
                    testMin = int.Parse(GetTestTimeDuration.First().TimeMinutes.ToString());
                testDuration = testHrs + ":" + testMin;
                Session["TestSectionTimeDuration"] = testDuration;
            }
        }
        // 24-02-2010 bip
        var GetTotalTestTimeRemains = from testtimedet in dataclass.TimerDetails
                                      where testtimedet.TestId == testId && testtimedet.TestSectionId == testsectionid && testtimedet.TestVariableId >0
                                      select testtimedet;
        if (GetTotalTestTimeRemains.Count() > 0)
        {
            int totalAssnTestSecVarTimehrs = 0, totalAssnTestSecVarTimemin = 0;
            string totalAssnTestSecVarTime = "";
            foreach (var timedet in GetTotalTestTimeRemains)
            {
                if (timedet.TimeHours != null)
                    totalAssnTestSecVarTimehrs += int.Parse(timedet.TimeHours.ToString());
                if (timedet.TimeMinutes != null)
                    totalAssnTestSecVarTimemin += int.Parse(timedet.TimeMinutes.ToString());
            }
            if (totalAssnTestSecVarTimemin >= 60)
            {
                double getHrs = float.Parse(totalAssnTestSecVarTimemin.ToString()) / 60;
                string[] newHrs = getHrs.ToString().Split(new char[] { '.' });
                if (newHrs.Length > 1)
                {
                    int newhours = int.Parse(newHrs[0]);
                    totalAssnTestSecVarTimehrs += newhours;

                    float newMin = float.Parse(getHrs.ToString().Substring(1)) * 60;
                    string[] newMinutes = newMin.ToString().Split(new char[] { '.' });
                    int minutes = int.Parse(newMinutes[0]);
                    totalAssnTestSecVarTimemin = minutes;
                }
            }
            totalAssnTestSecVarTime = totalAssnTestSecVarTimehrs + ":" + totalAssnTestSecVarTimemin;
            // Session["TotalTimeForAssgnSections"] = totalAssnTestSecTime;
            if (testDuration != "")
            {
                TimeSpan tsTestTime = new TimeSpan(testHrs, testMin, 0);
                TimeSpan tsTestSecTime = new TimeSpan(totalAssnTestSecVarTimehrs, totalAssnTestSecVarTimemin, 0);

                TimeSpan tsTotal = tsTestTime.Subtract(tsTestSecTime);

                int genTimeHrs = tsTotal.Hours;
                int genTimeMin = tsTotal.Minutes;
                //int genTimeSec = tsTotal.Seconds;
                string strGenTime = genTimeHrs + ":" + genTimeMin;// +":" + genTimeSec;// 25-02-2010
                Session["TotalTimeForUnAssgnSecVariables"] = strGenTime + ":" + 0;
            }
        }
        //

    }
    // 24-02-2010 bip
    private void SetTestSectionVariableTimeDetails()
    {
        int testsectionId = 0;
        if (Session["CurrentTestSectionId"] != null)
            testsectionId = int.Parse(Session["CurrentTestSectionId"].ToString());
        if (testsectionId <= 0) return;

        int testsectionvarId = 0, cutestsecvarID = 0;
        if (Session["CurrentTestFirstVariableId"] != null)
            testsectionvarId = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
        if (testsectionvarId > 0)
        {
            if (Session["curTestSectionVariableId_timer"] != null)
            {
                cutestsecvarID = int.Parse(Session["curTestSectionVariableId_timer"].ToString());
                if (testsectionvarId == cutestsecvarID) return;
            }
            Session["curTestSectionVariableId_timer"] = testsectionvarId.ToString();
        }

        //if (Session["TestSectionStartTime"] == null)
        Session["TestSectionVariableStartTime"] = DateTime.Now;

        string testDuration = "";
        if (Session["TestSectionVariableTimeDuration"] == null)
        {
            var GetTestTimeDuration = from testtimedet in dataclass.TimerDetails
                                      where testtimedet.TestId == testId && testtimedet.TestSectionId == testsectionid && testtimedet.TestVariableId == testsectionvarId
                                      select testtimedet;
            if (GetTestTimeDuration.Count() > 0)
            {
                if (GetTestTimeDuration.First().TimeHours != null)
                    testDuration = GetTestTimeDuration.First().TimeHours.ToString();
                if (GetTestTimeDuration.First().TimeMinutes != null)
                    testDuration = testDuration + ":" + GetTestTimeDuration.First().TimeMinutes.ToString();

                Session["TestSectionVariableTimeDuration"] = testDuration + ":" + 0;
            }
        }
    }
    //
    private void FillObjectiveQuestionInstructions()
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
            quesrystring = "select InstructionDetails,InstructionImage1,InstructionDetails2,InstructionImage2,InstructionDetails3 from TestSectionVariablewiseInstructions where CategoryId =1 and TestId=" + testId + " and TestSectionId =" + testSectionID + " and FirstVariableId=" + testFirstVariableId + " and SecondVariableId =" + testSecondVariableId;
        else if (testFirstVariableId > 0)
            quesrystring = "select InstructionDetails,InstructionImage1,InstructionDetails2,InstructionImage2,InstructionDetails3 from TestSectionVariablewiseInstructions where CategoryId =1 and TestId=" + testId + " and TestSectionId =" + testSectionID + " and FirstVariableId=" + testFirstVariableId;

        dsInstructiondet = clsclass.GetValuesFromDB(quesrystring);

        if (dsInstructiondet != null) if (dsInstructiondet.Tables.Count > 0) if (dsInstructiondet.Tables[0].Rows.Count > 0)
                {
                    //divInstructions.InnerHtml = dsInstructiondet.Tables[0].Rows[0]["InstructionDetails"].ToString(); valueexists = true; 
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
                                 where instructiondet.CategoryId == 1 
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
                                 where instructiondet.CategoryId == 1
                                 select instructiondet;
        if (InstructionDetails.Count() > 0)
        {
            if (InstructionDetails.First().Title != null)            
                divtitle.InnerHtml = InstructionDetails.First().Title.ToString();
            
            if (InstructionDetails.First().InstructionDetails != null)            
                divInstructions.InnerHtml = InstructionDetails.First().InstructionDetails.ToString();            
        }
    }

    private DataSet GetQuestionList(int testsectionId)//, int testFirstVariableId,int testSecondVariableId)
    {
        // bip 07122009        

        string firstVariableName = ""; string secondVariableName = "";
        if (Session["TestFirstVariableName"] != null || Session["TestFirstVariableName"] != "")
            firstVariableName = Session["TestFirstVariableName"].ToString();
        if (Session["TestSecondVariableName"] != null || Session["TestSecondVariableName"] != "")
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

        DataRow drQurstionList;
        // bip 08122009;
        if (secondVariableName != "")
            querystring = "select distinct ObjQuestionCount,SectionId from QuestionCount where ObjQuestionCount>0 and TestId=" + testId + " and TestSectionId=" + testsectionId + " and SectionName='" + firstVariableName + "' and SectionNameSub1='" + secondVariableName + "' order by sectionid";
        else if (firstVariableName != "")
            querystring = "select distinct ObjQuestionCount,SectionId from QuestionCount where ObjQuestionCount>0 and TestId=" + testId + " and TestSectionId=" + testsectionId + " and SectionName='" + firstVariableName + "' order by sectionid";

       // lblmessage.Text = " quesry= " + querystring;//bipson 11092010

        DataSet dsQuestioncount = new DataSet();

        dsQuestioncount = clsclass.GetValuesFromDB(querystring);        

        if (dsQuestioncount != null)
            if (dsQuestioncount.Tables.Count > 0)
                if (dsQuestioncount.Tables[0].Rows.Count > 0)
                {
                    
                    for (int c = 0; c < dsQuestioncount.Tables[0].Rows.Count; c++)//                    
                    {                        
                        int objQuescount = int.Parse(dsQuestioncount.Tables[0].Rows[c]["ObjQuestionCount"].ToString());
                        int sectionid = int.Parse(dsQuestioncount.Tables[0].Rows[c]["SectionId"].ToString());
                        if (objQuescount > 0)
                        {
                            querystring = "SELECT TOP (" + objQuescount + ") QuestionID,Question,Answer,Option1,Option2,Option3,Option4,Option5,Option6,Option7,Option8,Option9,Option10  FROM View_TestBaseQuestionList where Category = 'Objective' AND TestBaseQuestionStatus=1 AND TestSectionId=" + testsectionId + " AND SectionId=" + sectionid + " AND TestId=" + testId + " ORDER BY RAND((100*QuestionID)*DATEPART(millisecond, GETDATE())) ";
                             
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

                                        drQurstionList["Option6"] = dsQuesCount.Tables[0].Rows[i]["Option6"];
                                        drQurstionList["Option7"] = dsQuesCount.Tables[0].Rows[i]["Option7"];
                                        drQurstionList["Option8"] = dsQuesCount.Tables[0].Rows[i]["Option8"];
                                        drQurstionList["Option9"] = dsQuesCount.Tables[0].Rows[i]["Option9"];
                                        drQurstionList["Option10"] = dsQuesCount.Tables[0].Rows[i]["Option10"];
                                        dtQuestionList.Rows.Add(drQurstionList);
                                    }
                                }
                        }
                    }
                }

        DataSet dsQuestionList = new DataSet();
        if (dtQuestionList.Rows.Count > 0)
        {           

            dsQuestionList.Tables.Add(dtQuestionList); Session["questionColl"] = dsQuestionList;
            Session["totalQuesCount"] = dsQuestionList.Tables[0].Rows.Count.ToString();
        }
        else { Session["questionColl"] = null; dsQuestionList = null; }

    

        return dsQuestionList; 
    }



    private int GetTestVariableId(int testsectionId)
    {
        int testFirstVariableId = 0; int testSecondVariableId = 0; int testVariableId = 0;
        try
        {
            DataSet dsTestVariableIds = new DataSet();
            if (Session["dsTestVariableIds"] != null)
            {
                dsTestVariableIds = (DataSet)Session["dsTestVariableIds"];
            }
            else
            {
                Session["questionColl"] = null;// bip 10122009

                string querystringSec = "";
                querystringSec = "select distinct  testid,testsectionid, sectionname,sectionnamesub1,id from questioncount where TestId=" + testId + " and Testsectionid =" + testsectionId + " group by sectionname,sectionnamesub1,testid,testsectionid,id order by id asc ";

                DataSet dsTestVariableIds1 = new DataSet();
                dsTestVariableIds1 = clsclass.GetValuesFromDB(querystringSec);
                GridView1.DataSource = dsTestVariableIds1.Tables[0];
                GridView1.DataBind();

                DataTable dtVariableList = new DataTable();
                dtVariableList.Columns.Add("FirstVariableName");
                dtVariableList.Columns.Add("SecondVariableName");
                dtVariableList.Columns.Add("FirstVariableId");
                dtVariableList.Columns.Add("SecondVariableId");

                DataRow drVariables;

                string firstvariablename = "", secondvariablename = "";
                int firstvariableid = 0, secondvariableid = 0;

                if (GridView1.Rows.Count > 0)
                {
                    for (int v = 0; v < GridView1.Rows.Count; v++)
                    {

                        if (GridView1.Rows[v].Cells[0].Text != "&nbsp;" && GridView1.Rows[v].Cells[0].Text != "")
                            firstvariablename = GridView1.Rows[v].Cells[0].Text.Replace("&amp;", "&");// bip 09072011

                        if (GridView1.Rows[v].Cells[1].Text != "&nbsp;" && GridView1.Rows[v].Cells[1].Text != "")
                            secondvariablename = GridView1.Rows[v].Cells[1].Text.Replace("&amp;", "&");// bip 09072011
                        bool variableExists = false;
                        if (secondvariablename != "")
                        {
                            if (dtVariableList.Rows.Count > 0)
                                for (int cnt = 0; cnt < dtVariableList.Rows.Count; cnt++)
                                {
                                    string fstVarName = "", scndVarName = "";
                                    fstVarName = dtVariableList.Rows[cnt]["FirstVariableName"].ToString();
                                    scndVarName = dtVariableList.Rows[cnt]["SecondVariableName"].ToString();
                                    if (fstVarName == firstvariablename && scndVarName == secondvariablename)
                                    { variableExists = true; break; }

                                }
                        }
                        else if (firstvariablename != "")
                        {
                            for (int cnt = 0; cnt < dtVariableList.Rows.Count; cnt++)
                            {
                                string fstVarName = "", scndVarName = "";
                                fstVarName = dtVariableList.Rows[cnt]["FirstVariableName"].ToString();
                                scndVarName = dtVariableList.Rows[cnt]["SecondVariableName"].ToString();
                                if (fstVarName == firstvariablename && scndVarName == "")
                                { variableExists = true; break; }

                            }
                        }
                        else continue;

                        if (variableExists == true) continue;

                        if (firstvariablename != "")
                        {
                            var getSecondVariableName = from variabledet in dataclass.SectionDetails
                                                        where variabledet.SectionName == firstvariablename && variabledet.ParentId == 0
                                                        select variabledet;
                            if (getSecondVariableName.Count() > 0)
                            {
                                firstvariableid = int.Parse(getSecondVariableName.First().SectionId.ToString());
                            }
                            if (firstvariableid > 0)
                            {
                                if (secondvariablename != "")
                                {
                                    var getSecondVariableName1 = from variabledet in dataclass.SectionDetails
                                                                 where variabledet.SectionName == secondvariablename && variabledet.ParentId == firstvariableid
                                                                 select variabledet;
                                    if (getSecondVariableName1.Count() > 0)
                                    {
                                        secondvariableid = int.Parse(getSecondVariableName1.First().SectionId.ToString());
                                    }
                                }
                            }
                        }

                        drVariables = dtVariableList.NewRow();
                        drVariables["FirstVariableName"] = firstvariablename;
                        drVariables["SecondVariableName"] = secondvariablename;
                        drVariables["FirstVariableId"] = firstvariableid.ToString();
                        drVariables["SecondVariableId"] = secondvariableid.ToString();

                        dtVariableList.Rows.Add(drVariables);
                    }
                    ////dtVariableList.DefaultView.Sort = "FirstVariableId asc";
                    //DataView dv = new DataView(dtVariableList);
                    //dv.Sort = "FirstVariableId asc";
                    //dtVariableList = dv.ToTable();
                }
                dsTestVariableIds = new DataSet();
                dsTestVariableIds.Tables.Add(dtVariableList);
                if (dsTestVariableIds != null)
                {
                    if (dsTestVariableIds.Tables[0].Rows.Count > 0)
                    {
                        testFirstVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[0]["FirstVariableId"].ToString());
                        testSecondVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[0]["SecondVariableId"].ToString());
                        Session["dsTestVariableIds"] = dsTestVariableIds;
                        Session["TestFirstVariableName"] = dsTestVariableIds.Tables[0].Rows[0]["FirstVariableName"].ToString();
                        Session["TestSecondVariableName"] = dsTestVariableIds.Tables[0].Rows[0]["SecondVariableName"].ToString();
                    }
                    else Session["dsTestVariableIds"] = null;
                }
                else Session["dsTestVariableIds"] = null;
            }
            if (dsTestVariableIds == null) return 0;
            int TotalVariableCount = 1;
            if (dsTestVariableIds.Tables[0].Rows.Count > 0)
            {
                Session["TotalVariableCount"] = dsTestVariableIds.Tables[0].Rows.Count.ToString();
                TotalVariableCount = dsTestVariableIds.Tables[0].Rows.Count;

                int variableIdPageNo = 1;
                if (Session["VariableIdIndexNo"] != null)
                {
                    variableIdPageNo = int.Parse(Session["VariableIdIndexNo"].ToString());
                    if (variableIdPageNo <= 0) return 0;

                    // 27-02-2010 bip starts ...

                    if (Session["FirstVariableIdForTimer"] != null)
                    {
                        int firstvaridTimer = 0;
                        int firstvarid = 0;
                        firstvaridTimer = int.Parse(Session["FirstVariableIdForTimer"].ToString());
                        if (Session["CurrentTestFirstVariableId"] != null)
                            firstvarid = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
                        if (firstvaridTimer != firstvarid)
                        {
                            int curfirstvarid;
                            int timeindex = 0; int pagenum = variableIdPageNo;
                            variableIdPageNo = int.Parse(Session["VariableIdIndexNo"].ToString());

                            int varidindex = 0, varidindex_timer = 0;
                            varidindex = int.Parse(Session["VariableIdIndexNo"].ToString());
                            varidindex_timer = int.Parse(Session["VariableIdIndexNo_timer"].ToString());

                            int pgstartindex = variableIdPageNo;
                            for (int t = 0; t < dsTestVariableIds.Tables[0].Rows.Count; t++)
                            {
                                if (varidindex != varidindex_timer)
                                {
                                    if (t < varidindex) continue;
                                }
                                Session["VariableIdIndexNo"] = t + 1; //variableIdPageNo;
                                Session["VariableIdIndexNo_timer"] = t + 1;// bip 12052010
                                curfirstvarid = int.Parse(dsTestVariableIds.Tables[0].Rows[t]["FirstVariableId"].ToString());

                                //if (curfirstvarid > firstvarid)
                                if (curfirstvarid != firstvarid)// bip 12052010
                                {
                                    //Session["VariableIdIndexNo"] = variableIdPageNo + 1;
                                    testFirstVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[t]["FirstVariableId"].ToString());
                                    testSecondVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[t]["SecondVariableId"].ToString());

                                    Session["TestFirstVariableName"] = dsTestVariableIds.Tables[0].Rows[t]["FirstVariableName"].ToString();
                                    Session["TestSecondVariableName"] = dsTestVariableIds.Tables[0].Rows[t]["SecondVariableName"].ToString();

                                    //Session["curTestSectionVariableId_timer"] = null;// 04-03-2010 bip
                                    Session["FirstVariableIdForTimer"] = null;// 04-03-2010 bip
                                    break;
                                }

                            }
                            //}
                        }
                        else
                            if (variableIdPageNo <= TotalVariableCount)
                            {
                                testFirstVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["FirstVariableId"].ToString());
                                testSecondVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["SecondVariableId"].ToString());

                                Session["TestFirstVariableName"] = dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["FirstVariableName"].ToString();
                                Session["TestSecondVariableName"] = dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["SecondVariableName"].ToString();
                            }
                    }

                    else
                        if (variableIdPageNo <= TotalVariableCount)
                        {
                            testFirstVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["FirstVariableId"].ToString());
                            testSecondVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["SecondVariableId"].ToString());

                            Session["TestFirstVariableName"] = dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["FirstVariableName"].ToString();
                            Session["TestSecondVariableName"] = dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["SecondVariableName"].ToString();
                        }

                    //Session["sectionIdIndexNo"] = (sectionIdIndexNo + 1).ToString();           
                }
                else
                {
                    Session["VariableIdIndexNo"] = "1";
                    Session["VariableIdIndexNo_timer"] = "1";// bip 12052010
                    testFirstVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[0]["FirstVariableId"].ToString());
                    testSecondVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[0]["SecondVariableId"].ToString());

                    Session["TestFirstVariableName"] = dsTestVariableIds.Tables[0].Rows[0]["FirstVariableName"].ToString();
                    Session["TestSecondVariableName"] = dsTestVariableIds.Tables[0].Rows[0]["SecondVariableName"].ToString();
                }
                //
                if (testSecondVariableId > 0)
                {
                    if (Session["CurrentTestSecondVariableId"] != null)
                    {
                        if (Session["CurrentTestSecondVariableId"].ToString() != testSecondVariableId.ToString())
                        { ClearDataSetValues(); ClearAllPageCountValues(0); }
                    }
                    else if (Session["CurrentTestFirstVariableId"] != null)
                        if (Session["CurrentTestFirstVariableId"].ToString() != testFirstVariableId.ToString())
                        { ClearDataSetValues(); ClearAllPageCountValues(0); }
                }
                //

                if (testSecondVariableId > 0)
                    Session["CurrentTestSecondVariableId"] = testSecondVariableId;
                // else Session["CurrentTestSecondVariableId"] = null;// 07-03-2010 bip
                if (testFirstVariableId > 0)
                {
                    Session["CurrentTestFirstVariableId"] = testFirstVariableId;
                    Session["FirstVariableIdForTimer"] = testFirstVariableId;
                }
                //  else Session["CurrentTestFirstVariableId"] = null;// 07-03-2010 bip

                if (variableIdPageNo > TotalVariableCount)
                {
                    // Session["dsTestVariableIds"] = null;// bip 10122009
                    return 0;
                } //{ GoToResultPage(); return 0; }

                if (testSecondVariableId > 0)
                    testVariableId = testSecondVariableId;
                else if (testFirstVariableId > 0)
                    testVariableId = testFirstVariableId;
            }

        }
        catch (Exception ex) {return testVariableId; }
        return testVariableId;

    }

    private void SetNextSectionDetails()
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
        Session["VariableIdIndexNo"] = null;
        Session["VariableIdIndexNo_timer"] = null;// bip 12052010

        Session["dsTestVariableIds"] = null;

        Session["questionColl"] = null;// bip 10122009       

        FillQuestion();


    }

    private int GetTestSectionId()
    {
        int testsectionId = 0;
        try
        {
            DataSet dsTestSectionIdCount;
            if (Session["dsTestSectionIds"] != null)
            {
                dsTestSectionIdCount = (DataSet)Session["dsTestSectionIds"];
            }
            else
            {
                string condition = "";
                var getTestSectiondetails = from testsecdet in dataclass.UserTestSectionDetails
                                            where testsecdet.UserId == userid && testsecdet.TestId == testId
                                            select testsecdet;
                if (getTestSectiondetails.Count() > 0)
                {
                    foreach (var testsecids in getTestSectiondetails)
                    {
                        // if (condition != "")
                        condition += " and ";

                        condition += " TestSectionId <> " + testsecids.TestSectionId;
                    }
                }

                if (Session["CurrentTestSectionId"] != null)
                    testsectionId = int.Parse(Session["CurrentTestSectionId"].ToString());
                string querystringSec = "";
                if (testsectionId > 0)
                    querystringSec = "SELECT distinct  Testsectionid FROM TestBaseQuestionList where TestId=" + testId + " and Testsectionid > " + testsectionId;// ORDER BY RAND((1000*QuestionID)*DATEPART(millisecond, GETDATE())) ";
                else querystringSec = "SELECT distinct  Testsectionid FROM TestBaseQuestionList where TestId=" + testId + " and Testsectionid >0 ";// ORDER BY RAND((1000*QuestionID)*DATEPART(millisecond, GETDATE())) ";

                if (condition != "")
                    querystringSec = querystringSec + condition;

                //string querystringSec = "SELECT distinct  Testsectionid FROM TestBaseQuestionList where TestId=" + testId + " and Testsectionid >0 ";// ORDER BY RAND((1000*QuestionID)*DATEPART(millisecond, GETDATE())) ";
                dsTestSectionIdCount = new DataSet();
                dsTestSectionIdCount = clsclass.GetValuesFromDB(querystringSec);
                if (dsTestSectionIdCount != null)
                {
                    if (dsTestSectionIdCount.Tables[0].Rows.Count > 0)
                    {
                        testsectionId = int.Parse(dsTestSectionIdCount.Tables[0].Rows[0]["Testsectionid"].ToString());
                        Session["dsTestSectionIds"] = dsTestSectionIdCount;
                    }
                    else Session["dsTestSectionIds"] = null;
                }
                else Session["dsTestSectionIds"] = null;
            }
            if (dsTestSectionIdCount == null) return 0;
            int TotalSectionCount = 1;
            if (dsTestSectionIdCount.Tables[0].Rows.Count > 0)
            {
                Session["TotalSectionCount"] = dsTestSectionIdCount.Tables[0].Rows.Count.ToString();
                TotalSectionCount = dsTestSectionIdCount.Tables[0].Rows.Count;

                int sectionIdPageNo = 1;
                if (Session["sectionIdIndexNo"] != null)
                {
                    sectionIdPageNo = int.Parse(Session["sectionIdIndexNo"].ToString());
                    if (sectionIdPageNo <= TotalSectionCount)
                        testsectionId = int.Parse(dsTestSectionIdCount.Tables[0].Rows[sectionIdPageNo - 1]["testsectionId"].ToString());
                    //Session["sectionIdIndexNo"] = (sectionIdIndexNo + 1).ToString();           
                }
                else
                {
                    Session["sectionIdIndexNo"] = "1";
                    testsectionId = int.Parse(dsTestSectionIdCount.Tables[0].Rows[0]["testsectionId"].ToString());
                }
                //lblmessage.Text += "testsecid= " + testsectionId;
                Session["CurrentTestSectionId"] = testsectionId.ToString();
                if (sectionIdPageNo > TotalSectionCount) { GoToResultPage(); return 0; }

            }

        }
        catch (Exception ex) {return testsectionId; }
        return testsectionId;
    }

    private void GoToResultPage()
    {
        Session["SubCtrl"] = "ThankYou.ascx";
        Response.Redirect("FJAHome.aspx");
    }
    private void PreviousPageSelection()
    {
        int testsectionId = 0, TotalSectionCount = 0;
        DataSet dsTestSectionIdCount = new DataSet();
        if (Session["dsTestSectionIds"] != null)
        {
            dsTestSectionIdCount = (DataSet)Session["dsTestSectionIds"];
        }
        if (dsTestSectionIdCount != null)
            if (dsTestSectionIdCount.Tables.Count > 0)
                if (dsTestSectionIdCount.Tables[0].Rows.Count > 0)
                {
                    Session["TotalSectionCount"] = dsTestSectionIdCount.Tables[0].Rows.Count.ToString();
                    TotalSectionCount = dsTestSectionIdCount.Tables[0].Rows.Count;

                    int sectionIdIndexNo = 1;
                    if (Session["sectionIdIndexNo"] != null)
                    {
                        sectionIdIndexNo = int.Parse(Session["sectionIdIndexNo"].ToString());
                        //sectionIdIndexNo = (sectionIdIndexNo - 1);//commented by bip 200110

                        if (sectionIdIndexNo < 0) return;// bip 200110

                        Session["sectionIdIndexNo"] = sectionIdIndexNo.ToString();//commented by bip 200110
                        if (sectionIdIndexNo > 0)
                            testsectionId = int.Parse(dsTestSectionIdCount.Tables[0].Rows[sectionIdIndexNo - 1]["testsectionId"].ToString());
                    }
                }
        if (testsectionId > 0)
        {
            Session["CurrentTestSectionId"] = testsectionId.ToString();
            CheckPreviousVariableAccess();
            //GoToLastPage(); 
            return;
        }
        else
        {
            CheckPreviousVariableAccess();
            //GoToPreviousPage(); 
            return;
        }
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
        int questionid = 0; string questiontype = "Objective";

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

                    querystringquesTemp = "SELECT QuestionID,Question,Answer,Option1,Option2,Option3,Option4,Option5,Option6,Option7,Option8,Option9,Option10  FROM View_TestBaseQuestionList where " + questionids;
                    dsTempData = clsclass.GetValuesFromDB(querystringquesTemp);
                    Session["questionColl"] = dsTempData;

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
                            Session["pagecount"] = pagecount;                            
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
            
            Session["ValueExists"] = "null";// "False";

            ClearControls();
            string querystring = "";
            //int totalQues = 0;
            int QuestionID = 0;            

            //if (Session["pagecount"] != null)
            //    pagecount = int.Parse(Session["pagecount"].ToString());

            // 10122009
            testsectionid = GetTestSectionId(); //return;// 011209

            if (testsectionid <= 0)
            {
                GoToResultPage(); return;
            }

            int testVariableId = 0;
            testVariableId = GetTestVariableId(testsectionid);            

            if (testVariableId == 0)
            {
                Timer1.Enabled = false;

                pnlMessage.Visible = true;
                btnSubmit.Visible = false; btnPrevious.Visible = false; return;
            }

            SetTestSectionTimeDetails();// 22-02-2010 bip
            SetTestSectionVariableTimeDetails();// 24-02-2010 bip

            int testSecondVariableId = 0, testFirstVariableId = 0;
            if (Session["CurrentTestSecondVariableId"] != null)
                testSecondVariableId = int.Parse(Session["CurrentTestSecondVariableId"].ToString());
            if (Session["CurrentTestFirstVariableId"] != null)
                testFirstVariableId = int.Parse(Session["CurrentTestFirstVariableId"].ToString());           

            ds = new DataSet();
            //da.Fill(ds);
            if (Session["questionColl"] != null)
            {

                ds = (DataSet)Session["questionColl"];

                testVariableId = GetTestVariableId(testsectionid);
                if (testVariableId == 0)
                {

                    SetNextSectionDetails(); return;                   
                }
            }
            else
            {                
                //
                ds = GetTempData();
                bool newentry = false;
                if (ds == null)
                {
                    newentry = true;
                    ds = GetQuestionList(testsectionid);
                }
                else if (ds.Tables.Count <= 0)
                { newentry = true; ds = GetQuestionList(testsectionid); }

               // return;//bipson 11092010


                if (ds == null)
                {
                    //lblmessage.Text += " errortest num6..  ";

                    string evaldirection = "Next";
                    if (Session["evaldirection"] != null)
                        evaldirection = Session["evaldirection"].ToString();
                    if (evaldirection == "Next")
                        GoToNextPage();
                    else
                    {
                        PreviousPageSelection(); return;
                    }

                }
                else // store the questiondetails in a temp table.
                {
                    int questionid = 0; string questiontype = "Objective";
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
            if (ds == null)
            {
                //lblmessage.Text += " errortest num6..  ";

                string evaldirection = "Next";
                if (Session["evaldirection"] != null)
                    evaldirection = Session["evaldirection"].ToString();
                if (evaldirection == "Next")
                    GoToNextPage();
                else
                {
                    PreviousPageSelection(); return;
                }
            }
            if (Session["pagecount"] != null)
                pagecount = int.Parse(Session["pagecount"].ToString());
            int slno = 0;
            slno = pagecount * quesperPage + 1;
            if (slno < 0)
                slno = 1;

            int pagecnt = 0;
            int curntquescnt = 0;
            int slnos = 0;

            //lblmessage.Text += " errortest num7..  ";

            
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        FillObjectiveQuestionInstructions(); ////lblmessage.Text += " welcome1001.. ";

                        Session["totalQuesAvailable"] = ds.Tables[0].Rows.Count.ToString();

                        int j = 0;
                        for (int i = slno - 1; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (j >= quesperPage) break;
                            string Answer = "";
                            Session["CurrentControlCtrl"] = "ObjectiveQuestns.ascx";// bip 08012010
                            Session["ValueExists"] = "True";

                            int optindex = 1;
                            switch (j)
                            {
                                case 0:
                                    optindex = 1;
                                    // slnos = CheckSlNo();
                                    lblNo1.Text = slno.ToString() + ".  ";
                                    //lblQues1.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                                    tcellQues1.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                                    lblQuesID1.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                                    var Ques1 = from Ques in dataclass.EvaluationResults
                                                where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                                Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                                select Ques;//Ques.UserCode == usercode &&
                                    if (Ques1.Count() > 0)
                                        if (Ques1.First().Answer != null)
                                            Answer = Ques1.First().Answer.ToString();
                                    if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                    {
                                        rbQues1Answer1.Visible = true; lblA1.Visible = true;
                                        rbQues1Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                            rbQues1Answer1.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                    {
                                        if (optindex != 2)
                                            lblB1.Text = GetAnswerOptionOrder(optindex);
                                        rbQues1Answer2.Visible = true; lblB1.Visible = true;
                                        rbQues1Answer2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                            rbQues1Answer2.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                    {
                                        if (optindex != 3)
                                            lblC1.Text = GetAnswerOptionOrder(optindex);
                                        rbQues1Answer3.Visible = true; lblC1.Visible = true;
                                        rbQues1Answer3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                            rbQues1Answer3.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                    {
                                        if (optindex != 4)
                                            lblD1.Text = GetAnswerOptionOrder(optindex);
                                        rbQues1Answer4.Visible = true; lblD1.Visible = true;
                                        rbQues1Answer4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                            rbQues1Answer4.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                    {
                                        if (optindex != 5)
                                            lblE1.Text = GetAnswerOptionOrder(optindex);
                                        rbQues1Answer5.Visible = true; lblE1.Visible = true;
                                        rbQues1Answer5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                            rbQues1Answer5.Checked = true;

                                    }

                                     if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                                    {
                                        if (optindex != 6)
                                            lblF1.Text = GetAnswerOptionOrder(optindex);
                                        rbQues1Answer6.Visible = true; lblF1.Visible = true;
                                        rbQues1Answer6.Text = ds.Tables[0].Rows[i]["Option6"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                            rbQues1Answer6.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                                    {
                                        if (optindex != 7)
                                            lblG1.Text = GetAnswerOptionOrder(optindex);
                                        rbQues1Answer7.Visible = true; lblG1.Visible = true;
                                        rbQues1Answer7.Text = ds.Tables[0].Rows[i]["Option7"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                            rbQues1Answer7.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                                    {
                                        if (optindex != 8)
                                            lblH1.Text = GetAnswerOptionOrder(optindex);
                                        rbQues1Answer8.Visible = true; lblH1.Visible = true;
                                        rbQues1Answer8.Text = ds.Tables[0].Rows[i]["Option8"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                            rbQues1Answer8.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                                    {
                                        if (optindex != 9)
                                            lblI1.Text = GetAnswerOptionOrder(optindex);
                                        rbQues1Answer9.Visible = true; lblI1.Visible = true;
                                        rbQues1Answer9.Text = ds.Tables[0].Rows[i]["Option9"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                            rbQues1Answer9.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                                    {
                                        if (optindex != 10)
                                            lblJ1.Text = GetAnswerOptionOrder(optindex);
                                        rbQues1Answer10.Visible = true; lblJ1.Visible = true;
                                        rbQues1Answer10.Text = ds.Tables[0].Rows[i]["Option10"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                            rbQues1Answer10.Checked = true;

                                    }

                                    curntquescnt++;
                                    j++;
                                    pnlQuestion1.Visible = true;
                                    break;

                                case 1:
                                    optindex = 1;
                                    lblNo2.Text = (slno + 1).ToString() + ".  ";
                                    //lblQues2.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                                    tcellQues2.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                                    lblQuesID2.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                                    var Ques2 = from Ques in dataclass.EvaluationResults
                                                where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                                Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                                select Ques;
                                    if (Ques2.Count() > 0)
                                        if (Ques2.First().Answer != null)
                                            Answer = Ques2.First().Answer.ToString();
                                    if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                    {
                                        rbQues2Answer1.Visible = true; lblA2.Visible = true;
                                        rbQues2Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                            rbQues2Answer1.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                    {
                                        if (optindex != 2)
                                            lblB2.Text = GetAnswerOptionOrder(optindex);
                                        rbQues2Answer2.Visible = true; lblB2.Visible = true;
                                        rbQues2Answer2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                            rbQues2Answer2.Checked = true; optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                    {
                                        if (optindex != 3)
                                            lblC2.Text = GetAnswerOptionOrder(optindex);
                                        rbQues2Answer3.Visible = true; lblC2.Visible = true;
                                        rbQues2Answer3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                            rbQues2Answer3.Checked = true;
                                        optindex++;
                                    }

                                    //radioQues2.Items.Add(ds.Tables[0].Rows[i]["Option3"].ToString());
                                    if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                    {
                                        if (optindex != 4)
                                            lblD2.Text = GetAnswerOptionOrder(optindex);
                                        rbQues2Answer4.Visible = true; lblD2.Visible = true;
                                        rbQues2Answer4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                            rbQues2Answer4.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                    {
                                        if (optindex != 5)
                                            lblE2.Text = GetAnswerOptionOrder(optindex);
                                        rbQues2Answer5.Visible = true; lblE2.Visible = true;
                                        rbQues2Answer5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                            rbQues2Answer5.Checked = true;
                                    }

                                     if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                                    {
                                        if (optindex != 6)
                                            lblF2.Text = GetAnswerOptionOrder(optindex);
                                        rbQues2Answer6.Visible = true; lblF2.Visible = true;
                                        rbQues2Answer6.Text = ds.Tables[0].Rows[i]["Option6"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                            rbQues2Answer6.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                                    {
                                        if (optindex != 7)
                                            lblG2.Text = GetAnswerOptionOrder(optindex);
                                        rbQues2Answer7.Visible = true; lblG2.Visible = true;
                                        rbQues2Answer7.Text = ds.Tables[0].Rows[i]["Option7"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                            rbQues2Answer7.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                                    {
                                        if (optindex != 8)
                                            lblH2.Text = GetAnswerOptionOrder(optindex);
                                        rbQues2Answer8.Visible = true; lblH2.Visible = true;
                                        rbQues2Answer8.Text = ds.Tables[0].Rows[i]["Option8"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                            rbQues2Answer8.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                                    {
                                        if (optindex != 9)
                                            lblI2.Text = GetAnswerOptionOrder(optindex);
                                        rbQues2Answer9.Visible = true; lblI2.Visible = true;
                                        rbQues2Answer9.Text = ds.Tables[0].Rows[i]["Option9"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                            rbQues2Answer9.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                                    {
                                        if (optindex != 10)
                                            lblJ2.Text = GetAnswerOptionOrder(optindex);
                                        rbQues2Answer10.Visible = true; lblJ2.Visible = true;
                                        rbQues2Answer10.Text = ds.Tables[0].Rows[i]["Option10"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                            rbQues2Answer10.Checked = true;

                                    }

                                    curntquescnt++;
                                    j++;
                                    pnlQuestion2.Visible = true;
                                    break;

                                case 2:
                                    optindex = 1;
                                    lblNo3.Text = (slno + 2).ToString() + ".  ";
                                    //lblQues3.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                                    tcellQues3.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                                    lblQuesID3.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                                    var Ques3 = from Ques in dataclass.EvaluationResults
                                                where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                                Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                                select Ques;
                                    if (Ques3.Count() > 0)
                                        if (Ques3.First().Answer != null)
                                            Answer = Ques3.First().Answer.ToString();
                                    if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                    {
                                        rbQues3Answer1.Visible = true; lblA3.Visible = true;
                                        rbQues3Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                            rbQues3Answer1.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                    {
                                        if (optindex != 2)
                                            lblB3.Text = GetAnswerOptionOrder(optindex);
                                        rbQues3Answer2.Visible = true; lblB3.Visible = true;
                                        rbQues3Answer2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                            rbQues3Answer2.Checked = true;
                                        optindex++;
                                    }

                                    //radioQues3.Items.Add(ds.Tables[0].Rows[i]["Option2"].ToString());
                                    if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                    {
                                        if (optindex != 3)
                                            lblC3.Text = GetAnswerOptionOrder(optindex);
                                        rbQues3Answer3.Visible = true; lblC3.Visible = true;
                                        rbQues3Answer3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                            rbQues3Answer3.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                    {
                                        if (optindex != 4)
                                            lblD3.Text = GetAnswerOptionOrder(optindex);
                                        rbQues3Answer4.Visible = true; lblD3.Visible = true;
                                        rbQues3Answer4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                            rbQues3Answer4.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                    {
                                        if (optindex != 5)
                                            lblE3.Text = GetAnswerOptionOrder(optindex);
                                        rbQues3Answer5.Visible = true; lblE3.Visible = true;
                                        rbQues3Answer5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                            rbQues3Answer5.Checked = true;
                                        optindex++;
                                    }

                                     if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                                    {
                                        if (optindex != 6)
                                            lblF3.Text = GetAnswerOptionOrder(optindex);
                                        rbQues3Answer6.Visible = true; lblF3.Visible = true;
                                        rbQues3Answer6.Text = ds.Tables[0].Rows[i]["Option6"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                            rbQues3Answer6.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                                    {
                                        if (optindex != 7)
                                            lblG3.Text = GetAnswerOptionOrder(optindex);
                                        rbQues3Answer7.Visible = true; lblG3.Visible = true;
                                        rbQues3Answer7.Text = ds.Tables[0].Rows[i]["Option7"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                            rbQues3Answer7.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                                    {
                                        if (optindex != 8)
                                            lblH3.Text = GetAnswerOptionOrder(optindex);
                                        rbQues3Answer8.Visible = true; lblH3.Visible = true;
                                        rbQues3Answer8.Text = ds.Tables[0].Rows[i]["Option8"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                            rbQues3Answer8.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                                    {
                                        if (optindex != 9)
                                            lblI3.Text = GetAnswerOptionOrder(optindex);
                                        rbQues3Answer9.Visible = true; lblI3.Visible = true;
                                        rbQues3Answer9.Text = ds.Tables[0].Rows[i]["Option9"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                            rbQues3Answer9.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                                    {
                                        if (optindex != 10)
                                            lblJ3.Text = GetAnswerOptionOrder(optindex);
                                        rbQues3Answer10.Visible = true; lblJ3.Visible = true;
                                        rbQues3Answer10.Text = ds.Tables[0].Rows[i]["Option10"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                            rbQues3Answer10.Checked = true;

                                    }

                                    curntquescnt++;
                                    j++;
                                    pnlQuestion3.Visible = true;
                                    break;

                                case 3:
                                    optindex = 1;
                                    lblNo4.Text = (slno + 3).ToString() + ".  ";
                                    //lblQues4.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                                    tcellQues4.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                                    lblQuesID4.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                                    var Ques4 = from Ques in dataclass.EvaluationResults
                                                where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                                Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                                select Ques;
                                    if (Ques4.Count() > 0)
                                        if (Ques4.First().Answer != null)
                                            Answer = Ques4.First().Answer.ToString();

                                    if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                    {
                                        rbQues4Answer1.Visible = true; lblA4.Visible = true;
                                        rbQues4Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                            rbQues4Answer1.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                    //radioQues4.Items.Add(ds.Tables[0].Rows[i]["Option1"].ToString());
                                    {
                                        if (optindex != 2)
                                            lblB4.Text = GetAnswerOptionOrder(optindex);
                                        rbQues4Answer2.Visible = true; lblB4.Visible = true;
                                        rbQues4Answer2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                            rbQues4Answer2.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                    {
                                        if (optindex != 3)
                                            lblC4.Text = GetAnswerOptionOrder(optindex);
                                        rbQues4Answer3.Visible = true; lblC4.Visible = true;
                                        rbQues4Answer3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                            rbQues4Answer3.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                    {
                                        if (optindex != 4)
                                            lblD4.Text = GetAnswerOptionOrder(optindex);
                                        rbQues4Answer4.Visible = true; lblD4.Visible = true;
                                        rbQues4Answer4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                            rbQues4Answer4.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                    {
                                        if (optindex != 5)
                                            lblE4.Text = GetAnswerOptionOrder(optindex);
                                        rbQues4Answer5.Visible = true; lblE4.Visible = true;
                                        rbQues4Answer5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                            rbQues4Answer5.Checked = true;
                                        optindex++;
                                    }

                                     if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                                    {
                                        if (optindex != 6)
                                            lblF4.Text = GetAnswerOptionOrder(optindex);
                                        rbQues4Answer6.Visible = true; lblF4.Visible = true;
                                        rbQues4Answer6.Text = ds.Tables[0].Rows[i]["Option6"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                            rbQues4Answer6.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                                    {
                                        if (optindex != 7)
                                            lblG4.Text = GetAnswerOptionOrder(optindex);
                                        rbQues4Answer7.Visible = true; lblG4.Visible = true;
                                        rbQues4Answer7.Text = ds.Tables[0].Rows[i]["Option7"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                            rbQues4Answer7.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                                    {
                                        if (optindex != 8)
                                            lblH4.Text = GetAnswerOptionOrder(optindex);
                                        rbQues4Answer8.Visible = true; lblH4.Visible = true;
                                        rbQues4Answer8.Text = ds.Tables[0].Rows[i]["Option8"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                            rbQues4Answer8.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                                    {
                                        if (optindex != 9)
                                            lblI4.Text = GetAnswerOptionOrder(optindex);
                                        rbQues4Answer9.Visible = true; lblI4.Visible = true;
                                        rbQues4Answer9.Text = ds.Tables[0].Rows[i]["Option9"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                            rbQues4Answer9.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                                    {
                                        if (optindex != 10)
                                            lblJ4.Text = GetAnswerOptionOrder(optindex);
                                        rbQues4Answer10.Visible = true; lblJ4.Visible = true;
                                        rbQues4Answer10.Text = ds.Tables[0].Rows[i]["Option10"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                            rbQues4Answer10.Checked = true;

                                    }

                                    curntquescnt++;
                                    j++;
                                    pnlQuestion4.Visible = true;
                                    break;

                                case 4:
                                    optindex = 1;
                                    lblNo5.Text = (slno + 4).ToString() + ".  ";
                                    //lblQues5.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                                    tcellQues5.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                                    lblQuesID5.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                                    var Ques5 = from Ques in dataclass.EvaluationResults
                                                where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                                Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                                select Ques;
                                    if (Ques5.Count() > 0)
                                        if (Ques5.First().Answer != null)
                                            Answer = Ques5.First().Answer.ToString();
                                    if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                    {
                                        rbQues5Answer1.Visible = true; lblA5.Visible = true;
                                        rbQues5Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                            rbQues5Answer1.Checked = true;
                                        optindex++;
                                    }
                                    //radioQues5.Items.Add(ds.Tables[0].Rows[i]["Option1"].ToString());
                                    if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                    {
                                        if (optindex != 2)
                                            lblB5.Text = GetAnswerOptionOrder(optindex);
                                        rbQues5Answer2.Visible = true; lblB5.Visible = true;
                                        rbQues5Answer2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                            rbQues5Answer2.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                    {
                                        if (optindex != 3)
                                            lblC5.Text = GetAnswerOptionOrder(optindex);
                                        rbQues5Answer3.Visible = true; lblC5.Visible = true;
                                        rbQues5Answer3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                            rbQues5Answer3.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                    {
                                        if (optindex != 4)
                                            lblD5.Text = GetAnswerOptionOrder(optindex);
                                        rbQues5Answer4.Visible = true; lblD5.Visible = true;
                                        rbQues5Answer4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                            rbQues5Answer4.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                    {
                                        if (optindex != 5)
                                            lblE5.Text = GetAnswerOptionOrder(optindex);
                                        rbQues5Answer5.Visible = true; lblE5.Visible = true;
                                        rbQues5Answer5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                            rbQues5Answer5.Checked = true;
                                    }

                                     if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                                    {
                                        if (optindex != 6)
                                            lblF5.Text = GetAnswerOptionOrder(optindex);
                                        rbQues5Answer6.Visible = true; lblF5.Visible = true;
                                        rbQues5Answer6.Text = ds.Tables[0].Rows[i]["Option6"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option6"].ToString() == Answer)
                                            rbQues5Answer6.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                                    {
                                        if (optindex != 7)
                                            lblG5.Text = GetAnswerOptionOrder(optindex);
                                        rbQues5Answer7.Visible = true; lblG5.Visible = true;
                                        rbQues5Answer7.Text = ds.Tables[0].Rows[i]["Option7"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option7"].ToString() == Answer)
                                            rbQues5Answer7.Checked = true;
                                        optindex++;
                                    }
                                    if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                                    {
                                        if (optindex != 8)
                                            lblH5.Text = GetAnswerOptionOrder(optindex);
                                        rbQues5Answer8.Visible = true; lblH5.Visible = true;
                                        rbQues5Answer8.Text = ds.Tables[0].Rows[i]["Option8"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option8"].ToString() == Answer)
                                            rbQues5Answer8.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                                    {
                                        if (optindex != 9)
                                            lblI5.Text = GetAnswerOptionOrder(optindex);
                                        rbQues5Answer9.Visible = true; lblI5.Visible = true;
                                        rbQues5Answer9.Text = ds.Tables[0].Rows[i]["Option9"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option9"].ToString() == Answer)
                                            rbQues5Answer9.Checked = true;
                                        optindex++;
                                    }

                                    if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                                    {
                                        if (optindex != 10)
                                            lblJ5.Text = GetAnswerOptionOrder(optindex);
                                        rbQues5Answer10.Visible = true; lblJ5.Visible = true;
                                        rbQues5Answer10.Text = ds.Tables[0].Rows[i]["Option10"].ToString();
                                        if (ds.Tables[0].Rows[i]["Option10"].ToString() == Answer)
                                            rbQues5Answer10.Checked = true;

                                    }

                                    curntquescnt++;
                                    j++;
                                    pnlQuestion5.Visible = true;
                                    break;
                                ///////////////                        

                            }
                        }
                    }else { GoToNextPage(); }
                }else { GoToNextPage(); }
            }
            else { GoToNextPage(); }
            Session["curntques"] = curntquescnt;
        //}
        //catch (Exception ex)
        //{
        //    // FillQuestion();//bipson 14082010 to avoid arithmetic error... lblmessage.Text += " error :" + ex.Message;
           
        //}
    }

    private void GoToNextPage()
    {
        SetTestSectionTimeDetails();// 22-02-2010 bip
        SetTestSectionVariableTimeDetails();//24-02-2010 bip

        ClearAllPageCountValues(0);//14-05-2013
        Session["evaldirection"] = "Next";
        //string subctrl = getNextControl();
        Session["SubCtrl"] = "FillBalnksQues.ascx";//subctrl; //
        Response.Redirect("FJAHome.aspx"); return;
    }
    /*
    private string getNextControl()
    {
        string nextquestiontype = "";
        string questiontypes = "";
        if (Session["questiontypelist"] != null)
        {
            questiontypes = Session["questiontypelist"].ToString();
            string[] strQuesTypes = questiontypes.Split(new char[] { ',' });
            
            nextquestiontype = "FillBlanks";    //2  
            foreach (string questype in strQuesTypes)
            {
                if (questype == nextquestiontype)
                    return "FillBalnksQues.ascx";
            }
            nextquestiontype = "ImageType";     //3
            foreach (string questype in strQuesTypes)
            {
                if (questype == nextquestiontype)
                    return "ImageQuestions.ascx";
            }
            nextquestiontype = "PhotoType";     //4
            foreach (string questype in strQuesTypes)
            {
                if (questype == nextquestiontype)
                    return "ImageQuestions.ascx";
            }
            nextquestiontype = "MemTestWords";  //5
            nextquestiontype = "MemTestImages"; //6
            nextquestiontype = "AudioType";     //7
            nextquestiontype = "VideoType";     //8         
            nextquestiontype = "RatingType";    //9
            nextquestiontype = "Objective";     //1
        }
        else
        {
            var getQuestionTypes = from questiontypes in dataclass.OrganizationQuestionTypes where questiontypes.OrganizationId == OrganizationID select questiontypes;
            if (getQuestionTypes.Count() > 0)
            {
                int i = 0;
                foreach (var orgQuestionTypes in getQuestionTypes)
                {
                    if (i > 0)
                        questiontypes += ",";
                    questiontypes += orgQuestionTypes.QuestionTypeName.ToString();
                    i++;
                }
                Session["questiontypelist"] = questiontypes;
            }
        }

    }
    */
    private void GoToLastPage()
    {
       // ClearSessionValues();
        int variableIdPageNo = 0;
        if (Session["VariableIdIndexNo"] != null)
        {
            variableIdPageNo = int.Parse(Session["VariableIdIndexNo"].ToString());
            variableIdPageNo = variableIdPageNo - 1;
            Session["VariableIdIndexNo"] = variableIdPageNo.ToString();
            Session["VariableIdIndexNo_timer"] = variableIdPageNo.ToString();// bip 12052010

            if (Session["CurrentTestSectionId"] != null)
                GetTestVariableId(testsectionid);
        }

        ClearDataSetValues();
        Session["evaldirection"] = "Previous";
        Session["SubCtrl"] = "RatingQuestions.ascx";
        Response.Redirect("FJAHome.aspx");
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
                Timer1.Enabled = false;// 26-10-2010 bip

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
        if (Session["totalQuesCount"] != null)
            totalcount = int.Parse(Session["totalQuesCount"].ToString());
        if (Session["totalQuesAvailable"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecount"] != null)
            pagecount = int.Parse(Session["pagecount"].ToString());
        int curindex = 0;
        curindex = (pagecount + 1) * quesperPage;
        if (curindex < curcount)
        {
            pagecount++;
            Session["pagecount"] = pagecount;SetCurrentPageCount();// 230110 bip
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
        int testSecondVariableId = 0, testFirstVariableId = 0;
        if (Session["CurrentTestSecondVariableId"] != null)
            testSecondVariableId = int.Parse(Session["CurrentTestSecondVariableId"].ToString());
        if (Session["CurrentTestFirstVariableId"] != null)
            testFirstVariableId = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
        int questionid = 0; string questiontype = "Objective";
        dataclass.Procedure_UserTestPageIndex_Temp(userid, testId, testsectionid, testFirstVariableId, testSecondVariableId, pagecount, questiontype);
    }
    private Boolean CheckAnswer()
    {
        Boolean answercompleted = true;

        string answer = "";
        string message = "You have missed question No:";

        qusid1 = 0;
        if (lblQuesID1.Text.Trim() != "")
        {
            if (rbQues1Answer1.Checked == true)
                answer = rbQues1Answer1.Text;
            else if (rbQues1Answer2.Checked == true)
                answer = rbQues1Answer2.Text;
            else if (rbQues1Answer3.Checked == true)
                answer = rbQues1Answer3.Text;
            else if (rbQues1Answer4.Checked == true)
                answer = rbQues1Answer4.Text;
            else if (rbQues1Answer5.Checked == true)
                answer = rbQues1Answer5.Text;
            else if (rbQues1Answer6.Checked == true)
                answer = rbQues1Answer6.Text;
            else if (rbQues1Answer7.Checked == true)
                answer = rbQues1Answer7.Text;
            else if (rbQues1Answer8.Checked == true)
                answer = rbQues1Answer8.Text;
            else if (rbQues1Answer9.Checked == true)
                answer = rbQues1Answer9.Text;
            else if (rbQues1Answer10.Checked == true)
                answer = rbQues1Answer10.Text;

            if (answer == "")
            {
                message += lblNo1.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

        answer = "";
        if (lblQuesID2.Text.Trim() != "")
        {
            if (rbQues2Answer1.Checked == true)
                answer = rbQues2Answer1.Text;
            else if (rbQues2Answer2.Checked == true)
                answer = rbQues2Answer2.Text;
            else if (rbQues2Answer3.Checked == true)
                answer = rbQues2Answer3.Text;
            else if (rbQues2Answer4.Checked == true)
                answer = rbQues2Answer4.Text;
            else if (rbQues2Answer5.Checked == true)
                answer = rbQues2Answer5.Text;
            else if (rbQues2Answer6.Checked == true)
                answer = rbQues2Answer6.Text;
            else if (rbQues2Answer7.Checked == true)
                answer = rbQues2Answer7.Text;
            else if (rbQues2Answer8.Checked == true)
                answer = rbQues2Answer8.Text;
            else if (rbQues2Answer9.Checked == true)
                answer = rbQues2Answer9.Text;
            else if (rbQues2Answer10.Checked == true)
                answer = rbQues2Answer10.Text;
            if (answer == "")
            {
                message += lblNo2.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

        answer = "";
        if (lblQuesID3.Text.Trim() != "")
        {
            if (rbQues3Answer1.Checked == true)
                answer = rbQues3Answer1.Text;
            else if (rbQues3Answer2.Checked == true)
                answer = rbQues3Answer2.Text;
            else if (rbQues3Answer3.Checked == true)
                answer = rbQues3Answer3.Text;
            else if (rbQues3Answer4.Checked == true)
                answer = rbQues3Answer4.Text;
            else if (rbQues3Answer5.Checked == true)
                answer = rbQues3Answer5.Text;
            else if (rbQues3Answer6.Checked == true)
                answer = rbQues3Answer6.Text;
            else if (rbQues3Answer7.Checked == true)
                answer = rbQues3Answer7.Text;
            else if (rbQues3Answer8.Checked == true)
                answer = rbQues3Answer8.Text;
            else if (rbQues3Answer9.Checked == true)
                answer = rbQues3Answer9.Text;
            else if (rbQues3Answer10.Checked == true)
                answer = rbQues3Answer10.Text;
            if (answer == "")
            {
                message += lblNo3.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

        answer = "";
        if (lblQuesID4.Text.Trim() != "")
        {

            if (rbQues4Answer1.Checked == true)
                answer = rbQues4Answer1.Text;
            else if (rbQues4Answer2.Checked == true)
                answer = rbQues4Answer2.Text;
            else if (rbQues4Answer3.Checked == true)
                answer = rbQues4Answer3.Text;
            else if (rbQues4Answer4.Checked == true)
                answer = rbQues4Answer4.Text;
            else if (rbQues4Answer5.Checked == true)
                answer = rbQues4Answer5.Text;
            else if (rbQues4Answer6.Checked == true)
                answer = rbQues4Answer6.Text;
            else if (rbQues4Answer7.Checked == true)
                answer = rbQues4Answer7.Text;
            else if (rbQues4Answer8.Checked == true)
                answer = rbQues4Answer8.Text;
            else if (rbQues4Answer9.Checked == true)
                answer = rbQues4Answer9.Text;
            else if (rbQues4Answer10.Checked == true)
                answer = rbQues4Answer10.Text;
            if (answer == "")
            {
                message += lblNo4.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

        answer = "";
        if (lblQuesID5.Text.Trim() != "")
        {
            if (rbQues5Answer1.Checked == true)
                answer = rbQues5Answer1.Text;
            else if (rbQues5Answer2.Checked == true)
                answer = rbQues5Answer2.Text;
            else if (rbQues5Answer3.Checked == true)
                answer = rbQues5Answer3.Text;
            else if (rbQues5Answer4.Checked == true)
                answer = rbQues5Answer4.Text;
            else if (rbQues5Answer5.Checked == true)
                answer = rbQues5Answer5.Text;
            else if (rbQues5Answer6.Checked == true)
                answer = rbQues1Answer6.Text;
            else if (rbQues5Answer7.Checked == true)
                answer = rbQues5Answer7.Text;
            else if (rbQues5Answer8.Checked == true)
                answer = rbQues5Answer8.Text;
            else if (rbQues5Answer9.Checked == true)
                answer = rbQues5Answer9.Text;
            else if (rbQues5Answer10.Checked == true)
                answer = rbQues5Answer10.Text;
            if (answer == "")
            {
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
    private void SaveAnswer()
    {
        string quescategory = "Objective";
        int testsecid=0;
        if(Session["CurrentTestSectionId"] !=null)
            testsecid=int.Parse(Session["CurrentTestSectionId"].ToString());

        string answer = "";

        qusid1 = 0;
        if (lblQuesID1.Text.Trim() != "")
        {
            if (rbQues1Answer1.Checked == true)
                //answer = rbQues1Answer1.Text;
                answer = "1";
            else if (rbQues1Answer2.Checked == true)
                //answer = rbQues1Answer2.Text;
                answer = "2";
            else if (rbQues1Answer3.Checked == true)
                //answer = rbQues1Answer3.Text;
                answer = "3";
            else if (rbQues1Answer4.Checked == true)
                //answer = rbQues1Answer4.Text;
                answer = "4";
            else if (rbQues1Answer5.Checked == true)
                //answer = rbQues1Answer5.Text;
                answer = "5";

            else if (rbQues1Answer6.Checked == true)
                answer = "6";
            else if (rbQues1Answer7.Checked == true)
                answer = "7";
            else if (rbQues1Answer8.Checked == true)
                answer = "8";
            else if (rbQues1Answer9.Checked == true)
                answer = "9";
            else if (rbQues1Answer10.Checked == true)
                answer = "10";

            qusid1 = int.Parse(lblQuesID1.Text.Trim());

            dataclass.Procedure_QuesAnswers(qusid1, usercode, tcellQues1.InnerHtml, answer, userid,testId,testsecid,quescategory);

        }

        answer = "";

        qusid2 = 0;
        if (lblQuesID2.Text.Trim() != "")
        {
            if (rbQues2Answer1.Checked == true)
                //answer = rbQues2Answer1.Text;
                answer = "1";
            else if (rbQues2Answer2.Checked == true)
                //answer = rbQues2Answer2.Text;
                answer = "2";
            else if (rbQues2Answer3.Checked == true)
                //answer = rbQues2Answer3.Text;
                answer = "3";
            else if (rbQues2Answer4.Checked == true)
                //answer = rbQues2Answer4.Text;
                answer = "4";
            else if (rbQues2Answer5.Checked == true)
                //answer = rbQues2Answer5.Text;
                answer = "5";

            else if (rbQues2Answer6.Checked == true)
                answer = "6";
            else if (rbQues2Answer7.Checked == true)
                answer = "7";
            else if (rbQues2Answer8.Checked == true)
                answer = "8";
            else if (rbQues2Answer9.Checked == true)
                answer = "9";
            else if (rbQues2Answer10.Checked == true)
                answer = "10";

            qusid2 = int.Parse(lblQuesID2.Text.Trim());

            dataclass.Procedure_QuesAnswers(qusid2, usercode, tcellQues2.InnerHtml, answer, userid, testId, testsecid,quescategory);
        }
        answer = "";


        qusid3 = 0;
        if (lblQuesID3.Text.Trim() != "")
        {
            if (rbQues3Answer1.Checked == true)
                //answer = rbQues3Answer1.Text;
                answer = "1";
            else if (rbQues3Answer2.Checked == true)
                //answer = rbQues3Answer2.Text;
                answer = "2";
            else if (rbQues3Answer3.Checked == true)
                //answer = rbQues3Answer3.Text;
                answer = "3";
            else if (rbQues3Answer4.Checked == true)
                //answer = rbQues3Answer4.Text;
                answer = "4";
            else if (rbQues3Answer5.Checked == true)
                //answer = rbQues3Answer5.Text;
                answer = "5";

            else if (rbQues3Answer6.Checked == true)
                answer = "6";
            else if (rbQues3Answer7.Checked == true)
                answer = "7";
            else if (rbQues3Answer8.Checked == true)
                answer = "8";
            else if (rbQues3Answer9.Checked == true)
                answer = "9";
            else if (rbQues3Answer10.Checked == true)
                answer = "10";

            qusid3 = int.Parse(lblQuesID3.Text.Trim());

            dataclass.Procedure_QuesAnswers(qusid3, usercode, tcellQues3.InnerHtml, answer, userid, testId, testsecid,quescategory);
        }
        answer = "";

        qusid4 = 0;
        if (lblQuesID4.Text.Trim() != "")
        {

            if (rbQues4Answer1.Checked == true)
                //answer = rbQues4Answer1.Text;
                answer = "1";
            else if (rbQues4Answer2.Checked == true)
                //answer = rbQues4Answer2.Text;
                answer = "2";
            else if (rbQues4Answer3.Checked == true)
                //answer = rbQues4Answer3.Text;
                answer = "3";
            else if (rbQues4Answer4.Checked == true)
                //answer = rbQues4Answer4.Text;
                answer = "4";
            else if (rbQues4Answer5.Checked == true)
                //answer = rbQues4Answer5.Text;
                answer = "5";

            else if (rbQues4Answer6.Checked == true)
                answer = "6";
            else if (rbQues4Answer7.Checked == true)
                answer = "7";
            else if (rbQues4Answer8.Checked == true)
                answer = "8";
            else if (rbQues4Answer9.Checked == true)
                answer = "9";
            else if (rbQues4Answer10.Checked == true)
                answer = "10";

            qusid4 = int.Parse(lblQuesID4.Text.Trim());

            dataclass.Procedure_QuesAnswers(qusid4, usercode, tcellQues4.InnerHtml, answer, userid, testId, testsecid,quescategory);
        }
        answer = "";

        qusid5 = 0;
        if (lblQuesID5.Text.Trim() != "")
        {
            if (rbQues5Answer1.Checked == true)
                //answer = rbQues5Answer1.Text;
                answer = "1";
            else if (rbQues5Answer2.Checked == true)
                //answer = rbQues5Answer2.Text;
                answer = "2";
            else if (rbQues5Answer3.Checked == true)
                //answer = rbQues5Answer3.Text;
                answer = "3";
            else if (rbQues5Answer4.Checked == true)
                //answer = rbQues5Answer4.Text;
                answer = "4";
            else if (rbQues5Answer5.Checked == true)
                //answer = rbQues5Answer5.Text;
                answer = "5";

            else if (rbQues5Answer6.Checked == true)
                answer = "6";
            else if (rbQues5Answer7.Checked == true)
                answer = "7";
            else if (rbQues5Answer8.Checked == true)
                answer = "8";
            else if (rbQues5Answer9.Checked == true)
                answer = "9";
            else if (rbQues5Answer10.Checked == true)
                answer = "10";

            qusid5 = int.Parse(lblQuesID5.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid5, usercode, tcellQues5.InnerHtml, answer, userid, testId, testsecid,quescategory);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session["SubCtrl"] = null;
       // Session["MasterCtrl"] = "~/MasterPage.master";
        Response.Redirect("FJAHome.aspx");

    }
    private void FillPreviousDetails()
    {
        int curcount = 0;
        int totalcount = 0, totalquesavail = 0;
        if (Session["totalQuesCount"] != null)
            totalcount = int.Parse(Session["totalQuesCount"].ToString());
        if (Session["totalQuesAvailable"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecount"] != null)
            pagecount = int.Parse(Session["pagecount"].ToString());
        int curindex = 0;
        curindex = (pagecount - 1) * quesperPage;
        if (curindex >= 0)
        {
            pagecount--;
            Session["pagecount"] = pagecount; SetCurrentPageCount();// 230110 bip
            FillQuestion();// 04-03-2010 bip
        }
        else
        {
            CheckPreviousVariableAccess();
            //GoToPreviousPage();           
        }
       // FillQuestion();// 04-03-2010 bip

    }
    protected void ptnPrevious_Click(object sender, EventArgs e)
    {
        Session["evaldirection"] = "Previous";
        Timer1.Enabled = false;// 05-03-2010 bip
        FillPreviousDetails();       
    }
    private void CheckPreviousVariableAccess()
    {
        int testFirstVariableId = 0; int testSecondVariableId = 0; int testVariableId = 0;
        Session["TestFirstVariableName"] = null; Session["TestSecondVariableName"] = null;
        DataSet dsTestVariableIds = new DataSet();
        if (Session["dsTestVariableIds"] != null)
        {
            dsTestVariableIds = (DataSet)Session["dsTestVariableIds"];
        }
        if (dsTestVariableIds == null) {GoToPreviousPage(); return;}
        int TotalVariableCount = 1;
        if (dsTestVariableIds.Tables[0].Rows.Count > 0)
        {
            Session["TotalVariableCount"] = dsTestVariableIds.Tables[0].Rows.Count.ToString();
            TotalVariableCount = dsTestVariableIds.Tables[0].Rows.Count;

            int variableIdPageNo = 0;
            if (Session["VariableIdIndexNo"] != null)
            {
                variableIdPageNo = int.Parse(Session["VariableIdIndexNo"].ToString());
                variableIdPageNo = variableIdPageNo - 1;
                if ((variableIdPageNo - 1) < 0) {  GoToPreviousPage(); return; }

                if (variableIdPageNo <= TotalVariableCount)
                {
                    testFirstVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["FirstVariableId"].ToString());
                    testSecondVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["SecondVariableId"].ToString());
                                        
                    // new code started here // 04-03-2010 bip
                    // code to check wheather the testfirstvariable id changes

                    if(Session["CurrentTestFirstVariableId"]!=null)
                        if (Session["CurrentTestFirstVariableId"].ToString() != testFirstVariableId.ToString())
                        {

                            DataTable dtTimeDetails = new DataTable();
                            dtTimeDetails = (DataTable)Session["FirstvarTimeDet"];
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


                                //if yes check wheather the assigned time completed for this variable
                                if (firstvarid == testFirstVariableId)
                                {
                                    if (assignStatus == "0")
                                    {
                                        Boolean blcompStatus = CheckPreviousGenVarTimeValidity();
                                        if (blcompStatus == true)
                                        {  //  display message box; 
                                            testFirstVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo]["FirstVariableId"].ToString());
                                            testSecondVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo]["SecondVariableId"].ToString());

                                            Timer1.Enabled = false;
                                            lblpopup_previous.Text = "The Time allotted for this variable was completed, so you cann't access previous part.";
                                            pnlpopup_previous.Visible = true;
                                            return;
                                        }
                                        else
                                        {
                                            // update sectionidindex here
                                            
                                            testFirstVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo-1]["FirstVariableId"].ToString());
                                            testSecondVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo-1]["SecondVariableId"].ToString());
                                            
                                        }
                                    }
                                    else
                                        if (competionstatus == "1")
                                        {
                                            // display message box; 
                                            testFirstVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo]["FirstVariableId"].ToString());
                                            testSecondVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo]["SecondVariableId"].ToString());

                                            Timer1.Enabled = false;
                                            lblpopup_previous.Text = "The Time allotted for this variable was completed, so you cann't access previous part.";
                                            pnlpopup_previous.Visible = true;
                                            return;
                                        }
                                        else
                                        {
                                            // update sectionidindex here
                                            testFirstVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["FirstVariableId"].ToString());
                                            testSecondVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["SecondVariableId"].ToString());


                                        }
                                    //}////

                                    //if no then set the time for this variable ad remaining time.
                                    if (timeused != null)
                                    {
                                        if (Session["TestSectionVariableTimeDuration"] != null)
                                        {
                                            string[] strTimeUsed = timeused.Split(new char[] { ':' });
                                            int setHrs = int.Parse(strTimeUsed[0].ToString());
                                            int setMin = int.Parse(strTimeUsed[1].ToString());
                                            int setsec = int.Parse(strTimeUsed[2].ToString());

                                            string[] strTimeAssigned = assigntime.Split(new char[] { ':' });
                                            int getHrs = int.Parse(strTimeAssigned[0].ToString());
                                            int getMin = int.Parse(strTimeAssigned[1].ToString());
                                            int getsec = int.Parse(strTimeAssigned[2].ToString());

                                            TimeSpan tsTimeUsed = new TimeSpan(setHrs, setMin, setsec);
                                            TimeSpan tsTimeAssign = new TimeSpan(getHrs, getMin, getsec);
                                            TimeSpan tsRemains = tsTimeAssign.Subtract(tsTimeUsed);
                                            int remainHrs = tsRemains.Hours;
                                            int remainMin = tsRemains.Minutes;
                                            int remainsec = tsRemains.Seconds;
                                            Session["TestSectionVariableTimeDuration"] = remainHrs + ":" + remainMin + ":" + remainsec;
                                        }
                                    }// 05-03-2010 bip
                                    int firstvaridfortimer = 0, varidindex_timer = 0; ;
                                    if (Session["FirstVariableIdForTimer"] != null)
                                    {
                                        firstvaridfortimer = int.Parse(Session["FirstVariableIdForTimer"].ToString());
                                        firstvaridfortimer -= 1;
                                        Session["FirstVariableIdForTimer"] = firstvaridfortimer;

                                        /// bip 12052010
                                        varidindex_timer=int.Parse(Session["VariableIdIndexNo_timer"].ToString());
                                        Session["VariableIdIndexNo_timer"] = varidindex_timer - 1;
                                        ///
                                    }

                                }////
                            }
                        }
                    // new code ended here

                    Session["TestFirstVariableName"] = dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["FirstVariableName"].ToString();
                    Session["TestSecondVariableName"] = dsTestVariableIds.Tables[0].Rows[variableIdPageNo - 1]["SecondVariableName"].ToString();
                }
                //Session["sectionIdIndexNo"] = (sectionIdIndexNo + 1).ToString();           
            }
            else
            {
                Session["VariableIdIndexNo"] = "1";
                Session["VariableIdIndexNo_timer"] = "1";// bip 12052010

                testFirstVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[0]["FirstVariableId"].ToString());
                testSecondVariableId = int.Parse(dsTestVariableIds.Tables[0].Rows[0]["SecondVariableId"].ToString());

                Session["TestFirstVariableName"] = dsTestVariableIds.Tables[0].Rows[0]["FirstVariableName"].ToString();
                Session["TestSecondVariableName"] = dsTestVariableIds.Tables[0].Rows[0]["SecondVariableName"].ToString();
            }

            Session["CurrentTestSecondVariableId"] = testSecondVariableId;
            Session["CurrentTestFirstVariableId"] = testFirstVariableId;

            if (variableIdPageNo > TotalVariableCount)
            {
                GoToPreviousPage(); return;
            }
            if (testSecondVariableId > 0)
                testVariableId = testSecondVariableId;
            else if (testFirstVariableId > 0)
                testVariableId = testFirstVariableId;          
                        
            if (testVariableId <= 0)
                if (Session["evaldirection"] != null)
                    if (Session["evaldirection"].ToString() == "Previous")
                    { GoToPreviousPage(); return; }

            GoToLastPage();
        }
    }

    private Boolean CheckPreviousGenVarTimeValidity()
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

            }
        return false;
    }

    private void GoToPreviousPage()
    {
        Session["VariableIdIndexNo"] = null;
        Session["VariableIdIndexNo_timer"] = null;// bip 12052010

       // ClearSessionValues();
        Session["evaldirection"] = null;
        Session["SubCtrl"] = "TestIntroductionControl.ascx";
        Response.Redirect("FJAHome.aspx");
    }

    private void ClearSessionValues()
    {
      //  Session["pagecount1"] = null; Session["pagecount"] = null;
       
    }
    private void ClearAllPageCountValues(int index)
    {
        if (index == 0)
            Session["pagecount"] = null;//objques

        Session["pagecount_fill"] = null;
        Session["pagecount_img"] = null;
        Session["pagecount_memWords"] = null;
        Session["pagecount_audio"] = null;
        Session["pagecount_memImages"] = null;
        Session["pagecount_imgPhoto"] = null;
        Session["pagecountRating"] = null;
        Session["pagecount_video"] = null;
    }
    private void ClearDataSetValues()
    {
        Session["questionColl"] = null;
        Session["questionColl_fill"] = null;
        Session["questionColl_img"] = null;
        Session["questionColl_imgPhoto"] = null;
        Session["questionColl_memWords"] = null;
        Session["questionColl_memImages"] = null;
        Session["questionColl_Audio"] = null;
        Session["questionColl_video"] = null;
        Session["questionColl_Rating"] = null;
    }
    
    private void ClearControls()
    {
        rbQues1Answer1.Checked = false; rbQues1Answer1.Visible = false;
        rbQues1Answer2.Checked = false; rbQues1Answer2.Visible = false;
        rbQues1Answer3.Checked = false; rbQues1Answer3.Visible = false;
        rbQues1Answer4.Checked = false; rbQues1Answer4.Visible = false;
        rbQues1Answer5.Checked = false; rbQues1Answer5.Visible = false;
        rbQues1Answer6.Checked = false; rbQues1Answer6.Visible = false;
        rbQues1Answer7.Checked = false; rbQues1Answer7.Visible = false;
        rbQues1Answer8.Checked = false; rbQues1Answer8.Visible = false;
        rbQues1Answer9.Checked = false; rbQues1Answer9.Visible = false;
        rbQues1Answer10.Checked = false; rbQues1Answer10.Visible = false;
        
        rbQues2Answer1.Checked = false; rbQues2Answer1.Visible = false;
        rbQues2Answer2.Checked = false; rbQues2Answer2.Visible = false;
        rbQues2Answer3.Checked = false; rbQues2Answer3.Visible = false;
        rbQues2Answer4.Checked = false; rbQues2Answer4.Visible = false;
        rbQues2Answer5.Checked = false; rbQues2Answer5.Visible = false;
        rbQues2Answer6.Checked = false; rbQues2Answer6.Visible = false;
        rbQues2Answer7.Checked = false; rbQues2Answer7.Visible = false;
        rbQues2Answer8.Checked = false; rbQues2Answer8.Visible = false;
        rbQues2Answer9.Checked = false; rbQues2Answer9.Visible = false;
        rbQues2Answer10.Checked = false; rbQues2Answer10.Visible = false;
       
        rbQues3Answer1.Checked = false; rbQues3Answer1.Visible = false;
        rbQues3Answer2.Checked = false; rbQues3Answer2.Visible = false;
        rbQues3Answer3.Checked = false; rbQues3Answer3.Visible = false;
        rbQues3Answer4.Checked = false; rbQues3Answer4.Visible = false;
        rbQues3Answer5.Checked = false; rbQues3Answer5.Visible = false;
        rbQues3Answer6.Checked = false; rbQues3Answer6.Visible = false;
        rbQues3Answer7.Checked = false; rbQues3Answer7.Visible = false;
        rbQues3Answer8.Checked = false; rbQues3Answer8.Visible = false;
        rbQues3Answer9.Checked = false; rbQues3Answer9.Visible = false;
        rbQues3Answer10.Checked = false; rbQues3Answer10.Visible = false;
        
        rbQues4Answer1.Checked = false; rbQues4Answer1.Visible = false;
        rbQues4Answer2.Checked = false; rbQues4Answer2.Visible = false;
        rbQues4Answer3.Checked = false; rbQues4Answer3.Visible = false;
        rbQues4Answer4.Checked = false; rbQues4Answer4.Visible = false;
        rbQues4Answer5.Checked = false; rbQues4Answer5.Visible = false;
        rbQues4Answer6.Checked = false; rbQues4Answer6.Visible = false;
        rbQues4Answer7.Checked = false; rbQues4Answer7.Visible = false;
        rbQues4Answer8.Checked = false; rbQues4Answer8.Visible = false;
        rbQues4Answer9.Checked = false; rbQues4Answer9.Visible = false;
        rbQues4Answer10.Checked = false; rbQues4Answer10.Visible = false;
       
        rbQues5Answer1.Checked = false; rbQues5Answer1.Visible = false;
        rbQues5Answer2.Checked = false; rbQues5Answer2.Visible = false;
        rbQues5Answer3.Checked = false; rbQues5Answer3.Visible = false;
        rbQues5Answer4.Checked = false; rbQues5Answer4.Visible = false;
        rbQues5Answer5.Checked = false; rbQues5Answer5.Visible = false;
        rbQues5Answer6.Checked = false; rbQues5Answer6.Visible = false;
        rbQues5Answer7.Checked = false; rbQues5Answer7.Visible = false;
        rbQues5Answer8.Checked = false; rbQues5Answer8.Visible = false;
        rbQues5Answer9.Checked = false; rbQues5Answer9.Visible = false;
        rbQues5Answer10.Checked = false; rbQues5Answer10.Visible = false;

        lblA1.Visible = false; lblB1.Visible = false; lblC1.Visible = false; lblD1.Visible = false; lblE1.Visible = false; 
        lblA2.Visible = false; lblB2.Visible = false; lblC2.Visible = false; lblD2.Visible = false; lblE2.Visible = false; 
        lblA3.Visible = false; lblB3.Visible = false; lblC3.Visible = false; lblD3.Visible = false; lblE3.Visible = false; 
        lblA4.Visible = false; lblB4.Visible = false; lblC4.Visible = false; lblD4.Visible = false; lblE4.Visible = false; 
        lblA5.Visible = false; lblB5.Visible = false; lblC5.Visible = false; lblD5.Visible = false; lblE5.Visible = false;

        lblF1.Visible = false; lblG1.Visible = false; lblH1.Visible = false; lblI1.Visible = false; lblJ1.Visible = false;
        lblF2.Visible = false; lblG2.Visible = false; lblH2.Visible = false; lblI2.Visible = false; lblJ2.Visible = false;
        lblF3.Visible = false; lblG3.Visible = false; lblH3.Visible = false; lblI3.Visible = false; lblJ3.Visible = false;
        lblF4.Visible = false; lblG4.Visible = false; lblH4.Visible = false; lblI4.Visible = false; lblJ4.Visible = false;
        lblF5.Visible = false; lblG5.Visible = false; lblH5.Visible = false; lblI5.Visible = false; lblJ5.Visible = false;

        lblQuesID1.Text = ""; lblQuesID2.Text = ""; lblQuesID3.Text = ""; lblQuesID4.Text = ""; lblQuesID5.Text = "";
        lblNo1.Text = ""; lblNo2.Text = ""; lblNo3.Text = ""; lblNo4.Text = ""; lblNo5.Text = "";
        lblQues1.Text = ""; lblQues2.Text = ""; lblQues3.Text = ""; lblQues4.Text = ""; lblQues5.Text = ""; 
        tcellQues1.InnerHtml = ""; tcellQues2.InnerHtml = ""; tcellQues3.InnerHtml = ""; tcellQues4.InnerHtml = ""; tcellQues5.InnerHtml = "";
       

    }

    protected void btnPrevSection_Click(object sender, EventArgs e)
    {
        Session["SubCtrl"] = "QuestionairIntroductionControl.ascx";
        Response.Redirect("FJAHome.aspx");
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {        
        pnlMessage.Visible = false; pnlMessage_confirm.Visible = true;
        btnSubmit.Visible = false; btnPrevious.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //// 07-03-2010 bip
        //int variableIdPageNo = 0;
        //if (Session["VariableIdIndexNo"] != null)
        //    variableIdPageNo = int.Parse(Session["VariableIdIndexNo"].ToString());
        //variableIdPageNo = variableIdPageNo - 1;
        //Session["VariableIdIndexNo"] = variableIdPageNo.ToString();
        //////

        //Response.Redirect("CareerJudge.htm");

        if (Session["TestSectionCompletionStatus"] != null)// bip 08052010
            if (Session["TestSectionCompletionStatus"].ToString() == "1")// bip 08052010
                lblmessage.Text = "You cant goback to previous section, the time alloted for this section was expired..";

        

        Session["FirstVariableIdForTimer"] = null;// 07-03-2010 bip
        ////commented by bip 07052010
        //Timer1.Enabled = true;
        //pnlMessage.Visible = false;
        //btnSubmit.Visible = true; btnPrevious.Visible = true;
        //
        ReloadCurrentControl();
    }
    protected void btnYes_confirm_Click(object sender, EventArgs e)
    {

        //// 230110 bip        
        DeleteTempValuesFromDB();//dataclass.Procedure_DeleteUserTest_TempValues(userid, 0, 0);
        ////
        pnlMessage.Visible = false; pnlMessage_confirm.Visible = false;
        ClearDbValues();

        // bip 07052010
        ClearAllPageCountValues(0);
        Session["CurrentTestSectionId"] = null;
        Session["dsTestVariableIds"] = null;
        Session["VariableIdIndexNo"] = null;
        Session["FirstVariableIdForTimer"] = null;
        Session["CurrentTestFirstVariableId"] = null;
        Session["CurrentTestSecondVariableId"] = null;
        Session["TestFirstVariableName"] = null;
        Session["TestSecondVariableName"] = null;        
        Session["evaldirection"] = "Next";
        //
        Session["VariableIdIndexNo_timer"] = null;// bip 12052010

        SetNextSectionDetails();
        btnSubmit.Visible = true; btnPrevious.Visible = true;
        Timer1.Enabled = true;
    }
    protected void btnCancel_confirm_Click(object sender, EventArgs e)
    {
        //// 07-03-2010 bip
        
        //////
        Session["FirstVariableIdForTimer"] = null;// 07-03-2010 bip
       
        ReloadCurrentControl();
    }
    private void DeleteTempValuesFromDB()// bip 08052010
    {
        dataclass.Procedure_DeleteUserTest_TempValues(userid, 0, 0);
    }
    private void ReloadCurrentControl()
    {
        //Label1.Text = "null";

        int variableIdPageNo = 0;
        if (Session["VariableIdIndexNo"] != null)
        {
            variableIdPageNo = int.Parse(Session["VariableIdIndexNo"].ToString());
            variableIdPageNo = variableIdPageNo - 1;
            Session["VariableIdIndexNo"] = variableIdPageNo.ToString();
            Session["VariableIdIndexNo_timer"] = variableIdPageNo.ToString();// bip 12052010

            if (Session["CurrentTestSectionId"] != null)
                GetTestVariableId(testsectionid);
        }
        if (Session["CurrentControlCtrl"] != null)
            if (Session["CurrentControlCtrl"].ToString() != "ObjectiveQuestns.ascx")
            {
                ////bip 07052010
                Session["MemWordPrevious"] = "True";
                Session["MemImagePrevious"] = "True";
                Session["AudioPrevious"] = "True";
                Session["VideoPrevious"] = "True";
                ////
                Session["curRatingQuesCompleted"] = null;
                //Label1.Text = Session["CurrentControlCtrl"].ToString();
                Session["SubCtrl"] = Session["CurrentControlCtrl"].ToString();
                Response.Redirect("FJAHome.aspx");
            }
        //bip 07052010
        Timer1.Enabled = true;
        pnlMessage_confirm.Visible = false; pnlMessage.Visible = false;
        btnSubmit.Visible = true; btnPrevious.Visible = true;
        //
        FillQuestion();//07-03-2010 bip
    }
    private void ClearDbValues()
    {
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

    }
    protected void btnYespopup_Click(object sender, EventArgs e)
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
                    //drTimedet["CompletionStatus"] = 0;
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
                    genSec=int.Parse(genTimeUsed[2]);
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
                    //    return true;

                    //return false;// 03-02-2010 bip

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
                Session["TestSectionVariableTimeDuration"] = null; // 07-03-2010 bip
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
                //int sec=tsNow.Seconds;

                string strDuration = Session["TestSectionTimeDuration"].ToString();
                string[] timeValues = strDuration.Split(new char[] { ':' });
                int setHrs = int.Parse(timeValues[0].ToString());
                int setMin = int.Parse(timeValues[1].ToString());
                //int setSec=int.Parse(timeValues[2].ToString());
                TimeSpan tsDuration = new TimeSpan(setHrs, setMin, 0);//25-02-2010 bip
                if (tsNow > tsDuration)
                {
                    Session["TestSectionCompletionStatus"] = 1;// bip 08052010
                    return true;
                }
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
                    {
                        Session["TestSectionCompletionStatus"] = 1;// bip 08052010
                        return true;
                    }
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
                //int sec=tsNow.Seconds;
                string[] timeValues = strTestDuration.Split(new char[] { ':' });
                int setHrs = int.Parse(timeValues[0].ToString());
                int setMin = int.Parse(timeValues[1].ToString());
                //int setSec=int.Parse(timeValues[2].ToString());

                TimeSpan tsDuration = new TimeSpan(setHrs, setMin, 0);
                if (tsNow >tsDuration )
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
        //

        ShowNextControl_Timer(1);

    }
    private void ShowNextControl_Timer(int index)// 24-02-2010 bip
    {
        Session["curTestSectionVariableId_timer"] = null;// 04-03-2010 bip
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

        //FillQuestion();

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
        Response.Redirect("FJAHome.aspx"); //bipson 18082010// Response.Redirect("CareerJudge.htm");;
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

    protected void Timer1_Tick(object sender, EventArgs e)// 26-10-2010 bip
    {        

        if (Session["timeExpired"] != null)
            if (Session["timeExpired"].ToString() == "True")
                return;            

        CheckTime();
        //SaveAnswer();

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Timer1.Enabled = true;// 26-10-2010 bip
    }
    private void setSessionTime()
    {
        if (Session["TimeRemains"] != null)
        {
            //string[] arrTimeRemains = (string[])Session["TimeRemains"];
            //string testvarvalues = "varid:" + Session["CurrentTestFirstVariableId"].ToString() +
            //    "asgntime:" + Session["TestSectionVariableTimeDuration"].ToString() +
            //    "timeused:" + Session["TestSectionVariableTimeUsed"].ToString();

        }
    }
    protected void btnOk_Previous_Click(object sender, EventArgs e)
    {       
        pnlpopup_previous.Visible = false;
        //ReloadCurrentControl();
        Timer1.Enabled = true;
    }
    protected void btnNoExit_Click(object sender, EventArgs e)// bip 08052010 
    {
        DeleteTempValuesFromDB();
        Response.Redirect("FJAHome.aspx"); //bipson 18082010// Response.Redirect("CareerJudge.htm");;
    }
}