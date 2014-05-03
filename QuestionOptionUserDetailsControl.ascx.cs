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

public partial class QuestionOptionUserDetailsControl : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataClasses = new AssesmentDataClassesDataContext();
    DBManagementClass clsclass=new DBManagementClass();
    int quescount1 = 0; int testid = 0;

    bool specialadmin = false;
    int OrganizationID = 0;
    string organizationName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {
            ddlOrganizationList.Visible = false; lblOrganization.Visible = false;
            specialadmin = true; OrganizationID = int.Parse(Session["AdminOrganizationID"].ToString());

            if (Session["adminOrgName"] != null)
                organizationName = Session["adminOrgName"].ToString();
            else
            {
                var orgName = from orgDet in dataClasses.Organizations
                              where orgDet.OrganizationID == OrganizationID
                              select orgDet;
                if (orgName.Count() > 0)
                { organizationName = orgName.First().Name.ToString(); Session["adminOrgName"] = organizationName; }

            }
            //FillTestList();
        }
        else
            ddlOrganizationList.DataBind();
        if (Session["orgIndex_report"] != null)
            ddlOrganizationList.SelectedIndex = int.Parse(Session["orgIndex_report"].ToString());
        FillTestList();
    }
    private void FillTestList()
    {
        int testindex = 0;
        if (Session["testIndex_Quesreport"] != null)
            testindex = int.Parse(Session["testIndex_Quesreport"].ToString());
        ddlTestList.Items.Clear();
        ListItem litem = new ListItem("-- Select --", "0");
        ddlTestList.Items.Add(litem);

        //int adminaccess = 1;
        if (specialadmin == false)
            organizationName = ddlOrganizationList.SelectedItem.Text;

        if (ddlOrganizationList.SelectedIndex > 0 || specialadmin==true)
        {
            var Gettestdetails = from testdet in dataClasses.TestLists
                                 where testdet.OrganizationName == organizationName
                                 select testdet;
            if (Gettestdetails.Count() > 0)
            {
                ddlTestList.DataSource = Gettestdetails;
                ddlTestList.DataTextField = "TestName";
                ddlTestList.DataValueField = "TestId";
                ddlTestList.DataBind();
                if (testindex > 0)
                {
                    ddlTestList.SelectedIndex = testindex;
                    FillReportDetails();
                }
            }
        }
    }

    private void FillReportDetails()
    {
        DataSet dsQuestionDetails; DataSet dsUserDetails123;
        if (Session["UserDetails"] != null || Session["QuestionDetails"] != null)
        {
            if (Session["QuestionDetails"] != null)
            {
                dsQuestionDetails = new DataSet();
                dsQuestionDetails = (DataSet)Session["QuestionDetails"];
                gvwQuestions.DataSource = dsQuestionDetails.Tables[0];// dt;
                gvwQuestions.DataBind();

                if (Session["UserDetails"] != null)
                {
                    dsUserDetails123 = new DataSet();
                    dsUserDetails123 = (DataSet)Session["UserDetails"];
                    gvwReport.DataSource = dsUserDetails123.Tables[0];
                    gvwReport.DataBind();
                }
            }
            return;
        }

        string clearhtmlques = "";
        
        int testId = 0;
        
        if (ddlTestList.SelectedIndex > 0)
        { 
            testId = int.Parse(ddlTestList.SelectedValue);
        string querystring = "SELECT UserProfile.FirstName + ' ' + UserProfile.MiddleName + ' ' + UserProfile.LastName AS Name, " +
                      "UserProfile.EmailId, Industry.Name AS CompanyName, JobCategory.Name AS JobCategory, UserProfile.UserId,UserProfile.FirstLoginDate FROM UserProfile LEFT OUTER JOIN " +
                      "Industry ON UserProfile.CompanyID = Industry.IndustryID LEFT OUTER JOIN JobCategory ON UserProfile.JobCategoryID = JobCategory.JobCatID WHERE UserProfile.Status=1 and UserProfile.TestId =" + testId;
        DataSet dsUserDetails = new DataSet();
            dsUserDetails=clsclass.GetValuesFromDB(querystring);
            if (dsUserDetails != null)
                if (dsUserDetails.Tables[0].Rows.Count > 0)
                {
                    gvwUserDetails.DataSource = dsUserDetails;
                    gvwUserDetails.DataBind();
                }
            //
            DataTable dtQuestionIdColl = new DataTable();
            DataRow drQuestionColl;
            dtQuestionIdColl.Columns.Add("QuestionID");
            dtQuestionIdColl.Columns.Add("NoOfOptions");
            dtQuestionIdColl.Columns.Add("CorrectAnsOpt");
            dtQuestionIdColl.Columns.Add("QuestionCode");//17102011
            ///Chenju
            DataTable QuestionTab = new DataTable();

            DataRow QuestionRow;
            QuestionTab.Columns.Add("QuestionID");
            QuestionTab.Columns.Add("Question");
            QuestionTab.Columns.Add("Category");
            QuestionTab.Columns.Add("QuestionCode");//17102011
           

            //
            //querystring="SELECT  QuestionCollection.QuestionID, QuestionCollection.Category, QuestionCollection.Question, QuestionCollection.Answer, " +
            //          "QuestionCollection.Option1, QuestionCollection.Option2, QuestionCollection.Option3, QuestionCollection.Option4, " +
            //          "QuestionCollection.Option5, QuestionCollection.Option6, QuestionCollection.Option7, QuestionCollection.Option8, " +
            //          "QuestionCollection.Option9, QuestionCollection.Option10, QuestionCollection.QuestionFileName, " +
            //          "QuestionCollection.Option1FileName, QuestionCollection.Option2FileName, QuestionCollection.Option3FileName, " +
            //          "QuestionCollection.Option4FileName, QuestionCollection.Option5FileName, QuestionCollection.ScoringStyle, " +
            //          "TestBaseQuestionList.TestId, SectionDetail.BriefName FROM QuestionCollection INNER JOIN "+
            //          "TestBaseQuestionList ON QuestionCollection.QuestionID = TestBaseQuestionList.QuestionId INNER JOIN " +
            //          "SectionDetail ON QuestionCollection.SectionId = SectionDetail.SectionId " +
            //          " WHERE (TestBaseQuestionList.Status = 1) AND (QuestionCollection.Status = 1) AND (TestBaseQuestionList.TestId =" + testId+")";

            querystring = "SELECT QuestionCollection.QuestionID, QuestionCollection.Category, QuestionCollection.Question, QuestionCollection.Answer, " +
                                  "QuestionCollection.Option1, QuestionCollection.Option2, QuestionCollection.Option3, QuestionCollection.Option4, " +
                                  "QuestionCollection.Option5, QuestionCollection.Option6, QuestionCollection.Option7, QuestionCollection.Option8, " +
                                  "QuestionCollection.Option9, QuestionCollection.Option10, QuestionCollection.QuestionFileName, " +
                                  "QuestionCollection.Option1FileName, QuestionCollection.Option2FileName, QuestionCollection.Option3FileName, " +
                                  "QuestionCollection.Option4FileName, QuestionCollection.Option5FileName, QuestionCollection.ScoringStyle, " +
                                  "TestBaseQuestionList.TestId,QuestionCollection.QuestionCode FROM QuestionCollection INNER JOIN TestBaseQuestionList ON " +
                                  "QuestionCollection.QuestionID = TestBaseQuestionList.QuestionId where TestBaseQuestionList.Status=1 and QuestionCollection.Status=1 and TestBaseQuestionList.TestId=" + testId;

            DataSet dsQuestionIdDetails = new DataSet();
            dsQuestionIdDetails = clsclass.GetValuesFromDB(querystring);
            if (dsQuestionIdDetails != null)
                if (dsQuestionIdDetails.Tables[0].Rows.Count > 0)
                {                    
                    for (int i = 0; i < dsQuestionIdDetails.Tables[0].Rows.Count; i++)
                    {
                        QuestionRow = QuestionTab.NewRow();
                        drQuestionColl = dtQuestionIdColl.NewRow();
                        drQuestionColl["QuestionID"] = dsQuestionIdDetails.Tables[0].Rows[i]["QuestionID"];
                        drQuestionColl["QuestionCode"] = dsQuestionIdDetails.Tables[0].Rows[i]["QuestionCode"];//17102011
                        QuestionRow["QuestionID"] = dsQuestionIdDetails.Tables[0].Rows[i]["QuestionID"];
                        clearhtmlques = ClearHTMLTags(dsQuestionIdDetails.Tables[0].Rows[i]["Question"].ToString());
                        QuestionRow["Question"] = clearhtmlques;
                        QuestionRow["Category"] = dsQuestionIdDetails.Tables[0].Rows[i]["Category"];
                        QuestionRow["QuestionCode"] = dsQuestionIdDetails.Tables[0].Rows[i]["QuestionCode"];//17102011
                        int optioncount = 0;

                        if (dsQuestionIdDetails.Tables[0].Rows[i]["Category"].ToString() == "ImageType" || dsQuestionIdDetails.Tables[0].Rows[i]["Category"].ToString() == "PhotoType")
                        {
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option1"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option1"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option2"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option2"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option3"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option3"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option4"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option4"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option5"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option5"].ToString() != "")
                                optioncount++;
                            if (optioncount == 0)
                            {
                                if (dsQuestionIdDetails.Tables[0].Rows[i]["Option1FileName"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option1FileName"].ToString() != "")
                                    optioncount++;
                                if (dsQuestionIdDetails.Tables[0].Rows[i]["Option2FileName"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option2FileName"].ToString() != "")
                                    optioncount++;
                                if (dsQuestionIdDetails.Tables[0].Rows[i]["Option3FileName"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option3FileName"].ToString() != "")
                                    optioncount++;
                                if (dsQuestionIdDetails.Tables[0].Rows[i]["Option4FileName"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option4FileName"].ToString() != "")
                                    optioncount++;
                                if (dsQuestionIdDetails.Tables[0].Rows[i]["Option5FileName"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option5FileName"].ToString() != "")
                                    optioncount++;

                            }

                        }
                        else
                        {

                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option1"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option1"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option2"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option2"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option3"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option3"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option4"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option4"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option5"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option5"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option6"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option6"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option7"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option7"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option8"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option8"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option9"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option9"].ToString() != "")
                                optioncount++;
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Option10"] != null && dsQuestionIdDetails.Tables[0].Rows[i]["Option10"].ToString() != "")
                                optioncount++;
                        }
                       
                        drQuestionColl["NoOfOptions"] = optioncount.ToString();

                        string answer = ""; int answerIndex = 0;
                        answer = dsQuestionIdDetails.Tables[0].Rows[i]["Answer"].ToString();
                        if (answer != "")
                            if (dsQuestionIdDetails.Tables[0].Rows[i]["Category"] != null)
                                if (dsQuestionIdDetails.Tables[0].Rows[i]["Category"].ToString() != "ImageType" && dsQuestionIdDetails.Tables[0].Rows[i]["Category"].ToString() != "PhotoType")
                                {
                                    if (answer == dsQuestionIdDetails.Tables[0].Rows[i]["Option1"].ToString())
                                        answerIndex = 1;
                                    else if (answer == dsQuestionIdDetails.Tables[0].Rows[i]["Option2"].ToString())
                                        answerIndex = 2;
                                    else if (answer == dsQuestionIdDetails.Tables[0].Rows[i]["Option3"].ToString())
                                        answerIndex = 3;
                                    else if (answer == dsQuestionIdDetails.Tables[0].Rows[i]["Option4"].ToString())
                                        answerIndex = 4;
                                    else if (answer == dsQuestionIdDetails.Tables[0].Rows[i]["Option5"].ToString())
                                        answerIndex = 5;
                                    else if (answer == dsQuestionIdDetails.Tables[0].Rows[i]["Option6"].ToString())
                                        answerIndex = 6;
                                    else if (answer == dsQuestionIdDetails.Tables[0].Rows[i]["Option7"].ToString())
                                        answerIndex = 7;
                                    else if (answer == dsQuestionIdDetails.Tables[0].Rows[i]["Option8"].ToString())
                                        answerIndex = 8;
                                    else if (answer == dsQuestionIdDetails.Tables[0].Rows[i]["Option9"].ToString())
                                        answerIndex = 9;
                                    else if (answer == dsQuestionIdDetails.Tables[0].Rows[i]["Option10"].ToString())
                                        answerIndex = 10;
                                }

                                else answerIndex = int.Parse(answer.ToString());
                        if (dsQuestionIdDetails.Tables[0].Rows[i]["Category"].ToString() == "FillBlanks" || dsQuestionIdDetails.Tables[0].Rows[i]["Category"].ToString() == "RatingType")
                        {
                            drQuestionColl["CorrectAnsOpt"] = "";
                        }
                        else
                            drQuestionColl["CorrectAnsOpt"] = answerIndex.ToString();

                        dtQuestionIdColl.Rows.Add(drQuestionColl);
                        QuestionTab.Rows.Add(QuestionRow);
                    }

                    gvwQuestionIdDet.DataSource = dtQuestionIdColl;
                    gvwQuestionIdDet.DataBind();
                }


            //
            dtQuestionIdColl = new DataTable();
            dtQuestionIdColl.Columns.Add("QuestionID");
            dtQuestionIdColl.Columns.Add("NoOfOptions");
            dtQuestionIdColl.Columns.Add("CorrectAnsOpt");
            dtQuestionIdColl.Columns.Add("QuestionCode");

            querystring = "SELECT MemmoryTestImageQuesCollection.QuestionID, MemmoryTestImageQuesCollection.Category,MemmoryTestImageQuesCollection.Question, MemmoryTestImageQuesCollection.Image1, MemmoryTestImageQuesCollection.Image2, " +
                      "MemmoryTestImageQuesCollection.Image3, MemmoryTestImageQuesCollection.Image4, MemmoryTestImageQuesCollection.Image5, MemmoryTestImageQuesCollection.Image6, MemmoryTestImageQuesCollection.Image7, MemmoryTestImageQuesCollection.Image8, " +
                      "MemmoryTestImageQuesCollection.Image9, MemmoryTestImageQuesCollection.Image10, MemmoryTestImageQuesCollection.Image11, MemmoryTestImageQuesCollection.Image12, MemmoryTestImageQuesCollection.Image13, MemmoryTestImageQuesCollection.Image14, " +
                      "MemmoryTestImageQuesCollection.Image15, MemmoryTestImageQuesCollection.Image16, MemmoryTestImageQuesCollection.Image17, MemmoryTestImageQuesCollection.Image18, MemmoryTestImageQuesCollection.Image19, MemmoryTestImageQuesCollection.Image20, " +
                      "MemmoryTestImageQuesCollection.OptionFile1, MemmoryTestImageQuesCollection.OptionFile2, MemmoryTestImageQuesCollection.OptionFile3, MemmoryTestImageQuesCollection.OptionFile4, MemmoryTestImageQuesCollection.OptionFile5, " +
                      "MemmoryTestImageQuesCollection.Option1, MemmoryTestImageQuesCollection.Option2, MemmoryTestImageQuesCollection.Option3, MemmoryTestImageQuesCollection.Option4, MemmoryTestImageQuesCollection.Option5, MemmoryTestImageQuesCollection.Answer, " +
                      "MemmoryTestImageQuesCollection.DisplayDuration, MemmoryTestImageQuesCollection.DisplayType, TestBaseQuestionList.TestId,MemmoryTestImageQuesCollection.QuestionCode FROM MemmoryTestImageQuesCollection INNER JOIN TestBaseQuestionList ON MemmoryTestImageQuesCollection.QuestionID = TestBaseQuestionList.QuestionId " +
                      " where TestBaseQuestionList.Status=1 and MemmoryTestImageQuesCollection.Status=1 and TestBaseQuestionList.TestId=" + testId;

            DataSet dsQuestionIdDetails_memImages = new DataSet();
            dsQuestionIdDetails_memImages = clsclass.GetValuesFromDB(querystring);
            if (dsQuestionIdDetails_memImages != null)
                if (dsQuestionIdDetails_memImages.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsQuestionIdDetails_memImages.Tables[0].Rows.Count; i++)
                    {
                        drQuestionColl = dtQuestionIdColl.NewRow();

                        /// Chenju
                        
                        QuestionRow = QuestionTab.NewRow();
                        QuestionRow["QuestionID"] = dsQuestionIdDetails_memImages.Tables[0].Rows[i]["QuestionID"];
                        clearhtmlques = ClearHTMLTags(dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Question"].ToString());
                        QuestionRow["Question"] = clearhtmlques;
                        QuestionRow["Category"] = dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Category"];
                        QuestionRow["QuestionCode"] = dsQuestionIdDetails_memImages.Tables[0].Rows[i]["QuestionCode"];//17102011

                        drQuestionColl["QuestionId"] =dsQuestionIdDetails_memImages.Tables[0].Rows[i]["QuestionID"];
                        drQuestionColl["QuestionCode"] = dsQuestionIdDetails_memImages.Tables[0].Rows[i]["QuestionCode"];//17102011
                        int optioncount = 0;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image1"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image1"].ToString() != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image2"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image2"].ToString() != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image3"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image3"].ToString() != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image4"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image4"].ToString() != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image5"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image5"].ToString() != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image6"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image6"].ToString() != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image7"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image7"].ToString() != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image8"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image8"].ToString() != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image9"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image9"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image10"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image10"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image11"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image11"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image12"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image12"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image13"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image13"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image14"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image14"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image15"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image15"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image16"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image16"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image17"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image17"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image18"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image18"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image19"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image19"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image20"] != null && dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Image20"] != "")
                            optioncount++;


                        drQuestionColl["NoOfOptions"] = optioncount.ToString();

                        string answer = "";
                        answer = dsQuestionIdDetails_memImages.Tables[0].Rows[i]["Answer"].ToString();
                        drQuestionColl["CorrectAnsOpt"] = answer.ToString();

                        dtQuestionIdColl.Rows.Add(drQuestionColl);
                        QuestionTab.Rows.Add(QuestionRow);
                    }

                    gvwQuestionIdDet_memimg.DataSource = dtQuestionIdColl;
                    gvwQuestionIdDet_memimg.DataBind();
                }



           //


            //
            dtQuestionIdColl = new DataTable();
            dtQuestionIdColl.Columns.Add("QuestionID");
            dtQuestionIdColl.Columns.Add("NoOfOptions");
            dtQuestionIdColl.Columns.Add("CorrectAnsOpt");
            dtQuestionIdColl.Columns.Add("QuestionCode");//17102011

            querystring = "SELECT MemmoryTestTextQuesCollection.QuestionID, MemmoryTestTextQuesCollection.Category, MemmoryTestTextQuesCollection.Question,MemmoryTestTextQuesCollection.Unit1, MemmoryTestTextQuesCollection.Unit2, MemmoryTestTextQuesCollection.Unit3, " +
                           "MemmoryTestTextQuesCollection.Unit4, MemmoryTestTextQuesCollection.Unit5, MemmoryTestTextQuesCollection.Unit6, MemmoryTestTextQuesCollection.Unit7, MemmoryTestTextQuesCollection.Unit8, MemmoryTestTextQuesCollection.Unit9, " +
                           "MemmoryTestTextQuesCollection.Unit10, MemmoryTestTextQuesCollection.Unit11, MemmoryTestTextQuesCollection.Unit12, MemmoryTestTextQuesCollection.Unit13, MemmoryTestTextQuesCollection.Unit14, MemmoryTestTextQuesCollection.Unit15, " +
                           "MemmoryTestTextQuesCollection.Unit16, MemmoryTestTextQuesCollection.Unit17, MemmoryTestTextQuesCollection.Unit18, MemmoryTestTextQuesCollection.Unit19, MemmoryTestTextQuesCollection.Unit20, MemmoryTestTextQuesCollection.Option1, " +
                           "MemmoryTestTextQuesCollection.Option2, MemmoryTestTextQuesCollection.Option3, MemmoryTestTextQuesCollection.Option4, MemmoryTestTextQuesCollection.Option5, MemmoryTestTextQuesCollection.Answer, TestBaseQuestionList.TestId,MemmoryTestTextQuesCollection.QuestionCode " +
                           "FROM MemmoryTestTextQuesCollection INNER JOIN TestBaseQuestionList ON MemmoryTestTextQuesCollection.QuestionID = TestBaseQuestionList.QuestionId where TestBaseQuestionList.Status=1 and MemmoryTestTextQuesCollection.Status=1 and TestBaseQuestionList.TestId=" + testId;


            DataSet dsQuestionIdDetails_memWords = new DataSet();
            dsQuestionIdDetails_memWords = clsclass.GetValuesFromDB(querystring);
            if (dsQuestionIdDetails_memWords != null)
                if (dsQuestionIdDetails_memWords.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dsQuestionIdDetails_memWords.Tables[0].Rows.Count; i++)
                    {
                        drQuestionColl = dtQuestionIdColl.NewRow();
                        
                        QuestionRow = QuestionTab.NewRow();
                        QuestionRow["QuestionID"] = dsQuestionIdDetails_memWords.Tables[0].Rows[i]["QuestionID"];
                        clearhtmlques = ClearHTMLTags(dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Question"].ToString());
                        QuestionRow["Question"] = clearhtmlques;
                        QuestionRow["Category"] = dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Category"];
                        QuestionRow["QuestionCode"] = dsQuestionIdDetails_memWords.Tables[0].Rows[i]["QuestionCode"];//17102011
                        drQuestionColl["QuestionId"] =dsQuestionIdDetails_memWords.Tables[0].Rows[i]["QuestionID"];
                        drQuestionColl["QuestionCode"] = dsQuestionIdDetails_memWords.Tables[0].Rows[i]["QuestionCode"];//17102011
                        int optioncount = 0;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit1"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit1"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit2"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit2"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit3"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit3"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit4"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit4"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit5"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit5"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit6"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit6"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit7"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit7"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit8"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit8"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit9"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit9"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit10"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit10"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit11"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit11"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit12"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit12"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit13"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit13"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit14"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit14"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit15"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit15"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit16"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit16"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit17"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit17"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit18"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit18"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit19"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit19"] != "")
                            optioncount++;
                        if (dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit20"] != null && dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Unit20"] != "")
                            optioncount++;


                        drQuestionColl["NoOfOptions"] = optioncount.ToString();

                        string answer = ""; int answerIndex = 0;
                        answer = dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Answer"].ToString();
                        if (answer != "")
                        {
                            if (answer == dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Option1"].ToString())
                                answerIndex = 1;
                            else if (answer == dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Option2"].ToString())
                                answerIndex = 2;
                            else if (answer == dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Option3"].ToString())
                                answerIndex = 3;
                            else if (answer == dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Option4"].ToString())
                                answerIndex = 4;
                            else if (answer == dsQuestionIdDetails_memWords.Tables[0].Rows[i]["Option5"].ToString())
                                answerIndex = 5;                            
                        }
                        drQuestionColl["CorrectAnsOpt"] = answerIndex.ToString();

                        dtQuestionIdColl.Rows.Add(drQuestionColl);
                        QuestionTab.Rows.Add(QuestionRow);
                    }
                    
                }
            gvwQuestionIdDet_memword.DataSource = dtQuestionIdColl;
            gvwQuestionIdDet_memword.DataBind();

            //
            
            Panel3.BorderWidth = 1;
            gvwQuestions.DataSource = QuestionTab;
            gvwQuestions.DataBind();

            DataSet ds = new DataSet();
            ds.Tables.Add(QuestionTab);            
            Session["QuestionDetails"] = ds;

            DataTable dtQuestionIdList = new DataTable();
            DataRow drQuestionRow;
            ////17102011
            dtQuestionIdList.Columns.Add("Title");
            dtQuestionIdList.Columns.Add("EmailId");
            dtQuestionIdList.Columns.Add("Company");
            dtQuestionIdList.Columns.Add("Job-Category");
            //
            //for (int i = 0; i <= gvwQuestionIdDet.Rows.Count; i++)
            for (int i = 1; i <= gvwQuestionIdDet.Rows.Count; i++)
            {
                //if (i == 0)
                //{
                //    dtQuestionIdList.Columns.Add("Title");
                //    dtQuestionIdList.Columns.Add("EmailId");
                //    dtQuestionIdList.Columns.Add("Company");
                //    dtQuestionIdList.Columns.Add("Job-Category");
                //}
                //else
                //{
                //    dtQuestionIdList.Columns.Add("QN" + i.ToString());
                //}
                   dtQuestionIdList.Columns.Add("QN" + (i ).ToString());
            }
            ////Chenju
            quescount1 = gvwQuestionIdDet.Rows.Count;            

            for (int i = 0; i < gvwQuestionIdDet_memimg.Rows.Count; i++)
            {
                dtQuestionIdList.Columns.Add("QN-MI-" + (i+1).ToString());
            }
            for (int i = 0; i < gvwQuestionIdDet_memword.Rows.Count; i++)
            {
                dtQuestionIdList.Columns.Add("QN-MT-" + (i+1).ToString());
            }

            dtQuestionIdList.Columns.Add("TestTakenDate");// 18022010 bip

            ///Chenju

            drQuestionRow = dtQuestionIdList.NewRow();
           // for (int j = 0; j < 3; j++)
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i <= gvwQuestionIdDet.Rows.Count; i++)
                {
                    
                    if (j == 0)
                    {
                        if (i == 0)
                        {
                            drQuestionRow = dtQuestionIdList.NewRow();
                            drQuestionRow["Title"] = "QuestionID";
                            dtQuestionIdList.Rows.Add(drQuestionRow);
                        }
                        
                        else
                        {                            
                            drQuestionRow = dtQuestionIdList.Rows[j];
                            drQuestionRow["QN" + i.ToString()] = gvwQuestionIdDet.Rows[i - 1].Cells[0].Text;
                        }
                    }
                    if (j == 1)
                    {
                        if (i == 0)
                        {
                            drQuestionRow = dtQuestionIdList.NewRow();
                            drQuestionRow["Title"] = "QuestionCode";
                            dtQuestionIdList.Rows.Add(drQuestionRow);
                        }

                        else
                        {
                            drQuestionRow = dtQuestionIdList.Rows[j];
                            if (gvwQuestionIdDet.Rows[i - 1].Cells[3].Text != "&nbsp;")
                                drQuestionRow["QN" + i.ToString()] = gvwQuestionIdDet.Rows[i - 1].Cells[3].Text;//17102011
                        }
                    }


                    else if (j == 2)
                    {
                        if (i == 0)
                        {
                            drQuestionRow = dtQuestionIdList.NewRow();
                            drQuestionRow["Title"] = "No. of Options";
                            dtQuestionIdList.Rows.Add(drQuestionRow);
                        }
                        //}
                        else
                        {                            
                            drQuestionRow = dtQuestionIdList.Rows[j];
                            drQuestionRow["QN" + i.ToString()] = gvwQuestionIdDet.Rows[i - 1].Cells[1].Text;
                            //}
                        }
                    }


                    else if (j == 3)
                    {
                        if (i == 0)
                        {
                            drQuestionRow = dtQuestionIdList.NewRow();
                            drQuestionRow["Title"] = "Correct Option Id";
                            dtQuestionIdList.Rows.Add(drQuestionRow);
                        }
                        else
                        {

                            drQuestionRow = dtQuestionIdList.Rows[j];
                            if (gvwQuestionIdDet.Rows[i - 1].Cells[2].Text != "&nbsp;")
                                drQuestionRow["QN" + i.ToString()] = gvwQuestionIdDet.Rows[i-1].Cells[2].Text;
                            //}
                        }
                        // }



                    }
                }
            }
                ///Chenju
           

            for (int i = 0; i < gvwQuestionIdDet_memimg.Rows.Count; i++)
            {
                drQuestionRow = dtQuestionIdList.Rows[0];

                drQuestionRow["QN-MI-" + (i + 1).ToString()] = gvwQuestionIdDet_memimg.Rows[i].Cells[3].Text;//17102011
                //drQuestionRow["QN-MI-" + (i + 1).ToString()] = gvwQuestionIdDet_memimg.Rows[i].Cells[0].Text;
                drQuestionRow = dtQuestionIdList.Rows[1];

                drQuestionRow["QN-MI-" + (i + 1).ToString()] = gvwQuestionIdDet_memimg.Rows[i].Cells[1].Text;
                drQuestionRow = dtQuestionIdList.Rows[2];

                drQuestionRow["QN-MI-" + (i + 1).ToString()] = gvwQuestionIdDet_memimg.Rows[i].Cells[2].Text;                
            }

            for (int i = 0; i < gvwQuestionIdDet_memword.Rows.Count; i++)
            {
                drQuestionRow = dtQuestionIdList.Rows[0];
                drQuestionRow["QN-MT-" + (i + 1).ToString()] = gvwQuestionIdDet_memword.Rows[i].Cells[3].Text;//17102011
                //drQuestionRow["QN-MT-" + (i + 1).ToString()] = gvwQuestionIdDet_memword.Rows[i].Cells[0].Text;
                drQuestionRow = dtQuestionIdList.Rows[1];

                drQuestionRow["QN-MT-" + (i + 1).ToString()] = gvwQuestionIdDet_memword.Rows[i].Cells[1].Text;
                drQuestionRow = dtQuestionIdList.Rows[2];

                drQuestionRow["QN-MT-" + (i + 1).ToString()] = gvwQuestionIdDet_memword.Rows[i].Cells[2].Text;
               
            }

            for (int x = 0; x < gvwUserDetails.Rows.Count; x++)
            {
                //drQuestionRow = dtQuestionIdList.NewRow();
                if (gvwUserDetails.Rows[x].Cells[0].Text != "&nbsp;")
                {
                    drQuestionRow = dtQuestionIdList.NewRow(); drQuestionRow["Title"] = gvwUserDetails.Rows[x].Cells[0].Text;
                }
                else { continue; }
                if (gvwUserDetails.Rows[x].Cells[1].Text != "&nbsp;")
                    drQuestionRow["EmailId"] = gvwUserDetails.Rows[x].Cells[1].Text;
                if (gvwUserDetails.Rows[x].Cells[2].Text != "&nbsp;")
                    drQuestionRow["Company"] = gvwUserDetails.Rows[x].Cells[2].Text;
                if (gvwUserDetails.Rows[x].Cells[3].Text != "&nbsp;")
                    drQuestionRow["Job-Category"] = gvwUserDetails.Rows[x].Cells[3].Text;

                // 18022010 bip

                if (gvwUserDetails.Rows[x].Cells[5].Text != "&nbsp;")
                    drQuestionRow["TestTakenDate"] = gvwUserDetails.Rows[x].Cells[5].Text;
                //

                dtQuestionIdList.Rows.Add(drQuestionRow);
                int rowcount = dtQuestionIdList.Rows.Count - 1;
                int userid = int.Parse(gvwUserDetails.Rows[x].Cells[4].Text);
                int questionid = 0;
                string columname = "";
                //for (int col = 4; col < dtQuestionIdList.Columns.Count; col++)
                for (int col = 4; col < dtQuestionIdList.Columns.Count-1; col++)// 18022010 bip
                {
                    columname = dtQuestionIdList.Columns[col].ToString();
                    drQuestionRow = dtQuestionIdList.Rows[0];
                    questionid = int.Parse(drQuestionRow[col].ToString());
                    string[] questype = columname.Split(new char[] { '-' });
                    string useranswer = "";
                    int optionid=0;
                    if (questype.Length > 1)
                    {
                        if (questype[1].ToString() == "MI")
                        {
                            var memoryimageans1 = from memoryimageans in dataClasses.EvaluationResults
                                                  where memoryimageans.UserId == userid
                                                  && memoryimageans.QuestionID == questionid
                                                  && memoryimageans.Category == "MemTestImages"
                                                  select memoryimageans;
                            if (memoryimageans1.Count() > 0)
                            {
                                if (memoryimageans1.First().Answer == "" || memoryimageans1.First().Answer.ToUpper() == "NULL")// bip 05062011
                                    useranswer = "NIL";
                                else

                                    useranswer = memoryimageans1.First().Answer;



                                drQuestionRow = dtQuestionIdList.Rows[rowcount];
                                drQuestionRow[col] = useranswer;
                            }

                        }
                        else if (questype[1].ToString() == "MT")
                        {
                            var memorytextans1 = from memorytextans in dataClasses.EvaluationResults
                                                 where memorytextans.UserId == userid
                                                 && memorytextans.QuestionID == questionid
                                                 && memorytextans.Category == "MemTestWords"
                                                 select memorytextans;
                            if (memorytextans1.Count() > 0)
                            {
                                if (memorytextans1.First().Answer == "" || memorytextans1.First().Answer.ToUpper() == "NULL")// bip 05062011
                                    useranswer = "NIL";
                                else
                                {
                                    useranswer = memorytextans1.First().Answer;
                                    var OptionID2 = from OptionID in dataClasses.MemmoryTestTextQuesCollections
                                                    where OptionID.QuestionID == questionid
                                                    select OptionID;
                                    if (OptionID2.Count() > 0)
                                    {
                                        if (OptionID2.First().Option1 == useranswer)
                                            optionid = 1;
                                        else if (OptionID2.First().Option2 == useranswer)
                                            optionid = 2;
                                        else if (OptionID2.First().Option3 == useranswer)
                                            optionid = 3;
                                        else if (OptionID2.First().Option4 == useranswer)
                                            optionid = 4;
                                        else if (OptionID2.First().Option5 == useranswer)
                                            optionid = 5;                                        

                                        useranswer = optionid.ToString();
                                    }
                                }
                                drQuestionRow = dtQuestionIdList.Rows[rowcount];
                                drQuestionRow[col] = useranswer;
                            }
                        }
                    }
                    else
                    {
                        var otherans1 = from otherans in dataClasses.EvaluationResults
                                        where otherans.UserId == userid
                                        && otherans.QuestionID == questionid
                                        && otherans.Category != "MemTestWords"
                                        && otherans.Category != "MemTestImages"
                                        select otherans;
                        if (otherans1.Count() > 0)
                        {
                            if (otherans1.First().Answer == "" || otherans1.First().Answer.ToUpper() == "NULL")
                                useranswer = "NIL";// user didn't get this question while attending the test
                            else
                            {
                                useranswer = otherans1.First().Answer;

                                if (otherans1.First().Category == "ImageType" || otherans1.First().Category == "RatingType" || otherans1.First().Category == "PhotoType" )// 03122009 bip
                                {
                                    goto save;
                                }
                                var OptionID1 = from OptionID in dataClasses.QuestionCollections
                                                where OptionID.QuestionID == questionid
                                                select OptionID;
                                if (OptionID1.Count() > 0)
                                {
                                    if(OptionID1.First().Option1 == useranswer)
                                        optionid=1;
                                    else if(OptionID1.First().Option2 == useranswer)
                                        optionid=2;
                                    else if(OptionID1.First().Option3 == useranswer)
                                        optionid=3;
                                     else if(OptionID1.First().Option4 == useranswer)
                                        optionid=4;
                                     else if(OptionID1.First().Option5 == useranswer)
                                        optionid=5;
                                     else if(OptionID1.First().Option6 == useranswer)
                                        optionid=6;
                                     else if(OptionID1.First().Option7 == useranswer)
                                        optionid=7;
                                     else if(OptionID1.First().Option8 == useranswer)
                                        optionid=8;
                                     else if(OptionID1.First().Option9 == useranswer)
                                        optionid=9;
                                     else if(OptionID1.First().Option10 == useranswer)
                                        optionid=10;

                                    useranswer = optionid.ToString();
                                }
                                
                            }
                        save:
                            drQuestionRow = dtQuestionIdList.Rows[rowcount];
                            drQuestionRow[col] = useranswer;
                        }
                    }
                }
                
            }
                //code to hide questionid from the report.. instead code will display
            dtQuestionIdList.Rows[0].Delete();//17102011

                Panel2.BorderWidth = 1;
            gvwReport.DataSource = dtQuestionIdList;
            gvwReport.DataBind();

            DataSet ds1 = new DataSet();
            ds1.Tables.Add(dtQuestionIdList);
            Session["UserDetails"] = ds1;
            //Response.Redirect("DownloadRefDataToExcel.aspx");

        }
    }
    

    protected void ddlOrganizationList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganizationList.SelectedIndex > 0)
        {
            if (Session["orgIndex_report"] != null)
                if (Session["orgIndex_report"].ToString() != ddlOrganizationList.SelectedIndex.ToString())
                    Session["testIndex_report"] = null;
            Session["orgIndex_report"] = ddlOrganizationList.SelectedIndex.ToString();
            FillTestList();
        }
        else Session["orgIndex_report"] = null;
    }
    public static string ClearHTMLTags(string source)
    {
        if (string.IsNullOrEmpty(source))
            return source;
        string temp = source;
        while (temp.IndexOf('<') != -1 && temp.IndexOf('>') != -1)
        {
            int start = temp.IndexOf('<');
            int end = temp.IndexOf('>');
            temp = temp.Remove(start, end - start + 1);
        }
        return temp;
    }

    protected void btnUserDetails_Click(object sender, EventArgs e)
    {      
        ExportToExcell(1);
    }
    protected void btnQuesDetails_Click(object sender, EventArgs e)
    {
      
        ExportToExcell(0);
    }
    private void ExportToExcell(int index)
    {
        if (ddlTestList.SelectedIndex <= 0)
        { lblMessage.Text = "Please select a Test from the list"; return; }

        bool testchanged = true;
        if (Session["reporttestid"] != null)
            if (Session["reporttestid"].ToString() == ddlTestList.SelectedValue)
                testchanged = false;
        Session["reporttestid"] = ddlTestList.SelectedValue;

        if (testchanged == true) FillReportDetails();
        if (Session["QuestionDetails"] == null) FillReportDetails();

        if (index == 0)
            Session["dsDownloadtoExcel"] = Session["QuestionDetails"];
        else Session["dsDownloadtoExcel"] = Session["UserDetails"];

        Response.Redirect("DownloadRefDataToExcel.aspx");

    }
    protected void ddlTestList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestList.SelectedIndex > 0)
        {
            testid = int.Parse(ddlTestList.SelectedValue);
            if (Session["testIndex_Quesreport"] != null)
                if (Session["testIndex_Quesreport"].ToString() != ddlTestList.SelectedIndex.ToString())
                {Session["QuestionDetails"] = null; Session["UserDetails"] = null;}

            Session["testIndex_Quesreport"] = ddlTestList.SelectedIndex.ToString();
            FillReportDetails();
        }
        else Session["testIndex_Quesreport"] = null;
       // FillReportDetails();
    }
}
