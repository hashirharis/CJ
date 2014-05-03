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
public partial class UploadTrainingDetailsControl : System.Web.UI.UserControl
{
AssesmentDataClassesDataContext dataclasses = new  AssesmentDataClassesDataContext();
    string desc1, desc2, desc3, desc4, desc5, videodesc1, videodesc2;
    string img1 = "", img2 = "", img3 = "", img4 = "", img5 = "", video1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowImages();
    }
    private void ClearValues()
    {
        FreeTextBox1.Text = "";
        FreeTextBox2.Text = "";
        FreeTextBox3.Text = "";
        FreeTextBox4.Text = "";
        FreeTextBox5.Text = "";
        FreeTextBox6.Text = "";
        FreeTextBox7.Text = "";
        txtVideoFile.Value = "";

        Image1.Src = "";
        Image2.Src = "";
        Image3.Src = "";
        Image4.Src = "";
        Image5.Src = "";

        ClearSessionValues();
        
    }
    private void ClearSessionValues()
    {
        Session["FJAImageFile_1_cur"] = null;
        Session["FJAImageFile_2_cur"] = null;
        Session["FJAImageFile_3_cur"] = null;
        Session["FJAImageFile_4_cur"] = null;
        Session["FJAImageFile_5_cur"] = null;

        Session["FJAImageFile_1"] = null;
        Session["FJAImageFile_2"] = null;
        Session["FJAImageFile_3"] = null;
        Session["FJAImageFile_4"] = null;
        Session["FJAImageFile_5"] = null;

    }
    private void ShowImages()
    {
        if (Session["FJAImageFile_1_cur"] != null)
            Image1.Src = "images/" + Session["FJAImageFile_1_cur"].ToString();
        if (Session["FJAImageFile_2_cur"] != null)
            Image2.Src = "images/" + Session["FJAImageFile_2_cur"].ToString();
        if (Session["FJAImageFile_3_cur"] != null)
            Image3.Src = "images/" + Session["FJAImageFile_3_cur"].ToString();
        if (Session["FJAImageFile_4_cur"] != null)
            Image4.Src = "images/" + Session["FJAImageFile_4_cur"].ToString();
        if (Session["FJAImageFile_5_cur"] != null)
            Image5.Src = "images/" + Session["FJAImageFile_5_cur"].ToString();


        if (Session["FJAImageFile_1"] != null)
            Image1.Src = "UploadedImages/" + Session["FJAImageFile_1"].ToString();
        if (Session["FJAImageFile_2"] != null)
            Image2.Src = "UploadedImages/" + Session["FJAImageFile_2"].ToString();
        if (Session["FJAImageFile_3"] != null)
            Image3.Src = "UploadedImages/" + Session["FJAImageFile_3"].ToString();
        if (Session["FJAImageFile_4"] != null)
            Image4.Src = "UploadedImages/" + Session["FJAImageFile_4"].ToString();
        if (Session["FJAImageFile_5"] != null)
            Image5.Src = "UploadedImages/" + Session["FJAImageFile_5"].ToString();

    }

    private void SaveFile(string filenameOld, string filenameNew)
    {
        string imagePath = "images/" + filenameOld;
        string FilePath = "UploadedImages/" + filenameNew;
        //string newfilename = "images/" + filenameNew;

        if (File.Exists(Server.MapPath(imagePath)))
        { File.Delete(Server.MapPath(imagePath)); }
        File.Copy(Server.MapPath(FilePath), Server.MapPath(imagePath));
        File.Delete(Server.MapPath(FilePath));

    }
    private string GenerateVideoFilename(int index)
    {
        string FilePath = txtVideoFile.Value;

        String[] testImg = FilePath.Split(new char[] { '.' });
        int index1 = testImg.Count() - 1;
        string imageExtension = Convert.ToString(testImg[index1]);

        string imgName = ddlIntroductioncategories.SelectedItem.Text.Replace("/", "");// ddlIntroductioncategories.SelectedItem.Text.Replace("/", "");
        imgName = imgName.Replace(" ", "");
        string imageFileName = imgName + "_video" + index + "." + imageExtension;
        return imageFileName;
    }
    private string GenerateFilename(int index)
    {
        string FilePath = Session["FJAImageFile_" + index].ToString();

        String[] testImg = FilePath.Split(new char[] { '.' });
        int index1 = testImg.Count() - 1;
        string imageExtension = Convert.ToString(testImg[index1]);
        //imgfilename = txtName.Text.Replace(" ", "") + "." + imageExtension;
        //imgfilename = txtUserCode.Text.Replace(" ", "") + "." + imageExtension;

        string imgName = ddlIntroductioncategories.SelectedItem.Text.Replace("/", "");// ddlIntroductioncategories.SelectedItem.Text.Replace("/", "");
        imgName = imgName.Replace(" ", "");
        string imageFileName = imgName + "_image" + index + "." + imageExtension;

        return imageFileName;
    }
    private void GetImageName()
    {
        if (Session["FJAImageFile_1_cur"] != null)
            img1 = Session["FJAImageFile_1_cur"].ToString();
        if (Session["FJAImageFile_2_cur"] != null)
            img2 = Session["FJAImageFile_2_cur"].ToString();
        if (Session["FJAImageFile_3_cur"] != null)
            img3 = Session["FJAImageFile_3_cur"].ToString();
        if (Session["FJAImageFile_4_cur"] != null)
            img4 = Session["FJAImageFile_4_cur"].ToString();
        if (Session["FJAImageFile_5_cur"] != null)
            img5 = Session["FJAImageFile_5_cur"].ToString();

        if (Session["FJAVideoFile_cur"] != null)
            video1 = Session["FJAVideoFile_cur"].ToString();

        //if (File.Exists(Server.MapPath(imagePath)))
        //{ File.Delete(Server.MapPath(imagePath)); }
        //File.Copy(Server.MapPath(FilePath), Server.MapPath(imagePath));
        //File.Delete(Server.MapPath(FilePath));
        //Session["FJAVideoFile_cur"]

        if (Session["FJAImageFile_1"] != null)
        {
            if (img1 == "")
                img1 = GenerateFilename(1);

            SaveFile(img1, Session["FJAImageFile_1"].ToString());
            //img1 = Session["FJAImageFile_1"].ToString();
        }
        if (Session["FJAImageFile_2"] != null)
        {
            if (img2 == "")
                img2 = GenerateFilename(2);

            SaveFile(img2, Session["FJAImageFile_2"].ToString());
            //img2 = Session["FJAImageFile_2"].ToString();
        }
        if (Session["FJAImageFile_3"] != null)
        {
            if (img3 == "")
                img3 = GenerateFilename(3);

            SaveFile(img3, Session["FJAImageFile_3"].ToString());
            //img3 = Session["FJAImageFile_3"].ToString();
        }
        if (Session["FJAImageFile_4"] != null)
        {
            if (img4 == "")
                img4 = GenerateFilename(4);

            SaveFile(img4, Session["FJAImageFile_4"].ToString());
            //img4 = Session["FJAImageFile_4"].ToString();
        }
        if (Session["FJAImageFile_5"] != null)
        {
            if (img5 == "")
                img5 = GenerateFilename(5);

            SaveFile(img5, Session["FJAImageFile_5"].ToString());
            //img5 = Session["FJAImageFile_5"].ToString();
        }


        if (video1 != txtVideoFile.Value.Trim())
        {
            if (video1 == "")
                video1 = GenerateVideoFilename(1);

            SaveFile(video1, txtVideoFile.Value.Trim());
            //video1 = txtVideoFile.Value;
        }
        //if (txtVideoFile.Value.Trim() != "")           

    }


    private void GetDescriptionDetails()
    {
        desc1 = FreeTextBox1.Text;
        desc2 = FreeTextBox2.Text;
        desc3 = FreeTextBox3.Text;
        desc4 = FreeTextBox4.Text;
        desc5 = FreeTextBox5.Text;
        videodesc1 = FreeTextBox6.Text;
        videodesc2 = FreeTextBox7.Text;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (FreeTextBox1.Text == "" && FreeTextBox2.Text == "" && FreeTextBox3.Text == "" && FreeTextBox4.Text == "" &&
               FreeTextBox5.Text == "" && FreeTextBox6.Text == "" && FreeTextBox7.Text == "")
            { lblMessage.Text = "Enter training details"; return; }
            if (ddlIntroductioncategories.SelectedIndex > 0)
            {
                //pnlProcessing.Visible = true;
                int userid = 0;//int.Parse(Session["UserID"].ToString());
                if (Session["UserID"] != null)
                    userid = int.Parse(Session["UserID"].ToString());
                int trainingtypeid = 0;
                trainingtypeid = int.Parse(ddlIntroductioncategories.SelectedValue);
                GetDescriptionDetails();
                GetImageName();

                dataclasses.Procedure_UserTrainingDetails(trainingtypeid, desc1, img1, desc2, img2, desc3, img3, desc4, img4, desc5, img5, videodesc1, video1, videodesc2, userid, 1);
                ddlIntroductioncategories.SelectedIndex = 0;
                ClearValues();
                lblMessage.Text = "Saved Successfully....";
                Session["Trainingindex"] = null;
                
            }
        }
        catch (Exception ex) { }
        //pnlProcessing.Visible = false;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearValues(); ddlIntroductioncategories.SelectedIndex = 0;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlIntroductioncategories.SelectedIndex > 0)
        {
            DeleteTrainingImagesAndVideos();
            int trainingtypeid = int.Parse(ddlIntroductioncategories.SelectedValue);
            dataclasses.Procedure_DeleteUserTrainingDetails(trainingtypeid);
            ClearValues();
            lblMessage.Text = "Deleted successfully";
        }
        else lblMessage.Text = "Please select Category"; return;
    }
    private void DeleteTrainingImagesAndVideos()
    {        
        var trainingdet = from trainingdetails in dataclasses.UserTrainingDetails
                          where trainingdetails.TrainingTypeId == int.Parse(ddlIntroductioncategories.SelectedValue)
                          select trainingdetails;
        if (trainingdet.Count() > 0)
        {
            string filname = "";
            if (trainingdet.First().Image1 != null)
            {
                filname = "images/" + trainingdet.First().Image1.ToString();
                if (File.Exists(Server.MapPath(filname)))
                    File.Delete(Server.MapPath(filname));
            }
            if (trainingdet.First().Image2 != null)
            {
                filname = "images/" + trainingdet.First().Image2.ToString();
                if (File.Exists(Server.MapPath(filname)))
                    File.Delete(Server.MapPath(filname));
            }
            if (trainingdet.First().Image3 != null)
            {
                filname = "images/" + trainingdet.First().Image3.ToString();
                if (File.Exists(Server.MapPath(filname)))
                    File.Delete(Server.MapPath(filname));
            }
            if (trainingdet.First().Image4 != null)
            {
                filname = "images/" + trainingdet.First().Image4.ToString();
                if (File.Exists(Server.MapPath(filname)))
                    File.Delete(Server.MapPath(filname));
            }
            if (trainingdet.First().Image5 != null)
            {
                filname = "images/" + trainingdet.First().Image5.ToString();
                if (File.Exists(Server.MapPath(filname)))
                    File.Delete(Server.MapPath(filname));
            }
            if (trainingdet.First().VideoFile1 != null)
            {
                filname = trainingdet.First().VideoFile1.ToString();
                if (File.Exists(Server.MapPath(filname)))
                    File.Delete(Server.MapPath(filname));
            }
        }
    }
    private void FillTrainingDetails()
    {
        ClearValues();
        var trainingdet = from trainingdetails in dataclasses.UserTrainingDetails
                          where trainingdetails.TrainingTypeId == int.Parse(ddlIntroductioncategories.SelectedValue)
                          select trainingdetails;
        if (trainingdet.Count() > 0)
        {
            if (trainingdet.First().Description1 != null)
                FreeTextBox1.Text = trainingdet.First().Description1.ToString();
            if (trainingdet.First().Image1 != null)
            {
                Image1.Src = "images/" + trainingdet.First().Image1.ToString();
                Session["FJAImageFile_1_cur"] = trainingdet.First().Image1.ToString();
            }
            if (trainingdet.First().Description2 != null)
                FreeTextBox2.Text = trainingdet.First().Description2.ToString();
            if (trainingdet.First().Image2 != null)
            {
                Image2.Src = "images/" + trainingdet.First().Image2.ToString();
                Session["FJAImageFile_2_cur"] = trainingdet.First().Image2.ToString();
            }
            if (trainingdet.First().Description3 != null)
                FreeTextBox3.Text = trainingdet.First().Description3.ToString();
            if (trainingdet.First().Image3 != null)
            {
                Image3.Src = "images/" + trainingdet.First().Image3.ToString();
                Session["FJAImageFile_3_cur"] = trainingdet.First().Image3.ToString();
            }
            if (trainingdet.First().Description4 != null)
                FreeTextBox4.Text = trainingdet.First().Description4.ToString();
            if (trainingdet.First().Image4 != null)
            {
                Image4.Src = "images/" + trainingdet.First().Image4.ToString();
                Session["FJAImageFile_4_cur"] = trainingdet.First().Image4.ToString();
            }
            if (trainingdet.First().Description5 != null)
                FreeTextBox5.Text = trainingdet.First().Description5.ToString();
            if (trainingdet.First().Image5 != null)
            {
                Image5.Src = "images/" + trainingdet.First().Image5.ToString();
                Session["FJAImageFile_5_cur"] = trainingdet.First().Image5.ToString();
            }
            if (trainingdet.First().VideoDescription1 != null)
                FreeTextBox6.Text = trainingdet.First().VideoDescription1.ToString();
            if (trainingdet.First().VideoDescription2 != null)
                FreeTextBox7.Text = trainingdet.First().VideoDescription2.ToString();
            if (trainingdet.First().VideoFile1 != null)
            {
                txtVideoFile.Value = trainingdet.First().VideoFile1.ToString();
                Session["FJAVideoFile_cur"] = trainingdet.First().VideoFile1.ToString();
            }
        }
    }

    protected void ddlIntroductioncategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            if (Session["Trainingindex"] != null)
                if (Session["Trainingindex"].ToString() == ddlIntroductioncategories.SelectedIndex.ToString())
                    return;
            
            Session["Trainingindex"] = ddlIntroductioncategories.SelectedIndex.ToString();
            FillTrainingDetails();
           // pnlProcessingSel.Visible = false;
        }
        catch (Exception ex) { lblMessage.Text = ex.Message; }
    }
    protected void btnImageDel1_Click(object sender, EventArgs e)
    {
        DeleteImage(1);
    }
    private void ClearImage(int index)
    {
        Session["FJAImageFile_" + index + "_cur"] = null; Session["FJAImageFile_" + index] = null; Session["FJAVideoFile_cur"] = null;
        if (index == 1) Image1.Src = "";
        else if (index == 2) Image2.Src = "";
        else if (index == 3) Image3.Src = "";
        else if (index == 4) Image4.Src = "";
        else if (index == 5) Image5.Src = "";
        else if (index == 6) txtVideoFile.Value = "";
    }
    private void DeleteImage(int index)
    {
        if (Session["FJAImageFile_" + index + "_cur"] == null && Session["FJAImageFile_" + index] == null && Session["FJAVideoFile_cur"] == null)
            return;
        string imagename = "";
        bool imagefordelete = false;
        if (Session["FJAImageFile_" + index + "_cur"] != null && Session["FJAImageFile_" + index + "_cur"].ToString() != "")
        {
            imagename = Session["FJAImageFile_" + index + "_cur"].ToString();
            imagefordelete = true;
        }
        if (index == 6)
            if (Session["FJAVideoFile_cur"] != null)
            {
                imagename = Session["FJAVideoFile_cur"].ToString();
                imagefordelete = true;
            }

        if (imagefordelete == true && imagename != "")
        {
            string imagePath = "images/" + imagename;
            if (File.Exists(Server.MapPath(imagePath)))
                File.Delete(Server.MapPath(imagePath));

            int trainingtypeid = int.Parse(ddlIntroductioncategories.SelectedValue);
            dataclasses.Procedure_DeleteUserTrainingDetailsImage(trainingtypeid, index);

        }

        ClearImage(index);

    }
    protected void btnImageDel2_Click(object sender, EventArgs e)
    {
        DeleteImage(2);
    }
    protected void btnImageDel3_Click(object sender, EventArgs e)
    {
        DeleteImage(3);
    }
    protected void btnImageDel4_Click(object sender, EventArgs e)
    {
        DeleteImage(4);
    }
    protected void btnImageDel5_Click(object sender, EventArgs e)
    {
        DeleteImage(5);
    }
    protected void btnImageDel6_Click(object sender, EventArgs e)
    {
        DeleteImage(6);
    }
}
