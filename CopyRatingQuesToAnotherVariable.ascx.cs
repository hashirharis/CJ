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

public partial class CopyRatingQuesToAnotherVariable : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    int userid = 0;

    bool specialadmin = false;
    int OrganizationID = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {            
            specialadmin = true; OrganizationID = int.Parse(Session["AdminOrganizationID"].ToString());            
        }

        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());
        FillSessionslist();FillSessionslist_copy();
        FillRatingTypeQuestions();
    }

    private void FillRatingTypeQuestions()
    {
        gvwQuestionBank.DataSource = null; gvwQuestionBank.DataBind();
        gvwQuestionBank.Dispose();
        
        int sectionid = 0;
        if (ddlSecondLevelList.SelectedIndex > 0)
            sectionid = int.Parse(ddlSecondLevelList.SelectedValue);
        else if (ddlFirstLevelList.SelectedIndex > 0)
            sectionid = int.Parse(ddlFirstLevelList.SelectedValue);
        else if (ddlSectionList.SelectedIndex > 0)
            sectionid = int.Parse(ddlSectionList.SelectedValue);

        var ObjQues1 = from ObjQues in dataclass.QuestionCollections
                       where ObjQues.Category == "RatingType" && ObjQues.SectionId == sectionid
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
           // dtQuestions.Columns.Add("SectionName");
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionId");
            dtQuestions.Columns.Add("ScoringStyle");
            //dtQuestions.Columns.Add("QuestionFileNameSub1");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();

                //drQuestions["SectionName"] = objquestions.SectionName.ToString();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                drQuestions["ScoringStyle"] = objquestions.ScoringStyle.ToString();
                //drQuestions["QuestionFileName"] = "";
               // drQuestions["QuestionFileNameSub1"] = "";
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwQuestionBank.DataSource = ds.Tables[0];
            gvwQuestionBank.DataBind();
        }
        else gvwQuestionBank.Dispose();
    }

    private void FillSessionslist()
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
            int adminaccess = 1;
            if (specialadmin == true)
                adminaccess = 0;

            var details1 = from details in dataclass.SectionDetails
                           where details.ParentId == 0 && details.AdminAccess==adminaccess && details.OrganizationId==OrganizationID
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
                Session["SubLevel1Index"] = null;
                Session["SubLevel2Index"] = null;
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
                       select details;
        if (details1.Count() > 0)
        {
            ddlFirstLevelList.DataSource = details1;
            ddlFirstLevelList.DataTextField = "SectionName";
            ddlFirstLevelList.DataValueField = "SectionId";
            ddlFirstLevelList.DataBind();
        }
                
        if (parentindex > 0)
        {ddlFirstLevelList.SelectedIndex = parentindex;
            FillSubLevel2Sections();
        }
        else
        {
            Session["SubLevel2Index"] = null;
            listname = new ListItem("-- select --", "0");
            ddlSecondLevelList.Items.Clear();
            ddlSecondLevelList.Items.Add(listname);

            FillRatingTypeQuestions();
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
        }
        

        FillRatingTypeQuestions();
    }

    private bool CheckChildExists(int parentid)
    {
        var details1 = from details in dataclass.SectionDetails
                       where details.ParentId == parentid
                       select details;
        if (details1.Count() > 0)
            return true;

        else return false;

    }
    private bool validateSelection()
    {
        bool variableselected = true;
        if (ddlSectionList0.SelectedIndex <= 0 && ddlFirstLevelList0.SelectedIndex <= 0 && ddlSecondLevelList0.SelectedIndex <= 0)
        { lblMessage.Text = "Please select a section variable from the list"; variableselected = false; }
        else
        {
            if (ddlSectionList0.SelectedIndex > 0)
            {
                if (ddlFirstLevelList0.SelectedIndex > 0)
                {
                    if (ddlSecondLevelList0.SelectedIndex <= 0)
                    {
                        if (CheckChildExists(int.Parse(ddlFirstLevelList0.SelectedValue)) == true)
                        {
                            variableselected = false;
                            lblMessage.Text = "Please select a 2nd level sub variable from the list";
                        }
                    }
                }
                else
                {// code to check wheather there is a sublevel entries under this selected value.

                    if (CheckChildExists(int.Parse(ddlSectionList0.SelectedValue)) == true)
                    {
                        variableselected = false;
                        lblMessage.Text = "Please select a 1st level sub variable from the list";
                    }
                }
            }
            else { lblMessage.Text = "Please select a section from the list"; variableselected = false; }
        }
        return variableselected;
    }


    protected void gvwQuestionBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwQuestionBank.PageIndex = e.NewPageIndex;
        gvwQuestionBank.DataBind();
    }
    protected void ddlSectionList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSectionList.SelectedIndex > 0)
        {
            Session["sectionIndex"] = ddlSectionList.SelectedIndex.ToString();
            Session["SubLevel1Index"] = null; FillSubLevel1Sections();
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

            FillRatingTypeQuestions();
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

            FillRatingTypeQuestions();
        }
    }
    protected void ddlSecondLevelList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSecondLevelList.SelectedIndex > 0)
            Session["SubLevel2Index"] = ddlSecondLevelList.SelectedIndex.ToString();
        else Session["SubLevel2Index"] = null;
        FillRatingTypeQuestions();
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (validateSelection() == false) return;

        bool saved = false;
        int i = 0;
        int questionid = 0;
        foreach (GridViewRow gr in gvwQuestionBank.Rows)
        {
            questionid = int.Parse(gvwQuestionBank.Rows[i].Cells[1].Text);
            CheckBox cb = new CheckBox();
            cb = (CheckBox)gr.Controls[0].FindControl("CheckBox1");
            if (cb.Checked)
            {
                InsertQuestionDetails(questionid);
                saved = true;
            }

            i = i + 1;

        }
        if (saved == true)
            lblMessage.Text = "saved Successfully";
        else lblMessage.Text = "no item selected";
    }
    private void InsertQuestionDetails(int questionid)
    {
        int sectionid = 0;
        if (ddlSecondLevelList0.SelectedIndex > 0)
            sectionid = int.Parse(ddlSecondLevelList0.SelectedValue);
        else if (ddlFirstLevelList0.SelectedIndex > 0)
            sectionid = int.Parse(ddlFirstLevelList0.SelectedValue);
        else if (ddlSectionList0.SelectedIndex > 0)
            sectionid = int.Parse(ddlSectionList0.SelectedValue);

        string sectionname = "", subsection1 = "", subsection2 = "", category = "RatingType";
        if (ddlSectionList0.SelectedIndex > 0)
            sectionname = ddlSectionList0.SelectedItem.Text;
        if (ddlFirstLevelList0.SelectedIndex > 0)
            subsection1 = ddlFirstLevelList0.SelectedItem.Text;
        if (ddlSecondLevelList0.SelectedIndex > 0)
            subsection2 = ddlSecondLevelList0.SelectedItem.Text;


        string question="", Answer="", option1="", option2="", option3="", option4="", option5="", option6="", option7="", option8="", option9="", option10 = "";
        string ratingscale, questionCode = ""; ;
        ratingscale = ddlScoringStyle.SelectedValue;
        var questiondetails = from questiondet in dataclass.QuestionCollections
                              where questiondet.QuestionID == questionid
                              select questiondet;
        if (questiondetails.Count() > 0)
        {
            if (questiondetails.First().Question != null)
                question = questiondetails.First().Question.ToString();
            if (questiondetails.First().Answer != null)
                Answer = questiondetails.First().Answer.ToString();
            if (questiondetails.First().Option1 != null)
                option1 = questiondetails.First().Option1.ToString();
            if (questiondetails.First().Option2 != null)
                option2 = questiondetails.First().Option2.ToString();
            if (questiondetails.First().Option3 != null)
                option3 = questiondetails.First().Option3.ToString();
            if (questiondetails.First().Option4 != null)
                option4 = questiondetails.First().Option4.ToString();
            if (questiondetails.First().Option5 != null)
                option5 = questiondetails.First().Option5.ToString();
            if (questiondetails.First().Option6 != null)
                option6 = questiondetails.First().Option6.ToString();
            if (questiondetails.First().Option7 != null)
                option7 = questiondetails.First().Option7.ToString();
            if (questiondetails.First().Option8 != null)
                option8 = questiondetails.First().Option8.ToString();
            if (questiondetails.First().Option9 != null)
                option9 = questiondetails.First().Option9.ToString();
            if (questiondetails.First().Option10 != null)
                option10 = questiondetails.First().Option10.ToString();
            if (questiondetails.First().QuestionCode != null)
                questionCode = questiondetails.First().QuestionCode.ToString()+"_cp";



            dataclass.Procedure_AddQuestions(0, sectionid, sectionname, subsection1, subsection2, category, question, Answer,
                           option1, option2, option3, option4, option5, option6, option7, option8, option9, option10, "", "", "", "", "", 0, userid, ratingscale, "", "", questionCode);


        }

    }


   

    private void FillSessionslist_copy()
    {
        try
        {
            ListItem listname;
            listname = new ListItem("-- select --", "0");
            ddlSectionList0.Items.Clear();
            ddlSectionList0.Items.Add(listname);
            int sectionIndex = 0;

            if (Session["sectionIndex_0"] != null)
                sectionIndex = int.Parse(Session["sectionIndex_0"].ToString());

            int adminaccess = 1;
            if (specialadmin == true)
                adminaccess = 0;

            var details1 = from details in dataclass.SectionDetails
                           where details.ParentId == 0 && details.AdminAccess==adminaccess && details.OrganizationId==OrganizationID
                           select details;
            if (details1.Count() > 0)
            {
                ddlSectionList0.DataSource = details1;
                ddlSectionList0.DataTextField = "SectionName";
                ddlSectionList0.DataValueField = "SectionId";
                ddlSectionList0.DataBind();
            }
            
            if (sectionIndex > 0)
            {ddlSectionList0.SelectedIndex = sectionIndex;
                FillSubLevel1Sections_copy();
            }
            else
            {
                Session["SubLevel1Index_0"] = null;
                Session["SubLevel2Index_0"] = null;
                listname = new ListItem("-- select --", "0");
                ddlFirstLevelList0.Items.Clear();
                ddlFirstLevelList0.Items.Add(listname);
                listname = new ListItem("-- select --", "0");
                ddlSecondLevelList0.Items.Clear();
                ddlSecondLevelList0.Items.Add(listname);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void FillSubLevel1Sections_copy()
    {
        ListItem listname;

        listname = new ListItem("-- select --", "0");
        ddlFirstLevelList0.Items.Clear();
        ddlFirstLevelList0.Items.Add(listname);
        int parentindex = 0;
        if (Session["SubLevel1Index_0"] != null)
            parentindex = int.Parse(Session["SubLevel1Index_0"].ToString());

        var details1 = from details in dataclass.SectionDetails
                       where details.ParentId == int.Parse(ddlSectionList0.SelectedValue)
                       select details;
        if (details1.Count() > 0)
        {
            ddlFirstLevelList0.DataSource = details1;
            ddlFirstLevelList0.DataTextField = "SectionName";
            ddlFirstLevelList0.DataValueField = "SectionId";
            ddlFirstLevelList0.DataBind();
        }

       
        if (parentindex > 0)
        { ddlFirstLevelList0.SelectedIndex = parentindex;
            FillSubLevel2Sections_copy();
        }
        else
        {
            Session["SubLevel2Index_0"] = null;
            listname = new ListItem("-- select --", "0");
            ddlSecondLevelList0.Items.Clear();
            ddlSecondLevelList0.Items.Add(listname);
        }
    }

    private void FillSubLevel2Sections_copy()
    {
        ListItem listname;
        listname = new ListItem("-- select --", "0");
        ddlSecondLevelList0.Items.Clear();
        ddlSecondLevelList0.Items.Add(listname);

        int subindex = 0;
        if (Session["SubLevel2Index_0"] != null)
            subindex = int.Parse(Session["SubLevel2Index_0"].ToString());

        var details1 = from details in dataclass.SectionDetails
                       where details.ParentId == int.Parse(ddlFirstLevelList0.SelectedValue)
                       select details;
        if (details1.Count() > 0)
        {
            ddlSecondLevelList0.DataSource = details1;
            ddlSecondLevelList0.DataTextField = "SectionName";
            ddlSecondLevelList0.DataValueField = "SectionId";
            ddlSecondLevelList0.DataBind();
        }

        //if (subindex > 0)
        //{
        ddlSecondLevelList0.SelectedIndex = subindex;
        //}else
    }



    protected void ddlSectionList0_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSectionList0.SelectedIndex > 0)
        {
            Session["sectionIndex_0"] = ddlSectionList0.SelectedIndex.ToString();
            Session["SubLevel1Index_0"] = null; FillSubLevel1Sections_copy();
        }
        else
        {
            Session["sectionIndex_0"] = null; Session["SubLevel1Index_0"] = null;
            ListItem listname;
            listname = new ListItem("-- select --", "0");
            ddlFirstLevelList0.Items.Clear();
            ddlFirstLevelList0.Items.Add(listname);
            listname = new ListItem("-- select --", "0");
            ddlSecondLevelList0.Items.Clear();
            ddlSecondLevelList0.Items.Add(listname);
        }
    }
    protected void ddlFirstLevelList0_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFirstLevelList0.SelectedIndex > 0)
        {
            Session["SubLevel1Index_0"] = ddlFirstLevelList0.SelectedIndex.ToString();
            FillSubLevel2Sections_copy();
        }
        else
        {
            Session["SubLevel1Index_0"] = null;
            ListItem listname;
            listname = new ListItem("-- select --", "0");
            ddlSecondLevelList0.Items.Clear();
            ddlSecondLevelList0.Items.Add(listname);
        }
    }
    protected void ddlSecondLevelList0_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSecondLevelList0.SelectedIndex > 0)
            Session["SubLevel2Index_0"] = ddlSecondLevelList0.SelectedIndex.ToString();
        else Session["SubLevel2Index_0"] = null;
       
    }
}
