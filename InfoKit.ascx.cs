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
using System.Drawing;
public partial class InfoKit : System.Web.UI.UserControl
{
    DBManagementClass clsClass = new DBManagementClass();
    AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    //FJADataClassesDataContext dataclasses = new FJADataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        FillDownloadFileList();
        FillDetails();
    }
    private void FillDownloadFileList()
    {
        var infokitcategorylist = from infokitdet in dataclasses.InfokitCategories
                                  where infokitdet.Status == 1
                                  orderby infokitdet.DisplayOrder ascending
                                  select infokitdet;
        if (infokitcategorylist.Count() > 0)
        {
            int i = 1;
            HtmlTable dtInfikitList = new HtmlTable(); dtInfikitList.Width = "275";
            HtmlTableRow trInfokitList;
            HtmlTableCell tcellInfokitList;
            foreach (var categorylist in infokitcategorylist)
            {
                string categoryname = categorylist.CategoryName.ToString();
                int categoryid = int.Parse(categorylist.CategoryId.ToString());
                //Label lblGroupname = new Label();
                //lblGroupname.Font.Bold = true;
                //lblGroupname.Font.Size = 14;
                //lblGroupname.Text = categoryname;
                //trInfokitList = new HtmlTableRow();
                //tcellInfokitList = new HtmlTableCell();
                //tcellInfokitList.Controls.Add(lblGroupname);
                //trInfokitList.Cells.Add(tcellInfokitList);
                //dtInfikitList.Rows.Add(trInfokitList);

                var downloadfilelist = from downloadablefiles in dataclasses.InfokitFileLists
                                       where downloadablefiles.CategoryId == categoryid
                                       orderby downloadablefiles.DisplayOrder ascending
                                       select downloadablefiles;
                if (downloadfilelist.Count() > 0)
                {


                    //blank cell
                    Label lblGroupname = new Label(); lblGroupname.Width = 25; lblGroupname.Text = "";
                    trInfokitList = new HtmlTableRow();
                    tcellInfokitList = new HtmlTableCell();
                    tcellInfokitList.Controls.Add(lblGroupname);
                    trInfokitList.Cells.Add(tcellInfokitList);
                    dtInfikitList.Rows.Add(trInfokitList);
                    // if files exists under current category then add category name ..
                    lblGroupname = new Label(); lblGroupname.ForeColor = Color.Maroon;
                    lblGroupname.Font.Bold = true;
                    lblGroupname.Font.Size = 10;
                    lblGroupname.Text = categoryname;
                    trInfokitList = new HtmlTableRow();
                    tcellInfokitList = new HtmlTableCell(); //tcellInfokitList.Style.Add("text-align", "center");
                    tcellInfokitList.Controls.Add(lblGroupname);
                    trInfokitList.Cells.Add(tcellInfokitList);
                    dtInfikitList.Rows.Add(trInfokitList);


                    foreach (var filelist in downloadfilelist)
                    {
                        trInfokitList = new HtmlTableRow();
                        tcellInfokitList = new HtmlTableCell();
                        LinkButton lbtnFileName = new LinkButton();
                        lbtnFileName.ID = "FileName_" + i.ToString();
                        lbtnFileName.Text = filelist.DisplayName; lbtnFileName.Font.Size = 9;
                        lbtnFileName.Font.Underline = false;
                        lbtnFileName.CommandName = "FileName" + i.ToString();
                        lbtnFileName.CommandArgument = "images/InfoKitFiles/" + filelist.InfoFileName;
                        lbtnFileName.Click += new EventHandler(lbtnFileName_Click);
                        tcellInfokitList.Controls.Add(lbtnFileName);
                        trInfokitList.Cells.Add(tcellInfokitList);
                        dtInfikitList.Rows.Add(trInfokitList);

                        i++;
                    }
                }
            }

            pnlDownloadFileList.Controls.Clear();
            pnlDownloadFileList.Controls.Add(dtInfikitList);
        }
    }

    void lbtnFileName_Click(object sender, EventArgs e)
    {
        //throw new NotImplementedException();

        LinkButton btn = (LinkButton)sender;
        string filename = btn.CommandArgument;
        if (!File.Exists(Server.MapPath(filename)))
        { return; }
        Response.Redirect(filename);
       
        
    }

    private void FillDetails()
    {
        //return;
        string folderpath = "images/InfoKitFiles/";
        string str = "";
        int trainingtypeid = 2;
        var Details = from det in dataclasses.InfokitDynamicContents                      
                      select det;
        if (Details.Count() > 0)
        {//Class='mainimage'
            string trainingdetails = "<div style='width: 530px;'>";// height:450px;overflow: scroll'>";
            if (Details.First().Description1 != null && Details.First().Description1 != "")
                trainingdetails += Details.First().Description1.ToString();
            if (Details.First().Image1 != null && Details.First().Image1 != "")
                trainingdetails += "<br/>" + "<div align='center'><img Class='imageinInfikit' Src=" + folderpath + Details.First().Image1.ToString() + " /></div>";
            if (Details.First().Description2 != null && Details.First().Description2 != "")
                trainingdetails += "<br/>" + Details.First().Description2.ToString();
            if (Details.First().Image2 != null && Details.First().Image2 != "")
                trainingdetails += "<br/>" + "<div align='center'><img Class='imageinInfikit' Src=" + folderpath + Details.First().Image2.ToString() + " /></div>";
            if (Details.First().Description3 != null && Details.First().Description3 != "")
                trainingdetails += "<br/>" + Details.First().Description3.ToString();
            if (Details.First().Image3 != null && Details.First().Image3 != "")
                trainingdetails += "<br/>" + "<div align='center'><img Class='imageinInfikit' Src=" + folderpath + Details.First().Image3.ToString() + " /></div>";

            if (Details.First().Description4 != null && Details.First().Description4 != "")
                trainingdetails += "<br/>" + Details.First().Description4.ToString();
            if (Details.First().Image4 != null && Details.First().Image4 != "")
                trainingdetails += "<br/>" + "<div align='center'><img Class='imageinInfikit' Src=" + folderpath + Details.First().Image4.ToString() + " /></div>";

            if (Details.First().Description5 != null && Details.First().Description5 != "")
                trainingdetails += "<br/>" + Details.First().Description5.ToString();
            if (Details.First().Image5 != null && Details.First().Image5 != "")
                trainingdetails += "<br/>" + "<div align='center'><img Class='imageinInfikit' Src=" + folderpath + Details.First().Image5.ToString() + " /></div>";
            trainingdetails += "</div>";
            tcellInfoKitdetails.InnerHtml = trainingdetails;
            
            if (Details.First().PhotoImage != null && Details.First().PhotoImage != "")
            {
                str = "<div style='background-position: right bottom; width: 100px; height: 100px; vertical-align: top; text-align: right; font-weight: bold;background-image:url(" + folderpath + Details.First().PhotoImage.ToString() + "); background-repeat: no-repeat;'></div>";
               // str = "<img style='background-position: left top; width: 100px; height: 100px; vertical-align: top; text-align: right; font-weight: bold;background-image:url(" + folderpath + Details.First().PhotoImage.ToString() + "); background-repeat: no-repeat;'>";
               // Image1.ImageUrl = folderpath + Details.First().PhotoImage.ToString();
            }

        }
        if (str == "")
            str = "<div style='background-position: left top; width: 100px; height: 100px; vertical-align: top; text-align: right; font-weight: bold;background-image:url(" + folderpath + "Code-I image 1.jpg); background-repeat: no-repeat;'>";

        tcelPhotoimage.InnerHtml = str;

        //if (Details.First().PhotoImage != null && Details.First().Image5 != "")
       // tcelPhotoimage.InnerHtml = "background-image: url(images/Code-I image 1.jpg); background-repeat: no-repeat; background-position: left top";
        //string str = "<div style='background-position: left top; width: 100px; height: 100px; vertical-align: top; text-align: right; font-weight: bold;background-image:url(images/Code-I image 1.jpg); background-repeat: no-repeat;'>";
        //tcelPhotoimage.InnerHtml = str;
        //tcelPhotoimage.Style.Add("background-image", "url(images/Code-I image 1.jpg)");
        //tcelPhotoimage.Style.Add("background-repeat", "no-repeat");
        //tcelPhotoimage.Style.Add("background-position", "left top");
    }
}
