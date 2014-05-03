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

public partial class DesignationControl : System.Web.UI.UserControl
{
AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    int postId = 0;
    int userId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        fillDataGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            userId = int.Parse(Session["UserID"].ToString());
        }
        if (txtPostName.Text.Trim() == "")
            lblMessage.Text = "Enter the values";
        else
        {
            var details1 = from details in dataclasses.Designations
                           where details.PostName == txtPostName.Text
                           select details;
            if (details1.Count() > 0 && Session["PostId"] == null)
            {
                lblMessage.Text = "Name Duplication. Enter a New  values";
            }
            else
            {

                //int userId = 0;// int.Parse(Session["UserId"].ToString());
                dataclasses = new AssesmentDataClassesDataContext();
                int Status = int.Parse(ddlStatus.SelectedValue);
                if (Session["PostId"] != null)
                {
                    postId = int.Parse(Session["PostId"].ToString());
                    
                }
                dataclasses.AddDesignation(postId, txtPostName.Text, Status, userId);
                lblMessage.Text = "Value Saved";
                ClearControls();
                Session["PostId"] = null;
                fillDataGrid();
            }
        }
    }
    private void fillDataGrid()
    {
        GridView1.DataSource = LinqDataSource1;
        GridView1.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["PostId"] = GridView1.SelectedRow.Cells[3].Text;
        txtPostName.Text = GridView1.SelectedRow.Cells[1].Text;
        ddlStatus.SelectedValue = GridView1.SelectedRow.Cells[2].Text;
    }
    private void ClearControls()
    {
        txtPostName.Text = "";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
        Session["PostId"] = null;
        txtPostName.Text = "";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["PostId"] != null)
        {
            int postid = int.Parse(Session["PostId"].ToString());
            dataclasses.DeleteDesignation(postid);
            Session["PostId"] = null;
            ClearControls();
            lblMessage.Text = "Designation deleted successfully";
            fillDataGrid();
        }
        else lblMessage.Text = "Please select a value for deletion";

    }
}
