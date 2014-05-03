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

public partial class AddInstructionsControl : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    bool specialadmin = false;
    int OrganizationID = 0;
    string organizationName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {
            lblOrganization.Visible = false; ddlOrganization.Visible = false;
            specialadmin = true; OrganizationID = int.Parse(Session["AdminOrganizationID"].ToString());
            bool perassigned = false;
            ListItem litem = new ListItem("-- select --", "0");
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(litem);

            var getQuestionTypes = from questiontypes in dataclasses.OrganizationQuestionTypes where questiontypes.OrganizationId == OrganizationID select questiontypes;
            if (getQuestionTypes.Count() > 0)
            {
                foreach (var orgQuestionTypes in getQuestionTypes)
                {
                    string questiontype=orgQuestionTypes.QuestionTypeName.ToString();
                    if (questiontype == "Objective") questiontype = "1";
                    else if (questiontype == "FillBlanks") questiontype = "2";
                    else if (questiontype == "RatingType") questiontype = "3";
                    else if (questiontype == "ImageType") questiontype = "4";
                    else if (questiontype == "VideoType") questiontype = "5";
                    else if (questiontype == "AudioType") questiontype = "6";
                    else if (questiontype == "MemTestWords") questiontype = "7";
                    else if (questiontype == "MemTestImages") questiontype = "8";
                    //else if (questiontype == "PhotoType") questiontype = "9";

                    litem = new ListItem(orgQuestionTypes.QuestionTypeDescription.ToString(), questiontype);
                    ddlCategory.Items.Add(litem);
                    perassigned = true;
                }
            }
            if (perassigned == false)
            { lblMessage.Text = "Please contact your site admin to get permissions to add questions"; return; }

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
        }
        else ddlOrganization.DataBind();


        if (Session["OrgIdTestInstructionIndex"] != null)
            ddlOrganization.SelectedIndex = int.Parse(Session["OrgIdTestInstructionIndex"].ToString());

        FillTestList();

        
    }

    private void FillTestList()
    {
        if (ddlOrganization.SelectedIndex > 0)
            organizationName = ddlOrganization.SelectedItem.Text;

        //if (ddlOrganization.SelectedIndex > 0)
        //{
        if (organizationName != "")
        {
            ListItem listnew;
            listnew = new ListItem("--select--", "0");
            ddlTestName.Items.Clear();
            ddlTestName.Items.Add(listnew);
            int testIndex = 0;

            if (Session["TestIdTestInstructionIndex"] != null)
            {
                testIndex = int.Parse(Session["TestIdTestInstructionIndex"].ToString());
            }

            var testListDetails = from testlistsdet in dataclasses.TestLists
                                  where testlistsdet.OrganizationName == organizationName
                                  select testlistsdet;
            ddlTestName.DataSource = testListDetails;
            ddlTestName.DataTextField = "TestName";
            ddlTestName.DataValueField = "TestId";
            ddlTestName.DataBind();
            if (testIndex > 0)
            { ddlTestName.SelectedIndex = testIndex; FillInstructionDetails(); }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int userid=0;int category=0;int status=0;
        status=int.Parse( ddlStatus.SelectedValue );
        if(Session["UserID"]!=null)
            userid=int.Parse(Session["UserID"].ToString());
        if (ddlCategory.SelectedIndex > 0 && ddlTestName.SelectedIndex>0 && (FreeTextBox1.Text.Trim() != "" || FreeTextBox2.Text.Trim() != ""))
        {
            int adminaccess = 0; int testid = 0;
            if (specialadmin == false)
            { adminaccess = 1; OrganizationID = int.Parse(ddlOrganization.SelectedValue); }

            testid = int.Parse(ddlTestName.SelectedValue);
            category = int.Parse(ddlCategory.SelectedValue);
            dataclasses.Procedure_TestInstructions(0, FreeTextBox1.Text, category, status, userid,FreeTextBox2.Text,adminaccess,OrganizationID,testid);

            clearValues();            
            lblMessage.Text = "Saved Successfully";
            

        }
        else { lblMessage.Text = "Please select category/enter Title or Instructions"; }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clearValues();
    }
    private void clearValues()
    {
        Session["OrgIdTestInstructionIndex"] = null; ddlOrganization.SelectedIndex = 0;
        Session["TestIdTestInstructionIndex"] = null; ddlTestName.SelectedIndex = 0; 

        FreeTextBox1.Text = "";  FreeTextBox2.Text = "";        
        Session["catIndex"] = null;ddlCategory.SelectedIndex = 0;
         
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex > 0 && ddlTestName.SelectedIndex>0)
        {
            int adminaccess = 0; int testid = 0;
            if (specialadmin == false)
            {
                adminaccess = 1;
                OrganizationID = int.Parse(ddlOrganization.SelectedValue);
            }
            testid = int.Parse(ddlTestName.SelectedValue);

            dataclasses.Procedure_DeleteTestInstructions(int.Parse(ddlCategory.SelectedValue),adminaccess,OrganizationID,testid);
            lblMessage.Text = "Deletion successfull ..";
            clearValues();
        }
        else { lblMessage.Text = "Please select category"; }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddlCategory.SelectedIndex > 0)
        {
            if (Session["catIndex"] != null)
                if (Session["catIndex"].ToString() == ddlCategory.SelectedIndex.ToString())
                    return;
            FreeTextBox1.Text = ""; FreeTextBox2.Text = "";
            Session["catIndex"] = ddlCategory.SelectedIndex.ToString();
            FillInstructionDetails();
        }
    }
    private void FillInstructionDetails()
    {
        int adminaccess = 0;
        if (specialadmin == false)
        {adminaccess = 1;OrganizationID=int.Parse(ddlOrganization.SelectedValue);}

        if (ddlTestName.SelectedIndex > 0)
        {
            int testid=0;
            testid=int.Parse(ddlTestName.SelectedValue);
            //var Instructiondetails = from instructiondet in dataclasses.TestInstructions
            //                         where instructiondet.CategoryId == int.Parse(ddlCategory.SelectedValue) && instructiondet.AdminAccess == adminaccess && instructiondet.OrganizationId == OrganizationID
            //                         select instructiondet;

            var Instructiondetails = from instructiondet in dataclasses.TestInstructions
                                     where instructiondet.CategoryId == int.Parse(ddlCategory.SelectedValue) && instructiondet.TestId == testid && instructiondet.AdminAccess == adminaccess && instructiondet.OrganizationId == OrganizationID
                                     select instructiondet;
            if (Instructiondetails.Count() > 0)
            {
                if (Instructiondetails.First().Title != null)
                    FreeTextBox2.Text = Instructiondetails.First().Title.ToString();

                if (Instructiondetails.First().InstructionDetails != null)
                    FreeTextBox1.Text = Instructiondetails.First().InstructionDetails.ToString();
                if (Instructiondetails.First().Status != null)
                    if (Instructiondetails.First().Status.ToString() == "1")
                        ddlStatus.SelectedIndex = 0;
                    else ddlStatus.SelectedIndex = 1;
            }
        }
    }
    protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganization.SelectedIndex > 0)
        {
            if (Session["OrgIdTestInstructionIndex"] != null)
                if (Session["OrgIdTestInstructionIndex"].ToString() == ddlOrganization.SelectedIndex.ToString())
                    return;

            Session["TestIdTestInstructionIndex"] = null; ddlTestName.SelectedIndex = 0;
            FreeTextBox1.Text = ""; FreeTextBox2.Text = "";
            Session["OrgIdTestInstructionIndex"] = ddlOrganization.SelectedIndex.ToString();
            FillTestList();
        }
        else clearValues();//Session["OrgIdTestInstructionIndex"] = null;
    }
    protected void ddlTestName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestName.SelectedIndex > 0)
        {
            if (Session["TestIdTestInstructionIndex"] != null)
                if (Session["TestIdTestInstructionIndex"].ToString() == ddlTestName.SelectedIndex.ToString())
                    return;

            FreeTextBox1.Text = ""; FreeTextBox2.Text = "";
            Session["TestIdTestInstructionIndex"] = ddlTestName.SelectedIndex.ToString();
            FillInstructionDetails();
        }
        else Session["TestIdTestInstructionIndex"] = null;
    }
}
