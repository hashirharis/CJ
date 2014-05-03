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

public partial class JobCategoryControl : System.Web.UI.UserControl
{
  AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    int jobCatCode = 0;
    int userId = 0;
    bool specialadmin = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
            specialadmin = true;

        fillDataGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtJobCategoryName.Text == "")
            lblMessage.Text = "Enter the Values";
        else
        {
            var details1 = from details in dataclasses.JobCategories
                           where details.Name == txtJobCategoryName.Text
                           select details;
            if (details1.Count() > 0 && Session["JobCatCode"] == null)
            {
                lblMessage.Text = "Name Duplication.Enter new values";
            }
            else
            {
                //dataclasses = new AssesmentDataClassesDataContext();
                int status = int.Parse(ddlStatus.SelectedValue);
                if (Session["UserID"] != null)
                    userId = int.Parse(Session["UserID"].ToString());
                if (Session["JobCatCode"] != null)
                {
                    jobCatCode = int.Parse(Session["JobCatCode"].ToString());
                    
                }
                int adminaccess = 1;
                if (specialadmin == true) adminaccess = 0;

                dataclasses.AddJobCategory(jobCatCode, txtJobCategoryName.Text, status, userId,adminaccess);
                lblMessage.Text = "Values are Saved";
                 ClearControls();
                 Session["JobCatCode"] = null;
                fillDataGrid();

            }
        }
    }


    private void fillDataGrid()
    {
        gvwJobCategory.DataSource = LinqJobCat;
        gvwJobCategory.DataBind();
    }

    protected void gvwJobCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["JobCatCode"] = gvwJobCategory.SelectedRow.Cells[3].Text;
        jobCatCode = int.Parse(gvwJobCategory.SelectedRow.Cells[3].Text);
        txtJobCategoryName.Text = gvwJobCategory.SelectedRow.Cells[1].Text;
        ddlStatus.SelectedValue = gvwJobCategory.SelectedRow.Cells[2].Text;
    }
    private void ClearControls()
    {

        txtJobCategoryName.Text = "";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
        Session["JobCatCode"] = null;
        txtJobCategoryName.Text = "";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["JobCatCode"] != null)
        {
            int jobcatid = int.Parse(Session["JobCatCode"].ToString());           
            dataclasses.DeletedJobCategory(jobcatid);
            Session["JobCatCode"] = null;
            ClearControls();
            lblMessage.Text = "Job category Deleted successfully";
            fillDataGrid();
        }
        else lblMessage.Text = "Please select a value for deletion";

    }
}
