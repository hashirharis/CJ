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

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.ComponentModel;
public partial class RptPrntCertify : System.Web.UI.Page
{
    DBManagementClass clsClasses = new DBManagementClass();
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    DataTable dt = new DataTable();
    int userid = 0; int testid = 0;
    DataTable dtEmptySessionList = new DataTable();
    string scoringType; string scoretype;
    int totalQuestionMark = 0;

    DataTable dtTestSection;
    Rectangle objRectangle_3 = new Rectangle(250, 200, 50, 25);

    // DataTable dtTestSection = new DataTable();
    DataTable dtvariablevalues = new DataTable();
    // string scoretype; int userid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        testid = int.Parse(Session["UserTestID_Report"].ToString());        

        bool valexists = false;
        if (Session["TestName"] != null)
        {
            lblTestName.Text = Session["TestName"].ToString();
            if (Session["ReportTypeName"] != null)
            { lblReportType.Text = Session["ReportTypeName"].ToString(); valexists = true; }
        }
        if (valexists == false)
        {
            var GetTestName = from testnamedet in dataclass.TestLists
                              where testnamedet.TestId == testid
                              select testnamedet;
            if (GetTestName.Count() > 0)
            {
                if (GetTestName.First().TestName != null)
                {
                    lblTestName.Text = GetTestName.First().TestName.ToString();
                    Session["TestName"] = GetTestName.First().TestName.ToString();
                }
                if (GetTestName.First().ReportType != null)
                {
                    lblReportType.Text = GetTestName.First().ReportType.ToString();
                    Session["ReportTypeName"] = GetTestName.First().ReportType.ToString();
                }
            }
        }


        userid = int.Parse(Session["UserId_Report"].ToString());
        if (Session["ScoringType"] != null)
            scoretype = Session["ScoringType"].ToString();
        if (Session["totalparts"] != null)
            txtParts.Text = Session["totalparts"].ToString();
        if (Session["displayvalues"] != null)
            txtValues.Text = Session["displayvalues"].ToString();
        if (Session["testsectionreportvalues"] != null)
            dtTestSection = (DataTable)Session["testsectionreportvalues"];

        FillUserReportDetails(); FillReportDescriptionDetails();

        
        DisplayIndividualCertificateDetails();
        this.Dispose();
    }

    
    private void FillReportDescriptionDetails()
    {
        lblReportDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");//.ToShortDateString();

        var SummaryDetails1 = from SummaryDetails in dataclass.ReportDescriptions
                              where SummaryDetails.TestId == testid
                              select SummaryDetails;
        if (SummaryDetails1.Count() > 0)
        {
            if (SummaryDetails1.First().Summary1 != null)
                lblSummary1.Text = SummaryDetails1.First().Summary1.ToString();
            if (SummaryDetails1.First().Summary2 != null)
                lblSummary2.Text = SummaryDetails1.First().Summary2.ToString();
            if (SummaryDetails1.First().DescriptiveReport != null)
                tcellDescriptionReport.InnerHtml = SummaryDetails1.First().DescriptiveReport.ToString();
            if (SummaryDetails1.First().Conclusion != null)
                lblConclusion.Text = SummaryDetails1.First().Conclusion.ToString();
            if (SummaryDetails1.First().ScoringType != null)
                scoringType = SummaryDetails1.First().ScoringType.ToString();
        }

    }

    private void FillUserReportDetails()
    {
        Table tblDisplay = new Table();
        TableCell tblCell = new TableCell();
        TableRow tblRow;
        Label label;
        tblDisplay = new Table();

        string querystring = "SELECT Name, OrganizationName,JobCategory,GroupName,Designation FROM View_UserProfileDetails WHERE UserID =  " + userid;

        DataSet ds = new DataSet();
        //da.Fill(ds);
        ds = clsClasses.GetValuesFromDB(querystring);
        tblDisplay.CellSpacing = 0;
        tblDisplay.CellPadding = 2;
        tblDisplay.BorderWidth = 2;
        //tblDisplay.Width = 400;
        tblDisplay.BorderColor = Color.Orange;
        Color clrTitleRow = ColorTranslator.FromHtml("#4F81BC");
        Color clrRow1 = ColorTranslator.FromHtml("#9FBCE6");
        Color clrRow2 = ColorTranslator.FromHtml("#C9DCFD");
        Color clrTextcolor = ColorTranslator.FromHtml("#56182F");

        if (ds != null)
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    //tblCell.Width = 200;
                    tblCell.ColumnSpan = 2;
                    tblCell.BackColor = clrTitleRow; //Color.DarkBlue; //Color.LightBlue;

                    label = new Label(); label.ForeColor = Color.White;
                    label.Text = "Demographic Profile";
                    label.Font.Size = 14;

                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblCell.Style.Value = "text-align: center; vertical-align: middle";
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    int tableftcellwidth = 100;
                    int tabrightcellwidth = 350;


                    tblRow = new TableRow(); tblRow.BackColor = clrRow1;
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2; //tblCell.Style.Add("padding-left:", "20px");
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; Name";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.Width = tabrightcellwidth;
                    tblCell.BorderWidth = 2;
                    label = new Label(); label.Font.Bold = true; label.ForeColor = clrTextcolor;
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["Name"].ToString();
                    lblName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 1:
                    //    {
                    tblRow = new TableRow(); tblRow.BackColor = clrRow2;
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; Organization";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label(); label.Font.Bold = true; label.ForeColor = clrTextcolor;
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["OrganizationName"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 2:
                    //    {
                    tblRow = new TableRow(); tblRow.BackColor = clrRow1;
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; JobCategory";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label(); label.Font.Bold = true; label.ForeColor = clrTextcolor;
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["JobCategory"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 3:
                    //    {
                    tblRow = new TableRow(); tblRow.BackColor = clrRow2;
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; Designation";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label(); label.Font.Bold = true; label.ForeColor = clrTextcolor;
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["Designation"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 4:
                    //    {
                    tblRow = new TableRow(); tblRow.BackColor = clrRow1;
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; GroupName";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label(); label.Font.Bold = true; label.ForeColor = clrTextcolor;
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["GroupName"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //            break;
                    //        }
                }

        tcellUserDetails.Controls.Clear();
        tcellUserDetails.Controls.Add(tblDisplay);
        // }
        //}

    }

    
    private string GetBenchMarkTestSectionwise(string curmark, string TESTSECTIONID)
    {
        int benchmark = 0;
        string remarks = "";
        try
        {
            string[] strValue = curmark.Split(new char[] { '.' });
            int mark = int.Parse(strValue[0]);

            // int secid = GetSectionId(variableName);
            string querystring1 = "";

            if (curmark == "0")
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestSectionResultBands WHERE TestID = " + testid;
                querystring1 += " AND (0 >= MarkFrom AND 0 <= MarkTo)";
                querystring1 += " AND SectionId = " + TESTSECTIONID;
            }
            else
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestSectionResultBands WHERE TestID = " + testid;
                querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//   MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
                querystring1 += " AND SectionId = " + TESTSECTIONID;
            }
            DataSet ds1 = new DataSet();
            ds1 = clsClasses.GetValuesFromDB(querystring1);
            if (ds1 != null)
                if (ds1.Tables.Count > 0)
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["BenchMark"] != "")
                        {
                            benchmark = int.Parse(ds1.Tables[0].Rows[0]["BenchMark"].ToString());
                            remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();
                        }
                    }
        }
        catch (Exception ex) { remarks = ""; }

        return remarks;
    }

    private void DisplayIndividualCertificateDetails()
    {
        //goToPrintPage(); return;

        string banddescription = GetBenchMarkTestSectionwise(dtTestSection.Rows[0]["TotalMark_sec"].ToString(), dtTestSection.Rows[0]["TestSectionID"].ToString());

        tcellBarGraph.InnerHtml = banddescription;
        return;
    }    


       
}
