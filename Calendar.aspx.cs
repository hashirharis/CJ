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

public partial class Calendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
      //  Close.Attributes.Add("OnClick", "window.opener.document.forms[0].submit();window.close();");
        this.form1.Attributes.Add("OnLoad", "window.close();");
        
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        //if (Calendar1.SelectedDate < DateTime.Today)
        //{ MessageBox.Show("Invalid date"); return; }
        //Session["SelectedDate"] = Calendar1.SelectedDate.ToShortDateString();

        //this.Dispose();
        //Chnage this to pass date and control name
        //SelectDate("12/6/08","txtFromDate)
        //pass txtFromdate as part of request
        //Request["ctrl"]
        string script = "<script type=\"text/javascript\">SelectDate('"+  Calendar1.SelectedDate.ToString("dd-MM-yyyy")+"','"+Request["ctrl"].ToString()+"');</script>";
        this.ClientScript.RegisterClientScriptBlock(System.Type.GetType("System.String"), "closewindow",script );

    }
}
