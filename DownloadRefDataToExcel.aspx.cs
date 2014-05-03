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

public partial class DownloadRefDataToExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["dsDownloadtoExcel"] != null)
        {

            DataSet ds = new DataSet();
            ds = (DataSet)Session["dsDownloadtoExcel"];
            DataTable dt = new DataTable();// = GetData();
           // dt=(DataTable)Session["dsDownloadtoExcel"];
            dt = ds.Tables[0];

            string attachment = "attachment; filename=Employee.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    string newstr = dr[i].ToString().Replace(":", ".");
                    newstr = newstr.Replace(";", "");
                    newstr=newstr.Replace("\r", "");
                    newstr=newstr.Replace("\n", "");
                    Response.Write(tab + newstr);
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();

            this.Dispose();


            
        }
        
        
    }
}
