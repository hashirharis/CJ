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

public partial class TestPassMarkControl : System.Web.UI.UserControl
{
  AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
   {
       var getpassmark = from passmarkdet in dataclasses.TestPassmarks
                         select passmarkdet;
       if (getpassmark.Count() > 0)
           if (getpassmark.First().Passmark != null)
               txtpassmark.Text = getpassmark.First().Passmark.ToString();                
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {if (txtpassmark.Text.Trim() != "")
        //if (txtpassmark.Text.Trim() != "")
        {
            try
            {
                decimal passmark = 0;
                passmark = decimal.Parse(txtpassmark.Text);
                int userid = 0;
                if (Session["UserID"] != null)
                    userid = int.Parse(Session["UserID"].ToString());
                dataclasses.ProcedureAddTestPassmarks(passmark,userid );
                lblMesasage.Text = "Saved successfully";
            }
            catch (Exception ex) { lblMesasage.Text = ex.Message; }
        }
        else lblMesasage.Text = "Please enter mark percentage";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtpassmark.Text = "";
    }
}
