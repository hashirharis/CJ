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

public partial class AddQualificationControl : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataClasses = new AssesmentDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        gvwQualifications.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtQualification.Text.Trim() != "")
        {
            int qualificationId=0;
            if(Session["QualiId"]!=null)
                qualificationId=int.Parse(Session["QualiId"].ToString());

            dataClasses.AddQualification(qualificationId, txtQualification.Text, int.Parse(ddlStatus.SelectedValue), int.Parse(Session["UserID"].ToString()));
            lblMessage.Text = "saved successfully";
            txtQualification.Text = "";
            ddlStatus.SelectedIndex = 0;
            Session["QualiId"] = null;
            gvwQualifications.DataBind();
        }

    }
    protected void gvwQualifications_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["QualiId"] = gvwQualifications.SelectedRow.Cells[3].Text;
        txtQualification.Text = gvwQualifications.SelectedRow.Cells[1].Text;
        ddlStatus.SelectedValue = gvwQualifications.SelectedRow.Cells[2].Text;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Session["QualiId"] = null;
        txtQualification.Text = "";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["QualiId"] != null)
        {
            int qualificId = int.Parse(Session["QualiId"].ToString());
            dataClasses.DeleteQualification(qualificId);

            Session["QualiId"] = null;
            txtQualification.Text = "";
            lblMessage.Text = "Dleted successfully";
            gvwQualifications.DataBind();
        }
        else lblMessage.Text = "Please select a value for deletion";
    }
}
