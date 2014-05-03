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
using System.Data.OleDb;

public partial class UploadTestResults : System.Web.UI.UserControl
{

    AssesmentDataClassesDataContext dataClass = new AssesmentDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpdateAndImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtfilename.Text.Trim() == "") { lblMessage.Text = "Please select a file"; return; }
            if (!File.Exists(Server.MapPath("images/" + txtfilename.Text)))
            { lblMessage.Text = "file not found"; return; }

            String[] strs = txtfilename.Text.Split(new char[] { '\\', '\\' });
            String imageFleName = strs[strs.Length - 1];
            String[] testImg = imageFleName.Split(new char[] { '.' });
            int index = testImg.Count() - 1;
            string ext;
            ext = testImg[index];
            if (ext != "xls")
            { lblMessage.Text = "Please Select a Valid Excel File"; return; }
            // code to insert dataworking....
            OleDbConnection oconn = new OleDbConnection
        (@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
        Server.MapPath("images/" + txtfilename.Text) + ";Extended Properties=Excel 8.0");

            SaveUserProfileDetails(oconn);// 02-06-2013

            //qusid1, usercode, tcellQues1.InnerHtml, answer, userid,testId,testsecid,quescategory
            //UserId	TestId	TestSectionId	QuestionId	Answer	QuestionCategory

            OleDbCommand ocmd = new OleDbCommand("select QuestionId, Answer, UserId, TestId, TestSectionId,QuestionCategory from [" + txtSheetName.Text + "$]", oconn);
            oconn.Open();  //Here [txtSheetName.Text$] is the name of the sheet in the Excel file where the data is present
            OleDbDataReader odr = ocmd.ExecuteReader();
            int questionId, testId, testSectionId, userId = 0;
            string answer, questionCategory = "";
            string userCode = "";
            string question = "";
            //UserId , UserCode , TestId , TestSectionId , QuestionId , Question , Answer , QuestionCategory

            while (odr.Read())
            {
                questionId = int.Parse(odr[0].ToString());
                //userCode = (odr[1].ToString());

                var Ques1 = from Ques in dataClass.QuestionCollections
                            where Ques.QuestionID == questionId
                        select Ques;
                if (Ques1.Count() > 0)
                {
                    question = Ques1.First().Question.ToString();
                }

                //question = (odr[2].ToString());
                answer = (odr[1].ToString());
                userId = int.Parse(odr[2].ToString());
                testId = int.Parse(odr[3].ToString());
                testSectionId = int.Parse(odr[4].ToString());
                questionCategory = (odr[5]).ToString();
                dataClass.Procedure_QuesAnswers(questionId, userCode, question, answer, userId, testId, testSectionId, questionCategory);
            }
            oconn.Close();
            lblMessage.Text = "Saved...";

            //////

        }
        catch (Exception ex) { lblMessage.Text = ex.Message; }
        
    }

    private void SaveUserProfileDetails(OleDbConnection oconn)
    {
        // User Profile Details starts here 02-06-2013

        /*UserId,	First_Name,	Middle_Name, Last_Name,	Gender,	Age, Email,	Contact_Num, Organisation_Id, User_Group_Id, Industry_Id, Industry_Name, 
        Vocation_Id, Designation, Experience_Year, Experience_Month, Present_Job_Exp_Year, Present_Job_Exp_Month, Education_Qualification_Id, 
        Education_Qualification_Text, Professional_Qualification, Professional_Certification*/

        String sheetname = "Profile";
        OleDbCommand ocmd = new OleDbCommand("select UserId, First_Name, Middle_Name, Last_Name, Gender, Age, Email, Contact_Num, Organisation_Id, User_Group_Id, " +
        " Industry_Id, Industry_Name, Vocation_Id, Designation, Experience_Year, Experience_Month, Present_Job_Exp_Year, Present_Job_Exp_Month, Education_Qualification_Id, " +
        " Education_Qualification_Text, Professional_Qualification, Professional_Certification from [" + sheetname + "$]", oconn);
        oconn.Open();  //Here [sheetname$] is the name of the sheet in the Excel file where the data is present
        OleDbDataReader odr = ocmd.ExecuteReader();
        int UserId = 0;
        while (odr.Read())
        {
            try
            {                
                if (odr[1] != null)
                    UserId = int.Parse(odr[1].ToString());
                string First_Name = "";
                if (odr[2] != null)
                    First_Name = (odr[2].ToString());
                string Middle_Name = "";
                if (odr[3] != null)
                    Middle_Name = (odr[3].ToString());
                string Last_Name = "";
                if (odr[4] != null)
                    Last_Name = (odr[4].ToString());
                string Gender = "";
                if (odr[5] != null)
                    Gender = (odr[5].ToString());
                int Age = 0;
                if (odr[6] != null)
                    Age = int.Parse(odr[6].ToString());
                string Email = "";
                if (odr[7] != null)
                    Email = (odr[7].ToString());
                string Contact_Num = "";
                if (odr[8] != null)
                    Contact_Num = (odr[8].ToString());
                int Organisation_Id = 0;
                if (odr[9] != null)
                    Organisation_Id = int.Parse(odr[9].ToString());
                int User_Group_Id = 0;
                if (odr[10] != null)
                    User_Group_Id = int.Parse(odr[10].ToString());

                int Industry_Id = 0;
                string Industry_Name = "";
                if (odr[11] != null)
                    Industry_Id = int.Parse(odr[11].ToString());
                else
                {
                    Industry_Name = (odr[12].ToString());
                    if (Industry_Name != "")
                    {
                        dataClass.AddIndustry(0, Industry_Name, 2, UserId);

                        var getindId = from inddet in dataClass.Industries
                                       where inddet.Name == Industry_Name
                                       select inddet;
                        if (getindId.Count() > 0)
                            if (getindId.First().IndustryID != null)
                                Industry_Id = int.Parse(getindId.First().IndustryID.ToString());
                    }
                }

                int Vocation_Id = int.Parse(odr[13].ToString());
                string Designation = "";
                if (odr[14] != null)
                    Designation = (odr[14].ToString());
                int Experience_Year = 0;
                if (odr[15] != null)
                    Experience_Year = int.Parse(odr[15].ToString());
                int Experience_Month = 0;
                if (odr[16] != null)
                    Experience_Month = int.Parse(odr[16].ToString());
                int Present_Job_Exp_Year = 0;
                if (odr[17] != null)
                    Present_Job_Exp_Year = int.Parse(odr[17].ToString());

                int Present_Job_Exp_Month = 0;
                if (odr[18] != null)
                    Present_Job_Exp_Month = int.Parse(odr[18].ToString());

                int Education_Qualification_Id = 0;
                string Education_Qualification_Text = "";
                if (odr[19] != null)
                    Education_Qualification_Id = int.Parse(odr[19].ToString());
                else
                {
                    if (odr[20] != null)
                    {
                        Education_Qualification_Text = (odr[20].ToString());
                        if (Education_Qualification_Text != "")
                            dataClass.AddQualification(0, Education_Qualification_Text, 2, UserId);
                    }
                }

                string Professional_Qualification = "";
                if (odr[21] != null)
                    Professional_Qualification = (odr[21].ToString());
                string Professional_Certification = "";
                if (odr[22] != null)
                    Professional_Certification = (odr[22].ToString());

                dataClass.AddUserProfile(UserId, First_Name, Middle_Name, Last_Name, Gender, Age, Organisation_Id, Industry_Id, Vocation_Id, User_Group_Id, Designation, Experience_Year, Experience_Month, Present_Job_Exp_Year, Present_Job_Exp_Month, Education_Qualification_Text, Professional_Certification, 2, UserId, Email, Contact_Num);
                
                dataClass.AddUserFirstLoginDate(UserId);

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                return;
            }

        }
        oconn.Close();


        // User Profile Details ends here
    }
}
