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

public partial class UserTrainingControl : System.Web.UI.UserControl
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        Session["evaldirection"] = "Next";
        ShowNextControl();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {       
        Session.Clear();
        Session["SubCtrl"] = null;
        //Session["MasterCtrl"] = "~/MasterPage.master";
        Response.Redirect("FJAHome.aspx");

    }
    protected void ptnPrevious_Click(object sender, EventArgs e)
    {
        Session["evaldirection"] = "Previous";
        ShowPreviousControl();
        
    }
    private void ShowPreviousControl()
    {
        Session["SubCtrl"] = "UserProfileControl.ascx";
        Response.Redirect("FJAHome.aspx");
    }

    private void ShowNextControl()
    {
        if (Session["evaldirection"] != null)
            if (Session["evaldirection"].ToString() == "Previous")
                ShowPreviousControl();

        Session["SubCtrl"] = "TrainingDisplay.ascx";// "TraingIntroduction.ascx";//
        Response.Redirect("FJAHome.aspx");
    }
}
