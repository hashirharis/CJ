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
public partial class RptPrntIndvl : System.Web.UI.Page
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

        if (Session["variablereportvalues"] != null)
        {
            dtvariablevalues = (DataTable)Session["variablereportvalues"];
            GridView1.DataSource = dtvariablevalues;
            GridView1.DataBind();
        }

        DisplayIndividualColorGraph();

        this.Dispose();
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
        tblDisplay.BorderWidth = 2; tblDisplay.BorderColor = Color.Black;
        //tblDisplay.Width = 400;

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
                    tblCell.BackColor = clrTitleRow; //Color.LightBlue;
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

    private void FillReportDescriptionDetails()
    {
        lblReportDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");//.ToShortDateString();

        var SummaryDetails1 = from SummaryDetails in dataclass.ReportDescriptions
                              where SummaryDetails.TestId == testid
                              select SummaryDetails;
        if (SummaryDetails1.Count() > 0)
        {
            if (SummaryDetails1.First().Summary1 != null)
                //lblSummary1.Text = SummaryDetails1.First().Summary1.ToString();  
                tcellDescription1.InnerHtml = SummaryDetails1.First().Summary1.ToString();
            if (SummaryDetails1.First().Summary2 != null)
                //lblSummary2.Text = SummaryDetails1.First().Summary2.ToString();
                tcellDescription2.InnerHtml = SummaryDetails1.First().Summary2.ToString();
            if (SummaryDetails1.First().DescriptiveReport != null)
                tcellDescriptionReport.InnerHtml = SummaryDetails1.First().DescriptiveReport.ToString();
            if (SummaryDetails1.First().Conclusion != null)
                //lblConclusion.Text = SummaryDetails1.First().Conclusion.ToString();
                tcellConclution.InnerHtml = SummaryDetails1.First().Conclusion.ToString();
            if (SummaryDetails1.First().ScoringType != null)
                scoringType = SummaryDetails1.First().ScoringType.ToString();
        }

    }

    
    private void DisplayIndividualColorGraph()
    {
        Table tblDisplay = new Table();
        TableCell tblCell = new TableCell();
        TableRow tblRow;
        Label label;
        int i = 0;
        int rowid = 0;
        int totalmarks = 0;
        int sectionid = 0;
        // code to draw section(variable)wise bargraph 

        //tblDisplay.Width = 650;
        tblDisplay.CellPadding = 0;
        tblDisplay.CellSpacing = 0;
        tblDisplay.BorderWidth = 1;
        tblDisplay.BorderStyle = BorderStyle.Ridge;
        tblDisplay.BorderColor = Color.Black;
        string mark = "0"; string benchmark = "";
        float totalmark_variablewise = 0;
        int GRADE = 0;
        int gradecount = 0;

        for (int j = 0; j < GridView1.Rows.Count; j++)
        {

            if (GridView1.Rows[j].Cells[1].Text != "&nbsp;")
            {
                string TESTSECTIONID = GridView1.Rows[j].Cells[2].Text;
                //mark = "0";
                if (GridView1.Rows[j].Cells[4].Text != "0" && GridView1.Rows[j].Cells[4].Text != "&nbsp;")
                    totalmark_variablewise = float.Parse(GridView1.Rows[j].Cells[4].Text);
                //mark = GridView1.Rows[j].Cells[4].Text;

                string remarks = ""; string querystring1 = "";
                benchmark = "";

                int secid = GetSectionId(GridView1.Rows[j].Cells[1].Text);

                querystring1 = "SELECT TestID, BenchMark,DisplayName,MarkFrom,MarkTo FROM TestVariableResultBands WHERE TestID = " + testid + " AND TestSectionId = " + TESTSECTIONID;
                querystring1 += " AND VariableId = " + secid + "";


                DataSet ds1 = new DataSet(); bool benchmarkexists = false;
                ds1 = clsClasses.GetValuesFromDB(querystring1);
                if (ds1 != null)
                    if (ds1.Tables.Count > 0)
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            benchmarkexists = true;
                            gradecount = ds1.Tables[0].Rows.Count;
                            for (int g = 0; g < ds1.Tables[0].Rows.Count; g++)
                            {
                                if (totalmark_variablewise >= float.Parse(ds1.Tables[0].Rows[g]["MarkFrom"].ToString()) && totalmark_variablewise <= float.Parse(ds1.Tables[0].Rows[g]["MarkTo"].ToString()))
                                { GRADE = g + 1; break; }

                            }
                        }


                if (benchmarkexists == false)
                {

                    querystring1 = "SELECT TestID, BenchMark,DisplayName,MarkFrom,MarkTo FROM TestSectionResultBands WHERE TestID = " + testid;
                    querystring1 += " AND SectionId = " + TESTSECTIONID;

                    ds1 = new DataSet();
                    ds1 = clsClasses.GetValuesFromDB(querystring1);
                    if (ds1 != null)
                        if (ds1.Tables.Count > 0)
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                gradecount = ds1.Tables[0].Rows.Count;
                                for (int g = 0; g < ds1.Tables[0].Rows.Count; g++)
                                {
                                    if (totalmark_variablewise >= float.Parse(ds1.Tables[0].Rows[g]["MarkFrom"].ToString()) && totalmark_variablewise <= float.Parse(ds1.Tables[0].Rows[g]["MarkTo"].ToString()))
                                    { GRADE = g + 1; break; }

                                }

                            }
                }



                if (j == 0)
                {

                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    tblCell.RowSpan = 2;                   
                    tblCell.Style.Value = "vertical-align:middle;text-align:center;padding-left:10px;padding-right:10px;border: 1px ridge #000000;font-weight: bold";
                    tblCell.Text = "Variable Name";
                    tblRow.Cells.Add(tblCell);
                    tblCell = new TableCell();
                    tblCell.ColumnSpan = gradecount;
                    tblCell.Style.Value = "vertical-align:middle;text-align:center;border: 1px ridge #000000;font-weight: bold";
                    tblCell.Text = "Grade";
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    tblRow = new TableRow();
                    for (int c = 0; c < gradecount; c++)
                    {
                        tblCell = new TableCell();
                        tblCell.Text = (c + 1).ToString();
                        tblCell.Style.Value = "vertical-align:middle;text-align:center;border: 1px ridge #000000;font-weight: bold;width:70px";
                        tblRow.Cells.Add(tblCell);
                    }
                    tblDisplay.Rows.Add(tblRow);
                }


                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.Text = GridView1.Rows[j].Cells[1].Text;//"Variable name1";
                tblCell.Style.Value = "vertical-align:middle;text-align:left;padding-left:10px;padding-right:10px;border: 1px ridge #000000;font-weight: bold;font-size: 12px";
                tblRow.Cells.Add(tblCell);
                HtmlImage himage = new HtmlImage();
                string imgvalue = "width:100%;height:22px";
                for (int n = 0; n < gradecount; n++)
                {
                    tblCell = new TableCell();
                    himage = new HtmlImage(); himage.Style.Value = imgvalue;
                    if (gradecount == 2)
                    {
                        //tblCell.Text=i+1;
                        if (GRADE == n + 1)
                        {
                            if (GRADE == 1)
                            {                                
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptRed.JPG";
                                tblCell.Controls.Add(himage);

                            }
                            else if (GRADE == 2)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptGreen.JPG";
                                tblCell.Controls.Add(himage);
                            }
                        }
                    }
                    else if (gradecount == 3)
                    {
                        if (GRADE == n + 1)
                        {
                            if (GRADE == 1)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptRed.JPG";
                                tblCell.Controls.Add(himage);
                            }
                            else if (GRADE == 2)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptYellowOrange.JPG";
                                tblCell.Controls.Add(himage);
                            }
                            else if (GRADE == 3)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptGreen.JPG";
                                tblCell.Controls.Add(himage);
                            }
                        }
                    }
                    else if (gradecount == 4)
                    {
                        if (GRADE == n + 1)
                        {
                            if (GRADE == 1)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptRed.JPG";
                                tblCell.Controls.Add(himage);
                            }
                            else if (GRADE == 2)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptYellowOrange.JPG";
                                tblCell.Controls.Add(himage);
                            }
                            else if (GRADE == 3)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptGreenYellow.JPG";
                                tblCell.Controls.Add(himage);
                            }
                            else if (GRADE == 4)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptGreen.JPG";
                                tblCell.Controls.Add(himage);
                            }
                        }
                    }
                    else if (gradecount == 5)
                    {
                        if (GRADE == n + 1)
                        {
                            if (GRADE == 1)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptRed.JPG";
                                tblCell.Controls.Add(himage);
                            }
                            else if (GRADE == 2)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptRedOrange.JPG";
                                tblCell.Controls.Add(himage);
                            }
                            else if (GRADE == 3)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptYellowOrange.JPG";
                                tblCell.Controls.Add(himage);
                            }
                            else if (GRADE == 4)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptGreenYellow.JPG";
                                tblCell.Controls.Add(himage);
                            }
                            else if (GRADE == 5)
                            {
                                himage.Src = "~/QuestionAnswerFiles/ReportImages/rptGreen.JPG";
                                tblCell.Controls.Add(himage);
                            }

                        }
                    }

                    tblCell.Style.Value = "border: 1px ridge #000000;width:70px";
                    tblRow.Cells.Add(tblCell);
                }
                tblDisplay.Rows.Add(tblRow);
                // bipson 13-01-2011
            }
        }

        if (dtEmptySessionList.Rows.Count > 0)
        {
            for (int k = 0; k < dtEmptySessionList.Rows.Count; k++)
            {
                //TestVariableResultBands
                string remarks = "";
                benchmark = "";

                int secid = GetSectionId(dtEmptySessionList.Rows[k][0].ToString());

                string querystring1 = "SELECT TestID, BenchMark,DisplayName,MarkFrom,MarkTo FROM TestVariableResultBands WHERE TestID = " + testid;
                querystring1 += " AND VariableId = " + secid;
                DataSet ds1 = new DataSet();
                ds1 = clsClasses.GetValuesFromDB(querystring1);
                if (ds1 != null)
                    if (ds1.Tables.Count > 0)
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            if (gradecount == 0)
                                gradecount = ds1.Tables[0].Rows.Count;
                            for (int g = 0; g < ds1.Tables[0].Rows.Count; g++)
                            {
                                if (totalmark_variablewise >= int.Parse(ds1.Tables[0].Rows[g]["MarkFrom"].ToString()) && totalmark_variablewise <= int.Parse(ds1.Tables[0].Rows[g]["MarkTo"].ToString()))
                                { GRADE = g + 1; break; }
                            }
                        }


                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.Text = dtEmptySessionList.Rows[k][0].ToString();//"Variable name1";
                tblRow.Cells.Add(tblCell);

                for (int m = 0; m < gradecount; m++)
                {
                    tblCell = new TableCell();
                    HtmlImage himage = new HtmlImage(); himage.Style.Value = "width:100%;height:22px";
                    himage.Src = "~/QuestionAnswerFiles/ReportImages/rptRed.JPG";
                    tblCell.Controls.Add(himage);
                    tblRow.Cells.Add(tblCell);
                }
                tblDisplay.Rows.Add(tblRow);
            }
        }

        // tcellBarGraph.Width = "200";
        tcellBarGraph.Controls.Add(tblDisplay);
        // imgGraph.Visible = false;


    }
   

    private int GetBenchMarkVariablewise(string variableName, string curmark, string TESTSECTIONID)
    {
        int benchmark = 0;
        string remarks = "";
        try
        {
            string[] strValue = curmark.Split(new char[] { '.' });
            int mark = int.Parse(strValue[0]);

            int secid = GetSectionId(variableName);
            string querystring1 = "";
            if (curmark == "0")
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid + " AND TestSectionId = " + TESTSECTIONID;
                querystring1 += " AND (0 >= MarkFrom AND 0 <= MarkTo)";
                querystring1 += " AND VariableId = " + secid;
            }
            else
            {
                querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid + " AND TestSectionId = " + TESTSECTIONID;
                querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//   MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
                querystring1 += " AND VariableId = " + secid + "";
            }
            bool benchmarkexists = false;
            DataSet ds1 = new DataSet();
            ds1 = clsClasses.GetValuesFromDB(querystring1);
            if (ds1 != null)
                if (ds1.Tables.Count > 0)
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["BenchMark"] != "")
                            benchmark = int.Parse(ds1.Tables[0].Rows[0]["BenchMark"].ToString());
                        remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();
                        benchmarkexists = true;
                    }

            if (benchmarkexists == false)
            {
                benchmark = GetBenchMarkTestSectionwise(curmark, TESTSECTIONID);
            }

            // lblMessage.Text += querystring1;

        }
        catch (Exception ex) { benchmark = 0; }

        return benchmark;
    }

    private int GetBenchMarkTestSectionwise(string curmark, string TESTSECTIONID)
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
                            benchmark = int.Parse(ds1.Tables[0].Rows[0]["BenchMark"].ToString());
                        remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();

                    }
        }
        catch (Exception ex) { benchmark = 0; }

        return benchmark;
    }
   

    private int GetSectionId(string sectionname)
    {
        int secID = 0;
        string querystring1 = "select SectionId from SectionDetail where ParentId=0 and SectionName='" + sectionname + "'";

        DataSet ds1 = new DataSet();
        ds1 = clsClasses.GetValuesFromDB(querystring1);
        if (ds1 != null)
            if (ds1.Tables.Count > 0)
                if (ds1.Tables[0].Rows.Count > 0)
                    if (ds1.Tables[0].Rows[0]["SectionId"] != "")
                        secID = int.Parse(ds1.Tables[0].Rows[0]["SectionId"].ToString());

        return secID;
    }

    private string GetTestSectionName(int testsecionID)
    {
        string secName = "";
        var testsectionname = from testsecname in dataclass.TestSectionsLists
                              where testsecname.TestSectionId == testsecionID
                              select testsecname;
        if (testsectionname.Count() > 0)
        {
            if (testsectionname.First().SectionName != null && testsectionname.First().SectionName != "")
                secName = testsectionname.First().SectionName.ToString();
        }
        return secName;
    }
}
