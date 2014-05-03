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

public partial class FJAHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_PreInit(Object sender, EventArgs e)
    {
        if (MasterPageControl != null)
        {
            this.MasterPageFile = MasterPageControl;
        }
        else
        {
            this.MasterPageFile = "~/MasterPage5.master";
            Session["MasterCtrl"] = "~/MasterPage5.master";
        }
    }
    private string MasterPageControl
    {
        get
        {
            return Session["MasterCtrl"] as string;
        }
        set
        {
            Session["MasterCtrl"] = value;
        }
    }
}
