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

public partial class IndustryControl : System.Web.UI.UserControl
{
  AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    int indCode1 = 0;
    int PhNo = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        fillDataGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        int userid=0;
        if(Session["UserID"]!=null)
            userid=int.Parse(Session["UserID"].ToString());

        if (txtIndustryName.Text == "")
            lblMessage.Text = "Enter the values";
        else
        {
            if (Session["IndustryCode"] != null)
                indCode1 = int.Parse(Session["IndustryCode"].ToString());
            var details1 = from details in dataclasses.Industries where details.Name == txtIndustryName.Text && details.IndustryID != indCode1 select details;
            if (details1.Count() > 0)
            { lblMessage.Text = "IndustryName Duplication.Enter New Value"; return; }
            // && Session["IndustryCode"] == null)
            //

            //{
            //    indCode = int.Parse(Session["IndCode"].ToString());


            //        if (txtPhNo.Text != "")
            //{
            //    PhNo = txtPhNo.Text;

            dataclasses = new AssesmentDataClassesDataContext();
            dataclasses.AddIndustry(indCode1, txtIndustryName.Text,int.Parse(ddlStatus.SelectedValue),userid);
            lblMessage.Text = "value saved";
           ClearControls();
            Session["IndustryCode"] = null;
            fillDataGrid();

            //}
        }
    }


    private void fillDataGrid()
    {
        GridView1.DataSource = LinqDataSource1;
        GridView1.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtIndustryName.Text = GridView1.SelectedRow.Cells[1].Text;
        ddlStatus.SelectedValue=GridView1.SelectedRow.Cells[2].Text;
        Session["IndustryCode"] = GridView1.SelectedRow.Cells[3].Text;

    }
    private void ClearControls()
    {
        txtIndustryName.Text = "";
        ddlStatus.SelectedIndex = 0;
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
        Session["IndustryCode"] = null;
        txtIndustryName.Text = "";
        ddlStatus.SelectedIndex = 0;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["IndustryCode"] != null)
        {
            int industryid = int.Parse(Session["IndustryCode"].ToString());
            dataclasses.DeleteIndustry(industryid);
            Session["IndustryCode"] = null;
            ClearControls();
            lblMessage.Text = "Industry deleted successfullly";
            fillDataGrid();
        }
        else lblMessage.Text = "Please select a value for deletion";

    }
}
