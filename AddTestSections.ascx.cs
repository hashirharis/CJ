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

public partial class AddTestSections : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext assesDataClasses = new AssesmentDataClassesDataContext();
    bool specialadmin = false;
    int OrganizationID = 0;
    string organizationName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {
            lblOrganization.Visible = false; ddlOrganization.Visible = false;
            specialadmin = true; OrganizationID = int.Parse(Session["AdminOrganizationID"].ToString());

            if (Session["adminOrgName"] != null)
                organizationName = Session["adminOrgName"].ToString();
            else
            {
                var orgName = from orgDet in assesDataClasses.Organizations
                              where orgDet.OrganizationID == OrganizationID
                              select orgDet;
                if (orgName.Count() > 0)
                { organizationName = orgName.First().Name.ToString(); Session["adminOrgName"] = organizationName; }
            }
        }
        else ddlOrganization.DataBind();


        if (Session["OrgIdTestSectionIndex"] != null)
            ddlOrganization.SelectedIndex = int.Parse(Session["OrgIdTestSectionIndex"].ToString());

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

            if (Session["TestIdTestSectionIndex"] != null)
            {
                testIndex = int.Parse(Session["TestIdTestSectionIndex"].ToString());
            }

            var testListDetails = from testlistsdet in assesDataClasses.TestLists
                                  where testlistsdet.OrganizationName == organizationName
                                  select testlistsdet;
            ddlTestName.DataSource = testListDetails;
            ddlTestName.DataTextField = "TestName";
            ddlTestName.DataValueField = "TestId";
            ddlTestName.DataBind();
            if (testIndex > 0)
            { ddlTestName.SelectedIndex = testIndex; FillTestSectionDetails(); }
        }
    }

    private void FillTestSectionDetails()
    {
        gvwSectionName.DataSource = "";
        gvwSectionName.DataBind();
        int testid = 0;
        testid = int.Parse(ddlTestName.SelectedValue);
        if (testid > 0)
        {
            LinqSectionNameList.Where = " TestId=" + testid;
            gvwSectionName.DataSource = LinqSectionNameList;
            gvwSectionName.DataBind();

            ddlSectionNameList.Items.Clear();
            ListItem litem = new ListItem("-- Select a Section from the List -- ", "0");
            ddlSectionNameList.Items.Add(litem);
            LinqTestSectionNameList.Where = "TestId=" + ddlTestName.SelectedValue;
            ddlSectionNameList.DataSource = LinqTestSectionNameList;
            ddlSectionNameList.DataTextField = "SectionName";
            ddlSectionNameList.DataValueField = "TestSectionId";
            ddlSectionNameList.DataBind();
            if (Session["sectionnameIndex"] != null)
            { ddlSectionNameList.SelectedIndex = int.Parse(Session["sectionnameIndex"].ToString());  }

        }
        FillTestSectionBandDetails();
    }
    private void FillTestSectionBandDetails()
    {
        //gvwSectionBands.DataSource = "";
        //gvwSectionBands.DataBind();
        //if (ddlSectionNameList.SelectedIndex > 0)
        //{
        //    LinqSectionBandDetails.Where = "SectionId=" + ddlSectionNameList.SelectedValue;
        //    gvwSectionBands.DataSource = LinqSectionBandDetails;
        //    gvwSectionBands.DataBind();
        //}


        var ObjTestSecBandDetails = from ObjTestSecBandDet in assesDataClasses.TestSectionResultBands
                                    where ObjTestSecBandDet.SectionId == int.Parse(ddlSectionNameList.SelectedValue)
                                    select ObjTestSecBandDet;
        if (ObjTestSecBandDetails.Count() > 0)
        {
            DataTable dtTestSecBandDetails = new DataTable();
            dtTestSecBandDetails.Columns.Add("MarkFrom");
            dtTestSecBandDetails.Columns.Add("MarkTo");
            dtTestSecBandDetails.Columns.Add("DisplayName");
            dtTestSecBandDetails.Columns.Add("BenchMark");
            dtTestSecBandDetails.Columns.Add("SectionBandId");
            dtTestSecBandDetails.Columns.Add("TestId");
            dtTestSecBandDetails.Columns.Add("SectionId");

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
                drTestSecBandDetails["SectionBandId"] = objTestSecBandDet.SectionBandId.ToString();
                drTestSecBandDetails["TestId"] = objTestSecBandDet.TestId.ToString();
                drTestSecBandDetails["SectionId"] = objTestSecBandDet.SectionId.ToString();
                dtTestSecBandDetails.Rows.Add(drTestSecBandDetails);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtTestSecBandDetails);
            gvwSectionBands.DataSource = ds.Tables[0];
            gvwSectionBands.DataBind();


            FillSelectedBandDetails();
        }        

    }
    protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganization.SelectedIndex > 0)
        {
            Session["OrgIdTestSectionIndex"] = ddlOrganization.SelectedIndex.ToString();
            FillTestList();
        }
        else Session["OrgIdTestSectionIndex"] = null;
    }
    protected void ddlTestName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestName.SelectedIndex > 0)
        {
            Session["TestIdTestSectionIndex"] = ddlTestName.SelectedIndex.ToString();
            FillTestSectionDetails();
        }
        else Session["TestIdTestSectionIndex"] = null;
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
    protected void btnAddSectionName_Click(object sender, EventArgs e)
    {
        if (txtSectionName.Text.Trim() != "")
        {
            int userid = int.Parse(Session["UserID"].ToString());
            int testid = 0;
            if (ddlTestName.SelectedIndex>0)
            {
                testid = int.Parse(ddlTestName.SelectedValue);
                int testsectionid = 0;
                if (Session["testsectionID"] != null)
                    testsectionid = int.Parse(Session["testsectionID"].ToString());
                int adminaccess = 1;
                if (specialadmin == true) adminaccess = 0;
                assesDataClasses.Procedure_TestSectionsList(testsectionid, testid, txtSectionName.Text.Trim(), 1, userid,adminaccess);
                txtSectionName.Text = ""; Session["testsectionID"] = null;
                FillTestSectionDetails();
            }
            else { lblMessageSectionName.Text = "Please select a test"; return; }
        }
        else lblMessageSectionName.Text = "Please enter section name";
    }
    protected void btnResetSectionName_Click(object sender, EventArgs e)
    {
        Session["testsectionid"] = null; txtSectionName.Text = "";
        FillTestSectionDetails();
    }
    protected void btnDeleteSectionName_Click(object sender, EventArgs e)
    {
        int testsectionid = 0;
        if (Session["testsectionid"] != null)
        {
            testsectionid = int.Parse(Session["testsectionid"].ToString());
            assesDataClasses.Procedure_DeleteTestSectionsList(testsectionid);
            txtSectionName.Text = ""; Session["testsectionid"] = null; Session["sectionnameIndex"] = null;
            FillTestSectionDetails(); FillTestSectionBandDetails();
        }
        else { lblMessageSectionName.Text = "Please select section name"; }
    }
    protected void gvwSectionName_SelectedIndexChanged(object sender, EventArgs e)
    {       
        Session["testsectionID"] = gvwSectionName.SelectedRow.Cells[2].Text;
        txtSectionName.Text = gvwSectionName.SelectedRow.Cells[1].Text;
    }

    protected void ddlSectionNameList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSectionNameList.SelectedIndex > 0)
        {
            Session["sectionnameIndex"] = ddlSectionNameList.SelectedIndex.ToString();
            FillTestSectionBandDetails();
        }
        else Session["sectionnameIndex"] = null;
    }    

    protected void btnAddSectionBands_Click(object sender, EventArgs e)
    {
        if (txtSectionMarksFrom.Text.Trim() == "" || txtSectionMarksTo.Text.Trim() == "" || txtSectionDisplayName.Text.Trim() == "" || txtSectionBenchMark.Text.Trim() == "")
        { lblMessageSectionBand.Text = "Please enter required values"; return; }
        if (CheckMark(txtSectionMarksFrom.Text.Trim()) == false) return;
        if (CheckMark(txtSectionMarksTo.Text.Trim()) == false) return;
        if (CheckMark(txtSectionBenchMark.Text.Trim()) == false) return;

        int SectionBandId = 0;
        if (Session["SectionBandId"] != null)
           SectionBandId = int.Parse(Session["SectionBandId"].ToString());
        int testid = 0;int sectionid=0;
        testid=int.Parse(ddlTestName.SelectedValue);sectionid=int.Parse(ddlSectionNameList.SelectedValue);
        int markfrom=0,markto=0,benchmark=0;
        markfrom=int.Parse(txtSectionMarksFrom.Text.Trim());markto=int.Parse(txtSectionMarksTo.Text.Trim());
        benchmark=int.Parse(txtSectionBenchMark.Text.Trim());
        int userid=int.Parse(Session["UserID"].ToString());
        assesDataClasses.Procedure_TestSectionResultBands(SectionBandId, testid, sectionid, benchmark, markfrom, markto, txtSectionDisplayName.Text.Trim(), "", 1, userid);
        
        Session["SectionBandId"] = null;FillTestSectionBandDetails();
        txtSectionMarksFrom.Text = ""; txtSectionMarksTo.Text = ""; txtSectionDisplayName.Text = ""; txtSectionBenchMark.Text = "";
        lblMessageSectionBand.Text = "Saved Successfully..";
       

    }
    protected void btnResetSectionBands_Click(object sender, EventArgs e)
    {
        Session["SectionBandId"] = null; FillTestSectionBandDetails();
        txtSectionMarksFrom.Text = ""; txtSectionMarksTo.Text = ""; txtSectionDisplayName.Text = ""; txtSectionBenchMark.Text = "";       
    }
    protected void btnDeleteSectionBands_Click(object sender, EventArgs e)
    {
        if (Session["SectionBandId"] != null)
        {int SectionBandId=int.Parse(Session["SectionBandId"].ToString());
        assesDataClasses.Procedure_DeleteTestSectionResultBands(SectionBandId);
        Session["SectionBandId"] = null; FillTestSectionBandDetails();
        txtSectionMarksFrom.Text = ""; txtSectionMarksTo.Text = ""; txtSectionDisplayName.Text = ""; txtSectionBenchMark.Text = "";
        lblMessageSectionBand.Text = "Deleted Successfully..";

        }
    }

    protected void gvwSectionBands_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SectionBandId"] = gvwSectionBands.SelectedRow.Cells[5].Text;
        FillSelectedBandDetails();
        /*
        if (gvwSectionBands.SelectedRow.Cells[1].Text != "&nbsp;")
            txtSectionMarksFrom.Text = gvwSectionBands.SelectedRow.Cells[1].Text;
        if (gvwSectionBands.SelectedRow.Cells[2].Text != "&nbsp;")
            txtSectionMarksTo.Text = gvwSectionBands.SelectedRow.Cells[2].Text;
        if (gvwSectionBands.SelectedRow.Cells[3].Text != "&nbsp;")
            txtSectionDisplayName.Text = gvwSectionBands.SelectedRow.Cells[3].Text;
        if (gvwSectionBands.SelectedRow.Cells[4].Text != "&nbsp;")
            txtSectionBenchMark.Text = gvwSectionBands.SelectedRow.Cells[4].Text;
         * */
    }
    private void FillSelectedBandDetails()
    {
        if (Session["SectionBandId"] != null)
        {
            var ObjTestSecBandDetails = from ObjTestSecBandDet in assesDataClasses.TestSectionResultBands
                                        where ObjTestSecBandDet.SectionId == int.Parse(Session["SectionBandId"].ToString())
                                        select ObjTestSecBandDet;
            if (ObjTestSecBandDetails.Count() > 0)
            {
                txtSectionMarksFrom.Text = ObjTestSecBandDetails.First().MarkFrom.ToString();
                txtSectionMarksTo.Text = ObjTestSecBandDetails.First().MarkTo.ToString();
                txtSectionDisplayName.Text = ObjTestSecBandDetails.First().DisplayName.ToString();
                txtSectionBenchMark.Text = ObjTestSecBandDetails.First().BenchMark.ToString();

            }
        }
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
}
