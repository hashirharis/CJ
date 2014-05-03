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

public partial class SpecialAdminCreation : System.Web.UI.UserControl
{
    DBManagementClass clsClass = new DBManagementClass();
    AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    int userCode = 0;
    int userId = 0;    
    int organizationid = 0;    
    string userName = "";    
    int testid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] != null)
        {
            txtPassword.Enabled = false; ddlOrg.Enabled = false;
        }

        if (ViewState["Password"] != null)
            txtPassword.Attributes.Add("Value", ViewState["Password"].ToString());
        ddlOrg.DataBind();
        if (!IsPostBack)
            fillValues();

        fillDataGrid();
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "" || ddlOrg.SelectedIndex < 0)
        { lblMessage.Text = "Enter the values"; }
        else
        {
            if (txtPassword.Text != "")
                ViewState["Password"] = txtPassword.Text;
            string fromDate = ValidateDate(txtLoginFromDate.Value);
            string toDate = ValidateDate(txtLoginToDate.Value);

            DateTime dtFrom = DateTime.Now.AddDays(-1);
            DateTime dtTo = DateTime.Now.AddDays(-1);

            if (fromDate != "")
                dtFrom = DateTime.Parse(fromDate);
            if (toDate != "")
                dtTo = DateTime.Parse(toDate);

            if (dtFrom > dtTo)
            { lblMessage.Text = "From Date must be less than Todate"; return; }

            if (Session["UserCode"] != null)
                userCode = int.Parse(Session["UserCode"].ToString());
            if (Session["UserID"] != null)
                userId = int.Parse(Session["UserID"].ToString());

            var checkDuplication = from userdetails in dataclasses.UserProfiles
                                   where (userdetails.UserName == txtUserName.Text.Trim() && userdetails.Password == txtPassword.Text.Trim()) && userdetails.UserId != userCode
                                   select userdetails;
            if (checkDuplication.Count() > 0)
            { lblMessage.Text = "Username/password already exists"; return; }
            int testid = 0;
            
            organizationid = int.Parse(ddlOrg.SelectedValue);
            int status = int.Parse(ddlStatus.SelectedValue);
            string emailid = txtEmailId.Text.Trim();
           //// dataclasses.AddUserCreation(userCode, txtUserName.Text, txtPassword.Text, "SpecialAdmin", organizationid, 0, dtFrom, dtTo, status, userId, emailid, testid, 1);
            int adminaccess = 0;
            if (status == 0)
            {
                adminaccess = 1;
            }
            if (userCode > 0)
            {
                dataclasses.Procedure_UpdateUserLoginDate(organizationid, dtTo);
                dataclasses.Procedure_UserStatus(organizationid, status);
            }

            dataclasses.Procedure_OrganizationAdmAccess(organizationid, adminaccess);

            lblMessage.Text = "Values Saved";
            ClearControls();
            Session["UserCode"] = null;
            fillDataGrid();
            Session["OrgIndex"] = null;
            ddlOrg.SelectedIndex = 0;

        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearControls();
        Session["UserCode"] = null;
        txtUserName.Text = "";
        txtPassword.Text = "";        
        ddlOrg.SelectedIndex = 0;
        ddlOrg.Enabled = true; txtPassword.Enabled = true;
        
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["UserCode"] != null)
        {
            lblMessageDelete.Text += "(" + ddlOrg.SelectedItem.Text + ")?";
            pnldelete.Visible = true; pnlDeleteConfirm.Visible = false;

            //int seluserid = int.Parse(Session["UserCode"].ToString());
            ////dataclasses.DeleteUser(seluserid);

            //// code to delete all entries related to selected organization // bip 28122009
            //var testlistdetail = from testlistdet in dataclasses.TestLists
            //                     where testlistdet.OrganizationName == ddlOrg.SelectedItem.Text
            //                     select testlistdet;
            //foreach (var testdetails in testlistdetail)
            //{
            //    int testid = testdetails.TestId;
            //    dataclasses.DeleteTestLists(testid);
            //}
            ////
            //dataclasses.DeleteUser(seluserid);
            //ClearControls();
            //Session["UserCode"] = null;
            //fillDataGrid();
            //lblMessage.Text = "Deleted successfully";

        }
        else lblMessage.Text = "Please select a user for deletion";
    }
    private void deleleAdmin()
    {
        if (ddlOrg.SelectedIndex <= 0)
        { lblMessage.Text = "Please select a user"; return; }
        int seluserid = int.Parse(Session["UserCode"].ToString());
        //dataclasses.DeleteUser(seluserid);

        // code to delete all entries related to selected organization // bip 28122009
        var testlistdetail = from testlistdet in dataclasses.TestLists
                             where testlistdet.OrganizationName == ddlOrg.SelectedItem.Text
                             select testlistdet;
        foreach (var testdetails in testlistdetail)
        {
            int testid = testdetails.TestId;
            dataclasses.DeleteTestLists(testid);
        }
        //
        dataclasses.DeleteUser(seluserid);
        ClearControls();
        Session["UserCode"] = null;
        fillDataGrid();
        lblMessage.Text = "Deleted successfully";

    }


    protected void gvwSpecialAdminCreation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["UserCode"] = gvwSpecialAdminCreation.SelectedRow.Cells[4].Text;
        fillValues();   
    }
    protected void gvwSpecialAdminCreation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwSpecialAdminCreation.PageIndex = e.NewPageIndex;
        fillDataGrid();
    }
    /*
    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        int selUserId = 0;
        int i = 0;
        bool itemchecked=false;
        foreach (GridViewRow gr in gvwSpecialAdminCreation.Rows)
        {
            selUserId = int.Parse(gvwSpecialAdminCreation.Rows[i].Cells[9].Text);
            CheckBox cb = new CheckBox();
            cb = (CheckBox)gr.Controls[0].FindControl("CheckBox1");
            if (cb.Checked)
            {
                itemchecked = true;
                dataclasses.DeleteUser(selUserId);
            }
            i = i + 1;
            
        }
        if (itemchecked == true)
        {
            ClearControls();
            Session["UserCode"] = null;
            fillDataGrid();
            lblMessageDelAll.Text = "Deletion successfull";
        }
        else lblMessageDelAll.Text = "please select user(s) from the above list";
    }
      */
    
    private void fillDataGrid()
    {

        var userdetailfromdb = from userdet in dataclasses.View_UserDetails
                               where userdet.AdminAccess == 1 && userdet.UserType == "SpecialAdmin"
                               select userdet;// new {"UserName, Password, UserType, OrgName, GroupName, UserId, OrganizationID, GroupUserID, LoginFromDate, LoginToDate, ReportAccess, Status, EmailId"};

        gvwSpecialAdminCreation.DataSource = userdetailfromdb;   
        gvwSpecialAdminCreation.DataBind();
        //if (gvwSpecialAdminCreation.Rows.Count <= 0)
        //    btnDeleteAll.Visible = false;
    }
    private void fillValues()
    {        
        int userID = 0;
        if (Session["UserCode"] != null)
            userID = int.Parse(Session["UserCode"].ToString());
        if (userID > 0)
        {
            var details1 = from details in dataclasses.UserProfiles where details.UserId == userID select details;
            
            if (details1.Count() > 0)
            {
                if (details1.First().UserName != null)
                    txtUserName.Text = details1.First().UserName.ToString();

                txtPassword.Enabled = false;
                txtPassword.Text = details1.First().Password.ToString();
                txtPassword.Attributes["Value"] = txtPassword.Text;
                ViewState["Password"] = txtPassword.Text;
                txtPassword.Attributes.Add("Value", ViewState["Password"].ToString());
                
                if (details1.First().OrganizationID != null)
                    for (int i = 0; i < ddlOrg.Items.Count; i++)
                    {
                        if (ddlOrg.Items[i].Value == details1.First().OrganizationID.ToString())
                        {
                            ddlOrg.SelectedIndex = i;
                            Session["OrgIndex"] = i.ToString();
                            ddlOrg.Enabled = false;
                            break;
                        }
                    }
                               

                if (details1.First().LoginFromDate != null)
                    txtLoginFromDate.Value = DateTime.Parse(details1.First().LoginFromDate.ToString()).ToString("dd-MM-yyyy");
                if (details1.First().LoginToDate != null)
                    txtLoginToDate.Value = DateTime.Parse(details1.First().LoginToDate.ToString()).ToString("dd-MM-yyyy");

                if (details1.First().Status != null)
                    ddlStatus.SelectedValue = details1.First().Status.ToString();
                if (details1.First().EmailId != null)
                    txtEmailId.Text = details1.First().EmailId.ToString();



            }
        }
    }
    private string ValidateDate(string ctrl)
    {
        try
        {
            if (ctrl.Length != 10)
            { lblMessage.Text = "Enter valid date"; return ""; }

            //DateTime dt = DateTime.Parse(txtPassword.Text);
            string dt = ctrl;
            string dateCheck = "";
            string date = "";
            string month = "";
            string year = "";
            for (int i = 0; i < dt.Length; i++)
            {
                char[] a = dt.ToCharArray();
                if (i < 2)
                    date += (a[i]).ToString();
                else if (i > 2 && i < 5)
                    month += (a[i]).ToString();
                else if (i > 5)
                    year += (a[i]).ToString();

            }
            dateCheck = month + "-" + date + "-" + year;
            string curdate = date + "-" + month + "-" + year;
            DateTime curdt = DateTime.Parse(dateCheck);
            //Session["dob"] = curdate;
            return curdt.ToString("MM-dd-yyyy");
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Enter valid date "; return "";
        }
    }

    private void ClearControls()
    {
        txtUserName.Text = "";
        txtPassword.Text = "";
        txtLoginFromDate.Value = "";
        txtLoginToDate.Value = "";
        txtEmailId.Text = "";
    }
    protected void btnDeleteYes_Click(object sender, EventArgs e)
    {
        lblMessageDeleteConfirm.Text = "If you delete this client admin, the uploaded data under selected organization" + "(" + ddlOrg.SelectedItem.Text + ") will be deleted permanently and  you cannot retrieve datas under this organization" + "(" + ddlOrg.SelectedItem.Text + ") later. <br/> ";
        pnldelete.Visible = false; pnlDeleteConfirm.Visible = true;
    }
    protected void btnDeleteNo_Click(object sender, EventArgs e)
    {
        pnldelete.Visible = false; pnlDeleteConfirm.Visible = false;
    }
    protected void btnDeleteYesConfirm_Click(object sender, EventArgs e)
    {
        pnldelete.Visible = false; pnlDeleteConfirm.Visible = false;
        deleleAdmin();
    }
}
