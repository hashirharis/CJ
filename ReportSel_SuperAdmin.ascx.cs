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

public partial class ReportSel_SuperAdmin : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataClasses = new AssesmentDataClassesDataContext();
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


       // FillTestList();
        FillGroupList();
    }

    private void FillGroupList()
    {
        /**
      * bip 10042011 include groupwise user report
      */
        int groupindex = 0;
        if (Session["groupIndex_report"] != null)
            groupindex = int.Parse(Session["groupIndex_report"].ToString());

        if (specialadmin == false)
            OrganizationID = int.Parse(ddlOrganizationList.SelectedValue);

        if (OrganizationID > 0)
        {
            GroupListSource.Where = "OrganizationID = " + OrganizationID;

            ddlUserGroup.DataSource = GroupListSource;
            ddlUserGroup.DataTextField = "GroupName";
            ddlUserGroup.DataValueField = "GroupUserID";
            ddlUserGroup.DataBind();

            if (groupindex > 0)
                ddlUserGroup.SelectedIndex = groupindex;
        
        }


        FillTestList();

    }

    private void FillTestList()
    {
        int testindex = 0;
        if (Session["testIndex_report"] != null)
            testindex = int.Parse(Session["testIndex_report"].ToString());
        ddlTestList.Items.Clear();
        ListItem litem = new ListItem("-- Select --", "0");
        ddlTestList.Items.Add(litem);
        if (specialadmin == false)
            organizationName = ddlOrganizationList.SelectedItem.Text;

        if (ddlOrganizationList.SelectedIndex > 0 || specialadmin == true)
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
                    FilluserList();
                }
            }
        }
    }

    private void FilluserList()
    {
        int userindexReport = 0;
        if (Session["userIndex_Report"] != null)
            userindexReport = int.Parse(Session["userIndex_Report"].ToString());
        ddlUserList.Items.Clear();
        ddlUserList.DataSource = "";ddlUserList.DataBind();
        ListItem litem=new ListItem("-- select --","0");
        ddlUserList.Items.Add(litem);

        int orgid = 0, testid = 0; 
        //if (ddlOrganizationList.SelectedIndex > 0)
        //    orgid = int.Parse(ddlOrganizationList.SelectedValue);
        if(specialadmin==false)
            OrganizationID=int.Parse(ddlOrganizationList.SelectedValue);

        ///bip 10042011
        int grpid = 0;
        if (ddlUserGroup.SelectedIndex > 0)
            grpid = int.Parse(ddlUserGroup.SelectedValue);

        ///

        if (ddlTestList.SelectedIndex > 0)
            testid = int.Parse(ddlTestList.SelectedValue);
        if (OrganizationID > 0 && testid > 0)
        {
            if (grpid > 0)
            {
                ///bip 10042011
                var userlist = from userdet in dataClasses.UserProfiles
                               where userdet.FirstLoginDate.HasValue == true && userdet.OrganizationID == OrganizationID && userdet.TestId == testid && userdet.GrpUserID==grpid &&
                               (userdet.UserType != "SuperAdmin" && userdet.UserType != "OrgAdmin" && userdet.UserType != "GrpAdmin" && userdet.UserType != "SpecialAdmin")
                               select userdet;// bip 07052010
                //
                // LinqUserList.Where = "OrganizationId=" + orgid + " && TestId=" + testid;
                // ddlUserList.DataSource = LinqUserList;
                ddlUserList.DataSource = userlist;
                ddlUserList.DataTextField = "UserName";
                ddlUserList.DataValueField = "UserId";
                ddlUserList.DataBind();

            }
            else
            {

                var userlist = from userdet in dataClasses.UserProfiles
                               where userdet.FirstLoginDate.HasValue == true && userdet.OrganizationID == OrganizationID && userdet.TestId == testid &&
                               (userdet.UserType != "SuperAdmin" && userdet.UserType != "OrgAdmin" && userdet.UserType != "GrpAdmin" && userdet.UserType != "SpecialAdmin")
                               select userdet;// bip 07052010
                //
                // LinqUserList.Where = "OrganizationId=" + orgid + " && TestId=" + testid;
                // ddlUserList.DataSource = LinqUserList;
                ddlUserList.DataSource = userlist;
                ddlUserList.DataTextField = "UserName";
                ddlUserList.DataValueField = "UserId";
                ddlUserList.DataBind();

            }
            ddlUserList.SelectedIndex = userindexReport;

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
            //FillTestList();
            FillGroupList();
        }
        else Session["orgIndex_report"] = null;
    }
    protected void ddlTestList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestList.SelectedIndex > 0)
        {
            Session["testIndex_report"] = ddlTestList.SelectedIndex.ToString();
            FilluserList();
        }
        else Session["testIndex_report"] = null;
        
      
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {       
 
        //// bipson 06022011 new report with colour
        if (ddlUserGroup.SelectedIndex ==0 && ddlUserList.SelectedIndex == 0)
        { lblMessage.Text = "Please select a Group/User from the list"; return; }

        if (ddlTestList.SelectedIndex > 0)// || ddlUserList.SelectedIndex > 0)
        {

            //if (ddlReportType.SelectedIndex != 2)
            if(ddlUserList.SelectedIndex >0)
            {
                Session["UserId_Report"] = ddlUserList.SelectedValue;

                if (ddlReportType.SelectedIndex == 0)
                    Session["SubCtrl"] = "ReportPreviewCtrl.ascx";
                else if (ddlReportType.SelectedIndex == 1)
                    Session["SubCtrl"] = "ReportPreviewCtrl_IdvlRpt.ascx";
                else
                    Session["SubCtrl"] = "ReportPreviewCtrl_Certify.ascx";

                int userid = int.Parse(ddlUserList.SelectedValue);
                int testid = int.Parse(ddlTestList.SelectedValue);
                dataClasses.DeleteSectionMarks(userid, testid);
            }
            else
            {
                if (ddlUserGroup.SelectedIndex > 0)
                    Session["UserGroupID_Report"] = ddlUserGroup.SelectedValue;
                else { lblMessage.Text = "Please select a Group from the list"; return; }

                Session["UserId_Report"] = null;
                Session["SubCtrl"] = "ReportPreviewCtrl_GrpRpt.ascx";
            }
            Session["ReportType"] = ddlReportType.SelectedValue;
            Session["ScoringType"] = ddlSummaryGraph.SelectedValue;
            Session["UserTestID_Report"] = ddlTestList.SelectedValue; //GetUserTestId(ddlUserList.SelectedValue);

            Session["TestName"] = ddlTestList.SelectedItem.Text;

            Session["OrganiationID_Report"] = ddlOrganizationList.SelectedValue;

            //int userid = int.Parse(ddlUserList.SelectedValue);
            //int testid = int.Parse(ddlTestList.SelectedValue);
            //dataClasses.DeleteSectionMarks(userid, testid);

            //Session["SubCtrl"] = "ReportPreviewCtrl.ascx";//"ReportPreview.ascx"; //"ReportPreviewControl.ascx"; //

            Response.Redirect("FJAHome.aspx");

        }
        else if (ddlTestList.SelectedIndex <= 0 && ddlUserList.SelectedIndex <= 0)
        { //if (ddlUserList.SelectedIndex <= 0)
            lblMessage.Text = "Please select a Test/User from the list";
            // else lblMessage.Text = "Please select Graph type";
        }
        
        
        /*
        if (ddlUserList.SelectedIndex > 0)
        {
            Session["UserId_Report"] = ddlUserList.SelectedValue;
            
            Session["ReportType"] = ddlReportType.SelectedValue;
            Session["ScoringType"] = ddlSummaryGraph.SelectedValue;
            Session["UserTestID_Report"] = ddlTestList.SelectedValue; //GetUserTestId(ddlUserList.SelectedValue);

            Session["TestName"] = ddlTestList.SelectedItem.Text;
                        
            Session["OrganiationID_Report"] = ddlOrganizationList.SelectedValue;

            int userid = int.Parse(ddlUserList.SelectedValue);
            int testid = int.Parse(ddlTestList.SelectedValue);
            dataClasses.DeleteSectionMarks(userid, testid);

            Session["SubCtrl"] = "ReportPreviewCtrl.ascx";//"ReportPreview.ascx"; //"ReportPreviewControl.ascx"; //
            Response.Redirect("FJAHome.aspx");
        }
        else
        { //if (ddlUserList.SelectedIndex <= 0)
            lblMessage.Text = "Please select a User from the list";
            // else lblMessage.Text = "Please select Graph type";
        }
        */
        
    }
    protected void ddlUserList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["userIndex_Report"] = ddlUserList.SelectedIndex;
    }
    private string GetReportType(int testidreport)
    {
        string reporttype = "";
        var ReportTextDetails = from reportdet in dataClasses.ReportDescriptions
                                where reportdet.TestId == testidreport
                                select reportdet;
        if (ReportTextDetails.Count() > 0)
        {
            if (ReportTextDetails.First().ReportType != null)
                reporttype = ReportTextDetails.First().ReportType.ToString();
        }
        return reporttype;

    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReportType.SelectedIndex > 0)
            ddlReportCategory.Enabled = false;
    }
    protected void ddlUserGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUserGroup.SelectedIndex > 0)
        {
            Session["groupIndex_report"] = ddlUserGroup.SelectedIndex.ToString();
            FilluserList();
        }
        else { Session["groupIndex_report"] = null; Session["userIndex_Report"] = null; }
    }
}
