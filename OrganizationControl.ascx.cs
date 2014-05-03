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
using System.IO;
public partial class OrganizationControl : System.Web.UI.UserControl
{
  AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext ();
    int orgCode = 0;
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
        if (txOrgName.Text == "")
            lblMessage.Text = "Enter The values";
        else
        {
            var details1 = from details in dataclasses.Organizations
                           where details.Name == txOrgName.Text
                           select details;
            if (details1.Count() > 0 && Session["OrgCode"] == null)
            { lblMessage.Text = "Name Duplication. Enter the New Values"; }
            else
            {
                if (txtLogoRight.Value.Trim() != "")
                    SaveLogo(txtLogoRight.Value.Trim());
                if (txtLogoLeft.Value.Trim() != "")
                    SaveLogo(txtLogoLeft.Value.Trim());
                if (txtLogoMiddle.Value.Trim() != "")
                    SaveLogo(txtLogoMiddle.Value.Trim());

                //dataclasses = new AssesmentDataClassesDataContext();
                int Status = int.Parse(ddlStatus.SelectedValue);
                if (Session["OrgCode"] != null)
                {
                    orgCode = int.Parse(Session["OrgCode"].ToString());
                }
                
                dataclasses.AddOrganization(orgCode, txOrgName.Text, Status, userId, txtLogoRight.Value.Trim(), 1,txtLogoLeft.Value,txtLogoMiddle.Value);
                if (orgCode > 0)
                    dataclasses.Procedure_UserStatus(orgCode, Status);
                lblMessage.Text = "Value Saved";
               ClearControls();
                Session["OrgCode"] = null;
                fillDataGrid();
            }
        }
    }

    private void SaveLogo(string filename)
    {
        string sourcepath, destinationpath;
        sourcepath = "UploadedImages\\" + filename;
        destinationpath = "images\\bannerfiles\\" + filename;
        if (File.Exists(Server.MapPath(sourcepath)))
        {
            if (!File.Exists(Server.MapPath(destinationpath)))
                File.Copy(Server.MapPath(sourcepath), Server.MapPath(destinationpath));
            File.Delete(Server.MapPath(sourcepath));
        }
        else { }

    }

    private void fillDataGrid()
    {
        //gvwOrganization. = LinqDataSource1;
        gvwOrganization.DataSource = Linqorg;
        gvwOrganization.DataBind();
    }
    protected void gvwOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["OrgCode"] =gvwOrganization.SelectedRow.Cells[3].Text;
        FillOrganizationDetails();
       // txOrgName.Text = gvwOrganization.SelectedRow.Cells[1].Text;
       // ddlStatus.SelectedValue = gvwOrganization.SelectedRow.Cells[2].Text;
    }
    //08102010 bipson
    private void FillOrganizationDetails()
    {
        var orgdetails = from orgdet in dataclasses.Organizations where orgdet.OrganizationID == int.Parse(Session["OrgCode"].ToString()) select orgdet;
        if (orgdetails.Count() > 0)
        {
            txOrgName.Text = orgdetails.First().Name.ToString();
            if (orgdetails.First().LogoFileName != null)
                txtLogoRight.Value = orgdetails.First().LogoFileName.ToString();
            if (orgdetails.First().LogoFileNameLeft != null)
                txtLogoLeft.Value = orgdetails.First().LogoFileNameLeft.ToString();
            if (orgdetails.First().LogoFileNameMiddle!= null)
                txtLogoMiddle.Value = orgdetails.First().LogoFileNameMiddle.ToString();
            ddlStatus.SelectedValue = orgdetails.First().Status.ToString();
        }
    }
    private void ClearControls()
    {
        txOrgName.Text = "";
        if (txtLogoRight.Value != "")
        {
            deleteImage(txtLogoRight.Value, 1);
            txtLogoRight.Value = "";
        }
        if (txtLogoLeft.Value != "")
        {
            deleteImage(txtLogoLeft.Value, 1);
            txtLogoLeft.Value = "";
        }
        if (txtLogoMiddle.Value != "")
        {
            deleteImage(txtLogoMiddle.Value, 1);
            txtLogoMiddle.Value = "";
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
        Session["OrgCode"] = null;        
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["OrgCode"] != null)
        {
            int orgId = int.Parse(Session["OrgCode"].ToString());
            // code to delete all entries related to selected organization // bip 28122009
            var testlistdetail = from testlistdet in dataclasses.TestLists
                                 where testlistdet.OrganizationName == txOrgName.Text.Trim()
                                 select testlistdet;
            foreach (var testdetails in testlistdetail)
            {
                int testid = testdetails.TestId;
                dataclasses.DeleteTestLists(testid);
            }
            //
            dataclasses.DeleteOrganization(orgId);
            deleteImage(txtLogoRight.Value, 0);
            deleteImage(txtLogoLeft.Value, 0);
            deleteImage(txtLogoMiddle.Value, 0);
            Session["OrgCode"] = null;
            lblMessage.Text = "Organization deleted successfully";
            ClearControls();
            fillDataGrid();
        }
        else lblMessage.Text = "Please a select a value for deletion";

    }
    protected void btnDeleteLogo_Click(object sender, EventArgs e)
    {
        if (txtLogoRight.Value != "")
        {
            deleteImage(txtLogoRight.Value,0);
            txtLogoRight.Value = "";
            lblMessage.Text = "Logo deleted successfully";
        }
        else lblMessage.Text = "Logo not found ...";
    }
    private void deleteImage(String fileName, int index)
    {
        // 08102010 bipson
        
        if (fileName != "")
        {
            string sourcepath, destinationpath;
            sourcepath = "UploadedImages\\" + fileName;
            destinationpath = "images\\bannerfiles\\" + fileName;
            if (index == 0)
            {
                if (File.Exists(Server.MapPath(sourcepath)))
                    File.Delete(Server.MapPath(sourcepath));
                else if (File.Exists(Server.MapPath(destinationpath)))
                    File.Delete(Server.MapPath(destinationpath));
            }
            else
            {
                if (File.Exists(Server.MapPath(sourcepath)))
                    File.Delete(Server.MapPath(sourcepath));
            }

        }

    }
    protected void btnDeleteLogoLeft_Click(object sender, EventArgs e)
    {
        if (txtLogoLeft.Value != "")
        {
            deleteImage(txtLogoLeft.Value,0);
            txtLogoLeft.Value = "";
            lblMessage.Text = "Logo deleted successfully";
        }
        else lblMessage.Text = "Logo not found ...";
    }
    protected void btnDeleteLogoMiddle_Click(object sender, EventArgs e)
    {
        if (txtLogoMiddle.Value != "")
        {
            deleteImage(txtLogoMiddle.Value,0);
            txtLogoMiddle.Value = "";
            lblMessage.Text = "Logo deleted successfully";
        }
        else lblMessage.Text = "Logo not found ...";
    }
}
