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

public partial class ChangePasswordControl : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    int userid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["pwdChanged"]!=null)
            if (Session["pwdChanged"].ToString() == "Yes")
            {
                pnlPasswordMessage.Visible = true; pnlPasswordChange.Visible = false;
            }
        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());

        if (userid == 0)
        {
            Session["UserID"] = null;
            Session["SubCtrl"] = null;           
            Response.Redirect("FJAHome.aspx");
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool passwordexists = false;
        if (txtPassword.Text.Trim() != "" && txtNewPassword.Text.Trim() != "" && txtConfirmPassword.Text.Trim() != "")
        {
            if (txtConfirmPassword.Text == txtNewPassword.Text)
            {
                var getPassword = from passworddet in dataclasses.UserProfiles
                                  where passworddet.UserId == userid && passworddet.Password==txtPassword.Text
                                  select passworddet;
                if (getPassword.Count() > 0)
                {
                    passwordexists = true;
                    dataclasses.Procedure_ChangePassword(userid, txtNewPassword.Text);
                    pnlPasswordChange.Visible = false; pnlPasswordMessage.Visible = true;
                    Session["pwdChanged"] = "Yes";
                }
                else { lblMessage.Text = "Wrong password !!!"; }

            }
            else { lblMessage.Text = "New password and confirm passwords should match."; }
        }
        else { lblMessage.Text = "Enter required values"; }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

}
