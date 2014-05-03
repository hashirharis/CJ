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
using System.IO;

public partial class AddQuestions : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    bool specialadmin = false;
    int OrganizationID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {
            bool perassigned = false;
            specialadmin = true; OrganizationID = int.Parse(Session["AdminOrganizationID"].ToString());
            ListItem litem = new ListItem("-- select --", "0");
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(litem);

            var getQuestionTypes = from questiontypes in dataclass.OrganizationQuestionTypes where questiontypes.OrganizationId == OrganizationID select questiontypes;
            if (getQuestionTypes.Count() > 0)
            {
                foreach (var orgQuestionTypes in getQuestionTypes)
                {
                    litem = new ListItem(orgQuestionTypes.QuestionTypeDescription.ToString(), orgQuestionTypes.QuestionTypeName.ToString());
                    ddlCategory.Items.Add(litem);
                    perassigned = true;
                }
            }
            if (perassigned == false)
            { lblMessage.Text = "Please contact your site admin to approve question types to get permissions to add questions"; return; }
        }   


        //FillSessionslist(); 
        FillSectionCategory();

        int catindex = 0;
        if (Session["catindex"] != null)
        {
            catindex = int.Parse(Session["catindex"].ToString());
            ddlCategory.SelectedIndex = catindex;
            if (ddlCategory.SelectedValue == "RatingType" )
            {  pnlRatingStyle.Visible = true; pnlAnswer.Visible = false;}
            else if (ddlCategory.SelectedValue == "FillBlanks")
            { pnlAnswer.Visible = false; }
        }
        
        FillGrid();        

        FillAnswerOptions();
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

            if (specialadmin == true)
            {
                var details1 = from details in dataclass.SectionDetails
                               where details.ParentId == 0 && details.OrganizationId==OrganizationID && details.SectionCategoryId==sectionCategoryId
                               orderby details.SectionName ascending
                               select details;
                if (details1.Count() > 0)
                {
                    ddlSectionList.DataSource = details1;
                    ddlSectionList.DataTextField = "SectionName";
                    ddlSectionList.DataValueField = "SectionId";
                    ddlSectionList.DataBind();
                }

            }
            else
            {
                var details1 = from details in dataclass.SectionDetails
                               where details.ParentId == 0 && details.AdminAccess == 1 && details.SectionCategoryId == sectionCategoryId
                               orderby details.SectionName ascending
                               select details;
                if (details1.Count() > 0)
                {
                    ddlSectionList.DataSource = details1;
                    ddlSectionList.DataTextField = "SectionName";
                    ddlSectionList.DataValueField = "SectionId";
                    ddlSectionList.DataBind();
                }
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

                FillGrid();
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

        ddlFirstLevelList.SelectedIndex = parentindex;
        if (parentindex > 0)
        {
            FillSubLevel2Sections();
        }
        else
        {
            Session["SubLevel2Index"] = null;
            listname = new ListItem("-- select --", "0");
            ddlSecondLevelList.Items.Clear();
            ddlSecondLevelList.Items.Add(listname);

            FillGrid();
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

        //if (subindex > 0)
        //{
        ddlSecondLevelList.SelectedIndex = subindex;
        //}else

        FillGrid();
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
        if (ddlSectionList.SelectedIndex <= 0 && ddlFirstLevelList.SelectedIndex <= 0 && ddlSecondLevelList.SelectedIndex <= 0)
        { lblMessage.Text = "Please select a section variable from the list"; variableselected = false; }
        else
        {
            if (ddlSectionList.SelectedIndex > 0)
            {
                if (ddlFirstLevelList.SelectedIndex > 0)
                {
                    if (ddlSecondLevelList.SelectedIndex <= 0)
                    {
                        if (CheckChildExists(int.Parse(ddlFirstLevelList.SelectedValue)) == true)
                        {
                            variableselected = false;
                            lblMessage.Text = "Please select a 2nd level sub variable from the list";
                        }
                    }
                }
                else
                {// code to check wheather there is a sublevel entries under this selected value.

                    if (CheckChildExists(int.Parse(ddlSectionList.SelectedValue)) == true)
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

    private int GetRatingtypeAnswer()
    {
        bool optionvalid = false;
        if (ddlOption.SelectedValue == "1")
        {
            if (txtOption1.Text.Trim() == "")
            { lblMessage.Text = "No value for 1st option"; }
            else optionvalid = true;
        }
        else if (ddlOption.SelectedValue == "2")
        {
            if (txtOption2.Text.Trim() == "")
            { lblMessage.Text = "No value for 2nd option"; }
            else optionvalid = true;
        }
        else if (ddlOption.SelectedValue == "3")
        {
            if (txtOption3.Text.Trim() == "")
            { lblMessage.Text = "No value for 3rd option"; }
            else optionvalid = true;
        }
        else if (ddlOption.SelectedValue == "4")
        {
            if (txtOption4.Text.Trim() == "")
            { lblMessage.Text = "No value for 4th option"; }
            else optionvalid = true;
        }
        else if (ddlOption.SelectedValue == "5")
        {
            if (txtOption5.Text.Trim() == "")
            { lblMessage.Text = "No value for 5th option"; }
            else optionvalid = true;
        }
        else if (ddlOption.SelectedValue == "6")
        {
            if (txtOption6.Text.Trim() == "")
            { lblMessage.Text = "No value for 6th option"; }
            else optionvalid = true;
        }
        else if (ddlOption.SelectedValue == "7")
        {
            if (txtOption7.Text.Trim() == "")
            { lblMessage.Text = "No value for 7th option"; }
            else optionvalid = true;
        }
        else if (ddlOption.SelectedValue == "8")
        {
            if (txtOption8.Text.Trim() == "")
            { lblMessage.Text = "No value for 8th option"; }
            else optionvalid = true;
        }
        else if (ddlOption.SelectedValue == "9")
        {
            if (txtOption9.Text.Trim() == "")
            { lblMessage.Text = "No value for 9th option"; }
            else optionvalid = true;
        }
        else if (ddlOption.SelectedValue == "10")
        {
            if (txtOption10.Text.Trim() == "")
            { lblMessage.Text = "No value for 10th option"; }
            else optionvalid = true;
        }

        if (optionvalid == false)
            return 0;

        int Answer = 0;
        int optioncount = 0;
        if (txtOption1.Text.Trim() != "")
        {
            optioncount++;
            if (ddlOption.SelectedValue == "1")
                Answer = optioncount;
        }
        if (txtOption2.Text.Trim() != "")
        {
            optioncount++;
            if (ddlOption.SelectedValue == "2")
                Answer = optioncount;
        }
        if (txtOption3.Text.Trim() != "")
        {
            optioncount++;
            if (ddlOption.SelectedValue == "3")
                Answer = optioncount;
        }
        if (txtOption4.Text.Trim() != "")
        {
            optioncount++;
            if (ddlOption.SelectedValue == "4")
                Answer = optioncount;
        }
        if (txtOption5.Text.Trim() != "")
        {
            optioncount++;
            if (ddlOption.SelectedValue == "5")
                Answer = optioncount;
        }
        if (txtOption6.Text.Trim() != "")
        {
            optioncount++;
            if (ddlOption.SelectedValue == "6")
                Answer = optioncount;
        }
        if (txtOption7.Text.Trim() != "")
        {
            optioncount++;
            if (ddlOption.SelectedValue == "7")
                Answer = optioncount;
        }
        if (txtOption8.Text.Trim() != "")
        {
            optioncount++;
            if (ddlOption.SelectedValue == "8")
                Answer = optioncount;
        }
        if (txtOption9.Text.Trim() != "")
        {
            optioncount++;
            if (ddlOption.SelectedValue == "9")
                Answer = optioncount;
        }
        if (txtOption10.Text.Trim() != "")
        {
            optioncount++;
            if (ddlOption.SelectedValue == "10")
                Answer = optioncount;
        }
        if (Answer > 0)
        {
            if (ddlScoringStyle.SelectedValue == "Reverse")
            {
                Answer = (optioncount - Answer) + 1;
            }
        }
        else { lblMessage.Text = "Please select a valid answer"; }

        return Answer;
    }    


    private string GetImageTypeMemAnswer()
    {
        string Answer = "";
        if (txtOption1.Text.Trim() != "" || txtOption2.Text.Trim() != "" || txtOption3.Text.Trim() != "" || txtOption4.Text.Trim() != "" || txtOption5.Text.Trim() != "")
        {
            if (ddlOption.SelectedValue == "1")
            {
                if (txtOption1.Text.Trim() == "")
                { lblMessage.Text = "No value for 1st option"; }//return;
                else Answer = "1";
            }
            else if (ddlOption.SelectedValue == "2")
            {
                if (txtOption2.Text.Trim() == "")
                { lblMessage.Text = "No value for 2nd option"; }//return;
                else Answer = "2";
            }
            else if (ddlOption.SelectedValue == "3")
            {
                if (txtOption3.Text.Trim() == "")
                { lblMessage.Text = "No value for 3rd option"; }//return;
                else Answer = "3";
            }
            else if (ddlOption.SelectedValue == "4")
            {
                if (txtOption4.Text.Trim() == "")
                { lblMessage.Text = "No value for 4th option"; }//return;
                else Answer = "4";
            }
            else if (ddlOption.SelectedValue == "5")
            {
                if (txtOption5.Text.Trim() == "")
                { lblMessage.Text = "No value for 5th option"; }//return;
                else Answer = "5";
            }

            if (ddlOption.SelectedValue == "6")
            {
                if (txtOption6.Text.Trim() == "")
                { lblMessage.Text = "No value for 6th option"; }//return;
                else Answer = "6";
            }
            else if (ddlOption.SelectedValue == "7")
            {
                if (txtOption7.Text.Trim() == "")
                { lblMessage.Text = "No value for 7th option"; }//return;
                else Answer = "7";
            }
            else if (ddlOption.SelectedValue == "8")
            {
                if (txtOption8.Text.Trim() == "")
                { lblMessage.Text = "No value for 8th option"; }//return;
                else Answer = "8";
            }
            else if (ddlOption.SelectedValue == "9")
            {
                if (txtOption9.Text.Trim() == "")
                { lblMessage.Text = "No value for 9th option"; }//return;
                else Answer = "9";
            }
            else if (ddlOption.SelectedValue == "10")
            {
                if (txtOption10.Text.Trim() == "")
                { lblMessage.Text = "No value for 10th option"; }//return;
                else Answer = "10";
            }
        }
        else
        {
            if (txtFileName1.Value.Trim() != "" || txtFileName2.Value.Trim() != "" || txtFileName3.Value.Trim() != "" || txtFileName4.Value.Trim() != "" || txtFileName5.Value.Trim() != "")
            {
                if (ddlOption.SelectedValue == "1")
                {
                    if (txtFileName1.Value.Trim() == "")
                    { lblMessage.Text = "No value for 1st option"; }//return;
                    else Answer = "1";
                }
                else if (ddlOption.SelectedValue == "2")
                {
                    if (txtFileName2.Value.Trim() == "")
                    { lblMessage.Text = "No value for 2nd option"; }//return;
                    else Answer = "2";
                }
                else if (ddlOption.SelectedValue == "3")
                {
                    if (txtFileName3.Value.Trim() == "")
                    { lblMessage.Text = "No value for 3rd option"; }//return;
                    else Answer = "3";
                }
                else if (ddlOption.SelectedValue == "4")
                {
                    if (txtFileName4.Value.Trim() == "")
                    { lblMessage.Text = "No value for 4th option"; }//return;
                    else Answer = "4";
                }
                else if (ddlOption.SelectedValue == "5")
                {
                    if (txtFileName5.Value.Trim() == "")
                    { lblMessage.Text = "No value for 4th option"; }//return;
                    else Answer = "5";
                }
            }
        }

        return Answer;
    }


    private bool SaveMemTypeImages()
    {
        if (txtfilenameMemTypeA.Value.Trim() != "" || txtfilenameMemTypeB.Value.Trim() != "" || txtfilenameMemTypeC.Value.Trim() != "" || txtfilenameMemTypeD.Value.Trim() != "" || txtfilenameMemTypeE.Value.Trim() != "" || txtfilenameMemTypeF.Value.Trim() != "" || txtfilenameMemTypeG.Value.Trim() != "" || txtfilenameMemTypeH.Value.Trim() != "" || txtfilenameMemTypeI.Value.Trim() != "" || txtfilenameMemTypeJ.Value.Trim() != "" ||
            txtfilenameMemTypeK.Value.Trim() != "" || txtfilenameMemTypeL.Value.Trim() != "" || txtfilenameMemTypeM.Value.Trim() != "" || txtfilenameMemTypeN.Value.Trim() != "" || txtfilenameMemTypeO.Value.Trim() != "" || txtfilenameMemTypeP.Value.Trim() != "" || txtfilenameMemTypeQ.Value.Trim() != "" || txtfilenameMemTypeR.Value.Trim() != "" || txtfilenameMemTypeS.Value.Trim() != "" || txtfilenameMemTypeT.Value.Trim() != "")
        {
            if (txtfilenameMemTypeA.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeA.Value.Trim());
            if (txtfilenameMemTypeB.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeB.Value.Trim());
            if (txtfilenameMemTypeC.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeC.Value.Trim());
            if (txtfilenameMemTypeD.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeD.Value.Trim());
            if (txtfilenameMemTypeE.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeE.Value.Trim());
            if (txtfilenameMemTypeF.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeF.Value.Trim());
            if (txtfilenameMemTypeG.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeG.Value.Trim());
            if (txtfilenameMemTypeH.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeH.Value.Trim());
            if (txtfilenameMemTypeI.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeI.Value.Trim());
            if (txtfilenameMemTypeJ.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeJ.Value.Trim());
            if (txtfilenameMemTypeK.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeK.Value.Trim());
            if (txtfilenameMemTypeL.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeL.Value.Trim());
            if (txtfilenameMemTypeM.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeM.Value.Trim());
            if (txtfilenameMemTypeN.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeN.Value.Trim());
            if (txtfilenameMemTypeO.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeO.Value.Trim());
            if (txtfilenameMemTypeP.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeP.Value.Trim());
            if (txtfilenameMemTypeQ.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeQ.Value.Trim());
            if (txtfilenameMemTypeR.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeR.Value.Trim());
            if (txtfilenameMemTypeS.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeS.Value.Trim());
            if (txtfilenameMemTypeT.Value.Trim() != "")
                SaveFile(txtfilenameMemTypeT.Value.Trim());

            return true;
            
        }
        else lblMessage.Text = "Please select images for memmory test"; return false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (validateSelection() == false) return;

        int userid = 0;
        int quesID = 0;
        string Answer = "";
        //string Section = "";
        if (txtQues.Text == "" || ddlCategory.SelectedIndex <= 0)// ||ddlIntroductioncategories.SelectedIndex <= 0)
        {
            lblMessage.Text = "Enter The Required Fields";
            return;
        }
        string filblankanswer = ""; string ratingscale = "";
        if (ddlCategory.SelectedValue == "Objective" || ddlCategory.SelectedValue == "RatingType" || ddlCategory.SelectedValue == "MemTestWords")
        {
            int optcount = 0;
            if (txtOption1.Text.Trim() != "")
                optcount += 1;
            if (txtOption2.Text.Trim() != "")
                optcount += 1;
            if (txtOption3.Text.Trim() != "")
                optcount += 1;
            if (txtOption4.Text.Trim() != "")
                optcount += 1;
            if (txtOption5.Text.Trim() != "")
                optcount += 1;
            if (txtOption6.Text.Trim() != "")
                optcount += 1;
            if (txtOption7.Text.Trim() != "")
                optcount += 1;
            if (txtOption8.Text.Trim() != "")
                optcount += 1;
            if (txtOption9.Text.Trim() != "")
                optcount += 1;
            if (txtOption10.Text.Trim() != "")
                optcount += 1;

            if (optcount<2)
            {
                lblMessage.Text = "Enter Atleast 2 Options";
                return;
            }
            if (ddlCategory.SelectedValue == "RatingType")
                ratingscale = ddlScoringStyle.SelectedValue;
        }
        else if (ddlCategory.SelectedValue == "VideoType" || ddlCategory.SelectedValue == "AudioType")
        {
            if (txtFileName_main.Value.Trim() == "")
            {
                lblMessage.Text = "Browse a reference file for current question";
                return;
            }
            if (txtOption1.Text == "" || txtOption2.Text == "")
            {
                lblMessage.Text = "Enter Atleast 2 Options";
                return;
            }
            // code to insert image file. to "QuestionAnswerFiles" folder
            SaveFile(txtFileName_main.Value.Trim());
        }
        else if (ddlCategory.SelectedValue == "ImageType" || ddlCategory.SelectedValue == "MemTestImages" ||  ddlCategory.SelectedValue == "PhotoType") //021209 bip
        {
            if (ddlCategory.SelectedValue == "ImageType" || ddlCategory.SelectedValue == "PhotoType")// 021209 bip
            {
                if (txtFileName_main.Value.Trim() == "" && txtFileName_sub.Value.Trim() == "")
                {
                    lblMessage.Text = "Browse a reference file for current question";
                    return;
                }
            }
            if (txtOption1.Text.Trim() != "" || txtOption2.Text.Trim() != "" || txtOption3.Text.Trim() != "" || txtOption4.Text.Trim() != "" || txtOption5.Text.Trim() != "")
            {
                int optcount = 0;
                if (txtOption1.Text.Trim() != "")
                    optcount += 1;
                if (txtOption2.Text.Trim() != "")
                    optcount += 1;
                if (txtOption3.Text.Trim() != "")
                    optcount += 1;
                if (txtOption4.Text.Trim() != "")
                    optcount += 1;
                if (txtOption5.Text.Trim() != "")
                    optcount += 1;
                if (txtOption6.Text.Trim() != "")
                    optcount += 1;
                if (txtOption7.Text.Trim() != "")
                    optcount += 1;
                if (txtOption8.Text.Trim() != "")
                    optcount += 1;
                if (txtOption9.Text.Trim() != "")
                    optcount += 1;
                if (txtOption10.Text.Trim() != "")
                    optcount += 1;

                if (optcount < 2)
                {
                    lblMessage.Text = "Enter Atleast 2 Options";
                    return;
                }
            }
            else if (txtFileName1.Value.Trim() != "" || txtFileName2.Value.Trim() != "" || txtFileName3.Value.Trim() != "" || txtFileName4.Value.Trim() != "" || txtFileName5.Value.Trim() != "")
            {
                int optioncount = 0;
                if (txtFileName1.Value.Trim() != "")
                    optioncount += 1;
                if (txtFileName2.Value.Trim() != "")
                    optioncount += 1;
                if (txtFileName3.Value.Trim() != "")
                    optioncount += 1;
                if (txtFileName4.Value.Trim() != "")
                    optioncount += 1;
                if (txtFileName5.Value.Trim() != "")
                    optioncount += 1;

                if (optioncount < 2)
                {
                    lblMessage.Text = "Enter Atleast 2 Options";
                    return;
                }

                if (txtFileName1.Value.Trim() != "")
                    SaveFile(txtFileName1.Value.Trim());
                if (txtFileName2.Value.Trim() != "")
                    SaveFile(txtFileName2.Value.Trim());
                if (txtFileName3.Value.Trim() != "")
                    SaveFile(txtFileName3.Value.Trim());
                if (txtFileName4.Value.Trim() != "")
                    SaveFile(txtFileName4.Value.Trim());
                if (txtFileName5.Value.Trim() != "")
                    SaveFile(txtFileName5.Value.Trim());
            }
            else
            {
                lblMessage.Text = "Please Enter answer Options";
                return;
            }

            // code to insert image file.   "QuestionAnswerFiles" folder.
            if (txtFileName_main.Value.Trim() != "")
                SaveFile(txtFileName_main.Value.Trim());
            if (txtFileName_sub.Value.Trim() != "")
                SaveFile(txtFileName_sub.Value.Trim());
        }
        else
        {
            if (ddlCategory.SelectedValue == "FillBlanks")
            {
                if (txtOption1.Text.Trim() != "")
                    filblankanswer = txtOption1.Text.Trim();
                if (txtOption2.Text.Trim() != "")
                {
                    if (filblankanswer != "") filblankanswer += ", ";
                    filblankanswer += txtOption2.Text.Trim();
                }
                if (txtOption3.Text.Trim() != "")
                {
                    if (filblankanswer != "") filblankanswer += ", ";
                    filblankanswer += txtOption3.Text.Trim();
                }
                if (txtOption4.Text.Trim() != "")
                {
                    if (filblankanswer != "") filblankanswer += ", ";
                    filblankanswer += txtOption4.Text.Trim();
                }
                if (txtOption5.Text.Trim() != "")
                {
                    if (filblankanswer != "") filblankanswer += ", ";
                    filblankanswer += txtOption5.Text.Trim();
                }
            }
            
        }
        if (Session["QuesID"] != null)
        {
            quesID = int.Parse(Session["QuesID"].ToString());
            //Session["QuesID"] = null;
        }

        
        if (ddlCategory.SelectedValue == "MemTestImages")
        {
            if (txtOption1.Text.Trim() != "" || txtOption2.Text.Trim() != "" || txtOption3.Text.Trim() != "" || txtOption4.Text.Trim() != "" || txtOption5.Text.Trim() != "")
            {
                if (txtFileName1.Value.Trim() != "" || txtFileName2.Value.Trim() != "" || txtFileName3.Value.Trim() != "" || txtFileName4.Value.Trim() != "" || txtFileName5.Value.Trim() != "")
                { lblMessage.Text = "You can't select both options(image options and text options)  at a time"; return; }
            }
            else
            {
                if (txtFileName1.Value.Trim() == "" && txtFileName2.Value.Trim() == "" && txtFileName3.Value.Trim() == "" && txtFileName4.Value.Trim() == "" && txtFileName5.Value.Trim() == "")
                { lblMessage.Text = "Please enter answer options"; return; }

            }
            bool imagesaved = false;
            imagesaved = SaveMemTypeImages();
            if (imagesaved == false) return;
        }

        if (ddlOption.SelectedIndex > 0)
        {

            if (ddlCategory.SelectedValue == "MemTestImages" || ddlCategory.SelectedValue == "ImageType" || ddlCategory.SelectedValue == "PhotoType")// 021209 bip
            {
                Answer = GetImageTypeMemAnswer();
                if (Answer == "") return;

            }
            else if (ddlCategory.SelectedValue == "RatingType")
            {
                Answer = GetRatingtypeAnswer().ToString();
                if (Answer == "") return;
            }
            else if (ddlCategory.SelectedValue != "ImageType")
            {
                if (ddlOption.SelectedValue == "1")
                {
                    if (txtOption1.Text.Trim() == "")
                    { lblMessage.Text = "No value for 1st option"; return; }
                    //Answer = txtOption1.Text;
                    Answer = "1";
                }
                else if (ddlOption.SelectedValue == "2")
                {
                    if (txtOption2.Text.Trim() == "")
                    { lblMessage.Text = "No value for 2nd option"; return; }
                    //Answer = txtOption2.Text;
                    Answer = "2";
                }
                else if (ddlOption.SelectedValue == "3")
                {
                    if (txtOption3.Text.Trim() == "")
                    { lblMessage.Text = "No value for 3rd option"; return; } 
                    //Answer = txtOption3.Text;
                    Answer = "3";
                }
                else if (ddlOption.SelectedValue == "4")
                {
                    if (txtOption4.Text.Trim() == "")
                    { lblMessage.Text = "No value for 4th option"; return; }
                    //Answer = txtOption4.Text;
                    Answer = "4";
                }
                else if (ddlOption.SelectedValue == "5")
                {
                    if (txtOption5.Text.Trim() == "")
                    { lblMessage.Text = "No value for 5th option"; return; }
                    //Answer = txtOption5.Text;
                    Answer = "5";
                }
                else if (ddlOption.SelectedValue == "6")
                {
                    if (txtOption6.Text.Trim() == "")
                    { lblMessage.Text = "No value for 6th option"; return; }
                    //Answer = txtOption6.Text;
                    Answer = "6";
                }
                else if (ddlOption.SelectedValue == "7")
                {
                    if (txtOption7.Text.Trim() == "")
                    { lblMessage.Text = "No value for 7th option"; return; }
                    //Answer = txtOption7.Text;
                    Answer = "7";
                }
                else if (ddlOption.SelectedValue == "8")
                {
                    if (txtOption8.Text.Trim() == "")
                    { lblMessage.Text = "No value for 8th option"; return; }
                    //Answer = txtOption8.Text;
                    Answer = "8";
                }
                else if (ddlOption.SelectedValue == "9")
                {
                    if (txtOption9.Text.Trim() == "")
                    { lblMessage.Text = "No value for 9th option"; return; }
                    //Answer = txtOption9.Text;
                    Answer = "9";
                }
                else if (ddlOption.SelectedValue == "10")
                {
                    if (txtOption10.Text.Trim() == "")
                    { lblMessage.Text = "No value for 10th option"; return; }
                    //Answer = txtOption10.Text;
                    Answer = "10";
                }
            }            
        }
        else
        {
            if (ddlCategory.SelectedValue != "FillBlanks" && ddlCategory.SelectedValue != "RatingType")
            { lblMessage.Text = "Select a Valid Option"; return; }
        }
        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());

        int sectionid = 0;
        if (ddlSecondLevelList.SelectedIndex > 0)
            sectionid = int.Parse(ddlSecondLevelList.SelectedValue);
        else if (ddlFirstLevelList.SelectedIndex > 0)
            sectionid = int.Parse(ddlFirstLevelList.SelectedValue);
        else if (ddlSectionList.SelectedIndex > 0)
            sectionid = int.Parse(ddlSectionList.SelectedValue);

        string sectionname = "", subsection1 = "", subsection2 = "";
        if (ddlSectionList.SelectedIndex > 0)
            sectionname = ddlSectionList.SelectedItem.Text;
        if (ddlFirstLevelList.SelectedIndex > 0)
            subsection1 = ddlFirstLevelList.SelectedItem.Text;
        if (ddlSecondLevelList.SelectedIndex > 0)
            subsection2 = ddlSecondLevelList.SelectedItem.Text;

        if (ddlCategory.SelectedValue == "MemTestWords")
        {
            if (CheckDurationValues(txtWordDisplayDuration.Text) == false) return;
            double duration = double.Parse(txtWordDisplayDuration.Text);
            int displaytype = 0;
            if (ddlDisplayType_Words.SelectedIndex > 0)
                displaytype = int.Parse(ddlDisplayType_Words.SelectedValue);
            else { lblMessage.Text = "Please select words display type."; return; }

            if (txtMemTypeWords.Text == "" && txtMemTypeWord2.Text == "" && txtMemTypeWord3.Text == "" && txtMemTypeWord4.Text == "" && txtMemTypeWord5.Text == "" &&
                txtMemTypeWord6.Text == "" && txtMemTypeWord7.Text == "" && txtMemTypeWord8.Text == "" && txtMemTypeWord9.Text == "" && txtMemTypeWord10.Text == "" &&
                txtMemTypeWord11.Text == "" && txtMemTypeWord12.Text == "" && txtMemTypeWord13.Text == "" && txtMemTypeWord14.Text == "" && txtMemTypeWord15.Text == "" &&
                txtMemTypeWord16.Text == "" && txtMemTypeWord17.Text == "" && txtMemTypeWord18.Text == "" && txtMemTypeWord19.Text == "" && txtMemTypeWord20.Text == "")
                lblMessage.Text = "Enter values";
            else
            {                

                //int memTypeWordQuesID = 0;
                //if (Session["MemTypeWordQuesID"] != null)
                //    memTypeWordQuesID = int.Parse(Session["MemTypeWordQuesID"].ToString());
                dataclass.Procedure_MemmoryTestTextQuesCollection(quesID, sectionid, sectionname, subsection1, subsection2, ddlCategory.SelectedValue, txtQues.Text, txtMemTypeWords.Text.Trim(), txtMemTypeWord2.Text.Trim(), txtMemTypeWord3.Text.Trim(), txtMemTypeWord4.Text.Trim(), txtMemTypeWord5.Text.Trim(),
                    txtMemTypeWord6.Text.Trim(), txtMemTypeWord7.Text.Trim(), txtMemTypeWord8.Text.Trim(), txtMemTypeWord9.Text.Trim(), txtMemTypeWord10.Text.Trim(), txtMemTypeWord11.Text.Trim(), txtMemTypeWord12.Text.Trim(), txtMemTypeWord13.Text.Trim(), txtMemTypeWord14.Text.Trim(), txtMemTypeWord15.Text.Trim(),
                    txtMemTypeWord16.Text.Trim(), txtMemTypeWord17.Text.Trim(), txtMemTypeWord18.Text.Trim(), txtMemTypeWord19.Text.Trim(), txtMemTypeWord20.Text.Trim(), txtOption1.Text.Trim(), txtOption2.Text.Trim(), txtOption3.Text.Trim(), txtOption4.Text.Trim(), txtOption5.Text.Trim(), Answer, duration, 0, userid, displaytype,txtQuestionCode.Text);
            }
        }
        else if (ddlCategory.SelectedValue == "MemTestImages")
        {
            if (CheckDurationValues(txtImageDisplayDuration.Text) == false) return;
            double duration = double.Parse(txtImageDisplayDuration.Text);
            int displaytype=0;
            if (ddlDisplayType_Images.SelectedIndex > 0)
                displaytype = int.Parse(ddlDisplayType_Images.SelectedValue);
            else { lblMessage.Text = "Please select image display type."; return; }

            if (txtfilenameMemTypeA.Value.Trim() == "" && txtfilenameMemTypeB.Value.Trim() == "" && txtfilenameMemTypeC.Value.Trim() == "" && txtfilenameMemTypeD.Value.Trim() == "" && txtfilenameMemTypeE.Value.Trim() == "" &&
                txtfilenameMemTypeF.Value.Trim() == "" && txtfilenameMemTypeG.Value.Trim() == "" && txtfilenameMemTypeH.Value.Trim() == "" && txtfilenameMemTypeI.Value.Trim() == "" && txtfilenameMemTypeJ.Value.Trim() == "" &&
                txtfilenameMemTypeK.Value.Trim() == "" && txtfilenameMemTypeL.Value.Trim() == "" && txtfilenameMemTypeM.Value.Trim() == "" && txtfilenameMemTypeN.Value.Trim() == "" && txtfilenameMemTypeO.Value.Trim() == "" &&
                txtfilenameMemTypeP.Value.Trim() == "" && txtfilenameMemTypeQ.Value.Trim() == "" && txtfilenameMemTypeR.Value.Trim() == "" && txtfilenameMemTypeS.Value.Trim() == "" && txtfilenameMemTypeT.Value.Trim() == "")
                lblMessage.Text = "Enter Image files";
            else
            {

                //int memTypeImageQuesID = 0;
                //if (Session["MemTypeImageQuesID"] != null)
                //    memTypeImageQuesID = int.Parse(Session["MemTypeWordQuesID"].ToString());
                dataclass.Procedure_MemmoryTestImageQuesCollection(quesID, sectionid, sectionname, subsection1, subsection2, ddlCategory.SelectedValue, txtQues.Text, txtfilenameMemTypeA.Value.Trim(), txtfilenameMemTypeB.Value.Trim(), txtfilenameMemTypeC.Value.Trim(), txtfilenameMemTypeD.Value.Trim(), txtfilenameMemTypeE.Value.Trim(),
                    txtfilenameMemTypeF.Value.Trim(), txtfilenameMemTypeG.Value.Trim(), txtfilenameMemTypeH.Value.Trim(), txtfilenameMemTypeI.Value.Trim(), txtfilenameMemTypeJ.Value.Trim(), txtfilenameMemTypeK.Value.Trim(), txtfilenameMemTypeL.Value.Trim(), txtfilenameMemTypeM.Value.Trim(), txtfilenameMemTypeN.Value.Trim(), txtfilenameMemTypeO.Value.Trim(),
                    txtfilenameMemTypeP.Value.Trim(), txtfilenameMemTypeQ.Value.Trim(), txtfilenameMemTypeR.Value.Trim(), txtfilenameMemTypeS.Value.Trim(), txtfilenameMemTypeT.Value.Trim(),
                    txtFileName1.Value.Trim(), txtFileName2.Value.Trim(), txtFileName3.Value.Trim(), txtFileName4.Value.Trim(), txtFileName5.Value.Trim(), txtOption1.Text.Trim(), txtOption2.Text.Trim(), txtOption3.Text.Trim(), txtOption4.Text.Trim(), txtOption5.Text.Trim(), Answer, duration, displaytype, 0, userid,txtQuestionCode.Text);
                
            }           
        }

        else if (ddlCategory.SelectedValue == "FillBlanks")
        {
            /// bip 13052010 code to insert questionimage file.   
            if (txtFileName_main.Value.Trim() != "")
                SaveFile(txtFileName_main.Value.Trim());
            if (txtFileName_sub.Value.Trim() != "")
                SaveFile(txtFileName_sub.Value.Trim());
            ///

            dataclass.Procedure_AddQuestions(quesID, sectionid, sectionname, subsection1, subsection2, ddlCategory.SelectedValue,
                txtQues.Text, filblankanswer, txtOption1.Text, txtOption2.Text, txtOption3.Text, txtOption4.Text, txtOption5.Text,
                txtOption6.Text, txtOption7.Text, txtOption8.Text, txtOption9.Text, txtOption10.Text, txtFileName_main.Value,
                txtFileName1.Value, txtFileName2.Value, txtFileName3.Value, txtFileName4.Value, 0, userid, ratingscale, txtFileName5.Value.Trim(), txtFileName_sub.Value.Trim(),txtQuestionCode.Text);
        }
        else
            dataclass.Procedure_AddQuestions(quesID, sectionid, sectionname, subsection1, subsection2, ddlCategory.SelectedValue, txtQues.Text,
                Answer, txtOption1.Text, txtOption2.Text, txtOption3.Text, txtOption4.Text, txtOption5.Text, txtOption6.Text, txtOption7.Text,
                txtOption8.Text, txtOption9.Text, txtOption10.Text, txtFileName_main.Value, txtFileName1.Value, txtFileName2.Value,
                txtFileName3.Value, txtFileName4.Value, 0, userid, ratingscale, txtFileName5.Value.Trim(), txtFileName_sub.Value.Trim(),txtQuestionCode.Text);

        lblMessage.Text = "Question is Posted Successfully";
        Session["optionIndex"] = null; Session["QuesID"] = null;
        ClearControls();
        FillGrid();
    }

    private bool CheckDurationValues(string duration)
    {
        bool Isvalid = true;

        try
        {
            double dispDuration = double.Parse(duration);

        }
        catch (Exception ex) { lblMessage.Text = "Please enter a valid time at duration field"; Isvalid = false; }
        return Isvalid;
    }

    private void SaveFile(string filename)
    {
        string sourcepath, destinationpath;
        sourcepath = "UploadedImages\\" + filename;
        destinationpath = "QuestionAnswerFiles\\" + filename;
        if (File.Exists(Server.MapPath(sourcepath)))
        {
            if (!File.Exists(Server.MapPath(destinationpath)))
                File.Copy(Server.MapPath(sourcepath), Server.MapPath(destinationpath));
            File.Delete(Server.MapPath(sourcepath));
        }
        else { }

    }

    private void ClearControls()
    {
        txtQuestionCode.Text = "";
        txtQues.Text = "";
        //ddlCategory.SelectedIndex = 0;
        txtOption1.Text = ""; txtOption2.Text = ""; txtOption3.Text = ""; txtOption4.Text = ""; txtOption5.Text = "";
        txtOption6.Text = ""; txtOption7.Text = ""; txtOption8.Text = ""; txtOption9.Text = ""; txtOption10.Text = "";
        txtFileName_main.Value = ""; txtFileName_sub.Value = "";
        txtFileName1.Value = ""; txtFileName2.Value = ""; txtFileName3.Value = ""; txtFileName4.Value = ""; txtFileName5.Value = "";
        txtfilenameMemTypeA.Value = ""; txtMemTypeWords.Text = "";
        ddlOption.SelectedIndex = 0;

        txtfilenameMemTypeB.Value = ""; txtfilenameMemTypeC.Value = ""; txtfilenameMemTypeD.Value = ""; txtfilenameMemTypeE.Value = "";
        txtfilenameMemTypeF.Value = ""; txtfilenameMemTypeG.Value = ""; txtfilenameMemTypeH.Value = ""; txtfilenameMemTypeI.Value = ""; txtfilenameMemTypeJ.Value = "";
        txtfilenameMemTypeK.Value = ""; txtfilenameMemTypeL.Value = ""; txtfilenameMemTypeM.Value = ""; txtfilenameMemTypeN.Value = ""; txtfilenameMemTypeO.Value = "";
        txtfilenameMemTypeP.Value = ""; txtfilenameMemTypeQ.Value = ""; txtfilenameMemTypeR.Value = ""; txtfilenameMemTypeS.Value = ""; txtfilenameMemTypeT.Value = "";
        txtMemTypeWord2.Text = ""; txtMemTypeWord3.Text = ""; txtMemTypeWord4.Text = ""; txtMemTypeWord5.Text = "";
        txtMemTypeWord6.Text = ""; txtMemTypeWord7.Text = ""; txtMemTypeWord8.Text = ""; txtMemTypeWord9.Text = ""; txtMemTypeWord10.Text = "";
        txtMemTypeWord11.Text = ""; txtMemTypeWord12.Text = ""; txtMemTypeWord13.Text = ""; txtMemTypeWord14.Text = ""; txtMemTypeWord15.Text = "";
        txtMemTypeWord16.Text = ""; txtMemTypeWord17.Text = ""; txtMemTypeWord18.Text = ""; txtMemTypeWord19.Text = ""; txtMemTypeWord20.Text = "";
            
    
    }


    private void ClearFields()
    {
        gvwQues.DataSource = "";
        gvwQues.DataBind();
        txtQues.Text = "";
        txtOption1.Text = ""; txtOption2.Text = ""; txtOption3.Text = ""; txtOption4.Text = ""; txtOption5.Text = "";
        txtOption6.Text = ""; txtOption7.Text = ""; txtOption8.Text = ""; txtOption9.Text = ""; txtOption10.Text = "";
        txtfilenameMemTypeA.Value = ""; txtMemTypeWords.Text = "";
        txtFileName_main.Value = ""; txtFileName_sub.Value = "";
        txtFileName1.Value = ""; txtFileName2.Value = ""; txtFileName3.Value = ""; txtFileName4.Value = ""; txtFileName5.Value = "";
    }
    private void FillQuestionDetails()
    {
        ClearControls();
        if (Session["QuesID"] != null)
        {
            int qusid = int.Parse(Session["QuesID"].ToString());
            var Ques1 = from Ques in dataclass.QuestionCollections
                        where Ques.QuestionID == qusid
                        select Ques;
            if (Ques1.Count() > 0)
            {
                int optindex = 0;
                // ddlIntroductioncategories.SelectedValue = Ques1.First().Section.ToString();
                ddlCategory.SelectedValue = Ques1.First().Category.ToString();
                txtQues.Text = Ques1.First().Question.ToString();
                string Answer = Ques1.First().Answer.ToString();

                string scoringstyle="";
                if (Ques1.First().ScoringStyle != null && Ques1.First().ScoringStyle != "")
                        scoringstyle = Ques1.First().ScoringStyle.ToString();

                if (Ques1.First().Option1 != null && Ques1.First().Option1 != "")
                {
                    txtOption1.Text = Ques1.First().Option1.ToString();
                    if (ddlCategory.SelectedValue == "RatingType")
                    {
                        if (optindex.ToString() == Answer)                            
                            ddlOption.SelectedIndex = optindex;
                        optindex++;
                    }
                    else if (ddlCategory.SelectedValue != "FillBlanks")
                        //if (Ques1.First().Option1.ToString() == Ques1.First().Answer.ToString())
                        if(Ques1.First().Answer.ToString()=="1")
                            ddlOption.SelectedIndex = 1;
                }
                if (Ques1.First().Option2 != null && Ques1.First().Option2 != "")
                {
                    txtOption2.Text = Ques1.First().Option2.ToString();
                    if (ddlCategory.SelectedValue == "RatingType")
                    {
                        if (optindex.ToString() == Answer)
                            ddlOption.SelectedIndex = optindex;
                        optindex++;
                    }
                    else if (ddlCategory.SelectedValue != "FillBlanks")
                        //if (Ques1.First().Option2.ToString() == Ques1.First().Answer.ToString())
                        if (Ques1.First().Answer.ToString() == "2")
                            ddlOption.SelectedIndex = 2;
                }
                if (Ques1.First().Option3 != null && Ques1.First().Option3 != "")
                {
                    txtOption3.Text = Ques1.First().Option3.ToString();
                    if (ddlCategory.SelectedValue == "RatingType")
                    {
                        if (optindex.ToString() == Answer)
                            ddlOption.SelectedIndex = optindex;
                        optindex++;
                    }
                    else if (ddlCategory.SelectedValue != "FillBlanks")
                        //if (Ques1.First().Option3.ToString() == Ques1.First().Answer.ToString())
                        if (Ques1.First().Answer.ToString() == "3")
                            ddlOption.SelectedIndex = 3;
                }
                if (Ques1.First().Option4 != null && Ques1.First().Option4 != "")
                {
                    txtOption4.Text = Ques1.First().Option4.ToString();
                    if (ddlCategory.SelectedValue == "RatingType")
                    {
                        if (optindex.ToString() == Answer)
                            ddlOption.SelectedIndex = optindex;
                        optindex++;
                    }
                    else if (ddlCategory.SelectedValue != "FillBlanks")
                        //if (Ques1.First().Option4.ToString() == Ques1.First().Answer.ToString())
                        if (Ques1.First().Answer.ToString() == "4")
                            ddlOption.SelectedIndex = 4;
                }
                if (Ques1.First().Option5 != null && Ques1.First().Option5 != "")
                {
                    txtOption5.Text = Ques1.First().Option5.ToString();
                    if (ddlCategory.SelectedValue == "RatingType")
                    {
                        if (optindex.ToString() == Answer)
                            ddlOption.SelectedIndex = optindex;
                        optindex++;
                    }
                    else if (ddlCategory.SelectedValue != "FillBlanks")
                        //if (Ques1.First().Option5.ToString() == Ques1.First().Answer.ToString())
                        if (Ques1.First().Answer.ToString() == "5")
                            ddlOption.SelectedIndex = 5;
                }
                if (Ques1.First().Option6 != null && Ques1.First().Option6 != "")
                {
                    txtOption6.Text = Ques1.First().Option6.ToString();
                    if (ddlCategory.SelectedValue == "RatingType")
                    {
                        if (optindex.ToString() == Answer)
                            ddlOption.SelectedIndex = optindex;
                        optindex++;
                    }
                    else if (ddlCategory.SelectedValue != "FillBlanks")
                        //if (Ques1.First().Option6.ToString() == Ques1.First().Answer.ToString())
                        if (Ques1.First().Answer.ToString() == "6")
                            ddlOption.SelectedIndex = 6;
                }
                if (Ques1.First().Option7 != null && Ques1.First().Option7 != "")
                {
                    txtOption7.Text = Ques1.First().Option7.ToString();
                    if (ddlCategory.SelectedValue == "RatingType")
                    {
                        if (optindex.ToString() == Answer)
                            ddlOption.SelectedIndex = optindex;
                        optindex++;
                    }
                    else if (ddlCategory.SelectedValue != "FillBlanks")
                        //if (Ques1.First().Option7.ToString() == Ques1.First().Answer.ToString())
                        if (Ques1.First().Answer.ToString() == "7")
                            ddlOption.SelectedIndex = 7;
                }
                if (Ques1.First().Option8 != null && Ques1.First().Option8 != "")
                {
                    txtOption8.Text = Ques1.First().Option8.ToString();
                    if (ddlCategory.SelectedValue == "RatingType")
                    {
                        if (optindex.ToString() == Answer)
                            ddlOption.SelectedIndex = optindex;
                        optindex++;
                    }
                    else if (ddlCategory.SelectedValue != "FillBlanks")
                        //if (Ques1.First().Option8.ToString() == Ques1.First().Answer.ToString())
                        if (Ques1.First().Answer.ToString() == "8")
                            ddlOption.SelectedIndex = 8;
                }
                if (Ques1.First().Option9 != null && Ques1.First().Option9 != "")
                {
                    txtOption9.Text = Ques1.First().Option9.ToString();
                    if (ddlCategory.SelectedValue == "RatingType")
                    {
                        if (optindex.ToString() == Answer)
                            ddlOption.SelectedIndex = optindex;
                        optindex++;
                    }
                    else if (ddlCategory.SelectedValue != "FillBlanks")
                        //if (Ques1.First().Option9.ToString() == Ques1.First().Answer.ToString())
                        if (Ques1.First().Answer.ToString() == "9")
                            ddlOption.SelectedIndex = 9;
                }
                if (Ques1.First().Option10 != null && Ques1.First().Option10 != "")
                {
                    txtOption10.Text = Ques1.First().Option10.ToString();
                    if (ddlCategory.SelectedValue == "RatingType")
                    {
                        if (optindex.ToString() == Answer)
                            ddlOption.SelectedIndex = optindex;
                        optindex++;
                    }
                    else if (ddlCategory.SelectedValue != "FillBlanks")
                        //if (Ques1.First().Option10.ToString() == Ques1.First().Answer.ToString())
                        if (Ques1.First().Answer.ToString() == "10")
                            ddlOption.SelectedIndex = 10;
                }

                if (Ques1.First().QuestionFileName != null)
                {
                    txtFileName_main.Value = Ques1.First().QuestionFileName.ToString();
                }

                if (Ques1.First().QuestionFileNameSub1 != null)
                {
                    txtFileName_sub.Value = Ques1.First().QuestionFileNameSub1.ToString();
                }

                if (Ques1.First().Option1FileName != null)
                {
                    txtFileName1.Value = Ques1.First().Option1FileName.ToString();
                }
                if (Ques1.First().Option2FileName != null)
                {
                    txtFileName2.Value = Ques1.First().Option2FileName.ToString();
                }
                if (Ques1.First().Option3FileName != null)
                {
                    txtFileName3.Value = Ques1.First().Option3FileName.ToString();
                }
                if (Ques1.First().Option4FileName != null)
                {
                    txtFileName4.Value = Ques1.First().Option4FileName.ToString();
                }
                if (Ques1.First().Option5FileName != null)
                {
                    txtFileName5.Value = Ques1.First().Option5FileName.ToString();
                }

                if (Ques1.First().SectionName != null)
                {
                    string sectionname = Ques1.First().SectionName.ToString();
                    for (int i = 0; i < ddlSectionList.Items.Count; i++)
                    {
                        if (ddlSectionList.Items[i].Text == sectionname)
                        {
                            ddlSectionList.SelectedIndex = i; Session["sectionIndex"] = i.ToString();
                            Session["SubLevel1Index"] = null; Session["SubLevel2Index"] = null;
                            FillSubLevel1Sections();
                            break;
                        }
                    }
                }
                if (Ques1.First().SectionNameSub1 != null)
                {
                    string sectionname = Ques1.First().SectionNameSub1.ToString();
                    for (int i = 0; i < ddlFirstLevelList.Items.Count; i++)
                    {
                        if (ddlFirstLevelList.Items[i].Text == sectionname)
                        {
                            ddlFirstLevelList.SelectedIndex = i; Session["SubLevel1Index"] = i.ToString();
                            Session["SubLevel2Index"] = null;
                            FillSubLevel2Sections();
                            break;
                        }
                    }
                }
                if (Ques1.First().SectionNameSub2 != null)
                {
                    string sectionname = Ques1.First().SectionNameSub2.ToString();
                    for (int i = 0; i < ddlSecondLevelList.Items.Count; i++)
                    {
                        if (ddlSecondLevelList.Items[i].Text == sectionname)
                        {
                            ddlSecondLevelList.SelectedIndex = i; Session["SubLevel2Index"] = i.ToString();
                            break;
                        }
                    }
                }
                if (Ques1.First().Category == "RatingType")
                {
                    pnlRatingStyle.Visible = true; pnlAnswer.Visible = false;
                    if (Ques1.First().ScoringStyle != null && Ques1.First().ScoringStyle != "")
                        ddlScoringStyle.SelectedValue = Ques1.First().ScoringStyle.ToString();
                    else ddlScoringStyle.SelectedIndex = 0;

                    if (scoringstyle != "Reverse")
                        ddlOption.SelectedIndex = optindex;
                    else
                    {
                        if (Answer != "")
                        {
                            int index = 0;
                            int curanswer = int.Parse(Answer);
                            if (curanswer == 10) index = 1;
                            else if (curanswer == 9) index = 2;
                            else if (curanswer == 8) index = 3;
                            else if (curanswer == 7) index = 4;
                            else if (curanswer == 6) index = 5;
                            else if (curanswer == 5) index = 6;
                            else if (curanswer == 4) index = 7;
                            else if (curanswer == 3) index = 8;
                            else if (curanswer == 2) index = 9;
                            else if (curanswer == 1) index = 10;

                            ddlOption.SelectedIndex = index;
                        }
                    }
                }
                else if (Ques1.First().Category == "FillBlanks") pnlAnswer.Visible = false;
            }
        }

    }
    protected void gvwQues_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["QuesID"] = gvwQues.SelectedRow.Cells[5].Text;//16102011
            FillQuestionDetails();
            FillGrid();
        }
        catch (Exception ex)
        { lblMessage.Text = ex.Message; }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblmsgImageOption.Text = "";
        if (ddlCategory.SelectedIndex > 0)
            if (ddlSectionList.SelectedIndex <= 0 && ddlFirstLevelList.SelectedIndex <= 0 && ddlSecondLevelList.SelectedIndex <= 0)
            { lblMessage.Text = "Please select a section from the above lists"; return; }

        if (ddlCategory.SelectedIndex > 0)
        {
            if (ddlCategory.SelectedValue == "MemTestWords")
            {
                pnlRatingScale.Visible = false; pnlImageOptions.Visible = false;
                pnlQuestionImage.Visible = false; pnlAnswer.Visible = true;
                pnlRatingStyle.Visible = false; pnlOptionEntry.Visible = true;
                pnlMemTypeImages.Visible = false; pnlMemTypewords.Visible = true; pnlQuestionImageSub.Visible = false;

                Panel1.Visible = false; Panel2.Visible = true; Panel3.Visible = false;
            }
            else if (ddlCategory.SelectedValue == "MemTestImages")
            {
                lblmsgImageOption.Text = "You can add images for answer option, but you cant add both text and images for answer options";
                pnlImageOptions.Visible = true; pnlRatingScale.Visible = false;
                pnlQuestionImage.Visible = false; pnlAnswer.Visible = true;
                pnlRatingStyle.Visible = false; pnlOptionEntry.Visible = true;
                pnlMemTypeImages.Visible = true; pnlMemTypewords.Visible = false; pnlQuestionImageSub.Visible = false;

                Panel1.Visible = false; Panel2.Visible = false; Panel3.Visible = true;
            }
            else if (ddlCategory.SelectedValue == "FillBlanks")
            { pnlAnswer.Visible = false; 
            pnlQuestionImage.Visible = true; pnlQuestionImageSub.Visible = true;//bip 13052010 to add question images with fillblanks questions
            }
            else if (ddlCategory.SelectedValue == "RatingType" )
            {
                Panel1.Visible = true; Panel2.Visible = false; Panel3.Visible = false; pnlRatingScale.Visible = true; pnlImageOptions.Visible = false;
                pnlQuestionImage.Visible = true; pnlQuestionImageSub.Visible = true;//bip 13052010 to add question images with fillblanks questions//pnlQuestionImage.Visible = false; pnlQuestionImageSub.Visible = false;
                pnlAnswer.Visible = false; pnlRatingStyle.Visible = true; pnlOptionEntry.Visible = true;
            }
            else if (ddlCategory.SelectedValue == "ImageType") //021209 bip
            { Panel1.Visible = true; Panel2.Visible = false; Panel3.Visible = false; pnlImageOptions.Visible = true; pnlOptionEntry.Visible = true; pnlRatingScale.Visible = true; pnlQuestionImage.Visible = true; pnlAnswer.Visible = true; pnlRatingStyle.Visible = false; pnlQuestionImageSub.Visible = true; }
            else if (ddlCategory.SelectedValue == "PhotoType") //021209 bip
            { Panel1.Visible = true; Panel2.Visible = false; Panel3.Visible = false; pnlImageOptions.Visible = true; pnlOptionEntry.Visible = true; pnlRatingScale.Visible = false; pnlQuestionImage.Visible = true; pnlAnswer.Visible = true; pnlRatingStyle.Visible = false; pnlQuestionImageSub.Visible = true; }
            else if (ddlCategory.SelectedValue == "VideoType" || ddlCategory.SelectedValue == "AudioType")
            { Panel1.Visible = true; Panel2.Visible = false; Panel3.Visible = false; pnlImageOptions.Visible = false; pnlOptionEntry.Visible = true; pnlRatingScale.Visible = false; pnlQuestionImage.Visible = true; pnlAnswer.Visible = true; pnlRatingStyle.Visible = false; pnlQuestionImageSub.Visible = false; }
            else if(ddlCategory.SelectedValue == "Objective"){ pnlOptionEntry.Visible = true; pnlRatingScale.Visible = true; pnlImageOptions.Visible = false; pnlAnswer.Visible = true; pnlRatingStyle.Visible = false; pnlQuestionImage.Visible = false; pnlQuestionImageSub.Visible = false; }
            if (Session["catindex"] != null)
            {
                if (Session["catindex"].ToString() != ddlCategory.SelectedIndex.ToString())
                {
                    FillAnswerOptions();
                    Session["catindex"] = ddlCategory.SelectedIndex;
                    if (validateSelection() == false) return;
                    if (Session["QuesID"] == null)
                        ClearFields();
                }
            }
            else { Session["catindex"] = ddlCategory.SelectedIndex; if (validateSelection() == false) return; FillAnswerOptions(); }
        }

        Session["catindex"] = ddlCategory.SelectedIndex;
        FillGrid();


    }

    private void FillAnswerOptions()
    {
        ListItem optItems;
        ddlOption.Items.Clear();
        optItems = new ListItem("-- Select --", "0");
        ddlOption.Items.Add(optItems);
        optItems = new ListItem("Option1", "1");
        ddlOption.Items.Add(optItems);
        optItems = new ListItem("Option2", "2");
        ddlOption.Items.Add(optItems);
        optItems = new ListItem("Option3", "3");
        ddlOption.Items.Add(optItems);
        optItems = new ListItem("Option4", "4");
        ddlOption.Items.Add(optItems);
        optItems = new ListItem("Option5", "5");
        ddlOption.Items.Add(optItems);

        if (ddlCategory.SelectedValue == "RatingType" || ddlCategory.SelectedValue == "Objective" || ddlCategory.SelectedValue == "ImageType")
        {
            pnlRatingScale.Visible = true;

            optItems = new ListItem("Option6", "6");
            ddlOption.Items.Add(optItems);
            optItems = new ListItem("Option7", "7");
            ddlOption.Items.Add(optItems);
            optItems = new ListItem("Option8", "8");
            ddlOption.Items.Add(optItems);
            optItems = new ListItem("Option9", "9");
            ddlOption.Items.Add(optItems);
            optItems = new ListItem("Option10", "10");
            ddlOption.Items.Add(optItems);
        }
        //if (Session["optionIndex"] != null)
        //    ddlOption.SelectedIndex = int.Parse(Session["optionIndex"].ToString());


        if (ddlCategory.SelectedIndex > 0)
        {
            if (ddlCategory.SelectedValue == "MemTestWords")
            {
                pnlRatingScale.Visible = false; pnlImageOptions.Visible = false;
                pnlQuestionImage.Visible = false; pnlAnswer.Visible = true;
                pnlRatingStyle.Visible = false; pnlOptionEntry.Visible = true;
                pnlMemTypeImages.Visible = false; pnlMemTypewords.Visible = true; pnlQuestionImageSub.Visible = false;
            }
            else if (ddlCategory.SelectedValue == "MemTestImages")
            {
                lblmsgImageOption.Text = "You can add images for answer option, but you cant add both text and images for answer options";
                pnlImageOptions.Visible = true; pnlRatingScale.Visible = false;
                pnlQuestionImage.Visible = false; pnlAnswer.Visible = true;
                pnlRatingStyle.Visible = false; pnlOptionEntry.Visible = true;
                pnlMemTypeImages.Visible = true; pnlMemTypewords.Visible = false; pnlQuestionImageSub.Visible = false;
            }
            else if (ddlCategory.SelectedValue == "FillBlanks")
            { pnlAnswer.Visible = false; 
                pnlQuestionImage.Visible = true; pnlQuestionImageSub.Visible = true;//bip 13052010 to add question images with fillblanks questions
            }
            else if (ddlCategory.SelectedValue == "RatingType")
            { pnlRatingScale.Visible = true; pnlImageOptions.Visible = false; pnlQuestionImage.Visible = false; pnlAnswer.Visible = false; pnlRatingStyle.Visible = true; pnlOptionEntry.Visible = true; pnlQuestionImageSub.Visible = false; }
            else if (ddlCategory.SelectedValue == "ImageType" )//021209 bip
            { pnlImageOptions.Visible = true; pnlOptionEntry.Visible = true; pnlRatingScale.Visible = true; pnlQuestionImage.Visible = true; pnlAnswer.Visible = true; pnlRatingStyle.Visible = false; pnlQuestionImageSub.Visible = true; }
            else if (ddlCategory.SelectedValue == "PhotoType")//021209 bip
            { pnlImageOptions.Visible = true; pnlOptionEntry.Visible = true; pnlRatingScale.Visible = false; pnlQuestionImage.Visible = true; pnlAnswer.Visible = true; pnlRatingStyle.Visible = false; pnlQuestionImageSub.Visible = true; }
            else if (ddlCategory.SelectedValue == "VideoType" || ddlCategory.SelectedValue == "AudioType")
            { pnlImageOptions.Visible = false; pnlOptionEntry.Visible = true; pnlRatingScale.Visible = false; pnlQuestionImage.Visible = true; pnlAnswer.Visible = true; pnlRatingStyle.Visible = false; pnlQuestionImageSub.Visible = false; }
            else { pnlOptionEntry.Visible = true; pnlRatingScale.Visible = true; pnlImageOptions.Visible = false; pnlAnswer.Visible = true; pnlRatingStyle.Visible = false; pnlQuestionImage.Visible = false; pnlQuestionImageSub.Visible = false; }


        }
    }
    private void FillWordTypeMemQuestions(int sectionid)
    {

        gvwWordTypeMemQuestions.DataSource = "";
        gvwWordTypeMemQuestions.DataBind();
        var memQues1 = from memQues in dataclass.MemmoryTestTextQuesCollections
                       where memQues.SectionId == sectionid
                       select memQues;
        if (memQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("SectionName");
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("DisplayDuration");
            dtQuestions.Columns.Add("QuestionId");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in memQues1)
            {
                drQuestions = dtQuestions.NewRow();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["SectionName"] = objquestions.SectionName.ToString();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                drQuestions["DisplayDuration"] = objquestions.DisplayDuration.ToString();
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwWordTypeMemQuestions.DataSource = ds.Tables[0];
            gvwWordTypeMemQuestions.DataBind();
        }
    }
    private void FillImageTypeMemQuestions(int sectionid)
    {
       // ClearControls();
        gvwImageTypeMemQuestions.DataSource = "";
        gvwImageTypeMemQuestions.DataBind();
        var memQues1 = from memQues in dataclass.MemmoryTestImageQuesCollections
                       where memQues.SectionId == sectionid
                       select memQues;
        if (memQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("SectionName");
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("DisplayDuration");
            dtQuestions.Columns.Add("QuestionId");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in memQues1)
            {
                drQuestions = dtQuestions.NewRow();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["SectionName"] = objquestions.SectionName.ToString();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                drQuestions["DisplayDuration"] = objquestions.DisplayDuration.ToString();
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwImageTypeMemQuestions.DataSource = ds.Tables[0];
            gvwImageTypeMemQuestions.DataBind();
        }
    }

    private void FillObjectiveQuestions(int sectionid)
    {

        var ObjQues1 = from ObjQues in dataclass.QuestionCollections
                       where ObjQues.Category == "Objective" && ObjQues.SectionId == sectionid
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("SectionName");
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionId");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["SectionName"] = objquestions.SectionName.ToString();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                drQuestions["QuestionFileName"] = "";
                drQuestions["QuestionFileNameSub1"] = "";
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
                       where ObjQues.Category == "FillBlanks" && ObjQues.SectionId == sectionid
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("SectionName");
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionId");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["SectionName"] = objquestions.SectionName.ToString();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                drQuestions["QuestionFileName"] = "";
                drQuestions["QuestionFileNameSub1"] = "";
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
                       where ObjQues.Category == "RatingType" && ObjQues.SectionId == sectionid
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("SectionName");
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionId");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["SectionName"] = objquestions.SectionName.ToString();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                drQuestions["QuestionFileName"] = "";
                drQuestions["QuestionFileNameSub1"] = "";
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
                       where ObjQues.Category == "ImageType" && ObjQues.SectionId == sectionid
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("SectionName");
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionId");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["SectionName"] = objquestions.SectionName.ToString();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                if (objquestions.QuestionFileName != null)
                    drQuestions["QuestionFileName"] = objquestions.QuestionFileName.ToString();
                else drQuestions["QuestionFileName"] = "";
                if (objquestions.QuestionFileNameSub1 != null)
                    drQuestions["QuestionFileNameSub1"] = objquestions.QuestionFileNameSub1.ToString();
                else drQuestions["QuestionFileNameSub1"] = "";
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
                       where ObjQues.Category == "PhotoType" && ObjQues.SectionId == sectionid
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("SectionName");
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionId");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["SectionName"] = objquestions.SectionName.ToString();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                if (objquestions.QuestionFileName != null)
                    drQuestions["QuestionFileName"] = objquestions.QuestionFileName.ToString();
                else drQuestions["QuestionFileName"] = "";
                if (objquestions.QuestionFileNameSub1 != null)
                    drQuestions["QuestionFileNameSub1"] = objquestions.QuestionFileNameSub1.ToString();
                else drQuestions["QuestionFileNameSub1"] = "";
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
                       where ObjQues.Category == "VideoType" && ObjQues.SectionId == sectionid
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("SectionName");
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionId");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                if(objquestions.QuestionCode!=null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["SectionName"] = objquestions.SectionName.ToString();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                drQuestions["QuestionFileName"] = objquestions.QuestionFileName.ToString();
                drQuestions["QuestionFileNameSub1"] = "";
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
                       where ObjQues.Category == "AudioType" && ObjQues.SectionId == sectionid
                       select ObjQues;
        if (ObjQues1.Count() > 0)
        {
            DataTable dtQuestions = new DataTable();
            dtQuestions.Columns.Add("QuestionCode");
            dtQuestions.Columns.Add("SectionName");
            dtQuestions.Columns.Add("Question");
            dtQuestions.Columns.Add("Answer");
            dtQuestions.Columns.Add("QuestionId");
            dtQuestions.Columns.Add("QuestionFileName");
            dtQuestions.Columns.Add("QuestionFileNameSub1");
            DataRow drQuestions;
            string questiondescription = "";
            foreach (var objquestions in ObjQues1)
            {
                drQuestions = dtQuestions.NewRow();
                if (objquestions.QuestionCode != null)
                    drQuestions["QuestionCode"] = objquestions.QuestionCode.ToString();
                drQuestions["SectionName"] = objquestions.SectionName.ToString();
                questiondescription = ClearHTMLTags(objquestions.Question.ToString());
                drQuestions["Question"] = questiondescription;
                drQuestions["Answer"] = objquestions.Answer.ToString();
                drQuestions["QuestionId"] = objquestions.QuestionID.ToString();
                drQuestions["QuestionFileName"] = objquestions.QuestionFileName.ToString();
                drQuestions["QuestionFileNameSub1"] = "";
                dtQuestions.Rows.Add(drQuestions);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtQuestions);
            gvwQues.DataSource = ds.Tables[0];
            gvwQues.DataBind();
        }
    }


    private void FillGrid()
    {

        gvwQues.DataSource = "";
        gvwQues.DataBind();

        int sectionid = 0;
        if (ddlSecondLevelList.SelectedIndex > 0)
            sectionid = int.Parse(ddlSecondLevelList.SelectedValue);
        else if (ddlFirstLevelList.SelectedIndex > 0)
            sectionid = int.Parse(ddlFirstLevelList.SelectedValue);
        else if (ddlSectionList.SelectedIndex > 0)
            sectionid = int.Parse(ddlSectionList.SelectedValue);
        if (ddlCategory.SelectedValue == "MemTestWords")
        {
            FillWordTypeMemQuestions(sectionid);
            Panel2.Visible = true; Panel3.Visible = false; Panel1.Visible = false;
        }
        else if (ddlCategory.SelectedValue == "MemTestImages")
        {
            FillImageTypeMemQuestions(sectionid);
            Panel2.Visible = false; Panel3.Visible = true; Panel1.Visible = false;
        }
        else
        {
            Panel1.Visible = true; Panel2.Visible = false; Panel3.Visible = false;
            if (ddlCategory.SelectedValue == "Objective")
            {
                FillObjectiveQuestions(sectionid);                
            }
            else if (ddlCategory.SelectedValue == "FillBlanks")
            {
                FillFillBlanksQuestions(sectionid);                
            }
            else if (ddlCategory.SelectedValue == "RatingType")
            {
                FillRatingTypeQuestions(sectionid);                
            }            
            else if (ddlCategory.SelectedValue == "ImageType")
            {
                FillImageTypeQuestions(sectionid);               
            }

            else if (ddlCategory.SelectedValue == "VideoType")
            {
                FillVideoTypeQuestions(sectionid);
            }

            else if (ddlCategory.SelectedValue == "AudioType")
            {
                FillAudioTypeQuestions(sectionid);
            }
            else if (ddlCategory.SelectedValue == "PhotoType")
            {
                FillPhotoTypeQuestions(sectionid);
            }

        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Session["QuesID"] = null;
        ClearControls();

        Session["catindex"] = null;
        Session["sectionIndex"] = null;
        Session["SubLevel1Index"] = null;
        Session["SubLevel2Index"] = null;
        FillSessionslist(0);
        ddlCategory.SelectedIndex = 0;
        gvwQues.DataSource = ""; gvwQues.DataBind();
        gvwWordTypeMemQuestions.DataSource = ""; gvwWordTypeMemQuestions.DataBind();
        gvwImageTypeMemQuestions.DataSource = ""; gvwImageTypeMemQuestions.DataBind();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["QuesID"] != null)
        {
            int index = 0;
            if (ddlCategory.SelectedValue == "MemTestImages")
                index = 1;
            else if (ddlCategory.SelectedValue == "MemTestWords")
                index = 2;
            else index = 0;

            int questionid = int.Parse(Session["QuesID"].ToString());
            dataclass.Procedure_DeleteQuestion(questionid,index);
            Session["QuesID"] = null;
            ClearControls();
            lblMessage.Text = "Deleted successfully";
            FillGrid();
        }
        else lblMessage.Text = "Please select a value for deletion";
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
    protected void gvwQues_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
        ////
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
    protected void ddlOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOption.SelectedIndex > 0)
            Session["optionIndex"] = ddlOption.SelectedIndex.ToString();
        else Session["optionIndex"] = null;
    }
    protected void ddlSecondLevelList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SubLevel2Index"] = ddlSecondLevelList.SelectedIndex.ToString();
    }
    protected void btnDeleteQuestionFilemain_Click(object sender, EventArgs e)
    {
        if (txtFileName_main.Value.Trim() != "")
        {
            if (DeleteFile(txtFileName_main.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtFileName_main.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    private bool DeleteFile(string filename)
    {
        bool deletionsuccess = false;
        string sourcepath, destinationpath;
        sourcepath = "UploadedImages\\" + filename;
        destinationpath = "QuestionAnswerFiles\\" + filename;
        if (File.Exists(Server.MapPath(sourcepath)))
        {
            if (File.Exists(Server.MapPath(sourcepath)))
            { File.Delete(Server.MapPath(sourcepath)); deletionsuccess = true; }
        }
        else { if (File.Exists(Server.MapPath(destinationpath))) { File.Delete(Server.MapPath(destinationpath)); deletionsuccess = true; } }

        return deletionsuccess;
    }
    protected void btnDeleteQuestionFileSub_Click(object sender, EventArgs e)
    {
        if (txtFileName_sub.Value.Trim() != "")
        {
            if (DeleteFile(txtFileName_sub.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtFileName_sub.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteQuestionFileOption1_Click(object sender, EventArgs e)
    {
        if (txtFileName1.Value.Trim() != "")
        {
            if (DeleteFile(txtFileName1.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully..";  }
            else lblMessage.Text = "File not found..";
            txtFileName1.Value = "";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteQuestionFileOption2_Click(object sender, EventArgs e)
    {
        if (txtFileName2.Value.Trim() != "")
        {
            if (DeleteFile(txtFileName2.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully..";  }
            else lblMessage.Text = "File not found..";
            txtFileName2.Value = "";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteQuestionFileOption3_Click(object sender, EventArgs e)
    {
        if (txtFileName3.Value.Trim() != "")
        {
            if (DeleteFile(txtFileName3.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully..";  }
            else lblMessage.Text = "File not found..";
            txtFileName3.Value = "";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteQuestionFileOption4_Click(object sender, EventArgs e)
    {
        if (txtFileName4.Value.Trim() != "")
        {
            if (DeleteFile(txtFileName4.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully..";  }
            else lblMessage.Text = "File not found..";
            txtFileName4.Value = "";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteQuestionFileOption5_Click(object sender, EventArgs e)
    {
        if (txtFileName5.Value.Trim() != "")
        {
            if (DeleteFile(txtFileName5.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully..";  }
            else lblMessage.Text = "File not found..";
            txtFileName5.Value = "";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnAddMemTypeWords_Click(object sender, EventArgs e)
    {

    }
    protected void btnDeleteMemTypeImages_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeA.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeA.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeA.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages2_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeB.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeB.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeB.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages3_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeC.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeC.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeC.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages4_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeD.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeD.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeD.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages5_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeE.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeE.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeE.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages6_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeF.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeF.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeF.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages7_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeG.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeG.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeG.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages8_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeH.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeH.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeH.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages9_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeI.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeI.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeI.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages10_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeJ.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeJ.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeJ.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages11_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeK.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeK.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeK.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages12_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeL.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeL.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeL.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages13_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeM.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeM.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeM.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages14_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeN.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeN.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeN.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages15_Click(object sender, EventArgs e)
    {

        if (txtfilenameMemTypeO.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeO.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeO.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages16_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeP.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeP.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeP.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages17_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeQ.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeQ.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeQ.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages18_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeR.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeR.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeR.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages19_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeS.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeS.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeS.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    }
    protected void btnDeleteMemTypeImages20_Click(object sender, EventArgs e)
    {
        if (txtfilenameMemTypeT.Value.Trim() != "")
        {
            if (DeleteFile(txtfilenameMemTypeT.Value.Trim()) == true)
            { lblMessage.Text = "File deleted successfully.."; txtfilenameMemTypeT.Value = ""; }
            else lblMessage.Text = "File not found..";
        }
        else lblMessage.Text = "Please select a file";
    
    }

    private void FillWorTypeMemQuestionDetails()
    {
        ClearControls();
        if (Session["QuesID"] != null)
        {
             int qusid = int.Parse(Session["QuesID"].ToString());
            var Ques1 = from Ques in dataclass.MemmoryTestTextQuesCollections
                        where Ques.QuestionID == qusid
                        select Ques;
            if (Ques1.Count() > 0)
            {
                ddlCategory.SelectedValue = Ques1.First().Category.ToString();
                txtQues.Text = Ques1.First().Question.ToString();

                if (Ques1.First().SectionName != null)
                {
                    string sectionname = Ques1.First().SectionName.ToString();
                    for (int i = 0; i < ddlSectionList.Items.Count; i++)
                    {
                        if (ddlSectionList.Items[i].Text == sectionname)
                        {
                            ddlSectionList.SelectedIndex = i; Session["sectionIndex"] = i.ToString();
                            Session["SubLevel1Index"] = null; Session["SubLevel2Index"] = null;
                            FillSubLevel1Sections();
                            break;
                        }
                    }
                }
                if (Ques1.First().SectionNameSub1 != null)
                {
                    string sectionname = Ques1.First().SectionNameSub1.ToString();
                    for (int i = 0; i < ddlFirstLevelList.Items.Count; i++)
                    {
                        if (ddlFirstLevelList.Items[i].Text == sectionname)
                        {
                            ddlFirstLevelList.SelectedIndex = i; Session["SubLevel1Index"] = i.ToString();
                            Session["SubLevel2Index"] = null;
                            FillSubLevel2Sections();
                            break;
                        }
                    }
                }
                if (Ques1.First().SectionNameSub2 != null)
                {
                    string sectionname = Ques1.First().SectionNameSub2.ToString();
                    for (int i = 0; i < ddlSecondLevelList.Items.Count; i++)
                    {
                        if (ddlSecondLevelList.Items[i].Text == sectionname)
                        {
                            ddlSecondLevelList.SelectedIndex = i; Session["SubLevel2Index"] = i.ToString();
                            break;
                        }
                    }
                }

                // fill dyanamic words

                if (Ques1.First().Unit1 != null)
                    txtMemTypeWords.Text = Ques1.First().Unit1.ToString();
                if (Ques1.First().Unit2 != null)
                    txtMemTypeWord2.Text = Ques1.First().Unit2.ToString();
                if (Ques1.First().Unit3 != null)
                    txtMemTypeWord3.Text = Ques1.First().Unit3.ToString();
                if (Ques1.First().Unit4 != null)
                    txtMemTypeWord4.Text = Ques1.First().Unit4.ToString();
                if (Ques1.First().Unit5 != null)
                    txtMemTypeWord5.Text = Ques1.First().Unit5.ToString();
                if (Ques1.First().Unit6 != null)
                    txtMemTypeWord6.Text = Ques1.First().Unit6.ToString();
                if (Ques1.First().Unit7 != null)
                    txtMemTypeWord7.Text = Ques1.First().Unit7.ToString();
                if (Ques1.First().Unit8 != null)
                    txtMemTypeWord8.Text = Ques1.First().Unit8.ToString();
                if (Ques1.First().Unit9 != null)
                    txtMemTypeWord9.Text = Ques1.First().Unit9.ToString();
                if (Ques1.First().Unit10 != null)
                    txtMemTypeWord10.Text = Ques1.First().Unit10.ToString();
                if (Ques1.First().Unit11 != null)
                    txtMemTypeWord11.Text = Ques1.First().Unit11.ToString();
                if (Ques1.First().Unit12 != null)
                    txtMemTypeWord12.Text = Ques1.First().Unit12.ToString();
                if (Ques1.First().Unit13 != null)
                    txtMemTypeWord13.Text = Ques1.First().Unit13.ToString();
                if (Ques1.First().Unit14 != null)
                    txtMemTypeWord14.Text = Ques1.First().Unit14.ToString();
                if (Ques1.First().Unit15 != null)
                    txtMemTypeWord15.Text = Ques1.First().Unit15.ToString();
                if (Ques1.First().Unit16 != null)
                    txtMemTypeWord16.Text = Ques1.First().Unit16.ToString();
                if (Ques1.First().Unit17 != null)
                    txtMemTypeWord17.Text = Ques1.First().Unit17.ToString();
                if (Ques1.First().Unit18 != null)
                    txtMemTypeWord18.Text = Ques1.First().Unit18.ToString();
                if (Ques1.First().Unit19 != null)
                    txtMemTypeWord19.Text = Ques1.First().Unit19.ToString();
                if (Ques1.First().Unit20 != null)
                    txtMemTypeWord20.Text = Ques1.First().Unit20.ToString();

                //

                if (Ques1.First().Option1 != null)
                {
                    txtOption1.Text = Ques1.First().Option1.ToString();
                        if (Ques1.First().Option1.ToString() == Ques1.First().Answer.ToString())
                            ddlOption.SelectedIndex = 1;
                }
                if (Ques1.First().Option2 != null)
                {
                    txtOption2.Text = Ques1.First().Option2.ToString();
                        if (Ques1.First().Option2.ToString() == Ques1.First().Answer.ToString())
                            ddlOption.SelectedIndex = 2;
                }
                if (Ques1.First().Option3 != null)
                {
                    txtOption3.Text = Ques1.First().Option3.ToString();
                        if (Ques1.First().Option3.ToString() == Ques1.First().Answer.ToString())
                            ddlOption.SelectedIndex = 3;
                }
                if (Ques1.First().Option4 != null)
                {
                    txtOption4.Text = Ques1.First().Option4.ToString();
                        if (Ques1.First().Option4.ToString() == Ques1.First().Answer.ToString())
                            ddlOption.SelectedIndex = 4;
                }
                if (Ques1.First().Option5 != null)
                {
                    txtOption5.Text = Ques1.First().Option5.ToString();                    
                        if (Ques1.First().Option5.ToString() == Ques1.First().Answer.ToString())
                            ddlOption.SelectedIndex = 5;
                }

                if (Ques1.First().DisplayDuration != null)
                    txtWordDisplayDuration.Text = Ques1.First().DisplayDuration.ToString();
                if (Ques1.First().DisplayType != null)
                    ddlDisplayType_Words.SelectedValue = Ques1.First().DisplayType.ToString();
            }
        }
    }

    protected void gvwWordTypeMemQuestions_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["QuesID"] = gvwWordTypeMemQuestions.SelectedRow.Cells[6].Text;//16102011
        FillWorTypeMemQuestionDetails();
        FillGrid();
    }

    private void FillImageTypeMemQuestionType()
    {
        ClearControls();
        if (Session["QuesID"] != null)
        {
            int qusid = int.Parse(Session["QuesID"].ToString());
            var Ques1 = from Ques in dataclass.MemmoryTestImageQuesCollections
                        where Ques.QuestionID == qusid
                        select Ques;
            if (Ques1.Count() > 0)
            {
                ddlCategory.SelectedValue = Ques1.First().Category.ToString();
                txtQues.Text = Ques1.First().Question.ToString();

                if (Ques1.First().SectionName != null)
                {
                    string sectionname = Ques1.First().SectionName.ToString();
                    for (int i = 0; i < ddlSectionList.Items.Count; i++)
                    {
                        if (ddlSectionList.Items[i].Text == sectionname)
                        {
                            ddlSectionList.SelectedIndex = i; Session["sectionIndex"] = i.ToString();
                            Session["SubLevel1Index"] = null; Session["SubLevel2Index"] = null;
                            FillSubLevel1Sections();
                            break;
                        }
                    }
                }
                if (Ques1.First().SectionNameSub1 != null)
                {
                    string sectionname = Ques1.First().SectionNameSub1.ToString();
                    for (int i = 0; i < ddlFirstLevelList.Items.Count; i++)
                    {
                        if (ddlFirstLevelList.Items[i].Text == sectionname)
                        {
                            ddlFirstLevelList.SelectedIndex = i; Session["SubLevel1Index"] = i.ToString();
                            Session["SubLevel2Index"] = null;
                            FillSubLevel2Sections();
                            break;
                        }
                    }
                }
                if (Ques1.First().SectionNameSub2 != null)
                {
                    string sectionname = Ques1.First().SectionNameSub2.ToString();
                    for (int i = 0; i < ddlSecondLevelList.Items.Count; i++)
                    {
                        if (ddlSecondLevelList.Items[i].Text == sectionname)
                        {
                            ddlSecondLevelList.SelectedIndex = i; Session["SubLevel2Index"] = i.ToString();
                            break;
                        }
                    }
                }
                //dispaly dynamic images
                if (Ques1.First().Image1 != null)
                    txtfilenameMemTypeA.Value = Ques1.First().Image1.ToString();
                if (Ques1.First().Image2 != null)
                    txtfilenameMemTypeB.Value = Ques1.First().Image2.ToString();
                if (Ques1.First().Image3 != null)
                    txtfilenameMemTypeC.Value = Ques1.First().Image3.ToString();
                if (Ques1.First().Image4 != null)
                    txtfilenameMemTypeD.Value = Ques1.First().Image4.ToString();
                if (Ques1.First().Image5 != null)
                    txtfilenameMemTypeE.Value = Ques1.First().Image5.ToString();
                if (Ques1.First().Image6 != null)
                    txtfilenameMemTypeF.Value = Ques1.First().Image6.ToString();
                if (Ques1.First().Image7 != null)
                    txtfilenameMemTypeG.Value = Ques1.First().Image7.ToString();
                if (Ques1.First().Image8 != null)
                    txtfilenameMemTypeH.Value = Ques1.First().Image8.ToString();
                if (Ques1.First().Image9 != null)
                    txtfilenameMemTypeI.Value = Ques1.First().Image9.ToString();
                if (Ques1.First().Image10 != null)
                    txtfilenameMemTypeJ.Value = Ques1.First().Image10.ToString();
                if (Ques1.First().Image11 != null)
                    txtfilenameMemTypeK.Value = Ques1.First().Image11.ToString();
                if (Ques1.First().Image12 != null)
                    txtfilenameMemTypeL.Value = Ques1.First().Image12.ToString();
                if (Ques1.First().Image13 != null)
                    txtfilenameMemTypeM.Value = Ques1.First().Image13.ToString();
                if (Ques1.First().Image14 != null)
                    txtfilenameMemTypeN.Value = Ques1.First().Image14.ToString();
                if (Ques1.First().Image15 != null)
                    txtfilenameMemTypeO.Value = Ques1.First().Image15.ToString();
                if (Ques1.First().Image16 != null)
                    txtfilenameMemTypeP.Value = Ques1.First().Image16.ToString();
                if (Ques1.First().Image17 != null)
                    txtfilenameMemTypeQ.Value = Ques1.First().Image17.ToString();
                if (Ques1.First().Image18 != null)
                    txtfilenameMemTypeR.Value = Ques1.First().Image18.ToString();
                if (Ques1.First().Image19 != null)
                    txtfilenameMemTypeS.Value = Ques1.First().Image19.ToString();
                if (Ques1.First().Image20 != null)
                    txtfilenameMemTypeT.Value = Ques1.First().Image20.ToString();

                // word options ...

                if (Ques1.First().Option1 != null)
                {
                    txtOption1.Text = Ques1.First().Option1.ToString();
                    if (Ques1.First().Option1.ToString() == Ques1.First().Answer.ToString())
                        ddlOption.SelectedIndex = 1;
                }
                if (Ques1.First().Option2 != null)
                {
                    txtOption2.Text = Ques1.First().Option2.ToString();
                    if (Ques1.First().Option2.ToString() == Ques1.First().Answer.ToString())
                        ddlOption.SelectedIndex = 2;
                }
                if (Ques1.First().Option3 != null)
                {
                    txtOption3.Text = Ques1.First().Option3.ToString();
                    if (Ques1.First().Option3.ToString() == Ques1.First().Answer.ToString())
                        ddlOption.SelectedIndex = 3;
                }
                if (Ques1.First().Option4 != null)
                {
                    txtOption4.Text = Ques1.First().Option4.ToString();
                    if (Ques1.First().Option4.ToString() == Ques1.First().Answer.ToString())
                        ddlOption.SelectedIndex = 4;
                }
                if (Ques1.First().Option5 != null)
                {
                    txtOption5.Text = Ques1.First().Option5.ToString();
                    if (Ques1.First().Option5.ToString() == Ques1.First().Answer.ToString())
                        ddlOption.SelectedIndex = 5;
                }
                //image options
                if (Ques1.First().OptionFile1 != null)
                {
                    txtFileName1.Value = Ques1.First().OptionFile1.ToString();
                    if (Ques1.First().Answer != null)
                        if (Ques1.First().Answer.ToString() == "1")
                            ddlOption.SelectedIndex = 1;
                }
                if (Ques1.First().OptionFile2 != null)
                {
                    txtFileName2.Value = Ques1.First().OptionFile2.ToString();
                    if (Ques1.First().Answer != null)
                        if (Ques1.First().Answer.ToString() == "2")
                            ddlOption.SelectedIndex = 2;
                }
                if (Ques1.First().OptionFile3 != null)
                {
                    txtFileName3.Value = Ques1.First().OptionFile3.ToString();
                    if (Ques1.First().Answer != null)
                        if (Ques1.First().Answer.ToString() == "3")
                            ddlOption.SelectedIndex = 3;
                }
                if (Ques1.First().OptionFile4 != null)
                {
                    txtFileName4.Value = Ques1.First().OptionFile4.ToString();
                    if (Ques1.First().Answer != null)
                        if (Ques1.First().Answer.ToString() == "4")
                            ddlOption.SelectedIndex = 4;
                }
                if (Ques1.First().OptionFile5 != null)
                {
                    txtFileName5.Value = Ques1.First().OptionFile5.ToString();
                    if (Ques1.First().Answer != null)
                        if (Ques1.First().Answer.ToString() == "5")
                            ddlOption.SelectedIndex = 5;
                }

                if (Ques1.First().DisplayDuration != null)
                    txtImageDisplayDuration.Text = Ques1.First().DisplayDuration.ToString();
                if (Ques1.First().DisplayType != null)
                    ddlDisplayType_Images.SelectedValue = Ques1.First().DisplayType.ToString();
            }
        }
    }

    protected void gvwImageTypeMemQuestions_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["QuesID"] = gvwImageTypeMemQuestions.SelectedRow.Cells[6].Text;//16102011
        FillImageTypeMemQuestionType();
        FillGrid();
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

        /*
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
        }
        */

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
}

