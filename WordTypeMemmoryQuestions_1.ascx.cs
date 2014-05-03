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

public partial class WordTypeMemmoryQuestions : System.Web.UI.UserControl
{
    protected System.Timers.Timer timer11; 
    private int test;
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();

    int userid = 0; string usercode = "";
    DBManagementClass clsclass = new DBManagementClass();
   
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
    int quesperPage = 1;//bip 121009

    int testId = 0; int testsectionid = 0;

    string wordpadding = "10px;";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserTestId"] != null)
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
        Session["SubCtrl"] = "ImageTypeMemmoryQuestions.ascx";
        Response.Redirect("FJAHome.aspx");
    }

    private void GoToPreviousPage()
    {
        //ClearSessionValues();
        Session["evaldirection"] = "Previous";
        Session["SubCtrl"] = "PhotoTypeQuestions.ascx";// "ImageQuestions.ascx";
        Response.Redirect("FJAHome.aspx");
    }

    private void FillMemWordQuestionInstructions(int dispalytype)
    {
        bool valueexists = false;
        var InstructionDetails = from instructiondet in dataclass.InstructionByDisplayTypes
                                 where instructiondet.CategoryId == 7 && instructiondet.DisplayTypeId==dispalytype
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
       // else FillTitle();
    }

    private void FillTitle()
    {
        var InstructionDetails = from instructiondet in dataclass.TestInstructions
                                 where instructiondet.CategoryId == 7
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
                                 where instructiondet.CategoryId == 8
                                 select instructiondet;
        if (InstructionDetails.Count() > 0)
        {
            if (InstructionDetails.First().InstructionDetails != null)
                divInstructions.InnerHtml = InstructionDetails.First().InstructionDetails.ToString();
        }
    }

    protected void ptnPrevious_Click(object sender, EventArgs e)
    {
        Session["displaystatus"] = null;
        Session["test"] = null;

        int curcount = 0;
        int totalcount = 0, totalquesavail = 0;
        if (Session["totalQuesCount_memWords"] != null)
            totalcount = int.Parse(Session["totalQuesCount_memWords"].ToString());
        if (Session["totalQuesAvailable_memWords"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable_memWords"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecount_memWords"] != null)
            pagecount = int.Parse(Session["pagecount_memWords"].ToString());

        Session["MemWordPrevious"] = "True";

        int curindex = 0;
        curindex = (pagecount - 1) * quesperPage;
        if (curindex >= 0)
        {
            pagecount--;
            Session["pagecount_memWords"] = pagecount; SetCurrentPageCount();// 230110 bip
        }
        else
        {
            GoToPreviousPage();
        }
        FillQuestion();
    }

    private void SaveAnswer()
    {
        string quescategory = "MemTestWords";
        int testsectionid = 0;
        if (Session["CurrentTestSectionId"] != null)
            testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());

        string answer = "";
        if (rbOption1.Checked == true)
            answer = rbOption1.Text;
        else if (rbOption2.Checked == true)
            answer = rbOption2.Text;
        else if (rbOption3.Checked == true)
            answer = rbOption3.Text;
        else if (rbOption4.Checked == true)
            answer = rbOption4.Text;
        else if (rbOption5.Checked == true)
            answer = rbOption5.Text;

        qusid1 = 0;
        if (lblQuesID1.Text.Trim() != "")
        {
            qusid1 = int.Parse(lblQuesID1.Text.Trim());
            dataclass.Procedure_QuesAnswers(qusid1, "", tcellQues1.InnerHtml, answer, userid, testId, testsectionid,quescategory);
        }

       // ClearControls();
    }

    private void ClearControls()
    {
        rbOption1.Checked = false; rbOption1.Visible = false; lblA.Visible = false;
        rbOption2.Checked = false; rbOption2.Visible = false; lblB.Visible = false;
        rbOption3.Checked = false; rbOption3.Visible = false; lblC.Visible = false;
        rbOption4.Checked = false; rbOption4.Visible = false; lblD.Visible = false;
        rbOption5.Checked = false; rbOption5.Visible = false; lblE.Visible = false;
        tcellQues1.InnerHtml = ""; lblNo1.Text = ""; lblQuesID1.Text = "";
    }

    private Boolean CheckTime()//bip 10052010 
    {
        Boolean timeExpired = false;//bip 10052010 
        if (CheckTestTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for your test is completed, <br> If you are interested to take this test again please contact our site admin";
            pnlpopup_timer.Visible = true; btnYes_timer_Test.Visible = true; UpdateTimer.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";//bip 10052010
            //return;//Timer1.Enabled = false; return;// 
        }
        else if (CheckTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for this Test Section is completed, <br> Do you want to continue with the remaining sections under your Test?";
            pnlpopup_timer.Visible = true; btnYes_timer.Visible = true; btnNo_timer.Visible = true; UpdateTimer.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";//bip 10052010
            //return;// Timer1.Enabled = false; return;//
        }
        else if (CheckTestSecVarTimeValidity() == true)
        {
            lblpopup_timer.Text = "The time alloted for this Variable under selected Section is completed, <br> Do you want to continue with the remaining Variables under your Test Section?";
            pnlpopup_timer.Visible = true; btnYes_timer_TestVariable.Visible = true; btnNo_timer.Visible = true; UpdateTimer.Enabled = false;
            timeExpired = true; pnlMain.Visible = false; Session["timeExpired"] = "True";//bip 10052010
            //return;//Timer1.Enabled = false; return;// 
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
        if (timeExpired == false)
        {
            if (CheckAnswer() == false)
            {
                //UpdateTimer.Enabled = false; // bip 07052010
                Timer1.Enabled = false; // 04-03-2010 bip
                pnlpopup.Visible = true;
                return;
            }
            SaveValues();
        }
        //else SaveAnswer();

        
    }
    private Boolean CheckAnswer()
    {
        Boolean answercompleted = true;

        string answer = "";

        if (rbOption1.Checked == true)
            answer = rbOption1.Text;
        else if (rbOption2.Checked == true)
            answer = rbOption2.Text;
        else if (rbOption3.Checked == true)
            answer = rbOption3.Text;
        else if (rbOption4.Checked == true)
            answer = rbOption4.Text;
        else if (rbOption5.Checked == true)
            answer = rbOption5.Text;

        if (answer == "")
        {
            lblpopup.Text += "You have missed question No:" + lblNo1.Text.Trim() + " Please, complete all questions before moving to the next page. <br> Do you want to go to next page? ";
            answercompleted = false;
        }
        return answercompleted;
    }
    private void SaveValues()
    {
        SaveAnswer();

        Session["displaystatus"] = null;
        Session["test"] = null;

        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());

        int curcount = 0;
        int totalcount = 0, totalquesavail = 0;
        if (Session["totalQuesCount_memWords"] != null)
            totalcount = int.Parse(Session["totalQuesCount_memWords"].ToString());
        if (Session["totalQuesAvailable_memWords"] != null)
            totalquesavail = int.Parse(Session["totalQuesAvailable_memWords"].ToString());
        if (totalcount == totalquesavail)
            curcount = totalcount;
        if (totalcount > totalquesavail)
            curcount = totalquesavail;
        if (Session["pagecount_memWords"] != null)
            pagecount = int.Parse(Session["pagecount_memWords"].ToString());
        int curindex = 0;
        curindex = (pagecount + 1) * quesperPage;
        if (curindex < curcount)
        {
            pagecount++;
            Session["pagecount_memWords"] = pagecount.ToString();
            SetCurrentPageCount();// 230110 bip
            FillQuestion();
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
        int questionid = 0; string questiontype = "MemTestWords";
        dataclass.Procedure_UserTestPageIndex_Temp(userid, testId, testsectionid, testFirstVariableId, testSecondVariableId, pagecount, questiontype);
    }
    private void ClearSessionValues()
    {
       // Session["pagecount_memWords"] = null;
    }
    private void ClearAllPageCountValues(int index)
    {
        if (index == 0)
            Session["pagecount_memWords"] = null;
        Session["pagecount_audio"] = null;
        Session["pagecount_memImages"] = null;        
        Session["pagecountRating"] = null;
        Session["pagecount_video"] = null;
    }
    private void timer11_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        lblQues1.Text = "count:" + test;
        if (test >= 10)
        { timer11.Stop(); timer11.Enabled = false; }
    }

    private string GetDisplayWord()
    {
        string displayword = "";
        if (Session["test"] != null)
            test = int.Parse(Session["test"].ToString());
      
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Session["pagecount_memWords"] != null)
                    pagecount = int.Parse(Session["pagecount_memWords"].ToString());

                int slno = 0;
                slno = pagecount * quesperPage + 1;
                if (slno < 0)
                    slno = 0;
                if (slno > ds.Tables[0].Rows.Count)//
                { //Session["displaystatus"] = "True"; //bip 07052010
                    Session["test"] = null; return ""; }

                double interval = 0; int displaytype = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[slno - 1]["DisplayDuration"].ToString() != "")
                        interval = double.Parse(ds.Tables[0].Rows[slno - 1]["DisplayDuration"].ToString());

                    if (ds.Tables[0].Rows[slno - 1]["DisplayType"].ToString() != "")
                        displaytype = int.Parse(ds.Tables[0].Rows[slno - 1]["DisplayType"].ToString());
                }

                if (displaytype == 1)//static
                {
                    displayword = GetMemWordCollection(slno - 1);                   
                    //tcelWordDisplay.Style.Add("text-align", "left");                   
                }
                else
                {
                    tcelWordDisplay.Style.Add("text-align", "center"); 
                    if (displaytype == 2)//sequesnce
                    {
                        if (test > 20) 
                        { 
                            return ""; }
                        displayword = ds.Tables[0].Rows[slno - 1][test + 2].ToString();
                        if (displayword == "")
                        { test = test + 1; Session["test"] = test.ToString(); GetDisplayWord(); }
                        //displayword = "<table style='font-size:22px'><tr><td style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[slno - 1][test + 2].ToString() + "</td></tr></table>";
                        displayword = "<table ><tr><td style='text-align:center;font-size:26px'>" + ds.Tables[0].Rows[slno - 1][test + 2].ToString() + "</td></tr></table>";
                        
                    }
                    else//passage
                    {
                        if (test > 1) 
                        { 
                            return ""; }
                        displayword = "<table style='width:100%;'><tr><td style='font-size:16px;font-weight:normal;text-align: justify; padding: " + wordpadding + ";line-height: 25px'>" + ds.Tables[0].Rows[slno - 1][test + 2].ToString() + "</td></tr></table>";
                        //displayword = ds.Tables[0].Rows[slno - 1][test + 2].ToString();
                                               
                    }
                }
            }
        }
        return displayword;
    }

    private string GetMemWordCollection(int rowindex)
    {
        string strBreak = "<BR>";//<BR><BR>";
        string space = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        int index = 0;
        string displayword = "";
        string wordstructure = "";//;line-height:30px;width:100%;
        wordstructure = "<table style='font-size:16px;'>";//;line-height:30px;
        if (ds.Tables[0].Rows[rowindex]["Unit1"] != null && ds.Tables[0].Rows[rowindex]["Unit1"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit1"].ToString() + "</td>"; index++;
        }

        if (ds.Tables[0].Rows[rowindex]["Unit2"] != null && ds.Tables[0].Rows[rowindex]["Unit2"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit2"].ToString() + "</td>"; index++;
        }
        if (ds.Tables[0].Rows[rowindex]["Unit3"] != null && ds.Tables[0].Rows[rowindex]["Unit3"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit3"].ToString() + "</td>"; index++;
        }
        if (ds.Tables[0].Rows[rowindex]["Unit4"] != null && ds.Tables[0].Rows[rowindex]["Unit4"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit4"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit5"] != null && ds.Tables[0].Rows[rowindex]["Unit5"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit5"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit6"] != null && ds.Tables[0].Rows[rowindex]["Unit6"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit6"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit7"] != null && ds.Tables[0].Rows[rowindex]["Unit7"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit7"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit8"] != null && ds.Tables[0].Rows[rowindex]["Unit8"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit8"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit9"] != null && ds.Tables[0].Rows[rowindex]["Unit9"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit9"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit10"] != null && ds.Tables[0].Rows[rowindex]["Unit10"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit10"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit11"] != null && ds.Tables[0].Rows[rowindex]["Unit11"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit11"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit12"] != null && ds.Tables[0].Rows[rowindex]["Unit12"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit12"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit13"] != null && ds.Tables[0].Rows[rowindex]["Unit13"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit13"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit14"] != null && ds.Tables[0].Rows[rowindex]["Unit14"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit14"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit15"] != null && ds.Tables[0].Rows[rowindex]["Unit15"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit15"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit16"] != null && ds.Tables[0].Rows[rowindex]["Unit16"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit16"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit17"] != null && ds.Tables[0].Rows[rowindex]["Unit17"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit17"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit18"] != null && ds.Tables[0].Rows[rowindex]["Unit18"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit18"].ToString() + "</td>";
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit19"] != null && ds.Tables[0].Rows[rowindex]["Unit19"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit19"].ToString() + "</td>"; index++;
            if (index >= 4) { wordstructure += "</tr>"; index = 0; }
        }
        if (ds.Tables[0].Rows[rowindex]["Unit20"] != null && ds.Tables[0].Rows[rowindex]["Unit20"].ToString() != "")
        {
            if (index == 0) wordstructure += "<tr>";
            wordstructure += "<td  style='padding: " + wordpadding + "'>" + ds.Tables[0].Rows[rowindex]["Unit20"].ToString() + "</td>"; index++;
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

    protected void UpdateTimer_Tick(object sender, EventArgs e)
    {
        /// bip 10052010
        if (Session["timeExpired"] != null)
            if (Session["timeExpired"].ToString() == "True")
                return;

        if (CheckTime() == true) {  return; }//SaveAnswer();
        ///
        
       // DateStampLabel.Text = DateTime.Now.ToString();
        bool countcompleted = false;
        if (Session["test"] != null)
            test = int.Parse(Session["test"].ToString());
        test = test + 1;
        Session["test"] = test.ToString();        

        string displayword = GetDisplayWord();
        tcelWordDisplay.InnerHtml = "";
        tcelWordDisplay.InnerHtml = displayword;
       
        if (Session["pagecount_memWords"] != null)
            pagecount = int.Parse(Session["pagecount_memWords"].ToString());

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
            pnlWordDisplay.Width = 200;
            if (test > 1)
                countcompleted = true;
        }
        else if (displaytype == 3)
        {
            //tblWordDisplay.Width = "600px";
            pnlWordDisplay.Width = 600;
            if (test > 1)
                countcompleted = true;
        }
        else
        {
            pnlWordDisplay.Width = 200;
            if (test >= 20)
                countcompleted = true;
        }

        if (countcompleted == true)
        {
            UpdateTimer.Enabled = false; Session["displaystatus"] = "True"; //bip 07052010
            Session["test"] = null;
            pnlWordDisplay.Visible = false; tcelWordDisplay.InnerHtml = ""; //WordDisplayLabel.Text = ""; 
            divInstructions.InnerHtml = ""; FillQuestion();
        }
        else pnlWordDisplay.Visible = true;
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
    private DataSet GetTempData()
    {
        DataSet dsTempData = new DataSet();
        //// 220110 ... bip
        int testSecondVariableId = 0, testFirstVariableId = 0;
        if (Session["CurrentTestSecondVariableId"] != null)
            testSecondVariableId = int.Parse(Session["CurrentTestSecondVariableId"].ToString());
        if (Session["CurrentTestFirstVariableId"] != null)
            testFirstVariableId = int.Parse(Session["CurrentTestFirstVariableId"].ToString());
        int questionid = 0; string questiontype = "MemTestWords";

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
                    
                    querystringquesTemp = "SELECT QuestionID,Question,Answer,Unit1,Unit2,Unit3,Unit4,Unit5,Unit6,Unit7,Unit8,Unit9,Unit10,Unit11,Unit12,Unit13,Unit14,Unit15,Unit16,Unit17,Unit18,Unit19,Unit20,Option1,Option2,Option3,Option4,Option5,DisplayDuration,DisplayType  FROM View_TestBaseQuestionList_memWords where " + questionids;
                    dsTempData = clsclass.GetValuesFromDB(querystringquesTemp);
                    Session["questionColl_Audio"] = dsTempData;

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
                            Session["pagecount_memWords"] = pagecount;
                            Session["displaystatus"] = "True";
                            Session["MemWordPrevious"] = "True";
                            Session["MemWordCurrentpage"] = pagecount;
                        }
                    }
                }
                else dsTempData = null;

        return dsTempData;
    }
    private void FillQuestion()
    {
        try
        {
            ClearControls();

            string querystring = "";
            //int totalQues = 0;
            int QuestionID = 0;
            
            if (Session["pagecount_memWords"] != null)
                pagecount = int.Parse(Session["pagecount_memWords"].ToString());

            string connString = ConfigurationManager.ConnectionStrings["talentscoutConnectionString"].ConnectionString;
                       
            ds = new DataSet();//int testsectionid = 0;
            if (Session["CurrentTestSectionId"] != null)
                testsectionid = int.Parse(Session["CurrentTestSectionId"].ToString());  
            if (Session["questionColl_memWords"] != null)
                ds = (DataSet)Session["questionColl_memWords"];
            else
            {
                //bip 081009
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
                    int questionid = 0; string questiontype = "MemTestWords";

                    ds = GetTempData();
                    bool newentry = false;
                    if (ds == null)
                    {
                        newentry = true;
                        ds = GetQuestionList(testsectionid);
                    }
                    else if (ds.Tables.Count <= 0)
                    { newentry = true; ds = GetQuestionList(testsectionid); }
                    ////.......
                    //
                 //   ds = GetQuestionList(testsectionid); 
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
                if (ds.Tables[0].Rows[slno - 1]["DisplayDuration"] != null)
                    interval = double.Parse(ds.Tables[0].Rows[slno - 1]["DisplayDuration"].ToString());

                if (ds.Tables[0].Rows[slno - 1]["DisplayType"] != null)
                    displaytype = int.Parse(ds.Tables[0].Rows[slno - 1]["DisplayType"].ToString());
                bool previousdisplay = false;
                int returnpage = 0;
                if (Session["MemWordPrevious"] != null)
                {
                    if (Session["pagecount_memWords"] != null)
                    {
                        returnpage = int.Parse(Session["pagecount_memWords"].ToString());
                        if (pagecount <= returnpage)
                        { previousdisplay = true; imgbtnClickHere.Visible = false; }
                    }
                }

                if (previousdisplay == false)
                {
                    if (Session["displaystatus"] != null)
                    {
                        if (Session["displaystatus"].ToString() != "True")
                        {
                            if (Session["showclicked"] != null)
                            {
                                if (Session["showclicked"].ToString() == "yes")
                                { Session["displaystatus"] = "False"; interval = (interval * 1000); UpdateTimer.Interval = int.Parse(interval.ToString()); UpdateTimer.Enabled = true; }//Timer1.Enabled = false;Timer1.Enabled = true;Timer1.Interval = int.Parse(interval.ToString()); ////bip 07052010
                                else { imgbtnClickHere.Visible = true; pnlClickHere.Visible = true; FillMemWordQuestionInstructions(displaytype); }
                            }
                            else { imgbtnClickHere.Visible = true; pnlClickHere.Visible = true; FillMemWordQuestionInstructions(displaytype); }
                        }

                    }
                    else { Session["showclicked"] = null; Session["displaystatus"] = "False"; imgbtnClickHere.Visible = true; pnlClickHere.Visible = true; FillMemWordQuestionInstructions(displaytype); btnSubmit.Enabled = false; UpdateTimer.Enabled = false; Timer1.Enabled = true; }// Timer1.Interval = 1000; }// interval = (interval * 1000); UpdateTimer.Interval = int.Parse(interval.ToString()); UpdateTimer.Enabled = true; }
                }
                else Session["displaystatus"] = "True";//bip 07052010
                //}
                if (Session["displaystatus"] != null)
                    if (Session["displaystatus"].ToString() == "True")
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["totalQuesAvailable_memWords"] = ds.Tables[0].Rows.Count.ToString();
                            int j = 0;
                            for (int i = slno - 1; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (j >= quesperPage) break;
                                string Answer = "";
                                Session["CurrentControlCtrl"] = "WordTypeMemmoryQuestions.ascx";// bip 08012010
                                Session["ValueExists"] = "True";

                                //UpdateTimer.Enabled = true;// 07-05-2010 bip
                                Timer1.Enabled = true;// 04-03-2010 bip
                                //Timer1.Interval = 5000;
                                UpdateTimer.Enabled = false;// 15052010 bip


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
                                        if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                                        {
                                            rbOption1.Visible = true; lblA.Visible = true;
                                            rbOption1.Text = ds.Tables[0].Rows[i]["Option1"].ToString();
                                            if (ds.Tables[0].Rows[i]["Option1"].ToString() == Answer)
                                                rbOption1.Checked = true;
                                            optindex++;
                                        }
                                        if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                                        {
                                            if (optindex != 2)
                                                lblB.Text = GetAnswerOptionOrder(optindex);
                                            rbOption2.Visible = true; lblB.Visible = true;
                                            rbOption2.Text = ds.Tables[0].Rows[i]["Option2"].ToString();
                                            if (ds.Tables[0].Rows[i]["Option2"].ToString() == Answer)
                                                rbOption2.Checked = true;
                                            optindex++;
                                        }
                                        if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                                        {
                                            if (optindex != 3)
                                                lblC.Text = GetAnswerOptionOrder(optindex);
                                            rbOption3.Visible = true; lblC.Visible = true;
                                            rbOption3.Text = ds.Tables[0].Rows[i]["Option3"].ToString();
                                            if (ds.Tables[0].Rows[i]["Option3"].ToString() == Answer)
                                                rbOption3.Checked = true;
                                            optindex++;
                                        }
                                        if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                                        {
                                            if (optindex != 4)
                                                lblD.Text = GetAnswerOptionOrder(optindex);
                                            rbOption4.Visible = true; lblD.Visible = true;
                                            rbOption4.Text = ds.Tables[0].Rows[i]["Option4"].ToString();
                                            if (ds.Tables[0].Rows[i]["Option4"].ToString() == Answer)
                                                rbOption4.Checked = true;
                                            optindex++;
                                        }
                                        if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                                        {
                                            if (optindex != 5)
                                                lblE.Text = GetAnswerOptionOrder(optindex);
                                            rbOption5.Visible = true; lblE.Visible = true;
                                            rbOption5.Text = ds.Tables[0].Rows[i]["Option5"].ToString();
                                            if (ds.Tables[0].Rows[i]["Option5"].ToString() == Answer)
                                                rbOption5.Checked = true;
                                        }

                                        btnSubmit.Enabled = true; pnlWordDisplay.Visible = false;
                                        curntquescnt++;
                                        j++;
                                        pnlQuestion.Visible = true;
                                        break;
                                }
                            }
                        }
                        else { GoToNextPage(); }
                        Session["curntques"] = curntquescnt;
                    }
            }
        }
        catch (Exception ex)
        {
            //conn.Close();
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

        dtQuestionList.Columns.Add("QuestionID");
        dtQuestionList.Columns.Add("Question");
        dtQuestionList.Columns.Add("Answer");

        dtQuestionList.Columns.Add("Unit1");
        dtQuestionList.Columns.Add("Unit2");
        dtQuestionList.Columns.Add("Unit3");
        dtQuestionList.Columns.Add("Unit4");
        dtQuestionList.Columns.Add("Unit5");
        dtQuestionList.Columns.Add("Unit6");
        dtQuestionList.Columns.Add("Unit7");
        dtQuestionList.Columns.Add("Unit8");
        dtQuestionList.Columns.Add("Unit9");
        dtQuestionList.Columns.Add("Unit10");
        dtQuestionList.Columns.Add("Unit11");
        dtQuestionList.Columns.Add("Unit12");
        dtQuestionList.Columns.Add("Unit13");
        dtQuestionList.Columns.Add("Unit14");
        dtQuestionList.Columns.Add("Unit15");
        dtQuestionList.Columns.Add("Unit16");
        dtQuestionList.Columns.Add("Unit17");
        dtQuestionList.Columns.Add("Unit18");
        dtQuestionList.Columns.Add("Unit19");
        dtQuestionList.Columns.Add("Unit20");

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
            querystring = "select distinct WordTypeMemQuestionCount,SectionId from QuestionCount where WordTypeMemQuestionCount>0 and TestId=" + testId + " and TestSectionId=" + testsectionid + " and SectionName='" + firstVariableName + "' and SectionNameSub1='" + secondVariableName + "' order by sectionid";
        else if (firstVariableName != "")
            querystring = "select distinct WordTypeMemQuestionCount,SectionId from QuestionCount where WordTypeMemQuestionCount>0 and TestId=" + testId + " and TestSectionId=" + testsectionid + " and SectionName='" + firstVariableName + "' order by sectionid";

        DataSet dsQuestioncount = new DataSet();
        dsQuestioncount = clsclass.GetValuesFromDB(querystring);
        if (dsQuestioncount != null)
            if (dsQuestioncount.Tables.Count > 0)
                if (dsQuestioncount.Tables[0].Rows.Count > 0)
                    for (int c = 0; c < dsQuestioncount.Tables[0].Rows.Count; c++)//        
                    {
                        int wordTypeMemQuestionCount = int.Parse(dsQuestioncount.Tables[0].Rows[c]["WordTypeMemQuestionCount"].ToString());
                        int sectionid = int.Parse(dsQuestioncount.Tables[0].Rows[c]["SectionId"].ToString());

                        if (wordTypeMemQuestionCount > 0)
                        {
                            querystring = "SELECT TOP (" + wordTypeMemQuestionCount + ") QuestionID,Question,Answer,Unit1,Unit2,Unit3,Unit4,Unit5,Unit6,Unit7,Unit8,Unit9,Unit10,Unit11,Unit12,Unit13,Unit14,Unit15,Unit16,Unit17,Unit18,Unit19,Unit20,Option1,Option2,Option3,Option4,Option5,DisplayDuration,DisplayType  FROM View_TestBaseQuestionList_memWords where Status= 1 AND TestSectionId=" + testsectionid + " AND SectionId=" + sectionid + " AND TestId=" + testId + " ORDER BY RAND((1000*QuestionID)*DATEPART(millisecond, GETDATE())) ";

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
                                        drQurstionList["Unit1"] = dsQuesCount.Tables[0].Rows[i]["Unit1"];
                                        drQurstionList["Unit2"] = dsQuesCount.Tables[0].Rows[i]["Unit2"];
                                        drQurstionList["Unit3"] = dsQuesCount.Tables[0].Rows[i]["Unit3"];
                                        drQurstionList["Unit4"] = dsQuesCount.Tables[0].Rows[i]["Unit4"];
                                        drQurstionList["Unit5"] = dsQuesCount.Tables[0].Rows[i]["Unit5"];
                                        drQurstionList["Unit6"] = dsQuesCount.Tables[0].Rows[i]["Unit6"];
                                        drQurstionList["Unit7"] = dsQuesCount.Tables[0].Rows[i]["Unit7"];
                                        drQurstionList["Unit8"] = dsQuesCount.Tables[0].Rows[i]["Unit8"];
                                        drQurstionList["Unit9"] = dsQuesCount.Tables[0].Rows[i]["Unit9"];
                                        drQurstionList["Unit10"] = dsQuesCount.Tables[0].Rows[i]["Unit10"];
                                        drQurstionList["Unit11"] = dsQuesCount.Tables[0].Rows[i]["Unit11"];
                                        drQurstionList["Unit12"] = dsQuesCount.Tables[0].Rows[i]["Unit12"];
                                        drQurstionList["Unit13"] = dsQuesCount.Tables[0].Rows[i]["Unit13"];
                                        drQurstionList["Unit14"] = dsQuesCount.Tables[0].Rows[i]["Unit14"];
                                        drQurstionList["Unit15"] = dsQuesCount.Tables[0].Rows[i]["Unit15"];
                                        drQurstionList["Unit16"] = dsQuesCount.Tables[0].Rows[i]["Unit16"];
                                        drQurstionList["Unit17"] = dsQuesCount.Tables[0].Rows[i]["Unit17"];
                                        drQurstionList["Unit18"] = dsQuesCount.Tables[0].Rows[i]["Unit18"];
                                        drQurstionList["Unit19"] = dsQuesCount.Tables[0].Rows[i]["Unit19"];
                                        drQurstionList["Unit20"] = dsQuesCount.Tables[0].Rows[i]["Unit20"];

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
            dsQuestionList.Tables.Add(dtQuestionList); Session["questionColl_memWords"] = dsQuestionList;
            Session["totalQuesCount_memWords"] = dsQuestionList.Tables[0].Rows.Count.ToString();
        }
        else { Session["questionColl_memWords"] = null; dsQuestionList = null; }

        return dsQuestionList;
    }
    protected void imgbtnClickHere_Click(object sender, ImageClickEventArgs e)
    {
        Session["showclicked"] = "yes";// bip 07052010
       if (Session["pagecount_memWords"] != null)
           pagecount = int.Parse(Session["pagecount_memWords"].ToString());

       int selPage = 0;
       if (Session["MemWordPrevious"] != null)
       {
           if (Session["MemWordCurrentpage"] != null)
           {
               selPage = int.Parse(Session["MemWordCurrentpage"].ToString());
               if (pagecount > selPage)
               { Session["MemWordPrevious"] = null; Session["MemWordCurrentpage"] = pagecount; }
           }
           else Session["MemWordCurrentpage"] = pagecount.ToString();
       }
       else Session["MemWordCurrentpage"] = pagecount.ToString();
       int interval = 100; UpdateTimer.Interval = interval; UpdateTimer.Enabled = true;//Timer1.Interval = interval; Timer1.Enabled = true;//bip 07052010

    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        //UpdateTimer.Enabled = true;// bip 07052010
        
        SaveValues(); pnlpopup.Visible = false;
        Timer1.Enabled = true;// 04-03-2010 bip
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
       // Session["MemWordPrevious"] = null; Session["MemWordCurrentpage"] = null;//bip 07052010
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
        Response.Redirect("CareerJudge.htm");
    }
    protected void btnYes_timer_Test_Click(object sender, EventArgs e)
    {
        Session["timeExpired"] = null; Session["saved"] = null;
        //SaveAnswer();// bip 07052010
        //// 230110 bip        
        dataclass.Procedure_DeleteUserTest_TempValues(userid, 0, 0);
        ////
        Session.Clear();
        Response.Redirect("CareerJudge.htm");
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
        //UpdateTimer.Enabled = true; // bip 07052010
       Timer1.Enabled = true;// 04-03-2010 bip
    }
}
