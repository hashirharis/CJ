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

using System.Data.OleDb;
using System.IO;

public partial class UploadScoringData : System.Web.UI.UserControl
{
    DBManagementClass clsClasses = new DBManagementClass();
    AssesmentDataClassesDataContext dataClasses = new AssesmentDataClassesDataContext();
    bool specialadmin = false;
    int OrganizationID = 0;
    string organizationName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {
             ddlOrganizationList.Visible = false;//lblOrganization.Visible = false;
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

        if (Session["testorgIndex"] != null)
            ddlOrganizationList.SelectedIndex = int.Parse(Session["testorgIndex"].ToString());
        FillTestNameList();
    }
    protected void btnUpdateAndImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtfilename.Text.Trim() == "") { lblMessage.Text = "Please select a file"; return; }
            if (!File.Exists(Server.MapPath("images/" + txtfilename.Text)))
            { lblMessage.Text = "file not found"; return; }

            String[] strs = txtfilename.Text.Split(new char[] { '\\', '\\' });
            String imageFleName = strs[strs.Length - 1];
            String[] testImg = imageFleName.Split(new char[] { '.' });
            int index = testImg.Count() - 1;
            string ext;
            ext = testImg[index];
            if (ext != "xls")
            { lblMessage.Text = "Please Select a Valid Excel File"; return; }

           // DeleteDataFromDB();
           // if (IsDeleted == false) return;

            

            // code to insert dataworking....
            OleDbConnection oconn = new OleDbConnection
        (@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
        Server.MapPath("images/" + txtfilename.Text) + ";Extended Properties=Excel 8.0");


            OleDbCommand ocmd = new OleDbCommand("select TestId,TestSectionId,SectionName,SectionId,TotalScore,UserId from [" + txtSheetName.Text + "$]", oconn);
            oconn.Open();  //Here [Sheet1$] is the name of the sheet 
            //in the Excel file where the data is present
            OleDbDataReader odr = ocmd.ExecuteReader();
            int TestId, TestSectionId, SectionId, UserId = 0;
            string SectionName = "";
            float TotalScore =0;

            TestId = int.Parse(odr[0].ToString());
           //// clsClasses.ExcecuteDB("delete from ScoreTable where TestId=" + TestId);

            while (odr.Read())
            {
                //id = int.Parse(odr[0].ToString());//Here we are calling the valid method
                TestId = int.Parse(odr[0].ToString());
                TestSectionId = int.Parse(odr[1].ToString());
                SectionName=(odr[2]).ToString();
                SectionId = int.Parse(odr[3].ToString());
                TotalScore = float.Parse(odr[4].ToString());
                //UserId=int.Parse(odr[5].ToString());
                UserId = GetUserId(odr[5].ToString());
                //Here using this method we are inserting the data into the database
               // insertdataintosql(TestId, TestSectionId,SectionName, SectionId,TotalScore,UserId);
                dataClasses.Procedure_ScoreTable(TestId, TestSectionId, SectionName, SectionId, TotalScore, UserId);

            }
            oconn.Close();
            lblMessage.Text = "Saved...";

            //////

        }
        catch (Exception ex) { lblMessage.Text = ex.Message; }
    }

    private int GetUserId(string userid)
    {
        int curuserid = 0;
        try
        {
            curuserid = int.Parse(userid);
        }
        catch (Exception ex) { curuserid = 0; }

        return curuserid;
    }

    protected void ddlOrganizationList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganizationList.SelectedIndex > 0)
        {
            Session["testorgIndex"] = ddlOrganizationList.SelectedIndex.ToString();
            FillTestNameList();
        }
        else Session["testorgIndex"] = null;

    }
    private void FillTestNameList()
    {
        //gvwTest.DataSource = "";
        //gvwTest.DataBind();
        if (specialadmin == false)
            organizationName = ddlOrganizationList.SelectedItem.Text;

        if (ddlOrganizationList.SelectedIndex > 0 || specialadmin == true)
        {
            var testlistDetails = from testdetails in dataClasses.TestLists
                                  where testdetails.OrganizationName == organizationName
                                  select testdetails;
            if (testlistDetails.Count() > 0)
            {
                gvwTest.DataSource = testlistDetails;
                gvwTest.DataBind();

                FillTestSectionDetails();
            }
        }
    }
    private void FillTestSectionDetails()
    {
        //gvwSectionNameList.DataSource = "";
        //gvwSectionNameList.DataBind();
        int testid = 0;
        if(Session["selTestId"]!=null)
        testid = int.Parse(Session["selTestId"].ToString());
        if (testid > 0)
        {
            LinqTestSectionList.Where = " TestId=" + testid;
            gvwSectionNameList.DataSource = LinqTestSectionList;
            gvwSectionNameList.DataBind();
        }
        FillTestVariableDetails();
    }

    private void FillTestVariableDetails()
    {
        //gvwVariableNameList.DataSource = "";
        //gvwVariableNameList.DataBind();
        int testid = 0; int testsectionid = 0;
        if(Session["selTestId"]!=null)
        testid = int.Parse(Session["selTestId"].ToString());
        if (Session["selTestsectionId"] != null)
            testsectionid = int.Parse(Session["selTestsectionId"].ToString());
        if (testid > 0)
        {
            if (testsectionid > 0)
                LinqVariableNameList.Where = " status=1 and  TestId=" + testid + " and TestSectionId=" + testsectionid;
            else LinqVariableNameList.Where = " status=1 and TestId=" + testid;
            gvwVariableNameList.DataSource = LinqVariableNameList;
            gvwVariableNameList.DataBind();
        }

    }
    protected void gvwTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvwTest.SelectedRow.Cells[2].Text != "&nbsp;")
        {
            Session["selTestId"] = gvwTest.SelectedRow.Cells[2].Text;
            Session["selTestsectionId"] = null;
            FillTestSectionDetails();
        }
        else
        {
            Session["selTestId"] = null;
            Session["selTestsectionId"] = null;
        }
    }
    protected void gvwSectionNameList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvwSectionNameList.SelectedRow.Cells[2].Text != "&nbsp;")
        {
            Session["selTestsectionId"] = gvwSectionNameList.SelectedRow.Cells[2].Text;
            FillTestVariableDetails();
        }
        else Session["selTestsectionId"] = null;
    }

    protected void gvwTest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwTest.PageIndex = e.NewPageIndex;
        gvwTest.DataBind();
    }
    protected void gvwSectionNameList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwSectionNameList.PageIndex = e.NewPageIndex;
        gvwSectionNameList.DataBind();
    }
    protected void gvwVariableNameList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwVariableNameList.PageIndex = e.NewPageIndex;
        gvwVariableNameList.DataBind();
    }

    protected void btnReplaceAndImport_Click(object sender, EventArgs e)
    {
        int maincount = 0, subcount = 0;
        try
        {
            if (txtfilename.Text.Trim() == "") { lblMessage.Text = "Please select a file"; return; }
            if (!File.Exists(Server.MapPath("images/" + txtfilename.Text)))
            { lblMessage.Text = "file not found"; return; }

            String[] strs = txtfilename.Text.Split(new char[] { '\\', '\\' });
            String imageFleName = strs[strs.Length - 1];
            String[] testImg = imageFleName.Split(new char[] { '.' });
            int index = testImg.Count() - 1;
            string ext;
            ext = testImg[index];
            if (ext != "xls")
            { lblMessage.Text = "Please Select a Valid Excel File"; return; }

            // DeleteDataFromDB();
            // if (IsDeleted == false) return;



            // code to insert dataworking....
            OleDbConnection oconn = new OleDbConnection
        (@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
        Server.MapPath("images/" + txtfilename.Text) + ";Extended Properties=Excel 8.0");


            OleDbCommand ocmd = new OleDbCommand("select TestId,TestSectionId,SectionName,SectionId,TotalScore,UserId from [" + txtSheetName.Text + "$]", oconn);
            oconn.Open();  //Here [Sheet1$] is the name of the sheet 
            //in the Excel file where the data is present
            OleDbDataReader odr = ocmd.ExecuteReader();
            int TestId, TestSectionId, SectionId, UserId = 0;
            string SectionName = "";
            float TotalScore = 0;
            int count = 0;
           

            while (odr.Read())
            {
                //if (maincount == 1634)
                //{ SectionName = ""; }

                maincount++; subcount++;
                if (count == 0)
                {
                    TestId = int.Parse(odr[0].ToString());
                    clsClasses.ExcecuteDB("delete from ScoreTable where TestId=" + TestId);
                    
                }count++;
                //id = int.Parse(odr[0].ToString());//Here we are calling the valid method
                //if (odr[0].ToString() == "")//bip 04052010
                //    break;

                TestId = int.Parse(odr[0].ToString());
                TestSectionId = int.Parse(odr[1].ToString());
                SectionName = (odr[2]).ToString();
                SectionId = int.Parse(odr[3].ToString());
                TotalScore = float.Parse(odr[4].ToString());
                //UserId=int.Parse(odr[5].ToString());
                UserId = GetUserId(odr[5].ToString());
                //Here using this method we are inserting the data into the database
                // insertdataintosql(TestId, TestSectionId,SectionName, SectionId,TotalScore,UserId);
                dataClasses.Procedure_ScoreTable(TestId, TestSectionId, SectionName, SectionId, TotalScore, UserId);
                if (subcount == 100)
                    subcount = 0;

            }
            oconn.Close();
            lblMessage.Text = "Saved...";

            //////

        }
        catch (Exception ex) { lblMessage.Text = ex.Message; }
    }
}
