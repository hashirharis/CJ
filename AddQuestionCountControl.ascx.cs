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

public partial class AddQuestionCountControl : System.Web.UI.UserControl
{
    DBManagementClass clsClasses = new DBManagementClass();
   AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
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
               var orgName = from orgDet in dataclasses.Organizations
                             where orgDet.OrganizationID == OrganizationID
                             select orgDet;
               if (orgName.Count() > 0)
               { organizationName = orgName.First().Name.ToString(); Session["adminOrgName"] = organizationName; }

           }
           FillTestList();
       }
       else
           FillOrganizationList();

        FillSessionslist();        
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
            if (ddlTestSectionList.SelectedIndex > 0)// bip 111009
            {
                string querystring = "select distinct SectionName,SectionId from View_FirstLevelSectionList where ParentId=0 and TestId=" + ddlTestList.SelectedValue + " and TestSectionId=" + ddlTestSectionList.SelectedValue;
                DataSet dsFirstLevelSectionLists = new DataSet();
                dsFirstLevelSectionLists = clsClasses.GetValuesFromDB(querystring);
                if (dsFirstLevelSectionLists != null)
                {
                    ddlSectionList.DataSource = dsFirstLevelSectionLists.Tables[0];
                    ddlSectionList.DataTextField = "SectionName";
                    ddlSectionList.DataValueField = "SectionId";
                    ddlSectionList.DataBind();
                }

                if (sectionIndex > 0)
                {
                    ddlSectionList.SelectedIndex = sectionIndex;
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

        if (ddlTestSectionList.SelectedIndex > 0 && ddlSectionList.SelectedIndex > 0)// bip 111009
        {

            string querystring = "select distinct SectionName,SectionId from View_SecondLevelSectionList where ParentId=" + ddlSectionList.SelectedValue + " and TestId=" + ddlTestList.SelectedValue + " and TestSectionId=" + ddlTestSectionList.SelectedValue;
            DataSet dsFirstLevelSectionLists = new DataSet();
            dsFirstLevelSectionLists = clsClasses.GetValuesFromDB(querystring);
            if (dsFirstLevelSectionLists != null)
            {
                ddlFirstLevelList.DataSource = dsFirstLevelSectionLists.Tables[0];
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
       
        if (ddlTestSectionList.SelectedIndex > 0 && ddlFirstLevelList.SelectedIndex > 0)// bip 111009
        {
            string querystring = "select distinct SectionName,SectionId from View_ThirdLevelSectionList where ParentId=" + ddlFirstLevelList.SelectedValue + " and TestId=" + ddlTestList.SelectedValue + " and TestSectionId=" + ddlTestSectionList.SelectedValue;
            DataSet dsFirstLevelSectionLists = new DataSet();
            dsFirstLevelSectionLists = clsClasses.GetValuesFromDB(querystring);
            if (dsFirstLevelSectionLists != null)
            {
                ddlSecondLevelList.DataSource = dsFirstLevelSectionLists.Tables[0];
                ddlSecondLevelList.DataTextField = "SectionName";
                ddlSecondLevelList.DataValueField = "SectionId";
                ddlSecondLevelList.DataBind();
            }

            if (subindex > 0)
                ddlSecondLevelList.SelectedIndex = subindex;
           
        }
    }
    private bool CheckChildExists(int parentid)
    {
         var details1 = from details in dataclasses.SectionDetails
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
        { lblMesasage.Text = "Please select a section variable from the list"; variableselected = false; }
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
                            lblMesasage.Text = "Please select a 2nd level sub variable from the list";
                        }
                    }
                }
                else
                {// code to check wheather there is a sublevel entries under this selected value.

                    if (CheckChildExists(int.Parse(ddlSectionList.SelectedValue)) == true)
                    {
                        variableselected = false;
                        lblMesasage.Text = "Please select a 1st level sub variable from the list";
                    }
                }               

            }
            else { lblMesasage.Text = "Please select a section from the list"; variableselected = false; }

        }

        return variableselected;

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int testId = 0;
        int organizationid = 0;
        if (validateSelection() == true)
            if (ValidateCount() == true)
            {

                if (ddlTestList.SelectedIndex > 0)
                    testId = int.Parse(ddlTestList.SelectedValue);
                else { lblMesasage.Text = "Please select a Test from the list .."; return; }
                int testsectionid = 0;
                if (ddlTestSectionList.SelectedIndex > 0)
                    testsectionid = int.Parse(ddlTestSectionList.SelectedValue);
                else { lblMesasage.Text = "Please select a Test section name from the list .."; return; }

                organizationid = int.Parse(ddlOrganizationList.SelectedValue);

                int userid = 0; if (Session["UserID"] != null) userid = int.Parse(Session["UserID"].ToString());
                int id = 0;
                if (Session["countid"] != null) id = int.Parse(Session["countid"].ToString());
                int sectionid = 0;
                string sectionname = "", subsection1name = "", subsection2name = "";
                if (ddlSecondLevelList.SelectedIndex > 0)
                {
                    sectionid = int.Parse(ddlSecondLevelList.SelectedValue);
                    sectionname = ddlSectionList.SelectedItem.Text;
                    subsection1name = ddlFirstLevelList.SelectedItem.Text;
                    subsection2name = ddlSecondLevelList.SelectedItem.Text;
                }
                else if (ddlFirstLevelList.SelectedIndex > 0)
                {
                    sectionid = int.Parse(ddlFirstLevelList.SelectedValue);

                    sectionname = ddlSectionList.SelectedItem.Text;
                    subsection1name = ddlFirstLevelList.SelectedItem.Text;
                    subsection2name = "";
                }
                else if (ddlSectionList.SelectedIndex > 0)
                {
                    sectionid = int.Parse(ddlSectionList.SelectedValue);

                    sectionname = ddlSectionList.SelectedItem.Text;
                    subsection1name = "";
                    subsection2name = "";
                }

                int objcount = 0, fillblanksCount = 0; int ratingquestioncount = 0;
                int imagequescount = 0; int videoquescount = 0; int audioquescount = 0; int wordtypememQuestioncount = 0; int imagetypememQuestioncount = 0;
                int photoquescount = 0;
                
                if (txtobjQuestionCount.Text.Trim() != "")
                    objcount = int.Parse(txtobjQuestionCount.Text.Trim());
                if (txtfillBlanksQuestionCount.Text.Trim() != "")
                    fillblanksCount = int.Parse(txtfillBlanksQuestionCount.Text.Trim());
                if (txtRatingQuestionCount.Text.Trim() != "")
                    ratingquestioncount = int.Parse(txtRatingQuestionCount.Text.Trim());
                if (txtImageQuestionCount.Text.Trim() != "")
                    imagequescount = int.Parse(txtImageQuestionCount.Text.Trim());
                if (txtVideoQuestionCount.Text.Trim() != "")
                    videoquescount = int.Parse(txtVideoQuestionCount.Text.Trim());
                if (txtAudioQuestionCount.Text.Trim() != "")
                    audioquescount = int.Parse(txtAudioQuestionCount.Text.Trim());
                if (txtWordTypeMemQuestionCount.Text.Trim() != "")
                    wordtypememQuestioncount = int.Parse(txtWordTypeMemQuestionCount.Text.Trim());
                if (txtImageTypeMemQuestionCount.Text.Trim() != "")
                    imagetypememQuestioncount = int.Parse(txtImageTypeMemQuestionCount.Text.Trim());
                if (txtPhotoTypeQuestionCount.Text.Trim() != "")
                    photoquescount = int.Parse(txtPhotoTypeQuestionCount.Text.Trim());

                dataclasses.Procedure_AddQuestionCount(id, sectionid, sectionname, subsection1name, subsection2name, objcount, fillblanksCount, ratingquestioncount, imagequescount, videoquescount, audioquescount, userid, organizationid, testId,testsectionid,wordtypememQuestioncount,imagetypememQuestioncount,photoquescount);
                lblMesasage.Text = "Saved Successfully..";
                ClearTextBoxes(); 
                gvwQuestionCount.DataBind();
               // ClearListBoxes();
            }
    }

    private void ClearTextBoxes()
    {
        txtfillBlanksQuestionCount.Text = ""; txtobjQuestionCount.Text = ""; txtRatingQuestionCount.Text = "";
        txtAudioQuestionCount.Text = ""; txtVideoQuestionCount.Text = ""; txtImageQuestionCount.Text = "";
        txtImageTypeMemQuestionCount.Text = ""; txtWordTypeMemQuestionCount.Text = ""; txtPhotoTypeQuestionCount.Text = "";
        Session["countid"] = null;
    }
    private void ClearListBoxes()
    {
        Session["sectionIndex"] = null; ddlSectionList.SelectedIndex = 0;
        Session["SubLevel1Index"] = null; Session["SubLevel2Index"] = null;
        ListItem listname;
        listname = new ListItem("-- select --", "0");
        ddlFirstLevelList.Items.Clear();
        ddlFirstLevelList.Items.Add(listname);
        listname = new ListItem("-- select --", "0");
        ddlSecondLevelList.Items.Clear();
        ddlSecondLevelList.Items.Add(listname);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtfillBlanksQuestionCount.Text = ""; txtobjQuestionCount.Text = "";
        Session["SubLevel1Index"] = null; Session["sectionIndex"] = null;
        ClearTextBoxes(); FillSessionslist(); ClearListBoxes();
    }
    private bool ValidateCount()
    {
        bool validentries = true;
        if (txtobjQuestionCount.Text.Trim() == "" && txtfillBlanksQuestionCount.Text.Trim() == "" && txtRatingQuestionCount.Text.Trim() == "" && txtImageQuestionCount.Text.Trim() == "" && txtVideoQuestionCount.Text.Trim() == "" && txtAudioQuestionCount.Text.Trim() == "" && txtWordTypeMemQuestionCount.Text.Trim()=="" && txtImageTypeMemQuestionCount.Text.Trim()=="" && txtPhotoTypeQuestionCount.Text.Trim()=="")
        { lblMesasage.Text = "Please enter questions count"; validentries = false; }
        //else if (txtobjQuestionCount.Text.Trim() == "0" && txtfillBlanksQuestionCount.Text.Trim() == "0" && txtRatingQuestionCount.Text.Trim() == "0" && txtImageQuestionCount.Text.Trim() == "0" && txtVideoQuestionCount.Text.Trim() == "0" && txtAudioQuestionCount.Text.Trim() == "0" && txtWordTypeMemQuestionCount.Text.Trim() == "0" && txtImageTypeMemQuestionCount.Text.Trim() == "0" && txtPhotoTypeQuestionCount.Text.Trim() == "0")
        //{ lblMesasage.Text = "Please enter questions count"; validentries = false; }
        else
        {
            try
            {
                int count = 0;
                if (txtfillBlanksQuestionCount.Text.Trim() != "")
                    count = int.Parse(txtfillBlanksQuestionCount.Text.Trim());
                if (txtobjQuestionCount.Text.Trim() != "")
                    count = int.Parse(txtobjQuestionCount.Text.Trim());
                if (txtRatingQuestionCount.Text.Trim() != "")
                    count = int.Parse(txtRatingQuestionCount.Text.Trim());

                if (txtImageQuestionCount.Text.Trim() != "")
                    count = int.Parse(txtImageQuestionCount.Text.Trim());
                if (txtVideoQuestionCount.Text.Trim() != "")
                    count = int.Parse(txtVideoQuestionCount.Text.Trim());
                if (txtAudioQuestionCount.Text.Trim() != "")
                    count = int.Parse(txtAudioQuestionCount.Text.Trim());
                if (txtImageTypeMemQuestionCount.Text.Trim() != "")
                    count = int.Parse(txtImageTypeMemQuestionCount.Text.Trim());
                if (txtWordTypeMemQuestionCount.Text.Trim() != "")
                    count = int.Parse(txtWordTypeMemQuestionCount.Text.Trim());
                if (txtPhotoTypeQuestionCount.Text.Trim() != "")
                    count = int.Parse(txtPhotoTypeQuestionCount.Text.Trim());

            }
            catch (Exception ex) { lblMesasage.Text = "Please enter a valid number"; validentries = false; }
        }
        return validentries;
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

    protected void gvwQuestionCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["OrgIndex_Count"] = null; Session["TestIndex_Count"] = null;
        ddlTestList.SelectedIndex = 0; ddlOrganizationList.SelectedIndex = 0;
        if (gvwQuestionCount.SelectedRow.Cells[14].Text != "&nbsp;")
            for (int i = 0; i < ddlOrganizationList.Items.Count; i++)
            {
                if (ddlOrganizationList.Items[i].Value == gvwQuestionCount.SelectedRow.Cells[14].Text)
                { ddlOrganizationList.SelectedIndex = i; Session["OrgIndex_Count"] = i.ToString(); FillTestList(); break; }
            }
        if (gvwQuestionCount.SelectedRow.Cells[15].Text != "&nbsp;")
            for (int i = 0; i < ddlTestList.Items.Count; i++)
            {
                if (ddlTestList.Items[i].Value == gvwQuestionCount.SelectedRow.Cells[15].Text)
                { ddlTestList.SelectedIndex = i; Session["TestIndex_Count"] = i.ToString(); FillTestSectionList(); break; }
            }
        if (gvwQuestionCount.SelectedRow.Cells[17].Text != "&nbsp;")
            for (int i = 0; i < ddlTestSectionList.Items.Count; i++)
            {
                if (ddlTestSectionList.Items[i].Value == gvwQuestionCount.SelectedRow.Cells[17].Text)
                { ddlTestSectionList.SelectedIndex = i; Session["TestSectionIndex"] = i.ToString(); FillSessionslist(); break; }
            }

        lblMesasage.Text = gvwQuestionCount.SelectedRow.Cells[16].Text;

        Session["countid"] = gvwQuestionCount.SelectedRow.Cells[16].Text;
        for (int i = 0; i < ddlSectionList.Items.Count; i++)
        {
            if (ddlSectionList.Items[i].Text == gvwQuestionCount.SelectedRow.Cells[1].Text)
            {
                ddlSectionList.SelectedIndex = i; Session["sectionIndex"] = i.ToString(); FillSubLevel1Sections();

                for (int j = 0; j < ddlFirstLevelList.Items.Count; j++)
                {
                    if (ddlFirstLevelList.Items[j].Text == gvwQuestionCount.SelectedRow.Cells[2].Text)
                    {
                        ddlFirstLevelList.SelectedIndex = j; Session["SubLevel1Index"] = j.ToString(); FillSubLevel2Sections();

                        for (int k = 0; k < ddlSecondLevelList.Items.Count; k++)
                        {
                            if (ddlSecondLevelList.Items[k].Text == gvwQuestionCount.SelectedRow.Cells[3].Text)
                            { ddlSecondLevelList.SelectedIndex = k; break; }
                        }

                       // break;
                    }
                }
                if (gvwQuestionCount.SelectedRow.Cells[4].Text != "&nbsp;") txtobjQuestionCount.Text = gvwQuestionCount.SelectedRow.Cells[4].Text;
                if (gvwQuestionCount.SelectedRow.Cells[5].Text != "&nbsp;") txtfillBlanksQuestionCount.Text = gvwQuestionCount.SelectedRow.Cells[5].Text;
                if (gvwQuestionCount.SelectedRow.Cells[6].Text != "&nbsp;") txtRatingQuestionCount.Text = gvwQuestionCount.SelectedRow.Cells[6].Text;
                if (gvwQuestionCount.SelectedRow.Cells[7].Text != "&nbsp;") txtImageQuestionCount.Text = gvwQuestionCount.SelectedRow.Cells[7].Text;
                if (gvwQuestionCount.SelectedRow.Cells[8].Text != "&nbsp;") txtVideoQuestionCount.Text = gvwQuestionCount.SelectedRow.Cells[8].Text;
                if (gvwQuestionCount.SelectedRow.Cells[9].Text != "&nbsp;") txtAudioQuestionCount.Text = gvwQuestionCount.SelectedRow.Cells[9].Text;
                if (gvwQuestionCount.SelectedRow.Cells[10].Text != "&nbsp;") txtWordTypeMemQuestionCount.Text = gvwQuestionCount.SelectedRow.Cells[10].Text;
                if (gvwQuestionCount.SelectedRow.Cells[11].Text != "&nbsp;") txtImageTypeMemQuestionCount.Text = gvwQuestionCount.SelectedRow.Cells[11].Text;
                if (gvwQuestionCount.SelectedRow.Cells[12].Text != "&nbsp;") txtPhotoTypeQuestionCount.Text = gvwQuestionCount.SelectedRow.Cells[12].Text;

                break;
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["countid"] != null)
        {
            int id = int.Parse(Session["countid"].ToString());
            dataclasses.Procedure_DeleteQuestionCount(id);
            lblMesasage.Text = "Deletion successfull";
            ClearTextBoxes(); //ClearListBoxes();
            FillQuestionCountDetails();
        }
        else lblMesasage.Text = "Please select a value from the list";
    }

    protected void ddlOrganizationList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganizationList.SelectedIndex > 0)
        {
            Session["OrgIndex_Count"] = ddlOrganizationList.SelectedIndex.ToString();
            FillTestList();
        }
        else
        {
            Session["OrgIndex_Count"] = null; Session["TestIndex_Count"] = null;Session["TestSectionIndex"] = null;
            
            ListItem listnew;
            listnew = new ListItem("--select--", "0");
            ddlTestList.Items.Clear();
            ddlTestList.Items.Add(listnew);

            FillTestSectionList(); FillQuestionCountDetails();
        }
    }
    protected void ddlTestList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestList.SelectedIndex > 0)
        { Session["TestIndex_Count"] = ddlTestList.SelectedIndex.ToString(); FillTestSectionList();  }
        else { Session["TestIndex_Count"] = null; Session["TestSectionIndex"] = null;  FillTestSectionList(); FillQuestionCountDetails();}
        
    }
    private void FillOrganizationList()
    {
        ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlOrganizationList.Items.Clear();
        ddlOrganizationList.Items.Add(listnew);
        int orgIndex = 0;

        if (Session["OrgIndex_Count"] != null)
        {
            orgIndex = int.Parse(Session["OrgIndex_Count"].ToString());
        }
        LinqOrganizationList.Where = "Status=1";
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
        ddlTestList.Items.Clear();
        ddlTestList.Items.Add(listnew);
        int testIndex = 0;int adminaccess=0;

        if (Session["TestIndex_Count"] != null)
        {
            testIndex = int.Parse(Session["TestIndex_Count"].ToString());
        }
        if(specialadmin==false)
        {organizationName=ddlOrganizationList.SelectedItem.Text;adminaccess=1;}

        var testListDetails = from testlistsdet in dataclasses.TestLists
                              where testlistsdet.OrganizationName == organizationName && testlistsdet.AdminAccess == adminaccess
                              select testlistsdet;

        ddlTestList.DataSource = testListDetails;
        ddlTestList.DataTextField = "TestName";
        ddlTestList.DataValueField = "TestId";
        ddlTestList.DataBind();
        if (testIndex > 0)
        { ddlTestList.SelectedIndex = testIndex; FillTestSectionList();  }//FillQuestionCountDetails();

    }
    protected void ddlTestSectionList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestSectionList.SelectedIndex > 0)
        { Session["TestSectionIndex"] = ddlTestSectionList.SelectedIndex.ToString(); FillSessionslist(); FillQuestionCountDetails(); }
        else Session["TestSectionIndex"] = null;
    }
    private void FillTestSectionList()
    {
        ddlTestSectionList.Items.Clear();
        ListItem litem = new ListItem("-- Select --", "0");
        ddlTestSectionList.Items.Add(litem);
        if (ddlTestList.SelectedIndex > 0)
        {
            LinqTestSectionList.Where = "TestId=" + ddlTestList.SelectedValue;
            ddlTestSectionList.DataSource = LinqTestSectionList;
            ddlTestSectionList.DataTextField = "SectionName";
            ddlTestSectionList.DataValueField = "TestSectionId";
            ddlTestSectionList.DataBind();
            if (Session["TestSectionIndex"] != null)
            {
                ddlTestSectionList.SelectedIndex = int.Parse(Session["TestSectionIndex"].ToString());
                FillSessionslist();
                FillQuestionCountDetails();
            }
        }
        else { FillSessionslist(); }
    }

    private void FillQuestionCountDetails()
    {
        LinqQuestionCount.Where = "TestId=0";
        gvwQuestionCount.DataSource = LinqQuestionCount;
        gvwQuestionCount.DataBind();
        bool testselected = false;

        if (ddlTestSectionList.SelectedIndex > 0)
        {
            LinqQuestionCount.Where = "TestId=" + ddlTestList.SelectedValue + " and TestSectionId=" + ddlTestSectionList.SelectedValue;
            gvwQuestionCount.DataSource = LinqQuestionCount;
            gvwQuestionCount.DataBind();
            testselected = true;
        }
    }
    protected void ddlSecondLevelList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // code to set question count in the text boxes
        setQuestionCount();
    }
    private void setQuestionCount()
    {
        // code to set question count in the text boxes
       // ClearTextBoxes();

    }
    
}
