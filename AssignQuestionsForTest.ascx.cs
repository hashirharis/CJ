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

public partial class AssignQuestionsForTest : System.Web.UI.UserControl
{
  AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
  int testid = 0;
  bool specialadmin = false;
  int OrganizationID = 0;
  string organizationName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {
            lblOrganization.Visible = false; ddlOrganizationList.Visible = false;
            specialadmin = true; OrganizationID = int.Parse(Session["AdminOrganizationID"].ToString());

            if (Session["adminOrgName"] != null)
                organizationName = Session["adminOrgName"].ToString();
            else
            {
                var orgName = from orgDet in dataclass.Organizations
                              where orgDet.OrganizationID == OrganizationID
                              select orgDet;
                if (orgName.Count() > 0)
                { organizationName = orgName.First().Name.ToString(); Session["adminOrgName"] = organizationName; }

            }
            FillTestList();
        }
        else
            FillOrganizationList();

      //FillSessionslist();
        FillSectionCategory();
       if (Session["QuesTypeIndx"] != null)
       {
           ddlQuestionType.SelectedIndex = int.Parse(Session["QuesTypeIndx"].ToString());
           Bind();
       }
       if (Session["chkassn"] != null)
       {
           if (Session["chkassn"].ToString() == "YES")

               return;
       }

       FillResultBandDetails();
       
    }
    private void FillSectionCategory()
    {
        ListItem listname;
        int sectionCategoryId = 0;
        int sectionCategoryIndex = 0;
        if (Session["sectionCategoryIndex"] != null)
            sectionCategoryIndex = int.Parse(Session["sectionCategoryIndex"].ToString());
        listname = new ListItem("-- Select Section Category --", "0");
        ddlSectionCategory.Items.Clear();
        ddlSectionCategory.Items.Add(listname);
        ddlSectionCategory.DataSource = sectionCategoryDataSource;
        ddlSectionCategory.DataTextField = "SectionCategoryName";
        ddlSectionCategory.DataValueField = "SectionCategoryId";
        ddlSectionCategory.DataBind();
        if (sectionCategoryIndex > 0)
        {
            ddlSectionCategory.SelectedIndex = sectionCategoryIndex;
            sectionCategoryId = int.Parse(ddlSectionCategory.SelectedValue);

            FillSessionslist(sectionCategoryId);
        }
    }
    private void FillSessionslist(int sectionCategoryId)
    {
        try
        {
            ListItem listname;
            listname = new ListItem("-- select --", "0");
            ddlSectionList.Items.Clear();
            ddlSectionList.Items.Add(listname);
            int sectionIndex = 0;

            if (Session["sectionIndex"] != null)
                sectionIndex = int.Parse(Session["sectionIndex"].ToString());

            var details1 = from details in dataclass.SectionDetails
                           where details.ParentId == 0 && details.SectionCategoryId==sectionCategoryId
                           orderby details.SectionName ascending
                           select details;
            if (details1.Count() > 0)
            {
                ddlSectionList.DataSource = details1;
                ddlSectionList.DataTextField = "SectionName";
                ddlSectionList.DataValueField = "SectionId";
                ddlSectionList.DataBind();
            }

            ddlSectionList.SelectedIndex = sectionIndex;
            if (sectionIndex > 0)
            {
                FillSubLevel1Sections();
            }
            else
            {
                listname = new ListItem("-- select --", "0");
                ddlFirstLevelList.Items.Clear();
                ddlFirstLevelList.Items.Add(listname);
                listname = new ListItem("-- select --", "0");
                ddlSecondLevelList.Items.Clear();
                ddlSecondLevelList.Items.Add(listname);
            }

        }
        catch (Exception ex)
        {

        }
    }

    private void FillSubLevel1Sections()
    {
        ListItem listname;

        listname = new ListItem("-- select --", "0");
        ddlFirstLevelList.Items.Clear();
        ddlFirstLevelList.Items.Add(listname);
        int parentindex = 0;
        if (Session["SubLevel1Index"] != null)
            parentindex = int.Parse(Session["SubLevel1Index"].ToString());

        var details1 = from details in dataclass.SectionDetails
                       where details.ParentId == int.Parse(ddlSectionList.SelectedValue)
                       orderby details.SectionName ascending
                       select details;
        if (details1.Count() > 0)
        {
            ddlFirstLevelList.DataSource = details1;
            ddlFirstLevelList.DataTextField = "SectionName";
            ddlFirstLevelList.DataValueField = "SectionId";
            ddlFirstLevelList.DataBind();
        }

        if (parentindex > 0)
        {
            ddlFirstLevelList.SelectedIndex = parentindex;
            FillSubLevel2Sections();
        }
        else
        {
            listname = new ListItem("-- select --", "0");
            ddlSecondLevelList.Items.Clear();
            ddlSecondLevelList.Items.Add(listname);
        }
    }

    private void FillSubLevel2Sections()
    {
        ListItem listname;
        listname = new ListItem("-- select --", "0");
        ddlSecondLevelList.Items.Clear();
        ddlSecondLevelList.Items.Add(listname);

        int subindex = 0;
        if (Session["SubLevel2Index"] != null)
            subindex = int.Parse(Session["SubLevel2Index"].ToString());

        var details1 = from details in dataclass.SectionDetails
                       where details.ParentId == int.Parse(ddlFirstLevelList.SelectedValue)
                       orderby details.SectionName ascending
                       select details;
        if (details1.Count() > 0)
        {
            ddlSecondLevelList.DataSource = details1;
            ddlSecondLevelList.DataTextField = "SectionName";
            ddlSecondLevelList.DataValueField = "SectionId";
            ddlSecondLevelList.DataBind();
        }

        if (subindex > 0)
        {
        ddlSecondLevelList.SelectedIndex = subindex;
        Bind();
        }

    }

    /*
    private void Fillcombo()
    {
        int QuesTypeIndx = 0;
         ListItem item1=new ListItem("--Select Question Type--","0");
         ListItem item2=new ListItem("Objective Type Questions","Objective");
         ListItem item3=new ListItem("Fill in the Blanks Questions","FillBlanks");
         ddlQuestionType.Items.Add(item1);
         ddlQuestionType.Items.Add(item2);
         ddlQuestionType.Items.Add(item3);
         if (Session["QuesTypeIndx"] != null)
         {
             QuesTypeIndx = int.Parse(Session["QuesTypeIndx"].ToString());
         }
         ddlQuestionType.SelectedIndex = QuesTypeIndx;
         
    }
    */

    protected void ddlQuestionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["QuesTypeIndx"] != null)
        {
            if (Session["QuesTypeIndx"].ToString() == ddlQuestionType.SelectedIndex.ToString())
            {
                //Bind(); BindGrid();
                return;
            }
        }
        Session["QuesTypeIndx"] = ddlQuestionType.SelectedIndex;
        Bind();
        //BindGrid();// bip 220110

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

    private void FillWordTypeMemQuestions(int sectionid)
    {
        var ObjQues1 = from ObjQues in dataclass.MemmoryTestTextQuesCollections
                       where ObjQues.SectionId == sectionid //&& ObjQues.Status == 1
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            //dtQuestions.Columns.Add("QuestionId");
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            dtQuestions.Columns.Add("QuestionId");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                //drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["QuestionFileName"] = "";
                drQuestions["QuestionFileNameSub1"] = "";
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwQues.DataSource = ds.Tables[0];
            gvwQues.DataBind();
        }
    }

    private void FillImageTypeMemQuestions(int sectionid)
    {

        var ObjQues1 = from ObjQues in dataclass.MemmoryTestImageQuesCollections
                       where ObjQues.SectionId == sectionid //&& ObjQues.Status == 1
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            dtQuestions.Columns.Add("QuestionId");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["QuestionFileName"] = "";
                drQuestions["QuestionFileNameSub1"] = "";
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwQues.DataSource = ds.Tables[0];
            gvwQues.DataBind();
        }
    }

    private void FillObjectiveQuestions(int sectionid)
    {
        var ObjQues1 = from ObjQues in dataclass.QuestionCollections
                       where ObjQues.Category == "Objective" && ObjQues.SectionId == sectionid //&& ObjQues.Status == 1
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            dtQuestions.Columns.Add("QuestionId");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["QuestionFileName"] = "";
                drQuestions["QuestionFileNameSub1"] = "";
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();

                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwQues.DataSource = ds.Tables[0];
            gvwQues.DataBind();
        }
    }

    private void FillFillBlanksQuestions(int sectionid)
    {

        var ObjQues1 = from ObjQues in dataclass.QuestionCollections
                       where ObjQues.Category == "FillBlanks" && ObjQues.SectionId == sectionid //&& ObjQues.Status == 1
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            dtQuestions.Columns.Add("QuestionId");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["QuestionFileName"] = "";
                drQuestions["QuestionFileNameSub1"] = "";
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwQues.DataSource = ds.Tables[0];
            gvwQues.DataBind();
        }
    }

    private void FillRatingTypeQuestions(int sectionid)
    {

        var ObjQues1 = from ObjQues in dataclass.QuestionCollections
                       where ObjQues.Category == "RatingType" && ObjQues.SectionId == sectionid //&& ObjQues.Status == 1
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            dtQuestions.Columns.Add("QuestionId");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["QuestionFileName"] = "";
                drQuestions["QuestionFileNameSub1"] = "";
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwQues.DataSource = ds.Tables[0];
            gvwQues.DataBind();
        }
    }

    private void FillImageTypeQuestions(int sectionid)
    {
        var ObjQues1 = from ObjQues in dataclass.QuestionCollections
                       where ObjQues.Category == "ImageType" && ObjQues.SectionId == sectionid //&& ObjQues.Status == 1
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            dtQuestions.Columns.Add("QuestionId");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                if (objquestions.QuestionFileName != null)
                    drQuestions["QuestionFileName"] = objquestions.QuestionFileName.ToString();
                else drQuestions["QuestionFileName"] = "";
                if (objquestions.QuestionFileNameSub1 != null)
                    drQuestions["QuestionFileNameSub1"] = objquestions.QuestionFileNameSub1.ToString();
                else drQuestions["QuestionFileNameSub1"] = "";
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwQues.DataSource = ds.Tables[0];
            gvwQues.DataBind();
        }
    }
    private void FillVideoTypeQuestions(int sectionid)
    {
        var ObjQues1 = from ObjQues in dataclass.QuestionCollections
                       where ObjQues.Category == "VideoType" && ObjQues.SectionId == sectionid //&& ObjQues.Status == 1
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            dtQuestions.Columns.Add("QuestionId");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["QuestionFileName"] = objquestions.QuestionFileName.ToString();
                drQuestions["QuestionFileNameSub1"] = "";
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwQues.DataSource = ds.Tables[0];
            gvwQues.DataBind();
        }
    }

    private void FillAudioTypeQuestions(int sectionid)
    {
        var ObjQues1 = from ObjQues in dataclass.QuestionCollections
                       where ObjQues.Category == "AudioType" && ObjQues.SectionId == sectionid //&& ObjQues.Status==1
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            dtQuestions.Columns.Add("QuestionId");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["QuestionFileName"] = objquestions.QuestionFileName.ToString();
                drQuestions["QuestionFileNameSub1"] = "";
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwQues.DataSource = ds.Tables[0];
            gvwQues.DataBind();
        }
    }

    private void FillPhotoTypeQuestions(int sectionid)
    {
        var ObjQues1 = from ObjQues in dataclass.QuestionCollections
                       where ObjQues.Category == "PhotoType" && ObjQues.SectionId == sectionid //&& ObjQues.Status == 1
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            dtQuestions.Columns.Add("QuestionId");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                if (objquestions.QuestionFileName != null)
                    drQuestions["QuestionFileName"] = objquestions.QuestionFileName.ToString();
                else drQuestions["QuestionFileName"] = "";
                if (objquestions.QuestionFileNameSub1 != null)
                    drQuestions["QuestionFileNameSub1"] = objquestions.QuestionFileNameSub1.ToString();
                else drQuestions["QuestionFileNameSub1"] = "";
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwQues.DataSource = ds.Tables[0];
            gvwQues.DataBind();
        }
    }

    private void Bind()
    {
        int sectionid = 0;
        if (ddlSecondLevelList.SelectedIndex > 0)
            sectionid = int.Parse(ddlSecondLevelList.SelectedValue);
        else if (ddlFirstLevelList.SelectedIndex > 0)
            sectionid = int.Parse(ddlFirstLevelList.SelectedValue);
        else if (ddlSectionList.SelectedIndex > 0)
            sectionid = int.Parse(ddlSectionList.SelectedValue);

        gvwQues.DataSource = "";
        gvwQues.DataBind();

       // lblMessage.Text = "secid=:" + sectionid.ToString();

       // "MemTestWords" "MemTestImages"

        if (ddlQuestionType.SelectedIndex > 0)
        {
            Session["chkassn"] = "YES";
            if (ddlQuestionType.SelectedValue == "MemTestWords")
            {
                FillWordTypeMemQuestions(sectionid);
            }
            else if (ddlQuestionType.SelectedValue == "MemTestImages")
            {
                FillImageTypeMemQuestions(sectionid);
            }
            else if (ddlQuestionType.SelectedValue == "Objective")
            {
                FillObjectiveQuestions(sectionid);               
            }
            else if (ddlQuestionType.SelectedValue == "FillBlanks")
            {
                FillFillBlanksQuestions(sectionid);
            }
            else if (ddlQuestionType.SelectedValue == "RatingType")
            {
                FillRatingTypeQuestions(sectionid);               
            }
               // ImageType VideoType AudioType
            else if (ddlQuestionType.SelectedValue == "ImageType")
            {
                FillImageTypeQuestions(sectionid);
            }

            else if (ddlQuestionType.SelectedValue == "VideoType")
            {
                FillVideoTypeQuestions(sectionid);
            }

            else if (ddlQuestionType.SelectedValue == "AudioType")
            {
                FillAudioTypeQuestions(sectionid);               
            }
            else if (ddlQuestionType.SelectedValue == "PhotoType")
            {
                FillPhotoTypeQuestions(sectionid);
            }
            
        }
        BindGrid(sectionid);

        FillResultBandDetails();

    }
    private void FillResultBandDetails()
    {
        
        gvwVariableBands.DataSource = "";
        gvwVariableBands.DataBind();
        int sectionid = 0;
        //if (ddlSecondLevelList.SelectedIndex > 0)
        //    sectionid = int.Parse(ddlSecondLevelList.SelectedValue);
        //else if (ddlFirstLevelList.SelectedIndex > 0)
        //    sectionid = int.Parse(ddlFirstLevelList.SelectedValue);
        //else  // 141109 bip

        if (ddlSectionList.SelectedIndex > 0)
            sectionid = int.Parse(ddlSectionList.SelectedValue);

        if (ddlTestName.SelectedIndex > 0 && ddlTestSectionName.SelectedIndex > 0 && sectionid > 0)
        {/*
            LinqVariableBandDetails.Where = " TestId= " + ddlTestName.SelectedValue + " and VariableId= " + sectionid + " and TestSectionId=" + ddlTestSectionName.SelectedValue;
            gvwVariableBands.DataSource = LinqVariableBandDetails;
            gvwVariableBands.DataBind();*/


            var ObjTestSecBandDetails = from ObjTestSecBandDet in dataclass.TestVariableResultBands
                                        where ObjTestSecBandDet.TestId == int.Parse(ddlTestName.SelectedValue) && ObjTestSecBandDet.VariableId == sectionid && ObjTestSecBandDet.TestSectionId == int.Parse(ddlTestSectionName.SelectedValue)
                                        select ObjTestSecBandDet;
            if (ObjTestSecBandDetails.Count() > 0)
            {
                DataTable dtTestSecBandDetails = new DataTable();
                dtTestSecBandDetails.Columns.Add("MarkFrom");
                dtTestSecBandDetails.Columns.Add("MarkTo");
                dtTestSecBandDetails.Columns.Add("DisplayName");
                dtTestSecBandDetails.Columns.Add("BenchMark");
                dtTestSecBandDetails.Columns.Add("VariableBandId");
                dtTestSecBandDetails.Columns.Add("TestId");
                dtTestSecBandDetails.Columns.Add("VariableId");
                dtTestSecBandDetails.Columns.Add("TestSectionId");
                DataRow drTestSecBandDetails;
                string bandDescription = "";
                foreach (var objTestSecBandDet in ObjTestSecBandDetails)
                {
                    drTestSecBandDetails = dtTestSecBandDetails.NewRow();
                    bandDescription = ClearHTMLTags(objTestSecBandDet.DisplayName.ToString());
                    drTestSecBandDetails["MarkFrom"] = objTestSecBandDet.MarkFrom.ToString();
                    drTestSecBandDetails["MarkTo"] = objTestSecBandDet.MarkTo.ToString();
                    drTestSecBandDetails["DisplayName"] = bandDescription;
                    drTestSecBandDetails["BenchMark"] = objTestSecBandDet.BenchMark.ToString();
                    drTestSecBandDetails["VariableBandId"] = objTestSecBandDet.VariableBandId.ToString();
                    drTestSecBandDetails["TestId"] = objTestSecBandDet.TestId.ToString();
                    drTestSecBandDetails["VariableId"] = objTestSecBandDet.VariableId.ToString();
                    drTestSecBandDetails["TestSectionId"] = objTestSecBandDet.TestSectionId.ToString();
                    dtTestSecBandDetails.Rows.Add(drTestSecBandDetails);
                }
                DataSet ds = new DataSet();
                ds.Tables.Add(dtTestSecBandDetails);
                gvwVariableBands.DataSource = ds.Tables[0];
                gvwVariableBands.DataBind();
                
                FillSelectedBandDetails();
            }
        }
    }

    private void FillSelectedBandDetails()
    {
        if (Session["variableBandId"] != null)
        {
            var ObjTestSecBandDetails = from ObjTestSecBandDet in dataclass.TestVariableResultBands
                                        where ObjTestSecBandDet.VariableBandId == int.Parse(Session["variableBandId"].ToString())
                                        select ObjTestSecBandDet;
            if (ObjTestSecBandDetails.Count() > 0)
            {
                txtVariableMarksFrom.Text = ObjTestSecBandDetails.First().MarkFrom.ToString();
                txtVariableMarksTo.Text = ObjTestSecBandDetails.First().MarkTo.ToString();
                txtVariableDisplayName.Text = ObjTestSecBandDetails.First().DisplayName.ToString();
                txtVariableBenchMark.Text = ObjTestSecBandDetails.First().BenchMark.ToString();

            }
        }
    }

    private void BindGrid(int sectionid)
    {
        testid = int.Parse(ddlTestName.SelectedValue);
        int testsectionid = int.Parse(ddlTestSectionName.SelectedValue);
        //int sectionid = 0;
        //if (ddlSecondLevelList.SelectedIndex > 0)
        //    sectionid = int.Parse(ddlSecondLevelList.SelectedValue);
        //else if (ddlFirstLevelList.SelectedIndex > 0)
        //    sectionid = int.Parse(ddlFirstLevelList.SelectedValue);
        //else if (ddlSectionList.SelectedIndex > 0)
        //    sectionid = int.Parse(ddlSectionList.SelectedValue);

        if (testid > 0)
        {
            int quesid = 0;
            int i = 0;
            if (gvwQues.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvwQues.Rows)
                {
                    CheckBox cb = new CheckBox();
                    cb = (CheckBox)gr.Controls[0].FindControl("CheckBox1");
                    quesid = int.Parse(gvwQues.Rows[i].Cells[6].Text);
                    var Status1 = from status123 in dataclass.TestBaseQuestionLists
                                  where status123.QuestionId == quesid && status123.TestId == testid && status123.Status == 1 && status123.TestSectionId==testsectionid && status123.SectionId==sectionid
                                  select status123;
                    if (Status1.Count() > 0)
                    {
                        cb.Checked = true;
                    }

                    i++;
                }
            }

          //  FillTestsectionList();
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
         testid = int.Parse(ddlTestName.SelectedValue);
        if(testid<=0){ lblMessage.Text = "Please Select a Test"; return;}
        int testsectionid = 0;
        if (ddlTestSectionName.SelectedIndex > 0)
            testsectionid = int.Parse(ddlTestSectionName.SelectedValue);
        else { lblMessage.Text = "Please Select a Test section"; return; }
        if (gvwQues.Rows.Count <= 0)
        { lblMessage.Text = "No values found"; return; }

        int sectionid = 0; string condition = "";
        if (ddlSecondLevelList.SelectedIndex > 0)
        sectionid = int.Parse(ddlSecondLevelList.SelectedValue);
        else if (ddlFirstLevelList.SelectedIndex > 0)
            sectionid = int.Parse(ddlFirstLevelList.SelectedValue);
        else if (ddlSectionList.SelectedIndex > 0)
            sectionid = int.Parse(ddlSectionList.SelectedValue);

        if(sectionid==0)
        { lblMessage.Text = "Please select a Section"; return; }

        int firstsectionid = 0, secondsectionid = 0, thirdsectionid = 0;
        if (ddlSectionList.SelectedIndex > 0)
        { firstsectionid = int.Parse(ddlSectionList.SelectedValue);}// condition = " and SectionName= " + ddlSectionList.SelectedItem.Text; }
        if (ddlFirstLevelList.SelectedIndex > 0)
        { secondsectionid = int.Parse(ddlFirstLevelList.SelectedValue);}// condition = " and SectionNameSub1= " + ddlFirstLevelList.SelectedItem.Text; }
        if (ddlSecondLevelList.SelectedIndex > 0)
        {thirdsectionid = int.Parse(ddlSecondLevelList.SelectedValue);}//condition= " and SectionNameSub2= " + ddlSecondLevelList.SelectedItem.Text;}
       
        int createdby = 0;
        int quesid = 0;
        int i = 0;
        if (Session["UserID"] != null)
        {
            createdby = int.Parse(Session["UserID"].ToString());
        }
        string filename = "";
        int status = 0;//bool itemchecked=false;
        foreach (GridViewRow gr in gvwQues.Rows)
        {
            quesid = int.Parse(gvwQues.Rows[i].Cells[6].Text);           
            CheckBox cb = new CheckBox();
            cb = (CheckBox)gr.Controls[0].FindControl("CheckBox1");
            if (cb.Checked)
            {
                status = 1; //itemchecked = true;
            }
            else status = 0;
            //dataclass.UpdateQuesStatus(int.Parse(gvwQues.Rows[i].Cells[1].Text), status, createdby);
            dataclass.Procedure_TestBaseQuestionList(0, testid, quesid, status, createdby, sectionid,testsectionid,firstsectionid,secondsectionid,thirdsectionid);
            i = i + 1;
          lblMessage.Text = "saved Successfully";
        }
        Session["chkassn"] = null;

    }

    protected void ddlSectionList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSectionList.SelectedIndex > 0)
        {
            Session["sectionIndex"] = ddlSectionList.SelectedIndex.ToString();
            Session["SubLevel1Index"] = null; FillSubLevel1Sections(); FillResultBandDetails();
        }
        else
        {
            Session["sectionIndex"] = null; Session["SubLevel1Index"] = null;
            ListItem listname;
            listname = new ListItem("-- select --", "0");
            ddlFirstLevelList.Items.Clear();
            ddlFirstLevelList.Items.Add(listname);
            listname = new ListItem("-- select --", "0");
            ddlSecondLevelList.Items.Clear();
            ddlSecondLevelList.Items.Add(listname);
        }
    }
    protected void ddlFirstLevelList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFirstLevelList.SelectedIndex > 0)
        {
            Session["SubLevel1Index"] = ddlFirstLevelList.SelectedIndex.ToString();
            FillSubLevel2Sections();
        }
        else
        {
            Session["SubLevel1Index"] = null;
            ListItem listname;
            listname = new ListItem("-- select --", "0");
            ddlSecondLevelList.Items.Clear();
            ddlSecondLevelList.Items.Add(listname);
        }
    }
    protected void ddlSecondLevelList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSecondLevelList.SelectedIndex > 0)
            Session["SubLevel2Index"] = ddlSecondLevelList.SelectedIndex.ToString();
        else Session["SubLevel2Index"] = null;
        Bind();
    }
    protected void gvwQues_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageindex = e.NewPageIndex;
        gvwQues.PageIndex = pageindex;
    }
    protected void ddlOrganizationList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganizationList.SelectedIndex > 0)
        {
            Session["OrgIndex_Test"] = ddlOrganizationList.SelectedIndex.ToString();
            FillTestList();
        }
        else Session["OrgIndex_Test"] = null;
    }
    private void FillOrganizationList()
    {
        ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlOrganizationList.Items.Clear();
        ddlOrganizationList.Items.Add(listnew);
        int orgIndex = 0;

        if (Session["OrgIndex_Test"] != null)
        {
            orgIndex = int.Parse(Session["OrgIndex_Test"].ToString());
        }
        LinqOrganizationList.Where = "Status=1 && AdminAccess=1";
        ddlOrganizationList.DataSource = LinqOrganizationList;
        ddlOrganizationList.DataTextField = "Name";
        ddlOrganizationList.DataValueField = "OrganizationID";
        ddlOrganizationList.DataBind();
        if (orgIndex > 0)
        { ddlOrganizationList.SelectedIndex = orgIndex; FillTestList(); }
    }

    private void FillTestList()
    {
        ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlTestName.Items.Clear();
        ddlTestName.Items.Add(listnew);
        int testIndex = 0; int adminaccess = 0;

        if (Session["TestIndex_Test"] != null)
        {
            testIndex = int.Parse(Session["TestIndex_Test"].ToString());
        }
        if (specialadmin == false)
        { organizationName = ddlOrganizationList.SelectedItem.Text; adminaccess = 1; }

        var testListDetails = from testlistsdet in dataclass.TestLists
                              where testlistsdet.OrganizationName == organizationName && testlistsdet.AdminAccess == adminaccess
                              select testlistsdet;

        //LinqTestList.Where="OrganizationName=" + ddlOrganizationList.SelectedItem.Text;
        //ddlTestName.DataSource = LinqTestList;
        ddlTestName.DataSource = testListDetails;
        ddlTestName.DataTextField = "TestName";
        ddlTestName.DataValueField = "TestId";
        ddlTestName.DataBind();
        if (testIndex > 0)
        { ddlTestName.SelectedIndex = testIndex; FillTestsectionList(); } //BindGrid();} // bip 220110

    }

    protected void ddlTestName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestName.SelectedIndex > 0)
        { Session["TestIndex_Test"] = ddlTestName.SelectedIndex.ToString(); FillTestsectionList(); }//BindGrid();  }//  bip 220110
        else Session["TestIndex_Test"] = null;
    }
    private bool CheckMark(string mark)
    {
        try
        {
            int passmark = 0;
            passmark = int.Parse(mark);
            return true;
        }
        catch (Exception ex) { lblMessageSectionBand.Text = "Enter valid Mark"; return false; }
    }
    protected void btnAddVariableBands_Click(object sender, EventArgs e)
    {
        int sectionid = 0;
        //if (ddlSecondLevelList.SelectedIndex > 0)
        //    sectionid = int.Parse(ddlSecondLevelList.SelectedValue);
        //else if (ddlFirstLevelList.SelectedIndex > 0)
        //    sectionid = int.Parse(ddlFirstLevelList.SelectedValue);
        //else  // 141109 bip
            if (ddlSectionList.SelectedIndex > 0)
            sectionid = int.Parse(ddlSectionList.SelectedValue);

        if (sectionid == 0)
        { lblMessage.Text = "Please select a Section"; return; }

        if (txtVariableBenchMark.Text.Trim() == "" || txtVariableDisplayName.Text.Trim() == "" || txtVariableMarksFrom.Text.Trim() == "" || txtVariableMarksTo.Text.Trim() == "")
        { lblMessageSectionBand.Text = "Please enter all band details"; return; }

        if (CheckMark(txtVariableMarksFrom.Text.Trim()) == false) return;
        else if (CheckMark(txtVariableMarksTo.Text.Trim()) == false) return;
        else if (CheckMark(txtVariableBenchMark.Text.Trim()) == false) return;

        int variableBandId = 0;
        if (Session["variableBandId"] != null)
            variableBandId = int.Parse(Session["variableBandId"].ToString());

        int markfrom = 0, markto = 0, benchmark = 0;
        markfrom = int.Parse(txtVariableMarksFrom.Text.Trim());
        markto = int.Parse(txtVariableMarksTo.Text.Trim());
        benchmark = int.Parse(txtVariableBenchMark.Text.Trim());
        if (ddlTestName.SelectedIndex > 0)
            testid = int.Parse(ddlTestName.SelectedValue);
        else { lblMessageSectionBand.Text = "Please select a test"; return; }

        int testsectionid=0;
        if (ddlTestSectionName.SelectedIndex > 0)
            testsectionid = int.Parse(ddlTestSectionName.SelectedValue);
        else { lblMessageSectionBand.Text = "Please select a test section"; return; }


        int userid = int.Parse(Session["UserID"].ToString());

        dataclass.Procedure_TestVariableResultBands(variableBandId, testid, sectionid,testsectionid, benchmark, markfrom, markto, txtVariableDisplayName.Text, "", 1, userid);
        txtVariableBenchMark.Text = ""; txtVariableDisplayName.Text = ""; txtVariableMarksFrom.Text = ""; txtVariableMarksTo.Text = "";
        Session["variableBandId"] = null;
        lblMessageSectionBand.Text = "Score band details saved successfully";
        FillResultBandDetails();
    }
    protected void btnResetVariableBands_Click(object sender, EventArgs e)
    {
        txtVariableBenchMark.Text = ""; txtVariableDisplayName.Text = ""; txtVariableMarksFrom.Text = ""; txtVariableMarksTo.Text = "";
        Session["variableBandId"] = null; //FillResultBandDetails();
    }
    protected void btnDeleteVariableBands_Click(object sender, EventArgs e)
    {
        if (Session["variableBandId"] != null)
        {
            int variablebandid = 0;
            variablebandid = int.Parse(Session["variableBandId"].ToString());
            dataclass.Procedure_DeleteTestVariableResultBands(variablebandid);
            txtVariableBenchMark.Text = ""; txtVariableDisplayName.Text = ""; txtVariableMarksFrom.Text = ""; txtVariableMarksTo.Text = "";
            Session["variableBandId"] = null;
            lblMessageSectionBand.Text = "Score band deleted successfully";
            FillResultBandDetails();
        }
        else lblMessageSectionBand.Text = "No etry selected for deletion.";
    }
    protected void gvwVariableBands_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["variableBandId"] = gvwVariableBands.SelectedRow.Cells[5].Text;
        FillSelectedBandDetails();
        /*
        txtVariableMarksFrom.Text = gvwVariableBands.SelectedRow.Cells[1].Text;
        txtVariableMarksTo.Text = gvwVariableBands.SelectedRow.Cells[2].Text;
        txtVariableDisplayName.Text = gvwVariableBands.SelectedRow.Cells[3].Text;
        txtVariableBenchMark.Text = gvwVariableBands.SelectedRow.Cells[4].Text;
         * */
    }
    protected void ddlTestSectionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestSectionName.SelectedIndex > 0)
        {
            Session["TestsectionIndex"] = ddlTestSectionName.SelectedIndex.ToString(); FillResultBandDetails(); //BindGrid();
        }
        else Session["TestsectionIndex"] = null;
    }
    private void FillTestsectionList()
    {
        ListItem litem = new ListItem("-- Select Test Section from the List --", "0");
        ddlTestSectionName.Items.Clear();
        ddlTestSectionName.Items.Add(litem);
        if (ddlTestName.SelectedIndex > 0)
        {
            LinqTestSectionList.Where = "TestId=" + ddlTestName.SelectedValue;
            ddlTestSectionName.DataSource = LinqTestSectionList;
            ddlTestSectionName.DataTextField = "SectionName";
            ddlTestSectionName.DataValueField = "TestSectionId";
            ddlTestSectionName.DataBind();
            if (Session["TestsectionIndex"] != null)
                ddlTestSectionName.SelectedIndex = int.Parse(Session["TestsectionIndex"].ToString());
        }
    }
    protected void gvwQues_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[2].Text != null && e.Row.Cells[2].Text != "")
        {
            if (e.Row.Cells[2].Text.Length > 100)
            {
                string txt = e.Row.Cells[2].Text;
                txt = txt.Substring(0, 100);
                e.Row.Cells[2].ToolTip = e.Row.Cells[2].Text;
                e.Row.Cells[2].Text = txt + " ...";
            }
        }
    }
    protected void ddlSectionCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSectionCategory.SelectedIndex > 0)
        {
            // txtSectionName.Visible = true;
            if (ddlSectionCategory.SelectedValue != "0")
            {
                if (Session["sectionCategoryIndex"] != null)
                    if (ddlSectionCategory.SelectedValue != Session["sectionCategoryIndex"].ToString())
                    {
                        Session["sectionIndex"] = null; Session["SubLevel1Index"] = null;
                        ListItem listname;
                        listname = new ListItem("-- select --", "0");
                        ddlFirstLevelList.Items.Clear();
                        ddlFirstLevelList.Items.Add(listname);
                        listname = new ListItem("-- select --", "0");
                        ddlSecondLevelList.Items.Clear();
                        ddlSecondLevelList.Items.Add(listname);
                    }
                Session["sectionCategoryIndex"] = ddlSectionCategory.SelectedIndex;
                int sectionCategoryId = int.Parse(ddlSectionCategory.SelectedValue);
                FillSessionslist(sectionCategoryId);
            }
            else
            {
                Session["sectionIndex"] = null; Session["SubLevel1Index"] = null;
                ListItem listname;
                listname = new ListItem("-- select --", "0");
                ddlFirstLevelList.Items.Clear();
                ddlFirstLevelList.Items.Add(listname);
                listname = new ListItem("-- select --", "0");
                ddlSecondLevelList.Items.Clear();
                ddlSecondLevelList.Items.Add(listname);
            }
        }
        else
        {
            Session["sectionCategoryIndex"] = null;
            Session["sectionIndex"] = null; Session["SubLevel1Index"] = null;
            ListItem listname;
            listname = new ListItem("-- select --", "0");
            ddlFirstLevelList.Items.Clear();
            ddlFirstLevelList.Items.Add(listname);
            listname = new ListItem("-- select --", "0");
            ddlSecondLevelList.Items.Clear();
            ddlSecondLevelList.Items.Add(listname);
        }
    }
}
