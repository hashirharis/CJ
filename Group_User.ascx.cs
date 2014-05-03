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

public partial class Group_User : System.Web.UI.UserControl
{
AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    int GroupUserID = 0;
    bool specialadmin = false;
    int OrganizationID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadGroup();// bipson 04082010
    }
    private void LoadGroup()
    {
        if (Session["usertype"] != null)
            if (Session["usertype"].ToString() == "SpecialAdmin")
            {
                LinqGroupUser.Where = "OrganizationId=" + Session["AdminOrganizationID"].ToString();
                specialadmin = true; OrganizationID = int.Parse(Session["AdminOrganizationID"].ToString());

                lblOrganization.Visible = false; ddlOrganization.Visible = false;
            }
            else LinqGroupUser.Where = "AdminAccess=1";

       gvwGroupUser.DataSource = LinqGroupUser;
       gvwGroupUser.DataBind();
    }
   
    private void ClearControls()
    {
        if (specialadmin == false)
            ddlOrganization.SelectedIndex = 0;
        ddlJobCategory.SelectedIndex = 0;
        txtGroupName.Text = "";

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (specialadmin == false)
            if (ddlOrganization.SelectedIndex <= 0)
            { lblMessage.Text = "Please select Organization"; return; }

        if (txtGroupName.Text.Trim() == "")
        { lblMessage.Text = "Enter groupname"; return; }

        //else
        //{
            try
            {
                int UserID = int.Parse(Session["UserID"].ToString());
                //int OrganizationID = 0;
                int JobCatID = 0;int adminaccess = 0;
                if (Session["GroupUserID"] != null)
                    GroupUserID = int.Parse(Session["GroupUserID"].ToString());
                if (specialadmin == false)
                    if (ddlOrganization.SelectedIndex > 0)
                    {
                        OrganizationID = int.Parse(ddlOrganization.SelectedValue);
                        adminaccess = 1;
                    }
                if (ddlJobCategory.SelectedIndex > 0)
                    JobCatID = int.Parse(ddlJobCategory.SelectedValue);
                int status = int.Parse(ddlStatus.SelectedValue);
                
                
                dataclass.AddGroupUser(GroupUserID, OrganizationID, JobCatID, txtGroupName.Text, status, UserID,adminaccess);

                ClearControls();
                lblMessage.Text = "Group Details are Saved Successfully";
                gvwGroupUser.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                return;
            }
       // }
    }
    
    protected void gvwGroupUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["GroupUserID"] = gvwGroupUser.SelectedRow.Cells[3].Text; ;
        txtGroupName.Text = gvwGroupUser.SelectedRow.Cells[1].Text;
        ddlStatus.SelectedValue = gvwGroupUser.SelectedRow.Cells[2].Text;
        if (specialadmin == false)
            ddlOrganization.SelectedValue = gvwGroupUser.SelectedRow.Cells[4].Text;
        ddlJobCategory.SelectedValue = gvwGroupUser.SelectedRow.Cells[5].Text;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["GroupUserID"] = null;
         ClearControls();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["GroupUserID"] != null)
        {
            int grpUserid = int.Parse(Session["GroupUserID"].ToString());
            dataclass.DeleteGroupUser(grpUserid);
            Session["GroupUserID"] = null;
            ClearControls();
            lblMessage.Text = "User group deleted successfully";
            //gvwGroupUser.DataBind();
            LoadGroup();
        }
        else lblMessage.Text = "Please select a value for deletion";
    }

}
