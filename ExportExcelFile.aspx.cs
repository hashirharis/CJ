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
public partial class ExportExcelFile : System.Web.UI.Page
{
    //string imagefilePath;
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.form1.Attributes.Add("OnLoad", "window.close();");
        lblMessage.Text = "";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)   // Checking for any page errors
        {
            //imagefilePath = FileUpload1.PostedFile.FileName;
            // Extract attributes
            string imageUrl = "";
            string ext;
            String[] strs = FileUpload1.PostedFile.FileName.Split(new char[] { '\\', '\\' });
            if (strs.Length > 0)
            {
                String imageFleName = strs[strs.Length - 1];
                String[] testImg = imageFleName.Split(new char[] { '.' });
                int index = testImg.Count() - 1;
                ext = testImg[index];
                if (ext != "xls")
                {lblMessage.Text = "Please Select a Valid Excel File"; return;}
                if (File.Exists(Server.MapPath("images/" + imageFleName)))
                    File.Delete(Server.MapPath("images/" + imageFleName));

                FileUpload1.PostedFile.SaveAs(Server.MapPath("images/" + imageFleName));

                string script = "<script type=\"text/javascript\">SetExcelFile('" + imageFleName + "','" + Request["ctrl"].ToString() + "');</script>";
                this.ClientScript.RegisterClientScriptBlock(System.Type.GetType("System.String"), "closewindow", script);
                //}
            }
        }
    }
}
