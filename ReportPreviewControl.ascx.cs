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
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Drawing;


public partial class ReportPreviewControl : System.Web.UI.UserControl
{
    DBManagementClass clsClasses = new DBManagementClass();
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    int userid = 0;
    int testid = 0;
    DataSet ds;
    Table tblDisplay = new Table();
    TableCell tblCell = new TableCell();
    TableRow tblRow;
    Label label;
    int i = 0;
    int rowid = 0;
    int totalmarks = 0;
    int sectionid = 0;
    string querystring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
         userid = int.Parse(Session["UserId_Report"].ToString());
        int MemmoryTestImage = 0;
        int MemmoryTestText = 0;
        int QuesCollection = 0;
        string sectionname = "";
        //SqlConnection conn;
        //SqlCommand cmd;
        //SqlDataAdapter da;
        //DataSet ds;
        //Table tblDisplay = new Table();
        //TableCell tblCell = new TableCell();
        //TableRow tblRow;
        //Label label;
        //int i = 0;
        //int rowid = 0;
        //int totalmarks = 0;   
        //int sectionid = 0;
        //int testid = int.Parse(Session["TestType"].ToString());
        testid = int.Parse(Session["UserTestID_Report"].ToString());
        //string connectionstring = ConfigurationManager.ConnectionStrings["talentscoutConnectionString"].ConnectionString;
        //conn = new SqlConnection(connectionstring);
        //conn.Open();

        dataclass.DeleteSectionMarks(userid, testid);

        var UserAnsws1 = from UserAnsws in dataclass.EvaluationResults
                         where UserAnsws.UserId == userid && UserAnsws.TestId == testid
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
                                    where MemWords.Question == UserAnswers.Question
                                    select MemWords;
                    if (MemWords1.Count() > 0)
                    {
                        sectionname = MemWords1.First().SectionName.ToString();
                        

                        sectionid = int.Parse(MemWords1.First().SectionId.ToString());
                        //if (sectionname == "Test2")
                        //    sectionid = 40;
                        if (MemWords1.First().Answer == UserAnswers.Answer)
                        //{
                            marks = 1;                           
                        //}
                        dataclass.ProcSectionMarks(userid, testid, sectionid, sectionname, marks);

                    }
                }

                else if (UserAnswers.Category == "MemTestImages")
                {
                    var MemImages1 = from MemImages in dataclass.MemmoryTestImageQuesCollections
                                     where MemImages.Question == UserAnswers.Question
                                     select MemImages;
                    if (MemImages1.Count() > 0)
                    {
                        sectionname = MemImages1.First().SectionName.ToString();
                        sectionid = int.Parse(MemImages1.First().SectionId.ToString());
                        //if (sectionname == "Test2")
                        //    sectionid = 40;
                        if (MemImages1.First().Answer == UserAnswers.Answer)
                        //{
                            marks = 1;
                            dataclass.ProcSectionMarks(userid, testid, sectionid, sectionname, marks);
                       // }
                    }
                }
                else if (UserAnswers.Category == "FillBlanks")
                {
                    var FillQues1 = from FillQues in dataclass.QuestionCollections
                                    where FillQues.Question == UserAnswers.Question
                                    select FillQues;
                    if (FillQues1.Count() > 0)
                    {
                        sectionname = FillQues1.First().SectionName.ToString();
                        sectionid = int.Parse(FillQues1.First().SectionId.ToString());
                        //if (sectionname == "Test2")
                        //    sectionid = 40;
                        if (FillQues1.First().Option1 == UserAnswers.Answer || FillQues1.First().Option2 == UserAnswers.Answer ||
                            FillQues1.First().Option3 == UserAnswers.Answer || FillQues1.First().Option4 == UserAnswers.Answer ||
                            FillQues1.First().Option5 == UserAnswers.Answer)
                        //{
                            marks = 1;
                            dataclass.ProcSectionMarks(userid, testid, sectionid, sectionname, marks);
                       // }
                    }
                }
                else if (UserAnswers.Category == "RatingType")
                {
                    var FillQues1 = from FillQues in dataclass.QuestionCollections
                                    where FillQues.Question == UserAnswers.Question
                                    select FillQues;
                    if (FillQues1.Count() > 0)
                    {
                        sectionname = FillQues1.First().SectionName.ToString();
                        sectionid = int.Parse(FillQues1.First().SectionId.ToString());
                        //if (sectionname == "Test2")
                        //    sectionid = 40;
                        if (FillQues1.First().Answer == UserAnswers.Answer)
                        //{
                            marks = int.Parse(UserAnswers.Answer.ToString());
                           // marks = ratingmark;
                            dataclass.ProcSectionMarks(userid, testid, sectionid, sectionname, marks);
                        //}
                    }
                }
                else
                {
                    var OtherQues1 = from OtherQues in dataclass.QuestionCollections
                                     where OtherQues.Question == UserAnswers.Question
                                     select OtherQues;
                    if (OtherQues1.Count() > 0)
                    {
                        sectionname = OtherQues1.First().SectionName.ToString();
                        sectionid = int.Parse(OtherQues1.First().SectionId.ToString());
                        //if (sectionname == "Test2")
                        //    sectionid = 40;
                        if (OtherQues1.First().Answer == UserAnswers.Answer)
                        //{
                            marks = 1;
                            dataclass.ProcSectionMarks(userid, testid, sectionid, sectionname, marks);
                       // }
                    }
                }
            }
        }
        
      
        if (Session["DiagramType"] != null)
        {
            if (Session["DiagramType"].ToString() == "PieGraph")
            {
                ReportViewer1.Visible = true;
             //   ReportViewer2.Visible = false;
            }
            else
            {
                ReportViewer1.Visible = false;
             //   ReportViewer2.Visible = true;
            }
        }

        querystring = "SELECT TestID, SectionID,SectionName,Marks FROM SectionMarks WHERE UserID =  " + userid;
        querystring += " AND TestID = " + testid;
        //cmd = new SqlCommand(querystring, conn);
        //da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        //da.Fill(ds);
        ds = clsClasses.GetValuesFromDB(querystring);

        if (Session["DiagramType"].ToString() == "PieGraph")
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource dsource1 = new ReportDataSource("DataSet1_SectionMarks", ds.Tables[0]);// gvwSectionMarks.DataSource);
            ReportViewer1.LocalReport.DataSources.Add(dsource1);
        }
        else
        {
            int markfrom = 0;
            int markto = 0;
            string remarks = "";
            int benchmark = 0;
            totalmarks = 1;
            //sectionid = 38;


            Table Report = new Table();
            TableCell ReportCell = new TableCell();
            TableRow ReportRow = new TableRow();

            Report.CellSpacing = 0;
            Report.CellPadding = 2;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                totalmarks = int.Parse(ds.Tables[0].Rows[i]["Marks"].ToString());
                sectionid = int.Parse(ds.Tables[0].Rows[i]["SectionID"].ToString());
                sectionname = ds.Tables[0].Rows[i]["SectionName"].ToString();

                querystring = "SELECT TestID, BenchMark,Marks ,DisplayName FROM ViewBenchMarks WHERE TestID = " + testid;
                querystring += " AND MarkFrom <= " + totalmarks + " AND ";
                querystring += " MarkTo >= " + totalmarks;
                querystring += " AND SectionName = '" + sectionname + "'";
                //querystring += " AND SectionID = " + sectionid;
                //cmd = new SqlCommand(querystring, conn);
                //da = new SqlDataAdapter(cmd);
                DataSet ds1 = new DataSet();
                //da.Fill(ds1);
                ds1 = clsClasses.GetValuesFromDB(querystring);

                remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();

                ReportViewer rptViewer1 = new ReportViewer();
                rptViewer1.ShowBackButton = false;
                rptViewer1.ShowCredentialPrompts = false;
                rptViewer1.ShowDocumentMapButton = false;
                rptViewer1.ShowExportControls = false;
                rptViewer1.ShowFindControls = false;
                rptViewer1.ShowPageNavigationControls = false;
                rptViewer1.ShowParameterPrompts = false;
                rptViewer1.ShowPrintButton = false;
                rptViewer1.ShowPromptAreaButton = false;
                rptViewer1.ShowRefreshButton = false;
                rptViewer1.Width = 948;
                rptViewer1.ShowToolBar = false;
                rptViewer1.ShowZoomControl = false;
                rptViewer1.SizeToReportContent = true;
                rptViewer1.AsyncRendering = false;

                rptViewer1.LocalReport.ReportPath = "Report2.rdlc";

                rptViewer1.LocalReport.DataSources.Clear();
                ReportDataSource dsource2 = new ReportDataSource("BenchMarks_ViewBenchMarks", ds1.Tables[0]);
                rptViewer1.LocalReport.DataSources.Add(dsource2);
                rptViewer1.LocalReport.Refresh();
                rptViewer1.Visible = true;

                ReportRow = new TableRow();
                ReportCell = new TableCell();
                ReportCell.Style.Value = "vertical-align: middle; text-align: center";

                Label lblSection = new Label();
                lblSection.Font.Size = 15;
                lblSection.Font.Bold = true;

                lblSection.Text = sectionname;
                ReportCell.Controls.Add(lblSection);

                ReportRow.Cells.Add(ReportCell);
                Report.Rows.Add(ReportRow);

                ReportRow = new TableRow();
                ReportCell = new TableCell();
                ReportCell.Style.Value = "vertical-align: middle; text-align: center";
                ReportCell.Controls.Add(rptViewer1);
                ReportRow.Cells.Add(ReportCell);
                Report.Rows.Add(ReportRow);

                Label lblRemarks = new Label();
                lblRemarks.Font.Size = 15;
                lblRemarks.Font.Bold = true;
                lblRemarks.Text = remarks;
                ReportCell = new TableCell();
                ReportCell.Style.Value = "vertical-align: middle; text-align: center";
                ReportRow = new TableRow();

                ReportCell.Controls.Add(lblRemarks);
                ReportRow.Cells.Add(ReportCell);
                Report.Rows.Add(ReportRow);

                ReportRow = new TableRow();
                ReportCell = new TableCell();
                lblRemarks = new Label(); lblRemarks.Text = ""; lblRemarks.Width = 10;
                ReportCell.Controls.Add(lblRemarks);
                ReportRow.Cells.Add(ReportCell);
                Report.Rows.Add(ReportRow);               

            }
            pnlreportview.Controls.Add(Report);

        }
        FillUserDetails();
        FillReportDesctiptionDetails();

    }


    private void FillUserDetails()
    {
        //  string querystring = "SELECT Name, Organization,JobCategory,GroupName,Designation FROM UserProfileView WHERE UserID =  " + userid;
         querystring = "SELECT Name, OrganizationName,JobCategory,GroupName,Designation FROM View_UserProfileDetails WHERE UserID =  " + userid;

        ds = new DataSet();
        
        ds = clsClasses.GetValuesFromDB(querystring);        
        tblDisplay.CellSpacing = 0;
        tblDisplay.CellPadding = 2;
        tblDisplay.BorderWidth = 2;
       
        if (ds != null)
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    //tblCell.Width = 200;
                    tblCell.ColumnSpan = 2;
                    tblCell.BackColor = Color.LightBlue;
                    label = new Label();
                    label.Text = "Candidate’s DemographicDetails";
                    label.Font.Size = 14;

                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblCell.Style.Value = "text-align: center; vertical-align: middle";
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    int tableftcellwidth = 75;
                    int tabrightcellwidth = 300;

                    //for (int count = 0; count < 5; count++)
                    //{
                    //    switch (count)
                    //    {
                    //        case 0:
                    //            {
                                    tblRow = new TableRow();
                                    tblCell = new TableCell();
                                    tblCell.BorderWidth = 2;
                                    tblCell.Width = tableftcellwidth;
                                    label = new Label();
                                    label.Text = "Name";
                                    label.Font.Bold = true;
                                    tblCell.Controls.Add(label);
                                    tblRow.Cells.Add(tblCell);

                                    tblCell = new TableCell();
                                    tblCell.Width = tabrightcellwidth;
                                    tblCell.BorderWidth = 2;
                                    label = new Label();
                                    label.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                                    lblName.Text = ds.Tables[0].Rows[0]["Name"].ToString();//display at header
                                    tblCell.Controls.Add(label);
                                    tblRow.Cells.Add(tblCell);
                                    tblDisplay.Rows.Add(tblRow);

                            //        break;
                            //    }
                            //case 1:
                            //    {
                                    tblRow = new TableRow();
                                    tblCell = new TableCell();
                                    tblCell.BorderWidth = 2;
                                    tblCell.Width = tableftcellwidth;
                                    label = new Label();
                                    label.Text = "Organization";
                                    label.Font.Bold = true;
                                    tblCell.Controls.Add(label);
                                    tblRow.Cells.Add(tblCell);

                                    tblCell = new TableCell();
                                    tblCell.BorderWidth = 2;
                                    tblCell.Width = tabrightcellwidth;
                                    label = new Label();
                                    label.Text = ds.Tables[0].Rows[0]["OrganizationName"].ToString();

                                    tblCell.Controls.Add(label);
                                    tblRow.Cells.Add(tblCell);
                                    tblDisplay.Rows.Add(tblRow);

                            //        break;
                            //    }
                            //case 2:
                            //    {
                                    tblRow = new TableRow();
                                    tblCell = new TableCell();
                                    tblCell.BorderWidth = 2;
                                    tblCell.Width = tableftcellwidth;
                                    label = new Label();
                                    label.Text = "JobCategory";
                                    label.Font.Bold = true;
                                    tblCell.Controls.Add(label);
                                    tblRow.Cells.Add(tblCell);

                                    tblCell = new TableCell();
                                    tblCell.BorderWidth = 2;
                                    tblCell.Width = tabrightcellwidth;
                                    label = new Label();
                                    label.Text = ds.Tables[0].Rows[0]["JobCategory"].ToString();

                                    tblCell.Controls.Add(label);
                                    tblRow.Cells.Add(tblCell);
                                    tblDisplay.Rows.Add(tblRow);

                            //        break;
                            //    }
                            //case 3:
                            //    {
                                    tblRow = new TableRow();
                                    tblCell = new TableCell();
                                    tblCell.BorderWidth = 2;
                                    tblCell.Width = tableftcellwidth;
                                    label = new Label();
                                    label.Text = "Designation";
                                    label.Font.Bold = true;
                                    tblCell.Controls.Add(label);
                                    tblRow.Cells.Add(tblCell);

                                    tblCell = new TableCell();
                                    tblCell.BorderWidth = 2;
                                    tblCell.Width = tabrightcellwidth;
                                    label = new Label();
                                    label.Text = ds.Tables[0].Rows[0]["Designation"].ToString();

                                    tblCell.Controls.Add(label);
                                    tblRow.Cells.Add(tblCell);
                                    tblDisplay.Rows.Add(tblRow);

                            //        break;
                            //    }
                            //case 4:
                            //    {
                                    tblRow = new TableRow();
                                    tblCell = new TableCell();
                                    tblCell.BorderWidth = 2;
                                    tblCell.Width = tableftcellwidth;
                                    label = new Label();
                                    label.Text = "GroupName";
                                    label.Font.Bold = true;
                                    tblCell.Controls.Add(label);
                                    tblRow.Cells.Add(tblCell);

                                    tblCell = new TableCell();
                                    tblCell.BorderWidth = 2;
                                    tblCell.Width = tabrightcellwidth;
                                    label = new Label();
                                    label.Text = ds.Tables[0].Rows[0]["GroupName"].ToString();

                                    tblCell.Controls.Add(label);
                                    tblRow.Cells.Add(tblCell);
                                    tblDisplay.Rows.Add(tblRow);

                                //    break;
                                //}
                        //}
                    //}
                }


        tcellUserDetails.Controls.Clear();
        tcellUserDetails.Controls.Add(tblDisplay);

    }

    private void FillReportDesctiptionDetails()
    {
        var SummaryDetails1 = from SummaryDetails in dataclass.ReportDescriptions
                              where
                                  SummaryDetails.TestId == testid
                              select SummaryDetails;
        if (SummaryDetails1.Count() > 0)
        {
            lblSummary1.Text = SummaryDetails1.First().Summary1.ToString();           
            lblSummary2.Text = SummaryDetails1.First().Summary2.ToString();
            lblConclusion.Text = SummaryDetails1.First().Conclusion.ToString();
        }

    }

}
