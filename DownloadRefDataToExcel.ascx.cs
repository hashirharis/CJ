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

public partial class DownloadRefDataToExcel : System.Web.UI.UserControl
{
    DBManagementClass clsClass = new DBManagementClass();
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    DataTable dt = new DataTable();
    DataTable dtTestSection = new DataTable();
    int userid = 0; int testid = 0; string username = "";
    bool specialadmin = false;
    int OrganizationID = 0;
    string organizationName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "SpecialAdmin")
        {
            ddlOrganizationList.Visible = false; lblOrganization.Visible = false;
            specialadmin = true; OrganizationID = int.Parse(Session["AdminOrganizationID"].ToString());

            if (Session["adminOrgName"] != null)
                organizationName = Session["adminOrgName"].ToString();
            else
            {
                var orgName = from orgDet in dataclass.Organizations
                              where orgDet.OrganizationID == OrganizationID
                              select orgDet;
                if (orgName.Count() > 0)
                { organizationName = orgName.First().Name.ToString(); Session["adminOrgName"] = organizationName; }

            }
            //FillTestList();
        }
        else
        ddlOrganizationList.DataBind();

        if (Session["orgIndex_report"] != null)
            ddlOrganizationList.SelectedIndex = int.Parse(Session["orgIndex_report"].ToString());
        FillTestList();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        GetReportGraphDetailsFromDB();

        //Session["dsDownloadtoExcel"] = null;
        //string querystring = "Select * from UserProfile";
        //DataSet dsuserdet = new DataSet();
        //dsuserdet = clsClass.GetValuesFromDB(querystring);
        //if(dsuserdet!=null)
        //    if(dsuserdet.Tables.Count>0)
        //        if (dsuserdet.Tables[0].Rows.Count > 0)
        //        {
        //            Session["dsDownloadtoExcel"] = dsuserdet;

        //            GridView1.DataSource = dsuserdet.Tables[0];
        //            GridView1.DataBind();
        //        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Session["dsDownloadtoExcel_refData"] != null)
        {
            Session["dsDownloadtoExcel"] = Session["dsDownloadtoExcel_refData"];
            Response.Redirect("DownloadRefDataToExcel.aspx");
        }
    }

    private void FillTestList()
    {
        int testindex = 0;
        if (Session["testIndex_report"] != null)
            testindex = int.Parse(Session["testIndex_report"].ToString());
        ddlTestList.Items.Clear();
        ListItem litem = new ListItem("-- Select --", "0");
        ddlTestList.Items.Add(litem);

        if (specialadmin == false)
            organizationName = ddlOrganizationList.SelectedItem.Text;

        if (ddlOrganizationList.SelectedIndex > 0 || specialadmin == true)
        {
            var Gettestdetails = from testdet in dataclass.TestLists
                                 where testdet.OrganizationName == organizationName
                                 select testdet;
            if (Gettestdetails.Count() > 0)
            {
                ddlTestList.DataSource = Gettestdetails;
                ddlTestList.DataTextField = "TestName";
                ddlTestList.DataValueField = "TestId";
                ddlTestList.DataBind();
                if (testindex > 0)
                {
                    testid = int.Parse(ddlTestList.SelectedValue);
                    ddlTestList.SelectedIndex = testindex;
                    GetReportGraphDetailsFromDB();
                }
            }
        }
    }
    private void GetReportGraphDetailsFromDB()
    {
        try
        {
            ////return;
            DataSet ds;

            if (Session["dsDownloadtoExcel_refData"] != null)
            {
                ds = new DataSet();
                ds = (DataSet)Session["dsDownloadtoExcel_refData"];
                GridView1.DataSource = ds.Tables[0];// dt;
                GridView1.DataBind();
                return;
            }


            dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("TestID");
            dt.Columns.Add("TestSectionID");
            dt.Columns.Add("SectionName");
            dt.Columns.Add("SectionID");
            dt.Columns.Add("TotalMarks");
            dt.Columns.Add("UserId");
            dt.Columns.Add("UserName");


            //int userid = int.Parse(Session["UserId_Report"].ToString());
            int MemmoryTestImage = 0;
            int MemmoryTestText = 0;
            int QuesCollection = 0;
            string sectionname = ""; int TestSecionID = 0;
            //SqlConnection conn;
            //SqlCommand cmd;
            //SqlDataAdapter da;

            Table tblDisplay = new Table();
            TableCell tblCell = new TableCell();
            TableRow tblRow;
            Label label;
            int i = 0;
            int rowid = 0;
            int totalmarks = 0;
            int sectionid = 0;
            ////int testid = int.Parse(Session["UserTestID_Report"].ToString());
// bip 07052010
            var UserList = from Userprf in dataclass.UserProfiles
                           where Userprf.TestId == testid && Userprf.FirstLoginDate.HasValue == true 
                           select Userprf;
            if (UserList.Count() > 0)
                foreach (var userlistdetails in UserList)
                {
                    username = userlistdetails.UserName;
                    userid = userlistdetails.UserId;
                    var UserAnsws1 = from UserAnsws in dataclass.EvaluationResults
                                     where UserAnsws.UserId == userid   // && UserAnsws.TestId == testid
                                     select UserAnsws;

                    if (UserAnsws1.Count() > 0)
                    {
                        foreach (var UserAnswers in UserAnsws1)
                        {
                            int marks = 0;
                            int currentmarks = 0;
                            sectionid = 0;
                            if (UserAnswers.Category == "MemTestWords")
                            {
                                var MemWords1 = from MemWords in dataclass.MemmoryTestTextQuesCollections
                                                where MemWords.Question == UserAnswers.Question && MemWords.QuestionID == UserAnswers.QuestionID
                                                select MemWords;
                                if (MemWords1.Count() > 0)
                                {
                                    TestSecionID = int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = MemWords1.First().SectionName.ToString();
                                    sectionid = GetSectionId(sectionname);// int.Parse(MemWords1.First().SectionId.ToString());
                                    if (UserAnswers.Answer != null)
                                        if (MemWords1.First().Answer == UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            else if (UserAnswers.Category == "MemTestImages")
                            {
                                var MemImages1 = from MemImages in dataclass.MemmoryTestImageQuesCollections
                                                 where MemImages.Question == UserAnswers.Question && MemImages.QuestionID == UserAnswers.QuestionID
                                                 select MemImages;
                                if (MemImages1.Count() > 0)
                                {
                                    TestSecionID = int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = MemImages1.First().SectionName.ToString();
                                    sectionid = GetSectionId(sectionname);// int.Parse(MemImages1.First().SectionId.ToString());
                                    if (UserAnswers.Answer != null)
                                        if (MemImages1.First().Answer == UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            else if (UserAnswers.Category == "FillBlanks")
                            {
                                var FillQues1 = from FillQues in dataclass.QuestionCollections
                                                where FillQues.Question == UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                                select FillQues;
                                if (FillQues1.Count() > 0)
                                {
                                    TestSecionID = int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = FillQues1.First().SectionName.ToString();
                                    sectionid = GetSectionId(sectionname);// int.Parse(FillQues1.First().SectionId.ToString());
                                    if (UserAnswers.Answer != null)
                                        if (FillQues1.First().Option1 == UserAnswers.Answer || FillQues1.First().Option2 == UserAnswers.Answer ||
                                            FillQues1.First().Option3 == UserAnswers.Answer || FillQues1.First().Option4 == UserAnswers.Answer ||
                                            FillQues1.First().Option5 == UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            else if (UserAnswers.Category == "RatingType")
                            {
                                var FillQues1 = from FillQues in dataclass.QuestionCollections
                                                where FillQues.Question == UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                                select FillQues;
                                if (FillQues1.Count() > 0)
                                {
                                    TestSecionID = int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = FillQues1.First().SectionName.ToString();
                                    sectionid = GetSectionId(sectionname);// int.Parse(FillQues1.First().SectionId.ToString());
                                    ////if (sectionname == "Test2")
                                    ////    sectionid = 40;
                                    //if (UserAnswers.Answer != null)
                                    //    if (FillQues1.First().Answer == UserAnswers.Answer)
                                    //    {
                                            marks = int.Parse(UserAnswers.Answer.ToString());
                                            //// marks = ratingmark;
                                            // dataclass.ProcSectionMarks(userid, testid, sectionid, sectionname, marks);
                                            ////}
                                     //   }
                                }
                            }
                            else
                            {
                                var OtherQues1 = from OtherQues in dataclass.QuestionCollections
                                                 where OtherQues.Question == UserAnswers.Question && OtherQues.QuestionID == UserAnswers.QuestionID
                                                 select OtherQues;
                                if (OtherQues1.Count() > 0)
                                {
                                    TestSecionID = int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = OtherQues1.First().SectionName.ToString();
                                    sectionid = GetSectionId(sectionname);// int.Parse(OtherQues1.First().SectionId.ToString());
                                    if (UserAnswers.Answer != null)
                                        if (OtherQues1.First().Answer == UserAnswers.Answer)
                                            marks = 1;
                                }

                            }
                            
                                
                                Boolean exists = CheckExistence(sectionname);
                                if (exists == true)
                                {
                                    rowid = int.Parse(Session["RowID"].ToString());
                                    dr = dt.Rows[rowid];
                                    currentmarks = int.Parse(dr["TotalMarks"].ToString());
                                    currentmarks = currentmarks + marks;
                                    dr["TotalMarks"] = currentmarks;
                                    Session["RowID"] = null;
                                }
                                else
                                {
                                    //if (currentmarks > 0)
                                    //    marks = currentmarks;

                                    dr = dt.NewRow();
                                    dr["TestID"] = testid;
                                    dr["TestSectionID"] = TestSecionID.ToString();
                                    dr["SectionName"] = sectionname;
                                    dr["SectionID"] = sectionid;
                                    //
                                    //if (exists == false)
                                    dr["TotalMarks"] = marks;
                                    // else
                                    //   dr["TotalMarks"] = "0";
                                    //dr["BandDescription"] = "";
                                    dr["UserId"] = userid; dr["UserName"] = username;
                                    dt.Rows.Add(dr);
                                }
                            //}
                        }
                    }
                }
            ds = new DataSet();
            ds.Tables.Add(dt);
            GridView1.DataSource = ds.Tables[0];// dt;
            GridView1.DataBind();
            Session["dsDownloadtoExcel_refData"] = ds;// dt;
            if (GridView1.Rows.Count <= 0)
                lblMessage.Text = "No data found for export";
        }
        catch (Exception ex) { lblMessage.Text = ex.Message; }
    }

    private Boolean CheckExistence(string section)
    {
        Boolean result = false;
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["SectionName"].ToString() == section && dt.Rows[i]["UserId"].ToString()==userid.ToString())
                {
                    result = true;
                    Session["RowID"] = i;
                }
            }
        }
        return result;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (Session["dsDownloadtoExcel_refData"] != null)
            if (GridView1.Rows.Count > 0)
            {
                Session["dsDownloadtoExcel"] = Session["dsDownloadtoExcel_refData"];
                Response.Redirect("DownloadRefDataToExcel.aspx");
            }
        lblMessage.Text = "No data found for export";
    }
    protected void ddlOrganizationList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganizationList.SelectedIndex > 0)
        {
            if (Session["orgIndex_report"] != null)
                if (Session["orgIndex_report"].ToString() != ddlOrganizationList.SelectedIndex.ToString())
                {Session["testIndex_report"] = null; Session["dsDownloadtoExcel_refData"] = null;}
            Session["orgIndex_report"] = ddlOrganizationList.SelectedIndex.ToString();
            FillTestList();
        }
        else Session["orgIndex_report"] = null;
    }
    protected void ddlTestList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestList.SelectedIndex > 0)
        {
            testid = int.Parse(ddlTestList.SelectedValue);
            if (Session["testIndex_report"] != null)
                if (Session["testIndex_report"].ToString() != ddlTestList.SelectedIndex.ToString())
                    Session["dsDownloadtoExcel_refData"] = null;

            Session["testIndex_report"] = ddlTestList.SelectedIndex.ToString();
            GetReportGraphDetailsFromDB();
        }
        else Session["testIndex_report"] = null;
    }

    private int GetSectionId(string sectionname)
    {
        
        int secID = 0;
        string querystring1 = "select SectionId from SectionDetail where ParentId=0 and SectionName='" + sectionname + "'";

        DataSet ds1 = new DataSet();
        ds1 = clsClass.GetValuesFromDB(querystring1);
        if (ds1 != null)
            if (ds1.Tables.Count > 0)
                if (ds1.Tables[0].Rows.Count > 0)
                    if (ds1.Tables[0].Rows[0]["SectionId"] != "")
                        secID = int.Parse(ds1.Tables[0].Rows[0]["SectionId"].ToString());

        return secID;
    }

}
