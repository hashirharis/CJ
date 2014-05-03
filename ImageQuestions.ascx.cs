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

public partial class ImageQuestions : System.Web.UI.UserControl
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
    int quesperPage = 5;//bip 121009

    int testId = 0; int testsectionid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if(!IsPostBack)
        if (Session["UserTestId"] != null)
        {
            if (Session["timeExpired"] != null)
                if (Session["timeExpired"].ToString() == "True")
                {                    
                    CheckTime();
                    return;
                }

            //if (CheckTime() == true) return;//bip 15052010
           
            testId = int.Parse(Session["UserTestId"].ToString());
            if (Session["UserID"] != null)
            {
                userid = int.Parse(Session["UserID"].ToString());                
                FillQuestion();
            }
        }
    }
    private void FillImageQuestionInstructions()
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
            quesrystring = "select InstructionDetails,InstructionImage1,InstructionDetails2,InstructionImage2,InstructionDetails3 from TestSectionVariablewiseInstructions where CategoryId =4 and TestId=" + testId + " and TestSectionId =" + testSectionID + " and FirstVariableId=" + testFirstVariableId + " and SecondVariableId =" + testSecondVariableId;
        else if (testFirstVariableId > 0)
            quesrystring = "select InstructionDetails,InstructionImage1,InstructionDetails2,InstructionImage2,InstructionDetails3 from TestSectionVariablewiseInstructions where CategoryId =4 and TestId=" + testId + " and TestSectionId =" + testSectionID + " and FirstVariableId=" + testFirstVariableId;

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
                                 where instructiondet.CategoryId == 4
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
                                 where instructiondet.CategoryId == 4
                                 select instructiondet;
        if (InstructionDetails.Count() > 0)
        {
            if (InstructionDetails.First().Title != null)
                divtitle.InnerHtml = InstructionDetails.First().Title.ToString();
            if (InstructionDetails.First().InstructionDetails != null)
                divInstructions.InnerHtml = InstructionDetails.First().InstructionDetails.ToString();

        }

    }

    protected void ptnPrevious_Click(object sender, EventArgs e)
    {
        int curcount = 0;
        int totalcount = 0, totalquesavail = 0;
        if (Session["totalQuesCount_img"] != null)
            totalcount = int.Parse(Session["totalQuesCount_img"].ToString());
        if (Session["totalQuesAvailable_img"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable_img"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecount_img"] != null)
            pagecount = int.Parse(Session["pagecount_img"].ToString());

        int curindex = 0;
        curindex = (pagecount - 1) * quesperPage;
        if (curindex >= 0)
        {
            pagecount--;
            Session["pagecount_img"] = pagecount;
            SetCurrentPageCount();// 230110 bip
        }
        else
        {
            GoToPreviousPage();
        }
        FillQuestion();
    }

    private void GoToPreviousPage()
    {
        //
        int testsectionid = 0;
        if (Session["CurrentTestSectionId"] != null)
        {
            testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());
        }       

       
        Session["evaldirection"] = "Previous";
        Session["SubCtrl"] = "FillBalnksQues.ascx";
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

        //usercode = Session["UserCode"].ToString();
        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());

        int curcount = 0;
        int totalcount = 0, totalquesavail = 0;
        if (Session["totalQuesCount_img"] != null)
            totalcount = int.Parse(Session["totalQuesCount_img"].ToString());
        if (Session["totalQuesAvailable_img"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable_img"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecount_img"] != null)
            pagecount = int.Parse(Session["pagecount_img"].ToString());

        int curindex = 0;
        curindex = (pagecount + 1) * quesperPage;
        if (curindex < curcount)
        {
            pagecount++;
            Session["pagecount_img"] = pagecount;SetCurrentPageCount();
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
        int questionid = 0; string questiontype = "ImageType";
        dataclass.Procedure_UserTestPageIndex_Temp(userid, testId, testsectionid, testFirstVariableId, testSecondVariableId, pagecount, questiontype);
    }
    private Boolean CheckAnswer()
    {
        Boolean answercompleted = true;
        string answer = "";
        string message = "You have missed question No:";
        if (lblQuesID1.Text.Trim() != "")
        {
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

            if (answer == "")
            {
               // lblpopup.Text += "QuestionNo:" + lblQuesID1.Text.Trim();
                message += lblNo1.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

        answer = "";

        if (lblQuesID2.Text.Trim() != "")
        {
            if (rbQues2Answer1.Checked == true)
                answer = "1";
            else if (rbQues2Answer2.Checked == true)
                answer = "2";
            else if (rbQues2Answer3.Checked == true)
                answer = "3";
            else if (rbQues2Answer4.Checked == true)
                answer = "4";
            else if (rbQues2Answer5.Checked == true)
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

            if (answer == "")
            {
               // lblpopup.Text += "QuestionNo:" + lblQuesID1.Text.Trim();
                message += lblNo2.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

        answer = "";
        if (lblQuesID3.Text.Trim() != "")
        {
            if (rbQues3Answer1.Checked == true)
                answer = "1";
            else if (rbQues3Answer2.Checked == true)
                answer = "2";
            else if (rbQues3Answer3.Checked == true)
                answer = "3";
            else if (rbQues3Answer4.Checked == true)
                answer = "4";
            else if (rbQues3Answer5.Checked == true)
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

            if (answer == "")
            {
               // lblpopup.Text += "QuestionNo:" + lblQuesID1.Text.Trim();
                message += lblNo3.Text.Trim() + ", ";
                answercompleted = false;
            }
        }

        answer = "";
        if (lblQuesID4.Text.Trim() != "")
        {
            if (rbQues4Answer1.Checked == true)
                answer = "1";
            else if (rbQues4Answer2.Checked == true)
                answer = "2";
            else if (rbQues4Answer3.Checked == true)
                answer = "3";
            else if (rbQues4Answer4.Checked == true)
                answer = "4";
            else if (rbQues4Answer5.Checked == true)
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
            if (rbQues5Answer1.Checked == true)
                answer = "1";
            else if (rbQues5Answer2.Checked == true)
                answer = "2";
            else if (rbQues5Answer3.Checked == true)
                answer = "3";
            else if (rbQues5Answer4.Checked == true)
                answer = "4";
            else if (rbQues5Answer5.Checked == true)
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
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session["SubCtrl"] = null;
        Response.Redirect("FJAHome.aspx");
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
        dtQuestionList.Columns.Add("QuestionFileName");
        dtQuestionList.Columns.Add("QuestionFileNameSub1");
        dtQuestionList.Columns.Add("Option1FileName");
        dtQuestionList.Columns.Add("Option2FileName");
        dtQuestionList.Columns.Add("Option3FileName");
        dtQuestionList.Columns.Add("Option4FileName");
        dtQuestionList.Columns.Add("Option5FileName");
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
            querystring = "select distinct ImageQuestionCount,SectionId from QuestionCount where ImageQuestionCount>0 and TestId=" + testId + " and TestSectionId=" + testsectionid + " and SectionName='" + firstVariableName + "' and SectionNameSub1='" + secondVariableName + "' order by sectionid";
        else if (firstVariableName != "")
            querystring = "select distinct ImageQuestionCount,SectionId from QuestionCount where ImageQuestionCount>0 and TestId=" + testId + " and TestSectionId=" + testsectionid + " and SectionName='" + firstVariableName + "' order by sectionid";

        DataSet dsQuestioncount = new DataSet();
        dsQuestioncount = clsclass.GetValuesFromDB(querystring);
        if (dsQuestioncount != null)
            if (dsQuestioncount.Tables.Count > 0)
                if (dsQuestioncount.Tables[0].Rows.Count > 0)
                    for (int c = 0; c < dsQuestioncount.Tables[0].Rows.Count; c++)//
                    {
                        int imgQuescount = int.Parse(dsQuestioncount.Tables[0].Rows[c]["ImageQuestionCount"].ToString());
                        int sectionid = int.Parse(dsQuestioncount.Tables[0].Rows[c]["SectionId"].ToString());

                        if (imgQuescount > 0)
                        {
                            querystring = "SELECT TOP (" + imgQuescount + ") QuestionID,Question,Answer,QuestionFileName,Option1FileName,Option2FileName,Option3FileName,Option4FileName,Option5FileName,QuestionFileNameSub1,Option1,Option2,Option3,Option4,Option5,Option6,Option7,Option8,Option9,Option10  FROM View_TestBaseQuestionList where Category = 'ImageType' AND TestBaseQuestionStatus=1 AND TestSectionId=" + testsectionid + " AND SectionId=" + sectionid + " AND TestId=" + testId + " ORDER BY RAND((100*QuestionID)*DATEPART(millisecond, GETDATE())) ";

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
                                        drQurstionList["QuestionFileName"] = dsQuesCount.Tables[0].Rows[i]["QuestionFileName"];
                                        drQurstionList["QuestionFileNameSub1"] = dsQuesCount.Tables[0].Rows[i]["QuestionFileNameSub1"];
                                        drQurstionList["Option1FileName"] = dsQuesCount.Tables[0].Rows[i]["Option1FileName"];
                                        drQurstionList["Option2FileName"] = dsQuesCount.Tables[0].Rows[i]["Option2FileName"];
                                        drQurstionList["Option3FileName"] = dsQuesCount.Tables[0].Rows[i]["Option3FileName"];
                                        drQurstionList["Option4FileName"] = dsQuesCount.Tables[0].Rows[i]["Option4FileName"];
                                        drQurstionList["Option5FileName"] = dsQuesCount.Tables[0].Rows[i]["Option5FileName"];
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
        DataSet dsQuestionList = new DataSet();
        if (dtQuestionList.Rows.Count > 0)
        {
            dsQuestionList.Tables.Add(dtQuestionList); Session["questionColl_img"] = dsQuestionList;
            Session["totalQuesCount_img"] = dsQuestionList.Tables[0].Rows.Count.ToString();
        }
        else { Session["questionColl_img"] = null; dsQuestionList = null; }

        return dsQuestionList;
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
        int questionid = 0; string questiontype = "ImageType";

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
                    //querystring = "SELECT TOP (" + imgQuescount + ") QuestionID,Question,Answer,QuestionFileName,Option1FileName,Option2FileName,Option3FileName,Option4FileName,Option5FileName,QuestionFileNameSub1,Option1,Option2,Option3,Option4,Option5  FROM View_TestBaseQuestionList where Category = 'ImageType' AND TestBaseQuestionStatus=1 AND TestSectionId=" + testsectionid + " AND SectionId=" + sectionid + " AND TestId=" + testId + " ORDER BY RAND((1000*QuestionID)*DATEPART(millisecond, GETDATE())) ";
                    querystringquesTemp = "SELECT QuestionID,Question,Answer,QuestionFileName,Option1FileName,Option2FileName,Option3FileName,Option4FileName,Option5FileName,QuestionFileNameSub1,Option1,Option2,Option3,Option4,Option5,Option6,Option7,Option8,Option9,Option10  FROM View_TestBaseQuestionList where " + questionids;
                    dsTempData = clsclass.GetValuesFromDB(querystringquesTemp);
                    Session["questionColl_img"] = dsTempData;
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
                            Session["pagecount_img"] = pagecount;                            
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
            
            if (Session["pagecount_img"] != null)
                pagecount = int.Parse(Session["pagecount_img"].ToString()); 

            string connString = ConfigurationManager.ConnectionStrings["talentscoutConnectionString"].ConnectionString;
            
            ds = new DataSet();//int testsectionid = 0;
            if (Session["CurrentTestSectionId"] != null)
                testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());  
            //da.Fill(ds);
            if (Session["questionColl_img"] != null)
                ds = (DataSet)Session["questionColl_img"];
            else
            {
                //bip 081009
               // int testsectionid = 0;
                if (Session["CurrentTestSectionId"] != null)
                {
                    testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());
                    //  //// 220110 ... bip
                    int testSecondVariableId = 0, testFirstVariableId = 0;
                    if (Session["CurrentTestSecondVariableId"] != null)
                        testSecondVariableId = int.Parse(Session["CurrentTestSecondVariableId"].ToString());
                    if (Session["CurrentTestFirstVariableId"] != null)
                        testFirstVariableId = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
                    int questionid = 0; string questiontype = "ImageType";

                    ds = GetTempData();
                    bool newentry = false;
                    if (ds == null)
                    {
                        newentry = true;
                        ds = GetQuestionList(testsectionid);
                    }
                    else if (ds.Tables.Count <= 0)
                    { newentry = true; ds = GetQuestionList(testsectionid); }

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
            int slno = 0;
            slno = pagecount * quesperPage + 1;
            if (slno < 0)
                slno = 1;

            int pagecnt = 0;
            int curntquescnt = 0;
            int slnos = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
               // Session["ValueExists"] = "True";

                FillImageQuestionInstructions();

               Session["totalQuesAvailable_img"] = ds.Tables[0].Rows.Count.ToString();
                int j = 0;
                for (int i = slno - 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (j >= quesperPage) break;
                    string Answer = "****";
                    Session["CurrentControlCtrl"] = "ImageQuestions.ascx";// bip 08012010
                    Session["ValueExists"] = "True";

                    int optindex = 1;
                    switch (j)
                    {
                        case 0:
                           
                            // slnos = CheckSlNo();
                            lblNo1.Text = slno.ToString() + ".  ";
                            //lblQues1.Text = ds.Tables[0].Rows[i]["Question"].ToString();
                            tcellQues1.InnerHtml= ds.Tables[0].Rows[i]["Question"].ToString();

                            lblQuesID1.Text = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                            var Ques1 = from Ques in dataclass.EvaluationResults
                                        where Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;//Ques.UserCode == usercode &&
                            if (Ques1.Count() > 0)
                                if (Ques1.First().Answer != null)
                                    Answer = Ques1.First().Answer.ToString();
                            if (ds.Tables[0].Rows[i]["QuestionFileName"].ToString() != "")
                            {
                                imgQuestion1.Visible = true;
                                imgQuestion1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileName"].ToString();
                            //Label1.Text=ds.Tables[0].Rows[i]["QuestionFileName"].ToString();
                            }

                            if (ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString() != "")
                            {
                                imgQuestion1Sub1.Visible = true;
                                imgQuestion1Sub1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString();
                          // Label2.Text=ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["Option1FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option2FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option3FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option4FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option5FileName"].ToString() != "")
                            {
                                if (ds.Tables[0].Rows[i]["Option1FileName"].ToString() != "")
                                {
                                    imgQues1Answer1.Visible = true; rbQues1Answer1.Visible = true; lblA1.Visible = true;
                                    imgQues1Answer1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option1FileName"].ToString();
                                    if (Answer == "1")
                                        rbQues1Answer1.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option2FileName"].ToString() != "")
                                {
                                    if (optindex != 2)
                                        lblB1.Text = GetAnswerOptionOrder(optindex);
                                    imgQues1Answer2.Visible = true; rbQues1Answer2.Visible = true; lblB1.Visible = true;
                                    imgQues1Answer2.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option2FileName"].ToString();
                                    if (Answer == "2")
                                        rbQues1Answer2.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option3FileName"].ToString() != "")
                                {
                                    if (optindex != 3)
                                        lblC1.Text = GetAnswerOptionOrder(optindex);
                                    imgQues1Answer3.Visible = true; rbQues1Answer3.Visible = true; lblC1.Visible = true;
                                    imgQues1Answer3.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option3FileName"].ToString();
                                    if (Answer == "3")
                                        rbQues1Answer3.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option4FileName"].ToString() != "")
                                {
                                    if (optindex != 4)
                                        lblD1.Text = GetAnswerOptionOrder(optindex);
                                    imgQues1Answer4.Visible = true; rbQues1Answer4.Visible = true; lblD1.Visible = true;
                                    imgQues1Answer4.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option4FileName"].ToString();
                                    if (Answer == "4")
                                        rbQues1Answer4.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option5FileName"].ToString() != "")
                                {
                                    if (optindex != 5)
                                        lblE1.Text = GetAnswerOptionOrder(optindex);
                                    imgQues1Answer5.Visible = true; rbQues1Answer5.Visible = true; lblE1.Visible = true;
                                    imgQues1Answer5.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option5FileName"].ToString();
                                    if (Answer == "5")
                                        rbQues1Answer5.Checked = true;
                                }
                            }
                            else
                            {
                                if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                {
                                    imgQues1Answer1.Visible = false; rbQues1Answer1.Visible = true; lblA1.Visible = true;
                                    rbQues1Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                    if (Answer == "1")
                                        rbQues1Answer1.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                {
                                    if (optindex != 2)
                                        lblB1.Text = GetAnswerOptionOrder(optindex);
                                    imgQues1Answer2.Visible = false; rbQues1Answer2.Visible = true; lblB1.Visible = true;
                                    rbQues1Answer2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                    if (Answer == "2")
                                        rbQues1Answer2.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                {
                                    if (optindex != 3)
                                        lblC1.Text = GetAnswerOptionOrder(optindex);
                                    imgQues1Answer3.Visible = false; rbQues1Answer3.Visible = true; lblC1.Visible = true;
                                    rbQues1Answer3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                    if (Answer == "3")
                                        rbQues1Answer3.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                {
                                    if (optindex != 4)
                                        lblD1.Text = GetAnswerOptionOrder(optindex);
                                    imgQues1Answer4.Visible = false; rbQues1Answer4.Visible = true; lblD1.Visible = true;
                                    rbQues1Answer4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                    if (Answer == "4")
                                        rbQues1Answer4.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                {
                                    if (optindex != 5)
                                        lblE1.Text = GetAnswerOptionOrder(optindex);
                                    imgQues1Answer5.Visible = false; rbQues1Answer5.Visible = true; lblE1.Visible = true;
                                    rbQues1Answer5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                    if (Answer == "5")
                                        rbQues1Answer5.Checked = true;
                                }


                                if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                                {
                                    if (optindex != 6)
                                        lblF1.Text = GetAnswerOptionOrder(optindex);
                                    rbQues1Answer6.Visible = true; lblF1.Visible = true;
                                    rbQues1Answer6.Text = ds.Tables[0].Rows[i]["Option6"].ToString();
                                    if (Answer == "6")
                                        rbQues1Answer6.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                                {
                                    if (optindex != 7)
                                        lblG1.Text = GetAnswerOptionOrder(optindex);
                                    rbQues1Answer7.Visible = true; lblG1.Visible = true;
                                    rbQues1Answer7.Text = ds.Tables[0].Rows[i]["Option7"].ToString();
                                    if (Answer == "7")
                                        rbQues1Answer7.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                                {
                                    if (optindex != 8)
                                        lblH1.Text = GetAnswerOptionOrder(optindex);
                                    rbQues1Answer8.Visible = true; lblH1.Visible = true;
                                    rbQues1Answer8.Text = ds.Tables[0].Rows[i]["Option8"].ToString();
                                    if (Answer == "8")
                                        rbQues1Answer8.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                                {
                                    if (optindex != 9)
                                        lblI1.Text = GetAnswerOptionOrder(optindex);
                                    rbQues1Answer9.Visible = true; lblI1.Visible = true;
                                    rbQues1Answer9.Text = ds.Tables[0].Rows[i]["Option9"].ToString();
                                    if (Answer == "9")
                                        rbQues1Answer9.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                                {
                                    if (optindex != 10)
                                        lblJ1.Text = GetAnswerOptionOrder(optindex);
                                    rbQues1Answer10.Visible = true; lblJ1.Visible = true;
                                    rbQues1Answer10.Text = ds.Tables[0].Rows[i]["Option10"].ToString();
                                    if (Answer == "10")
                                        rbQues1Answer10.Checked = true;
                                }


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
                                        where Ques.UserCode == usercode && Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques2.Count() > 0)
                                if (Ques2.First().Answer != null)
                                    Answer = Ques2.First().Answer.ToString();

                            if (ds.Tables[0].Rows[i]["QuestionFileName"].ToString() != "")
                            {
                                imgQuestion2.Visible = true;
                                imgQuestion2.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileName"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString() != "")
                            {
                                imgQuestion2Sub1.Visible = true;
                                imgQuestion2Sub1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["Option1FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option2FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option3FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option4FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option5FileName"].ToString() != "")
                            {
                                if (ds.Tables[0].Rows[i]["Option1FileName"].ToString() != "")
                                {
                                    imgQues2Answer1.Visible = true; rbQues2Answer1.Visible = true; lblA2.Visible = true;
                                    imgQues2Answer1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option1FileName"].ToString();
                                    // radioQues1.Items.Add(ds.Tables[0].Rows[i]["Option2"].ToString());
                                    if (Answer == "1")
                                        rbQues2Answer1.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option2FileName"].ToString() != "")
                                {
                                    if (optindex != 2)
                                        lblB2.Text = GetAnswerOptionOrder(optindex);
                                    imgQues2Answer2.Visible = true; rbQues2Answer2.Visible = true; lblB2.Visible = true;
                                    imgQues2Answer2.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option2FileName"].ToString();
                                    if (Answer == "2")
                                        rbQues2Answer2.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option3FileName"].ToString() != "")
                                {
                                    if (optindex != 3)
                                        lblC2.Text = GetAnswerOptionOrder(optindex);
                                    imgQues2Answer3.Visible = true; rbQues2Answer3.Visible = true; lblC2.Visible = true;
                                    imgQues2Answer3.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option3FileName"].ToString();
                                    if (Answer == "3")
                                        rbQues2Answer3.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option4FileName"].ToString() != "")
                                {
                                    if (optindex != 4)
                                        lblD2.Text = GetAnswerOptionOrder(optindex);
                                    imgQues2Answer4.Visible = true; rbQues2Answer4.Visible = true; lblD2.Visible = true;
                                    imgQues2Answer4.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option4FileName"].ToString();
                                    if (Answer == "4")
                                        rbQues2Answer4.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option5FileName"].ToString() != "")
                                {
                                    if (optindex != 5)
                                        lblE2.Text = GetAnswerOptionOrder(optindex);
                                    imgQues2Answer5.Visible = true; rbQues2Answer5.Visible = true; lblE2.Visible = true;
                                    imgQues2Answer5.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option5FileName"].ToString();
                                    if (Answer == "5")
                                        rbQues2Answer5.Checked = true;
                                }
                            }
                            else
                            {
                                if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                {
                                    imgQues2Answer1.Visible = false; rbQues2Answer1.Visible = true; lblA2.Visible = true;
                                    rbQues2Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                    // radioQues1.Items.Add(ds.Tables[0].Rows[i]["Option2"].ToString());
                                    if (Answer == "1")
                                        rbQues2Answer1.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                {
                                    if (optindex != 2)
                                        lblB2.Text = GetAnswerOptionOrder(optindex);
                                    imgQues2Answer2.Visible = false; rbQues2Answer2.Visible = true; lblB2.Visible = true;
                                    rbQues2Answer2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                    if (Answer == "2")
                                        rbQues2Answer2.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                {
                                    if (optindex != 3)
                                        lblC2.Text = GetAnswerOptionOrder(optindex);
                                    imgQues2Answer3.Visible = false; rbQues2Answer3.Visible = true; lblC2.Visible = true;
                                    rbQues2Answer3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                    if (Answer == "3")
                                        rbQues2Answer3.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                {
                                    if (optindex != 4)
                                        lblD2.Text = GetAnswerOptionOrder(optindex);
                                    imgQues2Answer4.Visible = false; rbQues2Answer4.Visible = true; lblD2.Visible = true;
                                    rbQues2Answer4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                    if (Answer == "4")
                                        rbQues2Answer4.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                {
                                    if (optindex != 5)
                                        lblE2.Text = GetAnswerOptionOrder(optindex);
                                    imgQues2Answer5.Visible = false; rbQues2Answer5.Visible = true; lblE2.Visible = true;
                                    rbQues2Answer5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                    if (Answer == "5")
                                        rbQues2Answer5.Checked = true;
                                }


                                if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                                {
                                    if (optindex != 6)
                                        lblF2.Text = GetAnswerOptionOrder(optindex);
                                    rbQues2Answer6.Visible = true; lblF2.Visible = true;
                                    rbQues2Answer6.Text = ds.Tables[0].Rows[i]["Option6"].ToString();
                                    if (Answer == "6")
                                        rbQues2Answer6.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                                {
                                    if (optindex != 7)
                                        lblG2.Text = GetAnswerOptionOrder(optindex);
                                    rbQues2Answer7.Visible = true; lblG2.Visible = true;
                                    rbQues2Answer7.Text = ds.Tables[0].Rows[i]["Option7"].ToString();
                                    if (Answer == "7")
                                        rbQues2Answer7.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                                {
                                    if (optindex != 8)
                                        lblH2.Text = GetAnswerOptionOrder(optindex);
                                    rbQues2Answer8.Visible = true; lblH2.Visible = true;
                                    rbQues2Answer8.Text = ds.Tables[0].Rows[i]["Option8"].ToString();
                                    if (Answer == "8")
                                        rbQues2Answer8.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                                {
                                    if (optindex != 9)
                                        lblI2.Text = GetAnswerOptionOrder(optindex);
                                    rbQues2Answer9.Visible = true; lblI2.Visible = true;
                                    rbQues2Answer9.Text = ds.Tables[0].Rows[i]["Option9"].ToString();
                                    if (Answer == "9")
                                        rbQues2Answer9.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                                {
                                    if (optindex != 10)
                                        lblJ2.Text = GetAnswerOptionOrder(optindex);
                                    rbQues2Answer10.Visible = true; lblJ2.Visible = true;
                                    rbQues2Answer10.Text = ds.Tables[0].Rows[i]["Option10"].ToString();
                                    if (Answer == "10")
                                        rbQues2Answer10.Checked = true;
                                }
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
                                        where Ques.UserCode == usercode && Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques3.Count() > 0)
                                if (Ques3.First().Answer != null)
                                    Answer = Ques3.First().Answer.ToString();

                            if (ds.Tables[0].Rows[i]["QuestionFileName"].ToString() != "")
                            {
                                imgQuestion3.Visible = true;
                                imgQuestion3.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileName"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString() != "")
                            {
                                imgQuestion3Sub1.Visible = true;
                                imgQuestion3Sub1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["Option1FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option2FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option3FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option4FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option5FileName"].ToString() != "")
                            {
                                if (ds.Tables[0].Rows[i]["Option1FileName"].ToString() != "")
                                {
                                    imgQues3Answer1.Visible = true; rbQues3Answer1.Visible = true; lblA3.Visible = true;
                                    imgQues3Answer1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option1FileName"].ToString();
                                    // radioQues1.Items.Add(ds.Tables[0].Rows[i]["Option2"].ToString());
                                    if (Answer == "1")
                                        rbQues3Answer1.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option2FileName"].ToString() != "")
                                {
                                    if (optindex != 2)
                                        lblB3.Text = GetAnswerOptionOrder(optindex);
                                    imgQues3Answer2.Visible = true; rbQues3Answer2.Visible = true; lblB3.Visible = true;
                                    imgQues3Answer2.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option2FileName"].ToString();
                                    if (Answer == "2")
                                        rbQues3Answer2.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option3FileName"].ToString() != "")
                                {
                                    if (optindex != 3)
                                        lblC3.Text = GetAnswerOptionOrder(optindex);
                                    imgQues3Answer3.Visible = true; rbQues3Answer3.Visible = true; lblC3.Visible = true;
                                    imgQues3Answer3.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option3FileName"].ToString();
                                    if (Answer == "3")
                                        rbQues3Answer3.Checked = true;
                                    optindex++;
                                }

                                if (ds.Tables[0].Rows[i]["Option4FileName"].ToString() != "")
                                {
                                    if (optindex != 4)
                                        lblD3.Text = GetAnswerOptionOrder(optindex);
                                    imgQues3Answer4.Visible = true; rbQues3Answer4.Visible = true; lblD3.Visible = true;
                                    imgQues3Answer4.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option4FileName"].ToString();
                                    if (Answer == "4")
                                        rbQues3Answer4.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option5FileName"].ToString() != "")
                                {
                                    if (optindex != 5)
                                        lblE3.Text = GetAnswerOptionOrder(optindex);
                                    imgQues3Answer5.Visible = true; rbQues3Answer5.Visible = true; lblE3.Visible = true;
                                    imgQues3Answer5.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option5FileName"].ToString();
                                    if (Answer == "5")
                                        rbQues3Answer5.Checked = true;
                                }
                            }
                            else
                            {
                                if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                {
                                    imgQues3Answer1.Visible = false; rbQues3Answer1.Visible = true; lblA3.Visible = true;
                                    rbQues3Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                    // radioQues1.Items.Add(ds.Tables[0].Rows[i]["Option2"].ToString());
                                    if (Answer == "1")
                                        rbQues3Answer1.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                {
                                    if (optindex != 2)
                                        lblB3.Text = GetAnswerOptionOrder(optindex);
                                    imgQues3Answer2.Visible = false; rbQues3Answer2.Visible = true; lblB3.Visible = true;
                                    rbQues3Answer2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                    if (Answer == "2")
                                        rbQues3Answer2.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                {
                                    if (optindex != 3)
                                        lblC3.Text = GetAnswerOptionOrder(optindex);
                                    imgQues3Answer3.Visible = false; rbQues3Answer3.Visible = true; lblC3.Visible = true;
                                    rbQues3Answer3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                    if (Answer == "3")
                                        rbQues3Answer3.Checked = true;
                                    optindex++;
                                }

                                if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                {
                                    if (optindex != 4)
                                        lblD3.Text = GetAnswerOptionOrder(optindex);
                                    imgQues3Answer4.Visible = false; rbQues3Answer4.Visible = true; lblD3.Visible = true;
                                    rbQues3Answer4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                    if (Answer == "4")
                                        rbQues3Answer4.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                {
                                    if (optindex != 5)
                                        lblE3.Text = GetAnswerOptionOrder(optindex);
                                    imgQues3Answer5.Visible = false; rbQues3Answer5.Visible = true; lblE3.Visible = true;
                                    rbQues3Answer5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                    if (Answer == "5")
                                        rbQues3Answer5.Checked = true;
                                }


                                if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                                {
                                    if (optindex != 6)
                                        lblF3.Text = GetAnswerOptionOrder(optindex);
                                    rbQues3Answer6.Visible = true; lblF3.Visible = true;
                                    rbQues3Answer6.Text = ds.Tables[0].Rows[i]["Option6"].ToString();
                                    if (Answer == "6")
                                        rbQues3Answer6.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                                {
                                    if (optindex != 7)
                                        lblG3.Text = GetAnswerOptionOrder(optindex);
                                    rbQues3Answer7.Visible = true; lblG3.Visible = true;
                                    rbQues3Answer7.Text = ds.Tables[0].Rows[i]["Option7"].ToString();
                                    if (Answer == "7")
                                        rbQues3Answer7.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                                {
                                    if (optindex != 8)
                                        lblH3.Text = GetAnswerOptionOrder(optindex);
                                    rbQues3Answer8.Visible = true; lblH3.Visible = true;
                                    rbQues3Answer8.Text = ds.Tables[0].Rows[i]["Option8"].ToString();
                                    if (Answer == "8")
                                        rbQues3Answer8.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                                {
                                    if (optindex != 9)
                                        lblI3.Text = GetAnswerOptionOrder(optindex);
                                    rbQues3Answer9.Visible = true; lblI3.Visible = true;
                                    rbQues3Answer9.Text = ds.Tables[0].Rows[i]["Option9"].ToString();
                                    if (Answer == "9")
                                        rbQues3Answer9.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                                {
                                    if (optindex != 10)
                                        lblJ3.Text = GetAnswerOptionOrder(optindex);
                                    rbQues3Answer10.Visible = true; lblJ3.Visible = true;
                                    rbQues3Answer10.Text = ds.Tables[0].Rows[i]["Option10"].ToString();
                                    if (Answer == "10")
                                        rbQues3Answer10.Checked = true;
                                }

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
                                        where Ques.UserCode == usercode && Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques4.Count() > 0)
                                if (Ques4.First().Answer != null)
                                    Answer = Ques4.First().Answer.ToString();

                            if (ds.Tables[0].Rows[i]["QuestionFileName"].ToString() != "")
                            {
                                imgQuestion4.Visible = true;
                                imgQuestion4.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileName"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString() != "")
                            {
                                imgQuestion4Sub1.Visible = true;
                                imgQuestion4Sub1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["Option1FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option2FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option3FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option4FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option5FileName"].ToString() != "")
                            {
                                if (ds.Tables[0].Rows[i]["Option1FileName"].ToString() != "")
                                {
                                    imgQues4Answer1.Visible = true; rbQues4Answer1.Visible = true; lblA4.Visible = true;
                                    imgQues4Answer1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option1FileName"].ToString();
                                    // radioQues1.Items.Add(ds.Tables[0].Rows[i]["Option2"].ToString());
                                    if (Answer == "1")
                                        rbQues4Answer1.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option2FileName"].ToString() != "")
                                {
                                    if (optindex != 2)
                                        lblB4.Text = GetAnswerOptionOrder(optindex);
                                    imgQues4Answer2.Visible = true; rbQues4Answer2.Visible = true; lblB4.Visible = true;
                                    imgQues4Answer2.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option2FileName"].ToString();
                                    if (Answer == "2")
                                        rbQues4Answer2.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option3FileName"].ToString() != "")
                                {
                                    if (optindex != 3)
                                        lblC4.Text = GetAnswerOptionOrder(optindex);
                                    imgQues4Answer3.Visible = true; rbQues4Answer3.Visible = true; lblC4.Visible = true;
                                    imgQues4Answer3.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option3FileName"].ToString();
                                    if (Answer == "3")
                                        rbQues4Answer3.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option4FileName"].ToString() != "")
                                {
                                    if (optindex != 4)
                                        lblD4.Text = GetAnswerOptionOrder(optindex);
                                    imgQues4Answer4.Visible = true; rbQues4Answer4.Visible = true; lblD4.Visible = true;
                                    imgQues4Answer4.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option4FileName"].ToString();
                                    if (Answer == "4")
                                        rbQues4Answer4.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option5FileName"].ToString() != "")
                                {
                                    if (optindex != 5)
                                        lblE4.Text = GetAnswerOptionOrder(optindex);
                                    imgQues4Answer5.Visible = true; rbQues4Answer5.Visible = true; lblE4.Visible = true;
                                    imgQues4Answer5.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option5FileName"].ToString();
                                    if (Answer == "5")
                                        rbQues4Answer5.Checked = true;
                                }
                                //Session["EdQuestionID"] = ds.Tables[0].Rows[i]["QuestionID"];
                            }
                            else
                            {
                                if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                {
                                    imgQues4Answer1.Visible = false; rbQues4Answer1.Visible = true; lblA4.Visible = true;
                                    rbQues4Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                    // radioQues1.Items.Add(ds.Tables[0].Rows[i]["Option2"].ToString());
                                    if (Answer == "1")
                                        rbQues4Answer1.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                {
                                    if (optindex != 2)
                                        lblB4.Text = GetAnswerOptionOrder(optindex);
                                    imgQues4Answer2.Visible = false; rbQues4Answer2.Visible = true; lblB4.Visible = true;
                                    rbQues4Answer2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                    if (Answer == "2")
                                        rbQues4Answer2.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                {
                                    if (optindex != 3)
                                        lblC4.Text = GetAnswerOptionOrder(optindex);
                                    imgQues4Answer3.Visible = false; rbQues4Answer3.Visible = true; lblC4.Visible = true;
                                    rbQues4Answer3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                    if (Answer == "3")
                                        rbQues4Answer3.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                {
                                    if (optindex != 4)
                                        lblD4.Text = GetAnswerOptionOrder(optindex);
                                    imgQues4Answer4.Visible = false; rbQues4Answer4.Visible = true; lblD4.Visible = true;
                                    rbQues4Answer4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                    if (Answer == "4")
                                        rbQues4Answer4.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                {
                                    if (optindex != 5)
                                        lblE4.Text = GetAnswerOptionOrder(optindex);
                                    imgQues4Answer5.Visible = false; rbQues4Answer5.Visible = true; lblE4.Visible = true;
                                    rbQues4Answer5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                    if (Answer == "5")
                                        rbQues4Answer5.Checked = true;
                                }


                                if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                                {
                                    if (optindex != 6)
                                        lblF4.Text = GetAnswerOptionOrder(optindex);
                                    rbQues4Answer6.Visible = true; lblF4.Visible = true;
                                    rbQues4Answer6.Text = ds.Tables[0].Rows[i]["Option6"].ToString();
                                    if (Answer == "6")
                                        rbQues4Answer6.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                                {
                                    if (optindex != 7)
                                        lblG4.Text = GetAnswerOptionOrder(optindex);
                                    rbQues4Answer7.Visible = true; lblG4.Visible = true;
                                    rbQues4Answer7.Text = ds.Tables[0].Rows[i]["Option7"].ToString();
                                    if (Answer == "7")
                                        rbQues4Answer7.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                                {
                                    if (optindex != 8)
                                        lblH4.Text = GetAnswerOptionOrder(optindex);
                                    rbQues4Answer8.Visible = true; lblH4.Visible = true;
                                    rbQues4Answer8.Text = ds.Tables[0].Rows[i]["Option8"].ToString();
                                    if (Answer == "8")
                                        rbQues4Answer8.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                                {
                                    if (optindex != 9)
                                        lblI4.Text = GetAnswerOptionOrder(optindex);
                                    rbQues4Answer9.Visible = true; lblI4.Visible = true;
                                    rbQues4Answer9.Text = ds.Tables[0].Rows[i]["Option9"].ToString();
                                    if (Answer == "9")
                                        rbQues4Answer9.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                                {
                                    if (optindex != 10)
                                        lblJ4.Text = GetAnswerOptionOrder(optindex);
                                    rbQues4Answer10.Visible = true; lblJ4.Visible = true;
                                    rbQues4Answer10.Text = ds.Tables[0].Rows[i]["Option10"].ToString();
                                    if (Answer == "10")
                                        rbQues4Answer10.Checked = true;
                                }
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
                                        where Ques.UserCode == usercode && Ques.UserId == userid && Ques.Question == ds.Tables[0].Rows[i]["Question"].ToString() && Ques.QuestionID == int.Parse(ds.Tables[0].Rows[i]["QuestionID"].ToString()) &&
                                        Ques.TestId == testId && Ques.TestSectionId == testsectionid
                                        select Ques;
                            if (Ques5.Count() > 0)
                                if (Ques5.First().Answer != null)
                                    Answer = Ques5.First().Answer.ToString();

                            if (ds.Tables[0].Rows[i]["QuestionFileName"].ToString() != "")
                            {
                                imgQuestion5.Visible = true;
                                imgQuestion5.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileName"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString() != "")
                            {
                                imgQuestion5Sub1.Visible = true;
                                imgQuestion5Sub1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["QuestionFileNameSub1"].ToString();
                            }
                            if (ds.Tables[0].Rows[i]["Option1FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option2FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option3FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option4FileName"].ToString() != "" || ds.Tables[0].Rows[i]["Option5FileName"].ToString() != "")
                            {

                                if (ds.Tables[0].Rows[i]["Option1FileName"].ToString() != "")
                                {
                                    imgQues5Answer1.Visible = true; rbQues5Answer1.Visible = true; lblA5.Visible = true;
                                    imgQues5Answer1.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option1FileName"].ToString();
                                    if (Answer == "1")
                                        rbQues5Answer1.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option2FileName"].ToString() != "")
                                {
                                    if (optindex != 2)
                                        lblB5.Text = GetAnswerOptionOrder(optindex);
                                    imgQues5Answer2.Visible = true; rbQues5Answer2.Visible = true; lblB5.Visible = true;
                                    imgQues5Answer2.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option2FileName"].ToString();
                                    if (Answer == "2")
                                        rbQues5Answer2.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option3FileName"].ToString() != "")
                                {
                                    if (optindex != 3)
                                        lblC5.Text = GetAnswerOptionOrder(optindex);
                                    imgQues5Answer3.Visible = true; rbQues5Answer3.Visible = true; lblC5.Visible = true;
                                    imgQues5Answer3.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option3FileName"].ToString();
                                    if (Answer == "3")
                                        rbQues5Answer3.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option4FileName"].ToString() != "")
                                {
                                    if (optindex != 4)
                                        lblD5.Text = GetAnswerOptionOrder(optindex);
                                    imgQues5Answer4.Visible = true; rbQues5Answer4.Visible = true; lblD5.Visible = true;
                                    imgQues5Answer4.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option4FileName"].ToString();
                                    if (Answer == "4")
                                        rbQues5Answer4.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option5FileName"].ToString() != "")
                                {
                                    if (optindex != 5)
                                        lblE5.Text = GetAnswerOptionOrder(optindex);
                                    imgQues5Answer5.Visible = true; rbQues5Answer5.Visible = true; lblE5.Visible = true;
                                    imgQues5Answer5.ImageUrl = "QuestionAnswerFiles/" + ds.Tables[0].Rows[i]["Option5FileName"].ToString();
                                    if (Answer == "5")
                                        rbQues5Answer5.Checked = true;
                                }
                            }
                            else
                            {
                                if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                {
                                    imgQues5Answer1.Visible = false; rbQues5Answer1.Visible = true; lblA5.Visible = true;
                                    rbQues5Answer1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                    if (Answer == "1")
                                        rbQues5Answer1.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                {
                                    if (optindex != 2)
                                        lblB5.Text = GetAnswerOptionOrder(optindex);
                                    imgQues5Answer2.Visible = false; rbQues5Answer2.Visible = true; lblB5.Visible = true;
                                    rbQues5Answer2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                    if (Answer == "2")
                                        rbQues5Answer2.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                {
                                    if (optindex != 3)
                                        lblC5.Text = GetAnswerOptionOrder(optindex);
                                    imgQues5Answer3.Visible = false; rbQues5Answer3.Visible = true; lblC5.Visible = true;
                                    rbQues5Answer3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                    if (Answer == "3")
                                        rbQues5Answer3.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                {
                                    if (optindex != 4)
                                        lblD5.Text = GetAnswerOptionOrder(optindex);
                                    imgQues5Answer4.Visible = false; rbQues5Answer4.Visible = true; lblD5.Visible = true;
                                    rbQues5Answer4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                    if (Answer == "4")
                                        rbQues5Answer4.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                {
                                    if (optindex != 5)
                                        lblE5.Text = GetAnswerOptionOrder(optindex);
                                    imgQues5Answer5.Visible = false; rbQues5Answer5.Visible = true; lblE5.Visible = true;
                                    rbQues5Answer5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                    if (Answer == "5")
                                        rbQues5Answer5.Checked = true;
                                }


                                if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                                {
                                    if (optindex != 6)
                                        lblF5.Text = GetAnswerOptionOrder(optindex);
                                    rbQues5Answer6.Visible = true; lblF5.Visible = true;
                                    rbQues5Answer6.Text = ds.Tables[0].Rows[i]["Option6"].ToString();
                                    if (Answer == "6")
                                        rbQues5Answer6.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option7"].ToString() != "")
                                {
                                    if (optindex != 7)
                                        lblG5.Text = GetAnswerOptionOrder(optindex);
                                    rbQues5Answer7.Visible = true; lblG5.Visible = true;
                                    rbQues5Answer7.Text = ds.Tables[0].Rows[i]["Option7"].ToString();
                                    if (Answer == "7")
                                        rbQues5Answer7.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option8"].ToString() != "")
                                {
                                    if (optindex != 8)
                                        lblH5.Text = GetAnswerOptionOrder(optindex);
                                    rbQues5Answer8.Visible = true; lblH5.Visible = true;
                                    rbQues5Answer8.Text = ds.Tables[0].Rows[i]["Option8"].ToString();
                                    if (Answer == "8")
                                        rbQues5Answer8.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option9"].ToString() != "")
                                {
                                    if (optindex != 9)
                                        lblI5.Text = GetAnswerOptionOrder(optindex);
                                    rbQues5Answer9.Visible = true; lblI5.Visible = true;
                                    rbQues5Answer9.Text = ds.Tables[0].Rows[i]["Option9"].ToString();
                                    if (Answer == "9")
                                        rbQues5Answer9.Checked = true;
                                    optindex++;
                                }
                                if (ds.Tables[0].Rows[i]["Option10"].ToString() != "")
                                {
                                    if (optindex != 10)
                                        lblJ5.Text = GetAnswerOptionOrder(optindex);
                                    rbQues5Answer10.Visible = true; lblJ5.Visible = true;
                                    rbQues5Answer10.Text = ds.Tables[0].Rows[i]["Option10"].ToString();
                                    if (Answer == "10")
                                        rbQues5Answer10.Checked = true;
                                }
                            }
                            curntquescnt++;
                            j++;
                            pnlQuestion5.Visible = true;
                            break;
                    }
                }
            }
            else { GoToNextPage(); }
            Session["curntques"] = curntquescnt;
        //}
        //catch (Exception ex)
        //{
        //    FillQuestion();//bipson 14082010 to avoid arithmetic error... lblmessage.Text = ex.Message;
        //    //conn.Close();
        //}
    }
    private void GoToNextPage()
    {
        ClearAllPageCountValues(1);
        Session["evaldirection"] = "Next";
        Session["SubCtrl"] = "PhotoTypeQuestions.ascx";// "WordTypeMemmoryQuestions.ascx";
        //Session["SubCtrl"] = "VideoQuestions.ascx";
        Response.Redirect("FJAHome.aspx");
    }

    private void SaveAnswer()
    {
        string quescategory = "ImageType";
        int testsectionid = 0;
        if (Session["CurrentTestSectionId"] != null)
            testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());

        string answer = "";

        if (lblQuesID1.Text.Trim() != "")
        {
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

            dataclass.Procedure_QuesAnswers(qusid1, usercode, tcellQues1.InnerHtml, answer, userid, testId, testsectionid, quescategory);
        }
        answer = "";

        if (lblQuesID2.Text.Trim() != "")
        {
            if (rbQues2Answer1.Checked == true)
                answer = "1";
            else if (rbQues2Answer2.Checked == true)
                answer = "2";
            else if (rbQues2Answer3.Checked == true)
                answer = "3";
            else if (rbQues2Answer4.Checked == true)
                answer = "4";
            else if (rbQues2Answer5.Checked == true)
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

            dataclass.Procedure_QuesAnswers(qusid2, usercode, tcellQues2.InnerHtml, answer, userid, testId, testsectionid, quescategory);
        }
        answer = "";

        if (lblQuesID3.Text.Trim() != "")
        {
            if (rbQues3Answer1.Checked == true)
                answer = "1";
            else if (rbQues3Answer2.Checked == true)
                answer = "2";
            else if (rbQues3Answer3.Checked == true)
                answer = "3";
            else if (rbQues3Answer4.Checked == true)
                answer = "4";
            else if (rbQues3Answer5.Checked == true)
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

            dataclass.Procedure_QuesAnswers(qusid3, usercode, tcellQues3.InnerHtml, answer, userid, testId, testsectionid, quescategory);
        }
        answer = "";

        if (lblQuesID4.Text.Trim() != "")
        {
            if (rbQues4Answer1.Checked == true)
                answer = "1";
            else if (rbQues4Answer2.Checked == true)
                answer = "2";
            else if (rbQues4Answer3.Checked == true)
                answer = "3";
            else if (rbQues4Answer4.Checked == true)
                answer = "4";
            else if (rbQues4Answer5.Checked == true)
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

            dataclass.Procedure_QuesAnswers(qusid4, usercode, tcellQues4.InnerHtml, answer, userid, testId, testsectionid, quescategory);
        }
        answer = "";

        if (lblQuesID5.Text.Trim() != "")
        {
            if (rbQues5Answer1.Checked == true)
                answer = "1";
            else if (rbQues5Answer2.Checked == true)
                answer = "2";
            else if (rbQues5Answer3.Checked == true)
                answer = "3";
            else if (rbQues5Answer4.Checked == true)
                answer = "4";
            else if (rbQues5Answer5.Checked == true)
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

            dataclass.Procedure_QuesAnswers(qusid5, usercode, tcellQues5.InnerHtml, answer, userid, testId, testsectionid, quescategory);
        }
        answer = "";

    }

    private void ClearControls()
    {
        rbQues1Answer1.Checked = false; rbQues1Answer2.Checked = false; rbQues1Answer3.Checked = false; rbQues1Answer4.Checked = false; rbQues1Answer5.Checked = false;
        rbQues2Answer1.Checked = false; rbQues2Answer2.Checked = false; rbQues2Answer3.Checked = false; rbQues2Answer4.Checked = false; rbQues2Answer5.Checked = false;
        rbQues3Answer1.Checked = false; rbQues3Answer2.Checked = false; rbQues3Answer3.Checked = false; rbQues3Answer4.Checked = false; rbQues3Answer5.Checked = false;
        rbQues4Answer1.Checked = false; rbQues4Answer2.Checked = false; rbQues4Answer3.Checked = false; rbQues4Answer4.Checked = false; rbQues4Answer5.Checked = false;
        rbQues5Answer1.Checked = false; rbQues5Answer2.Checked = false; rbQues5Answer3.Checked = false; rbQues5Answer4.Checked = false; rbQues5Answer5.Checked = false;
        rbQues1Answer6.Checked = false; rbQues1Answer7.Checked = false; rbQues1Answer8.Checked = false; rbQues1Answer9.Checked = false; rbQues1Answer10.Checked = false;
        rbQues2Answer6.Checked = false; rbQues2Answer7.Checked = false; rbQues2Answer8.Checked = false; rbQues2Answer9.Checked = false; rbQues2Answer10.Checked = false;
        rbQues3Answer6.Checked = false; rbQues3Answer7.Checked = false; rbQues3Answer8.Checked = false; rbQues3Answer9.Checked = false; rbQues3Answer10.Checked = false;
        rbQues4Answer6.Checked = false; rbQues4Answer7.Checked = false; rbQues4Answer8.Checked = false; rbQues4Answer9.Checked = false; rbQues4Answer10.Checked = false;
        rbQues5Answer6.Checked = false; rbQues5Answer7.Checked = false; rbQues5Answer8.Checked = false; rbQues5Answer9.Checked = false; rbQues5Answer10.Checked = false;        

        rbQues1Answer1.Visible = false; rbQues1Answer2.Visible = false; rbQues1Answer3.Visible = false; rbQues1Answer4.Visible = false; rbQues1Answer5.Visible = false;
        rbQues2Answer1.Visible = false; rbQues2Answer2.Visible = false; rbQues2Answer3.Visible = false; rbQues2Answer4.Visible = false; rbQues2Answer5.Visible = false;
        rbQues3Answer1.Visible = false; rbQues3Answer2.Visible = false; rbQues3Answer3.Visible = false; rbQues3Answer4.Visible = false; rbQues3Answer5.Visible = false;
        rbQues4Answer1.Visible = false; rbQues4Answer2.Visible = false; rbQues4Answer3.Visible = false; rbQues4Answer4.Visible = false; rbQues4Answer5.Visible = false;
        rbQues5Answer1.Visible = false; rbQues5Answer2.Visible = false; rbQues5Answer3.Visible = false; rbQues5Answer4.Visible = false; rbQues5Answer5.Visible = false;
        rbQues1Answer6.Visible = false; rbQues1Answer7.Visible = false; rbQues1Answer8.Visible = false; rbQues1Answer9.Visible = false; rbQues1Answer10.Visible = false;
        rbQues2Answer6.Visible = false; rbQues2Answer7.Visible = false; rbQues2Answer8.Visible = false; rbQues2Answer9.Visible = false; rbQues2Answer10.Visible = false;
        rbQues3Answer6.Visible = false; rbQues3Answer7.Visible = false; rbQues3Answer8.Visible = false; rbQues3Answer9.Visible = false; rbQues3Answer10.Visible = false;
        rbQues4Answer6.Visible = false; rbQues4Answer7.Visible = false; rbQues4Answer8.Visible = false; rbQues4Answer9.Visible = false; rbQues4Answer10.Visible = false;
        rbQues5Answer6.Visible = false; rbQues5Answer7.Visible = false; rbQues5Answer8.Visible = false; rbQues5Answer9.Visible = false; rbQues5Answer10.Visible = false;
        
        lblA1.Visible = false; lblB1.Visible = false; lblC1.Visible = false; lblD1.Visible = false; lblE1.Visible = false;
        lblA2.Visible = false; lblB2.Visible = false; lblC2.Visible = false; lblD2.Visible = false; lblE2.Visible = false;
        lblA3.Visible = false; lblB3.Visible = false; lblC3.Visible = false; lblD3.Visible = false; lblE3.Visible = false;
        lblA4.Visible = false; lblB4.Visible = false; lblC4.Visible = false; lblD4.Visible = false; lblE4.Visible = false;
        lblA5.Visible = false; lblB5.Visible = false; lblC5.Visible = false; lblD5.Visible = false; lblE5.Visible = false;        


        lblNo1.Text = ""; lblNo2.Text = ""; lblNo3.Text = ""; lblNo4.Text = ""; lblNo5.Text = "";
        lblQues1.Text = ""; lblQues2.Text = ""; lblQues3.Text = ""; lblQues4.Text = ""; lblQues5.Text = "";
        tcellQues1.InnerHtml = ""; tcellQues2.InnerHtml = ""; tcellQues3.InnerHtml = ""; tcellQues4.InnerHtml = ""; tcellQues5.InnerHtml = "";

        imgQuestion1.ImageUrl = ""; imgQuestion1Sub1.ImageUrl = ""; imgQues1Answer1.ImageUrl = ""; imgQues1Answer2.ImageUrl = ""; imgQues1Answer3.ImageUrl = ""; imgQues1Answer4.ImageUrl = ""; imgQues1Answer5.ImageUrl = "";
        imgQuestion2.ImageUrl = ""; imgQuestion2Sub1.ImageUrl = ""; imgQues2Answer1.ImageUrl = ""; imgQues2Answer2.ImageUrl = ""; imgQues2Answer3.ImageUrl = ""; imgQues2Answer4.ImageUrl = ""; imgQues2Answer5.ImageUrl = "";
        imgQuestion3.ImageUrl = ""; imgQuestion3Sub1.ImageUrl = ""; imgQues3Answer1.ImageUrl = ""; imgQues3Answer2.ImageUrl = ""; imgQues3Answer3.ImageUrl = ""; imgQues3Answer4.ImageUrl = ""; imgQues3Answer5.ImageUrl = "";
        imgQuestion4.ImageUrl = ""; imgQuestion4Sub1.ImageUrl = ""; imgQues4Answer1.ImageUrl = ""; imgQues4Answer2.ImageUrl = ""; imgQues4Answer3.ImageUrl = ""; imgQues4Answer4.ImageUrl = ""; imgQues4Answer5.ImageUrl = "";
        imgQuestion5.ImageUrl = ""; imgQuestion5Sub1.ImageUrl = ""; imgQues5Answer1.ImageUrl = ""; imgQues5Answer2.ImageUrl = ""; imgQues5Answer3.ImageUrl = ""; imgQues5Answer4.ImageUrl = ""; imgQues5Answer5.ImageUrl = "";

        imgQuestion1.Visible = false; imgQuestion1Sub1.Visible = false; imgQues1Answer1.Visible = false; imgQues1Answer2.Visible = false; imgQues1Answer3.Visible = false; imgQues1Answer4.Visible = false; imgQues1Answer5.Visible = false;
        imgQuestion2.Visible = false; imgQuestion2Sub1.Visible = false; imgQues2Answer1.Visible = false; imgQues2Answer2.Visible = false; imgQues2Answer3.Visible = false; imgQues2Answer4.Visible = false; imgQues2Answer5.Visible = false;
        imgQuestion3.Visible = false; imgQuestion3Sub1.Visible = false; imgQues3Answer1.Visible = false; imgQues3Answer2.Visible = false; imgQues3Answer3.Visible = false; imgQues3Answer4.Visible = false; imgQues3Answer5.Visible = false;
        imgQuestion4.Visible = false; imgQuestion4Sub1.Visible = false; imgQues4Answer1.Visible = false; imgQues4Answer2.Visible = false; imgQues4Answer3.Visible = false; imgQues4Answer4.Visible = false; imgQues4Answer5.Visible = false;
        imgQuestion5.Visible = false; imgQuestion5Sub1.Visible = false; imgQues5Answer1.Visible = false; imgQues5Answer2.Visible = false; imgQues5Answer3.Visible = false; imgQues5Answer4.Visible = false; imgQues5Answer5.Visible = false;
        
    }
   
    private void ClearAllPageCountValues(int index)
    {
        if (index == 0)
            Session["pagecount_img"] = null;
        Session["pagecount_memWords"] = null;
        Session["pagecount_audio"] = null;
        Session["pagecount_memImages"] = null;
        Session["pagecount_imgPhoto"] = null;
        Session["pagecountRating"] = null;
        Session["pagecount_video"] = null;
    }
    protected void btnYes_Click(object sender, EventArgs e)
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
                        //return true;

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
                if (tsNow > tsDuration)
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

   
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Timer1.Enabled = true;
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        
        if (Session["timeExpired"] != null)
            if (Session["timeExpired"].ToString() == "True")
                return;            

        CheckTime();

    }
}
