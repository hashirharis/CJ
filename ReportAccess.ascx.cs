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

public partial class ReportAccess : System.Web.UI.UserControl
{
    int userid = 0;
    //int OrganizationId = 0; 
    int GroupUserID = 0;
    AssesmentDataClassesDataContext dataclass = new  AssesmentDataClassesDataContext();
    DBManagementClass dbClass = new DBManagementClass();
    bool specialadmin = false;
    int OrganizationID = 0;
    string organizationName;
    int TestId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {
            pnlOrganization.Visible = false;
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

            FillGroups();
            FillTestList();
        }
        else
            FillOrganisationValues();

    }
    protected void btnOrganisation_Click(object sender, EventArgs e)
    {
        int status = 1;
        if (ddlStaus_Org.SelectedIndex > 0)
            status = 0;
        if (ddlOrganisation.SelectedIndex > 0)
        {
            OrganizationID = int.Parse(ddlOrganisation.SelectedValue);
            dataclass.Procedure_ReportAccess(OrganizationID, 1,status);
            lblMessage.Text = "Report Access Status of Selected Organization is Saved Successfully";
            ClearControls();
        }
        else
        {
            lblMessage.Text = "Select an Organization";
        }
    }
    protected void btnGroup_Click(object sender, EventArgs e)
    {
        int status = 1;
        if (ddlStaus_Grp.SelectedIndex > 0)
            status = 0;
        if (ddlGroup.SelectedIndex > 0)
        {
            GroupUserID = int.Parse(ddlGroup.SelectedValue);
            dataclass.Procedure_ReportAccess(GroupUserID, 2,status);
            lblMessage.Text = "Report Access Status of Selected GroupUser is Saved Successfully";
            ClearControls();
        }
        else
        {
            lblMessage.Text = "Select a GroupUser";
        }
    }
    protected void btnUser_Click(object sender, EventArgs e)
    {
        int i = 0;
        int status = 1;
        if (ddlStaus_User.SelectedIndex > 0)
            status = 0;
        Boolean UserSelected = false;
        foreach (GridViewRow gr in gvwUserList.Rows)
        {
            userid = int.Parse(gvwUserList.Rows[i].Cells[3].Text);
            CheckBox cb = new CheckBox();
            cb = (CheckBox)gr.Controls[0].FindControl("CheckBox1");

            if (cb.Checked)
            {
                status = status;
                //else status = 0;

                dataclass.Procedure_ReportAccess(userid, 0, status);
                UserSelected = true;
            }
            i = i + 1;
        }
        

        if (UserSelected = true)
        {
            lblMessage.Text = "Report Access Status of Selected User(s) is Saved Successfully";
            ClearControls();
        }
        else
        {
            lblMessage.Text = "Select a User";
        }

        //int status = 0;//bool itemchecked=false;
        //foreach (GridViewRow gr in gvwQues.Rows)
        //{
        //    userid = int.Parse(gvwUserList.Rows[i].Cells[3].Text);
        //    CheckBox cb = new CheckBox();
        //    cb = (CheckBox)gr.Controls[0].FindControl("CheckBox1");
        //    if (cb.Checked)
        //    {
        //        status = 1; //itemchecked = true;
        //    }
        //    else status = 0;
        //    //dataclass.UpdateQuesStatus(int.Parse(gvwQues.Rows[i].Cells[1].Text), status, createdby);
        //    dataclass.Procedure_TestBaseQuestionList(0, testid, quesid, status, createdby, sectionid, testsectionid, firstsectionid, secondsectionid, thirdsectionid);
        //    i = i + 1;
        //    lblMessage.Text = "saved Successfully";
        //}



        //int status = 1;
        //if (ddlStaus_User.SelectedIndex > 0)
        //    status = 0;
        //Boolean UserSelected = false;
        //for (int i = 0; i < cblUser.Items.Count; i++)
        //{
        //    if (cblUser.Items[i].Selected == true)
        //    {
        //        userid = int.Parse(cblUser.Items[i].Value);
        //        dataclass.Procedure_ReportAccess(userid, 0,status);
        //        UserSelected = true;
        //    }
        //}
        
        //if (UserSelected = true)
        //{
        //    lblMessage.Text = "Report Access Status of Selected User(s) is Saved Successfully";
        //    ClearControls();
        //}
        //else
        //{
        //    lblMessage.Text = "Select a User";
        //}
    }
    protected void ddlOrganisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganisation.SelectedIndex > 0)
        {
            if (Session["OrgIndex"] != null)
            {
                if (Session["OrgIndex"].ToString() != ddlOrganisation.SelectedIndex.ToString())
                {
                    Session["OrgIndex"] = ddlOrganisation.SelectedIndex;                    
                    Session["GrpUserIndex"] = null; Session["testIndex"] = null;
                    FillGroups(); FillTestList(); 
                }
            }
            else
            {
                Session["OrgIndex"] = ddlOrganisation.SelectedIndex;
                Session["GrpUserIndex"] = null; Session["testIndex"] = null;
                FillGroups(); FillTestList(); 
                
            }
        }
        else {Session["OrgIndex"] = null; FillUsers();}

    }
    
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGroup.SelectedIndex > 0)
        {
            if (Session["GrpUserIndex"] != null)
            {
                if (Session["GrpUserIndex"].ToString() != ddlGroup.SelectedIndex.ToString())
                {
                    Session["GrpUserIndex"] = ddlGroup.SelectedIndex;
                    FillUsers();
                }
            }
            else
            {
                Session["GrpUserIndex"] = ddlGroup.SelectedIndex;
                FillUsers();
            }
        }
        else {Session["GrpUserIndex"] = null; FillUsers();}
    }

    private void FillOrganisationValues()
    {
        int orgIndex = 0, groupIndex = 0;
        ListItem listnew;
        if (specialadmin == false)
        {
            listnew = new ListItem("--select--", "0");
            ddlOrganisation.Items.Clear();
            ddlOrganisation.Items.Add(listnew);
            if (Session["OrgIndex"] != null)
            {
                orgIndex = int.Parse(Session["OrgIndex"].ToString());
            }

            ddlOrganisation.DataSource = LinqOrganisation;
            ddlOrganisation.DataTextField = "Name";
            ddlOrganisation.DataValueField = "OrganizationID";

            ddlOrganisation.DataBind();
            ddlOrganisation.SelectedIndex = orgIndex;
        }

        listnew = new ListItem("--select--", "0");
        ddlGroup.Items.Clear();
        ddlGroup.Items.Add(listnew);

        listnew = new ListItem("--select--", "0");
        ddlTestList.Items.Clear();
        ddlTestList.Items.Add(listnew);

        if (orgIndex > 0)
        {
            FillGroups();           
        }
        else
        {
            //if (specialadmin == false)
            //{
            if (Session["GrpUserIndex"] != null)
                groupIndex = int.Parse(Session["GrpUserIndex"].ToString());
            ddlGroup.DataSource = LinqGroup;
            ddlGroup.DataTextField = "GroupName";
            ddlGroup.DataValueField = "GroupUserID";
            ddlGroup.DataBind();
            ddlGroup.SelectedIndex = groupIndex;

            if (GroupUserID > 0)
                FillUsers();
            
        }
    }
    
    private void FillTestList()
    {
        if (specialadmin == false)
            organizationName = ddlOrganisation.SelectedItem.Text;

        var testListDetails = from testListdet in dataclass.TestLists
                             where testListdet.OrganizationName == organizationName && testListdet.Status == 1
                             select new { testListdet.TestName, testListdet.TestId };
        int testIndex = 0;
        if (Session["testIndex"] != null)
            testIndex = int.Parse(Session["testIndex"].ToString());
        ListItem listnew = new ListItem("--select--", "0");
        ddlTestList.Items.Clear();
        ddlTestList.Items.Add(listnew);
        ddlTestList.DataSource = testListDetails;
        ddlTestList.DataTextField = "TestName";
        ddlTestList.DataValueField = "TestId";
        ddlTestList.DataBind();
        ddlTestList.SelectedIndex = testIndex;

        FillUsers();
    }
                             

    private void FillGroups()
    {
        int groupIndex = 0;
        ListItem listnew = new ListItem("--select--", "0");
        ddlGroup.Items.Clear();
        ddlGroup.Items.Add(listnew);
        if (Session["GrpUserIndex"] != null)
            groupIndex = int.Parse(Session["GrpUserIndex"].ToString());

        if(specialadmin==false)
            OrganizationID=int.Parse(ddlOrganisation.SelectedValue);

        LinqGroup.Where = "OrganizationID = " + OrganizationID;
        ddlGroup.DataSource = LinqGroup;
        ddlGroup.DataTextField = "GroupName";
        ddlGroup.DataValueField = "GroupUserID";
        ddlGroup.DataBind();
        ddlGroup.SelectedIndex = groupIndex;

       // cblUser.Items.Clear();

        FillTestList();
        
        
    }

    private void FillUsers()
    {
        //if (ddlOrganisation.SelectedIndex > 0 || ddlGroup.SelectedIndex > 0 || ddlTestList.SelectedIndex > 0)
        //{
        //    gvwUserList.DataSource = "";
        //   // gvwUserList.DataBind();
        //}

        string whereCondition = "";

        if (specialadmin == false)
        {
            if (ddlOrganisation.SelectedIndex > 0)
                whereCondition = "OrganizationID = " + ddlOrganisation.SelectedValue;
        }
        else
            whereCondition = "OrganizationID = " + OrganizationID;

        if (whereCondition != "")
        {
            if (ddlTestList.SelectedIndex > 0)
                whereCondition += " and Testid = " + ddlTestList.SelectedValue;
            if (ddlGroup.SelectedIndex > 0)
                whereCondition += " and GrpUserID = " + ddlGroup.SelectedValue;

            LinqUserList.Where = whereCondition;
            gvwUserList.DataSource = LinqUserList;
            gvwUserList.DataBind();
        }

        if (txtUserSearch.Text.Trim() != "")
        {
            if (whereCondition != "")
                whereCondition += " and ";
            whereCondition += " username like '%" + txtUserSearch.Text.Trim() + "%'";
        }
        
        if (whereCondition != "")
        {
            string searchQuery = "select username,reportaccess,userid from userprofile where " + whereCondition;

        }

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Session["OrgIndex"] = null; Session["GrpUserIndex"] = null; Session["testIndex"] = null;
        ClearControls();
        FillOrganisationValues();
    }

    private void ClearControls()
    {
        ddlOrganisation.SelectedIndex = 0;
        ddlGroup.SelectedIndex = 0;
        ddlTestList.SelectedIndex = 0;

        ddlStaus_User.SelectedIndex = 0; ddlStaus_Org.SelectedIndex = 0;
        ddlStaus_Grp.SelectedIndex = 0; ddlStaus_Test.SelectedIndex = 0;

    }
    protected void ddlTestList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["testIndex"] = ddlTestList.SelectedIndex;
        //FillUsers();


        if (ddlTestList.SelectedIndex > 0)
        {
            if (Session["testIndex"] != null)
            {
                if (Session["testIndex"].ToString() != ddlTestList.SelectedIndex.ToString())
                {
                    Session["testIndex"] = ddlTestList.SelectedIndex;
                    FillUsers();
                }
            }
            else
            {
                Session["testIndex"] = ddlTestList.SelectedIndex;
                FillUsers();
            }
        }
        else { Session["testIndex"] = null; FillUsers(); }


    }
    protected void btnTestList_Click(object sender, EventArgs e)
    {

        int status = 1;
        if (ddlStaus_Test.SelectedIndex > 0)
            status = 0;
        if (ddlTestList.SelectedIndex > 0)
        {
            TestId = int.Parse(ddlTestList.SelectedValue);
            dataclass.Procedure_ReportAccess(TestId, 3, status);
            lblMessage.Text = "Report Access Status of Selected Test is Saved Successfully";
            ClearControls();
        }
        else
        {
            lblMessage.Text = "Select a Test";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void gvwUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageindex = e.NewPageIndex;
        gvwUserList.PageIndex = pageindex;
    }
}
