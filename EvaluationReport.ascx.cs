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

public partial class EvaluationReport : System.Web.UI.UserControl
{
  AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    double res;
    protected void Page_Load(object sender, EventArgs e)
    {
        EvalReport();
    }

    private void EvalReport()
    {
        try
        {
            txtScore.Text = "";
            txtPass.Text = "";
            int i = 0;
            int qusid = 0;
            int marks = 0;
            int userid = 0;
            double numbers = 0;
            string usercode = "";
            string userans = "";
            string tablans = "";
            if (Session["UserCode"] != null)
            {
                usercode = Session["UserCode"].ToString();
            }
            if (Session["UserID"] != null)
                userid = int.Parse(Session["UserID"].ToString());
            var Ans1 = from Ans in dataclass.EvaluationResults
                       where Ans.UserId == userid// && Ans.UserCode == usercode
                       select Ans;
            if (Ans1.Count() > 0)
            {
                string question = "";
                foreach (var CompareAns in Ans1)
                {
                    //qusid = CompareAns.QuestionID;
                    question = CompareAns.Question.ToString();
                    numbers = int.Parse(Ans1.Count().ToString()); //= questncount();
                    //var QusAns1 = from QusAns in dataclass.ObjectiveQuestionsLists / QuesObjs
                    var QusAns1 = from QusAns in dataclass.QuestionCollections
                                  where QusAns.Question == question                                      //.QuestionID == qusid
                                  select QusAns;
                    if (QusAns1.Count() > 0)
                    {
                        if (QusAns1.First().Category == "FillBlanks")
                        {
                           // userans = CompareAns.Answer.ToString().ToLower();
                           // tablans = QusAns1.First().Answer.ToString().ToLower();
                            if (userans.Contains(tablans))
                            {
                                marks = marks + 1;
                            }

                            //
                            bool getanswer = false;
                            string answer = "", useranswer = "";
                            if (CompareAns.Answer != null)
                                useranswer = CompareAns.Answer.ToString().ToLower();

                            if (QusAns1.First().Option1 != null)
                            {
                                answer = QusAns1.First().Option1.ToString().ToLower();
                                if (useranswer.Contains(answer))
                                { marks = marks + 1; getanswer = true; }
                            }
                            if (getanswer == false)
                                if (QusAns1.First().Option2 != null)
                                {
                                    answer = QusAns1.First().Option2.ToString().ToLower();
                                    if (useranswer.Contains(answer))
                                    { marks = marks + 1; getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option3 != null)
                                {
                                    answer = QusAns1.First().Option3.ToString().ToLower();
                                    if (useranswer.Contains(answer))
                                    { marks = marks + 1; getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option4 != null)
                                {
                                    answer = QusAns1.First().Option4.ToString().ToLower();
                                    if (useranswer.Contains(answer))
                                    { marks = marks + 1; getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option5 != null)
                                {
                                    answer = QusAns1.First().Option5.ToString().ToLower();
                                    if (useranswer.Contains(answer))
                                    { marks = marks + 1; getanswer = true; }
                                }
                                //
                        }
                       /* commented on 15-06-2012(all answers are numbers except fillblanks questions)
                        * else if (QusAns1.First().Category == "Objective" || QusAns1.First().Category == "VideoType" || QusAns1.First().Category == "AudioType")
                        {
                            bool getanswer = false;
                            string answer = "", useranswer = "";
                            if (CompareAns.Answer != null)
                                useranswer = CompareAns.Answer.ToString().ToLower();
                            ////if (QusAns1.First().Answer != null)
                            ////    answer = QusAns1.First().Answer.ToString().ToLower();
                            ////if (useranswer.Contains(answer))
                            ////    marks = marks + 1;

                            answer = QusAns1.First().Answer.ToString().ToLower();
                            if (answer != "" && useranswer != "")
                            {
                                if (useranswer == answer)
                                { marks = marks + 1; }
                            }

                            /*
                            if (QusAns1.First().Option1 != null)
                            {
                                answer = QusAns1.First().Option1.ToString().ToLower();
                                if (useranswer == answer)
                                { marks = marks + 1; getanswer = true; }
                            }
                            if (getanswer == false)
                                if (QusAns1.First().Option2 != null)
                                {
                                    answer = QusAns1.First().Option2.ToString().ToLower();
                                    if (useranswer == answer)
                                    { marks = marks + 1; getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option3 != null)
                                {
                                    answer = QusAns1.First().Option3.ToString().ToLower();
                                    if (useranswer == answer)
                                    { marks = marks + 1; getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option4 != null)
                                {
                                    answer = QusAns1.First().Option4.ToString().ToLower();
                                    if (useranswer == answer)
                                    { marks = marks + 1; getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option5 != null)
                                {
                                    answer = QusAns1.First().Option5.ToString().ToLower();
                                    if (useranswer == answer)
                                    { marks = marks + 1; getanswer = true; }
                                }
                            ///* /

                        }*/
                        else if (QusAns1.First().Category == "RatingType")
                        {
                            bool getanswer = false;
                            string answer = "", useranswer = "";
                            if (CompareAns.Answer != null)
                                useranswer = CompareAns.Answer.ToString().ToLower();
                            ////if (QusAns1.First().Answer != null)
                            ////    answer = QusAns1.First().Answer.ToString().ToLower();
                            ////if (useranswer.Contains(answer))
                            ////    marks = marks + 1;
                            /// new 290909 bip
                            string scoringtype = "Forward";
                            if (QusAns1.First().ScoringStyle != null && QusAns1.First().ScoringStyle != "")
                                scoringtype = QusAns1.First().ScoringStyle.ToString();
                            ///
                            if (QusAns1.First().Option1 != null)
                            {
                                answer = QusAns1.First().Option1.ToString().ToLower();
                                if (useranswer == answer)
                                { if (scoringtype == "Forward") marks = marks + 1; else marks = marks + 10; getanswer = true; }
                            }
                            if (getanswer == false)
                                if (QusAns1.First().Option2 != null)
                                {
                                    answer = QusAns1.First().Option2.ToString().ToLower();
                                    if (useranswer == answer)
                                    { if (scoringtype == "Forward") marks = marks + 2; else marks = marks + 9; getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option3 != null)
                                {
                                    answer = QusAns1.First().Option3.ToString().ToLower();
                                    if (useranswer == answer)
                                    { if (scoringtype == "Forward") marks = marks + 3; else marks = marks + 8;  getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option4 != null)
                                {
                                    answer = QusAns1.First().Option4.ToString().ToLower();
                                    if (useranswer == answer)
                                    { if (scoringtype == "Forward") marks = marks + 4; else marks = marks + 7; getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option5 != null)
                                {
                                    answer = QusAns1.First().Option5.ToString().ToLower();
                                    if (useranswer == answer)
                                    { if (scoringtype == "Forward") marks = marks + 5; else marks = marks + 6; getanswer = true; }
                                }
                            if (QusAns1.First().Option6 != null)
                            {
                                answer = QusAns1.First().Option6.ToString().ToLower();
                                if (useranswer == answer)
                                { if (scoringtype == "Forward") marks = marks + 6; else marks = marks + 5; getanswer = true; }
                            }
                            if (getanswer == false)
                                if (QusAns1.First().Option7 != null)
                                {
                                    answer = QusAns1.First().Option7.ToString().ToLower();
                                    if (useranswer == answer)
                                    { if (scoringtype == "Forward") marks = marks + 7; else marks = marks + 4; getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option8 != null)
                                {
                                    answer = QusAns1.First().Option8.ToString().ToLower();
                                    if (useranswer == answer)
                                    { if (scoringtype == "Forward") marks = marks + 8; else marks = marks + 3; getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option9 != null)
                                {
                                    answer = QusAns1.First().Option9.ToString().ToLower();
                                    if (useranswer == answer)
                                    { if (scoringtype == "Forward") marks = marks + 9; else marks = marks + 2; getanswer = true; }
                                }
                            if (getanswer == false)
                                if (QusAns1.First().Option10 != null)
                                {
                                    answer = QusAns1.First().Option10.ToString().ToLower();
                                    if (useranswer == answer)
                                    { if (scoringtype == "Forward") marks = marks + 10; else marks = marks + 1; getanswer = true; }
                                }
                        }
                        else //if (QusAns1.First().Category == "ImageType")
                        {

                            bool getanswer = false;
                            string answer = "", useranswer = "";
                            if (CompareAns.Answer != null)
                                useranswer = CompareAns.Answer.ToString().ToLower();                          
                            
                                answer = QusAns1.First().Answer.ToString().ToLower();
                                if (useranswer != "" && answer != "")
                                {
                                    if (useranswer == answer)
                                    { marks = marks + 1; }
                                }                            
                        }
                    }
                }
            }

            //double score = double.Parse(txtScore.Text);
            if (marks > 0)
                res = (marks / numbers) * 100;
            int passmark = 0;
            int testid = 0;
            if (Session["UserTestId"] != null)
                testid = int.Parse(Session["UserTestId"].ToString());

            var getpassmark = from passmarkdet in dataclass.TestLists
                              where passmarkdet.TestId == testid
                              select passmarkdet;
            if (getpassmark.Count() > 0)
                if (getpassmark.First().PassMark != null)
                    passmark = int.Parse(getpassmark.First().PassMark.ToString());
            

            txtPass.Text = passmark.ToString(); //"70%";
            txtScore.Text = res.ToString("0.00");
            if (passmark == 0) return;
            if (res >= double.Parse(passmark.ToString()))// 70)
            {
                txtResult.Text = "Passed"; btnPrevious.Visible = false; btnTraining.Visible = true;

                //string usercode = "";
                string curcontrol = "ThankYou.ascx";
                int Evalstatid = 0;
                //int userid = 0;
                //userid = int.Parse(Session["UserID"].ToString());  
                //usercode = Session["UserCode"].ToString();
                if (Session["EvalStatId"] != null)
                {
                    Evalstatid = int.Parse(Session["EvalStatId"].ToString());
                }
                dataclass.ProcedureEvaluationStatus(Evalstatid, curcontrol, 1, 0, usercode, userid);
            }
            else
            {
                txtResult.Text = "Failed"; btnTraining.Visible = false;
                btnExit.Visible = false;
                //code to delete entries from evaluationresult

                dataclass.DeleteEvaluationResult(userid);
                int Evalstatid = 0;
                if (Session["EvalStatId"] != null)
                    Evalstatid = int.Parse(Session["EvalStatId"].ToString());

                dataclass.ProcedureEvaluationStatus(Evalstatid, "TestIntroductionControl.ascx", 0, 1, usercode, userid);
            }
            // }
        }
        catch (Exception ex) { }
    }

    private int questncount()
    {
        int totalQues = 0;
        var QuesCount1 = from QuesCount in dataclass.QuestionCounts
                         select QuesCount;
        if (QuesCount1.Count() > 0)
        {
            if (QuesCount1.First().FillBlanksQuestionCount != null)
                totalQues = int.Parse(QuesCount1.First().FillBlanksQuestionCount.ToString());
            if (QuesCount1.First().ObjQuestionCount != null)
                totalQues += int.Parse(QuesCount1.First().ObjQuestionCount.ToString());
        }
        int totQuesAvail = 0;
        var QusAns1 = from QusAns in dataclass.QuestionCollections
                      where QusAns.Status == 1
                      select QusAns;
        if (QusAns1.Count() > 0)
            totQuesAvail = int.Parse(QusAns1.Count().ToString());

        if (totalQues > totQuesAvail)
            totalQues = totQuesAvail;

        return totalQues;

    }
    protected void btnTraining_Click(object sender, EventArgs e)
    {
        Session["evalResult"] = "Passed";
        Session["SubCtrl"] = "ThankYou.ascx";
        Response.Redirect("FJAHome.aspx");        
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
        Session["preveval"] = 1;
        Session["SubCtrl"] = "TestIntroductionControl.ascx"; //"UserTrainingControl.ascx"; //"FillBalnksQues.ascx";
        Response.Redirect("FJAHome.aspx");
    }
}
