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
using System.Timers;

public partial class ImageTypeMemmoryQuestions : System.Web.UI.UserControl
{

    string satrttime = "";
    string timenow = "";
    //protected System.Timers.Timer timer11;
    private int test;
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    int userid = 0; string usercode = "";
    DBManagementClass clsclass = new DBManagementClass();
    DataSet ds;
    int pagecount = 0;
    int quesperPage = 1;//bip 121009
    int testId = 0; int testsectionid = 0;
    string imagepadding = "10px;";
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
            
            //if (CheckTime() == true) return;//bip 15052010

            FillTitle();
            
            testId = int.Parse(Session["UserTestId"].ToString());
            if (Session["UserID"] != null)
            {
                userid = int.Parse(Session["UserID"].ToString());
                FillQuestion();
            }
        }
    }

    private void GoToNextPage()
    {
        ClearAllPageCountValues(1);
        Session["evaldirection"] = "Next";
        Session["SubCtrl"] = "AudioQuestions.ascx";
        Response.Redirect("FJAHome.aspx");
    }

    private void GoToPreviousPage()
    {
        //ClearSessionValues();
        Session["MemWordPrevious"] = "True";
        Session["evaldirection"] = "Previous";
        Session["SubCtrl"] = "WordTypeMemmoryQuestions.ascx";
        Response.Redirect("FJAHome.aspx");
    }

    private void FillImageQuestionInstructions(int displaytype)
    {
        bool valueexists = false;
        var InstructionDetails = from instructiondet in dataclass.InstructionByDisplayTypes
                                 where instructiondet.CategoryId == 8 && instructiondet.DisplayTypeId == displaytype
                                 select instructiondet;
        if (InstructionDetails.Count() > 0)
        {
            if (InstructionDetails.First().Instructions != null)
            {
                divInstructions.InnerHtml = InstructionDetails.First().Instructions.ToString();
                valueexists = true;
            }
        }
        
        if (valueexists == false)
            FillCommonInstructions();
        //else FillTitle();
    }

    private void FillTitle()
    {
        var InstructionDetails = from instructiondet in dataclass.TestInstructions
                                 where instructiondet.CategoryId == 8
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
                                 where instructiondet.CategoryId == 9
                                 select instructiondet;
        if (InstructionDetails.Count() > 0)
        {            
            if (InstructionDetails.First().InstructionDetails != null)
                divInstructions.InnerHtml = InstructionDetails.First().InstructionDetails.ToString();
        }
    }

    public static void OnTimerEvent(object source, EventArgs e)
    {
        // lblmessage.Text = "Panel refreshed at: " + DateTime.Now.ToLongTimeString();
        //m_streamWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
        //m_streamWriter.Flush();
    }
        
    protected void ptnPrevious_Click(object sender, EventArgs e)
    {
        Session["displaystatus_img"] = null;
        Session["testimg"] = null;

        int curcount = 0;
        int totalcount = 0, totalquesavail = 0;
        if (Session["totalQuesCount_memImages"] != null)
            totalcount = int.Parse(Session["totalQuesCount_memImages"].ToString());
        if (Session["totalQuesAvailable_memImages"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable_memImages"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecount_memImages"] != null)
            pagecount = int.Parse(Session["pagecount_memImages"].ToString());

        Session["MemImagePrevious"] = "True";

        int curindex = 0;
        curindex = (pagecount - 1) * quesperPage;
        if (curindex >= 0)
        {
            pagecount--;
            Session["pagecount_memImages"] = pagecount; SetCurrentPageCount();// 230110 bip
        }
        else
        {
            GoToPreviousPage();           
        }
        FillQuestion();
    }
    private void SaveAnswer()
    {
        string quescategory = "MemTestImages";
        int testsectionid = 0;
        if (Session["CurrentTestSectionId"] != null)
            testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());
        string answer = "";
        int qusid1 = 0;
        if (rbQues1Answer1.Checked == true)
            answer = "1";
        else if (rbQues1Answer2.Checked == true)
            answer = "2";
        else if (rbQues1Answer3.Checked == true)
            answer = "3";
        else if (rbQues1Answer4.Checked == true)
            answer = "4";
        else if (rbQues1Answer5.Checked == true)
            answer = "5";

        if (lblQuesID1.Text.Trim() != "")
        {
            qusid1 = int.Parse(lblQuesID1.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid1, "", tcellQues1.InnerHtml, answer, userid, testId, testsectionid,quescategory);
        }
    }
    private Boolean CheckTime()
    {
        Boolean timeExpired = false;
        if (CheckTestTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for your test is completed, <br> If you are interested to take this test again please contact our site admin";
            pnlpopup_timer.Visible = true; btnYes_timer_Test.Visible = true; Timer1.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";//bip 10052010
            UpdateTimer.Enabled = false;//bip 15052010
           // return true;
        }
        else if (CheckTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for this Test Section is completed, <br> Do you want to continue with the remaining sections under your Test?";
            pnlpopup_timer.Visible = true; btnYes_timer.Visible = true; btnNo_timer.Visible = true; Timer1.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";//bip 10052010
            UpdateTimer.Enabled = false;//bip 15052010
            //return true;
        }
        else if (CheckTestSecVarTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for this Variable under selected Section is completed, <br> Do you want to continue with the remaining Variables under your Test Section?";
            pnlpopup_timer.Visible = true; btnYes_timer_TestVariable.Visible = true; btnNo_timer.Visible = true; Timer1.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";//bip 10052010
            UpdateTimer.Enabled = false;//bip 15052010
            // return true;
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
                // Timer1.Enabled = false;
                pnlpopup.Visible = true;
                return;
            }
            SaveValues();
        }
        //else { SaveAnswer();  }//bip 10052010 Session["timeExpired"] = "True";
    }
    private void SaveValues()
    {
        SaveAnswer();

        Session["displaystatus_img"] = null;
        Session["testimg"] = null;

        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());

        int curcount = 0;
        int totalcount = 0, totalquesavail = 0;
        if (Session["totalQuesCount_memImages"] != null)
            totalcount = int.Parse(Session["totalQuesCount_memImages"].ToString());
        if (Session["totalQuesAvailable_memImages"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable_memImages"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecount_memImages"] != null)
            pagecount = int.Parse(Session["pagecount_memImages"].ToString());

        int curindex = 0;
        curindex = (pagecount + 1) * quesperPage;
        if (curindex < curcount)
        {
            pagecount++;
            Session["pagecount_memImages"] = pagecount.ToString(); SetCurrentPageCount();
            FillQuestion();
        }
        else
        {
            ClearSessionValues();
            GoToNextPage();
        }


        //if (pagecount < curcount)
        //{
        //    pagecount++;
        //    Session["pagecount_memImages"] = pagecount.ToString(); SetCurrentPageCount();
        //    FillQuestion();
        //}
        //else
        //{
        //    ClearSessionValues();
        //    GoToNextPage();
        //}

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
        int questionid = 0; string questiontype = "MemTestImages";
        dataclass.Procedure_UserTestPageIndex_Temp(userid, testId, testsectionid, testFirstVariableId, testSecondVariableId, pagecount, questiontype);
    }
    private Boolean CheckAnswer()
    {
        Boolean answercompleted = true;

        string answer = "";
        int qusid1 = 0;
        if (rbQues1Answer1.Checked == true)
            answer = "1";
        else if (rbQues1Answer2.Checked == true)
            answer = "2";
        else if (rbQues1Answer3.Checked == true)
            answer = "3";
        else if (rbQues1Answer4.Checked == true)
            answer = "4";
        else if (rbQues1Answer5.Checked == true)
            answer = "5";

        if (answer == "")
        {
            lblpopup.Text += "You have missed question No:" + lblNo1.Text.Trim() + " Please, complete all questions before moving to the next page. <br> Do you want to go to next page? ";
            answercompleted = false;
        }

        return answercompleted;
    }
    private void ClearSessionValues()
    {
       // Session["pagecount_memImages"] = null;       
    }
    private void ClearAllPageCountValues(int index)
    {
        if (index == 0)
            Session["pagecount_memImages"] = null;
        Session["pagecount_audio"] = null;        
        Session["pagecountRating"] = null;
        Session["pagecount_video"] = null;
    }
    protected void UpdateTimer_Tick(object sender, EventArgs e)
    {
        
        ///bip 10052010
        if (Session["timeExpired"] != null)
            if (Session["timeExpired"].ToString() == "True")
                return;

        if (CheckTime() == true) {  return; }// SaveAnswer();
        ///

        bool countcompleted = false;
        if (Session["testimg"] != null)
            test = int.Parse(Session["testimg"].ToString());
        test = test + 1;
        Session["testimg"] = test.ToString();

        bool displayimage = GetDisplayImage();
       
        if (Session["pagecount_memImages"] != null)
            pagecount = int.Parse(Session["pagecount_memImages"].ToString());

        int slno = 0;
        slno = pagecount * quesperPage + 1;
        if (slno < 0)
            slno = 0;

        double interval = 0; int displaytype = 0;
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[slno - 1]["DisplayDuration"].ToString() != "")
                interval = double.Parse(ds.Tables[0].Rows[slno - 1]["DisplayDuration"].ToString());

            if (ds.Tables[0].Rows[slno - 1]["DisplayType"].ToString() != "")
                displaytype = int.Parse(ds.Tables[0].Rows[slno - 1]["DisplayType"].ToString());

            divInstructions.InnerHtml = "";            
        }

        if (displaytype == 1)
        {
            if (test > 1)
                countcompleted = true;
        }
        else if (displaytype == 3)
        {
            if (test > 1)
                countcompleted = true;
        }
        else
            if (test >= 20)
                countcompleted = true;

        if (countcompleted == true)
        {
            UpdateTimer.Enabled = false; Session["displaystatus_img"] = "True"; Session["testimg"] = null; ClearMemImages(); pnlmemImageDisplay.Visible = false; divInstructions.InnerHtml = ""; FillQuestion(); }
    }

    private void ClearMemImages()
    {
        tcelImageDisplay.InnerHtml = "";        
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

        return option;
    }
    private void ClearControls()
    {
        imgQues1Answer1.ImageUrl = ""; imgQues1Answer1.Visible = false;
        imgQues1Answer2.ImageUrl = ""; imgQues1Answer2.Visible = false;
        imgQues1Answer3.ImageUrl = ""; imgQues1Answer3.Visible = false;
        imgQues1Answer4.ImageUrl = ""; imgQues1Answer4.Visible = false;
        imgQues1Answer5.ImageUrl = ""; imgQues1Answer5.Visible = false;
        rbQues1Answer1.Checked = false; rbQues1Answer1.Visible = false; lblA1.Visible = false;
        rbQues1Answer2.Checked = false; rbQues1Answer2.Visible = false; lblB1.Visible = false;
        rbQues1Answer3.Checked = false; rbQues1Answer3.Visible = false; lblC1.Visible = false;
        rbQues1Answer4.Checked = false; rbQues1Answer4.Visible = false; lblD1.Visible = false;
        rbQues1Answer5.Checked = false; rbQues1Answer5.Visible = false; lblE1.Visible = false;
        tcellQues1.InnerHtml = ""; lblNo1.Text = ""; lblQuesID1.Text = "";
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
        int questionid = 0; string questiontype = "MemTestImages";

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

                    querystringquesTemp = "SELECT QuestionID,Question,Answer,Image1,Image2,Image3,Image4,Image5,Image6,Image7,Image8,Image9,Image10,Image11,Image12,Image13,Image14,Image15,Image16,Image17,Image18,Image19,Image20,OptionFile1,OptionFile2,OptionFile3,OptionFile4,OptionFile5,Option1,Option2,Option3,Option4,Option5,DisplayDuration,DisplayType  FROM View_TestBaseQuestionList_memImages where " + questionids;
                    dsTempData = clsclass.GetValuesFromDB(querystringquesTemp);
                    Session["questionColl_memImages"] = dsTempData;

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
                            Session["pagecount_memImages"] = pagecount;
                            Session["displaystatus_img"] = "True";
                            Session["MemImagePrevious"] = "True";
                            Session["MemImageCurrentpage"] = pagecount;
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
            ClearControls();

            string querystring = "";
            //int totalQues = 0;
            int QuestionID = 0;

            if (Session["pagecount_memImages"] != null)
                pagecount = int.Parse(Session["pagecount_memImages"].ToString());
            //else Session["pagecount_memImages"] = "0";               

            string connString = ConfigurationManager.ConnectionStrings["talentscoutConnectionString"].ConnectionString;

            ds = new DataSet();
            //int testsectionid = 0;
            if (Session["CurrentTestSectionId"] != null)
                testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());
            if (Session["questionColl_memImages"] != null)
            {
                ds = (DataSet)Session["questionColl_memImages"]; 
            }
            else
            {//bip 081009
                //int testsectionid = 0;
                if (Session["CurrentTestSectionId"] != null)
                {
                    testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());


                    //  //// 220110 ... bip
                    int testSecondVariableId = 0, testFirstVariableId = 0;
                    if (Session["CurrentTestSecondVariableId"] != null)
                        testSecondVariableId = int.Parse(Session["CurrentTestSecondVariableId"].ToString());
                    if (Session["CurrentTestFirstVariableId"] != null)
                        testFirstVariableId = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
                    int questionid = 0; string questiontype = "MemTestImages";

                    //bip 31072010 .. ds = GetTempData();
                    bool newentry = false;
                    if (ds == null)
                    {                        
                        newentry = true;
                        ds = GetQuestionList(testsectionid);
                    }
                    else if (ds.Tables.Count <= 0)
                    {
                        newentry = true; ds = GetQuestionList(testsectionid); 
                    }
                    else
                    {
                        ds = GetTempData(); //bip 31072010 ..
                       
                    }
                    //
                    // ds = GetQuestionList(testsectionid);
                    if (ds == null)
                    {
                        string evaldirection = "Next";
                        if (Session["evaldirection"] != null)
                            evaldirection = Session["evaldirection"].ToString();
                        if (evaldirection == "Next")
                            GoToNextPage();
                        else GoToPreviousPage();
                        return;

                        // GoToNextPage(); return; 
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
            int slno = 0;
            slno = pagecount * quesperPage + 1;
            if (slno < 0)
                slno = 1;

            int pagecnt = 0;
            int curntquescnt = 0;
            int slnos = 0;

            double interval = 0; int displaytype = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                if (ds.Tables[0].Rows[slno - 1]["DisplayDuration"] != null)//DisplayDuration
                    interval = double.Parse(ds.Tables[0].Rows[slno - 1]["DisplayDuration"].ToString());

                if (ds.Tables[0].Rows[slno - 1]["DisplayType"] != null)
                    displaytype = int.Parse(ds.Tables[0].Rows[slno - 1]["DisplayType"].ToString());

                ////FillImageQuestionInstructions(displaytype);

                bool previousdisplay = false;
                int returnpage = 0;

                if (Session["MemImagePrevious"] != null)
                {
                    if (Session["MemImageCurrentpage"] != null)
                    {
                        returnpage = int.Parse(Session["MemImageCurrentpage"].ToString());
                        if (pagecount <= returnpage)
                        { previousdisplay = true; imgbtnClickHere.Visible = false; }
                    }
                }

                if (previousdisplay == false)
                {
                    if (Session["displaystatus_img"] != null)
                    {
                        if (Session["displaystatus_img"].ToString() != "True")
                        {
                            Session["displaystatus_img"] = "False"; interval = (interval * 1000); UpdateTimer.Interval = int.Parse(interval.ToString());
                            Timer1.Enabled = false;//bip 10052010
                            UpdateTimer.Enabled = true;
                        }
                    }
                    else { Session["displaystatus_img"] = "False"; imgbtnClickHere.Visible = true; pnlClickHere.Visible = true; FillImageQuestionInstructions(displaytype); btnSubmit.Enabled = false; }//interval = 1000; UpdateTimer.Interval = int.Parse(interval.ToString()); UpdateTimer.Enabled = true; }
                }
                else Session["displaystatus_img"] = "True";
                //}
                string imagepath = "QuestionAnswerFiles/";
                if (Session["displaystatus_img"] != null)
                {
                    if (Session["displaystatus_img"].ToString() == "True")
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            // Session["ValueExists"] = "True";

                            Session["totalQuesAvailable_memImages"] = ds.Tables[0].Rows.Count.ToString();
                            int j = 0;
                            for (int i = slno - 1; i < ds.Tables[0].Rows.Count; i++)
                            {

                                string Answer = "";
                                Session["CurrentControlCtrl"] = "ImageTypeMemmoryQuestions.ascx";// bip 08012010
                                Session["ValueExists"] = "True";
                                Timer1.Enabled = true;
                                UpdateTimer.Enabled = false;// bip 15052010
                                if (j >= quesperPage) break;
                                switch (j)
                                {
                                    case 0:
                                        int optindex = 1;
                                        // slnos = CheckSlNo();
                                        lblNo1.Text = slno.ToString() + ".  ";
                                        // lblQues1.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                                        tcellQues1.InnerHtml = ds.Tables[0].Rows[i]["Question"].ToString();

                                        lblQuesID1.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                                        var Ques1 = from Ques in dataclass.EvaluationResults
                                                    where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                                     Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                                    select Ques;//Ques.UserCode == usercode &&
                                        if (Ques1.Count() > 0)
                                            if (Ques1.First().Answer != null)
                                                Answer = Ques1.First().Answer.ToString();

                                        if (ds.Tables[0].Rows[i]["OptionFile1"].ToString() != "" || ds.Tables[0].Rows[i]["OptionFile2"].ToString() != "" || ds.Tables[0].Rows[i]["OptionFile3"].ToString() != "" || ds.Tables[0].Rows[i]["OptionFile4"].ToString() != "" || ds.Tables[0].Rows[i]["OptionFile5"].ToString() != "")
                                        {

                                            if (ds.Tables[0].Rows[i]["OptionFile1"].ToString() != "")
                                            {
                                                rbQues1Answer1.Visible = true; lblA1.Visible = true; imgQues1Answer1.Visible = true;
                                                imgQues1Answer1.ImageUrl = imagepath + ds.Tables[0].Rows[i]["OptionFile1"].ToString();
                                                if (Answer == "1")
                                                    rbQues1Answer1.Checked = true;
                                                optindex++;
                                            }
                                            if (ds.Tables[0].Rows[i]["OptionFile2"].ToString() != "")
                                            {
                                                if (optindex != 2)
                                                    lblB1.Text = GetAnswerOptionOrder(optindex);
                                                rbQues1Answer2.Visible = true; lblB1.Visible = true; imgQues1Answer2.Visible = true;
                                                imgQues1Answer2.ImageUrl = imagepath + ds.Tables[0].Rows[i]["OptionFile2"].ToString();
                                                if (Answer == "2")
                                                    rbQues1Answer2.Checked = true;
                                                optindex++;
                                            }
                                            if (ds.Tables[0].Rows[i]["OptionFile3"].ToString() != "")
                                            {
                                                if (optindex != 3)
                                                    lblC1.Text = GetAnswerOptionOrder(optindex);
                                                rbQues1Answer3.Visible = true; lblC1.Visible = true; imgQues1Answer3.Visible = true;
                                                imgQues1Answer3.ImageUrl = imagepath + ds.Tables[0].Rows[i]["OptionFile3"].ToString();
                                                if (Answer == "3")
                                                    rbQues1Answer3.Checked = true;
                                                optindex++;
                                            }
                                            if (ds.Tables[0].Rows[i]["OptionFile4"].ToString() != "")
                                            {
                                                if (optindex != 4)
                                                    lblD1.Text = GetAnswerOptionOrder(optindex);
                                                rbQues1Answer4.Visible = true; lblD1.Visible = true; imgQues1Answer4.Visible = true;
                                                imgQues1Answer4.ImageUrl = imagepath + ds.Tables[0].Rows[i]["OptionFile4"].ToString();
                                                if (Answer == "4")
                                                    rbQues1Answer4.Checked = true;
                                                optindex++;
                                            }
                                            if (ds.Tables[0].Rows[i]["OptionFile5"].ToString() != "")
                                            {
                                                if (optindex != 5)
                                                    lblE1.Text = GetAnswerOptionOrder(optindex);
                                                rbQues1Answer5.Visible = true; lblE1.Visible = true; imgQues1Answer5.Visible = true;
                                                imgQues1Answer5.ImageUrl = imagepath + ds.Tables[0].Rows[i]["OptionFile5"].ToString();
                                                if (Answer == "5")
                                                    rbQues1Answer5.Checked = true;
                                            }
                                        }
                                        else
                                        {
                                            if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                            {
                                                rbQues1Answer1.Visible = true; lblA1.Visible = true; imgQues1Answer1.Visible = false;
                                                rbQues1Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                                if (Answer == "1")
                                                    rbQues1Answer1.Checked = true;
                                                optindex++;
                                            }
                                            if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                            {
                                                if (optindex != 2)
                                                    lblB1.Text = GetAnswerOptionOrder(optindex);
                                                rbQues1Answer2.Visible = true; lblB1.Visible = true; imgQues1Answer2.Visible = false;
                                                rbQues1Answer2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                                if (Answer == "2")
                                                    rbQues1Answer2.Checked = true;
                                                optindex++;
                                            }
                                            if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                            {
                                                if (optindex != 3)
                                                    lblC1.Text = GetAnswerOptionOrder(optindex);
                                                rbQues1Answer3.Visible = true; lblC1.Visible = true; imgQues1Answer3.Visible = false;
                                                rbQues1Answer3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                                if (Answer == "3")
                                                    rbQues1Answer3.Checked = true;
                                                optindex++;
                                            }
                                            if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                            {
                                                if (optindex != 4)
                                                    lblD1.Text = GetAnswerOptionOrder(optindex);
                                                rbQues1Answer4.Visible = true; lblD1.Visible = true; imgQues1Answer4.Visible = false;
                                                rbQues1Answer4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                                if (Answer == "4")
                                                    rbQues1Answer4.Checked = true;
                                                optindex++;
                                            }
                                            if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                            {
                                                if (optindex != 5)
                                                    lblE1.Text = GetAnswerOptionOrder(optindex);
                                                rbQues1Answer5.Visible = true; lblE1.Visible = true; imgQues1Answer5.Visible = false;
                                                rbQues1Answer5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                                if (Answer == "5")
                                                    rbQues1Answer5.Checked = true;
                                            }
                                        }

                                        btnSubmit.Enabled = true;
                                        curntquescnt++;
                                        j++;
                                        break;
                                }
                            }
                        }
                        else { GoToNextPage(); }
                        Session["curntques"] = curntquescnt;
                    }
                }
            }
        //}
        //catch (Exception ex)
        //{
        //    FillQuestion();//bipson 14082010 to avoid arithmetic error...//conn.Close();
        //}
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

        dtQuestionList.Columns.Add("Image1");
        dtQuestionList.Columns.Add("Image2");
        dtQuestionList.Columns.Add("Image3");
        dtQuestionList.Columns.Add("Image4");
        dtQuestionList.Columns.Add("Image5");
        dtQuestionList.Columns.Add("Image6");
        dtQuestionList.Columns.Add("Image7");
        dtQuestionList.Columns.Add("Image8");
        dtQuestionList.Columns.Add("Image9");
        dtQuestionList.Columns.Add("Image10");
        dtQuestionList.Columns.Add("Image11");
        dtQuestionList.Columns.Add("Image12");
        dtQuestionList.Columns.Add("Image13");
        dtQuestionList.Columns.Add("Image14");
        dtQuestionList.Columns.Add("Image15");
        dtQuestionList.Columns.Add("Image16");
        dtQuestionList.Columns.Add("Image17");
        dtQuestionList.Columns.Add("Image18");
        dtQuestionList.Columns.Add("Image19");
        dtQuestionList.Columns.Add("Image20");

        dtQuestionList.Columns.Add("OptionFile1");
        dtQuestionList.Columns.Add("OptionFile2");
        dtQuestionList.Columns.Add("OptionFile3");
        dtQuestionList.Columns.Add("OptionFile4");
        dtQuestionList.Columns.Add("OptionFile5");

        dtQuestionList.Columns.Add("Option1");
        dtQuestionList.Columns.Add("Option2");
        dtQuestionList.Columns.Add("Option3");
        dtQuestionList.Columns.Add("Option4");
        dtQuestionList.Columns.Add("Option5");
        dtQuestionList.Columns.Add("DisplayDuration");
        dtQuestionList.Columns.Add("DisplayType");
        DataRow drQurstionList;

        // bip 08122009;
        if (secondVariableName != "")
            querystring = "select distinct ImageTypeMemQuestionCount,SectionId from QuestionCount where ImageTypeMemQuestionCount>0 and TestId=" + testId + " and TestSectionId=" + testsectionid + " and SectionName='" + firstVariableName + "' and SectionNameSub1='" + secondVariableName + "' order by sectionid";
        else if (firstVariableName != "")
            querystring = "select distinct ImageTypeMemQuestionCount,SectionId from QuestionCount where ImageTypeMemQuestionCount>0 and TestId=" + testId + " and TestSectionId=" + testsectionid + " and SectionName='" + firstVariableName + "' order by sectionid";
        
        //lblmessage.Text = querystring;//bip 31072010

        DataSet dsQuestioncount = new DataSet();
        dsQuestioncount = clsclass.GetValuesFromDB(querystring);
        if (dsQuestioncount != null)
            if (dsQuestioncount.Tables.Count > 0)
                if (dsQuestioncount.Tables[0].Rows.Count > 0)
                    for (int c = 0; c < dsQuestioncount.Tables[0].Rows.Count; c++)//
                    {

                        int imagetypeMemQuestionCount = int.Parse(dsQuestioncount.Tables[0].Rows[c]["ImageTypeMemQuestionCount"].ToString());
                        int sectionid = int.Parse(dsQuestioncount.Tables[0].Rows[c]["SectionId"].ToString());

                        if (imagetypeMemQuestionCount > 0)
                        {
                            ////querystring = "SELECT TOP (" + imagetypeMemQuestionCount + ") QuestionID,Question,Answer,Image1,Image2,Image3,Image4,Image5,Image6,Image7,Image8,Image9,Image10,Image11,Image12,Image13,Image14,Image15,Image16,Image17,Image18,Image19,Image20,OptionFile1,OptionFile2,OptionFile3,OptionFile4,OptionFile5,Option1,Option2,Option3,Option4,Option5,DisplayDuration,DisplayType  FROM View_TestBaseQuestionList_memImages where Status= 1 AND TestSectionId=" + testsectionid + " AND SectionId=" + sectionid + " AND TestId=" + testId + " ORDER BY RAND((1000*QuestionID)*DATEPART(millisecond, GETDATE())) ";
                            querystring = "select TOP (" + imagetypeMemQuestionCount + ") QuestionID,Question,Answer,Image1,Image2,Image3,Image4,Image5,Image6,Image7,Image8,Image9,Image10,Image11,Image12,Image13,Image14,Image15,Image16,Image17,Image18,Image19,Image20,OptionFile1,OptionFile2,OptionFile3,OptionFile4,OptionFile5,Option1,Option2,Option3,Option4,Option5,DisplayDuration,DisplayType   from(SELECT DISTINCT " +
                                " QuestionID,Question,Answer,Image1,Image2,Image3,Image4,Image5,Image6,Image7,Image8,Image9,Image10,Image11,Image12,Image13,Image14,Image15,Image16,Image17,Image18,Image19,Image20,OptionFile1,OptionFile2,OptionFile3,OptionFile4,OptionFile5,Option1,Option2,Option3,Option4,Option5,DisplayDuration,DisplayType  FROM View_TestBaseQuestionList_memImages where Status= 1 AND TestSectionId=" + testsectionid + " AND SectionId=" + sectionid + " AND TestId=" + testId + " ) as tab ORDER BY RAND((100*QuestionID)*DATEPART(millisecond, GETDATE())) ";//ORDER BY NEWID()";
                                                                               

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
                                        drQurstionList["Image1"] = dsQuesCount.Tables[0].Rows[i]["Image1"];
                                        drQurstionList["Image2"] = dsQuesCount.Tables[0].Rows[i]["Image2"];
                                        drQurstionList["Image3"] = dsQuesCount.Tables[0].Rows[i]["Image3"];
                                        drQurstionList["Image4"] = dsQuesCount.Tables[0].Rows[i]["Image4"];
                                        drQurstionList["Image5"] = dsQuesCount.Tables[0].Rows[i]["Image5"];
                                        drQurstionList["Image6"] = dsQuesCount.Tables[0].Rows[i]["Image6"];
                                        drQurstionList["Image7"] = dsQuesCount.Tables[0].Rows[i]["Image7"];
                                        drQurstionList["Image8"] = dsQuesCount.Tables[0].Rows[i]["Image8"];
                                        drQurstionList["Image9"] = dsQuesCount.Tables[0].Rows[i]["Image9"];
                                        drQurstionList["Image10"] = dsQuesCount.Tables[0].Rows[i]["Image10"];
                                        drQurstionList["Image11"] = dsQuesCount.Tables[0].Rows[i]["Image11"];
                                        drQurstionList["Image12"] = dsQuesCount.Tables[0].Rows[i]["Image12"];
                                        drQurstionList["Image13"] = dsQuesCount.Tables[0].Rows[i]["Image13"];
                                        drQurstionList["Image14"] = dsQuesCount.Tables[0].Rows[i]["Image14"];
                                        drQurstionList["Image15"] = dsQuesCount.Tables[0].Rows[i]["Image15"];
                                        drQurstionList["Image16"] = dsQuesCount.Tables[0].Rows[i]["Image16"];
                                        drQurstionList["Image17"] = dsQuesCount.Tables[0].Rows[i]["Image17"];
                                        drQurstionList["Image18"] = dsQuesCount.Tables[0].Rows[i]["Image18"];
                                        drQurstionList["Image19"] = dsQuesCount.Tables[0].Rows[i]["Image19"];
                                        drQurstionList["Image20"] = dsQuesCount.Tables[0].Rows[i]["Image20"];

                                        drQurstionList["OptionFile1"] = dsQuesCount.Tables[0].Rows[i]["OptionFile1"];
                                        drQurstionList["OptionFile2"] = dsQuesCount.Tables[0].Rows[i]["OptionFile2"];
                                        drQurstionList["OptionFile3"] = dsQuesCount.Tables[0].Rows[i]["OptionFile3"];
                                        drQurstionList["OptionFile4"] = dsQuesCount.Tables[0].Rows[i]["OptionFile4"];
                                        drQurstionList["OptionFile5"] = dsQuesCount.Tables[0].Rows[i]["OptionFile5"];

                                        drQurstionList["Option1"] = dsQuesCount.Tables[0].Rows[i]["Option1"];
                                        drQurstionList["Option2"] = dsQuesCount.Tables[0].Rows[i]["Option2"];
                                        drQurstionList["Option3"] = dsQuesCount.Tables[0].Rows[i]["Option3"];
                                        drQurstionList["Option4"] = dsQuesCount.Tables[0].Rows[i]["Option4"];
                                        drQurstionList["Option5"] = dsQuesCount.Tables[0].Rows[i]["Option5"];

                                        drQurstionList["DisplayDuration"] = dsQuesCount.Tables[0].Rows[i]["DisplayDuration"];
                                        drQurstionList["DisplayType"] = dsQuesCount.Tables[0].Rows[i]["DisplayType"];
                                        dtQuestionList.Rows.Add(drQurstionList);
                                    }
                                }
                        }
                    }
        DataSet dsQuestionList = new DataSet();
        if (dtQuestionList.Rows.Count > 0)
        {
            dsQuestionList.Tables.Add(dtQuestionList); Session["questionColl_memImages"] = dsQuestionList;
            Session["totalQuesCount_memImages"] = dsQuestionList.Tables[0].Rows.Count.ToString();
        }
        else { Session["questionColl_memImages"] = null; dsQuestionList = null; 
            Session["totalQuesCount_memImages"] = null;//bip 28062010
        }

        return dsQuestionList;
    }

    private bool GetDisplayImage()
    {
        bool imageexists = false;
        string imagepath = "QuestionAnswerFiles/";

        string displayimage = "";
        if (Session["testimg"] != null)
            test = int.Parse(Session["testimg"].ToString());
        
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Session["pagecount_memImages"] != null)
                    pagecount = int.Parse(Session["pagecount_memImages"].ToString());

                int slno = 0;
                slno = pagecount * quesperPage + 1;
                if (slno < 0)
                    slno = 0;
                if (slno > ds.Tables[0].Rows.Count)
                { Session["displaystatus_img"] = "True"; Session["testimg"] = null; return false; }

                double interval = 0; int displaytype = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[slno - 1]["DisplayDuration"].ToString() != "")
                        interval = double.Parse(ds.Tables[0].Rows[slno - 1]["DisplayDuration"].ToString());

                    if (ds.Tables[0].Rows[slno - 1]["DisplayType"].ToString() != "")
                        displaytype = int.Parse(ds.Tables[0].Rows[slno - 1]["DisplayType"].ToString());
                    
                }

                if (displaytype == 1)
                {
                    imageexists = GetMemImageCollection(slno - 1);
                }
                else
                {
                    if (test > 20) { Session["displaystatus_img"] = "True"; pnlmemImageDisplay.Visible = false; return false; }
                    displayimage = ds.Tables[0].Rows[slno - 1][test + 2].ToString();
                    if (displayimage == "")
                    { test = test + 1; Session["testimg"] = test.ToString(); GetDisplayImage(); }

                   // imgQuestion1.Visible = true; 
                    imageexists = true; pnlmemImageDisplay.Visible = true;
                    //imgQuestion1.ImageUrl = imagepath + displayimage;
                    string wordstructure = "<table style='width:90%'>";//style='padding: " + imagepadding + "'
                    wordstructure += "<td style='text-align: center;'><img alt='' src='" + imagepath + displayimage + "' /></td></tr></table>";
                    tcelImageDisplay.InnerHtml = wordstructure;
                }
            }
        }
        if (imageexists == true) { pnlmemImageDisplay.Visible = true;  }//FillImageQuestionInstructions();
        else divInstructions.InnerHtml = "";
        return imageexists;
    }
    private bool GetMemImageCollection(int rowindex)
    {        
       
        bool imageexists = false;
        string imgvalues = SetMemImageCollection(rowindex);
        if (imgvalues != "")
            imageexists = true;

        tcelImageDisplay.InnerHtml = imgvalues;
        return imageexists;
    }
    private string SetMemImageCollection(int rowindex)
    {      
      
        string displayimagepath = "QuestionAnswerFiles/";
       
        string strBreak = "<BR>";//<BR><BR>";
        string space = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        int index = 0;
        string displayword = "";
        string wordstructure = "<table style='width:90%;'>";//;line-height:30px;
        if (ds.Tables[0].Rows[rowindex]["Image1"] != null && ds.Tables[0].Rows[rowindex]["Image1"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image1"].ToString() + "'/></td>"; index++;
        }

        if (ds.Tables[0].Rows[rowindex]["Image2"] != null && ds.Tables[0].Rows[rowindex]["Image2"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image2"].ToString() + "'/></td>"; index++;
        }
        if (ds.Tables[0].Rows[rowindex]["Image3"] != null && ds.Tables[0].Rows[rowindex]["Image3"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image3"].ToString() + "'/></td>"; index++;
        }
        if (ds.Tables[0].Rows[rowindex]["Image4"] != null && ds.Tables[0].Rows[rowindex]["Image4"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image4"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image5"] != null && ds.Tables[0].Rows[rowindex]["Image5"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image5"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image6"] != null && ds.Tables[0].Rows[rowindex]["Image6"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image6"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image7"] != null && ds.Tables[0].Rows[rowindex]["Image7"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image7"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image8"] != null && ds.Tables[0].Rows[rowindex]["Image8"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image8"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image9"] != null && ds.Tables[0].Rows[rowindex]["Image9"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image9"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image10"] != null && ds.Tables[0].Rows[rowindex]["Image10"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image10"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image11"] != null && ds.Tables[0].Rows[rowindex]["Image11"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image11"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image12"] != null && ds.Tables[0].Rows[rowindex]["Image12"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image12"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image13"] != null && ds.Tables[0].Rows[rowindex]["Image13"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image13"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image14"] != null && ds.Tables[0].Rows[rowindex]["Image14"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image14"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image15"] != null && ds.Tables[0].Rows[rowindex]["Image15"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image15"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image16"] != null && ds.Tables[0].Rows[rowindex]["Image16"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image16"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image17"] != null && ds.Tables[0].Rows[rowindex]["Image17"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image17"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image18"] != null && ds.Tables[0].Rows[rowindex]["Image18"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image18"].ToString() + "'/></td>";
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image19"] != null && ds.Tables[0].Rows[rowindex]["Image19"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image19"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Image20"] != null && ds.Tables[0].Rows[rowindex]["Image20"].ToString() != "")
        {           
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td style='padding: " + imagepadding + "'><img alt='' src='" + displayimagepath + ds.Tables[0].Rows[rowindex]["Image20"].ToString() + "'/></td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (index > 0)
        {
            for (int i = 0; i < index; i++)
            {
                wordstructure += "<td>&nbsp; </td>";
            }
            wordstructure += "</tr>";
        }
        wordstructure += "</table>";

        return wordstructure;
    }

    protected void imgbtnClickHere_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["pagecount_memWords"] != null)
            pagecount = int.Parse(Session["pagecount_memWords"].ToString());

        int selPage = 0;
        if (Session["MemImagePrevious"] != null)
        {
            if (Session["MemImageCurrentpage"] != null)
            {
                selPage = int.Parse(Session["MemImageCurrentpage"].ToString());
                if (pagecount > selPage)
                { Session["MemImagePrevious"] = null; Session["MemImageCurrentpage"] = pagecount; }
            }
            else Session["MemImageCurrentpage"] = pagecount.ToString();
        }
        else Session["MemImageCurrentpage"] = pagecount.ToString();

        int interval = 100; UpdateTimer.Interval = interval; UpdateTimer.Enabled = true;
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
       // Timer1.Enabled = true;
        SaveValues(); pnlpopup.Visible = false;
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
                        //return true;

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
                if ( tsNow> tsDuration)
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

    protected void Timer1_Tick1(object sender, EventArgs e)
    {        
        if (Session["timeExpired"] != null)
            if (Session["timeExpired"].ToString() == "True")              
                return;
            
        CheckTime();// bip 10052010
       
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
       // Timer1.Enabled = true;
    }
}
