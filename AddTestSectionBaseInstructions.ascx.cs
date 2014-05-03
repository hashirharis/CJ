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

public partial class AddTestSectionBaseInstructions : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    bool specialadmin = false;
    int OrganizationID = 0;
    string organizationName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {
            lblOrganization.Visible = false; ddlOrganizations.Visible = false;
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
    }
    private void FillOrganizationList()
    {
        ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlOrganizations.Items.Clear();
        ddlOrganizations.Items.Add(listnew);
        int orgIndex = 0;

        if (Session["OrgIndex_Intro"] != null)
        {
            orgIndex = int.Parse(Session["OrgIndex_Intro"].ToString());
        }
        ddlOrganizations.DataSource = LinqOrganizationList;
        ddlOrganizations.DataTextField = "Name";
        ddlOrganizations.DataValueField = "OrganizationID";
        ddlOrganizations.DataBind();
        if (orgIndex > 0)
        { ddlOrganizations.SelectedIndex = orgIndex; FillTestList(); }
        else { FreeTextBox1.Text = ""; FillInstructionDetails(); }
    }

    private void FillTestList()
    {
        ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlTestNameList.Items.Clear();
        ddlTestNameList.Items.Add(listnew);
        int testIndex = 0;
        if (Session["TestIndex_Intro"] != null)
        {
            testIndex = int.Parse(Session["TestIndex_Intro"].ToString());
        }
        if (specialadmin == false)
            organizationName = ddlOrganizations.SelectedItem.Text;

        var testListDetails = from testlistsdet in dataclasses.TestLists
                              where testlistsdet.OrganizationName == organizationName
                              select testlistsdet;

        //LinqTestList.Where="OrganizationName=" + ddlOrganizationList.SelectedItem.Text;
        //ddlTestName.DataSource = LinqTestList;
        ddlTestNameList.DataSource = testListDetails;
        ddlTestNameList.DataTextField = "TestName";
        ddlTestNameList.DataValueField = "TestId";
        ddlTestNameList.DataBind();
        if (testIndex > 0)
        { ddlTestNameList.SelectedIndex = testIndex; FillTestSectionList(); }
        else { FreeTextBox1.Text = ""; FillInstructionDetails(); }
    }
    private void FillTestSectionList()
    {
        ddlTestSectionList.Items.Clear();
        ListItem litem = new ListItem("-- Select --", "0");
        ddlTestSectionList.Items.Add(litem);
        if (ddlTestNameList.SelectedIndex > 0)
        {
            LinqTestSectionList.Where = "TestId=" + ddlTestNameList.SelectedValue;
            ddlTestSectionList.DataSource = LinqTestSectionList;
            ddlTestSectionList.DataTextField = "SectionName";
            ddlTestSectionList.DataValueField = "TestSectionId";
            ddlTestSectionList.DataBind();
            if (Session["TestSectionIndex"] != null)
            {
                ddlTestSectionList.SelectedIndex = int.Parse(Session["TestSectionIndex"].ToString());               
                FillInstructionDetails();
            }
            else { FreeTextBox1.Text = ""; FillInstructionDetails(); }
        }
    }
     
    private void FillInstructionDetails()
    {
        if (ddlTestSectionList.SelectedIndex > 0)
        {
            if (ddlCategory.SelectedIndex > 0)
            {
                var Instructiondetails = from instructiondet in dataclasses.TestSectionInstructions
                                         where instructiondet.OrganizationId == int.Parse(ddlOrganizations.SelectedValue) &&
                                         instructiondet.TestId == int.Parse(ddlTestNameList.SelectedValue) &&
                                         instructiondet.TestSectionId == int.Parse(ddlTestSectionList.SelectedValue) &&
                                         instructiondet.CategoryId == int.Parse(ddlCategory.SelectedValue)
                                         select instructiondet;
                if (Instructiondetails.Count() > 0)
                {
                    if (Instructiondetails.First().InstructionDetails != null)
                        FreeTextBox1.Text = Instructiondetails.First().InstructionDetails.ToString();
                    if (Instructiondetails.First().Status != null)
                        if (Instructiondetails.First().Status.ToString() == "1")
                            ddlStatus.SelectedIndex = 0;
                        else ddlStatus.SelectedIndex = 1;
                }
                //else                
                //    FreeTextBox1.Text = "";                   
                
            }
            //else
            //    FreeTextBox1.Text = "";
        }
        //else
        //    FreeTextBox1.Text = "";     
        
    }

    protected void ddlOrganizations_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganizations.SelectedIndex > 0)
        { Session["OrgIndex_Intro"] = ddlOrganizations.SelectedIndex.ToString(); FillTestList(); }
        else Session["OrgIndex_Intro"] = null;
    }
    protected void ddlTestNameList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestNameList.SelectedIndex > 0)
        { Session["TestIndex_Intro"] = ddlTestNameList.SelectedIndex.ToString(); FillTestSectionList(); }
        else Session["TestIndex_Intro"] = null;
    }
    protected void ddlTestSectionList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestSectionList.SelectedIndex > 0)
        { Session["TestSectionIndex"] = ddlTestSectionList.SelectedIndex.ToString(); FillInstructionDetails(); }
        else Session["TestSectionIndex"] = null;
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex > 0)
        {
            if (Session["categoryIndex"] != null)
                if (Session["categoryIndex"].ToString() != ddlCategory.SelectedIndex.ToString())
                    FreeTextBox1.Text = "";
            Session["categoryIndex"] = ddlCategory.SelectedIndex.ToString();  FillInstructionDetails(); }
        else
        {
            FreeTextBox1.Text = "";
            Session["categoryIndex"] = null;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int userid = 0; int category = 0; int status = 0; int organizationId = 0, testId = 0, testSectionId = 0;
        status = int.Parse(ddlStatus.SelectedValue);
        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());
        if (ddlCategory.SelectedIndex > 0 && FreeTextBox1.Text.Trim() != "" && ddlTestSectionList.SelectedIndex>0 && ddlTestNameList.SelectedIndex>0 && ddlOrganizations.SelectedIndex>0)
        {
            organizationId = int.Parse(ddlOrganizations.SelectedValue); testId = int.Parse(ddlTestNameList.SelectedValue);
            testSectionId = int.Parse(ddlTestSectionList.SelectedValue); category = int.Parse(ddlCategory.SelectedValue);
            dataclasses.Procedure_TestSectionInstructions(0, FreeTextBox1.Text, organizationId, testId, testSectionId, category, status, userid);

            FreeTextBox1.Text = ""; ddlCategory.SelectedIndex = 0;
            lblMessage.Text = "Saved Successfully";
            Session["categoryIndex"] = null;
        }
        else { lblMessage.Text = "Please select required fields"; }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlOrganizations.SelectedIndex = 0; ddlTestNameList.SelectedIndex = 0; ddlTestSectionList.SelectedIndex = 0; ddlCategory.SelectedIndex = 0;
        Session["OrgIndex_Intro"] = null; Session["TestIndex_Intro"] = null; Session["TestSectionIndex"] = null; Session["categoryIndex"] = null;
        FreeTextBox1.Text = "";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex > 0 && FreeTextBox1.Text.Trim() != "" && ddlTestSectionList.SelectedIndex > 0 && ddlTestNameList.SelectedIndex > 0 && ddlOrganizations.SelectedIndex > 0)
        {
            int category = 0, organizationId = 0, testId = 0, testSectionId = 0;
            organizationId = int.Parse(ddlOrganizations.SelectedValue); testId = int.Parse(ddlTestNameList.SelectedValue);
            testSectionId = int.Parse(ddlTestSectionList.SelectedValue); category = int.Parse(ddlCategory.SelectedValue);
            dataclasses.Procedure_DeleteTestSectionInstructions(organizationId, testId, testSectionId, category);
        }
        else { lblMessage.Text = "Please select an entry for deletion"; }
    }
}
