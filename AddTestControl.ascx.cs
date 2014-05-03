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

public partial class AddTestControl : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext assesDataClasses = new AssesmentDataClassesDataContext();
    bool specialadmin = false;
    int OrganizationID = 0;
    string organizationName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"] != null)
            if (Session["usertype"].ToString() == "SpecialAdmin")
            {
                lblOrganization.Visible = false; ddlOrganizations.Visible = false;
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

        //FillTestSectionDetails();
        FillGridvalues(); // FillTestResultBandDetails();

        //FillOrganizationList();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int userid = 0; int adminaccess = 0;
        if(Session["UserID"]!=null)
            userid=int.Parse(Session["UserID"].ToString());

        if (specialadmin == false)
            if (ddlOrganizations.SelectedIndex <= 0)
            { lblMessage.Text = "Please Select Organization"; return; }
            else {organizationName = ddlOrganizations.SelectedItem.Text; adminaccess = 1;}

        if (txtTestName.Text.Trim() == "")
        { lblMessage.Text = "Please enter Test Name"; return; }

        int testid = 0;
        if (Session["testid"] != null)
            testid = int.Parse(Session["testid"].ToString());

        if (CheckNameDuplication(testid) == true) return;

        //if (txtPassmark.Text.Trim() == "")
        //{ lblMessage.Text = "Please enter Passmark"; return; }

        //if (Checkpassmark(txtPassmark.Text) == false) return;
       // if (gvwBandList.Rows.Count <= 0) { lblMessage.Text = "Please enter Test Result Bands"; return; } //bip 23-11-09

        int passmark = 0;
        //passmark = int.Parse(txtPassmark.Text);

        int status = int.Parse(ddlStatus.SelectedValue);
        int createdby = int.Parse(Session["UserID"].ToString());
        int groupreportaccess = 0;
        if (chbGroupReport.Checked)// == true)
            groupreportaccess = 1;

        if (ddlReportType.SelectedIndex <= 0 && chbGroupReport.Checked == false)
        { lblMessage.Text = "Please select Report type/Report Access"; return; }

        assesDataClasses.AddTestLists(testid, txtTestName.Text.Trim(), organizationName, status, FreeTextBox1.Text, "", passmark, createdby, ddlReportType.SelectedValue,adminaccess,groupreportaccess);
        
        /*
        if (testid == 0)
        {
            var getTestId = from testdet in assesDataClasses.TestLists
                            where testdet.OrganizationName == organizationName && testdet.TestName == txtTestName.Text.Trim()
                            select testdet;
            if (getTestId.Count() > 0)
            {
                if (getTestId.First().TestId != null)
                {
                    testid = int.Parse(getTestId.First().TestId.ToString());
                }
            } if (testid > 0)
            {
                if (gvwBandList.Rows.Count > 0)
                {
                    int markfrom = 0, markto = 0, bandid = 0, benchmark = 0; string dispalyname = "";

                    for (int i = 0; i < gvwBandList.Rows.Count; i++)
                    {
                        if (gvwBandList.Rows[i].Cells[1].Text != "&nbsp;")
                            markfrom = int.Parse(gvwBandList.Rows[i].Cells[1].Text);
                        if (gvwBandList.Rows[i].Cells[2].Text != "&nbsp;")
                            markto = int.Parse(gvwBandList.Rows[i].Cells[2].Text);
                        if (gvwBandList.Rows[i].Cells[3].Text != "&nbsp;")
                            dispalyname = gvwBandList.Rows[i].Cells[3].Text;
                        if (gvwBandList.Rows[i].Cells[4].Text != "&nbsp;")
                            benchmark = int.Parse(gvwBandList.Rows[i].Cells[4].Text);
                        if (gvwBandList.Rows[i].Cells[6].Text != "&nbsp;")
                            bandid = int.Parse(gvwBandList.Rows[i].Cells[6].Text);

                        assesDataClasses.Procedure_TestResultBands(bandid, testid, benchmark, markfrom, markto, dispalyname, "", 1, userid);
                    }
                }
            }
        }
        */
        ClearTestResultSessions(); // ClearTestResultValues(); // FillTestResultBandDetails();
        txtTestName.Text = ""; Session["testid"] = null; ddlStatus.SelectedIndex = 0;
        lblMessage.Text = "Saved successfully ...";FreeTextBox1.Text = "";
        ddlReportType.SelectedIndex = 0;// bip 17062010
        FillGridvalues();
       // txtPassmark.Text = ""; 

    }
    private bool Checkpassmark(string mark)
    {
        try
        {
            int passmark = 0;
            passmark = int.Parse(mark);
            return true;
        }
        catch (Exception ex) { lblMessage.Text = "Enter valid Mark"; return false; }
    }
    private bool CheckNameDuplication(int testid)
    {
        if (specialadmin == false)
            organizationName = ddlOrganizations.SelectedItem.Text;

        var GetTestNameList = from testnamelist in assesDataClasses.TestLists
                              where (testnamelist.TestName == txtTestName.Text.Trim() && testnamelist.OrganizationName == organizationName) && testnamelist.TestId != testid
                              select testnamelist;
        if (GetTestNameList.Count() > 0)
        { lblMessage.Text = "Test Name already exists"; return true; }

        return false;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        int testid = 0;
        Session["testid"] = null;FreeTextBox1.Text = "";
        ddlOrganizations.SelectedIndex = 0; ddlStatus.SelectedIndex = 0; txtTestName.Text = "";
       // txtPassmark.Text = ""; 
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int testid = 0;
        if (Session["testid"] != null)
        {
            testid = int.Parse(Session["testid"].ToString());
            assesDataClasses.DeleteTestLists(testid);
            lblMessage.Text = "Deletion Successfull"; Session["testid"] = null;
            FillGridvalues();
        }
        else
        {
            lblMessage.Text = "Please select an item to delete";
        }
    }

    protected void gvwTestLists_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["testid"] = gvwTestLists.SelectedRow.Cells[5].Text;
        FillSelTestNameDetails(); // FillTestResultBandDetails();
    }
    /*
    private void FillTestResultBandDetails()
    {
        gvwBandList.DataSource = "";
        gvwBandList.DataBind();
        int testid = 0;
        if (Session["testid"] != null)
            testid = int.Parse(Session["testid"].ToString());
        if (testid > 0)
        {
            if (Session["dsBandList"] != null)
            {
                DataSet dsBandDetails = new DataSet();
                dsBandDetails = (DataSet)Session["dsBandList"];
                gvwBandList.DataSource = dsBandDetails.Tables[0];
                gvwBandList.DataBind();
            }
            else
            {

                LinqTestResultBandList.Where = " TestId=" + testid;
                gvwBandList.DataSource = LinqTestResultBandList;
                gvwBandList.DataBind();
                GetResultBandDetailsFromGrid();
            }
        }
    }

    private void GetResultBandDetailsFromGrid()
    {
        DataTable dtBandList = new DataTable();
        //QuestionID,Question,Answer,Option1,Option2,Option3,Option4,Option5
        dtBandList.Columns.Add("TestId");
        dtBandList.Columns.Add("BandId");
        dtBandList.Columns.Add("MarkFrom");
        dtBandList.Columns.Add("MarkTo");
        dtBandList.Columns.Add("DisplayName");
        dtBandList.Columns.Add("BenchMark");

        DataRow drBandList;

        DataSet dsBandList;
        
        if (gvwBandList.Rows.Count > 0)
        {
            for (int i = 0; i < gvwBandList.Rows.Count; i++)
            {
                drBandList = dtBandList.NewRow();

                if (gvwBandList.Rows[i].Cells[1].Text != "&nbsp;")
                    drBandList["MarkFrom"] = gvwBandList.Rows[i].Cells[1].Text;
                if (gvwBandList.Rows[i].Cells[2].Text != "&nbsp;")
                    drBandList["MarkTo"] = gvwBandList.Rows[i].Cells[2].Text;
                if (gvwBandList.Rows[i].Cells[3].Text != "&nbsp;")
                    drBandList["DisplayName"] = gvwBandList.Rows[i].Cells[3].Text;
                if (gvwBandList.Rows[i].Cells[4].Text != "&nbsp;")
                    drBandList["BenchMark"] = gvwBandList.Rows[i].Cells[4].Text;
                if (gvwBandList.Rows[i].Cells[5].Text != "&nbsp;")
                    drBandList["TestId"] = gvwBandList.Rows[i].Cells[5].Text;
                if (gvwBandList.Rows[i].Cells[6].Text != "&nbsp;")
                    drBandList["BandId"] = gvwBandList.Rows[i].Cells[6].Text;

                dtBandList.Rows.Add(drBandList);
            }
            dsBandList = new DataSet();
            dsBandList.Tables.Add(dtBandList);
            Session["dsBandList"] = dsBandList;
        }
    }
    */
    private void FillSelTestNameDetails()
    {
        int testid = 0;
        if (Session["testid"] != null)
            testid = int.Parse(Session["testid"].ToString());

        var TestNameDetails = from testdet in assesDataClasses.TestLists
                              where testdet.TestId == testid
                              select testdet;
        if (TestNameDetails.Count() > 0)
        {
            if(specialadmin==false)
                if (TestNameDetails.First().OrganizationName != null)
                {
                    for (int i = 0; i < ddlOrganizations.Items.Count; i++)
                    {
                        if (ddlOrganizations.Items[i].Text == TestNameDetails.First().OrganizationName.ToString())
                        { ddlOrganizations.SelectedIndex = i; break; }
                    }
                }
            if (TestNameDetails.First().TestName != null)
                txtTestName.Text = TestNameDetails.First().TestName.ToString();

            //if (TestNameDetails.First().PassMark != null)
            //    txtPassmark.Text = TestNameDetails.First().PassMark.ToString();

            if (TestNameDetails.First().Status != null)
            {
                if (TestNameDetails.First().Status.ToString() == "1")
                    ddlStatus.SelectedIndex = 0;
                else ddlStatus.SelectedIndex = 1;
            }

            if (TestNameDetails.First().Instructions != null)
                FreeTextBox1.Text = TestNameDetails.First().Instructions.ToString();
            if (TestNameDetails.First().ReportType != null)// bip 17062010
                ddlReportType.SelectedValue = TestNameDetails.First().ReportType.ToString();
            if (TestNameDetails.First().GroupReportAccess != null)
                if (TestNameDetails.First().GroupReportAccess.Value == 1)
                    chbGroupReport.Checked = true;

           // FillTestResultBandDetails();
        }

    }


    protected void ddlOrganizations_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganizations.SelectedIndex > 0)
        {
            Session["testorgIndex"] = ddlOrganizations.SelectedIndex.ToString();
            FillGridvalues();
        }
        else Session["testorgIndex"] = null;
    }

    private void FillGridvalues()
    {
        if (specialadmin == false)
            organizationName = ddlOrganizations.SelectedItem.Text;

        gvwTestLists.DataSource = "";
        gvwTestLists.DataBind();
        //if (ddlOrganizations.SelectedIndex > 0)
        //{
        var testlistDetails = from testdetails in assesDataClasses.TestLists
                              where testdetails.OrganizationName == organizationName//ddlOrganizations.SelectedItem.Text
                              select testdetails;
        if (testlistDetails.Count() > 0)
        {
            gvwTestLists.DataSource = testlistDetails;
            gvwTestLists.DataBind();
        }
        // }
    }


    
    
    
    private void ClearTestResultSessions()
    {
        Session["dsBandList"] = null; Session["bandid"] = null;        
    }
    /*
     private void ClearTestResultValues()
    {
        txtDisplayName.Text = ""; txtMarkFrom.Text = ""; txtMarkTo.Text = ""; txtBenchMark.Text = "";
    }

    protected void btnAddBands_Click(object sender, EventArgs e)
    {
        int userid = 0;
        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());
        int bandid = 0; int testid = 0;
        if (Session["testid"] != null)
            testid = int.Parse(Session["testid"].ToString());
        if (Session["bandid"] != null)
            bandid = int.Parse(Session["bandid"].ToString());

        if (txtMarkFrom.Text.Trim() == "") { lblMessageResultBands.Text = "Please enter mark from"; return; }
        else if (txtMarkTo.Text.Trim() == "") { lblMessageResultBands.Text = "Please enter mark to"; return; }
        else if (txtDisplayName.Text.Trim() == "") { lblMessageResultBands.Text = "Please enter Display Name details"; return; }
        else if (txtBenchMark.Text.Trim() == "") { lblMessageResultBands.Text = "Please enter bench mark"; return; }
        else
        {
            if (Checkpassmark(txtMarkFrom.Text.Trim()) == false) return;
            else if (Checkpassmark(txtMarkTo.Text.Trim()) == false) return;
            else if (Checkpassmark(txtBenchMark.Text.Trim()) == false) return;
        }
        if (testid > 0)
        {
            int markfrom = 0; int markto = 0; int benchMark = 0;
            markfrom = int.Parse(txtMarkFrom.Text.Trim()); markto = int.Parse(txtMarkTo.Text.Trim()); benchMark = int.Parse(txtBenchMark.Text.Trim());
            assesDataClasses.Procedure_TestResultBands(bandid, testid, benchMark, markfrom, markto, txtDisplayName.Text.Trim(), "", 1, userid);
            ClearTestResultSessions(); ClearTestResultValues();
            FillTestResultBandDetails();
        }
        else
        {
            DataTable dtBandList = new DataTable();
            //QuestionID,Question,Answer,Option1,Option2,Option3,Option4,Option5
            dtBandList.Columns.Add("TestId");
            dtBandList.Columns.Add("BandId");
            dtBandList.Columns.Add("MarkFrom");
            dtBandList.Columns.Add("MarkTo");
            dtBandList.Columns.Add("DisplayName");
            dtBandList.Columns.Add("BenchMark");

            DataRow drBandList;

            DataSet dsBandList = new DataSet();
            if (Session["dsBandList"] != null)
                dsBandList = (DataSet)Session["dsBandList"];
            if (dsBandList != null)
            {
                gvwBandList.DataSource = dsBandList.Tables[0];
                gvwBandList.DataBind();

                for (int i = 0; i < gvwBandList.Rows.Count; i++)
                {

                    drBandList = dtBandList.NewRow();

                    if (gvwBandList.Rows[i].Cells[1].Text != "&nbsp;")
                        drBandList["MarkFrom"] = gvwBandList.Rows[i].Cells[1].Text;
                    if (gvwBandList.Rows[i].Cells[2].Text != "&nbsp;")
                        drBandList["MarkTo"] = gvwBandList.Rows[i].Cells[2].Text;
                    if (gvwBandList.Rows[i].Cells[3].Text != "&nbsp;")
                        drBandList["DisplayName"] = gvwBandList.Rows[i].Cells[3].Text;
                    if (gvwBandList.Rows[i].Cells[4].Text != "&nbsp;")
                        drBandList["BenchMark"] = gvwBandList.Rows[i].Cells[4].Text;
                    if (gvwBandList.Rows[i].Cells[5].Text != "&nbsp;")
                        drBandList["TestId"] = gvwBandList.Rows[i].Cells[5].Text;
                    if (gvwBandList.Rows[i].Cells[6].Text != "&nbsp;")
                        drBandList["BandId"] = gvwBandList.Rows[i].Cells[6].Text;

                    dtBandList.Rows.Add(drBandList);
                }
            }

            drBandList = dtBandList.NewRow();
            drBandList["MarkFrom"] = txtMarkFrom.Text.Trim();
            drBandList["MarkTo"] = txtMarkTo.Text.Trim();
            drBandList["DisplayName"] = txtDisplayName.Text.Trim();
            drBandList["BenchMark"] = txtBenchMark.Text.Trim();
            drBandList["TestId"] = "0";
            drBandList["BandId"] = "0";
            dtBandList.Rows.Add(drBandList);

            dsBandList = new DataSet();
            dsBandList.Tables.Add(dtBandList);
            gvwBandList.DataSource = dsBandList.Tables[0];
            gvwBandList.DataBind();

            Session["dsBandList"] = dsBandList;

            ClearTestResultValues();
        }

    }
    protected void btnResetBands_Click(object sender, EventArgs e)
    {      
        FillTestResultBandDetails();
        ClearTestResultValues(); ClearTestResultSessions();
    }
    protected void btnDeleteBands_Click(object sender, EventArgs e)
    {
        int bandid = 0;
        if (Session["bandid"] != null)
            bandid = int.Parse(Session["bandid"].ToString());
        if (bandid == 0)
        { lblMessageResultBands.Text = "Please select an Entry .."; return; }

        assesDataClasses.Procedure_DeleteTestResultBands(bandid);
        ClearTestResultValues(); ClearTestResultSessions(); FillTestResultBandDetails();
    }

    protected void gvwBandList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["bandid"] = gvwBandList.SelectedRow.Cells[6].Text;
        if (gvwBandList.SelectedRow.Cells[1].Text != "&nbsp;")
            txtMarkFrom.Text = gvwBandList.SelectedRow.Cells[1].Text;
        if (gvwBandList.SelectedRow.Cells[2].Text != "&nbsp;")
            txtMarkTo.Text = gvwBandList.SelectedRow.Cells[2].Text;
        if (gvwBandList.SelectedRow.Cells[3].Text != "&nbsp;")
            txtDisplayName.Text = gvwBandList.SelectedRow.Cells[3].Text;
        if (gvwBandList.SelectedRow.Cells[4].Text != "&nbsp;")
            txtBenchMark.Text = gvwBandList.SelectedRow.Cells[4].Text;
    }
    */
    /*
    private void FillTestSectionDetails()
    {
        gvwSectionName.DataSource = "";
        gvwSectionName.DataBind();
        int testid = 0;
        if (Session["testid"] != null)
            testid = int.Parse(Session["testid"].ToString());
        if (testid > 0)
        {
            if (Session["dsSectionNameList"] != null)
            {
                DataSet dsSectionDetails = new DataSet();
                dsSectionDetails = (DataSet)Session["dsSectionNameList"];
                gvwSectionName.DataSource = dsSectionDetails.Tables[0];
                gvwSectionName.DataBind();
            }
            else
            {
                LinqSectionNameList.Where = " TestId=" + testid;
                gvwSectionName.DataSource = LinqSectionNameList;
                gvwSectionName.DataBind();
                GetTestSectionDetailsFromGrid();
            }
        }
    }

    private void GetTestSectionDetailsFromGrid()
    {
        DataTable dtSectionList = new DataTable();
        dtSectionList.Columns.Add("SectionName");
        dtSectionList.Columns.Add("testsectionid");
        dtSectionList.Columns.Add("TestId");
        DataRow drSectionList;
        DataSet dsSectionList;
        if (gvwSectionName.Rows.Count > 0)
        {
            for (int i = 0; i < gvwSectionName.Rows.Count; i++)
            {
                drSectionList = dtSectionList.NewRow();
                if (gvwSectionName.Rows[i].Cells[1].Text != "&nbsp;")
                    drSectionList["SectionName"] = gvwSectionName.Rows[i].Cells[1].Text;
                if (gvwSectionName.Rows[i].Cells[2].Text != "&nbsp;")
                    drSectionList["testsectionid"] = gvwSectionName.Rows[i].Cells[2].Text;
                if (gvwSectionName.Rows[i].Cells[3].Text != "&nbsp;")
                    drSectionList["TestId"] = gvwSectionName.Rows[i].Cells[3].Text;

                dtSectionList.Rows.Add(drSectionList);
            }

            dsSectionList = new DataSet();
            dsSectionList.Tables.Add(dtSectionList);
            Session["dsSectionNameList"] = dsSectionList;
        }
    }


    protected void btnAddSectionName_Click(object sender, EventArgs e)
    {
        if (txtSectionName.Text.Trim() != "")
        {
            int userid=int.Parse( Session["UserID"].ToString());
            int testid = 0;
            if (Session["testid"] != null)
            {
                testid = int.Parse(Session["testid"].ToString());
                int testsectionid = 0;
                if (Session["testsectionid"] != null)
                    testsectionid = int.Parse(Session["testsectionid"].ToString());
                
                assesDataClasses.Procedure_TestSectionsList(testsectionid, testid, txtSectionName.Text.Trim(), 1, userid);
                Session["dsSectionNameList"] = null; txtSectionName.Text = "";
                FillTestSectionDetails();
            }
            else
            {
                DataTable dtSectionList = new DataTable();
                dtSectionList.Columns.Add("SectionName");
                dtSectionList.Columns.Add("testsectionid");
                dtSectionList.Columns.Add("TestId");
                DataRow drSectionList;
                DataSet dsSectionNameList = new DataSet();
                if (Session["dsSectionNameList"] != null)
                    dsSectionNameList = (DataSet)Session["dsSectionNameList"];
                if (dsSectionNameList != null)
                {
                    gvwSectionName.DataSource = dsSectionNameList.Tables[0];
                    gvwSectionName.DataBind();

                    for (int i = 0; i < gvwSectionName.Rows.Count; i++)
                    {
                        drSectionList = dtSectionList.NewRow();

                        if (gvwSectionName.Rows[i].Cells[1].Text != "&nbsp;")
                            drSectionList["SectionName"] = gvwSectionName.Rows[i].Cells[1].Text;
                        if (gvwSectionName.Rows[i].Cells[2].Text != "&nbsp;")
                            drSectionList["testsectionid"] = gvwSectionName.Rows[i].Cells[2].Text;
                        if (gvwSectionName.Rows[i].Cells[3].Text != "&nbsp;")
                            drSectionList["TestId"] = gvwSectionName.Rows[i].Cells[3].Text;

                        dtSectionList.Rows.Add(drSectionList);
                    }
                }
                
                drSectionList = dtSectionList.NewRow();
                drSectionList["SectionName"] = txtSectionName.Text.Trim();
                drSectionList["testsectionid"] = "0";
                drSectionList["TestId"] = "0";
               
                dtSectionList.Rows.Add(drSectionList);

                dsSectionNameList = new DataSet();
                dsSectionNameList.Tables.Add(dtSectionList);
                gvwSectionName.DataSource = dsSectionNameList.Tables[0];
                gvwSectionName.DataBind();

                Session["dsSectionNameList"] = dsSectionNameList;
            }
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
            Session["dsSectionNameList"] = null; txtSectionName.Text = ""; FillTestSectionDetails();
        }
        else { lblMessageSectionName.Text = "Please select section name"; }
    }
   
    protected void gvwSectionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["testsectionid"] = gvwSectionName.SelectedRow.Cells[2].Text;
        txtSectionName.Text = gvwSectionName.SelectedRow.Cells[1].Text;
    }
    private void FillSectionBandDetails()
    {




    }
    protected void ddlSectionNameList_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    */
    /*
    private void FillOrganizationList()
    {
        ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlOrganization.Items.Clear();
        ddlOrganization.Items.Add(listnew);
        int orgIndex = 0;

        if (Session["OrgIndex_Timer"] != null)
        {
            orgIndex = int.Parse(Session["OrgIndex_Timer"].ToString());
        }
        
        ddlOrganization.DataSource = LinqOrganization;
        ddlOrganization.DataTextField = "Name";
        ddlOrganization.DataValueField = "OrganizationID";
        ddlOrganization.DataBind();
        if (orgIndex > 0)
        { ddlOrganization.SelectedIndex = orgIndex; FillTestList(); }
        else
        {
            listnew = new ListItem("--select--", "0");
            ddlTestName.Items.Clear();
            ddlTestName.Items.Add(listnew);

            listnew = new ListItem("-- Select Test Section from the List --", "0");
            ddlTestSectionName.Items.Clear();
            ddlTestSectionName.Items.Add(listnew);

        }
    }

    private void FillTestList()
    {
        ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlTestName.Items.Clear();
        ddlTestName.Items.Add(listnew);
        int testIndex = 0;

        if (Session["TestIndex_Timer"] != null)
        {
            testIndex = int.Parse(Session["TestIndex_Timer"].ToString());
        }

        var testTimeDetails = from testTimedet in assesDataClasses.TestLists
                              where testTimedet.OrganizationName == ddlOrganization.SelectedItem.Text
                              select testTimedet;        
        ddlTestName.DataSource = testTimeDetails;
        ddlTestName.DataTextField = "TestName";
        ddlTestName.DataValueField = "TestId";
        ddlTestName.DataBind();
        if (testIndex > 0)
        { ddlTestName.SelectedIndex = testIndex; FillTestsectionList(); } //BindGrid();} // bip 220110
        else
        {
            listnew = new ListItem("-- Select Test Section from the List --", "0");
            ddlTestSectionName.Items.Clear();
            ddlTestSectionName.Items.Add(listnew);

            gvwTimeDetails.DataSource = "";
            gvwTimeDetails.DataBind();
        }

    }

    private void FillTestsectionList()
    {
        ListItem litem = new ListItem("-- Select Test Section from the List --", "0");
        ddlTestSectionName.Items.Clear();
        ddlTestSectionName.Items.Add(litem);
        if (ddlTestName.SelectedIndex > 0)
        {
            LinqTestSection.Where = "TestId=" + ddlTestName.SelectedValue;
            ddlTestSectionName.DataSource = LinqTestSection;
            ddlTestSectionName.DataTextField = "SectionName";
            ddlTestSectionName.DataValueField = "TestSectionId";
            ddlTestSectionName.DataBind();
            if (Session["TestsectionIndex_Timer"] != null)
                ddlTestSectionName.SelectedIndex = int.Parse(Session["TestsectionIndex_Timer"].ToString());
        }
        FillTimeDetails();// 22-02-2010 bip
    }

    protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganization.SelectedIndex > 0)
        {
            if (Session["OrgIndex_Timer"] != null)
                if (Session["OrgIndex_Timer"].ToString() != ddlOrganization.SelectedIndex.ToString())
                {Session["TestIndex_Timer"] = null; ddlTestName.SelectedIndex = 0;}

            Session["OrgIndex_Timer"] = ddlOrganization.SelectedIndex.ToString();
            FillTestList();
        }
        else Session["OrgIndex_Timer"] = null;
    }
    protected void ddlTestName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestName.SelectedIndex > 0)
        {
            if (Session["TestIndex_Timer"] != null)
                if (Session["TestIndex_Timer"].ToString() != ddlTestName.SelectedIndex.ToString())
                {Session["TestsectionIndex_Timer"] = null; ddlTestSectionName.SelectedIndex = 0;}

            Session["TestIndex_Timer"] = ddlTestName.SelectedIndex.ToString(); FillTestsectionList(); }//BindGrid();  }//  bip 220110
        else Session["TestIndex_Timer"] = null;
    }
    ///*
    private void FillTimeDetails()
    {
        if (ddlTestSectionName.SelectedIndex > 0)
        {
            LinqTimerDetails.Where = "TestId=" + ddlTestName.SelectedValue + " && TestSectionId=" + ddlTestSectionName.SelectedValue;
            gvwTimeDetails.DataSource = LinqTimerDetails;
            gvwTimeDetails.DataBind();
        }
        else if (ddlTestName.SelectedIndex > 0)
        {
            LinqTimerDetails.Where = "TestId=" + ddlTestName.SelectedValue;
            gvwTimeDetails.DataSource = LinqTimerDetails;
            gvwTimeDetails.DataBind();
        }
    }
    

    private int GetTimeValue(string value)
    {
        int time = 0;
        try
        {
            time = int.Parse(value);
        }
        catch (Exception ex)
        { time = 0; }
        return time;

    }
    protected void btnAddTime_Click(object sender, EventArgs e)
    {
        int userid = 0;
        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());
        if (ddlTestName.SelectedIndex > 0)
        {
            if ((txtHours.Text.Trim() != "0" && txtHours.Text.Trim() != "") || (txtMinutes.Text.Trim() != "0" && txtMinutes.Text.Trim() != ""))
            {
                int hrs = 0, min = 0;
                hrs = GetTimeValue(txtHours.Text); min = GetTimeValue(txtMinutes.Text);
                int testid = 0, testsecid = 0, variableid = 0;
                testid = int.Parse(ddlTestName.SelectedValue); testsecid = int.Parse(ddlTestSectionName.SelectedValue);
                if (ddlTestSectionName.SelectedIndex > 0)
                    assesDataClasses.Procedure_TimerDetails(testid, testsecid, variableid, hrs, min, 1, userid, 2);
                else if (ddlTestName.SelectedIndex > 0)
                    assesDataClasses.Procedure_TimerDetails(testid, testsecid, variableid, hrs, min, 1, userid, 1);
                lblMessageTimes.Text = "Saved Successfully...";
                ClearTimerDetails();
            }
            else lblMessageTimes.Text = "Please enter Time.";
        }
        else lblMessageTimes.Text = "Please select a Test.";

    }
    protected void btnResetTime_Click(object sender, EventArgs e)
    {        
        ClearTimerDetails();
    }
    protected void btnDeleteTime_Click(object sender, EventArgs e)
    {
        int timerid = 0;
        if (Session["timerid"] != null)
        {
            timerid = int.Parse(Session["timerid"].ToString());
            assesDataClasses.Procedure_DeleteTimerDetails(timerid);
            lblMessageTimes.Text = "Deleted successfully...";
            ClearTimerDetails();
        }
        else lblMessageTimes.Text = "Please select a value for deletion.";
    }
    protected void gvwTimeDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["timerid"] = gvwTimeDetails.SelectedRow.Cells[8].Text;
        ddlTestName.SelectedValue=gvwTimeDetails.SelectedRow.Cells[6].Text;
        ddlTestSectionName.SelectedValue=gvwTimeDetails.SelectedRow.Cells[7].Text;
        txtHours.Text=gvwTimeDetails.SelectedRow.Cells[3].Text;
        txtMinutes.Text=gvwTimeDetails.SelectedRow.Cells[4].Text;
    }
    private void ClearTimerDetails()
    {
        Session["OrgIndex_Timer"] = null; Session["TestIndex_Timer"] = null; Session["TestsectionIndex_Timer"] = null;
        ddlOrganization.SelectedIndex = 0; ddlTestName.SelectedIndex = 0; ddlTestSectionName.SelectedIndex = 0;
        txtHours.Text = "0"; txtMinutes.Text = "0";
        Session["timerid"] = null;
        FillOrganizationList();
        gvwTimeDetails.DataSource = ""; gvwTimeDetails.DataBind();
    }
    private void FillTimeDetails()
    {
        if (ddlTestSectionName.SelectedIndex > 0)
        {
            LinqTimerDetails.Where = "TestId=" + ddlTestName.SelectedValue + " && TestSectionId=" + ddlTestSectionName.SelectedValue;
            gvwTimeDetails.DataSource = LinqTimerDetails;
            gvwTimeDetails.DataBind();
        }
        else if (ddlTestName.SelectedIndex > 0)
        {
            LinqTimerDetails.Where = "TestId=" + ddlTestName.SelectedValue;
            gvwTimeDetails.DataSource = LinqTimerDetails;
            gvwTimeDetails.DataBind();
        }
        else
        {
            gvwTimeDetails.DataSource = "";
            gvwTimeDetails.DataBind();
        }
    }
    protected void ddlTestSectionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestName.SelectedIndex > 0)
        { Session["TestsectionIndex_Timer"] = ddlTestName.SelectedIndex.ToString(); FillTimeDetails(); }//BindGrid();  }//  bip 220110
        else Session["TestsectionIndex_Timer"] = null;
    }

    */
}
