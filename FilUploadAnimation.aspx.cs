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

public partial class FilUploadAnimation : System.Web.UI.Page
{
    string imagefilePath;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.form1.Attributes.Add("OnLoad", "window.close();");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)   // Checking for any page errors
        {
            imagefilePath = FileUpload1.PostedFile.FileName;
            // Extract attributes
            string imageUrl = "";
            //string ext;
            String[] strs = FileUpload1.PostedFile.FileName.Split(new char[] { '\\', '\\' });
            if (strs.Length > 0)
            {
                String imageFleName = strs[strs.Length - 1];
                String[] testImg = imageFleName.Split(new char[] { '.' });
                int index = testImg.Count() - 1;
                //ext=testImg[index];
                //if (ext != "doc")
                //    lblMessage.Text = "Please Select a Valid Doc File";
                //else
                //{
                    //string imageExtension = Convert.ToString(testImg[index]);
                    //if (IsExtentionValid(imageExtension.ToLower()) == false)
                    //{ lblMessage.Text = "Please Select a valid file(.doc or .pdf only"; return; }

                    //bool imageExists = false;
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    if (Session["ImageBuf_" + i] != null)
                    //    { imageExists = true; break; }
                    //}
                    //// if (Session["ImageBuf_0"] == null)// && Session["ImageBuf_1"] == null && Session["ImageBuf_2"] == null)
                    //if (imageExists == false)
                    //{
                    //    string[] files = Directory.GetFiles(Server.MapPath("UploadedImages"));
                    //    foreach (string file in files)
                    //        File.Delete(file);
                    //}

                    FileUpload1.PostedFile.SaveAs(Server.MapPath("UploadedImages/" + imageFleName));
                    string script = "<script type=\"text/javascript\">SelectImage('" + imageFleName + "','" + Request["ctrl"].ToString() + "');</script>";
                    this.ClientScript.RegisterClientScriptBlock(System.Type.GetType("System.String"), "closewindow", script);
                    //string script = "<script type=\"text/javascript\">SelectImage('" + "UploadedImages/" + imageFleName + "','" + Request["ctrl"].ToString() + "');</script>";
                    //this.ClientScript.RegisterClientScriptBlock(System.Type.GetType("System.String"), "closewindow", script);
                //}
            }
        }
    }

    private bool IsExtentionValid(string extention)
    {
        try
        {
            if (extention != "doc" && extention != "pdf")
                return false;
            else return true;

        }
        catch (Exception ex) { return false; }

    }
}
